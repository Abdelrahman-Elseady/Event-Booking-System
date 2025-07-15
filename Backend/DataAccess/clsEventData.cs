using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsEventData
    {
        public static int AddNewEvent(string EventName, string Place, DateTime Date, float Price, int Organizer_ID, string ImagePath, int Category_ID)
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
                    cmd.Parameters.AddWithValue("@EventName", EventName);
                    cmd.Parameters.AddWithValue("@Place", Place);
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Organizer_ID", Organizer_ID);
                    cmd.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(ImagePath) ? DBNull.Value : (object)ImagePath);
                    cmd.Parameters.AddWithValue("@Category_ID", Category_ID);

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
        public static bool UpdateEvent(int EventID, string EventName, string Place, DateTime Date, float Price, int Organizer_ID, string ImagePath, int Category_ID)
        {
            bool isUpdated = false;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateEvent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Event_ID", EventID);
                    cmd.Parameters.AddWithValue("@EventName", EventName);
                    cmd.Parameters.AddWithValue("@Place", Place);
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@Price", Price);
                    cmd.Parameters.AddWithValue("@Organizer_ID", Organizer_ID);
                    cmd.Parameters.AddWithValue("@Image_Path", ImagePath);
                    cmd.Parameters.AddWithValue("@Category", Category_ID);

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
        public static bool FindEvent(int EventID, ref string EventName, ref string Place, ref DateTime Date, ref float Price, ref int Organizer_ID, ref string ImagePath, ref int Category_ID)
        {
            bool found = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_FindEvent", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EventID", EventID);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            EventName = reader["EventName"].ToString();
                            Place = reader["Place"].ToString();
                            Date = Convert.ToDateTime(reader["Date"]);
                            Price = float.Parse(reader["Price"].ToString());
                            Organizer_ID = Convert.ToInt32(reader["Organizer_ID"]);
                            ImagePath = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"].ToString();
                            Category_ID = Convert.ToInt32(reader["Category_ID"]);

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
        public static DataTable GetAllEvents()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllEvents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        dt.Load(reader);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }
            return dt;
        }

    }
}
