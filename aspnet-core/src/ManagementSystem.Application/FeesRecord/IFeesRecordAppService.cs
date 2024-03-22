using ManagementSystem.FeesRecord.Dto;
using ManagementSystem.Teacher.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.FeesRecords
{
    public interface IFeesRecordAppService
    {
        Task Create(CreateFeesRecordDto input);
        Task<FeesRecordDto> GetById(int id);
    }
}
