namespace SPCF.ViewModels.UserAspNet
{
    using System.Linq;
    using SPCF.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class EditAdmin
    {
        public string ID { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }

        public string SelectedRoleId { get; set; }
        public string SelectedRoleName { get; set; }

        public IEnumerable<RolesVM> RolesList { get; set; }

        public User.Edit UserProfile { get; set; }

        public EditAdmin()
        {
        }

        public EditAdmin(IEnumerable<IdentityRole> rolesList, ApplicationUser user)
        {

            RolesList = rolesList.Select(a => new RolesVM(a));

            ID = user.Id;
            Password = user.PasswordHash;
            Email = user.Email;
            PhoneNo = user.PhoneNumber;
            UserName = user.UserName;

            var roleIds = user.Roles.Select(a => a.RoleId);
            var roles = new List<IdentityRole>();

            using (var db = new SPContext())
            {
                foreach (var id in roleIds)
                {
                    var role = db.Roles.Find(id);
                    if (role != null)
                    {
                        roles.Add(role);
                    }
                }
            }

            SelectedRoleId = roles.FirstOrDefault().Id;
            SelectedRoleName = roles.FirstOrDefault().Name;


            if (user.UserProfiles != null)
            {
                UserProfile = new User.Edit(user.UserProfiles);
                UserProfile.Image = user.UserProfiles.Image;
            }
        }

    }
}