using DataBase.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace SPCF.Models
{
    public class SPContext : IdentityDbContext<ApplicationUser>
    {
        public SPContext(): base("DefaultConnection")
        { }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<FavouriteBooks> FavouriteBooks { get; set; }
        public DbSet<Rates> Rates { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }


        public static SPContext Create()
        {
            return new SPContext();
        }


    }

}

