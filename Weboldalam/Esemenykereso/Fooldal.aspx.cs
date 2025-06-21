using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;

public partial class Fooldal : BasePage
{
    public string con;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["createCalendarStart"] = null;
            Session["createCalendarEnd"] = null;

            Session["esemenyNev"] = null;
            Session["leiras"] = null;
            Session["orszag"] = null;
            Session["varos"] = null;
         }

        esemnynevTB.Focus();
        if (Page.IsPostBack)
            if (Session["createCalendarStart"] != null)
            {
                Calendar1.SelectedDate = (DateTime)Session["createCalendarStart"];
                Calendar1.VisibleDate = Calendar1.SelectedDate;
            }
            else
            {
                Calendar1.SelectedDate = DateTime.Now;
                Calendar1.VisibleDate = DateTime.Now;
            }


        if (Page.IsPostBack)
            if (Session["createCalendarEnd"] != null)
            {
                Calendar2.SelectedDate = (DateTime)Session["createCalendarEnd"];
                Calendar2.VisibleDate = Calendar2.SelectedDate;
            }
            else
            {
                Calendar2.SelectedDate = DateTime.Now;
                Calendar2.VisibleDate = DateTime.Now;
            }

        Osszesesemeny();

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

    void myButton_Click(object sender, EventArgs e)
    {
        int esemenyID = int.Parse((sender as Button).CommandArgument);
        string id = (sender as Button).ID;
        //Idő elmentése
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {   //Session["loginname"]    Session["szemelyID"] = 8;
                objSqlConnection.Open();
                //jelenlegi adat lekérése
                SqlCommand jelen_adat_csekk = new SqlCommand("SELECT mod_datuma, allapot "+
                    "FROM Esemeny_erdeklodes "+
                    "WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                jelen_adat_csekk.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                jelen_adat_csekk.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                jelen_adat_csekk.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;

                SqlDataReader jelen_adat_csekk_read = jelen_adat_csekk.ExecuteReader();

                if (jelen_adat_csekk_read.Read() && !jelen_adat_csekk_read.IsDBNull(0))
                {// van jelenlegi adat                   

                    //allapot= '" megtekintette"' insert into Esemeny_valtozasokba
                    SqlCommand naplo_tabla = new SqlCommand("INSERT INTO Esemeny_valtozasok(esemenyID,szemelyID,mikor,regi_allapot)" +
                           " VALUES (@esemenyID,@szemelyID,@datenow,'0')", objSqlConnection);

                    naplo_tabla.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                    naplo_tabla.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    naplo_tabla.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now; 

                    naplo_tabla.ExecuteNonQuery();                                                       

                }
                else
                { //idő mentés a statisztikához, ha még nincs régi adat
                   
                    SqlCommand ido_mentes = new SqlCommand("INSERT INTO Esemeny_erdeklodes(esemenyID,szemelyID,allapot,mod_datuma)" +
                           " VALUES (@esemenyID,@szemelyID,'0',@datenow);", objSqlConnection);

                    ido_mentes.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                    ido_mentes.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    ido_mentes.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;

                    ido_mentes.ExecuteNonQuery();
                }      

                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
        Response.Redirect("Esemeny_reszletei.aspx?esemenyID=" + esemenyID);

    }

    protected void Kereses_Click(object sender, EventArgs e)
    {
        //beírt-e egyáltalán valamit
        if (!string.IsNullOrEmpty(esemnynevTB.Text) || !string.IsNullOrEmpty(leirasTB.Text) || !string.IsNullOrEmpty(orszagTB.Text)
            || !string.IsNullOrEmpty(varosTB.Text) || Session["createCalendarStart"] != null || Session["createCalendarEnd"] != null)
        {
            Session["esemenyNev"] = esemnynevTB.Text;
            Session["leiras"] = leirasTB.Text;
            Session["orszag"] = orszagTB.Text;
            Session["varos"] = varosTB.Text;
        }
        Osszesesemeny();
    }

    protected void Osszesesemeny()
    {
        probaLB.Text = "";
        PlaceHolder1.Controls.Clear();

                //dátumok kiszedése
        string words = Calendar1.SelectedDate.Date.ToString("yyyy-MM-dd"); //Calendar1.SelectedDate.ToString().Split(' ');
        string words2 = Calendar2.SelectedDate.Date.ToString("yyyy-MM-dd"); 

        string where = "";

        //Idő ellenőrzés
        if (Session["createCalendarStart"] != null)
        {//2015.01.16
            where += " AND mettol >= '" + words + "'";
        }

        if (Session["createCalendarEnd"] != null)
        {//2015.01.16
            where += " AND meddig <= '" + words2 + "'";
        }
        //Nev ellenörzés
        if (Session["esemenyNev"] != null && Session["esemenyNev"].ToString().Length > 0)
        {
            where += " AND Esemeny_alap.esemeny_nev LIKE '%" + Session["esemenyNev"].ToString() + "%'";
        }
        //többi adat ellenőrzése
        if (Session["leiras"] != null && Session["leiras"].ToString().Length > 0)
        {//szerepel-e benne
            where += " AND Esemeny_alap.esemeny_leiras LIKE '%" + Session["leiras"].ToString() + "%'";
        }

        if (Session["orszag"] != null && Session["orszag"].ToString().Length > 0)
        {
            where += " AND orszag LIKE '%" + Session["orszag"].ToString() + "%'";
        }
        if (Session["varos"] != null && Session["varos"].ToString().Length > 0)
        {
            where += " AND varos LIKE '%" + Session["varos"].ToString() + "%'";
        }
        
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT esemeny_nev, esemeny_leiras, tipus, altipus " +
             ", mettol, meddig, Esemeny_hely.orszag, Esemeny_hely.varos, Esemeny_alap.esemenyID " +
                        "FROM Esemeny_alap, Esemeny_hely, Tipus_altipus WHERE Esemeny_alap.esemeny_hely=Esemeny_hely.helyID " +
                        " AND Esemeny_alap.tipusID=Tipus_altipus.tipusID "+where, objSqlConnection);

                SqlDataReader Esemy_alap = command.ExecuteReader();
                esemenyek.Clear();
                esemenyIDs.Clear();
                esemenynev.Clear();
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
            { }
        }
        if (esemenyek.Count > 0)
        {
            for (int i = 0; i < esemenyek.Count; i++)
            {
                Label myLabel = new Label();
                ListBox mylistbox = new ListBox();

                //cimkék beállítása
                myLabel.Text = esemenynev[i] + Resources.String.lbFooldal_ert1 + "\n";// " esemény alapadatai: \n";
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
                myButton.Click += myButton_Click;

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
            probaLB.Text = Resources.String.lbFooldal_ert;// "Nincs eredmény!";
        }


    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Session["createCalendarStart"] = Calendar1.SelectedDate;
        Calendar1.VisibleDate = Calendar1.SelectedDate;
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Session["createCalendarEnd"] = Calendar2.SelectedDate;
        Calendar2.VisibleDate = Calendar2.SelectedDate;
    }

   

}