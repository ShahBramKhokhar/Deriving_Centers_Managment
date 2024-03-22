using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;


namespace ManagementSystem.Students.Dto
{
    [AutoMapTo(typeof(Student))]
    public class CreateStudentDto 
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

        public string Password { get; set; }

        public int TenantId { get; set; }
        public DateTime CreationTime { get; set; }

        public string PhoneNumber { get; set; }
        public int CNIC { get; set; }
        public string ImageBase64String { get; set; }

        public int CenterId { get; set; }
        public int TotalFees { get; set; }


    }
}



