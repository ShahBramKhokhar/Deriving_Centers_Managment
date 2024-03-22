using Abp.Auditing;
using Abp.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Teacher.Dto
{
    public class CreateTeacherDto
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }


        public string Surname { get; set; }

        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public bool IsActive { get; set; }

        //  public string[] RoleNames { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        public int TenantId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
