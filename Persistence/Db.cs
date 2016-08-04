using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Migrations;
using Persistence.Models;

namespace Persistence
{
    public class Db : DbContext
    {
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        public Db() : base("C1")
        {

        }

        public static void ConfigureMigrations()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
