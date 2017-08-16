using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjektNET.Models;

namespace ProjektASP.App_Start
{
    public class AddRole
    {
        private ApplicationDbContext adc = new ApplicationDbContext();
        public void Add(string roleName)
        {
            if (adc.Roles.FirstOrDefault(r => r.Name.Equals(roleName)) == null)
            {
                IdentityRole adminRole = new IdentityRole(roleName);
                adc.Roles.Add(adminRole);
                adc.SaveChanges();
            }
        }
    }
}