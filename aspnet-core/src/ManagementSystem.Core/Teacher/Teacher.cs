using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using ManagementSystem.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Teacher
{
    public class Teacher : FullAuditedEntity, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }
        public virtual string Name { get; set; }
        public string LastName { get; set; }
        public int CNIC { get; set; }

        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual string PictureUrl { get; set; }

        public string PicturePublicId { get; set; }

        [ForeignKey(nameof(User))]
        public long? UserId { get; set; }

        public virtual User TeacherUser { get; set; }


    }
}
