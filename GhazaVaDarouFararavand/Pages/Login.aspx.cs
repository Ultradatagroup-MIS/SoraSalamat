using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GhazaVaDarouFararavand.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVorod_Click(object sender, EventArgs e)
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection cn = new SqlConnection(conString);
            SqlParameter p = null;
            DataTable dt = null;
            string strSQL = String.Empty;
            string PasswordHash = String.Empty;

            strSQL = "Select distinct CodeFard as  ccAfrad,RamzVorod as  PasswordSalt,RamzVorod as PasswordHash,'' as SessionID From   tblGl_Moshakhasatfardi as a Where a.Namevorod=@UserName ";

            p = new SqlParameter("UserName", txtUsername.Text);
            p.Value = txtUsername.Text;
            cm.Parameters.Add(p);
            cm.CommandText = strSQL;
            cm.Connection = cn;
            da.SelectCommand = cm;
            dt = new DataTable();
            da.Fill(dt);
           
            if ((dt.Rows.Count == 0))
            {
                string script = "alert(\"نام کاربری در سیستم موجود ثبت نشده\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }
            else
            {
               
                //System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
                //byte[] b = System.Text.Encoding.Default.GetBytes(( txtPassword.Text + dt.Rows[0]["PasswordSalt"]));
                //PasswordHash = Convert.ToBase64String(md5.ComputeHash(b));
                //var UserName = txtUsername.Text;
                PasswordHash= txtPassword.Text;


                if (PasswordHash.ToString() == dt.Rows[0]["PasswordHash"].ToString())
                // Cgvl4a1rvXwRc6Q5l2AhRA ==  "Cgvl4a1rvXwRc6Q5l2AhRA=="
                {

                    Session["UserName"] = txtUsername.Text;
                    Server.Transfer("~/Pages/Main.aspx");
                }
                else
                {
                    string script = "alert(\"کلمه عبور در سیستم موجود ثبت نشده\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);

                }
            }
        }
    }
}