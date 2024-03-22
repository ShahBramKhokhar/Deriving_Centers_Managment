using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Dashboard.Dto
{
    public class DashboardDto
    {
        public int CentersCount { get; set; }
        public int StudentsCount { get; set; }
        public int TeachersCount { get; set; }
        public int CenterAdminsCount { get; set; }
        public int UsersCount { get; set; }
        public int ClassesCount { get; set; }
        public long TotalFees { get; set; }
        public long TotalReceivedFromStudents { get; set; }
        public long TotalRemainingFromStudents { get; set; }
    }
}
