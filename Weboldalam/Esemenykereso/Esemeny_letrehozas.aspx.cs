using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using AjaxControlToolkit;

public partial class _Default : BasePage
{
    public string con;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["SelectedTypeIndex"] = TipusDL.SelectedValue;

        if (!Page.IsPostBack)
        {
            Session["createCalendarStart"] = null;
            Session["createCalendarEnd"] = null;
        }
       
        if (Page.IsPostBack)
            if (Session["createCalendarStart"] != null)
            {
                MettolCD.SelectedDate = (DateTime)Session["createCalendarStart"];
                MettolCD.VisibleDate = MettolCD.SelectedDate;
            }
            else
            {
                MettolCD.SelectedDate = DateTime.Now;
                MettolCD.VisibleDate = DateTime.Now;
            }

        if (Page.IsPostBack)
            if (Session["createCalendarEnd"] != null)
            {
                MeddigCD.SelectedDate = (DateTime)Session["createCalendarEnd"];
                MeddigCD.VisibleDate = MeddigCD.SelectedDate;
            }
            else
            {
                MeddigCD.SelectedDate = DateTime.Now;
                MeddigCD.VisibleDate = DateTime.Now;
            }
                                       
            DropDownList1_Load(sender, e);               
        
    }
     
    //A tipusok kiírása
    protected void DropDownList1_Load(object sender, EventArgs e)
    {
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
            using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    objSqlConnection.Open();
                    //Csak a különböző típusok jelenjenek meg
                    SqlCommand command = new SqlCommand("SELECT DISTINCT tipus " +
                    "FROM Tipus_altipus", objSqlConnection);

                    SqlDataReader ddltipus;
                    ddltipus = command.ExecuteReader();

                    TipusDL.DataSource = ddltipus;
                    TipusDL.DataValueField = "tipus";
                    TipusDL.DataTextField = "tipus";
                    TipusDL.DataBind();

                    objSqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Response.Write("Error : " + ex.Message.ToString());
                }
            }
           
        }
    }
    //Az altípusok kiírása az tipushoz képest, de nem váltódik ki ez az esemény, ezért AJAX van
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SelectedTypeIndex"] = TipusDL.SelectedIndex;
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
        {
            try
            {
                objSqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT altipus " +
                "FROM Tipus_altipus WHERE tipus='" + TipusDL.SelectedItem.Value + "' ", objSqlConnection);

                SqlDataReader ddlaltipus;
                ddlaltipus = command.ExecuteReader();

                AltipusDL.DataSource = ddlaltipus;
                AltipusDL.DataValueField = "altipus";
                AltipusDL.DataTextField = "altipus";
                AltipusDL.DataBind();

                objSqlConnection.Close();

            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.Message.ToString());
            }
        }
        // DropDownList2_Load(sender, e);       

    }

    //altípusok kiírása és altípus módosítása a típushoz képest
    protected void DropDownList2_Load(object sender, EventArgs e)
    {
        
        {

            string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
            using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    objSqlConnection.Open();

                    SqlCommand command = new SqlCommand("SELECT altipus " +
                    "FROM Tipus_altipus WHERE tipus='" + TipusDL.SelectedItem.Value + "' ", objSqlConnection);

                    SqlDataReader ddlaltipus;
                    ddlaltipus = command.ExecuteReader();

                    AltipusDL.DataSource = ddlaltipus;
                    AltipusDL.DataValueField = "altipus";
                    AltipusDL.DataTextField = "altipus";
                    AltipusDL.DataBind();

                    objSqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Response.Write("Error : " + ex.Message.ToString());
                }
            } // using bezáró
        }
        if (Session["SelectedSubtypeIndex"] != null)
            AltipusDL.SelectedIndex = (int)Session["SelectedSubtypeIndex"];
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SelectedSubtypeIndex"] = AltipusDL.SelectedIndex;
    }
    //Feltölti az adatbázisba az új eseményt
    protected void Esemnyletrehoz_Click(object sender, EventArgs e)
    {
        if (Session["createCalendarStart"] != null && Session["createCalendarEnd"] != null
            && (DateTime)Session["createCalendarStart"] <= (DateTime)Session["createCalendarEnd"]
            && (DateTime)Session["createCalendarEnd"]>=DateTime.Now)
        { //valid dátum és nem lehet üres

            DateTime calendarStartDate = (DateTime)Session["createCalendarStart"];
            DateTime calendarEndDate = (DateTime)Session["createCalendarEnd"];

            int ujesemeny_id = 0;
            int ujesemenyhely_id = 0;
            string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
            using (SqlConnection objSqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    objSqlConnection.Open();
                    //TODO: string összarakásnál
                    SqlCommand command3 = new SqlCommand("SELECT tipusID FROM Tipus_altipus WHERE altipus='" + AltipusDL.SelectedItem + "'", objSqlConnection);
                    SqlDataReader tipus = command3.ExecuteReader();
                    tipus.Read(); //kikell olvasni
                    int tipusID = tipus.GetInt32(0);
                    tipus.Close();
                                       
                    string[] words = ((DateTime)Session["createCalendarStart"]).ToString().Split(' ');
                    string[] words2 = ((DateTime)Session["createCalendarEnd"]).ToString().Split(' ');

                    SqlCommand command2 = new SqlCommand("INSERT INTO Esemeny_hely" +
                     "(orszag,varos, utca, hazszam,iranyitoszam)" +
                         " OUTPUT INSERTED.helyID " +
                     " VALUES (@Orszag,@Varos,@Utca,@Hazszam,@Iranyitoszam);", objSqlConnection);
                    
                    command2.Parameters.Add("@Orszag", SqlDbType.VarChar).Value = OrszagTB.Text;
                    command2.Parameters.Add("@Varos", SqlDbType.VarChar).Value = VarosTB.Text;
                 
                    if (UtcaTB.Text.Length > 0)
                        command2.Parameters.Add("@Utca", SqlDbType.Int).Value = Int32.Parse(UtcaTB.Text);
                    else
                        command2.Parameters.Add("@Utca", SqlDbType.Int).Value = DBNull.Value;
                    
                    if (HazszamTB.Text.Length > 0)
                        command2.Parameters.Add("@Hazszam", SqlDbType.Int).Value = Int32.Parse(HazszamTB.Text);
                    else
                        command2.Parameters.Add("@Hazszam", SqlDbType.Int).Value = DBNull.Value;

                    if (IranyitoszamTB.Text.Length > 0)
                        command2.Parameters.Add("@Iranyitoszam", SqlDbType.Int).Value = Int32.Parse(IranyitoszamTB.Text);
                    else
                        command2.Parameters.Add("@Iranyitoszam", SqlDbType.Int).Value = DBNull.Value;



                    ujesemenyhely_id = (int)command2.ExecuteScalar();
                    //SQL-ek formázása, ctrl+k+f
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO Esemeny_alap " +
                        "(esemeny_letrehozo, esemeny_nev,esemeny_leiras,tipusID,mettol,meddig,esemeny_hely) " +
                        "OUTPUT INSERTED.esemenyID " +
                        "VALUES (@szemelyID,@Esemnyneve,@Leiras,@tipusID,@startdate,@enddate,@ujesemenyhely);",
                        objSqlConnection);

                    command.Parameters.Add("@szemelyID", SqlDbType.Int).Value = (int)Session["szemelyID"];
                    command.Parameters.Add("@Esemnyneve", SqlDbType.VarChar).Value = EsemnyneveTB.Text;
                    command.Parameters.Add("@Leiras", SqlDbType.VarChar).Value = LeirasTA.InnerText;
                    command.Parameters.Add("@startdate", SqlDbType.DateTime).Value = calendarStartDate;
                    command.Parameters.Add("@enddate", SqlDbType.DateTime).Value = calendarEndDate;
                    command.Parameters.Add("@ujesemenyhely", SqlDbType.Int).Value = ujesemenyhely_id;
                    command.Parameters.Add("@tipusID", SqlDbType.Int).Value = tipusID;


                    ujesemeny_id = (int)command.ExecuteScalar();

                    //Képe feltöltése
                    Kepfeltolt(ujesemeny_id);

                    objSqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Response.Write("Error : " + ex.Message.ToString());
                }

                ErtesitesLB.Text = Resources.String.lbEsemnylet_ert; //"Esemény létrehozva!";

                Response.Redirect("Esemeny_reszletei_stat.aspx?esemenyID=" + ujesemeny_id);
            }
        }
        else
        {
            ErtesitesLB.Text = Resources.String.lbEsemnylet_ert2; //"Dátum mezők kitöltése!";
        }
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Session["createCalendarStart"] = MettolCD.SelectedDate;
        MettolCD.VisibleDate = MettolCD.SelectedDate;
    }

    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        Session["createCalendarEnd"] = MeddigCD.SelectedDate;
        MeddigCD.VisibleDate = MeddigCD.SelectedDate;
    }

    //Kép konvertálása
    private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
    {
        byte[] Ret;
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, formatOfImage);
                Ret = ms.ToArray();
            }
        }
        catch (Exception) { throw; }
        return Ret;
    }

    //Upload image to database
    protected void Kepfeltolt(int ujesemenyID)
    {
        if (flImage.PostedFile.ContentLength == 0)
        {//nincs kép
            return;
        }

        System.Drawing.Image imag = System.Drawing.Image.FromStream(flImage.PostedFile.InputStream);
        System.Data.SqlClient.SqlConnection conn = null;
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb;Integrated Security=SSPI";
        using (conn = new SqlConnection(connectionString))
        {
            try
            {
                try
                {
                    // conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand insertCommand = new System.Data.SqlClient.SqlCommand("Update [Esemeny_alap] SET kep=@Pic" + " WHERE esemenyID='" + ujesemenyID + "'", conn);
                    insertCommand.Parameters.Add("Pic", SqlDbType.Image, 0).Value = ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
                    int queryResult = insertCommand.ExecuteNonQuery();
                    if (queryResult == 1)
                        lblRes.Text = Resources.String.lbImgup_ert;// "A kép feltöltés megtörtént!";
                }
                catch (Exception ex)
                {
                    lblRes.Text = "Error: " + ex.Message;
                }
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }

        }
    }
}