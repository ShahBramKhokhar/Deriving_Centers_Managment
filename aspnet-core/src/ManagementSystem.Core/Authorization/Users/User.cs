using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Castle.Core.Resource;
using ManagementSystem.Centres;
using ManagementSystem.Students;

namespace ManagementSystem.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public override string Name { get; set; }
        public override string PhoneNumber { get; set; }
        public int CNIC { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual string PictureUrl { get; set; }
        public string PicturePublicId { get; set; }

        public int CenterId { get; set; }
        public virtual Center Center { get; set; }

        //[InverseProperty("CenterUser")]
        //public virtual Center Center { get; set; }

        [InverseProperty("StudentUser")]
        public virtual Student Student { get; set; }

        [InverseProperty("TeacherUser")]
        public virtual ManagementSystem.Teacher.Teacher Teacher { get; set; }
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
