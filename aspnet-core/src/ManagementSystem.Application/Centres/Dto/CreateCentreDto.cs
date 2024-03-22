using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;


namespace ManagementSystem.Centres.Dto
{
    [AutoMapTo(typeof(Center))]
    public class CreateCentreDto 
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

    }
}



