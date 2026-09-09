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
using System.Threading;

namespace GhazaVaDarouFararavand
{
    public partial class karshenas : System.Web.UI.Page
    {


        CallbackService cs = new CallbackService();
        StatisticsServiceClient client = new StatisticsServiceClient();
        Authentication au = new Authentication();
        EpcisEvent ee = new EpcisEvent();
        ObjectEvent ce = new ObjectEvent();
        CallbackService callBack = new CallbackService();




        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool isValid( string noe)
        {

            Boolean Valid = true;
            switch (noe)
            {
                case "btnSendUUID_Click":
                    if (String.IsNullOrEmpty(txtUUID.Text))
                    {
                        Valid = false;
                    }
                    break;
                case "btnSendccKardexSatr_Click":
                    if (String.IsNullOrEmpty(txtccKardexsatr.Text))
                    {
                        Valid = false;
                    }
                    break;
                case "btnUpdate_Click":
                    if (String.IsNullOrEmpty(txtUpdate.Text))
                    {
                        Valid = false;
                    }
                    break;
            }
            return Valid;

        }

        protected void btnSendUUID_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (isValid("btnSendUUID_Click"))
                {
                    ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                    if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                        throw new Exception("Fatal error: missing connecting string in web.config file");
                    var conString = mySetting.ConnectionString;
                    DataTable ds = new DataTable();


                   // var conString2 = mySetting2.ConnectionString;
                    SqlCommand cm = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlConnection cn = new SqlConnection(conString);
                    SqlParameter p = null;
                    DataTable dt = null;
                    string strSQL = String.Empty;
                    string PasswordHash = String.Empty;

                    strSQL = "select * from TTACConfig with(nolock)";
                    cm.CommandText = strSQL;
                    cm.Connection = cn;
                    da.SelectCommand = cm;
                    dt = new DataTable();
                    da.Fill(dt);

                    if ((dt.Rows.Count == 0))
                    {
                        string script = "alert(\"تنظیمات وب سرویس انجام نشده\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                                      "ServerControlScript", script, true);
                    }
                    else
                    {
                        using (SqlConnection conn = new SqlConnection(conString))
                        {
                            SqlCommand sqlComm = new SqlCommand("readUUID", conn);
                            sqlComm.Parameters.AddWithValue("@UUID", txtUUID.Text);
                            // sqlComm.Parameters.AddWithValue("@strGorohMoshtary", MoshtaryGoroh);
                            sqlComm.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter dattac = new SqlDataAdapter();
                            dattac.SelectCommand = sqlComm;
                            dattac.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                            dattac.Fill(ds);
                            // SaveWhere(MarkazPakhsh, Resid, Havaleh, KalaGoroh, TaminKonandeh, Brand);


                            if (ds.Rows.Count > 0)
                            {


                                for (int i = 0; i < ds.Rows.Count; i++)
                                {
                                    Destination d = new Destination();
                                    Ilmd oIlm = new Ilmd();
                                    QuantityElement oQuantityElement = new QuantityElement();
                                    ObjectEventExtension oEventExtension = new ObjectEventExtension();
                                    EpcisEvent ee = new EpcisEvent();
                                    BusinessLocation BL = new BusinessLocation();
                                    ObjectEvent oe = new ObjectEvent();
                                    DataSet dsTitrSatrTTAC = new DataSet();
                                    // DataTable dtAmarNamehVaset = new DataTable();
                                    // SqlDataAdapter daTitrSatrTTAC = new SqlDataAdapter();
                                    ReadPoint rp = new ReadPoint();
                                    //using (SqlConnection conn = new SqlConnection(conString))
                                    //{
                                    oe.EventTime = Convert.ToDateTime(ds.Rows[0]["EventTime"]);

                                    oe.Action = ds.Rows[0]["action"].ToString();
                                    oe.BizStep = ds.Rows[0]["BizStep"].ToString();
                                    oe.Disposition = ds.Rows[0]["Disposition"].ToString();
                                    // oe.EventID = dtTitrSatrTTAC.Rows[0]["EventID"].ToString();
                                    Guid EventId;
                                    Guid.TryParse(ds.Rows[0]["EventId"].ToString(), out EventId);
                                    // Guid.TryParse("617D64D8-02CA-4084-8B76-7E7EEBE006B0", out EventId);

                                    oe.EventID = EventId;
                                    rp.Id = ds.Rows[0]["ReadPointId"].ToString();

                                    if (oe.BizStep != "shipping")
                                    {
                                        BL.Id = ds.Rows[0]["bizLocationId"].ToString();

                                        ////Resid Az TaminKonandeh /Dar Resid Az Tamin konandeh Khat zir Niaz Ast
                                        oe.BizLocation = BL;
                                    }

                                    if (oe.BizStep == "shipping")
                                    {
                                        BL.Id = "";

                                    }
                                    d.Id = ds.Rows[0]["destinationListDestination"].ToString();
                                    d.Type = ds.Rows[0]["destinationListDestinationType"].ToString();
                                    oe.Extension = oEventExtension;
                                    if (oe.BizStep != "stock_taking" && oe.BizStep != "Cycle_counting")
                                    {

                                        oe.Extension.SourceList = new Source[] { new Source { Id = ds.Rows[0]["extensionSourceListSource"].ToString(), Type = "location" } };

                                        oe.Extension.DestinationList = new Destination[] { new Destination { Id = ds.Rows[0]["destinationListDestination"].ToString(), Type = "location" } };// dtTitr.Rows[0]["destinationListDestinationType"].ToString() } };
                                    }

                                    var quantityList = new List<QuantityElement>();
                                    quantityList.Add(new QuantityElement { IRC = ds.Rows[0]["QuantityElementilmdIRC"].ToString(), EpcClass = ds.Rows[0]["quantityElementepcClass"].ToString(), Quantity = ds.Rows[0]["quantityElementQuantity"].ToString(), ILMD = new Ilmd { LotNumber = ds.Rows[0]["QuantityElementIlmdlotNumber"].ToString(), MD = Convert.ToDateTime(ds.Rows[0]["QuantityElementIlmdMD"]), XD = Convert.ToDateTime(ds.Rows[0]["QuantityElementIlmdXD"]) }, UOM = ds.Rows[0]["quantityElementuom"].ToString() });

                                    oe.Extension.QuantityList = quantityList.ToArray();
                                    au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                                    au.Password = dt.Rows[0]["TTAC_Password"].ToString();
                                    client.Capture(new ObjectEvent[] { oe }, au, null);
                                }
                            }
                            else
                            {
                                string script = "alert(\"در این بازه رکوردی وجود ندارد\");";
                                ScriptManager.RegisterStartupScript(this, GetType(),
                                                              "ServerControlScript", script, true);
                            }
                        }
                    }
                }
                else
                {
                    string script1 = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script1, true);
                }

            }
            catch (Exception)
            {

                string script1 = "alert(\"خطا در تراکنش\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script1, true);
            }



        }

        protected void btnSendccKardexSatr_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                if (isValid("btnSendccKardexSatr_Click"))
                {

                }
                else
                {
                    string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }
            }
            catch (Exception)
            {

                string script1 = "alert(\"خطا در تراکنش\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script1, true);
            }



        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            try
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
                SqlConnection conn = new SqlConnection(conString);
                strSQL = "select * from TTACConfig with(nolock)";
                cm.CommandText = strSQL;
                cm.Connection = cn;
                da.SelectCommand = cm;
                dt = new DataTable();
                da.Fill(dt);

                if ((dt.Rows.Count == 0))
                {
                    string script = "alert(\"تنظیمات وب سرویس انجام نشده\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }
                else
                {

                    au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                    au.Password = dt.Rows[0]["TTAC_Password"].ToString();
                    if (isValid("btnUpdate_Click"))
                    {
                        Guid guid = new Guid(txtUpdate.Text);
                        //   Guid guid = new Guid("791fa5a1-f4e7-4c12-b916-980cf8ad01a2");

                        var result = client.GetCaptureStatusWithResponse(guid, au);


                        string Payam = "";
                        if (result.Message != null)
                        {
                            Payam = result.Message.ToString();
                        }

                        if (result.Successed == false)
                        {
                            conn.Open();
                            string sql2 = "Update sales.KardexUUID  set Status = @Status, Comment = @Comment where UUID=@UUID";
                            SqlCommand myCommand2 = new SqlCommand(sql2, conn);
                            myCommand2.Parameters.AddWithValue("@Status", 3);
                            myCommand2.Parameters.AddWithValue("@Comment", Payam);
                            myCommand2.Parameters.AddWithValue("@UUID", guid);
                            myCommand2.CommandTimeout = 9999999;
                            //  daUpdate.SelectCommand = myCommand2;
                            // da.SelectCommand.CommandTimeout = 9999999;
                            myCommand2.ExecuteNonQuery();
                            conn.Close();
                            //  daStatus.ttacForUpdateSuccessed(0, guid, 3, Payam);
                            //rx.UpdateDataToKardexUUID(Convert.ToInt32(dtKardex.Rows[i]["ccKardexUUID"].ToString()), Convert.ToInt32(dtKardex.Rows[i]["ccKardex"].ToString()), dtKardex.Rows[i]["UUID"].ToString(), 2, 3, result.StatusCode.ToString() + " : " + Payam, Convert.ToInt32(dtKardex.Rows[i]["CodeNoeForm"].ToString()));
                        }
                        if (result.Successed == true)
                        {
                            conn.Open();
                            string sql2 = "Update sales.KardexUUID  set  Status = @Status, Comment = @Comment where UUID=@UUID";
                            SqlCommand myCommand2 = new SqlCommand(sql2, conn);
                            myCommand2.Parameters.AddWithValue("@Status", 2);
                            myCommand2.Parameters.AddWithValue("@Comment", Payam);
                            myCommand2.Parameters.AddWithValue("@UUID", guid);
                            myCommand2.CommandTimeout = 9999999;
                            //    daUpdate.SelectCommand = myCommand2;
                            //  conn.Open();
                            myCommand2.ExecuteNonQuery();
                            conn.Close();
                            //  daStatus.ttacForUpdateSuccessed(0, guid, 2, Payam);
                            // rx.UpdateDataToKardexUUID(Convert.ToInt32(dtKardex.Rows[i]["ccKardexUUID"].ToString()), Convert.ToInt32(dtKardex.Rows[i]["ccKardex"].ToString()), dtKardex.Rows[i]["UUID"].ToString(), 2, 2, result.StatusCode.ToString() + " : " + Payam, Convert.ToInt32(dtKardex.Rows[i]["CodeNoeForm"].ToString()));
                        }
                    }
                    else
                    {
                        string script = "alert(\"خطا در بروزرسانی\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                                      "ServerControlScript", script, true);
                    }
                }
            }
            catch (Exception)
            {

                string script1 = "alert(\"خطا در تراکنش\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script1, true);
            }



        }


   
    }
}