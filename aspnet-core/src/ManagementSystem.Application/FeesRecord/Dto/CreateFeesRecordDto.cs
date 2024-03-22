using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.FeesRecord.Dto
{
    public class CreateFeesRecordDto
    {
        public int StudentId { get; set; }
        public int Total { get; set; }
        public int Discount { get; set; }
        public int Paid { get; set; }
        public int Remaining { get; set; }

    }
}
