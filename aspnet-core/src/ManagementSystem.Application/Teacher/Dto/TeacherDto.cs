using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using ManagementSystem.Authorization.Accounts.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Teachers.Dto
{

    [AutoMapFrom(typeof(ManagementSystem.Teacher.Teacher))]
    public class TeacherDto  : UserCommonDto
    {
       
    }
}



