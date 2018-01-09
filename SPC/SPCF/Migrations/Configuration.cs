namespace SecondProjectCodeFirst.Migrations
{
    using DataBase.Entities;
    using System;
    using System.Data.Entity.Migrations;


    internal sealed class Configuration : DbMigrationsConfiguration<SPCF.Models.SPContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SPCF.Models.SPContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Authors.AddOrUpdate(
              new Authors { FirstName = "Nalin", LastName = "Singh", Created = DateTime.UtcNow.Ticks },
              new Authors { FirstName = "Stella", LastName = "Parks", Created = DateTime.UtcNow.Ticks },
              new Authors { FirstName = "Stephen", LastName = "King", Created = DateTime.UtcNow.Ticks },
              new Authors { FirstName = "Yesmyn", LastName = "Ward", Created = DateTime.UtcNow.Ticks }
            );

            context.Category.AddOrUpdate(
              new Category { CategoryName = "Romance", Created = DateTime.UtcNow.Ticks },
              new Category { CategoryName = "Action", Created = DateTime.UtcNow.Ticks },
              new Category { CategoryName = "Biography", Created = DateTime.UtcNow.Ticks },
              new Category { CategoryName = "Cocking", Created = DateTime.UtcNow.Ticks }
            );

        }
    }
}
