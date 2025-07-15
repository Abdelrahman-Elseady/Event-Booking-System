using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBS_Business
{
    public class clsOrganizer
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;

        public int OrganizerID { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public int AccountID { get; set; }

        public clsOrganizer()
        {
            Mode = enMode.AddNew;
            OrganizerID = AccountID = -1;
            FirstName = MidName = LastName = string.Empty;
        }

        public clsOrganizer(int organizerID, string firstName, string midName, string lastName, int accountID)
        {
            Mode = enMode.Update;
            OrganizerID = organizerID;
            FirstName = firstName;
            MidName = midName;
            LastName = lastName;
            AccountID = accountID;
        }

        private bool _AddNewOrganizer()
        {
            this.OrganizerID = clsOrganizerData.AddNewOrganizer(this.FirstName, this.MidName, this.LastName, this.AccountID);
            return this.OrganizerID != -1;
        }

        private bool _UpdateOrganizer()
        {
            return clsOrganizerData.UpdateOrganizer(this.OrganizerID, this.FirstName, this.MidName, this.LastName, this.AccountID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewOrganizer())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdateOrganizer();
            }

            return false;
        }

        public static bool Delete(int id)
        {
           
            return clsOrganizerData.DeleteOrganizer(id);
        }

        public static clsOrganizer GetOrganizerByID(int id)
        {
            string firstName = string.Empty;
            string midName = string.Empty;
            string lastName = string.Empty;
            int accountID = -1;

            if (clsOrganizerData.FindOrganizer(id, ref firstName, ref midName, ref lastName, ref accountID))
            {
                return new clsOrganizer(id, firstName, midName, lastName, accountID);
            }

            return new clsOrganizer(); // Not found
        }

        public static DataTable GetAllOrganizers()
        {
            return clsOrganizerData.GetAllOrganizers();
        }
    }

}
