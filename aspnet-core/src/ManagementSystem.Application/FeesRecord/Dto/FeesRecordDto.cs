using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.FeesRecord.Dto
{
    public class FeesRecordDto: EntityDto, ICreationAudited
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentImage { get; set; }
        public decimal TotalFees { get; set; }
        public decimal Discount { get; set; }
        public decimal Paid { get; set; }
        public decimal RemainingFees { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
