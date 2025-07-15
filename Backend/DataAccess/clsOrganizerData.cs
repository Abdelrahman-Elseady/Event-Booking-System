using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsOrganizerData
    {
        public static int AddNewOrganizer(string FirstName, string MidName, string LastName, int AccountID)
        {
            int insertedID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddNewOrganizer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter("@Organizer_ID", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);

                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@MidName", MidName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Account_ID", AccountID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        insertedID = (int)cmd.Parameters["@Organizer_ID"].Value;
                    }
                    catch
                    {
                        insertedID = -1;
                    }
                }
            }

            return insertedID;
        }

        public static bool UpdateOrganizer(int OrganizerID, string FirstName, string MidName, string LastName, int AccountID)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateOrganizer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Organizer_ID", OrganizerID);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@MidName", MidName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Account_ID", AccountID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            isUpdated = true;
                    }
                    catch
                    {
                        isUpdated = false;
                    }
                }
            }

            return isUpdated;
        }

        public static bool DeleteOrganizer(int OrganizerID)
        {
            bool isDeleted = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteOrganizer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Organizer_ID", OrganizerID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            isDeleted = true;
                    }
                    catch
                    {
                        isDeleted = false;
                    }
                }
            }

            return isDeleted;
        }

        public static bool FindOrganizer(int OrganizerID, ref string FirstName, ref string MidName, ref string LastName, ref int Account_ID)
        {
            bool found = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_FindOrganizer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Organizer_ID", OrganizerID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FirstName = reader["FirstName"].ToString();
                                MidName = reader["MidName"] == DBNull.Value ? null : reader["MidName"].ToString();
                                LastName = reader["LastName"].ToString();
                                Account_ID = Convert.ToInt32(reader["Account_ID"]);
                                found = true;
                            }
                        }
                    }
                    catch
                    {
                        found = false;
                    }
                }
            }

            return found;
        }

        public static DataTable GetAllOrganizers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllOrganizers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch
                    {
                        // Optional: log error
                    }
                }
            }

            return dt;
        }
    }

}
