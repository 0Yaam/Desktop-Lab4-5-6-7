using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2312590_NNTDan_Lab07.Models
{
    public enum CategoryType
    {
        Drink,
        Food
    }
    public class Category
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public CategoryType Type
        {
            get; set;
        }

    }
}
