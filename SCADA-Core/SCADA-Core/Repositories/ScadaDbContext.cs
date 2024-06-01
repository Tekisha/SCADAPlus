using SCADA_Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Repositories
{

    public class ScadaDbContext : DbContext
    {
        public ScadaDbContext() : base("name=ScadaDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>()
                .Map<DigitalOutputTag>(m => m.Requires("TagType").HasValue("DigitalOutput"))
                .Map<DigitalInputTag>(m => m.Requires("TagType").HasValue("DigitalInput"))
                .Map<AnalogOutputTag>(m => m.Requires("TagType").HasValue("AnalogOutput"))
                .Map<AnalogInputTag>(m => m.Requires("TagType").HasValue("AnalogInput"));

            modelBuilder.Entity<User>().HasKey(u => u.Username);

            base.OnModelCreating(modelBuilder);
        }
    }
}
