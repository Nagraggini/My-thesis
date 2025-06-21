using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Resources;
using System.Threading;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void hun_click(object sender, EventArgs e)
    {
        Session["lang"] = "hu-HU";
        Response.Redirect(Request.RawUrl);
    }

    protected void eng_click(object sender, EventArgs e)
    {
        Session["lang"] = "en-US";       
        Response.Redirect(Request.RawUrl);
    }

}
