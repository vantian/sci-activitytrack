using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Bkpm.ActivityTracker.Authorization;
using Bkpm.ActivityTracker.Authorization.Roles;
using Bkpm.ActivityTracker.Authorization.Users;
using System.Collections.Generic;

namespace Bkpm.ActivityTracker.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly ActivityTrackerDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(ActivityTrackerDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();


            var checkActivityGroup = _context.ActivityGroup.IgnoreQueryFilters().FirstOrDefault();
            if (checkActivityGroup == null)
            {
                checkActivityGroup = _context.ActivityGroup.Add(new ActivityEntity.ActivityGroup() { Nama = "Progress Kinerja KSPN Bogor" }).Entity;
                _context.SaveChanges();
            }
        }

        private void CreateRolesAndUsers()
        {
            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            var opRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Host.Operator);
            if (opRoleForHost == null)
            {
                opRoleForHost = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Host.Operator, StaticRoleNames.Host.Operator) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            var adminOpRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Host.AdminOperator);
            if (adminOpRoleForHost == null)
            {
                adminOpRoleForHost = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Host.AdminOperator, StaticRoleNames.Host.AdminOperator) { IsStatic = true, IsDefault = false }).Entity;
                _context.SaveChanges();

                var adminOpPermission = new List<string> { PermissionNames.Pages_Users_Activation, PermissionNames.Pages_ActivityGroup };

                var permissions_adminOpRoleForHost = PermissionFinder
                    .GetAllPermissions(new ActivityTrackerAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                                adminOpPermission.Contains(p.Name))
                    .ToList();

                if (permissions_adminOpRoleForHost.Any())
                {
                    _context.Permissions.AddRange(
                        permissions_adminOpRoleForHost.Select(permission => new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminOpRoleForHost.Id
                        })
                    );
                    _context.SaveChanges();
                }
            }

            // Grant all permissions to admin role

            var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
                .OfType<RolePermissionSetting>()
                .Where(p => p.TenantId == _tenantId && p.RoleId == adminRole.Id)
                .Select(p => p.Name)
                .ToList();

            var permissions = PermissionFinder
                .GetAllPermissions(new ActivityTrackerAuthorizationProvider())
                .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) &&
                            !grantedPermissions.Contains(p.Name))
                .ToList();

            if (permissions.Any())
            {
                _context.Permissions.AddRange(
                    permissions.Select(permission => new RolePermissionSetting
                    {
                        TenantId = _tenantId,
                        Name = permission.Name,
                        IsGranted = true,
                        RoleId = adminRole.Id
                    })
                );
                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "Wsad-1234!");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();
            }
        }
    }
}
