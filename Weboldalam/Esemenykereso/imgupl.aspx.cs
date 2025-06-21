using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlClient.SqlConnection;
//using System.Drawing.Image;

public partial class namoona_imgupl : System.Web.UI.Page
{

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
  
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    //Upload image to database
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Drawing.Image imag = System.Drawing.Image.FromStream(flImage.PostedFile.InputStream);
        System.Data.SqlClient.SqlConnection conn = null;
        string connectionString = @"Data Source=localhost;Initial Catalog=Esemenydb2;Integrated Security=SSPI";
        using (conn = new SqlConnection(connectionString))
        {
            try
            {
                try
                {
                   // conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
                    conn.Open();
                    System.Data.SqlClient.SqlCommand insertCommand = new System.Data.SqlClient.SqlCommand("Update [Esemeny_alap] SET kep=@Pic" +" WHERE esemenyID='8'", conn);
                    insertCommand.Parameters.Add("Pic", SqlDbType.Image, 0).Value = ConvertImageToByteArray(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
                    int queryResult = insertCommand.ExecuteNonQuery();
                    if (queryResult == 1)
                        lblRes.Text = "A kép feltöltés megtörtént!";
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
