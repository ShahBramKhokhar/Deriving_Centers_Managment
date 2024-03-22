using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Core.Helpers;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Authorization;
using ManagementSystem.Users.Dto;
using ManagementSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystem.FeeRecords;
using ManagementSystem.Teacher.Dto;
using ManagementSystem.Teachers.Dto;
using ManagementSystem.FeesRecord.Dto;

namespace ManagementSystem.FeesRecords
{
    //[AbpAuthorize(PermissionNames.Pages_Teachers)]
    public class FeesRecordAppService : ManagementSystemServiceBase, IFeesRecordAppService
    {
        private readonly IRepository<FeeRecords.FeesRecord> _repository;
        private readonly IUserAppService _userAppService;
        IRepository<User, long> _userRepository;
        public FeesRecordAppService(
            IRepository<FeeRecords.FeesRecord> repository,
             IUserAppService userAppService,
            IRepository<User, long> userRepository
            )
        {

            _repository = repository;
            _userAppService = userAppService;
            _userRepository = userRepository;
        }

        public async Task Create(CreateFeesRecordDto input)
        {
            var paid = 0;
            var tenantId = AbpSession.TenantId ?? AppConstants.DefaultTenantId;
            var studentFeesRecords = await _repository.GetAllListAsync(x => x.StudentId == input.StudentId);
            foreach (var item in studentFeesRecords)
            {
                paid = paid + item.Paid;
            }

            var feesRecord = new FeeRecords.FeesRecord()
            {
                TenantId = tenantId,
                StudentId = input.StudentId,
                Total = input.Total,
                Paid = input.Paid,
                Discount = input.Discount,
                Remaining = input.Total - (paid+input.Paid),
                CreatedOn = new System.DateTime(),
                CreatedBy = AbpSession.UserId.Value,
            };

            var feeRecord = await _repository.InsertAsync(feesRecord);
            UnitOfWorkManager.Current.SaveChanges();
        }

        public async Task<PagedResultDto<FeesRecordDto>> GetPagedResultAsync(PagedFeesRecordResultRequestDto input)
        {
            var query = _repository.GetAllIncluding();//.GetAll();
            query = ApplyFilters(input, query);
            IQueryable<FeesRecordDto> selectQuery = GetSelectQuery(query);
            return await selectQuery.GetPagedResultAsync(input.SkipCount, input.MaxResultCount);
        }

        public async Task<FeesRecordDto> GetById(int id)
        {
            var data = await _repository.GetAsync(id);
            var feesRecord = new FeesRecordDto()
            {
                Id = data.Id,
                StudentId = data.StudentId,
                //StudentName = data.Student.StudentUser.Name,
                TotalFees = data.Total,
                Discount = data.Discount,
                Paid = data.Paid,
                RemainingFees = data.Remaining,
                //CreatedByName = data.User.Name,
                CreationTime = data.CreationTime,
            };

            return feesRecord;
        }


        #region Private Methods
        private static IQueryable<FeeRecords.FeesRecord> ApplyFilters(PagedFeesRecordResultRequestDto input, IQueryable<FeeRecords.FeesRecord> query)
        {
            if (string.IsNullOrWhiteSpace(input.Keyword) == false)
                query = query.Where(g => g.Student.StudentUser.Name.Contains(input.Keyword));
            if (input.StudentId != null && input.StudentId > 0)
                query = query.Where(g => g.StudentId == input.StudentId);
            
            return query;
        }
        private static IQueryable<FeesRecordDto> GetSelectQuery(IQueryable<FeeRecords.FeesRecord> query)
        {
            return query.Select(item => new FeesRecordDto
            {
                Id = item.Id,
                StudentId = item.StudentId,
                StudentName = item.Student.StudentUser.Name,
                StudentImage = item.Student.StudentUser.PictureUrl,
                TotalFees = item.Total,
                Discount = item.Discount,
                Paid = item.Paid,
                RemainingFees = item.Remaining,
                CreatedByName = item.User.Name,
                CreationTime = item.CreationTime,
            });
        }



        #endregion


    }
}
