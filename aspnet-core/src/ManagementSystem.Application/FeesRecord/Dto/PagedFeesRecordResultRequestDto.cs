using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.FeesRecord.Dto
{
    public class PagedFeesRecordResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? StudentId { get; set; }
    }
}
