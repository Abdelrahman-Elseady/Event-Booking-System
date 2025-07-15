using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBS_Business
{
    public class clsEventCategory
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;

        public int EventCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public clsEventCategory()
        {
            Mode = enMode.AddNew;
            EventCategoryID = -1;
            CategoryName = CategoryDescription = string.Empty;
        }

        public clsEventCategory(int id, string name, string description)
        {
            Mode = enMode.Update;
            EventCategoryID = id;
            CategoryName = name;
            CategoryDescription = description;
        }

        private bool _AddNew()
        {
            this.EventCategoryID = clsEventCategoryData.AddNewCategory(CategoryName, CategoryDescription);
            return this.EventCategoryID != -1;
        }

        private bool _Update()
        {
            return clsEventCategoryData.UpdateCategory(EventCategoryID, CategoryName, CategoryDescription);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _Update();
            }
            return false;
        }

        public static bool Delete(int id)
        {
            return clsEventCategoryData.DeleteCategory(id);
        }

        public static clsEventCategory GetByID(int id)
        {
            string name = string.Empty;
            string description = string.Empty;

            if (clsEventCategoryData.FindCategory(id, ref name, ref description))
            {
                return new clsEventCategory(id, name, description);
            }

            return new clsEventCategory(); // not found
        }

        public static DataTable GetAll()
        {
            return clsEventCategoryData.GetAllCategories();
        }
    }

}
