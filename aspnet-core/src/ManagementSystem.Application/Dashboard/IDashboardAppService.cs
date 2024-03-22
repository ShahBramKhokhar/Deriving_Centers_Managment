using ManagementSystem.Dashboard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Dashboard
{
    public interface IDashboardAppService
    {
        Task<DashboardDto> GetDashboardData();
    }
}
