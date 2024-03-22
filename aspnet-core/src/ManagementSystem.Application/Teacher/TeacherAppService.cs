using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Core.Helpers;
using ManagementSystem.Authorization;
using ManagementSystem.Authorization.Users;
using ManagementSystem.teacher;
using ManagementSystem.Teacher.Dto;
using ManagementSystem.Teachers.Dto;
using ManagementSystem.Users;
using ManagementSystem.Users.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Teacher
{
    [AbpAuthorize(PermissionNames.Pages_Teachers)]
    public class TeacherAppService : ManagementSystemServiceBase , ITeacherAppService 
    {
        private readonly IRepository<ManagementSystem.Teacher.Teacher> _repository;
        private readonly IUserAppService _userAppService;
        IRepository<User, long> _userRepository;
        public TeacherAppService(
            IRepository<ManagementSystem.Teacher.Teacher> repository,
             IUserAppService userAppService,
            IRepository<User, long> userRepository
            ) 
        {

            _repository = repository;
            _userAppService = userAppService;
            _userRepository = userRepository;
        }

        public async Task Create(CreateTeacherDto input)
        {
            string[] role = new string[1];

            role[0] = AppConstants.TeacherRole;

            var tenantId = AbpSession.TenantId ?? AppConstants.DefaultTenantId;
            input.TenantId = tenantId;

            var userDto = new CreateUserDto()
            {
                IsActive = input.IsActive,
                EmailAddress = input.EmailAddress,
                Name = input.Name,
                Password = input.Password,
                RoleNames = role,
                Surname = input.Surname,
                UserName = input.UserName,
                Address = input.Address,


            };


            var user = await _userAppService.CreateAsync(userDto);

            var teacher = new Teacher()
            {
                TenantId = tenantId,
                CreationTime = new System.DateTime(),
                LastModificationTime = new System.DateTime(),
                UserId = user.Id,

            };


            await _repository.InsertAsync(teacher);

            UnitOfWorkManager.Current.SaveChanges();


        }


        

        public async Task Delete(EntityDto input)
        {

            var data = await _repository.GetAsync(input.Id);
            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.TeacherNotFound);
            }
            if (data.UserId > 0)
            {
                var user = await _userRepository.FirstOrDefaultAsync((long)data.UserId);
                await _userRepository.DeleteAsync(user);
            }

            await _repository.DeleteAsync(data);

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<ListResultDto<TeacherDto>> GetAll()
        {


            //var data = (
            //from item in _repository.GetAll()
            //select TeacherToTeacherDto(item)
            //).ToList();

            var data = (
            _repository.GetAll().Select(item => new TeacherDto
            {

                Id = item.Id,
                UserId = item.UserId,
                Name = item.TeacherUser.Name,
                Surname = item.TeacherUser.Surname,
                Address = item.TeacherUser.Address,
                CNIC = item.TeacherUser.CNIC,
                Country = item.TeacherUser.Country,
                EmailAddress = item.TeacherUser.EmailAddress,
                FullName = item.TeacherUser.FullName,
                PhoneNumber = item.TeacherUser.PhoneNumber,
                PicturePublicId = item.TeacherUser.PicturePublicId,
                PictureUrl = item.TeacherUser.PictureUrl,
                PostalCode = item.TeacherUser.PostalCode,
                CreatorUserId = item.CreatorUserId,
                CreationTime = item.CreationTime,
                IsActive = item.TeacherUser.IsActive,
                UserName = item.TeacherUser.UserName
            }
           )).ToList();


            return new ListResultDto<TeacherDto>(data);

        }

        private TeacherDto TeacherToTeacherDto(Teacher item)
        {


            return new TeacherDto
            {
                Id = item.Id,
                UserId = item.UserId,
                Name = item.TeacherUser.Name,
                Surname = item.TeacherUser.Surname,
                Address = item.TeacherUser.Address,
                CNIC = item.TeacherUser.CNIC,
                Country = item.TeacherUser.Country,
                EmailAddress = item.TeacherUser.EmailAddress,
                FullName = item.TeacherUser.FullName,
                PhoneNumber = item.TeacherUser.PhoneNumber,
                PicturePublicId = item.TeacherUser.PicturePublicId,
                PictureUrl = item.TeacherUser.PictureUrl,
                PostalCode = item.TeacherUser.PostalCode,
                CreatorUserId = item.CreatorUserId,
                CreationTime = item.CreationTime,
                IsActive = item.TeacherUser.IsActive,
                UserName = item.TeacherUser.UserName

            };
        }

        public async Task<TeacherDto> GetById(int id)
        {
            var data = await _repository.GetAsync(id);
            var user = await _userRepository.GetAsync((long)data.UserId);
            data.TeacherUser = user;
            var TeacherDto = TeacherToTeacherDto(data);

            return TeacherDto;
        }

        public async Task<PagedResultDto<TeacherDto>> GetPagedResultAsync(PagedTeacherResultRequestDto input)
        {
            var userList = await _userRepository.GetAllListAsync();
            var query = _repository.GetAll();
            query = ApplyFilters(input, query);
            IQueryable<TeacherDto> selectQuery = GetSelectQuery(query);
            return await selectQuery.GetPagedResultAsync(input.SkipCount, input.MaxResultCount);
        }

        public async Task Update(TeacherDto input)
        {
            var data = await _repository.GetAsync(input.Id);


            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.TeacherNotFound);
            }

            if (data.UserId > 0)
            {
                var userData = await _userRepository.GetAsync((long)data.UserId);

                // update
                await _userRepository.UpdateAsync(userData);
                await UnitOfWorkManager.Current.SaveChangesAsync();

            }

            await _repository.UpdateAsync(data);

            await UnitOfWorkManager.Current.SaveChangesAsync();
           // ObjectMapper.Map(input, data);
        }


        #region Private Methods
        private static IQueryable<Teacher> ApplyFilters(PagedTeacherResultRequestDto input, IQueryable<Teacher> query)
        {
            if (string.IsNullOrWhiteSpace(input.Keyword) == false)
                query = query.Where(g => g.TeacherUser.Name.Contains(input.Keyword));
            return query;
        }
        private static IQueryable<TeacherDto> GetSelectQuery(IQueryable<Teacher> query)
        {
            return query.Select(item => new TeacherDto
            {
                Id = item.Id,
                UserId = item.UserId,
                Name = item.TeacherUser.Name,
                Surname = item.TeacherUser.Surname,
                Address = item.TeacherUser.Address,
                CNIC = item.TeacherUser.CNIC,
                Country = item.TeacherUser.Country,
                EmailAddress = item.TeacherUser.EmailAddress,
                FullName = item.TeacherUser.FullName,
                PhoneNumber = item.TeacherUser.PhoneNumber,
                PicturePublicId = item.TeacherUser.PicturePublicId,
                PictureUrl = item.TeacherUser.PictureUrl,
                PostalCode = item.TeacherUser.PostalCode,
                CreatorUserId = item.CreatorUserId,
                CreationTime = item.CreationTime,
                IsActive = item.TeacherUser.IsActive,
                UserName = item.TeacherUser.UserName

            });
        }



        #endregion


    }
}
