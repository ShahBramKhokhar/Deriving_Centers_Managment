using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using ManagementSystem.Authorization.Accounts.Dto;
using System;

namespace ManagementSystem.Centres.Dto
{

    [AutoMapFrom(typeof(Center))]
    public class CentreDto : EntityDto, ICreationAudited
    {
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public long TotalFees { get; set; }
        public long TotalPaid { get; set; }
        public long TotalRemaining { get; set; }
    }
}


