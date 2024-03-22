using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace ManagementSystem.ClassAreas.Dto
{
    public class ClassAreaDto  : EntityDto, ICreationAudited
    {
        public int Id { get; set; }

        public virtual string Title { get; set; }
       
        public string Descrtipton { get; set; }


        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }


        public int TeacherId { get; set; }

        public List<ManagementSystem.Students.Student> students { get; set; }

        public int TenantId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public int TotalFees { get; set; }
        public List<int> studentIds { get; set; }
    }
}



