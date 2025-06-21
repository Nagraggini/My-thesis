using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Globalization;


public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        felhnevTB.Focus();
        Fiz_esemny();

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

    protected void Register_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    
    }

    public string getStringUres(SqlDataReader Esemy_alap, int index)
    {
        if (Esemy_alap.IsDBNull(index))
        {
            return "";
        }
        return Esemy_alap.GetValue(index).ToString();
    }

    public List<string> esemenyek = new List<string>();
    public List<string> esemenynev = new List<string>();
    public List<int> esemenyIDs = new List<int>();
    //Fizetett események kiírása
    public void Fiz_esemny()
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                //nev, leiras,tipusok!,mettol, meddig, esem hely!
                SqlCommand command2 = new SqlCommand("SELECT esemeny_nev, esemeny_leiras,"+
                    " Tipus_altipus.tipus, Tipus_altipus.altipus" +
                    " , mettol, meddig, Esemeny_hely.orszag, Esemeny_hely.varos, Esemeny_alap.esemenyID" +
                    " FROM Esemeny_alap, Esemeny_hely, Tipus_altipus" +
                    " WHERE Esemeny_alap.esemeny_hely=Esemeny_hely.helyID AND" +
                    " Esemeny_alap.tipusID=Tipus_altipus.tipusID AND Esemeny_alap.fizetett='1'", objSqlConnection);

                SqlDataReader Esemy_alap = command2.ExecuteReader();

                while (Esemy_alap.Read())
                {
                    esemenynev.Add(Esemy_alap.GetString(0));
                    string esemenyReszletei = "";
                    for (int i = 1; i <= 7; ++i)
                    {
                        esemenyReszletei += getStringUres(Esemy_alap, i) + "\n";
                    }
                    esemenyek.Add(esemenyReszletei);
                    esemenyIDs.Add(Esemy_alap.GetInt32(8));
                }                          
                Esemy_alap.Close();

                objSqlConnection.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
        if (esemenyek != null)
        {
            for (int i = 0; i < esemenyek.Count; i++)
            {
                Label myLabel = new Label();
                ListBox mylistbox = new ListBox();

                //cimkék beállítása
                myLabel.Text = esemenynev[i] + " " + Resources.String.lbLogin_fiz + "\n"; //+" esemény alapadatai: \n";
                myLabel.ID = "Esadatok" + (i.ToString()); //van már ilyen nevű
                PlaceHolder1.Controls.Add(myLabel);
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                string[] reszletek = esemenyek[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < reszletek.Length; ++j)
                    mylistbox.Items.Add(reszletek[j]);
                mylistbox.ID = "EssadatokLB" + (i.ToString());
                PlaceHolder1.Controls.Add(mylistbox);
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                
            }
        }
    }

    //Bejelentkezés gomb
    protected void Login_Click(object sender, EventArgs e)
    {
        bool l=false;
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                //van-e ilyen felh név
                SqlCommand command = new SqlCommand("SELECT szemely_jelszo, szemelyID "+
                        "FROM Felhasznalok WHERE felh_nev=@felhnev", objSqlConnection);

                command.Parameters.Add("@felhnev", SqlDbType.VarChar).Value = felhnevTB.Text;

                SqlDataReader Felh_adatok = command.ExecuteReader();

               

                if (!Felh_adatok.HasRows)
                { l=false;
                    //Nincs ilyen felh
                teszt_lb.Text = Resources.String.lbLogin_ert; 
                    Felh_adatok.Close();
                }
                else
                {
                    l=true;
                  Felh_adatok.Read();
                    byte[] hashedPassword = GetSHA1(felhnevTB.Text, passTB.Text); //most írt be a felh
                    byte[] realPassword = (byte[])Felh_adatok[0];
                    if (MatchSHA1(hashedPassword, realPassword))
                    {
                        Console.WriteLine("Log him in!");
                        //login név sessionbe mentése
                        Session["szemelyID"] = Felh_adatok.GetInt32(1);
                        Session["loginname"] = felhnevTB.Text;
                      
                    }
                    else
                    {
                        l = false;
                        teszt_lb.Text = Resources.String.lbLogin_ert; 
                    }
                    Felh_adatok.Close();                       
                }
              
                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
        if (l)
        {
            Response.Redirect("Fooldal.aspx");    
        }
           
    }

    private static byte[] GetSHA1(string userName, string password)
    {  //jelszó kódolása
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        return sha.ComputeHash(System.Text.Encoding.ASCII.GetBytes(userName + password));
    }
    //ellenőrzi a jelszót
    private static bool MatchSHA1(byte[] p1, byte[] p2)
    {
        // If the two SHA1 hashes are the same, returns true.
        // Otherwise returns false.
        bool result = false;
        if (p1 != null && p2 != null)
        {
            if (p1.Length == p2.Length)
            {
                result = true;
                for (int i = 0; i < p1.Length; i++)
                {
                    if (p1[i] != p2[i])
                    {
                        result = false;
                        break;
                    }
                }
            }
        }
        return result;
    }
    

}