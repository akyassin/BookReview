using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using DataBase.Entities;

namespace DataBase.Data
{
    class SecondProjectContext: DbContext
    {
        public SecondProjectContext(): base("DefaultConnection")
        {}

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<FavouriteBooks> FavouriteBooks { get; set; }
        public DbSet<Rates> Rates { get; set; }
        public DbSet<UserProfiles> UserProfiles { get; set; }

    }
}
