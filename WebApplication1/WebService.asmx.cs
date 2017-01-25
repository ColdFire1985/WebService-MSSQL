using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Wenn der Aufruf dieses Webdiensts aus einem Skript zulässig sein soll, heben Sie mithilfe von ASP.NET AJAX die Kommentarmarkierung für die folgende Zeile auf. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        public WebService()
        {

            string conString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        /************************************************************
        GET / SELECT    
        *************************************************************/
        [WebMethod]
        public DataTable Get()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customers"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "Customers";
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        /************************************************************
        INSERT
        *************************************************************/
        [WebMethod]
        public int Insert(string sUserName, string sPass, string sEmail, string sGender)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Insert into dbo.tblEmployee(UserName,Pass, Email, Gender) values('" + sUserName + "','" + sPass + "','" + sEmail + "','" + sGender + "')", conn);
                    int row = cmd.ExecuteNonQuery();
                    return row;
                }
            }
            catch (SqlException ex)
            {
                return (ex.ErrorCode);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            
        }

        /************************************************************
         UPDATE
        *************************************************************/
        [WebMethod]
        public int Update(string sUserName, string sPass, string sEmail, string sGender, int iid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update tblEmployee set UserName='" + sUserName + "', Pass ='" + sPass + "', Email ='" + sEmail + "' , Gender='" + sGender + "' where ID='" + iid.ToString() + "'", conn);
                    int row = cmd.ExecuteNonQuery();
                    return row;
                }
            }
            catch (SqlException ex)
            {
                return (ex.ErrorCode);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


        /************************************************************
        DELETE
        *************************************************************/
        [WebMethod]
        public int Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("delete tblEmployee where ID='" + id + "'", conn);
                    int row = cmd.ExecuteNonQuery();
                    return row;
                }
            }
            catch (SqlException ex)
            {
                return (ex.ErrorCode);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }

}/*END*/

