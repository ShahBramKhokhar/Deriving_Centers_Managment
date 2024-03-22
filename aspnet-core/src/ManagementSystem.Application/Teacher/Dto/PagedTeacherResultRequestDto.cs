using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Teacher.Dto
{
    public class PagedTeacherResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
