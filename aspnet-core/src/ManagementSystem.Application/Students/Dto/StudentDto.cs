using Abp.AutoMapper;
using ManagementSystem.Authorization.Accounts.Dto;
using ManagementSystem.Centres;

namespace ManagementSystem.Students.Dto
{

    [AutoMapFrom(typeof(Student))]
    public class StudentDto : UserCommonDto
    {
        public int CenterId { get; set; }
        public Center Center { get; set; }
        public string ClassName { get; set; }
        //public int? TotalFees { get; set; }
        public int TotalFees { get; set; }

    }
}


