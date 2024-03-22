using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Students;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.FeeRecords
{
    public class FeesRecord: FullAuditedEntity, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Total { get; set; }
        public int Paid { get; set; }
        public int Discount { get; set; }
        public int Remaining { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("User")]
        public long CreatedBy { get; set; }
        public User User { get; set; }
    }
}
