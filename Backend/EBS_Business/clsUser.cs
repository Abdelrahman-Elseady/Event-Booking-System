using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBS_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int AccountID { get; set; }

        public clsUser()
        {
            Mode = enMode.AddNew;
            UserID = AccountID = -1;
            FirstName = MidName = LastName = string.Empty;
            Age = 0;
        }

        public clsUser(int userID, string firstName, string midName, string lastName, int age, int accountID)
        {
            Mode = enMode.Update;
            UserID = userID;
            FirstName = firstName;
            MidName = midName;
            LastName = lastName;
            Age = age;
            AccountID = accountID;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.FirstName, this.MidName, this.LastName, this.Age, this.AccountID);
            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.FirstName, this.MidName, this.LastName, this.Age, this.AccountID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static bool Delete(int id)
        {
            return clsUserData.DeleteUser(id);
        }

        public static clsUser GetUserByID(int id)
        {
            string firstName = string.Empty;
            string midName = string.Empty;
            string lastName = string.Empty;
            int age = 0;
            int accountID = -1;

            if (clsUserData.FindUser(id, ref firstName, ref midName, ref lastName, ref age, ref accountID))
            {
                return new clsUser(id, firstName, midName, lastName, age, accountID);
            }

            return new clsUser(); // Not found
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
    }

}
