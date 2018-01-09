using Microsoft.AspNet.Identity.EntityFramework;

namespace SPCF.ViewModels
{
    public class RolesVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public RolesVM()
        {
        }

        public RolesVM(IdentityRole role)
        {
            RoleId = role.Id;
            RoleName = role.Name;
        }

    }
}