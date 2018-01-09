using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DataBase.Entities
{
    using SPCF.Models;
    using SPCF.Models.Interfaces;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public enum GenderType
    {
        Male = 1,
        Female = 2
    }
    public class UserProfiles : IStoreable
    {
        //IStoreable
        public long? Created { get; set; }
        public long? Modified { get; set; }

        [Key]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "User Name")]
        public string fullName { get { return FirstName + " " + LastName; }}

        public GenderType? Gender { get; set; }

        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public int Zip { get; set; }

        public byte[] Image { get; set; }


        public List<Comments> Comment { get; set; }
        public List<Rates> Rate { get; set; }
        public List<FavouriteBooks> FavouriteBook { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        // Configure
        public class UserProfilesConfiguration : EntityTypeConfiguration<UserProfiles>
        {
            public UserProfilesConfiguration()
            {

                ToTable("UserProfiles");

                HasRequired(a => a.ApplicationUser)
                  .WithOptional(a => a.UserProfiles)
                  .WillCascadeOnDelete(true);

            }
        }
    }
}
