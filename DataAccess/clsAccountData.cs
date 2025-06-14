using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsAccountData
    {
        public static int AddNewAccount(string userName, string password, bool isActive)
        {
            int insertedID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddNewAccount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter("@Account_ID", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);

                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        insertedID = (int)cmd.Parameters["@Account_ID"].Value;
                    }
                    catch
                    {
                        insertedID = -1;
                    }
                }
            }

            return insertedID;
        }

        public static bool UpdateAccount(int accountID, string userName, string password, bool isActive)
        {
            bool isUpdated = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateAccount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Account_ID", accountID);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        isUpdated = rowsAffected > 0;
                    }
                    catch
                    {
                        isUpdated = false;
                    }
                }
            }

            return isUpdated;
        }

        public static bool DeleteAccount(int accountID)
        {
            bool isDeleted = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteAccount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Account_ID", accountID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        isDeleted = rowsAffected > 0;
                    }
                    catch
                    {
                        isDeleted = false;
                    }
                }
            }

            return isDeleted;
        }

        public static bool FindAccount(int accountID, ref string userName, ref string password, ref bool isActive)
        {
            bool found = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_FindAccount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Account_ID", accountID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userName = reader["UserName"].ToString();
                                password = reader["Password"].ToString();
                                isActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static DataTable GetAllAccounts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllAccounts", conn))
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
                        // Optional: log exception
                    }
                }
            }

            return dt;
        }
    }

}
