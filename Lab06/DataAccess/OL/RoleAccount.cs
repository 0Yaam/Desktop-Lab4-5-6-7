namespace DataAccess.OL
{
    public class RoleAccount
    {
        public int RoleID { get; set; }
        public string AccountName { get; set; } = "";
        public bool Actived { get; set; }
        public string Notes { get; set; } = "";
    }
}
