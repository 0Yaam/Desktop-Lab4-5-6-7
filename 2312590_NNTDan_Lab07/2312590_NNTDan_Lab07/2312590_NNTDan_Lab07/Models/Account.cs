using System.Collections.Generic;

namespace _2312590_NNTDan_Lab07.Models
{
    public class Account
    {
        public int Id
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string DisplayName
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public string Group
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string Tel
        {
            get; set;
        }
        public bool IsActive
        {
            get; set;
        }

        public virtual ICollection<Role> Roles
        {
            get; set;
        }
    }
}
