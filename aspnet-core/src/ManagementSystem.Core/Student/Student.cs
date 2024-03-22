using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using ManagementSystem.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using ManagementSystem.Centres;

namespace ManagementSystem.Students
{
    public class Student : FullAuditedEntity, IMustHaveTenant
    {

        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }
        public virtual int TenantId { get; set; }
        public int TotalFees { get; set; }

        [ForeignKey(nameof(User))]
        public long? UserId { get; set; }
        public virtual User StudentUser { get; set; }
        [ForeignKey(nameof(User))]
        public int? CenterId { get; set; }
        public virtual Center StudentCenter { get; set; }
    }
}
