using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace ManagementSystem.Authorization
{
    public class ManagementSystemAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.Pages_CenterAdmin, L("Centres"));
            //context.CreatePermission(PermissionNames.Pages_Employes, L("Employes"));
            //context.CreatePermission(PermissionNames.Pages_Customers, L("Customers"));
            context.CreatePermission(PermissionNames.Pages_Accounts, L("Accounts"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Teachers, L("Teachers"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Students, L("Students"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Classes, L("Classess"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Reports, L("Reports"), multiTenancySides: MultiTenancySides.Tenant);
            context.CreatePermission(PermissionNames.Pages_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);
          
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AppConstants.LocalizationSourceName);
        }
    }
}
