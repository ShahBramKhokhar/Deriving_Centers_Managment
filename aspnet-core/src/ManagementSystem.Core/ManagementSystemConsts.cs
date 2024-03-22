using ManagementSystem.Debugging;

namespace ManagementSystem
{
    public class AppConstants
    {
        public const string LocalizationSourceName = "ManagementSystem";


        // default roles
        public const string CenterRole = "center";
        public const string TeacherRole = "teacher";
        public const string StudentRole = "student";
        public const string AdminRole = "admin";

        public const string ConnectionStringName = "Default";
        public const string DateFormate = "yyyy-MM-dd";
        public const string DateTimeFormate = "yyyy-MM-dd HH:mm:ss";
        public const bool MultiTenancyEnabled = true;
        public const int DefaultTenantId = 1;
        public const int DefaultUserId1 = 1;
        public const int DefaultUserId2 = 2;
        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;

        public const string CloudinarySettingsKey = "CloudinarySettings";


        public static class ErrorMessages
        {
            public const string CenterNotFound = "Center not found";
            public const string TeacherNotFound = "Teacher not found";
            public const string StudentNotFound = "Student not found";
            public const string ClassAreaNotFound = "Class not found";
            public const string StudentInClassNotFound = "Please first assign this student a classs.";
        }
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "4c629d99cdfb4b4dac0e0defa762ee0c";
    }
}
