using Abp.Domain.Repositories;
using ManagementSystem.Authorization.Users;
using ManagementSystem.Centres;
using ManagementSystem.Dashboard.Dto;
using ManagementSystem.Students;
using ManagementSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Dashboard
{
    public class DashboardAppService: ManagementSystemServiceBase,IDashboardAppService
    {
        private readonly IRepository<FeeRecords.FeesRecord> _feesRecordRepository;
        private readonly IUserAppService _userAppService;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Center> _centerRepository;
        private readonly IRepository<ManagementSystem.Classess.ClassArea> _classRepository;
        private readonly IRepository<Student> _studentRepository;
        public DashboardAppService(
            IRepository<FeeRecords.FeesRecord> feesRecordRepository,
             IUserAppService userAppService,
            IRepository<User, long> userRepository,
            IRepository<Center> centerRepository,
            IRepository<ManagementSystem.Classess.ClassArea> classRepository,
            IRepository<Student> studentRepository
            )
        {

            _feesRecordRepository = feesRecordRepository;
            _userAppService = userAppService;
            _userRepository = userRepository;
            _centerRepository = centerRepository;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }

        public async Task<DashboardDto> GetDashboardData()
        {
            var centersCount = await _centerRepository.CountAsync();
            var usersCount = await _userRepository.CountAsync();
            var teacherRole = _userAppService.GetRoles().Result.Items.FirstOrDefault(x => x.Name == "teacher");
            var studentRole = _userAppService.GetRoles().Result.Items.FirstOrDefault(x => x.Name == "student");
            var teachersCount = await _userRepository.CountAsync(x => x.Roles.Any(x => x.RoleId == teacherRole.Id));
            var studentsCount = await _userRepository.CountAsync(x => x.Roles.Any(x => x.RoleId == studentRole.Id));
            var classesCount = await _classRepository.CountAsync();
            var totalStudents = await _studentRepository.GetAllListAsync();
            var totalFees = 0;
            var totalPaid = 0;
            foreach (var student in totalStudents)
            {
                totalFees = totalFees + student.TotalFees;
                var feesPaids = await _feesRecordRepository.GetAllListAsync(x => x.StudentId == student.Id);
                foreach (var item in feesPaids)
                {
                    totalPaid = totalPaid + item.Paid;
                }

            }

            return new DashboardDto()
            {
                CentersCount = centersCount,
                UsersCount = usersCount,
                TeachersCount = teachersCount,
                StudentsCount = studentsCount,
                ClassesCount = classesCount,
                TotalFees = totalFees,
                TotalReceivedFromStudents = totalPaid,
                TotalRemainingFromStudents = totalFees-totalPaid
            };
        }
    }
}
