using Abp.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.ClassArea.Dto
{
    public class CreateClassAreaDto
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public virtual string Title { get; set; }
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string Descrtipton { get; set; }

      
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int TotalFees { get; set; }
        public int TeacherId { get; set; }

        public int[] studentIds { get; set; }

        public int? TenantId { get; set; }
        public int? Id { get; set; }
    }
}
