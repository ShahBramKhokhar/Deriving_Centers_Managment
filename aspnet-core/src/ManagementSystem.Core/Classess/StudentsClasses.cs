using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using ManagementSystem.Students;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Classess
{
    public class StudentsClasses: FullAuditedEntity, IMustHaveTenant
    {
        
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student  student { get; set; }


        [ForeignKey("ClassArea")]
        public int ClassId { get; set; }

        public ClassArea ClassArea { get; set; }
        public int TenantId { get; set; }
    }
}
