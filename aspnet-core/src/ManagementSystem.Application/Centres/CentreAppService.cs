using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Core.Helpers;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Centres.Dto;
using ManagementSystem.Common.Dto;
using ManagementSystem.Students;
using ManagementSystem.Users;
using ManagementSystem.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Centres
{
    //[AbpAuthorize(PermissionNames.Pages_CenterAdmin)]
    public class CenterAppService : ManagementSystemServiceBase , ICentreAppService 
    {
        private readonly IRepository<Center> _repository;
        private readonly IUserAppService _userAppService;
        IRepository<User, long> _userRepository;
        private readonly IRepository<FeeRecords.FeesRecord> _feesRecordRepository;
        private readonly IRepository<Student> _studentRepository;

        public CenterAppService(
            IRepository<Center> repository,
            IUserAppService userAppService,
            IRepository<User, long> userRepository,
            IRepository<FeeRecords.FeesRecord> feesRecordRepository,
            IRepository<Student> studentRepository
            ) 
        {
            _repository = repository;
            _userAppService = userAppService;
            _userRepository = userRepository;
            _feesRecordRepository = feesRecordRepository;
            _studentRepository = studentRepository;
        }

        public async Task Create(CreateCentreDto input)
        {
            var tenantId = AbpSession.TenantId ?? AppConstants.DefaultTenantId;
            var center = new Center()
            {
                TenantId = tenantId,
                CreationTime = new System.DateTime(),
                LastModificationTime = new System.DateTime(),
                Name = input.Name,
                CreatorUserId = AbpSession.UserId,
            };
            
            await _repository.InsertAsync(center);
            UnitOfWorkManager.Current.SaveChanges();

        }

        public async Task Delete(EntityDto input)
        {
            
            var data = await _repository.GetAsync(input.Id);
            if (data == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.CenterNotFound);
            }
            await _repository.DeleteAsync(data);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async Task<ListResultDto<CentreDto>> GetAll()
        {
            //var data = (
            //from item in _repository.GetAll()
            //select CenterToCenterDto(item)
            //).ToList();

            var data = (
             _repository.GetAll().Select(item => new CentreDto
             {
                 Id = item.Id,
                 Name = item.Name,
                 CreatedBy = item.CreatorUser.Name,
             }
            )).ToList();
            return new ListResultDto<CentreDto>(data);
        }

        //public async Task<ListResultDto<LookUpDto<int>>> GetAll()
        //{
        //    return await _repository.GetAll().GetLookupResultAsync<Center, int>();
        //}

        private   CentreDto CenterToCenterDto(Center centre)
        {


            //return new CentreDto
            //{
            //    Id = item.Id,
            //    UserId = item.UserId,
            //    Name = item.CenterUser.Name,
            //    Surname = item.CenterUser.Surname,
            //    Address = item.CenterUser.Address,
            //    CNIC = item.CenterUser.CNIC,
            //    Country = item.CenterUser.Country,
            //    EmailAddress = item.CenterUser.EmailAddress,
            //    FullName = item.CenterUser.FullName,
            //    PhoneNumber = item.CenterUser.PhoneNumber,
            //    PicturePublicId = item.CenterUser.PicturePublicId,
            //    PictureUrl = item.CenterUser.PictureUrl,
            //    PostalCode = item.CenterUser.PostalCode,
            //    CreatorUserId = item.CreatorUserId,
            //    CreationTime = item.CreationTime,
            //    IsActive = item.CenterUser.IsActive,
            //    UserName = item.CenterUser.UserName

            //};
            return new CentreDto
            {
                Id = centre.Id,
                Name = centre.Name,
                //CreatedBy = centre.CreatorUser.FullName,
            };
        }

        //public async Task<CentreDto> GetById(int id)
        //{
        //    var data = await _repository.GetAsync(id);
        //    var user = await _userRepository.GetAsync((long)data.UserId);
        //    data.CenterUser = user;
        //    var centerDto =  CenterToCenterDto(data);
            
        //    return centerDto;
        //}
        public async Task<CentreDto> GetById(int id)
        {
            var centre = await _repository.GetAsync(id);
            var centerDto = CenterToCenterDto(centre);

            return centerDto;
        }
        public async Task<PagedResultDto<CentreDto>> GetPagedResultAsync(PagedCentreResultRequestDto input)
        {
            int? centerId = null;
            var userId = AbpSession.UserId;
            if(userId != AppConstants.DefaultUserId1 && userId != AppConstants.DefaultUserId2)
                centerId = _userRepository.Get(userId.Value).CenterId;
            var query = _repository.GetAll();
            query = ApplyFilters(input, query, centerId);
            IQueryable<CentreDto> selectQuery =await GetSelectQuery(query);
            return await selectQuery.GetPagedResultAsync(input.SkipCount, input.MaxResultCount);
        }

        public async Task Update(CentreDto input)
        {
            var center = await _repository.GetAsync(input.Id);
            if (center == null)
            {
                throw new UserFriendlyException(AppConstants.ErrorMessages.CenterNotFound);
            }

            await _repository.UpdateAsync(center);

            await UnitOfWorkManager.Current.SaveChangesAsync();
            //ObjectMapper.Map(input, data);
        }


        #region Private Methods
        private IQueryable<Center> ApplyFilters(PagedCentreResultRequestDto input, IQueryable<Center> query,int? centerId)
        {
            if (string.IsNullOrWhiteSpace(input.Keyword) == false)
                query = query.Where(g => g.Name.Contains(input.Keyword));
            if (centerId != null)
                query = query.Where(g => g.Id == centerId);
            return query;
        }
        private async Task<IQueryable<CentreDto>> GetSelectQuery(IQueryable<Center> query)
        {
            //var result = query.Select(item => new CentreDto
            //{
            //    Id = item.Id,
            //    //UserId = item.UserId,
            //    Name = item.Name,
            //    //Surname = item.CenterUser.Surname,
            //    //Address = item.CenterUser.Address,
            //    //CNIC = item.CenterUser.CNIC,
            //    //Country = item.CenterUser.Country,
            //    //EmailAddress = item.CenterUser.EmailAddress,
            //    //FullName = item.CenterUser.FullName,
            //    //PhoneNumber = item.CenterUser.PhoneNumber,
            //    //PicturePublicId = item.CenterUser.PicturePublicId,
            //    //PictureUrl = item.CenterUser.PictureUrl,
            //    //PostalCode = item.CenterUser.PostalCode,
            //    CreatorUserId = item.CreatorUserId,
            //    CreationTime = item.CreationTime,
            //    //IsActive = item.CenterUser.IsActive,
            //    //UserName = item.CenterUser.UserName

            //}).ToList();
            var asynclist =await query.ToListAsync();
            var list = new List<CentreDto>();
            foreach (var res in asynclist)
            {
                var totalFees = 0;
                var totalPaid = 0;
                var dto = new CentreDto();
                dto.Id = res.Id;
                dto.Name = res.Name;
                dto.CreatorUserId = res.CreatorUserId;
                  
                var user = _userRepository.GetAllList(x => x.Id == res.CreatorUserId.Value);

                var totalStudents = await _studentRepository.GetAllListAsync(x => x.CenterId == res.Id);
                foreach (var student in totalStudents)
                {
                    totalFees = totalFees + student.TotalFees;
                    var feesPaids = await _feesRecordRepository.GetAllListAsync(x => x.StudentId == student.Id);
                    foreach (var item in feesPaids)
                    {
                        totalPaid = totalPaid + item.Paid;
                    }

                }

                dto.CreatedBy = user.FirstOrDefault().FullName;
                dto.TotalFees = totalFees;
                dto.TotalPaid = totalPaid;
                dto.TotalRemaining = totalFees - totalPaid;
                list.Add(dto);
            };
            var ab = list.AsQueryable();
            return ab;
        }

        #endregion
    }
}
