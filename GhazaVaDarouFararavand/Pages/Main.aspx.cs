
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

namespace GhazaVaDarouFararavand.Pages
{
    public partial class Main : System.Web.UI.Page
    {
        string WhereMarkazPakhsh = "";
        string WhereResid = "";
        string WhereHavaleh = "";
        string WhereKalaGoroh = "";
        string WhereTaminKonandeh = "";
        string WhereBrand = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            Status();
            //if (isValid() == true)
            //{

            //}
            //     bindGrid();
            PersianFilterGrid();
            #region loadMainGrid

            //Pakhsh.loadMainGridDataTable dtMain = new Pakhsh.loadMainGridDataTable();
            //Models.PakhshTableAdapters.loadMainGridTableAdapter daMain = new Models.PakhshTableAdapters.loadMainGridTableAdapter();

            //daMain.Fill(dtMain);
            //MainGrid.DataSource = dtMain;



            #endregion
            if (!IsPostBack)
            {

                if (Session["UserName"] == null)
                {
                    Server.Transfer("~/Pages/Login.aspx");
                }
                bindGrid();


                #region SetDatePicker
                var now = PersianDateTime.Now;
                var today = now.ToString(PersianDateTimeFormat.Date);

                txtAzTarikh.Attributes["onclick"] = "PersianDatePicker.Show(this,'" + today + "');";
                txtTaTarikh.Attributes["onclick"] = "PersianDatePicker.Show(this,'" + today + "');";
                //txtAzTarikhErsal.Attributes["onclick"] = "PersianDatePicker.Show();";
                //txtTaTarikhErsal.Attributes["onclick"] = "PersianDatePicker.Show();";
                //txtAzTarikhForm.Attributes["onclick"] = "PersianDatePicker.Show();";
                //txtTaTarikhForm.Attributes["onclick"] = "PersianDatePicker.Show();";
                #endregion

                #region list Vorodi
                Pakhsh.ReadVorodiDataTable dtVorodi = new Pakhsh.ReadVorodiDataTable();
                Models.PakhshTableAdapters.ReadVorodiTableAdapter daVorodi = new Models.PakhshTableAdapters.ReadVorodiTableAdapter();
                daVorodi.Fill(dtVorodi);
                ListResid.DataSource = dtVorodi;
                ListResid.DataTextField = "txtNoeForm";
                ListResid.DataValueField = "CodeNoeForm";
                ListResid.DataBind();
                #endregion

                #region ListKhoroji
                Pakhsh.ReadKhorojiDataTable dtKhoroji = new Pakhsh.ReadKhorojiDataTable();
                Models.PakhshTableAdapters.ReadKhorojiTableAdapter daKhoroji = new Models.PakhshTableAdapters.ReadKhorojiTableAdapter();
                daKhoroji.Fill(dtKhoroji);
                daKhoroji.Adapter.SelectCommand.CommandTimeout = 99;
                ListHavaleh.DataSource = dtKhoroji;
                ListHavaleh.DataTextField = "txtNoeForm";
                ListHavaleh.DataValueField = "CodeNoeForm";
                ListHavaleh.DataBind();
                #endregion

                #region List MarkazPakhsh
                Pakhsh.MarkazPakhshDataTable dtMarkazPakhsh = new Pakhsh.MarkazPakhshDataTable();
                Models.PakhshTableAdapters.MarkazPakhshTableAdapter daMarkazPakhsh = new Models.PakhshTableAdapters.MarkazPakhshTableAdapter();
                daMarkazPakhsh.Fill(dtMarkazPakhsh);
                ListMarkazPakhsh.DataSource = dtMarkazPakhsh;
                ListMarkazPakhsh.DataTextField = "NameMarkazPakhsh";
                ListMarkazPakhsh.DataValueField = "ccMarkazPakhsh";
                ListMarkazPakhsh.DataBind();
                #endregion

                #region List NameGorohKala
                Pakhsh.NameGorohKalaDataTable dtNameGorohKala = new Pakhsh.NameGorohKalaDataTable();
                Models.PakhshTableAdapters.NameGorohKalaTableAdapter daNameGorohKala = new Models.PakhshTableAdapters.NameGorohKalaTableAdapter();
                daNameGorohKala.Fill(dtNameGorohKala);
                ListKalaGoroh.DataSource = dtNameGorohKala;
                ListKalaGoroh.DataTextField = "NameGoroh";
                ListKalaGoroh.DataValueField = "ccGoroh";
                ListKalaGoroh.DataBind();
                #endregion

                #region List MoshtaryGoroh
                Pakhsh.GorohMoshtaryDataTable dtMoshtaryGoroh = new Pakhsh.GorohMoshtaryDataTable();
                Models.PakhshTableAdapters.GorohMoshtaryTableAdapter daMoshtaryGoroh = new Models.PakhshTableAdapters.GorohMoshtaryTableAdapter();
                //daMoshtaryGoroh.Fill(dtMoshtaryGoroh);
                //ListMoshtaryGoroh.DataSource = dtMoshtaryGoroh;
                //ListMoshtaryGoroh.DataTextField = "NameGoroh";
                //ListMoshtaryGoroh.DataValueField = "ccGoroh";
                //ListMoshtaryGoroh.DataBind();
                #endregion

                #region List Brand
                Pakhsh.NameBrandKalaDataTable dtBrand = new Pakhsh.NameBrandKalaDataTable();
                Models.PakhshTableAdapters.NameBrandKalaTableAdapter daBrand = new Models.PakhshTableAdapters.NameBrandKalaTableAdapter();
                daBrand.Fill(dtBrand);
                ListBrand.DataSource = dtBrand;
                ListBrand.DataTextField = "NameBrand";
                ListBrand.DataValueField = "ccBrand";
                ListBrand.DataBind();
                #endregion

                #region List TaminKonandeh
                Pakhsh.NameTaminKonandehDataTable dtTaminKonandeh = new Pakhsh.NameTaminKonandehDataTable();
                Models.PakhshTableAdapters.NameTaminKonandehTableAdapter daTaminKonandeh = new Models.PakhshTableAdapters.NameTaminKonandehTableAdapter();
                daTaminKonandeh.Fill(dtTaminKonandeh);
                ListTaminKonandeh.DataSource = dtTaminKonandeh;
                ListTaminKonandeh.DataTextField = "NameTaminKonandeh";
                ListTaminKonandeh.DataValueField = "ccTaminkonandeh";
                ListTaminKonandeh.DataBind();
                #endregion

                ShowSaveWhere();
                ShowConfig();
                //ChangePassword();
            }

        }

        public void Status()
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;

            //  Models.PakhshTableAdapters.TTAC ttac = new Models.PakhshTableAdapters.TTAC();
            DataTable dtStatus = new DataTable();
            // DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("TTAC_infomation", conn);
                // sqlComm.Parameters.AddWithValue("@strGorohMoshtary", MoshtaryGoroh);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                da.Fill(dtStatus);



