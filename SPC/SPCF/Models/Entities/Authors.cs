using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;


namespace DataBase.Entities
{
    using SPCF.Models.Interfaces;

    public class Authors: IStoreable
    {


        [Key]
        public int AuthorID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Author Name")]
        public string fullName { get { return FirstName + " " + LastName; }  }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        //IStoreable
        public long? Created { get; set; }
        public long? Modified { get; set; }


        public List<Books> Book { get; set; }

    }
}
