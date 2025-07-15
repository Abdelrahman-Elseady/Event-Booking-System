using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsUserData
    {
        public static int AddNewUser(string FirstName,string MidName,string LastName,int Age,int AccountID)
        {
            int InsertedID = -1;
            using(SqlConnection conn=new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_AddNewUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter idParam = new SqlParameter("@UserID", SqlDbType.Int);
                    idParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idParam);

                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@MidName", MidName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Age", Age);
                    cmd.Parameters.AddWithValue("@Account_ID", AccountID);


                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        InsertedID = (int)cmd.Parameters["@User_ID"].Value;
                    }
                    catch(Exception ex)
                    {
                        InsertedID = -1;
                    }
                }
            }
            return InsertedID;
        }
        public static bool UpdateUser(int UserID,string FirstName,string MidName,string LastName,int Age, int AccountID)
        {
            bool IsUpdated = false;
            using(SqlConnection conn=new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_UpdateUser", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@FirstName",FirstName);
                    cmd.Parameters.AddWithValue("@MidName", MidName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Age", Age);
                    cmd.Parameters.AddWithValue("@Account_ID", AccountID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            IsUpdated = true;
                    }
                    catch (Exception ex)
                    {
                        IsUpdated = false;
                    }
                }
            }
            return IsUpdated;
        }
        public static bool DeleteUser(int UserID)
        {
            bool isDeleted = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_ID", UserID);

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

        public static bool FindUser(int ID, ref string FirstName, ref string MidName, ref string LastName, ref int Age, ref int Account_ID)
        {
            bool found = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_FindUser", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_ID", ID);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FirstName = reader["FirstName"].ToString();
                            MidName = reader["MidName"]==DBNull.Value ? null : reader["MidName"].ToString();
                            LastName = reader["LastName"].ToString();
                            Age = Convert.ToInt32(reader["Age"]);
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

            return found;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAllUsers", conn))
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
                    }
                }
            }

            return dt;
        }


    }
}
