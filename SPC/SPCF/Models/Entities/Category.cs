using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    using SPCF.Models.Interfaces;
    public class Category: IStoreable  
    {
        //IStoreable
        public long? Created { get; set; }
        public long? Modified { get; set; }

        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }


        public List<Books> Book { get; set; }
    }
}
