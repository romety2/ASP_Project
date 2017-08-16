using Microsoft.Owin;
using Owin;
using ProjektASP.App_Start;

[assembly: OwinStartupAttribute(typeof(ProjektNET.Startup))]
namespace ProjektNET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AddUser au = new AddUser();
            AddRole ar = new AddRole();
            au.Add("administrator@zp.pl", "administrator@zp.pl", "H@slo123", "Administrator");
            ar.Add("Pracownik");
            ar.Add("Klient");
            ConfigureAuth(app);
        }
    }
}
