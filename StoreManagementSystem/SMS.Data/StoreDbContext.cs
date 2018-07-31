using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SMS.Data.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SMS.Data
{
    public partial class StoreDbContext : DbContext
    {
        public StoreDbContext()
            : base("name=StoreDB")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

            Database.SetInitializer<StoreDbContext>(new StoreInitializer());
        }

        public virtual DbSet<StoreItem> StoreItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
