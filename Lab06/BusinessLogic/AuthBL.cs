using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.OL;

namespace BusinessLogic
{
    public sealed class AuthResult
    {
        public Account Account { get; set; }
        public List<string> RoleNames { get; set; } = new List<string>();
        public bool IsAdmin => RoleNames.Any(n => string.Equals(n, "Admin", StringComparison.OrdinalIgnoreCase));
        public bool IsThuNgan => RoleNames.Any(n => n.Equals("Thu ngân", StringComparison.OrdinalIgnoreCase));
    }

    public class AuthBL
    {
        private readonly AccountBL _accountBL = new AccountBL();
        private readonly RoleeBL _roleBL = new RoleeBL();
        private readonly RoleAccountBL _roleAccBL = new RoleAccountBL();

        public AuthResult TryLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                return null;

            var acc = _accountBL.GetAll().FirstOrDefault(a =>
                string.Equals(a.AccountName, username, StringComparison.OrdinalIgnoreCase) &&
                a.Password == password);

            if (acc == null) return null;

            var allRoles = _roleBL.GetAll();
            var rAcc = _roleAccBL.GetAll()
                .Where(ra => ra.AccountName.Equals(acc.AccountName, StringComparison.OrdinalIgnoreCase) && ra.Actived)
                .ToList();

            var roleNames = (from ra in rAcc
                             join r in allRoles on ra.RoleID equals r.ID
                             select (r.RoleName ?? "").Trim()).ToList();

            return new AuthResult
            {
                Account = acc,
                RoleNames = roleNames
            };
        }
    }
}
