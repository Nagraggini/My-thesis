using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Globalization;
// using System.Web.HttpContext;

public partial class Esemeny_reszletei_stat : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["createCalendarStart"] = null;
        Session["createCalendarEnd"] = null;

        Session["esemenyID"] = Int32.Parse(Request.Params["esemenyID"]);
        esemnyletrehozoLB.Text = (string)Session["loginname"];
        esemeny_feltolt();
        statisztikak();       

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
         using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                //lejárt az esemény és ott volt

                SqlCommand allapot_csekk = new SqlCommand("SELECT allapot " +
                    "FROM Esemeny_erdeklodes, Esemeny_alap " +
                    "WHERE szemelyID=@szemelyID AND Esemeny_erdeklodes.esemenyID=@esemenyID " +
                    "AND Esemeny_erdeklodes.esemenyID=Esemeny_alap.esemenyID AND meddig<@datenow " +
                    "AND (allapot='2' OR allapot='4')", objSqlConnection);
                //allapot csekkolása, hogy mehet-e a 4-re modositas

                allapot_csekk.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;
                allapot_csekk.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                allapot_csekk.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                SqlDataReader allapot_csekk_read = allapot_csekk.ExecuteReader();
                                
                //ha ott volt és lejárt akkor állapot 4-re modósítás
                if (allapot_csekk_read.Read() && !allapot_csekk_read.IsDBNull(0))
                {//insert into allapot 4-re modositas
                    SqlCommand allapot_update = new SqlCommand("Update [Esemeny_erdeklodes]  " +
                    "SET allapot='4' WHERE esemenyID=@esemenyID AND szemelyID=@szemelyID ", objSqlConnection);

                    allapot_update.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    allapot_update.Parameters.Add("@esemenyID", SqlDbType.Int).Value = (int)Session["esemenyID"];

                    allapot_update.ExecuteNonQuery();
                    allapotDL.Visible = false;
                    allapotmentBT.Visible = false;
                    //részt vett mutatása
                    allapot_ott_volt.Visible = true;
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
                    {//Nem jelentkezett rá 
                        allapotDL.Visible = false;
                        allapotmentBT.Visible = false;
                        allapotKIIR.Visible = false;                       
                    }
                }

                objSqlConnection.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }        
        }

         if (!Page.IsPostBack) //mert a button clikk előtt fut le a page load
             set_allapot();

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

                jelen_allapot_set.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];
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


    public string getStringUres(SqlDataReader Esemy_alap, int index)
    {
        if (Esemy_alap.IsDBNull(index))
        {
            return "";
        }
        return Esemy_alap.GetValue(index).ToString();
    }

    public List<string> esemenyek = new List<string>();
    public void esemeny_feltolt()
    {

        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();
                SqlCommand Esemny_alap = new SqlCommand("SELECT Felhasznalok.felh_nev, esemeny_nev," +
                    " esemeny_leiras, mettol, meddig, Esemeny_hely.orszag , Esemeny_hely.varos, Esemeny_hely.utca," +
                    " Esemeny_hely.hazszam, Esemeny_hely.iranyitoszam, Tipus_altipus.tipus, " +
                    "Tipus_altipus.altipus " +
                    " FROM Esemeny_alap, Esemeny_hely,Felhasznalok,Tipus_altipus" + //,Esemeny_erdeklodes" +
                    " WHERE Esemeny_alap.esemeny_hely=Esemeny_hely.helyID AND " +
                    "Esemeny_alap.esemeny_letrehozo=Felhasznalok.szemelyID AND " +
                    "Esemeny_alap.tipusID=Tipus_altipus.tipusID AND " +
                    //"Esemeny_alap.esemenyID=Esemeny_erdeklodes.esemenyID AND " +
                    //"Felhasznalok.szemelyID=Esemeny_erdeklodes.szemelyID AND " +
                    "Esemeny_alap.esemenyID=@esemenyID", objSqlConnection);

                Esemny_alap.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                SqlDataReader Esemny_alap_dr = Esemny_alap.ExecuteReader();

                while (Esemny_alap_dr.Read())
                {
                    string esemenyReszletei = "";
                    for (int i = 0; i <= 11; ++i)
                    {
                        esemenyReszletei += getStringUres(Esemny_alap_dr, i) + "\n";
                    }
                    esemenyek.Add(esemenyReszletei);
                }//két adat!!!!!!!!!
                Esemny_alap_dr.Close();

                SqlCommand hanyan_mennek = new SqlCommand("SELECT COUNT(*) " +
                   " FROM Esemeny_erdeklodes" +
                   " WHERE esemenyID=@esemenyID AND (allapot='2' OR allapot='4')", objSqlConnection);

                hanyan_mennek.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                SqlDataReader Hanyan = hanyan_mennek.ExecuteReader();

                //ertekeles atlag
                SqlCommand ertekeles_avg = new SqlCommand("SELECT AVG(ertekeles) "+
                        "FROM Esemeny_erdeklodes "+
                        "WHERE esemenyID=@esemenyID ", objSqlConnection);

                ertekeles_avg.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                SqlDataReader ertekeles = ertekeles_avg.ExecuteReader();


                string[] reszletek = new string[12];
                for (int i = 0; i < esemenyek.Count; i++)
                {
                    //Esemny_reszletei: 0letrehozó,1esemneve, 2leíras, 3mettol, 4meddig,5ország, 6város, 
                    //7utca, 8házszám, 9iranyitoszam, 10tipu, 11altipus, !!12hanyan mennek
                    //Adatok beállítása
                    reszletek = esemenyek[i].Split(new string[] { "\n" }, StringSplitOptions.None);
                }
               

                EsemenyneveLB.Text = "" + reszletek[1]; //kötelező
                //<br />
                if (!reszletek[2].Equals(""))
                {//van benne vmi
                    LeirasLB.Visible = true;
                    LeirasLB.Text = "" + reszletek[2];
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
               
                if (reszletek[5].Equals("") && reszletek[6].Equals("") && reszletek[7].Equals("") &&
                        reszletek[8].Equals("") && reszletek[9].Equals(""))
                {
                    orszagLB.Visible = true;
                    orszagLB.Text = Resources.String.lbEsemnyreszlstat_ert; //"Nincs megadva helyszín! ";
                }
                else
                {
                    if (!reszletek[5].Equals("") ||!reszletek[6].Equals("") ||!reszletek[7].Equals("")||!reszletek[8].Equals("0")
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

                //kiirja az ertekelest
                if (ertekeles.Read() && !ertekeles.IsDBNull(0))
                {
                    //ertekeles.Read();
                    ertekeles_avg_LB.Text = ertekeles.GetInt32(0).ToString();
                    ertekeles.Close();
                }
                else
                {
                    ertekeles_avg_KIIR.Visible = false;
                    ertekeles_avg_LB.Visible = false;
                }
                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }               

    }//metódus záró

    public void statisztikak()
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT kep FROM Esemeny_alap" +
                    " WHERE esemenyID=@esemenyID", objSqlConnection);

                command.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                SqlDataReader read = command.ExecuteReader();
                read.Read();
                if (!read.IsDBNull(0))
                {
                    ImagePreview.ImageUrl = "~/ImgHandler.ashx?esemenyID=" + Request.Params["esemenyID"] + "";
                }
                read.Close();
                objSqlConnection.Close();


            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }

        Series series_nem = new Series();
        series_nem.ChartType = SeriesChartType.Column;
        
        this.Chart_nem.ChartAreas[0].AxisY.Interval = 1;
        this.Chart_nem.ChartAreas[0].AxisX.Interval = 1;
        this.Chart_nem.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5;
      

        Series series_kor = new Series();
        series_kor.ChartType = SeriesChartType.Column;
        //x tengely név beállítás..
        
        this.Chart_kor.ChartAreas[0].AxisX.Minimum = 0;
        this.Chart_kor.ChartAreas[0].AxisY.Interval = 1;
        this.Chart_kor.ChartAreas[0].AxisX.Interval = 5;
        this.Chart_kor.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5;


        Series series_megt = new Series();
        series_megt.ChartType = SeriesChartType.Column;
        //x tengely név beállítás..
       
        this.Chart_megt.ChartAreas[0].AxisX.Minimum = 0;
        this.Chart_megt.ChartAreas[0].AxisX.Maximum = 24;
        this.Chart_megt.ChartAreas[0].AxisY.Interval = 1;
        this.Chart_megt.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5;
        this.Chart_megt.ChartAreas[0].AxisX.Interval = 1;


        Series series_megt2 = new Series();
        series_megt2.ChartType = SeriesChartType.Column;
        //x tengely név beállítás..
      
        this.Chart_megt2.ChartAreas[0].AxisX.Minimum = 0;
        this.Chart_megt2.ChartAreas[0].AxisX.Maximum = 24;
        this.Chart_megt2.ChartAreas[0].AxisY.Interval = 1;
        this.Chart_megt2.ChartAreas[0].AxisY.MajorGrid.Interval = 0.5;
        this.Chart_megt2.ChartAreas[0].AxisX.Interval = 1;
        
        //angol vagy magyar statisztika sávok
        if (Session["lang"] == "en-US")
        { //angol 
            this.Chart_nem.ChartAreas[0].AxisX.Title = "female, male";
            this.Chart_nem.ChartAreas[0].AxisX.CustomLabels.Add(-0.5, 0.5, "female");
            this.Chart_nem.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, "male");
            this.Chart_nem.ChartAreas[0].AxisY.Title = "quantity";

            this.Chart_kor.ChartAreas[0].AxisX.Title = "count";
            this.Chart_kor.ChartAreas[0].AxisY.Title = "quantity";

            this.Chart_megt.ChartAreas[0].AxisX.Title = "time ";
            this.Chart_megt.ChartAreas[0].AxisY.Title = "activity number";

            this.Chart_megt2.ChartAreas[0].AxisX.Title = " time ";
            this.Chart_megt2.ChartAreas[0].AxisY.Title = "I'll be there marking";
        }
        else
        {//magyar
            //x tengely név beállítás..
            this.Chart_nem.ChartAreas[0].AxisX.Title = "nő, férfi";
            this.Chart_nem.ChartAreas[0].AxisX.CustomLabels.Add(-0.5, 0.5, "nő");
            this.Chart_nem.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, "férfi");
            this.Chart_nem.ChartAreas[0].AxisY.Title = "darab szám";

            this.Chart_kor.ChartAreas[0].AxisX.Title = "kor";
            this.Chart_kor.ChartAreas[0].AxisY.Title = "darab szám";

            this.Chart_megt.ChartAreas[0].AxisX.Title = "idő ";
            this.Chart_megt.ChartAreas[0].AxisY.Title = "aktivitás szám";

            this.Chart_megt2.ChartAreas[0].AxisX.Title = " idő ";
            this.Chart_megt2.ChartAreas[0].AxisY.Title = "ott leszek jelölése";
        }


        List<int> nemek_aranya = new List<int>();
        for (int i = 0; i < 2; i++)
        {
            nemek_aranya.Add(0);
        }
        List<int> kor_count = new List<int>();
        for (int i = 0; i <= 100; i++)
        {
            kor_count.Add(0);
        }
        List<int> megtekintes_avg = new List<int>();
        for (int i = 0; i <= 24; i++)
        {
            megtekintes_avg.Add(0);
        }
        List<int> megtekintes_avg2 = new List<int>();
        for (int i = 0; i <= 24; i++)
        {
            megtekintes_avg2.Add(0);
        }
        bool vanadat = false;
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {//megy-e valaki egyáltalán
                objSqlConnection.Open();
                SqlCommand valaki = new SqlCommand("SELECT szemelyID "+
                   "FROM Esemeny_erdeklodes "+
                    "WHERE esemenyID=@esemenyID AND (allapot='2' OR allapot='4')", objSqlConnection);

                valaki.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                SqlDataReader read32 = valaki.ExecuteReader();
                if (read32.HasRows)
                {
                    vanadat = true;
                    Table1.Visible = true;
                }
                read32.Close();
                if (vanadat)
                {

                    //nemek aránya
                    SqlCommand nemek = new SqlCommand("SELECT COUNT(Felhasznalok.szemely_nem) AS neme_db , Felhasznalok.szemely_nem" +
                    " FROM Felhasznalok, Esemeny_erdeklodes" +
                    " WHERE Felhasznalok.szemelyID=Esemeny_erdeklodes.szemelyID AND " +
                    " Esemeny_erdeklodes.esemenyID=@esemenyID AND (allapot='2' OR allapot='4') " +
                    " GROUP BY Felhasznalok.szemely_nem ", objSqlConnection);

                    nemek.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                    SqlDataReader read_nem = nemek.ExecuteReader();
                    //nő db az a első

                    while (read_nem.Read())
                    {
                        if (!read_nem.GetValue(1).Equals(DBNull.Value))
                        {
                            nemek_aranya[read_nem.GetBoolean(1) ? 1 : 0] = read_nem.GetInt32(0);
                        }
                    }
                    read_nem.Close();

                    //kor aránya
                    SqlCommand korok = new SqlCommand(" SELECT COUNT(szemely_szulev), szemely_szulev " +
                            " FROM Felhasznalok, Esemeny_erdeklodes" +
                            " WHERE esemenyID=@esemenyID AND " +
                            " Felhasznalok.szemelyID=Esemeny_erdeklodes.szemelyID AND (allapot='2' OR allapot='4')" +
                            " GROUP BY szemely_szulev", objSqlConnection);

                    korok.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                    SqlDataReader read_kor = korok.ExecuteReader();

                    while (read_kor.Read())
                    {
                        if (!read_kor.GetValue(1).Equals(DBNull.Value))
                        {
                            //kor_count.Add(); //SORONKÉNT ÍRJA KI!!!!!!!!!!!!!!!!!!!  .
                            kor_count[DateTime.Now.Year - read_kor.GetInt32(1)] = read_kor.GetInt32(0);
                        }
                    }
                    read_kor.Close();

                    //aktivitas
                    SqlCommand aktivitas = new SqlCommand(" SELECT COUNT( DATEPART(hh,mikor))," +
                        " DATEPART(hh,mikor)" +
                        " FROM Esemeny_valtozasok" +
                        " WHERE esemenyID=@esemenyID" +
                        " GROUP BY  DATEPART(hh,mikor)", objSqlConnection);

                    aktivitas.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                    SqlDataReader read_megt = aktivitas.ExecuteReader();

                    while (read_megt.Read())
                    {
                        if (!read_megt.GetValue(1).Equals(DBNull.Value))
                        {
                            //megtekintes_avg.Add(read_megt.GetInt32(0)); //SORONKÉNT ÍRJA KI!!!!!!!!!!!!!!!!!!!  
                            megtekintes_avg[read_megt.GetInt32(1)] = read_megt.GetInt32(0);
                        }
                    }
                    read_megt.Close();

                    //ott leszek jelolese
                    SqlCommand ott_leszek_jelolese = new SqlCommand(" SELECT COUNT( DATEPART(hh,mod_datuma))," +
                        " DATEPART(hh,mod_datuma)" +
                        " FROM Esemeny_erdeklodes" +
                        " WHERE esemenyID=@esemenyID AND (allapot='2' OR allapot='4') " +
                        " GROUP BY  DATEPART(hh,mod_datuma)", objSqlConnection);

                    ott_leszek_jelolese.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                    SqlDataReader read_megt2 = ott_leszek_jelolese.ExecuteReader();

                    while (read_megt2.Read())
                    {
                        if (!read_megt2.GetValue(1).Equals(DBNull.Value))
                        {
                            megtekintes_avg2[read_megt2.GetInt32(1)] = read_megt2.GetInt32(0);
                        }
                    }
                    read_megt2.Close();
                }

                objSqlConnection.Close();
            }
            catch (Exception ex)
            { Response.Write("Error : " + ex.Message.ToString()); }
        }
        if (vanadat)
        {


            for (int i = 0; i < 2; ++i)
                series_nem.Points.Add(new DataPoint(i, nemek_aranya[i]));
            series_nem.Points[0].Color = System.Drawing.Color.Orange;
            this.Chart_nem.Series.Add(series_nem);
        
            for (int i = 0; i <= 100; ++i)
                series_kor.Points.Add(new DataPoint(i, kor_count[i]));
            int min_kor = 0;
            while (min_kor <= 100 && kor_count[min_kor] == 0)
            {
                min_kor++;
            }

            int max_kor = 100;
            while (max_kor >= 0 && kor_count[max_kor] == 0)
            {
                max_kor--;
            }
            if (min_kor <= 100)
            {
                this.Chart_kor.ChartAreas[0].AxisX.Minimum = min_kor - 1;
            }
            if (max_kor >= 0)
            {
                this.Chart_kor.ChartAreas[0].AxisX.Maximum = max_kor + 1;
            }
            this.Chart_kor.Series.Add(series_kor);

            for (int i = 0; i <= 24; ++i)
                series_megt.Points.Add(new DataPoint(i, megtekintes_avg[i]));

            this.Chart_megt.Series.Add(series_megt);

            for (int i = 0; i <= 24; ++i)
                series_megt2.Points.Add(new DataPoint(i, megtekintes_avg2[i]));

            this.Chart_megt2.Series.Add(series_megt2);
        }
    }
    //Esemény törlés
    protected void Del_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI; MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {//login név és eseményID van a sessionben
                objSqlConnection.Open();

                SqlCommand deleteCMD = new SqlCommand(" DELETE FROM Esemeny_valtozasok" +
                    " WHERE esemenyID=@esemenyID ;"+
                    " DELETE FROM Esemeny_erdeklodes" +
                    " WHERE esemenyID=@esemenyID ;" +
                    " DELETE FROM Esemeny_alap" +
                    " WHERE esemenyID=@esemenyID ;", objSqlConnection);

                deleteCMD.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                deleteCMD.ExecuteNonQuery();
                objSqlConnection.Close();

            }
            catch (Exception ex)
            {

                Response.Write("Error : " + ex.Message.ToString());
            }
        }
        Response.Redirect("Fooldal.aspx");
    }


    protected void Save_Click(object sender, EventArgs e)
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI;MultipleActiveResultSets = true";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {//login név és eseményID van a sessionben
                objSqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT allapot " +
                        "FROM Esemeny_erdeklodes " +
                        "WHERE szemelyID=@szemelyID "+
                        "AND esemenyID=@esemenyID ", objSqlConnection);

                command.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                command.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];

                SqlDataReader rd = command.ExecuteReader();
                bool hasRows = rd.HasRows;
                rd.Close();

                if (!hasRows)
                {//nincs ilyen

                    // Nem megy v megy

                    SqlCommand command3 = new SqlCommand("INSERT INTO Esemeny_erdeklodes(esemenyID, szemelyID, allapot, mod_datuma)" +
                    " VALUES (@esemenyID,@szemelyID,'" + allapotDL.SelectedValue + "', @datenow);", objSqlConnection);

                    command3.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    command3.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];
                    command3.Parameters.Add("@datenow", SqlDbType.DateTime).Value = DateTime.Now;

                     command3.ExecuteNonQuery();
                }
                else
                {
                    //már van ilyen
                    SqlCommand command2 = new SqlCommand("UPDATE Esemeny_erdeklodes SET allapot=@allapot "+
                        " WHERE Esemeny_erdeklodes.esemenyID=@esemenyID AND " +
                        " Esemeny_erdeklodes.szemelyID=@szemelyID", objSqlConnection);

                    command2.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    command2.Parameters.Add("@esemenyID", SqlDbType.Int).Value = Request.Params["esemenyID"];
                    command2.Parameters.Add("@allapot", SqlDbType.Int).Value = Int32.Parse(allapotDL.SelectedValue);

                    command2.ExecuteNonQuery();


                }


                objSqlConnection.Close();

              
                //mentés allapot: 0-megtekintette,1-gondolkodik,2-megy, 3-nem megy, 4-részt vett 

            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
            esemeny_feltolt();
        }
    }


}