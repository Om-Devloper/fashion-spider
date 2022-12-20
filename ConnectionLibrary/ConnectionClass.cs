using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace ConnectionLibrary
{
    public class ConnectionClass
    {
        SqlConnection con = null;
        string strConnectionClass = string.Empty;

        private static readonly SqlConnection Connection = new SqlConnection();
        public ConnectionClass()
        {
            string connetionString;
            SqlConnection con;
            connetionString = @"Data Source=LAPTOP-3RGNJ53I\SQLEXPRESS;Database= Bulky;Trusted_Connection = True;Encrypt= false";
            con = new SqlConnection(connetionString);
            con.Open();
        }
        public DataTable Select(string strQuery)
        {
            if (con == null)
            {
                string connetionString;
                SqlConnection con;
                connetionString = @"Data Source=LAPTOP-3RGNJ53I\SQLEXPRESS;Database= Bulky;Trusted_Connection = True;Encrypt= false";
                con = new SqlConnection(connetionString);
                con.Open();
                DataTable dt = new DataTable();
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //var count  =(int)cmd.ExecuteScalar();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                DataTable dt = new DataTable();
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //var count  =(int)cmd.ExecuteScalar();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    con.Close();
                }
            }

        }
        public int InsertNew(string strQuery)
        {
            if (con == null)
            {
                string connetionString;
                SqlConnection con;
                connetionString = @"Data Source=LAPTOP-3RGNJ53I\SQLEXPRESS;Database= Bulky;Trusted_Connection = True;Encrypt= false";
                con = new SqlConnection(connetionString);
                con.Open();
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    //SqlCommand command;
                    //SqlDataAdapter adapter = new SqlDataAdapter();
                    //command = new SqlCommand(strQuery, con);

                    //adapter.InsertCommand = new SqlCommand(sql, con);
                    //adapter.InsertCommand.ExecuteNonQuery();


                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Class Update Function Problem : {ex.Message}");
                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection Class Update Function Problem : {ex.Message}");
                    return 0;
                }
                finally
                {
                    con.Close();
                }
            }
            
        }
        public int Insert(string strQuery, out string StrErrorMessage)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(strQuery, con);
                StrErrorMessage = string.Empty;
                return Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                StrErrorMessage = ex.Message.Replace("'", " ");
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        public int Delete(string strQuery)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(strQuery, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection Class Delete Function Problem : {ex.Message}");
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        public int Update(string strQuery)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand(strQuery, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection Class Update Function Problem : {ex.Message}");
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        //public bool SaveTHtml(string organization, string name, string content)
        //{
        //    try
        //    {
        //        var DocumentFinalDirectory = ConfigurationManager.AppSettings["finalpath"];
        //        var filePath = $"{DocumentFinalDirectory}\\{DateTime.Now.Month}{DateTime.Now.Year}\\{DateTime.Now.Day}\\{organization}";
        //        Directory.CreateDirectory(filePath);

        //        FileStream fileStream = new FileStream(filePath + "\\" + name + ".html", FileMode.Create);
        //        using (fileStream)
        //        {
        //            using (StreamWriter w = new StreamWriter(fileStream, Encoding.UTF8))
        //            {
        //                w.Write(content);
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Doucment Save Problem {ex.Message}");
        //        return false;
        //    }
        //}
        public T ExecuteSQLProcedure<T>(string procedureName, SqlParameter[] parameters)
        {
            try
            {
                var sqlCommand = new SqlCommand(procedureName, con) { CommandType = CommandType.StoredProcedure };

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                if (parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        if (parameters != null) sqlCommand.Parameters.Add(parameter);
                    }
                }

                if (typeof(T) == typeof(DataTable))
                {
                    var dt = new DataTable();
                    var dataAdaper = new SqlDataAdapter(sqlCommand);
                    sqlCommand.CommandTimeout = 999999;
                    dataAdaper.Fill(dt);
                    return (T)Convert.ChangeType(dt, typeof(T));
                }
                else
                {
                    return (T)sqlCommand.ExecuteScalar();
                }
                return default;
            }
            catch (SqlException e)
            {
                return default;
            }
            finally
            {
                con.Close();
            }
        }
        public async static Task<string> GetContentAsStringAync(string url)
        {
            int ReReques = 0;
        Retry:
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var client = new HttpClient();
                    // client.Timeout = TimeSpan.FromMinutes(5000);
                    client.DefaultRequestHeaders.Add("User-Agent",
                                                     "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
                    client.DefaultRequestHeaders.Add("Connection", "Keep-alive");
                    client.DefaultRequestHeaders.ConnectionClose = true;

                    using (client)
                    {
                        return await client.GetAsync(url).GetAwaiter()
                     .GetResult().Content.ReadAsStringAsync();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                ReReques++;
                if (ReReques <= 3)
                {
                    goto Retry;
                }
                Console.WriteLine($"GetContentAsStringAync : {ex.Message}");
                return null;
            }
        }
    }
}