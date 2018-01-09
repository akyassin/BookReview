using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    using SPCF.Models.Interfaces;
    public class Comments : IStoreable
    {
        //IStoreable
        public long? Created { get; set; }
        public long? Modified { get; set; }

        [Key]
        public int CommentID { get; set; }
        
        public string Comment { get; set; }


        public int BookID { get; set; }
        public Books Books { get; set; }

        public string UserID { get; set; }
        public UserProfiles UserProfiles { get; set; }

    }
}
