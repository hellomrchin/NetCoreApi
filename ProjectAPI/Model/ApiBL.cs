using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Model
{
    public class ApiBL
    {
        private string _connectionString;

        public ApiBL(string _connString)
        {
            this._connectionString = _connString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public List<ApiDM> GetApiList()
        {
            List<ApiDM> lstOfApi = new List<ApiDM>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tApi", conn);
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    lstOfApi.Add(new ApiDM()
                    {
                        ApiId = rd["fApiId"].ToString(),
                        ApiDesc = rd["fApiDesc"].ToString()
                    });
                }

                conn.Close();
            }

            return lstOfApi;
        }

        public List<ApiDM> GetApiList(string id)
        {
            List<ApiDM> lstOfApi = new List<ApiDM>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tApi WHERE fApiId = @ApiId", conn);
                cmd.Parameters.AddWithValue("@ApiId", id);
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    lstOfApi.Add(new ApiDM()
                    {
                        ApiId = rd["fApiId"].ToString(),
                        ApiDesc = rd["fApiDesc"].ToString()
                    });
                }

                conn.Close();
            }

            return lstOfApi;
        }

        public string CreateApi(ApiDM apiDm)
        {
            string _returnedMsg = "";
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO tApi(fApiId, fApiDesc) VALUES (@ApiId, @ApiDesc)", conn);
                    cmd.Parameters.AddWithValue("@ApiId", apiDm.ApiId);
                    cmd.Parameters.AddWithValue("@ApiDesc", apiDm.ApiDesc);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                _returnedMsg = "API Successfully Added";
            }
            catch (Exception e)
            {
                _returnedMsg = "Operation Failed!";
            }

            return _returnedMsg;
        }

        public string UpdateApi(string apiId, ApiDM apiDm)
        {
            string _returnedMsg = "";
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE tApi SET fApiDesc = @ApiDesc WHERE fApiId = @ApiId", conn);
                    cmd.Parameters.AddWithValue("@ApiId", apiId);
                    cmd.Parameters.AddWithValue("@ApiDesc", apiDm.ApiDesc);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    _returnedMsg = "API Successfully Updated";
                }
            }
            catch (Exception e)
            {
                _returnedMsg = "Operation Failed!";
            }

            return _returnedMsg;
        }

        public string DeleteApi(string apiId)
        {
            string _returnedMsg = "";
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM tApi WHERE fApiId = @ApiId", conn);
                    cmd.Parameters.AddWithValue("@ApiId", apiId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    _returnedMsg = "Api Successfully Deleted";
                }
            }
            catch (Exception e)
            {
                _returnedMsg = "Operation Failed";
            }

            return _returnedMsg;
        }
    }
}
