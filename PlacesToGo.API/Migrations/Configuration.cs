namespace PlacesToGo.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlacesToGo.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PlacesToGo.API.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Places.AddOrUpdate(a => a.Name,
         new Models.Places
         {
             Name = "Ohio Stadium",
             Location = System.Data.Entity.Spatial.DbGeography.FromText("POINT(40.0016487 -83.0547094)")
         });

            context.Places.AddOrUpdate(a => a.Name,
                new Models.Places
                {
                    Name = "Jerome Schottenstein Center",
                    Location = DbGeography.FromText("POINT(39.9979667 -83.0350542)")
                }); 

            context.Places.AddOrUpdate(a => a.Name,
                new Models.Places
                {
                    Name = "Fawcett Event Center",
                    Location = DbGeography.FromText("POINT(40.0083876 -83.0289173)")
                });
        }
    }
}
