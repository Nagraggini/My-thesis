using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["createCalendarStart"] = null;
        Session["createCalendarEnd"] = null;
        Session["szemelyID"] = null;
        Session["loginname"] = null;

        Response.Redirect("Login.aspx");   
    }
}