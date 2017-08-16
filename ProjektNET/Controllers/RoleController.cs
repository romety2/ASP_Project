using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProjektNET.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ProjektNET.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private ApplicationDbContext adc = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(adc.Users.OrderBy(u => u.Email).ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string userEmail)
        {
            var myUser = adc.Users.FirstOrDefault(u => u.Email.Equals(userEmail, StringComparison.CurrentCultureIgnoreCase));
            adc.Users.Remove(myUser);
            adc.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Check()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Check(string userEmail)
        {
            if (!string.IsNullOrWhiteSpace(userEmail))
            {
                ApplicationUser user = adc.Users.FirstOrDefault(u => u.UserName.Equals(userEmail, StringComparison.CurrentCultureIgnoreCase));
                if (user != null)
                {
                    ApplicationUserManager account = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    ViewBag.RolesMyUser = account.GetRoles(user.Id);
                }
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string roleName)
        {
            var list = adc.Roles.ToList().Select(r =>
            new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string userEmail, string roleName)
        {
            ApplicationUser user = adc.Users.FirstOrDefault(u => u.UserName.Equals(userEmail, StringComparison.CurrentCultureIgnoreCase));
            if (user != null)
            {
                ApplicationUserManager account = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                account.RemoveFromRole(user.Id, "Administrator");
                account.RemoveFromRole(user.Id, "Pracownik");
                account.RemoveFromRole(user.Id, "Klient");
                account.AddToRole(user.Id, roleName);
            }

            var list = adc.Roles.ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = list;

            return View();
        }
    }
}