                lblQueue.Text = dtStatus.Rows[0]["Queue"].ToString();
                lblSuccess.Text = dtStatus.Rows[0]["Success"].ToString();
                lblWarning.Text = dtStatus.Rows[0]["Warning"].ToString();
                lblOprationStatus.Text = dtStatus.Rows[0]["OprationStatus"].ToString();
                lblError.Text = dtStatus.Rows[0]["Error"].ToString();
                //lblQueue
                //lblSuccess
                //lblWarning
                //lblOprationStatus
                //lblError
            }
        }
        public void bindGrid()
        {
            //   MainGrid.Width = Unit.Percentage(98);
            //MainGrid.PageSize = 15;
            // MainGrid.AllowPaging = true;
            //MainGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            //    MainGrid.AutoGenerateColumns = false;


            //MainGrid.MasterTableView.Width = Unit.Percentage(100);

            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Sal";
            boundColumn.HeaderText = "سال";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "TarikhFormWithoutSlash";
            boundColumn.HeaderText = "تاریخ کاردکس";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameMarkazPakhsh";
            boundColumn.HeaderText = "مرکز پخش";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameAnbar";
            boundColumn.HeaderText = "نام انبار";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "txtNoeForm";
            boundColumn.HeaderText = "نوع فرم";



            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameTaminKonandeh";
            boundColumn.HeaderText = "نام تامین کننده";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "NameKala";
            boundColumn.HeaderText = "نام کالا";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "Tedad3";
            boundColumn.HeaderText = "تعداد";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "TedadBasteh";
            boundColumn.HeaderText = "تعداد بسته";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "TedadKarton";
            boundColumn.HeaderText = "تعداد کارتن";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ccKardex";
            boundColumn.HeaderText = "شناسه تیتر کاردکس";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ccKardexSatr";
            boundColumn.HeaderText = "شناسه سطر کاردکس";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "ccKardexFlat";
            boundColumn.HeaderText = "شناسه کاردکس اصلی";
            /*
            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "";
            boundColumn.HeaderText = "";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "";
            boundColumn.HeaderText = "";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "";
            boundColumn.HeaderText = "";

            boundColumn = new GridBoundColumn();
            MainGrid.MasterTableView.Columns.Add(boundColumn);
            boundColumn.DataField = "";
            boundColumn.HeaderText = "";
            */
        }
        public void SelectListSend()
        {
            #region SendAmarNameh
            string Resid = "";
            string Havaleh = "";
            string TaminKonandeh = "";
            string KalaGoroh = "";
            string MarkazPakhsh = "";
            string MoshtaryGoroh = "";
            string Brand = "";
            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

            #region ListResid
            string chkListResid = "";
            //  var q= ListResid.Items.FindAll()
            foreach (RadListBoxItem item in ListResid.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListResid += selectlist + ",";
                }

            }
            if (chkListResid.Length > 0)
            {
                var lenListResid = chkListResid.Length - 1;
                var selectListResid = (chkListResid.Substring(0, lenListResid));
                Resid = selectListResid;
            }


            #endregion

            #region ListHavaleh

            string chkListHavaleh = "";
            foreach (RadListBoxItem item1 in ListHavaleh.Items)
            {


                if (item1.Checked == true)
                {
                    string selectlist = item1.Value;
                    chkListHavaleh += selectlist + ",";
                }

            }
            if (chkListHavaleh.Length > 0)
            {
                var lenListHavaleh = chkListHavaleh.Length - 1;
                var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                Havaleh = selectListHavaleh;
            }

            //if (lenListHavaleh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListHavaleh.Items)
            //    {
            //        ListHavaleh.CheckBoxes = item.Checked;
            //        string selectlistHavaleh = item.Value;
            //        chkListHavaleh += selectlistHavaleh + ",";
            //    }

            //}

            #endregion

            #region ListTaminKonandeh
            string chkListTaminKonandeh = "";
            foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListTaminKonandeh += selectlist + ",";
                }

            }
            if (chkListTaminKonandeh.Length > 0)
            {
                var lenListTaminKonandeh = chkListTaminKonandeh.Length - 1;
                var selectListTaminKonandeh = (chkListTaminKonandeh.Substring(0, lenListTaminKonandeh));
                TaminKonandeh = selectListTaminKonandeh;
            }

            //if (lenListTaminKonandeh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            //    {
            //        ListTaminKonandeh.CheckBoxes = item.Checked;
            //        string selectListTaminKonandeh = item.Value;
            //        chkListTaminKonandeh += selectListTaminKonandeh + ",";
            //    }

            //}

            #endregion

            #region ListKalaGoroh
            string chkListKalaGoroh = "";
            foreach (RadListBoxItem item in ListKalaGoroh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListKalaGoroh += selectlist + ",";
                }

            }
            if (chkListKalaGoroh.Length > 0)
            {
                var lenListKalaGoroh = chkListKalaGoroh.Length - 1;
                var selectListKalaGoroh = (chkListKalaGoroh.Substring(0, lenListKalaGoroh));
                KalaGoroh = selectListKalaGoroh;
            }

            //if (lenListKalaGoroh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListKalaGoroh.Items)
            //    {
            //        ListKalaGoroh.CheckBoxes = item.Checked;
            //        string selectListKalaGoroh = item.Value;
            //        chkListKalaGoroh += selectListKalaGoroh + ",";
            //    }

            //}

            #endregion

            #region ListMarkazPakhsh
            string chkListMarkazPakhsh = "";
            foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListMarkazPakhsh += selectList + ",";
                }

            }
            if (chkListMarkazPakhsh.Length > 0)
            {
                var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                MarkazPakhsh = selectListMarkazPakhsh;
            }


            #endregion

            #region ListMoshtaryGoroh
            string chkListMoshtaryGoroh = "";

            /*
                        foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                        {
                            if (item.Checked == true)
                            {
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }

                        }
                        if (chkListMoshtaryGoroh.Length > 0)
                        {
                            var lenListMoshtaryGoroh = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGoroh));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }
                        else
                        {
                            foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                            {
                                ListMoshtaryGoroh.CheckBoxes = item.Checked;
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }
                            var lenListMoshtaryGorohAll = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGorohAll));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }


                */
            #endregion

            #region ListBrand
            string chkListBrand = "";
            string selectListBrand = "";
            foreach (RadListBoxItem item in ListBrand.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListBrand += selectList + ",";
                }

            }
            if (chkListBrand.Length > 0)
            {
                var lenListBrand = chkListBrand.Length - 1;
                selectListBrand = (chkListBrand.Substring(0, lenListBrand));
                Brand = selectListBrand;
            }


            #endregion




            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;


            DataSet ds = new DataSet("DataSet");
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("ttac", conn);
                if (rbRepeat.Checked)
                {
                    Pakhsh.WhereGHazaDarouDataTable dtWhere = new Pakhsh.WhereGHazaDarouDataTable();
                    Models.PakhshTableAdapters.WhereGHazaDarouTableAdapter daWhere = new WhereGHazaDarouTableAdapter();
                    daWhere.Fill(dtWhere);

                    if (dtWhere.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtWhere.Rows)
                        {
                            string WhereMarkazPakhsh = row["MarkazPakhsh"].ToString();
                            string WhereResid = row["Resid"].ToString();
                            string WhereHavaleh = row["Havaleh"].ToString();
                            string WhereKalaGoroh = row["KalaGoroh"].ToString();
                            string WhereTaminKonandeh = row["TaminKonandeh"].ToString();
                            string WhereBrand = row["Brand"].ToString();

                            sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                            sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                            sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", WhereMarkazPakhsh);
                            sqlComm.Parameters.AddWithValue("@strVorodi", WhereResid);
                            sqlComm.Parameters.AddWithValue("@StrKHoroji", WhereHavaleh);
                            sqlComm.Parameters.AddWithValue("@strGorohKala", WhereKalaGoroh);
                            sqlComm.Parameters.AddWithValue("@strTaminKonandeh", WhereTaminKonandeh);
                            sqlComm.Parameters.AddWithValue("@strBrand", WhereBrand);
                        }
                    }
                    else
                    {

                        string script = "alert(\"هیچ شرایطی برای ارسال ذخیره نشده\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                                      "ServerControlScript", script, true);
                    }

                }
                else
                {
                    sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                    sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                    sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                    sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                    sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);
                    sqlComm.Parameters.AddWithValue("@strGorohKala", KalaGoroh);
                    sqlComm.Parameters.AddWithValue("@strTaminKonandeh", TaminKonandeh);
                    sqlComm.Parameters.AddWithValue("@strBrand", Brand);
                }

                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                da.Fill(ds);

            }

            SaveWhere(MarkazPakhsh, Resid, Havaleh, KalaGoroh, TaminKonandeh, Brand);

            var products = ds.Tables[0].AsEnumerable().ToList();
            var qq = products.Select(n => new
            {
                ccKardexsatr = n.Field<Int64>("ccKardexSatr")

            });

            foreach (var item in qq)
            {


                try
                {
                    ConnectionStringSettings mySetting2 = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                    if (mySetting2 == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                        throw new Exception("Fatal error: missing connecting string in web.config file");
                    var conString2 = mySetting.ConnectionString;
                    SqlCommand cm = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlConnection cn = new SqlConnection(conString2);
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

                        ce = ReadFromSQLVaset(item.ccKardexsatr);


                        CallbackService callBack = new CallbackService();
                        callBack.Url = dt.Rows[0]["TTAC_WebsSrviceUrl"].ToString();



                        using (SqlConnection conn = new SqlConnection(conString))
                        {
                            SqlCommand sqlComm = new SqlCommand("TitrSatrTTAC", conn);
                            sqlComm.Parameters.AddWithValue("@ccKardexSatr", item.ccKardexsatr);

                            sqlComm.CommandType = CommandType.StoredProcedure;

                            SqlDataAdapter da4 = new SqlDataAdapter();
                            da4.SelectCommand = sqlComm;

                            da4.Fill(dtKardex);


                            SqlCommand sqlComm2 = new SqlCommand("InsertKardexUUID", conn);
                            sqlComm2.Parameters.AddWithValue("@ccKardex", Convert.ToInt32(dtKardex.Rows[0]["ccKardex"].ToString()));
                            sqlComm2.Parameters.AddWithValue("@UUID", ce.EventID.ToString());
                            sqlComm2.Parameters.AddWithValue("@CodeNoeAction", 1);
                            sqlComm2.Parameters.AddWithValue("@Status", 1);
                            sqlComm2.Parameters.AddWithValue("@Comment", " ");
                            sqlComm2.Parameters.AddWithValue("@ccKardexSatr", Convert.ToInt32(dtKardex.Rows[0]["ccKardexSatr"].ToString()));

                            sqlComm2.CommandType = CommandType.StoredProcedure;
                            da4.SelectCommand = sqlComm2;
                            //   sqlComm2.ExecuteNonQuery;
                            da4.SelectCommand.CommandTimeout = 9999999;
                            da4.Fill(ds);

                        }


                        //  client.Capture(new ObjectEvent[] { ce }, au, callBack);

                    }
                }
                catch (Exception e)
                {
                    var a = e.Message;
                }





            }
            #endregion



        }
        public void SelectListItem()
        {

            string Resid = "";
            string Havaleh = "";
            string TaminKonandeh = "";
            string KalaGoroh = "";
            string MarkazPakhsh = "";
            string MoshtaryGoroh = "";
            string Brand = "";
            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

            #region ListResid
            string chkListResid = "";
            //  var q= ListResid.Items.FindAll()
            foreach (RadListBoxItem item in ListResid.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListResid += selectlist + ",";
                }

            }
            if (chkListResid.Length > 0)
            {
                var lenListResid = chkListResid.Length - 1;
                var selectListResid = (chkListResid.Substring(0, lenListResid));
                Resid = selectListResid;
            }


            #endregion

            #region ListHavaleh

            string chkListHavaleh = "";
            foreach (RadListBoxItem item1 in ListHavaleh.Items)
            {


                if (item1.Checked == true)
                {
                    string selectlist = item1.Value;
                    chkListHavaleh += selectlist + ",";
                }

            }
            if (chkListHavaleh.Length > 0)
            {
                var lenListHavaleh = chkListHavaleh.Length - 1;
                var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                Havaleh = selectListHavaleh;
            }

            //if (lenListHavaleh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListHavaleh.Items)
            //    {
            //        ListHavaleh.CheckBoxes = item.Checked;
            //        string selectlistHavaleh = item.Value;
            //        chkListHavaleh += selectlistHavaleh + ",";
            //    }

            //}

            #endregion

            #region ListTaminKonandeh
            string chkListTaminKonandeh = "";
            foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListTaminKonandeh += selectlist + ",";
                }

            }
            if (chkListTaminKonandeh.Length > 0)
            {
                var lenListTaminKonandeh = chkListTaminKonandeh.Length - 1;
                var selectListTaminKonandeh = (chkListTaminKonandeh.Substring(0, lenListTaminKonandeh));
                TaminKonandeh = selectListTaminKonandeh;
            }

            //if (lenListTaminKonandeh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            //    {
            //        ListTaminKonandeh.CheckBoxes = item.Checked;
            //        string selectListTaminKonandeh = item.Value;
            //        chkListTaminKonandeh += selectListTaminKonandeh + ",";
            //    }

            //}

            #endregion

            #region ListKalaGoroh
            string chkListKalaGoroh = "";
            foreach (RadListBoxItem item in ListKalaGoroh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListKalaGoroh += selectlist + ",";
                }

            }
            if (chkListKalaGoroh.Length > 0)
            {
                var lenListKalaGoroh = chkListKalaGoroh.Length - 1;
                var selectListKalaGoroh = (chkListKalaGoroh.Substring(0, lenListKalaGoroh));
                KalaGoroh = selectListKalaGoroh;
            }

            //if (lenListKalaGoroh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListKalaGoroh.Items)
            //    {
            //        ListKalaGoroh.CheckBoxes = item.Checked;
            //        string selectListKalaGoroh = item.Value;
            //        chkListKalaGoroh += selectListKalaGoroh + ",";
            //    }

            //}

            #endregion

            #region ListMarkazPakhsh
            string chkListMarkazPakhsh = "";
            foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListMarkazPakhsh += selectList + ",";
                }

            }
            if (chkListMarkazPakhsh.Length > 0)
            {
                var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                MarkazPakhsh = selectListMarkazPakhsh;
            }


            #endregion

            #region ListMoshtaryGoroh
            string chkListMoshtaryGoroh = "";

            /*
                        foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                        {
                            if (item.Checked == true)
                            {
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }

                        }
                        if (chkListMoshtaryGoroh.Length > 0)
                        {
                            var lenListMoshtaryGoroh = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGoroh));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }
                        else
                        {
                            foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                            {
                                ListMoshtaryGoroh.CheckBoxes = item.Checked;
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }
                            var lenListMoshtaryGorohAll = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGorohAll));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }


                */
            #endregion

            #region ListBrand
            string chkListBrand = "";
            string selectListBrand = "";
            foreach (RadListBoxItem item in ListBrand.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListBrand += selectList + ",";
                }

            }
            if (chkListBrand.Length > 0)
            {
                var lenListBrand = chkListBrand.Length - 1;
                selectListBrand = (chkListBrand.Substring(0, lenListBrand));
                Brand = selectListBrand;
            }


            #endregion




            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;

            //  Models.PakhshTableAdapters.TTAC ttac = new Models.PakhshTableAdapters.TTAC();






            DataSet ds = new DataSet("DataSet");
            // DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("ttac", conn);
                sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);
                sqlComm.Parameters.AddWithValue("@strGorohKala", KalaGoroh);
                sqlComm.Parameters.AddWithValue("@strTaminKonandeh", TaminKonandeh);
                sqlComm.Parameters.AddWithValue("@strBrand", Brand);
                // sqlComm.Parameters.AddWithValue("@strGorohMoshtary", MoshtaryGoroh);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                da.Fill(ds);
            }


            MainGrid.DataSource = ds;
            MainGrid.DataBind();
        }
        public void SelectListItemSend()
        {

            #region ShowSendData

            CallbackService cs = new CallbackService();
            StatisticsServiceClient client = new StatisticsServiceClient();
            Authentication au = new Authentication();
            EpcisEvent ee = new EpcisEvent();
            ObjectEvent ce = new ObjectEvent();
            CallbackService callBack = new CallbackService();

            string Resid = "";
            string Havaleh = "";
            string TaminKonandeh = "";
            string KalaGoroh = "";
            string MarkazPakhsh = "";
            string MoshtaryGoroh = "";
            string Brand = "";
            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

            #region ListResid
            string chkListResid = "";
            //  var q= ListResid.Items.FindAll()
            foreach (RadListBoxItem item in ListResid.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListResid += selectlist + ",";
                }

            }
            if (chkListResid.Length > 0)
            {
                var lenListResid = chkListResid.Length - 1;
                var selectListResid = (chkListResid.Substring(0, lenListResid));
                Resid = selectListResid;
            }


            #endregion

            #region ListHavaleh

            string chkListHavaleh = "";
            foreach (RadListBoxItem item1 in ListHavaleh.Items)
            {


                if (item1.Checked == true)
                {
                    string selectlist = item1.Value;
                    chkListHavaleh += selectlist + ",";
                }

            }
            if (chkListHavaleh.Length > 0)
            {
                var lenListHavaleh = chkListHavaleh.Length - 1;
                var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                Havaleh = selectListHavaleh;
            }

            //if (lenListHavaleh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListHavaleh.Items)
            //    {
            //        ListHavaleh.CheckBoxes = item.Checked;
            //        string selectlistHavaleh = item.Value;
            //        chkListHavaleh += selectlistHavaleh + ",";
            //    }

            //}

            #endregion

            #region ListMarkazPakhsh
            string chkListMarkazPakhsh = "";
            foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListMarkazPakhsh += selectList + ",";
                }

            }
            if (chkListMarkazPakhsh.Length > 0)
            {
                var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                MarkazPakhsh = selectListMarkazPakhsh;
            }


            #endregion

            #region ListTaminKonandeh
            string chkListTaminKonandeh = "";
            foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListTaminKonandeh += selectlist + ",";
                }

            }
            if (chkListTaminKonandeh.Length > 0)
            {
                var lenListTaminKonandeh = chkListTaminKonandeh.Length - 1;
                var selectListTaminKonandeh = (chkListTaminKonandeh.Substring(0, lenListTaminKonandeh));
                TaminKonandeh = selectListTaminKonandeh;
            }

            //if (lenListTaminKonandeh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            //    {
            //        ListTaminKonandeh.CheckBoxes = item.Checked;
            //        string selectListTaminKonandeh = item.Value;
            //        chkListTaminKonandeh += selectListTaminKonandeh + ",";
            //    }

            //}

            #endregion

            #region ListKalaGoroh
            string chkListKalaGoroh = "";
            foreach (RadListBoxItem item in ListKalaGoroh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListKalaGoroh += selectlist + ",";
                }

            }
            if (chkListKalaGoroh.Length > 0)
            {
                var lenListKalaGoroh = chkListKalaGoroh.Length - 1;
                var selectListKalaGoroh = (chkListKalaGoroh.Substring(0, lenListKalaGoroh));
                KalaGoroh = selectListKalaGoroh;
            }

            //if (lenListKalaGoroh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListKalaGoroh.Items)
            //    {
            //        ListKalaGoroh.CheckBoxes = item.Checked;
            //        string selectListKalaGoroh = item.Value;
            //        chkListKalaGoroh += selectListKalaGoroh + ",";
            //    }

            //}

            #endregion

            #region ListMoshtaryGoroh
            string chkListMoshtaryGoroh = "";

            /*
                        foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                        {
                            if (item.Checked == true)
                            {
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }

                        }
                        if (chkListMoshtaryGoroh.Length > 0)
                        {
                            var lenListMoshtaryGoroh = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGoroh));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }
                        else
                        {
                            foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                            {
                                ListMoshtaryGoroh.CheckBoxes = item.Checked;
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }
                            var lenListMoshtaryGorohAll = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGorohAll));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }


                */
            #endregion

            #region ListBrand
            string chkListBrand = "";
            string selectListBrand = "";
            foreach (RadListBoxItem item in ListBrand.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListBrand += selectList + ",";
                }

            }
            if (chkListBrand.Length > 0)
            {
                var lenListBrand = chkListBrand.Length - 1;
                selectListBrand = (chkListBrand.Substring(0, lenListBrand));
                Brand = selectListBrand;
            }


            #endregion




            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;

            //  Models.PakhshTableAdapters.TTAC ttac = new Models.PakhshTableAdapters.TTAC();






            DataTable ds = new DataTable();
            // DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("ttac", conn);
                sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);
                sqlComm.Parameters.AddWithValue("@strGorohKala", KalaGoroh);
                sqlComm.Parameters.AddWithValue("@strTaminKonandeh", TaminKonandeh);
                sqlComm.Parameters.AddWithValue("@strBrand", Brand);
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



                        ReadFromSQL(Convert.ToInt64(ds.Rows[i]["ccKardexSatr"].ToString()), Convert.ToInt64(ds.Rows[i]["ccKardex"].ToString()));
                    }
                }
                else
                {
                    string script = "alert(\"در این بازه رکوردی وجود ندارد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }
            }




            //var products = ds.Tables[0].AsEnumerable().ToList();
            //var qq = products.Select(n => new
            //{
            //    ccKardexsatr = n.Field<Int64>("ccKardexSatr")

            //});




            //foreach (var item in qq)
            //{

            //    ReadFromSQL(item.ccKardexsatr);
            //}

            ConnectionStringSettings mySetting2 = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting2 == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString2 = mySetting.ConnectionString;
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection cn = new SqlConnection(conString2);
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





                au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                au.Password = dt.Rows[0]["TTAC_Password"].ToString();
                callBack.Url = dt.Rows[0]["TTAC_WebsSrviceUrl"].ToString();






            }

            DataTable dtKardexReadVaset = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                //SqlCommand sqlCommRead = new SqlCommand("TitrSatrTTAC", conn);
                //sqlCommRead.Parameters.AddWithValue("@ccKardexSatr", item.ccKardexsatr);

                //sqlCommRead.CommandType = CommandType.StoredProcedure;


                //da4.SelectCommand = sqlCommRead;

                //da4.Fill(dtKardexReadVaset);


                SqlCommand sqlCommRead = new SqlCommand("readVasetAmarNameh", conn);



                //  sqlCommRead.Parameters.AddWithValue("@ccKardexSatr", item.ccKardexsatr);



                sqlCommRead.CommandType = CommandType.StoredProcedure;
                da4.SelectCommand = sqlCommRead;
                da4.SelectCommand.CommandTimeout = 9999999;
                da4.Fill(dtKardexReadVaset);


                //    }
                SqlDataAdapter da3 = new SqlDataAdapter();
                DataTable dtKardex = new DataTable();
                for (int i = 0; i < dtKardexReadVaset.Rows.Count; i++)
                {
                    ce = ReadFromSQLVaset(Convert.ToInt64(dtKardexReadVaset.Rows[i]["ccKardexSatr"]));

                    client.Capture(new ObjectEvent[] { ce }, au, callBack);


                    using (SqlConnection conn1 = new SqlConnection(conString))
                    {

                        //SqlCommand sqlComm = new SqlCommand("TitrSatrTTAC", conn);
                        //sqlComm.Parameters.AddWithValue("@ccKardexSatr", item.ccKardexsatr);

                        //sqlComm.CommandType = CommandType.StoredProcedure;


                        //da3.SelectCommand = sqlComm;

                        //da3.Fill(dtKardex);


                        SqlCommand sqlComm2 = new SqlCommand("InsertKardexUUID", conn);
                        sqlComm2.Parameters.AddWithValue("@ccKardex", Convert.ToInt32(dtKardexReadVaset.Rows[i]["ccKardex"].ToString()));
                        sqlComm2.Parameters.AddWithValue("@UUID", ce.EventID.ToString());
                        sqlComm2.Parameters.AddWithValue("@CodeNoeAction", 1);
                        sqlComm2.Parameters.AddWithValue("@Status", 1);
                        sqlComm2.Parameters.AddWithValue("@Comment", " ");
                        sqlComm2.Parameters.AddWithValue("@ccKardexSatr", Convert.ToInt32(dtKardexReadVaset.Rows[i]["ccKardexSatr"].ToString()));

                        sqlComm2.CommandType = CommandType.StoredProcedure;
                        da3.SelectCommand = sqlComm2;
                        da3.SelectCommand.CommandTimeout = 9999999;
                        da3.Fill(ds);


                    }

                    DataTable dtKardexDeleteVaset = new DataTable();
                    SqlDataAdapter daDelete = new SqlDataAdapter();
                    using (SqlConnection conn2 = new SqlConnection(conString))
                    {





                        //  Convert.ToInt32(dtKardexReadVaset.Rows[i]["ccKardex"].ToString())
                        SqlCommand sqlCommDelete = new SqlCommand("DeletereadVasetAmarNameh", conn);
                        sqlCommDelete.Parameters.AddWithValue("@ccKardexSatr", Convert.ToInt32(dtKardexReadVaset.Rows[i]["ccKardexSatr"].ToString()));
                        sqlCommDelete.CommandType = CommandType.StoredProcedure;
                        daDelete.SelectCommand = sqlCommDelete;
                        daDelete.SelectCommand.CommandTimeout = 9999999;
                        daDelete.Fill(dtKardexDeleteVaset);

                        DataRow dr = dtKardexReadVaset.Rows[i];

                        DataTable test = dtKardexReadVaset;
                        dtKardexReadVaset.Rows.Remove(dr);


                    }






                }

                dtKardexReadVaset.Clear();
                dtKardexReadVaset = null;



            }
            #endregion
        }
        public void SelectListItemRefresh()
        {

            string Resid = "";
            string Havaleh = "";
            string TaminKonandeh = "";
            string KalaGoroh = "";
            string MarkazPakhsh = "";
            string MoshtaryGoroh = "";
            string Brand = "";
            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

            #region ListResid
            string chkListResid = "";
            //  var q= ListResid.Items.FindAll()
            foreach (RadListBoxItem item in ListResid.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListResid += selectlist + ",";
                }

            }
            if (chkListResid.Length > 0)
            {
                var lenListResid = chkListResid.Length - 1;
                var selectListResid = (chkListResid.Substring(0, lenListResid));
                Resid = selectListResid;
            }


            #endregion

            #region ListHavaleh

            string chkListHavaleh = "";
            foreach (RadListBoxItem item1 in ListHavaleh.Items)
            {


                if (item1.Checked == true)
                {
                    string selectlist = item1.Value;
                    chkListHavaleh += selectlist + ",";
                }

            }
            if (chkListHavaleh.Length > 0)
            {
                var lenListHavaleh = chkListHavaleh.Length - 1;
                var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                Havaleh = selectListHavaleh;
            }

            //if (lenListHavaleh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListHavaleh.Items)
            //    {
            //        ListHavaleh.CheckBoxes = item.Checked;
            //        string selectlistHavaleh = item.Value;
            //        chkListHavaleh += selectlistHavaleh + ",";
            //    }

            //}

            #endregion

            #region ListTaminKonandeh
            string chkListTaminKonandeh = "";
            foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListTaminKonandeh += selectlist + ",";
                }

            }
            if (chkListTaminKonandeh.Length > 0)
            {
                var lenListTaminKonandeh = chkListTaminKonandeh.Length - 1;
                var selectListTaminKonandeh = (chkListTaminKonandeh.Substring(0, lenListTaminKonandeh));
                TaminKonandeh = selectListTaminKonandeh;
            }

            //if (lenListTaminKonandeh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            //    {
            //        ListTaminKonandeh.CheckBoxes = item.Checked;
            //        string selectListTaminKonandeh = item.Value;
            //        chkListTaminKonandeh += selectListTaminKonandeh + ",";
            //    }

            //}

            #endregion

            #region ListKalaGoroh
            string chkListKalaGoroh = "";
            foreach (RadListBoxItem item in ListKalaGoroh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListKalaGoroh += selectlist + ",";
                }

            }
            if (chkListKalaGoroh.Length > 0)
            {
                var lenListKalaGoroh = chkListKalaGoroh.Length - 1;
                var selectListKalaGoroh = (chkListKalaGoroh.Substring(0, lenListKalaGoroh));
                KalaGoroh = selectListKalaGoroh;
            }

            //if (lenListKalaGoroh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListKalaGoroh.Items)
            //    {
            //        ListKalaGoroh.CheckBoxes = item.Checked;
            //        string selectListKalaGoroh = item.Value;
            //        chkListKalaGoroh += selectListKalaGoroh + ",";
            //    }

            //}

            #endregion

            #region ListMarkazPakhsh
            string chkListMarkazPakhsh = "";
            foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListMarkazPakhsh += selectList + ",";
                }

            }
            if (chkListMarkazPakhsh.Length > 0)
            {
                var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                MarkazPakhsh = selectListMarkazPakhsh;
            }


            #endregion

            #region ListMoshtaryGoroh
            string chkListMoshtaryGoroh = "";

            /*
                        foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                        {
                            if (item.Checked == true)
                            {
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }

                        }
                        if (chkListMoshtaryGoroh.Length > 0)
                        {
                            var lenListMoshtaryGoroh = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGoroh));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }
                        else
                        {
                            foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                            {
                                ListMoshtaryGoroh.CheckBoxes = item.Checked;
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }
                            var lenListMoshtaryGorohAll = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGorohAll));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }


                */
            #endregion

            #region ListBrand
            string chkListBrand = "";
            string selectListBrand = "";
            foreach (RadListBoxItem item in ListBrand.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListBrand += selectList + ",";
                }

            }
            if (chkListBrand.Length > 0)
            {
                var lenListBrand = chkListBrand.Length - 1;
                selectListBrand = (chkListBrand.Substring(0, lenListBrand));
                Brand = selectListBrand;
            }


            #endregion




            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;

            //  Models.PakhshTableAdapters.TTAC ttac = new Models.PakhshTableAdapters.TTAC();






            DataSet ds = new DataSet("DataSet");
            // DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("ttacRefresh", conn);
                sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);
                sqlComm.Parameters.AddWithValue("@strGorohKala", KalaGoroh);
                sqlComm.Parameters.AddWithValue("@strTaminKonandeh", TaminKonandeh);
                sqlComm.Parameters.AddWithValue("@strBrand", Brand);
                // sqlComm.Parameters.AddWithValue("@strGorohMoshtary", MoshtaryGoroh);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                da.Fill(ds);
            }


            MainGrid.DataSource = ds;
            MainGrid.DataBind();

        }
        public void SelectListItemRefreshSend()
        {

            string Resid = "";
            string Havaleh = "";
            string TaminKonandeh = "";
            string KalaGoroh = "";
            string MarkazPakhsh = "";
            string MoshtaryGoroh = "";
            string Brand = "";
            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

            #region ListResid
            string chkListResid = "";
            //  var q= ListResid.Items.FindAll()
            foreach (RadListBoxItem item in ListResid.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListResid += selectlist + ",";
                }

            }
            if (chkListResid.Length > 0)
            {
                var lenListResid = chkListResid.Length - 1;
                var selectListResid = (chkListResid.Substring(0, lenListResid));
                Resid = selectListResid;
            }


            #endregion

            #region ListHavaleh

            string chkListHavaleh = "";
            foreach (RadListBoxItem item1 in ListHavaleh.Items)
            {


                if (item1.Checked == true)
                {
                    string selectlist = item1.Value;
                    chkListHavaleh += selectlist + ",";
                }

            }
            if (chkListHavaleh.Length > 0)
            {
                var lenListHavaleh = chkListHavaleh.Length - 1;
                var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                Havaleh = selectListHavaleh;
            }

            //if (lenListHavaleh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListHavaleh.Items)
            //    {
            //        ListHavaleh.CheckBoxes = item.Checked;
            //        string selectlistHavaleh = item.Value;
            //        chkListHavaleh += selectlistHavaleh + ",";
            //    }

            //}

            #endregion

            #region ListTaminKonandeh
            string chkListTaminKonandeh = "";
            foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListTaminKonandeh += selectlist + ",";
                }

            }
            if (chkListTaminKonandeh.Length > 0)
            {
                var lenListTaminKonandeh = chkListTaminKonandeh.Length - 1;
                var selectListTaminKonandeh = (chkListTaminKonandeh.Substring(0, lenListTaminKonandeh));
                TaminKonandeh = selectListTaminKonandeh;
            }

            //if (lenListTaminKonandeh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListTaminKonandeh.Items)
            //    {
            //        ListTaminKonandeh.CheckBoxes = item.Checked;
            //        string selectListTaminKonandeh = item.Value;
            //        chkListTaminKonandeh += selectListTaminKonandeh + ",";
            //    }

            //}

            #endregion

            #region ListKalaGoroh
            string chkListKalaGoroh = "";
            foreach (RadListBoxItem item in ListKalaGoroh.Items)
            {
                if (item.Checked == true)
                {
                    string selectlist = item.Value;
                    chkListKalaGoroh += selectlist + ",";
                }

            }
            if (chkListKalaGoroh.Length > 0)
            {
                var lenListKalaGoroh = chkListKalaGoroh.Length - 1;
                var selectListKalaGoroh = (chkListKalaGoroh.Substring(0, lenListKalaGoroh));
                KalaGoroh = selectListKalaGoroh;
            }

            //if (lenListKalaGoroh <= 0)
            //{

            //    foreach (RadListBoxItem item in ListKalaGoroh.Items)
            //    {
            //        ListKalaGoroh.CheckBoxes = item.Checked;
            //        string selectListKalaGoroh = item.Value;
            //        chkListKalaGoroh += selectListKalaGoroh + ",";
            //    }

            //}

            #endregion

            #region ListMarkazPakhsh
            string chkListMarkazPakhsh = "";
            foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListMarkazPakhsh += selectList + ",";
                }

            }
            if (chkListMarkazPakhsh.Length > 0)
            {
                var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                MarkazPakhsh = selectListMarkazPakhsh;
            }


            #endregion

            #region ListMoshtaryGoroh
            string chkListMoshtaryGoroh = "";

            /*
                        foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                        {
                            if (item.Checked == true)
                            {
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }

                        }
                        if (chkListMoshtaryGoroh.Length > 0)
                        {
                            var lenListMoshtaryGoroh = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGoroh));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }
                        else
                        {
                            foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                            {
                                ListMoshtaryGoroh.CheckBoxes = item.Checked;
                                string selectList = item.Value;
                                chkListMoshtaryGoroh += selectList + ",";
                            }
                            var lenListMoshtaryGorohAll = chkListMoshtaryGoroh.Length - 1;
                            var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGorohAll));
                            MoshtaryGoroh = selectListMoshtaryGoroh;
                        }


                */
            #endregion

            #region ListBrand
            string chkListBrand = "";
            string selectListBrand = "";
            foreach (RadListBoxItem item in ListBrand.Items)
            {
                if (item.Checked == true)
                {
                    string selectList = item.Value;
                    chkListBrand += selectList + ",";
                }

            }
            if (chkListBrand.Length > 0)
            {
                var lenListBrand = chkListBrand.Length - 1;
                selectListBrand = (chkListBrand.Substring(0, lenListBrand));
                Brand = selectListBrand;
            }


            #endregion




            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;

            //  Models.PakhshTableAdapters.TTAC ttac = new Models.PakhshTableAdapters.TTAC();






            DataSet ds = new DataSet("DataSet");
            // DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("ttacRefresh", conn);
                sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);
                sqlComm.Parameters.AddWithValue("@strGorohKala", KalaGoroh);
                sqlComm.Parameters.AddWithValue("@strTaminKonandeh", TaminKonandeh);
                sqlComm.Parameters.AddWithValue("@strBrand", Brand);
                // sqlComm.Parameters.AddWithValue("@strGorohMoshtary", MoshtaryGoroh);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                da.Fill(ds);
            }


            MainGrid.DataSource = ds;
            MainGrid.DataBind();
            //  bindGrid();

            var products = ds.Tables[0].AsEnumerable().ToList();
            var qq = products.Select(n => new
            {
                ccKardexsatr = n.Field<Int64>("ccKardexSatr")

            });

            foreach (var item in qq)
            {
                CallbackService cs = new CallbackService();
                StatisticsServiceClient client = new StatisticsServiceClient();
                Authentication au = new Authentication();
                EpcisEvent ee = new EpcisEvent();
                ObjectEvent c = new ObjectEvent();
                DataTable dtKardex = new DataTable();
                SqlCommand cm = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlConnection cn = new SqlConnection(conString);
                DataTable dt = null;
                string strSQL = String.Empty;
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
                    au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                    au.Password = dt.Rows[0]["TTAC_Password"].ToString();

                    // callBack.Url = dt.Rows[0]["TTAC_WebsSrviceUrl"].ToString();
                    using (SqlConnection conn = new SqlConnection(conString))
                    {
                        SqlCommand sqlComm = new SqlCommand("TitrSatrTTAC", conn);
                        sqlComm.Parameters.AddWithValue("@ccKardexSatr", item.ccKardexsatr);
                        sqlComm.Parameters.AddWithValue("@ccKardex", 1);

                        sqlComm.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da3 = new SqlDataAdapter();
                        da3.SelectCommand = sqlComm;

                        da3.Fill(dtKardex);
                    }



                    try
                    {
                        ObjectEvent ce = new ObjectEvent();
                        ce = ReadFromSQL(item.ccKardexsatr,1);
                        Guid guid = new Guid(dtKardex.Rows[0]["UUID"].ToString());
                        var result = client.GetCaptureStatusWithResponse(guid, au);
                        DataTable dtKardexInsertShodeh = new DataTable();
                        Models.PakhshTableAdapters.TTAC InsertKardexUUID = new Models.PakhshTableAdapters.TTAC();
                        string Payam = "";
                        if (result.Message != null)
                        {
                            Payam = result.Message.ToString();
                        }

                        if (result.Successed == false)
                        {

                            InsertKardexUUID.UpdateKardexUUID(3, result.StatusCode.ToString() + " : " + Payam, /*Convert.ToInt32(dtKardex.Rows[0]["ccKardexSatrUUID"].ToString())*/item.ccKardexsatr);
                            //   rx.UpdateDataToKardexUUID(Convert.ToInt32(dtKardex.Rows[i]["ccKardexUUID"].ToString()), Convert.ToInt32(dtKardex.Rows[i]["ccKardex"].ToString()), dtKardex.Rows[i]["UUID"].ToString(), 2, 3, result.StatusCode.ToString() + " : " + Payam, Convert.ToInt32(dtKardex.Rows[i]["CodeNoeForm"].ToString()));
                        }
                        if (result.Successed == true)
                        {
                            InsertKardexUUID.UpdateKardexUUID(2, result.StatusCode.ToString() + " : " + Payam, item.ccKardexsatr);
                            // rx.UpdateDataToKardexUUID(Convert.ToInt32(dtKardex.Rows[i]["ccKardexUUID"].ToString()), Convert.ToInt32(dtKardex.Rows[i]["ccKardex"].ToString()), dtKardex.Rows[i]["UUID"].ToString(), 2, 2, result.StatusCode.ToString() + " : " + Payam, Convert.ToInt32(dtKardex.Rows[i]["CodeNoeForm"].ToString()));
                        }



                    }
                    catch (Exception e)
                    {
                        var a = e.Message;

                        throw;
                    }

                }



            }
        }

        private void InsertDataToKardexUUID(/*int cckardexSatr,*/ int cckardex, string EventId, int CodeNoeAction, int Status, string Comment, int cckardexSatr/* , int CodeNoeForm*/)
        {
            //Models.PakhshTableAdapters.TTAC InsertKardexUUID = new Models.PakhshTableAdapters.TTAC();
            //InsertKardexUUID.InsertKardexUUID(cckardex, EventId, CodeNoeAction, Status, Comment, cckardexSatr);
            //InsertKardexUUID.InsertKardexUUID(cckardex,)
            //  inser
            //  InsertKardexUUID.
            //  mode
        }

        public ObjectEvent ReadFromSQL(Int64 ccKardexSatr,Int64 ccKardex)
        {



            Destination d = new Destination();
            Ilmd oIlm = new Ilmd();
            QuantityElement oQuantityElement = new QuantityElement();
            ObjectEventExtension oEventExtension = new ObjectEventExtension();
            EpcisEvent ee = new EpcisEvent();
            BusinessLocation BL = new BusinessLocation();
            ObjectEvent oe = new ObjectEvent();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            DataSet dsTitrSatrTTAC = new DataSet();
            DataTable dtTitrSatrTTAC = new DataTable();
            // SqlDataAdapter daTitrSatrTTAC = new SqlDataAdapter();
            ReadPoint rp = new ReadPoint();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("TitrSatrTTAC", conn);
                sqlComm.Parameters.AddWithValue("@ccKardexSatr", ccKardexSatr);
                sqlComm.Parameters.AddWithValue("@ccKardex", ccKardex);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter daTitrSatrTTAC = new SqlDataAdapter();
                daTitrSatrTTAC.SelectCommand = sqlComm;

                daTitrSatrTTAC.Fill(dtTitrSatrTTAC);

                return null;
            }
        }
        public ObjectEvent ReadFromSQLVaset(Int64 ccKardexSatr)
        {



            Destination d = new Destination();
            Ilmd oIlm = new Ilmd();
            QuantityElement oQuantityElement = new QuantityElement();
            ObjectEventExtension oEventExtension = new ObjectEventExtension();
            EpcisEvent ee = new EpcisEvent();
            BusinessLocation BL = new BusinessLocation();
            ObjectEvent oe = new ObjectEvent();
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            DataSet dsTitrSatrTTAC = new DataSet();
            DataTable dtAmarNamehVaset = new DataTable();
            // SqlDataAdapter daTitrSatrTTAC = new SqlDataAdapter();
            ReadPoint rp = new ReadPoint();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("readVasetAmarNameh_Send", conn);
                sqlComm.Parameters.AddWithValue("@ccKardexSatr", ccKardexSatr);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter daAmarNamehVaset = new SqlDataAdapter();
                daAmarNamehVaset.SelectCommand = sqlComm;
                daAmarNamehVaset.SelectCommand.CommandTimeout = 9999999;
                daAmarNamehVaset.Fill(dtAmarNamehVaset);

                oe.EventTime = Convert.ToDateTime(dtAmarNamehVaset.Rows[0]["EventTime"]);

                oe.Action = dtAmarNamehVaset.Rows[0]["action"].ToString();
                oe.BizStep = dtAmarNamehVaset.Rows[0]["BizStep"].ToString();
                oe.Disposition = dtAmarNamehVaset.Rows[0]["Disposition"].ToString();
                // oe.EventID = dtTitrSatrTTAC.Rows[0]["EventID"].ToString();
                Guid EventId;
                Guid.TryParse(dtAmarNamehVaset.Rows[0]["EventId"].ToString(), out EventId);
                // Guid.TryParse("617D64D8-02CA-4084-8B76-7E7EEBE006B0", out EventId);

                oe.EventID = EventId;
                rp.Id = dtAmarNamehVaset.Rows[0]["ReadPointId"].ToString();
                oe.ReadPoint = rp;

                
                if (oe.BizStep != "shipping")
                {
                    BL.Id = dtAmarNamehVaset.Rows[0]["bizLocationId"].ToString();

                    ////Resid Az TaminKonandeh /Dar Resid Az Tamin konandeh Khat zir Niaz Ast
                    oe.BizLocation = BL;
                }

                
                if (oe.BizStep == "shipping")
                {
                    BL.Id = "";
                }


                //<MALEK>
                if (dtAmarNamehVaset.Rows[0]["CodeNoeForm"].ToString() != "7" )
                {
                    BL.Id = dtAmarNamehVaset.Rows[0]["bizLocationId"].ToString();
                    oe.BizLocation = BL;
                }
                else
                {
                    BL.Id = "";
                }
                //</MALEK>



                d.Id = dtAmarNamehVaset.Rows[0]["destinationListDestination"].ToString();
                d.Type = dtAmarNamehVaset.Rows[0]["destinationListDestinationType"].ToString();
                oe.Extension = oEventExtension;


                //<MALEK>
                //if (oe.BizStep != "stock_taking" && oe.BizStep != "Cycle_counting")
                //{
                //    oe.Extension.SourceList = new Source[] { new Source { Id = dtAmarNamehVaset.Rows[0]["extensionSourceListSource"].ToString(), Type = "location" } };
                //    oe.Extension.DestinationList = new Destination[] { new Destination { Id = dtAmarNamehVaset.Rows[0]["destinationListDestination"].ToString(), Type = "location" } };// dtTitr.Rows[0]["destinationListDestinationType"].ToString() } };
                //}


                if (dtAmarNamehVaset.Rows[0]["CodeNoeForm"].ToString() != "7" && dtAmarNamehVaset.Rows[0]["CodeNoeForm"].ToString() != "8" )
                {
                    oe.Extension.SourceList = new Source[] { new Source { Id = dtAmarNamehVaset.Rows[0]["extensionSourceListSource"].ToString(), Type = "location" } };
                    oe.Extension.DestinationList = new Destination[] { new Destination { Id = dtAmarNamehVaset.Rows[0]["destinationListDestination"].ToString(), Type = "location" } };
                }                 
                //</MALEK>





                var quantityList = new List<QuantityElement>();
                quantityList.Add(new QuantityElement { IRC = dtAmarNamehVaset.Rows[0]["QuantityElementilmdIRC"].ToString(), EpcClass = dtAmarNamehVaset.Rows[0]["quantityElementepcClass"].ToString(), Quantity = dtAmarNamehVaset.Rows[0]["quantityElementQuantity"].ToString(), ILMD = new Ilmd { LotNumber = dtAmarNamehVaset.Rows[0]["QuantityElementIlmdlotNumber"].ToString(), MD = Convert.ToDateTime(dtAmarNamehVaset.Rows[0]["QuantityElementIlmdMD"]), XD = Convert.ToDateTime(dtAmarNamehVaset.Rows[0]["QuantityElementIlmdXD"]) }, UOM = dtAmarNamehVaset.Rows[0]["quantityElementuom"].ToString() });

                oe.Extension.QuantityList = quantityList.ToArray();
            }
            return oe;
        }

        public void PersianFilterGrid()
        {
            GridFilterMenu menu = MainGrid.FilterMenu;
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

        public bool isValidoperation()
        {
            Boolean operation = false;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;



            DataTable dtoperation = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("OprationTTAC", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daoperation = new SqlDataAdapter();
                daoperation.SelectCommand = sqlComm;
                daoperation.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                daoperation.Fill(dtoperation);
            }
            if (Convert.ToInt32(dtoperation.Rows[0]["flagOpration"].ToString()) == 0)
            {
                operation = true;

            }



            return operation;

        }

        public bool isValidLastOpration()
        {
            Boolean operationLastOpration = false;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;



            DataTable dtLastOpration = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlCommLastOpration = new SqlCommand("LastOpration", conn);
                sqlCommLastOpration.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daLastOpration = new SqlDataAdapter();
                daLastOpration.SelectCommand = sqlCommLastOpration;
                daLastOpration.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                daLastOpration.Fill(dtLastOpration);
            }


            if (dtLastOpration.Rows.Count == 0)
            {
                operationLastOpration = true;

            }




            return operationLastOpration;

        }

        public bool isValidStatus_concurrency()
        {
            Boolean ValidStatus_concurrency = false;
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;



            //DataSet ds = new DataSet("DataSet");
            DataTable dtStatus_concurrency = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("Status_concurrency", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daStatus_concurrency = new SqlDataAdapter();
                daStatus_concurrency.SelectCommand = sqlComm;
                daStatus_concurrency.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                daStatus_concurrency.Fill(dtStatus_concurrency);
            }
            if (dtStatus_concurrency.Rows.Count == 0)
            {
                ValidStatus_concurrency = true;

            }

            return ValidStatus_concurrency;


        }
        public void OprationStart()
        {

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;



            //DataSet ds = new DataSet("DataSet");
            DataTable dtOprationStart = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("OprationStartTTAC", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daOprationStart = new SqlDataAdapter();
                daOprationStart.SelectCommand = sqlComm;
                daOprationStart.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                daOprationStart.Fill(dtOprationStart);

            }



        }

        public void OprationEnd()
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;



            //DataSet ds = new DataSet("DataSet");
            DataTable dtStatus_concurrency = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand sqlComm = new SqlCommand("OprationEndTTAC", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter daStatus_concurrency = new SqlDataAdapter();
                daStatus_concurrency.SelectCommand = sqlComm;
                daStatus_concurrency.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                daStatus_concurrency.Fill(dtStatus_concurrency);
            }


        }
        public bool isValid()
        {

            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");




            Boolean Valid = true;
            //var b = txtTaTarikh.Text;
            //var a = Convert.ToInt64(txtTaTarikh.Text);
            if (String.IsNullOrEmpty(txtAzTarikh.Text) || String.IsNullOrWhiteSpace(txtTaTarikh.Text))
            {
                Valid = false;
            }
            //if (Convert.ToInt32(TaTarikh) - Convert.ToInt32(AzTarikh) >30)

            //{
            //    Valid = false;
            //}
            return Valid;

        }

        public bool isValidReport()
        {
            Boolean Valid = true;

            if (String.IsNullOrEmpty(txtAzTarikhForm.Text) && String.IsNullOrEmpty(txtTaTarikhForm.Text)
            && String.IsNullOrEmpty(txtAzTarikhErsal.Text) && String.IsNullOrEmpty(txtTaTarikhErsal.Text))
            {
                Valid = false;
            }
            return Valid;

        }

        public void Update()
        {

            try
            {
                #region Update

                CallbackService cs = new CallbackService();
                StatisticsServiceClient client = new StatisticsServiceClient();
                Authentication au = new Authentication();
                EpcisEvent ee = new EpcisEvent();
                ObjectEvent c = new ObjectEvent();
                DataTable dtKardex = new DataTable();
                SqlDataAdapter daUpdate = new SqlDataAdapter();



                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
                var conString2 = mySetting.ConnectionString;
                SqlCommand cm = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlConnection cn = new SqlConnection(conString2);
                SqlParameter p = null;
                DataTable dt = null;
                string strSQL = String.Empty;


                strSQL = "select * from TTACConfig with (nolock)";
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







                    //     Pakhsh.ttacForUpdateDataTable dtForUpdate = new Pakhsh.ttacForUpdateDataTable();
                    //  Models.PakhshTableAdapters.ttacForUpdateTableAdapter daForUpdate = new ttacForUpdateTableAdapter();

                    au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                    au.Password = dt.Rows[0]["TTAC_Password"].ToString();
                    string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
                    string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
                    string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
                    string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");




                    DataTable dtForUpdate = new DataTable();
                    SqlConnection conn = new SqlConnection(conString2);
                    SqlDataAdapter daForUpdate = new SqlDataAdapter();
                    SqlCommand sqlCommForUpdate = new SqlCommand("ttacForUpdate", conn);
                    sqlCommForUpdate.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                    sqlCommForUpdate.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);

                    sqlCommForUpdate.CommandType = CommandType.StoredProcedure;
                    daForUpdate.SelectCommand = sqlCommForUpdate;
                    daForUpdate.SelectCommand.CommandTimeout = 9999999;
                    daForUpdate.Fill(dtForUpdate);





                    try
                    {
                        #region ReadSatr
                        //  daForUpdate.Fill(dtForUpdate, AzTarikh, TaTarikh);

                        for (int i = 0; i < dtForUpdate.Rows.Count; i++)
                        {

                            //  conn.Open();
                            //SqlTransaction transaction;
                            //transaction = conn.BeginTransaction("aa");

                            Guid guid = new Guid(dtForUpdate.Rows[i]["UUID"].ToString());
                            //   Guid guid = new Guid("791fa5a1-f4e7-4c12-b916-980cf8ad01a2");
                            
                            var result = client.GetCaptureStatusWithResponse(guid, au);

                           
                            //  Models.PakhshTableAdapters.TTAC daStatus = new TTAC();
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
                            conn.Close();
                            #endregion
                        }

                    }
                    catch (Exception e)
                    {
                        var a = e.Message;
                        // transaction.Rollback("aa");
                        conn.Close();
                        string script = "alert(\"خطا در وب سرویس بروزرسانی دوباره بروزرسانی کنید\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                                      "ServerControlScript", script, true);
                    }




                }

                #endregion
            }




            catch (Exception e)
            {
                string script = "alert(\"خطا در بروزرسانی دوباره بروزرسانی کنید\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                var a = e.Message;
                var b = e.Message;


            }
        }

        public void UpdateDarSaf()
        {

            try
            {
                #region UpdateUpdateDarSaf

                CallbackService cs = new CallbackService();
                StatisticsServiceClient client = new StatisticsServiceClient();
                Authentication au = new Authentication();
                EpcisEvent ee = new EpcisEvent();
                ObjectEvent c = new ObjectEvent();
                DataTable dtKardex = new DataTable();
                SqlDataAdapter daUpdate = new SqlDataAdapter();



                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
                var conString2 = mySetting.ConnectionString;
                SqlCommand cm = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlConnection cn = new SqlConnection(conString2);
                SqlParameter p = null;
                DataTable dt = null;
                string strSQL = String.Empty;


                strSQL = "select * from TTACConfig with (nolock)";
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
                    string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
                    string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
                    string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
                    string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");




                    DataTable dtForUpdateDarSaf = new DataTable();
                    SqlConnection conn = new SqlConnection(conString2);
                    SqlDataAdapter daForUpdateDarSaf = new SqlDataAdapter();
                    SqlCommand sqlCommForUpdateDarSaf = new SqlCommand("ttacForUpdateDarSaf", conn);
                    //sqlCommForUpdateDarSaf.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                    //sqlCommForUpdateDarSaf.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);

                    sqlCommForUpdateDarSaf.CommandType = CommandType.StoredProcedure;
                    daForUpdateDarSaf.SelectCommand = sqlCommForUpdateDarSaf;
                    daForUpdateDarSaf.SelectCommand.CommandTimeout = 9999999;
                    daForUpdateDarSaf.Fill(dtForUpdateDarSaf);





                    try
                    {
                        #region ReadSatr
                        //  daForUpdate.Fill(dtForUpdate, AzTarikh, TaTarikh);

                        for (int i = 0; i < dtForUpdateDarSaf.Rows.Count; i++)
                        {

                            //  conn.Open();
                            //SqlTransaction transaction;
                            //transaction = conn.BeginTransaction("aa");

                            Guid guid = new Guid(dtForUpdateDarSaf.Rows[i]["UUID"].ToString());
                            //   Guid guid = new Guid("791fa5a1-f4e7-4c12-b916-980cf8ad01a2");
                            
                            var result = client.GetCaptureStatusWithResponse(guid, au);
                            //  Models.PakhshTableAdapters.TTAC daStatus = new TTAC();
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
                            conn.Close();
                            #endregion
                        }

                    }
                    catch (Exception e)
                    {
                        var a = e.Message;
                        UpdateDarSaf();

                        // transaction.Rollback("aa");
                        //  conn.Close();
                        //string script = "alert(\"خطا در وب سرویس بروزرسانی دوباره بروزرسانی کنید\");";
                        //ScriptManager.RegisterStartupScript(this, GetType(),
                        //                              "ServerControlScript", script, true);
                    }




                }

                #endregion
            }




            catch (Exception e)
            {
                //string script = "alert(\"خطا در وب سرویس بروزرسانی دوباره بروزرسانی کنید\");";
                //ScriptManager.RegisterStartupScript(this, GetType(),
                //                              "ServerControlScript", script, true);
                var a = e.Message;
                var b = e.Message;
                UpdateDarSaf();

            }
        }
        private DataTable GetData(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            String strConnString = System.Configuration.ConfigurationManager.
                 ConnectionStrings["conString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }
        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {



            if (isValid())
            {


            }
            else
            {

                string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }

        }

        protected void btnShow_Click(object sender, ImageClickEventArgs e)
        {


            if (isValid())
            {

                SelectListItem();

            }
            else
            {

                string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }


        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {


            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString22 = mySetting.ConnectionString;
            SqlConnection cn = new SqlConnection(conString22);




            cn.Open();
            IDbTransaction tran = cn.BeginTransaction();

            try
            {

                #region Update
                //if (isValidLastOpration())
                //{


                //if (isValidoperation())
                //{
                if (isValid())
                {

                    OprationStart();
                    Update();
                    OprationEnd();

                }
                else
                {

                    string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }
                //     }
                //else
                //{
                //    string script = "alert(\"برنامه در حال تبادل اطلاعات می باشد\");";
                //    ScriptManager.RegisterStartupScript(this, GetType(),
                //                                  "ServerControlScript", script, true);
                //}
                //  }

                //else
                //{
                //    string script = "alert(\"اطلاعات در حال ارسال می باشند در صورتی میتوان دوباره اطلاعات را ارسال کرد که عملیات قبلی بصورت کامل تکمیل شده باشد\");";
                //    ScriptManager.RegisterStartupScript(this, GetType(),
                //                                  "ServerControlScript", script, true);
                //}
                #endregion

                tran.Commit();


            }
            catch (Exception)
            {

                try
                {
                    tran.Rollback();
                }
                catch (Exception)
                {

                    throw;
                }


                string script = "alert(\"در تبادل اطلاعات خطا رخ داده با پشتیبانی تماس فرمایید\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }
            finally
            {
                cn.Close();
            }


        }


        protected void btnSend_Click(object sender, ImageClickEventArgs e)
        {

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString22 = mySetting.ConnectionString;
            SqlConnection cn = new SqlConnection(conString22);
            cn.Open();
            IDbTransaction tran = cn.BeginTransaction();

            try
            {

                if (isValidLastOpration())
                {
                    if (isValid())
                    {
                        if (isValidoperation())
                        {

                            #region Send
                            btnSend.Visible = false;
                            btnSend.Enabled = false;

                            if (isValidoperation())
                            {


                                if (isValid())
                                {

                                    if (isValidStatus_concurrency())
                                    {

                                        if (isValidoperation())
                                        {

                                            OprationStart();
                                            //    Update();
                                            SelectListItemSend();
                                            OprationEnd();

                                        }


                                    }
                                    else
                                    {
                                        string script = "alert(\"در سیستم  اطلاعاتی باوضعیت ارسال می باشد ابتدا باید برزرسانی انجام دهید\");";
                                        ScriptManager.RegisterStartupScript(this, GetType(),
                                                                      "ServerControlScript", script, true);
                                    }

                                    btnSend.Visible = true;
                                    btnSend.Enabled = true;

                                }
                                else
                                {

                                    string script = "alert(\" وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                                  "ServerControlScript", script, true);
                                }
                            }
                            else
                            {
                                string script = "alert(\"برنامه در حال تبادل اطلاعات می باشد\");";
                                ScriptManager.RegisterStartupScript(this, GetType(),
                                                              "ServerControlScript", script, true);
                            }
                            #endregion
                            tran.Commit();
                        }
                        else
                        {
                            string script = "alert(\"اطلاعات در حال ارسال می باشند در صورتی میتوان دوباره اطلاعات را ارسال کرد که عملیات قبلی بصورت کامل تکمیل شده باشد\");";
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                                          "ServerControlScript", script, true);

                        }
                    }

                    else
                    {
                        string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                                      "ServerControlScript", script, true);

                    }


                }
                else
                {
                    string script = "alert(\"اطلاعات در حال ارسال می باشند در صورتی میتوان دوباره اطلاعات را ارسال کرد که عملیات قبلی بصورت کامل تکمیل شده باشد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);

                }

            }
            catch (Exception E)
            {

                try
                {
                    tran.Rollback();
                    var A = E.Message;
                    string script = "alert(\"در تبادل اطلاعات خطا رخ داده با پشتیبانی تماس فرمایید\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }
                catch (Exception)
                {

                    throw;
                }



            }
            finally
            {
                cn.Close();
            }





        }


        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            if (isValid())
            {
                SelectListItemRefresh();

            }
            else
            {

                string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }
        }


        public void SaveWhere(string strMarkazPakhsh, string strResid, string streHavaleh, string strKalaGoroh, string strTaminKonandeh, string strBrand)
        {
            if (rbSave.Checked)
            {
                WhereMarkazPakhsh = strMarkazPakhsh;
                WhereResid = strResid;
                WhereHavaleh = streHavaleh;
                WhereKalaGoroh = strKalaGoroh;
                WhereTaminKonandeh = strTaminKonandeh;
                WhereBrand = strBrand;
                DataSet dsSave = new DataSet("DataSet");
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
                var conString = mySetting.ConnectionString;

                SqlDataAdapter da = new SqlDataAdapter();
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    SqlCommand sqlCommSave = new SqlCommand("InsertWhereGHazaDarou", conn);
                    sqlCommSave.Parameters.AddWithValue("@MarkazPakhsh", strMarkazPakhsh);
                    sqlCommSave.Parameters.AddWithValue("@Resid", strResid);
                    sqlCommSave.Parameters.AddWithValue("@Havaleh", streHavaleh);
                    sqlCommSave.Parameters.AddWithValue("@KalaGoroh", strKalaGoroh);
                    sqlCommSave.Parameters.AddWithValue("@TaminKonandeh", strTaminKonandeh);
                    sqlCommSave.Parameters.AddWithValue("@Brand", strBrand);


                    sqlCommSave.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = sqlCommSave;
                    da.SelectCommand.CommandTimeout = 9999999;
                    da.Fill(dsSave);

                }
            }






        }

        public void ShowSaveWhere()
        {
            //if (rbRepeat.Checked)
            //{

            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            Pakhsh.WhereGHazaDarouDataTable dtWhereGHazaDarou = new Pakhsh.WhereGHazaDarouDataTable();
            Models.PakhshTableAdapters.WhereGHazaDarouTableAdapter daWhereGHazaDarou = new WhereGHazaDarouTableAdapter();
            daWhereGHazaDarou.Fill(dtWhereGHazaDarou);


            if (dtWhereGHazaDarou.Rows.Count > 0)
            {
                foreach (DataRow row in dtWhereGHazaDarou.Rows)
                {
                    string ShowWhereMarkazPakhsh = row["MarkazPakhsh"].ToString();
                    string ShowWhereResid = row["Resid"].ToString();
                    string ShowWhereHavaleh = row["Havaleh"].ToString();
                    string ShowWhereKalaGoroh = row["KalaGoroh"].ToString();
                    string ShowWhereTaminKonandeh = row["TaminKonandeh"].ToString();
                    string ShowWhereBrand = row["Brand"].ToString();





                    if (String.IsNullOrEmpty(ShowWhereMarkazPakhsh))
                    {
                        lblMarkazPakhsh.Text = "تمامی مراکز پخش";
                    }
                    else
                    {
                        #region ShowWhereMarkazPakhsh
                        DataTable dtMarkazPakhsh = new DataTable();
                        string[] strShowWhereMarkazPakhsh = ShowWhereMarkazPakhsh.Split(',');
                        var forlabel = "";
                        for (int i = 0; i < strShowWhereMarkazPakhsh.Length; i++)
                        {
                            var a = strShowWhereMarkazPakhsh[i].ToString();

                            SqlDataAdapter da = new SqlDataAdapter();
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                SqlCommand sqlCommShowSaveWhere = new SqlCommand("ReadMarkazPakhshBystrccMarkazPakhsh", conn);
                                sqlCommShowSaveWhere.Parameters.AddWithValue("@strccMarkazPakhsh", a);

                                sqlCommShowSaveWhere.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = sqlCommShowSaveWhere;
                                da.SelectCommand.CommandTimeout = 9999999;
                                da.Fill(dtMarkazPakhsh);
                                foreach (DataRow rowdtSave in dtMarkazPakhsh.Rows)
                                {
                                    forlabel = forlabel + " , " + rowdtSave["NameMarkazPakhsh"].ToString();
                                }
                                dtMarkazPakhsh.Clear();
                            }
                        }

                        lblMarkazPakhsh.Text = forlabel;

                        #endregion
                    }
                    if (String.IsNullOrEmpty(ShowWhereResid))
                    {
                        lblVorodi.Text = "تمامی ورودی های انبار";
                    }
                    else
                    {
                        #region ShowWhereResid
                        DataTable dtResid = new DataTable();
                        string[] strShowWhereResid = ShowWhereResid.Split(',');
                        var forlabel = "";
                        for (int i = 0; i < strShowWhereResid.Length; i++)
                        {
                            var a = strShowWhereResid[i].ToString();

                            SqlDataAdapter da = new SqlDataAdapter();
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                SqlCommand sqlCommShowSaveWhere = new SqlCommand("ReadVorodiBystrCodeNoeForm", conn);
                                sqlCommShowSaveWhere.Parameters.AddWithValue("@strCodeNoeForm", a);

                                sqlCommShowSaveWhere.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = sqlCommShowSaveWhere;
                                da.SelectCommand.CommandTimeout = 9999999;
                                da.Fill(dtResid);
                                foreach (DataRow rowdtSave in dtResid.Rows)
                                {
                                    forlabel = forlabel + " , " + rowdtSave["txtNoeForm"].ToString();
                                }
                                dtResid.Clear();
                            }
                        }

                        lblVorodi.Text = forlabel;

                        #endregion
                    }
                    if (String.IsNullOrEmpty(ShowWhereHavaleh))
                    {
                        lblKhoroji.Text = "تمامی خروجی های انبار";
                    }
                    else
                    {
                        #region ShowWhereHavaleh
                        DataTable dtHavaleh = new DataTable();
                        string[] strShowWhereHavaleh = ShowWhereHavaleh.Split(',');
                        var forlabel = "";
                        for (int i = 0; i < strShowWhereHavaleh.Length; i++)
                        {
                            var a = strShowWhereHavaleh[i].ToString();

                            SqlDataAdapter da = new SqlDataAdapter();
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                SqlCommand sqlCommShowSaveWhere = new SqlCommand("ReadKhorojiByCodeNoeForm", conn);
                                sqlCommShowSaveWhere.Parameters.AddWithValue("@strCodeNoeForm", a);

                                sqlCommShowSaveWhere.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = sqlCommShowSaveWhere;
                                da.SelectCommand.CommandTimeout = 9999999;
                                da.Fill(dtHavaleh);
                                foreach (DataRow rowdtSave in dtHavaleh.Rows)
                                {
                                    forlabel = forlabel + " , " + rowdtSave["txtNoeForm"].ToString();
                                }
                                dtHavaleh.Clear();
                            }
                        }

                        lblKhoroji.Text = forlabel;

                        #endregion

                    }

                    if (String.IsNullOrEmpty(ShowWhereKalaGoroh))
                    {
                        lblKalaGoroh.Text = "تمامی گروه های کالا";
                    }
                    else
                    {
                        #region ShowKalaGoroh
                        DataTable dtKalaGoroh = new DataTable();
                        string[] strShowKalaGoroh = ShowWhereKalaGoroh.Split(',');
                        var forlabel = "";
                        for (int i = 0; i < strShowKalaGoroh.Length; i++)
                        {
                            var a = strShowKalaGoroh[i].ToString();

                            SqlDataAdapter da = new SqlDataAdapter();
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                SqlCommand sqlCommShowSaveWhere = new SqlCommand("NameGorohKalaByccGoroh", conn);
                                sqlCommShowSaveWhere.Parameters.AddWithValue("@strccGoroh", a);

                                sqlCommShowSaveWhere.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = sqlCommShowSaveWhere;
                                da.SelectCommand.CommandTimeout = 9999999;
                                da.Fill(dtKalaGoroh);
                                foreach (DataRow rowdtSave in dtKalaGoroh.Rows)
                                {
                                    forlabel = forlabel + " , " + rowdtSave["NameGoroh"].ToString();
                                }
                                dtKalaGoroh.Clear();
                            }
                        }

                        lblKalaGoroh.Text = forlabel;

                        #endregion
                    }

                    if (String.IsNullOrEmpty(ShowWhereTaminKonandeh))
                    {
                        lblTaminKonandeh.Text = "تمامی تامین کنندگان";
                    }
                    else
                    {
                        #region ShowWhereTaminKonandeh
                        DataTable dtTaminKonandehShow = new DataTable();
                        string[] strShowWhereTaminKonandeh = ShowWhereTaminKonandeh.Split(',');
                        var forlabel = "";
                        for (int i = 0; i < strShowWhereTaminKonandeh.Length; i++)
                        {
                            var a = strShowWhereTaminKonandeh[i].ToString();

                            SqlDataAdapter da = new SqlDataAdapter();
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                SqlCommand sqlCommShowSaveWhere = new SqlCommand("ReadTaminKonandehByccTaminKonandeh", conn);
                                sqlCommShowSaveWhere.Parameters.AddWithValue("@strccTaminKonandeh", a);

                                sqlCommShowSaveWhere.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = sqlCommShowSaveWhere;
                                da.SelectCommand.CommandTimeout = 9999999;
                                da.Fill(dtTaminKonandehShow);
                                foreach (DataRow rowdtSave in dtTaminKonandehShow.Rows)
                                {
                                    forlabel = forlabel + " , " + rowdtSave["NameTaminKonandeh"].ToString();
                                }
                                dtTaminKonandehShow.Clear();
                            }
                        }

                        lblTaminKonandeh.Text = forlabel;

                        #endregion

                    }

                    if (String.IsNullOrEmpty(ShowWhereBrand))
                    {
                        lblBrand.Text = "تمامی برند ها";

                    }
                    else
                    {

                        #region ShowWhereBrand
                        DataTable dtShowWhereBrand = new DataTable();
                        string[] strShowWhereBrand = ShowWhereBrand.Split(',');
                        var forlabel = "";
                        for (int i = 0; i < strShowWhereBrand.Length; i++)
                        {
                            var a = strShowWhereBrand[i].ToString();

                            SqlDataAdapter da = new SqlDataAdapter();
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                SqlCommand sqlCommShowSaveWhere = new SqlCommand("ReadBrandByccBrand", conn);
                                sqlCommShowSaveWhere.Parameters.AddWithValue("@strccBrand", a);

                                sqlCommShowSaveWhere.CommandType = CommandType.StoredProcedure;
                                da.SelectCommand = sqlCommShowSaveWhere;
                                da.SelectCommand.CommandTimeout = 9999999;
                                da.Fill(dtShowWhereBrand);
                                foreach (DataRow rowdtSave in dtShowWhereBrand.Rows)
                                {
                                    forlabel = forlabel + " , " + rowdtSave["NameBrand"].ToString();
                                }
                                dtShowWhereBrand.Clear();
                            }
                        }

                        lblBrand.Text = forlabel;

                        #endregion

                    }


                }
            }

        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {



            if (isValidReport())
            {

                if (Session["UserName"] == null)
                {
                    Server.Transfer("~/Pages/Login.aspx");
                }

                string txtAzTarikhErsalWs = this.txtAzTarikhErsal.Text;
                string txtTaTarikhErsalWs = this.txtTaTarikhErsal.Text;
                string txtAzTarikhFormWs = this.txtAzTarikhForm.Text;
                string txtTaTarikhFormWs = this.txtTaTarikhForm.Text;
                string txtAzTarikhErsal = "";
                string txtTaTarikhErsal = "";
                string txtAzTarikhForm = "";
                string txtTaTarikhForm = "";
                if (txtAzTarikhErsalWs.Length > 0)
                {
                    txtAzTarikhErsal = txtAzTarikhErsalWs.Replace("/", "");
                }

                if (txtTaTarikhErsalWs.Length > 0)
                {
                    txtTaTarikhErsal = txtTaTarikhErsalWs.Replace("/", "");

                }
                if (txtAzTarikhFormWs.Length > 0)
                {
                    txtAzTarikhForm = txtAzTarikhFormWs.Replace("/", "");
                }
                if (txtTaTarikhFormWs.Length > 0)
                {
                    txtTaTarikhForm = txtTaTarikhFormWs.Replace("/", "");
                }


                // Response.Redirect("qry_RptFn_MoghayesehKol.aspx?txtAzTarikh=" + txtAzTarikh + " &txtTaTarikh=" + txtTaTarikh + "&ccCompany=" + selectCompany);
                Response.Redirect("Report.aspx?txtAzTarikhErsal=" + txtAzTarikhErsal + "&txtTaTarikhErsal=" + txtTaTarikhErsal
                    + "&txtAzTarikhForm=" + txtAzTarikhForm + "&txtTaTarikhForm=" + txtTaTarikhForm);
            }
            else
            {
                string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);

            }




        }

        protected void btnExcel_Click1(object sender, ImageClickEventArgs e)
        {

            if (isValid() == true)
            {
                string Resid = "";
                string Havaleh = "";
                string TaminKonandeh = "";
                string KalaGoroh = "";
                string MarkazPakhsh = "";
                string MoshtaryGoroh = "";
                string Brand = "";
                string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
                string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
                string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
                string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");

                #region ListResid
                string chkListResid = "";
                //  var q= ListResid.Items.FindAll()
                foreach (RadListBoxItem item in ListResid.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectlist = item.Value;
                        chkListResid += selectlist + ",";
                    }

                }
                if (chkListResid.Length > 0)
                {
                    var lenListResid = chkListResid.Length - 1;
                    var selectListResid = (chkListResid.Substring(0, lenListResid));
                    Resid = selectListResid;
                }


                #endregion

                #region ListHavaleh

                string chkListHavaleh = "";
                foreach (RadListBoxItem item1 in ListHavaleh.Items)
                {


                    if (item1.Checked == true)
                    {
                        string selectlist = item1.Value;
                        chkListHavaleh += selectlist + ",";
                    }

                }
                if (chkListHavaleh.Length > 0)
                {
                    var lenListHavaleh = chkListHavaleh.Length - 1;
                    var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                    Havaleh = selectListHavaleh;
                }

                //if (lenListHavaleh <= 0)
                //{

                //    foreach (RadListBoxItem item in ListHavaleh.Items)
                //    {
                //        ListHavaleh.CheckBoxes = item.Checked;
                //        string selectlistHavaleh = item.Value;
                //        chkListHavaleh += selectlistHavaleh + ",";
                //    }

                //}

                #endregion

                #region ListTaminKonandeh
                string chkListTaminKonandeh = "";
                foreach (RadListBoxItem item in ListTaminKonandeh.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectlist = item.Value;
                        chkListTaminKonandeh += selectlist + ",";
                    }

                }
                if (chkListTaminKonandeh.Length > 0)
                {
                    var lenListTaminKonandeh = chkListTaminKonandeh.Length - 1;
                    var selectListTaminKonandeh = (chkListTaminKonandeh.Substring(0, lenListTaminKonandeh));
                    TaminKonandeh = selectListTaminKonandeh;
                }

                //if (lenListTaminKonandeh <= 0)
                //{

                //    foreach (RadListBoxItem item in ListTaminKonandeh.Items)
                //    {
                //        ListTaminKonandeh.CheckBoxes = item.Checked;
                //        string selectListTaminKonandeh = item.Value;
                //        chkListTaminKonandeh += selectListTaminKonandeh + ",";
                //    }

                //}

                #endregion

                #region ListKalaGoroh
                string chkListKalaGoroh = "";
                foreach (RadListBoxItem item in ListKalaGoroh.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectlist = item.Value;
                        chkListKalaGoroh += selectlist + ",";
                    }

                }
                if (chkListKalaGoroh.Length > 0)
                {
                    var lenListKalaGoroh = chkListKalaGoroh.Length - 1;
                    var selectListKalaGoroh = (chkListKalaGoroh.Substring(0, lenListKalaGoroh));
                    KalaGoroh = selectListKalaGoroh;
                }

                //if (lenListKalaGoroh <= 0)
                //{

                //    foreach (RadListBoxItem item in ListKalaGoroh.Items)
                //    {
                //        ListKalaGoroh.CheckBoxes = item.Checked;
                //        string selectListKalaGoroh = item.Value;
                //        chkListKalaGoroh += selectListKalaGoroh + ",";
                //    }

                //}

                #endregion

                #region ListMarkazPakhsh
                string chkListMarkazPakhsh = "";
                foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectList = item.Value;
                        chkListMarkazPakhsh += selectList + ",";
                    }

                }
                if (chkListMarkazPakhsh.Length > 0)
                {
                    var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                    var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                    MarkazPakhsh = selectListMarkazPakhsh;
                }


                #endregion

                #region ListMoshtaryGoroh
                string chkListMoshtaryGoroh = "";

                /*
                            foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                            {
                                if (item.Checked == true)
                                {
                                    string selectList = item.Value;
                                    chkListMoshtaryGoroh += selectList + ",";
                                }

                            }
                            if (chkListMoshtaryGoroh.Length > 0)
                            {
                                var lenListMoshtaryGoroh = chkListMoshtaryGoroh.Length - 1;
                                var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGoroh));
                                MoshtaryGoroh = selectListMoshtaryGoroh;
                            }
                            else
                            {
                                foreach (RadListBoxItem item in ListMoshtaryGoroh.Items)
                                {
                                    ListMoshtaryGoroh.CheckBoxes = item.Checked;
                                    string selectList = item.Value;
                                    chkListMoshtaryGoroh += selectList + ",";
                                }
                                var lenListMoshtaryGorohAll = chkListMoshtaryGoroh.Length - 1;
                                var selectListMoshtaryGoroh = (chkListMoshtaryGoroh.Substring(0, lenListMoshtaryGorohAll));
                                MoshtaryGoroh = selectListMoshtaryGoroh;
                            }


                    */
                #endregion

                #region ListBrand
                string chkListBrand = "";
                string selectListBrand = "";
                foreach (RadListBoxItem item in ListBrand.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectList = item.Value;
                        chkListBrand += selectList + ",";
                    }

                }
                if (chkListBrand.Length > 0)
                {
                    var lenListBrand = chkListBrand.Length - 1;
                    selectListBrand = (chkListBrand.Substring(0, lenListBrand));
                    Brand = selectListBrand;
                }


                #endregion




                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
                var conString = mySetting.ConnectionString;

                //  Models.PakhshTableAdapters.TTAC ttac = new Models.PakhshTableAdapters.TTAC();






                DataSet ds = new DataSet("DataSet");
                // DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(conString))
                {

                    SqlCommand sqlComm = new SqlCommand("ttac_xls", conn);
                    sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                    sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                    sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                    sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                    sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);
                    sqlComm.Parameters.AddWithValue("@strGorohKala", KalaGoroh);
                    sqlComm.Parameters.AddWithValue("@strTaminKonandeh", TaminKonandeh);
                    sqlComm.Parameters.AddWithValue("@strBrand", Brand);
                    // sqlComm.Parameters.AddWithValue("@strGorohMoshtary", MoshtaryGoroh);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    da.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                    da.Fill(ds);
                }








                //string CS = ConfigurationManager.ConnectionStrings["ZarBiConnectionString"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(CS))
                //{
                //    SqlDataAdapter da = new SqlDataAdapter("qry_tblRptFn_ForoshAmar_Gridxls", con); // Using a Store Procedure.
                //    //SqlDataAdapter da = new SqlDataAdapter("SELECT 'this is a test text' as test", con); To use a hard coded query.
                //    da.SelectCommand.CommandType = CommandType.StoredProcedure; // Comment if using hard coded query.
                //    DataSet ds = new DataSet(); // Definition: Memory representation of the database.
                //    da.SelectCommand.Parameters.AddWithValue("@txtAzTarikh", txtAzTarikh); // Repeat for each parameter present in the Store Procedure.
                //    da.SelectCommand.Parameters.AddWithValue("@txtTaTarikh", txtTaTarikh);
                //    da.SelectCommand.Parameters.AddWithValue("@ccCompany", ccCompany);
                //    da.Fill(ds);
                // 
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = ds;
                grid.DataBind();

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=لیست انتخاب شده برای ارسال.xls");
                Response.ContentType = "application/excel";
                Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Write(sw.ToString());

                Response.End();
            }
            else
            {
                string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);

            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (isValidReport())
            {

                string txtAzTarikhErsalWs = this.txtAzTarikhErsal.Text;
                string txtTaTarikhErsalWs = this.txtTaTarikhErsal.Text;
                string txtAzTarikhFormWs = this.txtAzTarikhForm.Text;
                string txtTaTarikhFormWs = this.txtTaTarikhForm.Text;
                string txtAzTarikhErsal = "";
                string txtTaTarikhErsal = "";
                string txtAzTarikhForm = "";
                string txtTaTarikhForm = "";
                int noe = Convert.ToInt32(DDlNoe.SelectedValue.ToString());
                if (txtAzTarikhErsalWs.Length > 0)
                {
                    txtAzTarikhErsal = txtAzTarikhErsalWs.Replace("/", "");
                }

                if (txtTaTarikhErsalWs.Length > 0)
                {
                    txtTaTarikhErsal = txtTaTarikhErsalWs.Replace("/", "");

                }
                if (txtAzTarikhFormWs.Length > 0)
                {
                    txtAzTarikhForm = txtAzTarikhFormWs.Replace("/", "");
                }
                if (txtTaTarikhFormWs.Length > 0)
                {
                    txtTaTarikhForm = txtTaTarikhFormWs.Replace("/", "");
                }


                #region SetGrid
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
                var conString = mySetting.ConnectionString;
                DataSet ds = new DataSet();
                // DataTable dt = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter();
                using (SqlConnection conn = new SqlConnection(conString))
                {

                    SqlCommand sqlComm2 = new SqlCommand("[dbo].[ReportAmarNameh_xls_Radman]", conn);
                    sqlComm2.Parameters.AddWithValue("@AzTarikhErsaliSHamsi", txtAzTarikhErsal);
                    sqlComm2.Parameters.AddWithValue("@TaTarikhErsaliSHamsi", txtTaTarikhErsal);
                    sqlComm2.Parameters.AddWithValue("@AzTarikhFormSHamsi", txtAzTarikhForm);
                    sqlComm2.Parameters.AddWithValue("@TaTarikhFormSHamsi", txtTaTarikhForm);
                    sqlComm2.Parameters.AddWithValue("@Noe", noe);


                    sqlComm2.CommandType = CommandType.StoredProcedure;

                    da2.SelectCommand = sqlComm2;
                    da2.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                    da2.Fill(ds);
                }

                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = ds;
                grid.DataBind();

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=گزارش آمارنامه.xls");
                Response.ContentType = "application/excel";
                Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
                #endregion













            }
            else
            {
                string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);

            }
        }


        public void ShowConfig()
        {


            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;

            Pakhsh.TTACConfigDataTable dtTTACConfig = new Pakhsh.TTACConfigDataTable();
            Models.PakhshTableAdapters.TTACConfigTableAdapter daTTACConfig = new TTACConfigTableAdapter();
            daTTACConfig.Fill(dtTTACConfig);


            if (dtTTACConfig.Rows.Count > 0)
            {
                foreach (DataRow row in dtTTACConfig.Rows)
                {

                    lblUserName.Text = row["TTAC_UserName"].ToString();
                    lblPassword.Text = row["TTAC_Password"].ToString();
                    lblWebService.Text = row["TTAC_WebsSrviceUrl"].ToString();
                }
            }

        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            //if (isValidoperation())
            if (true)
            {
                OprationStart();
                Ebtal();
                OprationEnd();
            }
            else
            {
                string script = "alert(\"برنامه در حال تبادل اطلاعات می باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }



        }
        public void Ebtal()
        {

            string txtAzTarikhWithSlash = this.txtAzTarikh.Text;
            string txtTaTarikhWithSlash = this.txtTaTarikh.Text;
            string AzTarikh = txtAzTarikhWithSlash.Replace("/", "");
            string TaTarikh = txtTaTarikhWithSlash.Replace("/", "");
            int noe = 1;
            //if (rbTarikhErsal.Checked)
            //{
            noe = 2;
            //}
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            DataTable dtSelect = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand cm = new SqlCommand();
                SqlConnection cn = new SqlConnection(conString);
                SqlParameter p = null;
                string strSQL = String.Empty;

                SqlDataAdapter daSelect = new SqlDataAdapter();
                strSQL = "select * from TTACConfig with (nolock)";
                cm.CommandText = strSQL;
                cm.Connection = cn;
                daSelect.SelectCommand = cm;


                dtSelect = new DataTable();

                daSelect.Fill(dtSelect);

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



                string Resid = "";
                string Havaleh = "";
                string MarkazPakhsh = "";


                #region ListResid
                string chkListResid = "";
                //  var q= ListResid.Items.FindAll()
                foreach (RadListBoxItem item in ListResid.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectlist = item.Value;
                        chkListResid += selectlist + ",";
                    }

                }
                if (chkListResid.Length > 0)
                {
                    var lenListResid = chkListResid.Length - 1;
                    var selectListResid = (chkListResid.Substring(0, lenListResid));
                    Resid = selectListResid;
                }


                #endregion

                #region ListHavaleh

                string chkListHavaleh = "";
                foreach (RadListBoxItem item1 in ListHavaleh.Items)
                {


                    if (item1.Checked == true)
                    {
                        string selectlist = item1.Value;
                        chkListHavaleh += selectlist + ",";
                    }

                }
                if (chkListHavaleh.Length > 0)
                {
                    var lenListHavaleh = chkListHavaleh.Length - 1;
                    var selectListHavaleh = (chkListHavaleh.Substring(0, lenListHavaleh));
                    Havaleh = selectListHavaleh;
                }

                //if (lenListHavaleh <= 0)
                //{

                //    foreach (RadListBoxItem item in ListHavaleh.Items)
                //    {
                //        ListHavaleh.CheckBoxes = item.Checked;
                //        string selectlistHavaleh = item.Value;
                //        chkListHavaleh += selectlistHavaleh + ",";
                //    }

                //}

                #endregion

                #region ListMarkazPakhsh
                string chkListMarkazPakhsh = "";
                foreach (RadListBoxItem item in ListMarkazPakhsh.Items)
                {
                    if (item.Checked == true)
                    {
                        string selectList = item.Value;
                        chkListMarkazPakhsh += selectList + ",";
                    }

                }
                if (chkListMarkazPakhsh.Length > 0)
                {
                    var lenListMarkazPakhsh = chkListMarkazPakhsh.Length - 1;
                    var selectListMarkazPakhsh = (chkListMarkazPakhsh.Substring(0, lenListMarkazPakhsh));
                    MarkazPakhsh = selectListMarkazPakhsh;
                }


                #endregion




                DataTable dtEbtal = new DataTable();
                using (SqlConnection connEbtal = new SqlConnection(conString))
                {

                    SqlCommand sqlComm = new SqlCommand("Ebtal", connEbtal);
                    sqlComm.Parameters.AddWithValue("@AzTarikhShamsi", AzTarikh);
                    sqlComm.Parameters.AddWithValue("@TaTarikhShamsi", TaTarikh);
                    sqlComm.Parameters.AddWithValue("@noe", @noe);
                    sqlComm.Parameters.AddWithValue("@strMarkazPakhsh", MarkazPakhsh);
                    sqlComm.Parameters.AddWithValue("@strVorodi", Resid);
                    sqlComm.Parameters.AddWithValue("@StrKHoroji", Havaleh);


                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter daEbtal = new SqlDataAdapter();
                    daEbtal.SelectCommand = sqlComm;
                    daEbtal.SelectCommand.CommandTimeout = 9999999; // default is 30 seconds
                                                                    //  daEbtal.Fill(dtEbtal);
                }


                //if (dtEbtal.Rows.Count != 0)
                //{
                if (true)
                {

                    //for (int i = 0; i < dtEbtal.Rows.Count; i += 2)
                    //{

                    for (int i = 0; i < 1; i++)
                    {

                        Guid guid = new Guid(dtEbtal.Rows[i]["uuid"].ToString());

                        // Guid guid = new Guid("36c74ad5-ec18-48ad-ab12-2acc4849173b");

                        CallbackService callbackService = new CallbackService();
                        var a = client.CancelCaptureWithResponse(guid, au);
                        var result = client.GetCaptureStatusWithResponse(guid, au);
                        string Payam = "";
                        if (result.Message != null)
                        {
                            Payam = result.Message.ToString();
                        }



                        if (a.IsCanceled == true)
                        {
                            DataTable dtInsert = new DataTable();
                            SqlConnection cnInsert = new SqlConnection(conString);
                            SqlDataAdapter daInsert = new SqlDataAdapter();

                            string sql2 = "UPDATE Sales.KardexUUID SET Comment = @Comment,status=100 WHERE uuid=@id";
                            SqlCommand myCommand2 = new SqlCommand(sql2, conn);
                            myCommand2.Parameters.AddWithValue("@Comment", Payam);
                            myCommand2.Parameters.AddWithValue("@id", guid);

                            daInsert.SelectCommand = myCommand2;
                            daInsert.SelectCommand.CommandTimeout = 9999999;
                            daInsert.Fill(dtInsert);


                        }

                    }

                }
                else
                {
                    string script = "alert(\"اطلاعات تکرای یافت نشد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }

            }
        }


        public void EbtalTektari()
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString = mySetting.ConnectionString;
            DataTable dtSelect = new DataTable();
            using (SqlConnection conn = new SqlConnection(conString))
            {

                SqlCommand cm = new SqlCommand();
                SqlConnection cn = new SqlConnection(conString);
                SqlParameter p = null;
                string strSQL = String.Empty;
                string PasswordHash = String.Empty;
                SqlDataAdapter daSelect = new SqlDataAdapter();
                strSQL = "select * from TTACConfig with (nolock)";
                cm.CommandText = strSQL;
                cm.Connection = cn;
                daSelect.SelectCommand = cm;
                dtSelect = new DataTable();
                daSelect.Fill(dtSelect);

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

                //for (int j = 1; j < 70; j++)
                //{
                strResult = "select * from Sales.KardexUUID with(nolock) where ccKardexSatrUUID in (select ccKardexSatrUUID from Sales.KardexUUID  with(nolock) where Status=2 group by ccKardexSatrUUID having count(ccKardexSatrUUID)=2) order by Sales.KardexUUID.ccKardexSatrUUID  ";
                cmResult.CommandText = strResult;
                cmResult.Connection = cnResult;
                daResult.SelectCommand = cmResult;
                dtcnResult = new DataTable();
                daResult.Fill(dtcnResult);
                // callbackService.Url = dtSelect.Rows[0]["TTAC_WebsSrviceUrl"].ToString();

                if (dtcnResult.Rows.Count != 0)
                {


                    for (int i = 0; i < dtcnResult.Rows.Count; i += 2)
                    {



                        Guid guid = new Guid(dtcnResult.Rows[i]["uuid"].ToString());



                        CallbackService callbackService = new CallbackService();
                        var a = client.CancelCaptureWithResponse(guid, au);
                        var result = client.GetCaptureStatusWithResponse(guid, au);
                        string Payam = "";
                        if (result.Message != null)
                        {
                            Payam = result.Message.ToString();
                        }



                        if (a.IsCanceled == true)
                        {
                            DataTable dtInsert = new DataTable();
                            SqlConnection cnInsert = new SqlConnection(conString);
                            SqlDataAdapter daInsert = new SqlDataAdapter();

                            string sql2 = "UPDATE Sales.KardexUUID SET Comment = @Comment,status=100 WHERE uuid=@id";
                            SqlCommand myCommand2 = new SqlCommand(sql2, conn);
                            myCommand2.Parameters.AddWithValue("@Comment", Payam);
                            myCommand2.Parameters.AddWithValue("@id", guid);

                            daInsert.SelectCommand = myCommand2;
                            daInsert.SelectCommand.CommandTimeout = 9999999;
                            daInsert.Fill(dtInsert);


                        }

                    }

                }
                else
                {
                    string script = "alert(\"اطلاعات تکرای یافت نشد\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                                  "ServerControlScript", script, true);
                }


            }
        }

        protected void btnTekrari_Click(object sender, ImageClickEventArgs e)
        {


            if (isValidoperation())
            {
                OprationStart();
                EbtalTektari();
                OprationEnd();
            }
            else
            {
                string script = "alert(\"برنامه در حال تبادل اطلاعات می باشد\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
            }








        }

        protected void btnRepeatVaset_Click(object sender, ImageClickEventArgs e)
        {

            try
            {

                #region SendTekrari
                CallbackService cs = new CallbackService();
                StatisticsServiceClient client = new StatisticsServiceClient();
                Authentication au = new Authentication();
                EpcisEvent ee = new EpcisEvent();
                ObjectEvent ce = new ObjectEvent();
                CallbackService callBack = new CallbackService();


                ConnectionStringSettings mySetting2 = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                if (mySetting2 == null || string.IsNullOrEmpty(mySetting2.ConnectionString))
                    throw new Exception("Fatal error: missing connecting string in web.config file");
                var conString2 = mySetting2.ConnectionString;
                SqlCommand cm = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlConnection cn = new SqlConnection(conString2);
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
                    au.UserName = dt.Rows[0]["TTAC_UserName"].ToString();
                    au.Password = dt.Rows[0]["TTAC_Password"].ToString();
                    callBack.Url = dt.Rows[0]["TTAC_WebsSrviceUrl"].ToString();
                    ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
                    if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                        throw new Exception("Fatal error: missing connecting string in web.config file");
                    var conString = mySetting.ConnectionString;
                    DataTable dtKardexReadVaset = new DataTable();
                    SqlDataAdapter da4 = new SqlDataAdapter();
                    using (SqlConnection conn = new SqlConnection(conString))
                    {
                        SqlCommand sqlCommRead = new SqlCommand("readVasetAmarNameh", conn);
                        sqlCommRead.CommandType = CommandType.StoredProcedure;
                        da4.SelectCommand = sqlCommRead;
                        da4.SelectCommand.CommandTimeout = 9999999;
                        da4.Fill(dtKardexReadVaset);


                        SqlDataAdapter da3 = new SqlDataAdapter();
                        DataTable dtKardex = new DataTable();

                        DataSet dsVaset = new DataSet();
                        da4.Fill(dsVaset);
                        int count = dtKardexReadVaset.Rows.Count;

                        //foreach (var item in dtKardexReadVaset.Rows)
                        //{

                        //}
                        //foreach(DataRow row in table.Rows)

                        //foreach (var item in dsVaset)
                        //{

                        //}


                        //while (dtKardexReadVaset.Rows.Count>=0)
                        //{

                        //}


                        for (int i = count; i > 0; i--)
                        {

                            Int64 cckardexsatr = Convert.ToInt64(dtKardexReadVaset.Rows[i - 1]["ccKardexSatr"]);
                            // ce = ReadFromSQLVaset(Convert.ToInt64(dtKardexReadVaset.Rows[i]["ccKardexSatr"]));
                            ce = ReadFromSQLVaset(cckardexsatr);

                            if (ce != null )
                            {
                                client.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(10);
                                client.Capture(new ObjectEvent[] { ce }, au, callBack);
                            }


                            using (SqlConnection conn1 = new SqlConnection(conString))
                            {
                                DataTable ds = new DataTable();
                                SqlCommand sqlComm2 = new SqlCommand("InsertKardexUUID", conn);
                                sqlComm2.Parameters.AddWithValue("@ccKardex", Convert.ToInt32(dtKardexReadVaset.Rows[i - 1]["ccKardex"].ToString()));
                                sqlComm2.Parameters.AddWithValue("@UUID", ce.EventID.ToString());
                                sqlComm2.Parameters.AddWithValue("@CodeNoeAction", 1);
                                sqlComm2.Parameters.AddWithValue("@Status", 1);
                                sqlComm2.Parameters.AddWithValue("@Comment", " ");
                                sqlComm2.Parameters.AddWithValue("@ccKardexSatr", Convert.ToInt32(dtKardexReadVaset.Rows[i - 1]["ccKardexSatr"].ToString()));

                                sqlComm2.CommandType = CommandType.StoredProcedure;
                                da3.SelectCommand = sqlComm2;
                                da3.SelectCommand.CommandTimeout = 9999999;
                                da3.Fill(ds);


                            }

                            DataTable dtKardexDeleteVaset = new DataTable();
                            SqlDataAdapter daDelete = new SqlDataAdapter();
                            using (SqlConnection conn2 = new SqlConnection(conString))
                            {


                                SqlCommand sqlCommDelete = new SqlCommand("DeletereadVasetAmarNameh", conn);
                                sqlCommDelete.Parameters.AddWithValue("@ccKardexSatr", Convert.ToInt32(dtKardexReadVaset.Rows[i - 1]["ccKardexSatr"].ToString()));
                                sqlCommDelete.CommandType = CommandType.StoredProcedure;
                                daDelete.SelectCommand = sqlCommDelete;
                                daDelete.SelectCommand.CommandTimeout = 9999999;
                                daDelete.Fill(dtKardexDeleteVaset);

                                DataRow dr = dtKardexReadVaset.Rows[i - 1];

                                DataTable test = dtKardexReadVaset;
                                dtKardexReadVaset.Rows.Remove(dr);

                            }

                        }

                        dtKardexReadVaset.Clear();
                        dtKardexReadVaset = null;

                    }
                    #endregion

                }
            }
            catch (Exception tt)
            {
                var a78 = tt.Message;
                string script = "alert(\"خطا در وب سرویس های سازمان\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);


            }
            OprationEnd();
        }

        protected void btnSafPardazesh_Click(object sender, ImageClickEventArgs e)
        {
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString22 = mySetting.ConnectionString;
            SqlConnection cn = new SqlConnection(conString22);




            cn.Open();
            //IDbTransaction tran = cn.BeginTransaction();

            try
            {

                #region Update

                //if (isValid())
                //{

                OprationStart();
                UpdateDarSaf();
                OprationEnd();

                //}
                //else
                //{

                //    string script = "alert(\"وارد کردن اطلاعات تاریخ اجباری می باشد همچنین تاریخ نباید بیشتر از 30 روز باشد\");";
                //    ScriptManager.RegisterStartupScript(this, GetType(),
                //                                  "ServerControlScript", script, true);
                //}

                #endregion

                //  tran.Commit();


            }
            catch (Exception)
            {

                //try
                //{
                //    tran.Rollback();
                //}
                //catch (Exception)
                //{

                //    throw;
                //}

                UpdateDarSaf();

                //string script = "alert(\"خطا در وب سرویس های سازمان\");";
                //ScriptManager.RegisterStartupScript(this, GetType(),
                //                              "ServerControlScript", script, true);
            }
            //finally
            //{
            //    cn.Close();
            //}
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {

            OprationStart();

            ConnectionStringSettings mySetting2 = ConfigurationManager.ConnectionStrings["PakhshConnectionString"];
            if (mySetting2 == null || string.IsNullOrEmpty(mySetting2.ConnectionString))
                throw new Exception("Fatal error: missing connecting string in web.config file");
            var conString2 = mySetting2.ConnectionString;





            DataTable dtDeleteStatus3 = new DataTable();
            SqlDataAdapter daDeleteStatus3 = new SqlDataAdapter();


            using (SqlConnection conn2 = new SqlConnection(conString2))
            {


                SqlCommand sqlCommDeleteStatus3 = new SqlCommand("DeleteStatus3", conn2);
                sqlCommDeleteStatus3.CommandType = CommandType.StoredProcedure;
                daDeleteStatus3.SelectCommand = sqlCommDeleteStatus3;
                daDeleteStatus3.SelectCommand.CommandTimeout = 9999999;
                daDeleteStatus3.Fill(dtDeleteStatus3);

            }

            OprationEnd();
        }

        public void ChangePassword()
        {
            try
            {
                StatisticsServiceClient client = new StatisticsServiceClient();
                Authentication au = new Authentication();
                CallbackService cs = new CallbackService();
                SearchBranchParameter s = new SearchBranchParameter();
                au.UserName = "0000137200014";
                au.Password = "A@123456";
                s.PageNumber = 1;

                var z = client.SearchBranchesWithResponse(s, au);
                var y = client.ChangePasswordWithResponse("Pa@133916", au);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                throw;
            }


        }
    }
}

//}


