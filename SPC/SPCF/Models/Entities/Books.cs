using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    using SPCF.Models.Interfaces;
    using System.Data.Entity.ModelConfiguration;

    public class Books: IStoreable
    {
        //IStoreable
        public long? Created { get; set; }
        public long? Modified { get; set; }

        [Key]
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }


        public int AuthorID { get; set; }
        public Authors Authors { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<Comments> Comments { get; set; }
        
    }
}
