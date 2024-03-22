using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;
using System;
using Abp.Domain.Entities.Auditing;

namespace ManagementSystem.Authorization.Accounts.Dto
{
    public class UserCommonDto : EntityDto, ICreationAudited
    {
        public long? UserId { get; set; }

        public int TenantId { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the name of the full.
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

     
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

      

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
      //  public string[] RoleNames { get; set; }

        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }

        public  string PhoneNumber { get; set; }
        public int CNIC { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual string PictureUrl { get; set; }
        public string PicturePublicId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
