using DataBase.Entities;
using SPCF.Models;
using System;

namespace SPCF.ViewModels
{
        public class UserViewModel
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            
            public UserViewModel()
            {

            }
            public UserViewModel(ApplicationUser user)
            {
                UserID = user.Id;
                UserName = user.UserName;
            }
        }
}