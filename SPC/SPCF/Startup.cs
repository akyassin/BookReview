using DataBase.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SPCF.Models;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(SPCF.Startup))]
namespace SPCF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //RemoveUsersAndRoles();
            //createRolesandUsers();
            //AddAuthorsAndCategories();
        }

        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()
        {
            SPContext context = new SPContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            { 

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "akyassin@gmail.com";
                user.Email = "akyassin@gmail.com";
                user.PhoneNumber = "0764148554";

                string userPWD = "ahmadahmad";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }

                //Create New UserProfile with same ID
                var db = new SPContext();
                var userprofile = new UserProfiles();
                userprofile.UserID = user.Id;
                userprofile.FirstName = "Ahmad";
                userprofile.LastName = "Yassin";
                userprofile.Created = DateTime.UtcNow.Ticks;
                userprofile.City = "Göteborg";
                userprofile.Street = "Sandeslätt 44";
                userprofile.Zip = 42436;
                
                db.UserProfiles.Add(userprofile);
                db.SaveChanges();
            }

            // creating Creating Readers role    
            if (!roleManager.RoleExists("Readers"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Readers";
                roleManager.Create(role);

            }

            // creating Creating Auditors role    
            if (!roleManager.RoleExists("Auditors"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Auditors";
                roleManager.Create(role);

            }
        }

        //Remove Users and Roles method....
        private void RemoveUsersAndRoles()
        {
            var db = new SPContext();

            var users = db.UserProfiles.ToList();
            foreach (var item in users)
            {
                db.UserProfiles.Remove(item);
            }

            var Aspusers = db.Users.ToList();
            foreach (var item in Aspusers)
            {
                db.Users.Remove(item);
            }

            var roles = db.Roles.ToList();
            foreach (var item in roles)
            {
                db.Roles.Remove(item);
            }

            try
            {
                db.SaveChanges();
            }
            catch (System.Exception e)
            {
                var temp = e;
            }
        }

        //Add Authors and Categories method...
        private void AddAuthorsAndCategories()
        {
            var db = new SPContext();


            if (db.Authors.Count() == 0)
            {
                //Add Authors
                var Author1 = new Authors();
                Author1.FirstName = "Nalin";
                Author1.LastName = "Singh";
                Author1.Created = DateTime.UtcNow.Ticks;
                db.Authors.Add(Author1);

                var Author2 = new Authors();
                Author2.FirstName = "Stella";
                Author2.LastName = "Parks";
                Author2.Created = DateTime.UtcNow.Ticks;
                db.Authors.Add(Author2);

                var Author3 = new Authors();
                Author3.FirstName = "Stephen";
                Author3.LastName = "King";
                Author3.Created = DateTime.UtcNow.Ticks;
                db.Authors.Add(Author3);

                var Author4 = new Authors();
                Author4.FirstName = "Yesmyn";
                Author4.LastName = "Ward";
                Author4.Created = DateTime.UtcNow.Ticks;
                db.Authors.Add(Author4);
            }

            if (db.Category.Count()==0)
            {
                //Add Categories
                var Category1 = new Category();
                Category1.CategoryName = "Romance";
                Category1.Created = DateTime.UtcNow.Ticks;
                db.Category.Add(Category1);

                var Category2 = new Category();
                Category2.CategoryName = "Action";
                Category2.Created = DateTime.UtcNow.Ticks;
                db.Category.Add(Category2);

                var Category3 = new Category();
                Category3.CategoryName = "Cocking";
                Category3.Created = DateTime.UtcNow.Ticks;
                db.Category.Add(Category3);

                var Category4 = new Category();
                Category4.CategoryName = "Biography";
                Category4.Created = DateTime.UtcNow.Ticks;
                db.Category.Add(Category4); 
            }


            //Save Changes To DataBase...
            db.SaveChanges();
        }


    }

}
