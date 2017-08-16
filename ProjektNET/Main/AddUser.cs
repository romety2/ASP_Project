using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjektNET.Models;

namespace ProjektASP.App_Start
{
    public class AddUser
    {
        private ApplicationDbContext adc = new ApplicationDbContext();
        public void Add(string userName, string userEmail, string password, string roleName)
        {
            if (adc.Roles.FirstOrDefault(r => r.Name.Equals(roleName)) == null)
            {
                IdentityRole adminRole = new IdentityRole(roleName);
                adc.Roles.Add(adminRole);
                adc.SaveChanges();
            }

            if (adc.Users.FirstOrDefault(u => u.Email.Equals(userEmail)) != null)
            {
                var user = adc.Users.FirstOrDefault(u => u.Email.Equals(userEmail));
                var role = adc.Roles.FirstOrDefault(r => r.Name.Equals(roleName));
                if (user.Roles.FirstOrDefault(r => r.RoleId.Equals(role.Id)) == null)
                {
                    IdentityUserRole iur = new IdentityUserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    };

                    user.Roles.Add(iur);
                    adc.SaveChanges();
                }
            }
            else
            {
                PasswordHasher ph = new PasswordHasher();

                ApplicationUser au = new ApplicationUser
                {
                    UserName = userName,
                    Email = userEmail,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = ph.HashPassword(password)
                };

                adc.Users.Add(au);
                adc.SaveChanges();

                IdentityUserRole iur = new IdentityUserRole
                {
                    UserId = adc.Users.FirstOrDefault(u => u.Email.Equals(userEmail)).Id,
                    RoleId = adc.Roles.FirstOrDefault(r => r.Name.Equals(roleName)).Id
                };

                au.Roles.Add(iur);
                adc.SaveChanges();
            }
        }
    }
}