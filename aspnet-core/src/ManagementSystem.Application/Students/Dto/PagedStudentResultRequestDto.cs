using Abp.Application.Services.Dto;

namespace ManagementSystem.Students.Dto
{
    public class PagedStudentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int CenterId { get; set; }
    }
}
