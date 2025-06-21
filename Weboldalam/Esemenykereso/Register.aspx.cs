using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;
using System.Resources;
using System.Globalization;

public partial class Register : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["createCalendarStart"] = null;
        Session["createCalendarEnd"] = null;
    }
    protected override void InitializeCulture()
    {
        if (Session["lang"] != null)
        {
            CultureInfo Cul = CultureInfo.CreateSpecificCulture(Session["lang"].ToString());
            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul;
        }
        else
        {
            CultureInfo Cul = CultureInfo.CreateSpecificCulture("hu-HU");
            System.Threading.Thread.CurrentThread.CurrentUICulture = Cul;
            System.Threading.Thread.CurrentThread.CurrentCulture = Cul;
            Session["lang"] = "hu-HU";
        }
        base.InitializeCulture();
    }
    //Regisztráció gomb
    protected void RegisterIn_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI; MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Felhasznalok "+
                        "WHERE felh_nev=@felhnev", objSqlConnection);

                command.Parameters.Add("@felhnev", SqlDbType.VarChar).Value = felhnevTB.Text;
               
                SqlDataReader Felh_adatok = command.ExecuteReader();
              
                if (Felh_adatok.HasRows)
                {//a felh foglalt
                    Felh_adatok.Close();
                    teszt_lb.Text = Resources.String.lbRegister_ert;  //"A felhasználó név már foglalt!";
                    objSqlConnection.Close();

                }
                else
                {//Nincs ilyen felh még
                    //objSqlConnection.Open();
                    Felh_adatok.Close();

                    SqlCommand command2 = new SqlCommand("INSERT INTO Felhasznalok (szemely_nev,felh_nev,szemely_jelszo)" +
                        " OUTPUT INSERTED.szemelyID " +
                   "VALUES (@nev, @felhnev, @Pic)", objSqlConnection);
                    command2.Parameters.Add("@felhnev", SqlDbType.VarChar).Value = felhnevTB.Text;
                    command2.Parameters.Add("@nev", SqlDbType.VarChar).Value = nevTB.Text;

                    byte[] hashedPassword = GetSHA1(felhnevTB.Text, passwordTB.Text);
                    command2.Parameters.Add("Pic", SqlDbType.Image, 0).Value = hashedPassword;

                    int ujszemely_id = (int)command2.ExecuteScalar();
                    //objSqlConnection.Close();
                    teszt_lb.Text += Resources.String.lbRegister_suc;// "A felhasználó létre hozva!";

                    Session["loginname"] = felhnevTB.Text;
                    Session["szemelyID"] = ujszemely_id;
                    objSqlConnection.Close();
                    Response.Redirect("Fooldal.aspx");

                }


            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }


    }

    private static byte[] GetSHA1(string userName, string password)
    {  //jelszó kódolása
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(userName + password));
    }

    //Vissza a bejelentkezéshez
    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }


}