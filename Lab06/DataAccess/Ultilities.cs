using System.Configuration;

namespace DataAccess
{
    public static class Ultilities
    {
        private const string StrName = "ConnectionStringName";
        public static readonly string ConnectionString =
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC602\Downloads\Desktop-mybranch\Desktop-mybranch\Lab06\DataAccess\DB.mdf;Integrated Security=True";

        // Food
        public const string Food_GetAll = "Food_GetAll";
        public const string Food_InsertUpdateDelete = "Food_InsertUpdateDelete";

        // Category
        public const string Category_GetAll = "Category_GetAll";
        public const string Category_InsertUpdateDelete = "Category_InsertUpdateDelete";

        // Ban
        public const string Ban_InsertUpdateDelete = "Ban_InsertUpdateDelete";

        // Bills
        public const string Bills_InsertUpdateDelete = "Bills_InsertUpdateDelete2"; // dùng bản gộp

        // BillDetails
        public const string BillDetails_InsertUpdateDelete = "BillDetails_InsertUpdateDelete2";

        // Account
        public const string Account_InsertUpdateDelete = "Account_InsertUpdateDelete";

        // Rolee
        public const string Rolee_InsertUpdateDelete = "Rolee_InsertUpdateDelete2";

        // RoleAccount (khoá kép)
        public const string RoleAccount_InsertUpdateDelete = "RoleAccount_InsertUpdateDelete2";
    }
}
