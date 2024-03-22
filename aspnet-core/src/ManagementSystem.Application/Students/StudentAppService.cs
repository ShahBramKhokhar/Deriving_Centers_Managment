using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Core.Helpers;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Classess;
using ManagementSystem.Common.Dto;
using ManagementSystem.Students.Dto;
using ManagementSystem.Studentss;
using ManagementSystem.Users;
using ManagementSystem.Users.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Students
{
    //[AbpAuthorize(PermissionNames.Pages_StudentAdmin)]
    public class StudentAppService : ManagementSystemServiceBase, IStudentAppService
    {
        private readonly IRepository<Student> _repository;
        private readonly IUserAppService _userAppService;
        private readonly ImageHelperService _imageHelperService;
        private readonly IRepository<StudentsClasses> _studentClassRepository;
        private readonly IRepository<ManagementSystem.Classess.ClassArea> _classAreaRepository;
        IRepository<User, long> _userRepository;


        public StudentAppService(
            IRepository<Student> repository,
            IUserAppService userAppService,
            ImageHelperService imageHelperService,
            IRepository<StudentsClasses> studentClassRepository,
            IRepository<ManagementSystem.Classess.ClassArea> classAreaRepository,
        IRepository<User, long> userRepository
            )
        {
            _repository = repository;
            _userAppService = userAppService;
            _userRepository = userRepository;
            _imageHelperService = imageHelperService;
            _studentClassRepository = studentClassRepository;
            _classAreaRepository = classAreaRepository;
        }

        public async Task Create(CreateStudentDto input)
        {
            string[] StudentRole = new string[1];
            StudentRole[0] = AppConstants.StudentRole;
            var tenantId = AbpSession.TenantId ?? AppConstants.DefaultTenantId;
            input.TenantId = tenantId;

            ImageUploadDto uploadResult = new ImageUploadDto();
            if (!string.IsNullOrWhiteSpace(input.ImageBase64String))
                uploadResult = await _imageHelperService.AddImageAsync(input.ImageBase64String);

            var userDto = new CreateUserDto()
            {
                IsActive = input.IsActive,
                EmailAddress = input.EmailAddress,
                Name = input.Name,
                Password = input.Password,
                RoleNames = StudentRole,
                Surname = input.Surname,
                UserName = input.UserName,
                Address = input.Address,
                CNIC = input.CNIC,
                PhoneNumber = input.PhoneNumber,
                PicturePublicId  = uploadResult.PublicId,
                PictureUrl = uploadResult.Url,
            };

            var user = await _userAppService.CreateAsync(userDto);

            var Student = new Student()
            {
                TenantId = tenantId,
                CreationTime = new System.DateTime(),
                LastModificationTime = new System.DateTime(),
                UserId = user.Id,
                TotalFees = input.TotalFees,
                CenterId = input.CenterId,
            };


            await _repository.InsertAsync(Student);

            UnitOfWorkManager.Current.SaveChanges();

        }

        public async Task Delete(EntityDto input)
        {

            var data = await _repository.GetAsync(input.Id);
            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.StudentNotFound);
            }
            if (data.UserId > 0)
            {
                var user = await _userRepository.FirstOrDefaultAsync((long)data.UserId);
                await _userRepository.DeleteAsync(user);
            }

            await _repository.DeleteAsync(data);

            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<ListResultDto<StudentDto>> GetAll()
        {
            //var data = (
            //from item in _repository.GetAll()
            //select StudentToStudentDto(item)
            //).ToList();
            var data = (
             _repository.GetAll().Select( item  => new StudentDto
             {
                 Id = item.Id,
                 UserId = item.UserId,
                 Name = item.StudentUser.Name,
                 Surname = item.StudentUser.Surname,
                 Address = item.StudentUser.Address,
                 CNIC = item.StudentUser.CNIC,
                 Country = item.StudentUser.Country,
                 EmailAddress = item.StudentUser.EmailAddress,
                 FullName = item.StudentUser.FullName,
                 PhoneNumber = item.StudentUser.PhoneNumber,
                 PicturePublicId = item.StudentUser.PicturePublicId,
                 PictureUrl = item.StudentUser.PictureUrl,
                 PostalCode = item.StudentUser.PostalCode,
                 CreatorUserId = item.CreatorUserId,
                 CreationTime = item.CreationTime,
                 IsActive = item.StudentUser.IsActive,
                 UserName = item.StudentUser.UserName,
             }   
            )).ToList();

            return new ListResultDto<StudentDto>(data);

        }

        private StudentDto StudentToStudentDto(Student item, ManagementSystem.Classess.ClassArea classArea)
        {


            return new StudentDto
            {
                Id = item.Id,
                UserId = item.UserId,
                Name = item.StudentUser.Name,
                Surname = item.StudentUser.Surname,
                Address = item.StudentUser.Address,
                CNIC = item.StudentUser.CNIC,
                Country = item.StudentUser.Country,
                EmailAddress = item.StudentUser.EmailAddress,
                FullName = item.StudentUser.FullName,
                PhoneNumber = item.StudentUser.PhoneNumber,
                PicturePublicId = item.StudentUser.PicturePublicId,
                PictureUrl = item.StudentUser.PictureUrl,
                PostalCode = item.StudentUser.PostalCode,
                CreatorUserId = item.CreatorUserId,
                CreationTime = item.CreationTime,
                IsActive = item.StudentUser.IsActive,
                UserName = item.StudentUser.UserName,
                ClassName = classArea == null?"":classArea.Title,
                TotalFees = item.TotalFees,
                // Center = item.StudentCenter,
                //  CenterId = (int)item.CenterId,
                

            };
        }

        public async Task<StudentDto> GetById(int id)
        {
            var data = await _repository.GetAsync(id);
            var user = await _userRepository.GetAsync((long)data.UserId);
            data.StudentUser = user;
            var studentClass = _studentClassRepository.GetAllIncluding().OrderByDescending(x => x.CreationTime).FirstOrDefault(x => x.StudentId == id);
            if(studentClass == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.StudentInClassNotFound);
            }
            var classArea = _classAreaRepository.GetAllIncluding().FirstOrDefault(x => x.Id == studentClass.ClassId);
            var StudentDto = StudentToStudentDto(data, classArea);

            return StudentDto;
        }

        public async Task<PagedResultDto<StudentDto>> GetPagedResultAsync(PagedStudentResultRequestDto input)
        {
            var userList = await _userRepository.GetAllListAsync();
            var query = _repository.GetAll();
            query = ApplyFilters(input, query);
            IQueryable<StudentDto> selectQuery = GetSelectQuery(query);
            return await selectQuery.GetPagedResultAsync(input.SkipCount, input.MaxResultCount);
        }

        public async Task Update(StudentDto input)
        {
            var data = await _repository.GetAsync(input.Id);


            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.StudentNotFound);
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
            //ObjectMapper.Map(input, data);
        }


        #region Private Methods
        private static IQueryable<Student> ApplyFilters(PagedStudentResultRequestDto input, IQueryable<Student> query)
        {
            
            if (string.IsNullOrWhiteSpace(input.Keyword) == false)
                query = query.Where(g => g.StudentUser.Name.Contains(input.Keyword));
            return query;

          
        }
        private static IQueryable<StudentDto> GetSelectQuery(IQueryable<Student> query)
        {
            
            return query.Select(item => new StudentDto
            {
                Id = item.Id,
                UserId = item.UserId,
                Name = item.StudentUser.Name,
                Surname = item.StudentUser.Surname,
                Address = item.StudentUser.Address,
                CNIC = item.StudentUser.CNIC,
                Country = item.StudentUser.Country,
                EmailAddress = item.StudentUser.EmailAddress,
                FullName = item.StudentUser.FullName,
                PhoneNumber = item.StudentUser.PhoneNumber,
                PicturePublicId = item.StudentUser.PicturePublicId,
                PictureUrl = item.StudentUser.PictureUrl,
                PostalCode = item.StudentUser.PostalCode,
                CreatorUserId = item.CreatorUserId,
                CreationTime = item.CreationTime,
                IsActive = item.StudentUser.IsActive,
                UserName = item.StudentUser.UserName,
                TotalFees = item.TotalFees

            });
        }



        #endregion


    }
}
