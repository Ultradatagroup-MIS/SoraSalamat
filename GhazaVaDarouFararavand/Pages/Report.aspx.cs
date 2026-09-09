using GhazaVaDarouFararavand.Models;
using GhazaVaDarouFararavand.Models.PakhshTableAdapters;
using GhazaVaDarouFararavand.StatisticsService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace GhazaVaDarouFararavand.Pages
{
    public partial class Report : System.Web.UI.Page
    {

        string txtAzTarikhErsalFromQueryString;
        string txtTaTarikhErsalFromQueryString;
        string txtAzTarikhFormFromQueryString;
        string txtTaTarikhFormFromQueryString;
        protected void Page_Load(object sender, EventArgs e)
        {



            txtAzTarikhErsalFromQueryString = Request.QueryString["txtAzTarikhErsal"];
            txtTaTarikhErsalFromQueryString = Request.QueryString["txtTaTarikhErsal"];
            txtAzTarikhFormFromQueryString = Request.QueryString["txtAzTarikhForm"];
            txtTaTarikhFormFromQueryString = Request.QueryString["txtTaTarikhForm"];
            if (!IsPostBack)
            {


                if (Session["UserName"] == null)
                {
                    Server.Transfer("~/Pages/Login.aspx");
                }


           

                
                //#region SetGrid
                //ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                //if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                //    throw new Exception("Fatal error: missing connecting string in web.config file");
                //var conString = mySetting.ConnectionString;
                ////DataSet ds = new DataSet("DataSet");
                //DataTable dt = new DataTable();
                //using (SqlConnection conn = new SqlConnection(conString))
                //{

                //    SqlCommand sqlComm = new SqlCommand("ReportAmarNameh", conn);
                //    sqlComm.Parameters.AddWithValue("@AzTarikhErsaliSHamsi", txtAzTarikhErsalFromQueryString);
                //    sqlComm.Parameters.AddWithValue("@TaTarikhErsaliSHamsi", txtTaTarikhErsalFromQueryString);
                //    sqlComm.Parameters.AddWithValue("@AzTarikhFormSHamsi", txtAzTarikhFormFromQueryString);
                //    sqlComm.Parameters.AddWithValue("@TaTarikhFormSHamsi", txtTaTarikhFormFromQueryString);

                //    sqlComm.CommandType = CommandType.StoredProcedure;
                //    SqlDataAdapter da = new SqlDataAdapter();
                //    da.SelectCommand = sqlComm;
                //    da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                //    da.Fill(dt);
                //}


                //RadGrid1.DataSource = dt;
                //RadGrid1.DataBind();
                //#endregion
bindGrid();

            }

            PersianFilterGrid();
           // bindGrid();

        }
        public void bindGrid()
        {
            //   MainGrid.Width = Unit.Percentage(98);
            //MainGrid.PageSize = 15;
            // MainGrid.AllowPaging = true;
            //MainGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            //    MainGrid.AutoGenerateColumns = false;


            //MainGrid.MasterTableView.Width = Unit.Percentage(100);

            //GridCheckBoxColumn boundColumn1;
            //boundColumn1 = new GridCheckBoxColumn();
            //// RadGrid1.MasterTableView.Columns.Add(CheckBox);
            //boundColumn1.DataField = "CheckBox1";
            //boundColumn1.HeaderText = "انتخاب";


            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameMarkazPakhsh";
            boundColumn.HeaderText = "مرکز پخش";

          //  GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "CodeKalaOld";
            boundColumn.HeaderText = "کد کالا";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameKala";
            boundColumn.HeaderText = "نام کالا";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "IRC";
            boundColumn.HeaderText = "ایران کد";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Codegtin";
            boundColumn.HeaderText = "کدGTIN";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ShomarehBach";
            boundColumn.HeaderText = "شماره بچ";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "TarikhTolid";
            boundColumn.HeaderText = "تاریخ تولید";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "TarikhEngheza";
            boundColumn.HeaderText = "تاریخ انقضا";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "CodeNoeForm";
            boundColumn.HeaderText = "کد نوع فرم";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "txtNoeForm";
            boundColumn.HeaderText = "نوع فرم";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ShomarehForm";
            boundColumn.HeaderText = "شماره فرم";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "TarikhForm";
            boundColumn.HeaderText = "تاریخ فرم";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameTaminKonandeh";
            boundColumn.HeaderText = "نام تامین کننده";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameAnbar";
            boundColumn.HeaderText = "نام انبار";


            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Status";
            boundColumn.HeaderText = "وضعیت";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Comment";
            boundColumn.HeaderText = "توضیحات";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Time";
            boundColumn.HeaderText = "تاریخ ارسال";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "CodeMoshtaryOld";
            boundColumn.HeaderText = "کد مشتری";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameMoshtary";
            boundColumn.HeaderText = "نام مشتری";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ShomarehFaktor";
            boundColumn.HeaderText = "شماره فاکتور";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ShomarehElamMarjoee";
            boundColumn.HeaderText = "شماره اعلام مرجوعی";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ccKardexSatrUUID";
            boundColumn.HeaderText = "شناسه ارسال";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ccKardex";
            boundColumn.HeaderText = "شناسه کاردکس";

            boundColumn = new GridBoundColumn();
            RadGrid1.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "UUID";
            boundColumn.HeaderText = "UUID";


        }
        public void PersianFilterGrid()
        {
            GridFilterMenu menu = RadGrid1.FilterMenu;
            foreach (RadMenuItem item in menu.Items)
            {
                if (item.Text == "NoFilter")
                {
                    item.Text = "حذف فیلتر";
                }
                if (item.Text == "Contains")
                {
                    item.Text = "شامل";
                }
                if (item.Text == "DoesNotContain")
                {
                    item.Text = "شامل نباشد";
                }
                if (item.Text == "StartsWith")
                {
                    item.Text = "شروع شود";
                }
                if (item.Text == "EndsWith")
                {
                    item.Text = "تمام شود";
                }
                if (item.Text == "EqualTo")
                {
                    item.Text = "برابر";
                }
                if (item.Text == "NotEqualTo")
                {
                    item.Text = "نا برابر";
                }
                if (item.Text == "GreaterThan")
                {
                    item.Text = "بزرگتر";
                }
                if (item.Text == "LessThan")
                {
                    item.Text = "کوچکتر";
                }
                if (item.Text == "GreaterThanOrEqualTo")
                {
                    item.Text = "بزرگتر یا مساوی";
                }
                if (item.Text == "LessThanOrEqualTo")
                {
                    item.Text = "کوچکتر یا مساوی";
                }
                if (item.Text == "Between")
                {
                    item.Text = "مابین";
                }
                if (item.Text == "NotBetween")
                {
                    item.Text = "مابین نباشد";
                }
                if (item.Text == "IsEmpty")
                {
                    item.Text = "خالی";
                }
                if (item.Text == "NotIsEmpty")
                {
                    item.Text = "خالی نباشد";
                }
                if (item.Text == "IsNull")
                {
                    item.Text = "بی مقدار";
                }
                if (item.Text == "NotIsNull")
                {
                    item.Text = "بی مقدار نباشد";
                }
             

            }

        }

        protected void ToggleRowSelection(object sender, EventArgs e)
        {
            ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
            bool checkHeader = true;
            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                if (!(dataItem.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    checkHeader = false;
                    break;
                }
            }
            GridHeaderItem headerItem = RadGrid1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
            (headerItem.FindControl("headerChkbox") as CheckBox).Checked = checkHeader;
        }

        protected void btnEbtal_Click(object sender, ImageClickEventArgs e)
        {


                #region Delete
                foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
                {
                CheckBox chk = (CheckBox)item.FindControl("CheckBox1");
                if (chk.Checked == true)
                {
                    ConnectionStringSettings mySetting5 = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                        if (mySetting5 == null || string.IsNullOrEmpty(mySetting5.ConnectionString))
                            throw new Exception("Fatal error: missing connecting string in web.config file");
                        var conString5 = mySetting5.ConnectionString;
                        SqlCommand cm = new SqlCommand();
                        SqlDataAdapter da = new SqlDataAdapter();
                        SqlConnection cn = new SqlConnection(conString5);
                        SqlParameter p = null;
                        DataTable dt = null;
                        string strSQL = String.Empty;
                        string PasswordHash = String.Empty;

                        strSQL = "select * from TTACConfig";
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
                            CallbackService cs = new CallbackService();
                            StatisticsServiceClient client = new StatisticsServiceClient();
                            Authentication au = new Authentication();
                            EpcisEvent ee = new EpcisEvent();
                            ObjectEvent ce = new ObjectEvent();
                            DataTable dtKardex = new DataTable();

                            au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                            au.Password = dt.Rows[0]["TTAC_Password"].ToString();

                            var UUID = item.GetDataKeyValue("UUID").ToString();
                            Guid guid = new Guid(item.GetDataKeyValue("UUID").ToString());

                            CallbackService callbackService = new CallbackService();
                            callbackService.Url = dt.Rows[0]["TTAC_WebsSrviceUrl"].ToString();



                       // var result = client.GetCaptureStatusWithResponse(guid, au);

                        //client.CancelCapture(guid, au, callbackService);

                        var result = client.GetCaptureStatusWithResponse(guid, au);

                        //Pakhsh.ttacForUpdateDataTable dtForUpdate = new Pakhsh.ttacForUpdateDataTable();
                        //Models.PakhshTableAdapters.ttacForUpdateTableAdapter daForUpdate = new ttacForUpdateTableAdapter();

                        //au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                        //au.Password = dt.Rows[0]["TTAC_Password"].ToString();
                        //string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
                        // string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
                        //string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
                        //string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

                        //daForUpdate.Fill(dtForUpdate, AzTarikh, TaTarikh);

                        ////DataTable dtKardexInsertShodeh = new DataTable();
                        ////Models.PakhshTableAdapters.TTAC InsertKardexUUID = new Models.PakhshTableAdapters.TTAC();
                        Models.PakhshTableAdapters.TTAC daStatus = new TTAC();
                        string Payam = "";
                        if (result.Message != null)
                        {
                            Payam = result.Message.ToString();
                        }

                        
                        if (result.Successed == true)
                        {
                            daStatus.ttacForUpdateSuccessed(0, guid, 100, Payam+ " ابطال");
                            // rx.UpdateDataToKardexUUID(Convert.ToInt32(dtKardex.Rows[i]["ccKardexUUID"].ToString()), Convert.ToInt32(dtKardex.Rows[i]["ccKardex"].ToString()), dtKardex.Rows[i]["UUID"].ToString(), 2, 2, result.StatusCode.ToString() + " : " + Payam, Convert.ToInt32(dtKardex.Rows[i]["CodeNoeForm"].ToString()));
                        }
                        //string Payam = "";
                        //if (result.Message != null)
                        //{
                        //    Payam = result.Message.ToString();
                        //}

                        //if (result.Successed == false)
                        //{
                        //    InsertKardexUUID.UpdateKardexUUIDAfterCancel(3, result.StatusCode.ToString() + " : " + Payam, UUID);
                        //}
                        //if (result.Successed == true)
                        //{
                        //    InsertKardexUUID.UpdateKardexUUIDAfterCancel(2, result.StatusCode.ToString() + " : " + Payam, UUID);
                        //}

                    }
                    }
                }
                #endregion

            

        }


        protected void radgrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                CheckBox chk = (CheckBox)item["CheckBox1"].Controls[0];
                chk.Enabled = true;
            }
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("~/Pages/Main.aspx");
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }
    }






       
    }
