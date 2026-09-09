using GhazaVaDarouFararavand.Models.PakhshTableAdapters;
using GhazaVaDarouFararavand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Configuration;
using GhazaVaDarouFararavand.StatisticsService;
using System.IO;

namespace GhazaVaDarouFararavand
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public void Ebtal()
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            DataTable dtSelect = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                //  SqlCommand sqlComm = new SqlCommand("select ccKardexSatrUUID,* from Sales.KardexUUID  where ccKardexSatrUUID in (select ccKardexSatrUUID from Sales.KardexUUID where Status=2 group by ccKardexSatrUUID having count(ccKardexSatrUUID)=2) order by Sales.KardexUUID.ccKardexSatrUUID  "
                // , conn);
                // SqlCommand sqlComm = new SqlCommand("select * from AmarnamehEventID", conn);

                //daSelect.SelectCommand = sqlComm;
                //daSelect.SelectCommand.CommandTimeout = 9999999;
                //daSelect.Fill(dtSelect);







                SqlCommand cm = new SqlCommand();
                SqlConnection cn = new SqlConnection(conString);
                SqlParameter p = null;
                string strSQL = String.Empty;
                string PasswordHash = String.Empty;
                SqlDataAdapter daSelect = new SqlDataAdapter();
                strSQL = "select * from TTACConfig";
                cm.CommandText = strSQL;
                cm.Connection = cn;
                daSelect.SelectCommand = cm;
                dtSelect = new DataTable();
                daSelect.Fill(dtSelect);

                //if ((dtSelect.Rows.Count == 0))
                //{
                //    string script = "alert(\"تنظیمات وب سرویس انجام نشده\");";
                //    ScriptManager.RegisterStartupScript(this, GetType(),
                //                                  "ServerControlScript", script, true);
                //}
                //else
                //{
                CallbackService cs = new CallbackService();
                StatisticsServiceClient client = new StatisticsServiceClient();
                Authentication au = new Authentication();
                EpcisEvent ee = new EpcisEvent();
                ObjectEvent ce = new ObjectEvent();

                SqlCommand cmResult = new SqlCommand();
                au.UserName = dtSelect.Rows[0]["TTAC_UserName"].ToString();
                au.Password = dtSelect.Rows[0]["TTAC_Password"].ToString();

                SqlDataAdapter daResult = new SqlDataAdapter();
                SqlConnection cnResult = new SqlConnection(conString);
                DataTable dtcnResult = new DataTable();
                string strResult = String.Empty;
                strResult = "select * from Sales.KardexUUID with(nolock) where ccKardexSatrUUID in (select ccKardexSatrUUID from Sales.KardexUUID  with(nolock) where Status=2 group by ccKardexSatrUUID having count(ccKardexSatrUUID)=2) order by Sales.KardexUUID.ccKardexSatrUUID  ";
                cmResult.CommandText = strResult;
                cmResult.Connection = cnResult;
                daResult.SelectCommand = cmResult;
                dtcnResult = new DataTable();
                daResult.Fill(dtcnResult);
                // callbackService.Url = dtSelect.Rows[0]["TTAC_WebsSrviceUrl"].ToString();

                for (int i = 0; i < dtcnResult.Rows.Count; i += 2)
                {


                    // var UUID = dtcnResult.Rows[i]["uuid"].ToString();
                    Guid guid = new Guid(dtcnResult.Rows[i]["uuid"].ToString());



                  //  CallbackService callbackService = new CallbackService();
                   // callbackService.Url = "http://5.202.191.115:8080/RestService2/RestServiceImpl.svc/CallResponse";
                  //  client.CancelCapture(guid, au, callbackService);


                    //client.CancelCaptureWithResponse(guid, au);
            //       var a= client.CancelCaptureWithResponse(guid, au);
                    var result = client.GetCaptureStatusWithResponse(guid, au);
                    //string Payam = "";
                    //if (result.Message != null)
                    //{
                    //    Payam = result.Message.ToString();
                    //}



                    //if (a.IsCanceled == true)
                    //{
                        DataTable dtInsert = new DataTable();
                        SqlConnection cnInsert = new SqlConnection(conString);
                        SqlDataAdapter daInsert = new SqlDataAdapter();

                        string sql2 = "UPDATE Sales.KardexUUID SET Comment = @Comment,status=100 WHERE uuid=@id";
                        SqlCommand myCommand2 = new SqlCommand(sql2, conn);
                        myCommand2.Parameters.AddWithValue("@Comment", "");
                        myCommand2.Parameters.AddWithValue("@id", guid);

                        daInsert.SelectCommand = myCommand2;
                        daInsert.SelectCommand.CommandTimeout = 9999999;
                        daInsert.Fill(dtInsert);


                   // }

                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                try
                {
                    Ebtal();




                }
                catch (Exception)
                {

                    Ebtal();
                }






            }
        }
    }

}
//    }
//}