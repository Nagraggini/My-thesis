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


public partial class Esemeny_reszletei : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        esemenyID = Int32.Parse(Request.Params["esemenyID"]);

        Session["createCalendarStart"] = null;
        Session["createCalendarEnd"] = null;

        if (!Page.IsPostBack) //mert a button clikk előtt fut le a page load
            set_allapot();
        Session["esemenyID"] = esemenyID;

        esemeny_feltolt();

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT kep FROM Esemeny_alap" +
                    " WHERE esemenyID=@esemenyID", objSqlConnection);

                command.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                SqlDataReader read = command.ExecuteReader();
                read.Read();
                if (!read.IsDBNull(0))
                {
                    ImagePreview.ImageUrl = "~/ImgHandler.ashx?esemenyID=" + Request.Params["esemenyID"] + "";
                }
                //lejárt az esemény és ott volt

                SqlCommand allapot_csekk = new SqlCommand("SELECT allapot " +
                    "FROM Esemeny_erdeklodes, Esemeny_alap " +
                    "WHERE szemelyID=@szemelyID AND Esemeny_erdeklodes.esemenyID=@esemenyID " +
                    "AND Esemeny_erdeklodes.esemenyID=Esemeny_alap.esemenyID AND meddig<@datenow "+
                    "AND (allapot='2' OR allapot='4')", objSqlConnection);
                //allapot csekkolása, hogy mehet-e a 4-re modositas

                allapot_csekk.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;
                allapot_csekk.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                allapot_csekk.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                SqlDataReader allapot_csekk_read = allapot_csekk.ExecuteReader();

                //értékelés funkció 
                //ha ott volt és lejárt akkor állapot 4-re modósítás
                if (allapot_csekk_read.Read() && !allapot_csekk_read.IsDBNull(0))
                {//insert into allapot 4-re modositas
                    SqlCommand allapot_update = new SqlCommand("Update [Esemeny_erdeklodes]  " +
                    "SET allapot='4' WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                    allapot_update.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    allapot_update.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                    allapot_update.ExecuteNonQuery();
                    allapotDL.Visible = false;
                    allapotmentBT.Visible=false;
                    allapot_ott_volt.Visible = true;

                    ertekelesDL.Visible = true;
                    ertekelesmentBT.Visible = true;
                }
                else
                {

                    //esemeny ideje lejart-e?
                    SqlCommand ido_csekk = new SqlCommand("SELECT allapot " +
                        "FROM Esemeny_erdeklodes, Esemeny_alap " +
                        "WHERE szemelyID=@szemelyID AND Esemeny_erdeklodes.esemenyID=@esemenyID " +
                        "AND Esemeny_erdeklodes.esemenyID=Esemeny_alap.esemenyID AND meddig<@datenow " +
                        " ", objSqlConnection);

                    ido_csekk.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;
                    ido_csekk.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    ido_csekk.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                    SqlDataReader ido_csekk_read = ido_csekk.ExecuteReader();
                    //lejárt eseményre már nincs jelentkezés
                    if (ido_csekk_read.Read() && !ido_csekk_read.IsDBNull(0))
                    {//Nem jelentkezett rá szóval nincs értékelés sem
                        allapotDL.Visible = false;
                        allapotmentBT.Visible = false;
                        allapotKIIR.Visible = false;

                        ertekelesDL.Visible = false;
                        ertekelesmentBT.Visible = false;
                    }
                }

                if (!Page.IsPostBack)
                {
                    SqlCommand command2 = new SqlCommand("SELECT ertekeles FROM Esemeny_erdeklodes" +
                        " WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID AND allapot='4'", objSqlConnection);

                    command2.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    command2.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                    SqlDataReader read2 = command2.ExecuteReader();
                    if (read2.Read() && !read2.IsDBNull(0))
                    { //A régi értékelést mutatja.
                        ertekelesDL.Visible = true;
                        ertekelesmentBT.Visible = true;
                        ertekelesDL.SelectedValue = getStringUres(read2, 0);
                    }
                }
               
                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
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

    public List<string> esemenyek = new List<string>();
    public void esemeny_feltolt()
    {

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                SqlCommand Esemny_alap = new SqlCommand("SELECT Felhasznalok.felh_nev, esemeny_nev," +
                    " esemeny_leiras, mettol, meddig, Esemeny_hely.orszag , Esemeny_hely.varos, Esemeny_hely.utca," +
                    " Esemeny_hely.hazszam, Esemeny_hely.iranyitoszam, Tipus_altipus.tipus, " +
                    "Tipus_altipus.altipus " +
                    " FROM Esemeny_alap, Esemeny_hely,Felhasznalok,Tipus_altipus" +
                    " WHERE Esemeny_alap.esemeny_hely=Esemeny_hely.helyID AND " +
                    "Esemeny_alap.esemeny_letrehozo=Felhasznalok.szemelyID AND " +
                    "Esemeny_alap.tipusID=Tipus_altipus.tipusID AND " +
                    "Esemeny_alap.esemenyID=@esemenyID ", objSqlConnection);

                Esemny_alap.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                SqlDataReader Esemny_alap_dr = Esemny_alap.ExecuteReader();

                while (Esemny_alap_dr.Read())
                {

                    string esemenyReszletei = "";
                    for (int i = 0; i <= 11; ++i)
                    {
                        esemenyReszletei += getStringUres(Esemny_alap_dr, i) + "\n";
                    }
                    esemenyek.Add(esemenyReszletei);

                }//kéta dat!!!!!!!!!
                Esemny_alap_dr.Close();

                SqlCommand hanyan_mennek = new SqlCommand("SELECT COUNT(*) " +
                   " FROM Esemeny_erdeklodes" +
                   " WHERE esemenyID=@esemenyID AND (allapot='2' OR allapot='4')", objSqlConnection);

                hanyan_mennek.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                SqlDataReader Hanyan = hanyan_mennek.ExecuteReader();


                string[] reszletek = new string[12];
                for (int i = 0; i < esemenyek.Count; i++)
                {
                    //Esemny_reszletei: 0letrehozó,1esemneve, 2leíras, 3mettol, 4meddig,5ország, 6város, 
                    //7utca, 8házszám, 9iranyitoszam, 10tipu, 11altipus, !!12hanyan mennek
                    //Adatok beállítása
                    reszletek = esemenyek[i].Split(new string[] { "\n" }, StringSplitOptions.None);
                }
                esemnyletrehozoLB.Text = "" + reszletek[0]; //kötelező   

                EsemenyneveLB.Text = "" + reszletek[1]; //kötelező

                if (!reszletek[2].Equals(""))
                {//van benne vmi
                    LeirasLB.Visible = true;
                    LeirasLB.Items.Add(reszletek[2]);
                    esemenyleirKIIR.Visible = true;

                }

                if (!reszletek[3].Equals(""))
                {//van benne vmi
                    mettolLB.Visible = true;
                    mettolLB.Text = "" + reszletek[3];
                    mettolKIIR.Visible = true;
                }

                if (!reszletek[4].Equals(""))
                {//van benne vmi
                    meddigLB.Visible = true;
                    meddigLB.Text = "" + reszletek[4];
                    meddigKIIR.Visible = true;
                }

                //Esemny_reszletei: 0letrehozó,1esemneve, 2leíras, 3mettol, 4meddig,5ország, 6város, 
                //7utca, 8házszám, 9iranyitoszam, 10tipu, 11altipus, !!12hanyan mennek
                //Adatok beállítása
                if (reszletek[5].Equals("") && reszletek[6].Equals("") && reszletek[7].Equals("") &&
                        reszletek[8].Equals("") && reszletek[9].Equals(""))
                {
                    orszagLB.Visible = true;
                    orszagLB.Text = Resources.String.lbEsemnyreszl_ert; //"Nincs megadva helyszín! ";
                }
                else
                {
                    if (!reszletek[5].Equals("") || !reszletek[6].Equals("") || !reszletek[7].Equals("") || !reszletek[8].Equals("0")
                        || !reszletek[9].Equals("0"))
                    {
                        esemenyhelyKIIR.Visible = true;
                    }

                    if (!reszletek[5].Equals(""))
                    {//van benne vmi
                        orszagLB.Visible = true;
                        orszagLB.Text = "" + reszletek[5];
                        orszagKIIR.Visible = true;
                    }

                    if (!reszletek[6].Equals(""))
                    {//van benne vmi
                        varosLB.Visible = true;
                        varosLB.Text = "" + reszletek[6];
                        varosKIIR.Visible = true;
                    }
                    if (!reszletek[7].Equals(""))
                    {//van benne vmi
                        utcaLB.Visible = true;
                        utcaLB.Text = "" + reszletek[7];
                        utcaKIIR.Visible = true;
                    }
                    if (!reszletek[8].Equals("0"))
                    {//van benne vmi
                        hazszamLB.Visible = true;
                        hazszamLB.Text = "" + reszletek[8];
                        hazszamKIIR.Visible = true;
                    }
                    if (!reszletek[9].Equals("0"))
                    {//van benne vmi
                        iranyszLB.Visible = true;
                        iranyszLB.Text = "" + reszletek[9];
                        iranyszKIIR.Visible = true;
                    }
                }
                if (!reszletek[10].Equals(""))//tipus
                {//van benne vmi
                    tipusLB.Visible = true;
                    tipusLB.Text = "" + reszletek[10];
                    tipusKIIR.Visible = true;
                }

                if (!reszletek[11].Equals(""))
                {//van benne vmi
                    altipusLB.Visible = true;
                    altipusLB.Text = "" + reszletek[11];
                    tipusKIIR.Visible = true;
                }

                Hanyan.Read();
                eddigmennekLB.Text = Hanyan.GetInt32(0).ToString();
                Hanyan.Close();
                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
    }//metódus záró

    public string getStringUres(SqlDataReader Esemy_alap, int index)
    {
        if (Esemy_alap.IsDBNull(index))
        {
            return "";
        }
        return Esemy_alap.GetValue(index).ToString();
    }

    public int esemenyID; 
    protected void Save_Click(object sender, EventArgs e)
    {
      
        //Idő elmentése
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {   //Session["loginname"]    Session["szemelyID"] = 8;
                objSqlConnection.Open();
                //jelenlegi adat lekérése
                SqlCommand jelen_adat_csekk = new SqlCommand("SELECT mod_datuma, allapot " +
                    "FROM Esemeny_erdeklodes " +
                    "WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                jelen_adat_csekk.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                jelen_adat_csekk.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];

                SqlDataReader jelen_adat_csekk_read = jelen_adat_csekk.ExecuteReader();

                if (jelen_adat_csekk_read.Read() && jelen_adat_csekk_read.HasRows)
                {
                    DateTime mod_datuma = jelen_adat_csekk_read.GetDateTime(0);
                    int allapot = jelen_adat_csekk_read.GetInt32(1);

                    //regi allapot insert into Esemeny_valtozasokba
                    SqlCommand naplo_tabla = new SqlCommand("INSERT INTO Esemeny_valtozasok(esemenyID,szemelyID,mikor,regi_allapot)" +
                           " VALUES (@esemenyID,@szemelyID,@mod_datuma,@allapot)", objSqlConnection);

                    naplo_tabla.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                    naplo_tabla.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    naplo_tabla.Parameters.Add("@mod_datuma", SqlDbType.DateTime).Value = mod_datuma;
                    naplo_tabla.Parameters.Add("@allapot", SqlDbType.Int).Value = allapot;

                    naplo_tabla.ExecuteNonQuery();

                    //  új allapot update az Esemeny_erdeklodesbe

                    SqlCommand allapot_update = new SqlCommand("Update [Esemeny_erdeklodes]  " +
                    "SET allapot=@allapot, mod_datuma=@datenow WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                    allapot_update.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    allapot_update.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];
                    allapot_update.Parameters.Add("@allapot", SqlDbType.Int).Value = Int32.Parse(allapotDL.SelectedValue);
                    allapot_update.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;

                    allapot_update.ExecuteNonQuery();
                }
                else { 
                    //még nincs allapot
                                     

                    SqlCommand naplo_tabla = new SqlCommand("INSERT INTO Esemeny_erdeklodes(esemenyID,szemelyID,allapot, ertekeles, "+
                        " mod_datuma)" +
                         " VALUES (@esemenyID,@szemelyID,@allapot,NULL, @mod_datuma)", objSqlConnection);

                    naplo_tabla.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                    naplo_tabla.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    naplo_tabla.Parameters.Add("@mod_datuma", SqlDbType.DateTime).Value = DateTime.Now;
                    naplo_tabla.Parameters.Add("@allapot", SqlDbType.Int).Value = Int32.Parse(allapotDL.SelectedValue);

                    naplo_tabla.ExecuteNonQuery();
                
                }

                

                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
            Response.Redirect("Esemeny_reszletei.aspx?esemenyID=" + Request.Params["esemenyID"]);
        }
       
    }
    //Értékelés mentése adatbázisba
    protected void Save_Click2(object sender, EventArgs e)
    {
          //Idő elmentése
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {   
                objSqlConnection.Open();

                SqlCommand regi_allapot_lehivasa = new SqlCommand("SELECT allapot " +
                   "FROM Esemeny_erdeklodes " +
                   "WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                regi_allapot_lehivasa.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                regi_allapot_lehivasa.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];

                SqlDataReader regi_allapot_lehivasa_read = regi_allapot_lehivasa.ExecuteReader();

                int regi_allapot=0;
                if (regi_allapot_lehivasa_read.Read())
                {
                  regi_allapot = regi_allapot_lehivasa_read.GetInt32(0);
                }
                    //aktivitás idejének mentése, insert into Esemeny_valtozasokba
                    SqlCommand naplo_tabla = new SqlCommand("INSERT INTO Esemeny_valtozasok(esemenyID,szemelyID,mikor,regi_allapot)" +
                           " VALUES (@esemenyID,@szemelyID,@mod_datuma,@regi_allapot)", objSqlConnection);

                    naplo_tabla.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                    naplo_tabla.Parameters.Add("@regi_allapot", SqlDbType.Int).Value = regi_allapot;
                    naplo_tabla.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    naplo_tabla.Parameters.Add("@mod_datuma", SqlDbType.DateTime).Value =DateTime.Now;
                   
                    naplo_tabla.ExecuteNonQuery();
                
                //  új ertekeles update az Esemeny_erdeklodesbe

                SqlCommand allapot_update = new SqlCommand("Update [Esemeny_erdeklodes]  " +
                "SET ertekeles=@ertekeles, mod_datuma=@datenow WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                allapot_update.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                allapot_update.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];
                allapot_update.Parameters.Add("@ertekeles", SqlDbType.Int).Value = Int32.Parse(ertekelesDL.SelectedValue);
                allapot_update.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;

                allapot_update.ExecuteNonQuery();

                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
            Response.Redirect("Esemeny_reszletei.aspx?esemenyID=" + Request.Params["esemenyID"]);
            //set_allapot();
        }

    }

    public void set_allapot()
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                //jelenlegi allapot lekérése
                SqlCommand jelen_allapot_set = new SqlCommand("SELECT mod_datuma, allapot " +
                    "FROM Esemeny_erdeklodes " +
                    "WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                jelen_allapot_set.Parameters.Add("@esemenyID", SqlDbType.Int).Value = esemenyID;
                jelen_allapot_set.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];

                SqlDataReader jelen_allapot_set_read = jelen_allapot_set.ExecuteReader();

                if (jelen_allapot_set_read.Read())
                {
                    DateTime mod_datuma = jelen_allapot_set_read.GetDateTime(0);
                    int allapot = jelen_allapot_set_read.GetInt32(1);
                    if (allapot >= 1 && allapot <= 3)
                    {
                        allapotDL.SelectedIndex = allapot - 1;
                    }
                    if (allapot == 4)
                    {
                        allapotDL.SelectedIndex = 1;
                    }
                    if (allapot == 0)
                    {
                        allapotDL.SelectedIndex = 2;
                    }
                }

                objSqlConnection.Close();
                //mentés allapot: 0-megtekintette,1-gondolkodik,2-megy, 3-nem megy, 4-részt vett 

            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }

    }




}



