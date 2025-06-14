using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsEventCategoryData
    {
        public static int AddNewCategory(string name, string description)
        {
            int id = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_AddNewCategory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter outId = new SqlParameter("@Event_Category_ID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(outId);
                cmd.Parameters.AddWithValue("@Category_Name", name);
                cmd.Parameters.AddWithValue("@Category_Description", description ?? (object)DBNull.Value);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Event_Category_ID"].Value;
                }
                catch { id = -1; }
            }

            return id;
        }

        public static bool UpdateCategory(int id, string name, string description)
        {
            bool updated = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateCategory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Event_Category_ID", id);
                cmd.Parameters.AddWithValue("@Category_Name", name);
                cmd.Parameters.AddWithValue("@Category_Description", description ?? (object)DBNull.Value);

                try
                {
                    conn.Open();
                    updated = cmd.ExecuteNonQuery() > 0;
                }
                catch { updated = false; }
            }

            return updated;
        }

        public static bool DeleteCategory(int id)
        {
            bool deleted = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_DeleteCategory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event_Category_ID", id);

                try
                {
                    conn.Open();
                    deleted = cmd.ExecuteNonQuery() > 0;
                }
                catch { deleted = false; }
            }

            return deleted;
        }

        public static bool FindCategory(int id, ref string name, ref string description)
        {
            bool found = false;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_FindCategory", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event_Category_ID", id);

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader["Category_Name"].ToString();
                            description = reader["Category_Description"]?.ToString();
                            found = true;
                        }
                    }
                }
                catch { found = false; }
            }

            return found;
        }

        public static DataTable GetAllCategories()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAllCategories", conn))
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
                catch { }
            }

            return dt;
        }
    }

}
