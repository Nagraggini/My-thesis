<%@ WebHandler Language="C#" Class="ImgHandler" %>

using System;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlClient.SqlConnection;
//using System.Drawing.Image;

public class ImgHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //Checking whether the imagebytes session variable have anything else not doing anything

        //if ((context.Session["ImageBytes"]) != null)
        //{
        //    byte[] image = (byte[])(context.Session["ImageBytes"]);
        //    context.Response.ContentType = "image/JPEG";
        //    context.Response.BinaryWrite(image);
        //}
        
        System.Data.SqlClient.SqlDataReader rdr = null;
        System.Data.SqlClient.SqlConnection conn = null;
        System.Data.SqlClient.SqlCommand selcmd = null;

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (conn = new SqlConnection(connectionString))
        {
            try
            {                
                //objSqlConnection.Open();
                //objSqlConnection.Close();
                //conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Esemenydb2ConnectionString"].ConnectionString);
                //selcmd = new System.Data.SqlClient.SqlCommand("select kep from Esemeny_alap where esemenyID=" + context.Request.QueryString["imgid"], conn);
                selcmd = new System.Data.SqlClient.SqlCommand("select kep from Esemeny_alap where esemenyID='" + context.Request.QueryString["esemenyID"] + "'", conn);
                conn.Open();  //select pic1 from msgdet2 where msgdet2id=
                rdr = selcmd.ExecuteReader();

                if (!rdr.Read() || rdr.IsDBNull(0))
                {                  
                    
                    rdr.Close();
                }
                else
                {
                    //while (rdr.Read())
                    {
                        context.Response.ContentType = "image/jpg";
                        context.Response.BinaryWrite((byte[])rdr["kep"]);
                        rdr.Close();
                    }
                }
                //while (rdr.Read())
                //{
                //    context.Response.ContentType = "image/jpg";
                //    context.Response.BinaryWrite((byte[])rdr["kep"]);
                //}
                //if (rdr != null)
                //    rdr.Close();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

        }
    }

    

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}