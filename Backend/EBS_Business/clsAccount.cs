using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace EBS_Business
{
    public class clsAccount
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;

        public int AccountID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsAccount()
        {
            Mode = enMode.AddNew;
            AccountID = -1;
            UserName = Password = string.Empty;
            IsActive = true;
        }

        public clsAccount(int accountID, string userName, string password, bool isActive)
        {
            Mode = enMode.Update;
            AccountID = accountID;
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }

        private bool _AddNewAccount()
        {
            this.AccountID = clsAccountData.AddNewAccount(this.UserName, this.Password, this.IsActive);
            return this.AccountID != -1;
        }

        private bool _UpdateAccount()
        {
            return clsAccountData.UpdateAccount(this.AccountID, this.UserName, this.Password, this.IsActive);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAccount())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateAccount();
            }

            return false;
        }

        public static bool Delete(int id)
        {
            return clsAccountData.DeleteAccount(id);
        }

        public static clsAccount GetAccountByID(int id)
        {
            string userName = string.Empty;
            string password = string.Empty;
            bool isActive = false;

            if (clsAccountData.FindAccount(id, ref userName, ref password, ref isActive))
            {
                return new clsAccount(id, userName, password, isActive);
            }

            return new clsAccount(); // not found
        }

        public static DataTable GetAllAccounts()
        {
            return clsAccountData.GetAllAccounts();
        }
    }

}
