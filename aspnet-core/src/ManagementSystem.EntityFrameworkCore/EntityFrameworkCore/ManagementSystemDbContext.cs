using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ManagementSystem.Authorization.Roles;
using ManagementSystem.Authorization.Users;
using ManagementSystem.MultiTenancy;
using ManagementSystem.Centres;
using ManagementSystem.Students;
using ManagementSystem.Classess;
using ManagementSystem.FeeRecords;

namespace ManagementSystem.EntityFrameworkCore
{
    public class ManagementSystemDbContext : AbpZeroDbContext<Tenant, Role, User, ManagementSystemDbContext>
    {
       
        public ManagementSystemDbContext(DbContextOptions<ManagementSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Center> Centres { get; set; }
        public DbSet<Student> students { get; set; }

        public DbSet<ManagementSystem.Teacher.Teacher> teachers { get; set; }

        public DbSet<ClassArea> classArea { get; set; }

        public DbSet<StudentsClasses> studentsClasses { get; set; }

        public DbSet<FeesRecord> feeRecords { get; set; }

    }
}
