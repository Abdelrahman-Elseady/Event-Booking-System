using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using EventBookingDataAccess;
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
        public int Category { get; set; }

        public clsEvent()
        {
            Mode = enMode.AddNew;
            EventID = Category = OrganizerID = -1;
            EventName = Place = ImagePath = string.Empty;
            Price = 0;
        }
        public clsEvent(DTOEvent DTO)
        {
            Mode = enMode.Update;
            this.EventID = DTO.EventID;
            this.EventName = DTO.EventName;
            this.Place = DTO.Place;
            this.Date = DTO.Date;
            this.Price = DTO.Price;
            this.OrganizerID = DTO.OrganizerID;
            this.ImagePath = DTO.ImagePath;
            this.Category = DTO.CategoryID;
        }
        private bool _AddNewEvent()
        {
            this.EventID = clsEventData.AddNewEvent(new DTOEvent(this.EventID,this.EventName, this.Place, this.Date, this.Price, this.OrganizerID, this.ImagePath, this.Category));
            return this.EventID != -1;
        }
        private bool _UpdateEvent()
        {
            return clsEventData.UpdateEvent(new DTOEvent(this.EventID, this.EventName, this.Place, this.Date, this.Price, this.OrganizerID, this.ImagePath, this.Category));
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewEvent())
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
        public static DTOEvent GetEventByID(int id)
        {
            DTOEvent DTO = new DTOEvent();
            DTO.EventID = id;
            if (clsEventData.FindEvent(DTO))
                return DTO;
            return null;
        }
        public static List<DTOEvent> GetAllEvents()
        {
            return clsEventData.GetAllEvents();
        }

    }
}
