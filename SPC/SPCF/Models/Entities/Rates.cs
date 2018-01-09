using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    using SPCF.Models.Interfaces;

    public enum RateValue
    {
        VeryWeak_1 = 1,
        Weak_2     = 2,
        Medium_3   = 3,
        Good_4     = 4,
        VeryGood_5 = 5
     }
    public class Rates : IStoreable
    {
        //IStoreable
        public long? Created { get; set; }
        public long? Modified { get; set; }

        [Key]
        public int RateID { get; set; }

        public RateValue RateValue { get; set; }

        public int BookID { get; set; }
        public Books Books { get; set; }

        public string UserID { get; set; }
        public UserProfiles UserProfiles { get; set; }
    }
}
