using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for TipusService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TipusService : WebService {

    public TipusService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public CascadingDropDownNameValue[] GetDropDownContents(
        string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT altipus, tipusID " +
                    "FROM Tipus_altipus WHERE tipus='" + 
                    knownCategoryValues.Substring(0, knownCategoryValues.Length - 1).Substring(10) +
                    "'", objSqlConnection);

                SqlDataReader ddlaltipus;
                ddlaltipus = command.ExecuteReader();

                while (ddlaltipus.Read())
                {
                    values.Add(new CascadingDropDownNameValue(ddlaltipus.GetString(0), ddlaltipus.GetInt32(1).ToString()));
                }


                objSqlConnection.Close();

            }
            catch (Exception ex)
            {
                
            }
        }             


        return values.ToArray();
    }


}
