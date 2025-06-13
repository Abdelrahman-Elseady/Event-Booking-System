using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace EBS_Business
{

    public class clsEvent
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode;
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public int OrganizerID { get; set; }
        public string ImagePath { get; set; }
        public int Category {  get; set; }

        public clsEvent()
        {
            Mode= enMode.AddNew;
            EventID=Category=OrganizerID=-1;
            EventName = Place = ImagePath = string.Empty;
            Price = 0;
        }
        public clsEvent(int EventID,string EventName,string Place,DateTime date,float Price,int OrganizerID,string ImagePath,int Category)
        {
            Mode= enMode.Update;
            this.EventID=EventID;
            this.EventName=EventName;
            this.Place=Place;
            this.Date=date;
            this.Price=Price;
            this.OrganizerID=OrganizerID;
            this.ImagePath = ImagePath;
            this.Category=Category;
        }
        private bool _AddNewEvent()
        {
            this.EventID = clsEventData.AddNewEvent(this.EventName,this.Place,this.Date,this.Price,this.OrganizerID,this.ImagePath,this.Category);
            return this.EventID != -1;
        }
        private bool _UpdateEvent()
        {
            return clsEventData.UpdateEvent(this.EventID, this.EventName, this.Place, this.Date, this.Price, this.OrganizerID, this.ImagePath, this.Category);
        }
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewEvent())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateEvent();
            }
            return false;
        }

        public static bool Delete(int id)
        {
            return clsEventData.DeleteEvent(id);
        }
        public static clsEvent GetEventByID(int id)
        {
            string EventName = string.Empty;
            string Place = string.Empty;
            DateTime date = DateTime.MinValue;
            float Price = 0;
            int Organizer_ID = -1;
            string ImagePath = string.Empty;
            int Category_ID = -1;
            if (clsEventData.FindEvent(id, ref EventName, ref Place, ref date, ref Price, ref Organizer_ID, ref ImagePath, ref Category_ID)) 
                return new clsEvent(id, EventName, Place, date,Price,Organizer_ID, ImagePath, Category_ID);
            return new clsEvent();
        }
        public static DataTable GetAllEvents()
        {  
            return clsEventData.GetAllEvents(); 
        }

    }
}
