using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondProjectCodeFirst.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryViewModel()
        {

        }
        public CategoryViewModel(Category category)
        {
            Id = category.CategoryID;
            Name = category.CategoryName;
        }
    }
}