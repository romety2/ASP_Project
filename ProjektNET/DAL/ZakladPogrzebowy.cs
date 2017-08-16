using ProjektNET.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class ZakladPogrzebowy : DbContext
    {

        public ZakladPogrzebowy() : base("DefaultConnection")
        {
        }

        public DbSet<Pogrzeb> Pogrzeby { get; set; }
        public DbSet<Trup> Trupy { get; set; }
        public DbSet<Grabarz> Grabarze { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}