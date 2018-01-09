namespace SPCF.ViewModels.Author
{
    using DataBase.Entities;
    using System;
    using System.Web;

    public class Create
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Upload { get; set; }

        public Create()
        {
        }


    }
}