using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Globalization;
//using System.Web.HttpContext;

public partial class Esemenyeim : BasePage
{       
    protected void Page_Load(object sender, EventArgs e)
    {
        tesztLb.Text = "";

            Session["createCalendarStart"] = null;
            Session["createCalendarEnd"] = null;
        
        felh_neveLB.Text += (string)Session["loginname"];

        //A felh saját eseményei, létrehozta
        Esemenyeim_kiir();             

        //Események amelyeken a felh részt vesz
        Esemenyeim_resztvesz();

        InitializeCulture();
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
    public void Esemenyeim_kiir()
    { //A felh saját eseményei, létrehozta

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                SqlCommand command2 = new SqlCommand("SELECT esemeny_nev, esemeny_leiras, tipus, altipus " +
                ", mettol, meddig, orszag, varos, Esemeny_alap.esemenyID" +
                " FROM Esemeny_alap, Esemeny_hely, Tipus_altipus " +
                "WHERE Esemeny_alap.esemeny_hely=Esemeny_hely.helyID AND Esemeny_alap.tipusID=Tipus_altipus.tipusID AND Esemeny_alap.esemeny_letrehozo = '" + (int)Session["szemelyID"] + "'", objSqlConnection);

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
       
        if (esemenyek.Count >0)
        {
            for (int i = 0; i < esemenyek.Count; i++)
            {
                Label myLabel = new Label();
                ListBox mylistbox = new ListBox();

                //cimkék beállítása
                myLabel.Text = esemenynev[i] +" "+ Resources.String.lbFooldal_ert1 + "\n";// " esemény alapadatai: \n";
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
                Button myButton = new Button();
                myButton.Click += StatBT_Click;
                //az új gombokhoz megadjuk az eseményid-ját
                myButton.CommandArgument = esemenyIDs[i].ToString();
                myButton.Text = esemenynev[i] + Resources.String.lbFooldal_ert2 + "...";// " részletei...";
                myButton.ID = "Esreszlet" + (i.ToString());
                PlaceHolder1.Controls.Add(myButton);
                //Egy üres sor adunk hozzá
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
                PlaceHolder1.Controls.Add(new LiteralControl("<br />"));
            }
        }                
        else
        {
            tesztLb.Text += Resources.String.lbEsemenyeim_ert2;  //"Még nem hoztál létre saját eseményt.";              
        }
         
    }


    void StatBT_Click(object sender, EventArgs e)
    {
        int esemenyID = int.Parse((sender as Button).CommandArgument);
        Response.Redirect("Esemeny_reszletei_stat.aspx?esemenyID=" + esemenyID);

    }

    public List<string> esemenyek2 = new List<string>();
    public List<string> esemenynev2 = new List<string>();
    public List<int> esemenyIDs2 = new List<int>();
    public void Esemenyeim_resztvesz()
    {//események amelyeken a felhasználó részt vesz
        
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                SqlCommand command2 = new SqlCommand("SELECT esemeny_nev, esemeny_leiras, tipus, altipus " +
                 ", mettol, meddig, Esemeny_hely.orszag, Esemeny_hely.varos, Esemeny_alap.esemenyID " +
                " FROM Esemeny_alap, Esemeny_hely, Esemeny_erdeklodes, Tipus_altipus " +
                " WHERE Esemeny_alap.esemeny_hely=Esemeny_hely.helyID AND " +
				" Esemeny_alap.tipusID=Tipus_altipus.tipusID AND "+
				" Esemeny_alap.esemenyID=Esemeny_erdeklodes.esemenyID AND "+
                " Esemeny_erdeklodes.szemelyID =@szemelyID " +
                " AND (Esemeny_erdeklodes.allapot=2 OR Esemeny_erdeklodes.allapot=4) AND Esemeny_alap.meddig>@datenow ", objSqlConnection);


                command2.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;
                command2.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];

              SqlDataReader Esemy_alap = command2.ExecuteReader();

                while (Esemy_alap.Read())
                {
                    esemenynev2.Add(Esemy_alap.GetString(0));
                    string esemenyReszletei = "";
                    for (int i = 1; i <= 7; ++i)
                    {
                        esemenyReszletei += getStringUres(Esemy_alap, i) + "\n";
                    }
                    esemenyek2.Add(esemenyReszletei);
                    esemenyIDs2.Add(Esemy_alap.GetInt32(8));
                }
                Esemy_alap.Close();

                objSqlConnection.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
       
        if (esemenyek2.Count >0)
        {
            for (int i = 0; i < esemenyek2.Count; i++)
            {
                Label myLabel2 = new Label();
                ListBox mylistbox2 = new ListBox();

                //cimkék beállítása
                myLabel2.Text = esemenynev2[i] +" "+ Resources.String.lbFooldal_ert1 + "\n";// " esemény alapadatai: \n";
                myLabel2.ID = "Esadatok0" + (i.ToString()); //van már ilyen nevű
                PlaceHolder2.Controls.Add(myLabel2);
                PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                string[] reszletek2 = esemenyek2[i].Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < reszletek2.Length; ++j)
                    mylistbox2.Items.Add(reszletek2[j]);
                mylistbox2.ID = "EssadatokLB0" + (i.ToString());
                PlaceHolder2.Controls.Add(mylistbox2);
                PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                Button myButton2 = new Button();
                myButton2.Click += EsemenyreszletBT_Click;
                //az új gombokhoz megadjuk az eseményid-ját
                myButton2.CommandArgument = esemenyIDs2[i].ToString();
                myButton2.Text = esemenynev2[i] + Resources.String.lbFooldal_ert2 + "...";// " részletei...";
                myButton2.ID = "Esreszlet0" + (i.ToString());
                PlaceHolder2.Controls.Add(myButton2);
                //Egy üres sor adunk hozzá
                PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
                PlaceHolder2.Controls.Add(new LiteralControl("<br />"));
            }
        }                
        else
        {
            tesztLb.Text += Resources.String.lbEsemenyeim_ert;//"Még nem jelentkeztél eseményre.";               
        }
         
    }
       
    void EsemenyreszletBT_Click(object sender, EventArgs e)
    {
        int esemenyID = int.Parse((sender as Button).CommandArgument);

        //Idő elmentése
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI; MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {   //Session["loginname"]    Session["szemelyID"] = 8;
                objSqlConnection.Open();
            
                //idő mentés
                SqlCommand ido_mentes = new SqlCommand("INSERT INTO Esemeny_valtozasok(esemenyID,szemelyID,mikor,regi_allapot)" +
                       "VALUES (@esemenyID,@szemelyID,@datenow,'0');", objSqlConnection);

                ido_mentes.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                ido_mentes.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                ido_mentes.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;


                ido_mentes.ExecuteNonQuery();

                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
        Response.Redirect("Esemeny_reszletei.aspx?esemenyID=" + esemenyID);

    }

}