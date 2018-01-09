namespace SPCF.ViewModels.User
{
    using DataBase.Entities;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    public class Edit
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType? Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public byte[] Image { get; set; }
        public HttpPostedFileBase Upload { get; set; }

        //public UserAspNet.Edit AspNetUser { get; set; }

        public Edit()
        {
        }

        //public UserProfiles CreateUserProfile()
        //{
        //    return new UserProfiles
        //    {
        //        UserID = Guid.NewGuid().ToString(),
        //        FirstName = FirstName,
        //        LastName = LastName,
        //        Gender = Gender,
        //        BirthDate = DateOfBirth,
        //        Street = Street,
        //        City = City,
        //        Zip = Zip,
        //    };
        //}

        public Edit(UserProfiles user)
        {
            ID = user.UserID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender;
            DateOfBirth = user.BirthDate;
            Street = user.Street;
            City = user.City;
            Zip = user.Zip;
            Image = user.Image == null ? null : user.Image;
            
        }

    }
}