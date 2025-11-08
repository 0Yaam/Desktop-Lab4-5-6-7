using System.Collections.Generic;

namespace _2312590_NNTDan_Lab07.Models
{
    public class Role
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public virtual ICollection<Account> Accounts
        {
            get; set;
        }
    }
}
