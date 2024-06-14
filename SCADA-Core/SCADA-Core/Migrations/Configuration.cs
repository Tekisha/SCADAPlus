using System.Data.Entity.Migrations;
using SCADA_Core.Repositories;

namespace SCADA_Core.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<ScadaDbContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(ScadaDbContext context)
    {
        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method
        //  to avoid creating duplicate seed data.
    }
}