using _2312590_NNTDan_Lab07.Models;

namespace _2312590_NNTDan_Lab07
{
    public static class AppSession
    {
        public static Account CurrentUser
        {
            get; set;
        }
        public static bool IsAdmin()
        {
            Account u = CurrentUser;
            if (u == null || u.Roles == null)
                return false;
            foreach (Role r in u.Roles)
            {
                if (r.Name == "ManageAccounts")
                    return true;
            }
            return false;
        }
    }
}
