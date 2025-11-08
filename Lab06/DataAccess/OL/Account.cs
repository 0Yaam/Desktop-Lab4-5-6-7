using System;

namespace DataAccess.OL
{
    public class Account
    {
        public string AccountName { get; set; } = "";
        public string Password { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Tell { get; set; } = "";
        public DateTime? DateCreated { get; set; }
    }
}
