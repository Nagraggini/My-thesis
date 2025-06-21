using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
//using System.Web.HttpContext;
using System.Security.Cryptography;
using System.Globalization;
using System.Data;

public partial class Beallitasok : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
            if (!Page.IsPostBack) //mert a button clikk előtt fut le a page load
                set_neme();
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
    public void set_neme()
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb2;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                //TODO: Aktuális felhasználó kell! A login sessiont is meg kell változtatni
                SqlCommand command = new SqlCommand("SELECT szemely_nem "+
                    "FROM Felhasznalok "+
                       "WHERE Felhasznalok.felh_nev='" + (string)Session["loginname"] + "' ", objSqlConnection);
                SqlDataReader read=command.ExecuteReader();
                if (read.Read() && !read.IsDBNull(0))//ha nincs sor nem állítbe semmit
                {
                    DropDownList1.Items[0].Selected = false;
                    DropDownList1.Items[1].Selected = false;
                    DropDownList1.Items.FindByValue(read.GetBoolean(0) ? "1" : "0").Selected = true; //az eredetileg beállított legyen
                    
                    //mentés nő=0 
                }
                read.Close();
                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }    
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        
        string set = "";
    
        byte[] hashedpassword = null;
        if (!string.IsNullOrEmpty(passTB.Text))
        {//mentés           
            hashedpassword = GetSHA1((string)Session["loginname"], passTB.Text);
            set += " szemely_jelszo=@Pic ,";
        }
        if (!string.IsNullOrEmpty(nevTB.Text))
        {//mentés
            set += " szemely_nev='" + nevTB.Text + "' ,";
        }
        
        if (!string.IsNullOrEmpty(szulevTB.Text))
        {//mentés
            int t;
            bool l = int.TryParse(szulevTB.Text, out t);
            //t-ben lesz az érték
            if (l && t >= DateTime.Now.Year - 100 && t <= DateTime.Now.Year)
            {
                set += " szemely_szulev='" + szulevTB.Text + "' ,";
            }
            else {
                szulevTB.Text = "";
            }
            
        } 
        //mentés nő=0
            set += " szemely_nem='" + DropDownList1.SelectedValue + "' ,";
        
        if (!string.IsNullOrEmpty(helyTB.Text))
        {//mentés
            set += " szemely_hely='" + helyTB.Text + "' ,";
        }
        if (!string.IsNullOrEmpty(TextBox7.Text))
        {//mentés
            set += " szemely_email='" + TextBox7.Text + "' ,";
        }
        //A nagy select végén ne , legyen            
        string where = set.Substring(0, set.Length - 1);
        //tesztLb.Text = where;
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                // A felhasználó név változtatáshoz "felh már foglalt kíirás"
                //objSqlConnection.Open();
                //SqlCommand command2 = new SqlCommand("SELECT * FROM Felhasznalok WHERE felh_nev=" +
                //    "'" + felhnevTB.Text + "'", objSqlConnection);

                //SqlDataReader Felh_adatok = command2.ExecuteReader();

                //if (Felh_adatok.HasRows)
                //{//a felh foglalt
                //    Felh_adatok.Close();
                //    ErtesitesLb.Text = Resources.String.lbBeallitasok_ert2;  //"A felhasználó név már foglalt!";
                //    objSqlConnection.Close();

                //}
                //else
                //{//Nincs ilyen felh még

                //    Felh_adatok.Close();
                //}

                objSqlConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE Felhasznalok " +
                   "SET " + where + "" +
                   "WHERE felh_nev='" + (string)Session["loginname"] + "';", objSqlConnection);

                //Aktuális felhasználó kell!
                if (hashedpassword !=null)
                {
                    command.Parameters.Add("Pic", SqlDbType.Image, 0).Value = hashedpassword;
                }
               
                command.ExecuteNonQuery();
                              
                objSqlConnection.Close();
                //if (!string.IsNullOrEmpty(felhnevTB.Text))
                //    Session["loginname"] = felhnevTB.Text;
                
                              
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
    }//metódus záró

    private static byte[] GetSHA1(string userName, string password)
    {  //jelszó kódolása
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(userName + password));
    }
    

}