using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Students;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Centres
{
    public class Center: FullAuditedEntity, IMustHaveTenant
    {
        public string Name { get; set; }
        public override long? CreatorUserId { get; set; }
        [ForeignKey(nameof(User))]
        public virtual User CreatorUser { get; set; }
        public override DateTime CreationTime { get; set; }
        public virtual int TenantId { get; set; }
        //[ForeignKey(nameof(User))]
        //public  long? UserId { get; set; }
        //public virtual User CenterUser { get; set; }
        //[InverseProperty("StudentCenter")]
        //public virtual Student Student { get; set; }


    }
}
