using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Classess
{

    public class ClassArea : FullAuditedEntity, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }
        public virtual string Title { get; set; }
        public string Descrtipton { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int TotalFees { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public ManagementSystem.Teacher.Teacher teacher { get; set; }


    }
}
