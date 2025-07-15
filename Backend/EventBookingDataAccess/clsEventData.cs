using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;


namespace EventBookingDataAccess
{
    public class DTOEvent
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public int OrganizerID { get; set; }
        public string ImagePath { get; set; }
        public int CategoryID { get; set; }

        public DTOEvent()
        {

        }
        public DTOEvent(int eventID, string eventName, string place, DateTime date, float price, int organizerID, string imagePath, int categoryID)
        {
            EventID = eventID;
            EventName = eventName;
            Place = place;
            Date = date;
            Price = price;
            OrganizerID = organizerID;
            ImagePath = imagePath;
            CategoryID = categoryID;
        }
    }
    public class clsEventData
    {
        public static int AddNewEvent(DTOEvent DTO)
        {
            int Inserted_ID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddNewEvent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Output parameter
                    SqlParameter idParam = new SqlParameter("@Inserted_ID", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);

                    // Input parameters
                    cmd.Parameters.AddWithValue("@EventName", DTO.EventName);
                    cmd.Parameters.AddWithValue("@Place", DTO.Place);
                    cmd.Parameters.AddWithValue("@Date", DTO.Date);
                    cmd.Parameters.AddWithValue("@Price", DTO.Price);
                    cmd.Parameters.AddWithValue("@Organizer_ID", DTO.OrganizerID);
                    cmd.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(DTO.ImagePath) ? DBNull.Value : (object)DTO.ImagePath);
                    cmd.Parameters.AddWithValue("@Category_ID", DTO.CategoryID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        Inserted_ID = (int)cmd.Parameters["@Inserted_ID"].Value;
                    }
                    catch
                    {
                        Inserted_ID = -1;
                    }
                }
            }

            return Inserted_ID;
        }
        public static bool UpdateEvent(DTOEvent DTO)
        {
            bool isUpdated = false;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateEvent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Event_ID", DTO.EventID);
                    cmd.Parameters.AddWithValue("@EventName", DTO.EventName);
                    cmd.Parameters.AddWithValue("@Place", DTO.Place);
                    cmd.Parameters.AddWithValue("@Date", DTO.Date);
                    cmd.Parameters.AddWithValue("@Price", DTO.Price);
                    cmd.Parameters.AddWithValue("@Organizer_ID", DTO.OrganizerID);
                    cmd.Parameters.AddWithValue("@Image_Path", DTO.ImagePath);
                    cmd.Parameters.AddWithValue("@Category", DTO.CategoryID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            isUpdated = true;
                        }
                    }
                    catch
                    {
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }
        public static bool DeleteEvent(int EventID)
        {
            bool IsDeleted = false;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteEvent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Event_ID", EventID);
                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            IsDeleted = true;
                    }
                    catch (Exception ex)
                    {
                        IsDeleted = false;
                    }
                }
            }
            return IsDeleted;
        }
        public static bool FindEvent(DTOEvent DTO)
        {
            bool found = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_FindEvent", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EventID", DTO.EventID);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DTO.EventName = reader["EventName"].ToString();
                            DTO.Place = reader["Place"].ToString();
                            DTO.Date = Convert.ToDateTime(reader["Date"]);
                            DTO.Price = float.Parse(reader["Price"].ToString());
                            DTO.OrganizerID = Convert.ToInt32(reader["Organizer_ID"]);
                            DTO.ImagePath = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString();
                            DTO.CategoryID = Convert.ToInt32(reader["Category_ID"]);

                            found = true;
                        }
                    }
                }
                catch
                {
                    found = false;
                }
            }

            return found;
        }
        public static List<DTOEvent> GetAllEvents()
        {
            List<DTOEvent> events = new List<DTOEvent>();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllEvents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {

                            events.Add(new DTOEvent
                                (
                                    reader.GetInt32(reader.GetOrdinal("Event_ID")),
                                    reader.GetString(reader.GetOrdinal("Event_Name")),
                                    reader.GetString(reader.GetOrdinal("Place")),
                                    reader.GetDateTime(reader.GetOrdinal("Date")),
                                    (float)reader.GetDecimal(reader.GetOrdinal("Price")),
                                    reader.GetInt32(reader.GetOrdinal("Organizer_ID")),
                                    reader.IsDBNull(reader.GetOrdinal("Image_Path")) ? "" : reader.GetString(reader.GetOrdinal("Image_Path")),
                                    reader.GetInt32(reader.GetOrdinal("Category"))
                                ));
                        }
                        return events;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
            }
            return events;
        }

    }

}
