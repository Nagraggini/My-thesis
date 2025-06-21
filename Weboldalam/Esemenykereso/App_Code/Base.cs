using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Esemenykereso;
using System.Globalization;

/// <summary>
/// Summary description for Base
/// </summary>
public class BasePage : System.Web.UI.Page
{   //Ellenőrzés, hogy van-e valaki bejelentkezve, ha nem visszadob a login-ra
    //TODO: Minden oldalt ebből kell majd leszármaztatni!
    public BasePage()
    {
       
    }

    protected override void OnLoad(EventArgs e) {
        if (Session[Const.LOGIN_NAME] == null)
        {//nincs bejelentkezve
            Response.Redirect("Login.aspx");
        }

        base.OnLoad(e);
    }

    //Nyelv beállító
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

}