using Abp.Application.Services.Dto;

namespace ManagementSystem.ClassArea.Dto
{
    public class PagedClassAreaResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
