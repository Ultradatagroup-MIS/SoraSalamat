<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="GhazaVaDarouFararavand.Pages.Main" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/PersianDatePicker.js"></script>
    <link href="../Content/PersianDatePicker.css" rel="stylesheet" />
    <style>
        h2, h4, h5 {
            color: wheat;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {


        });

        function Visible() {
            alert
        }

    </script>

    <meta name="viewport" content="width=device-width, initial-scale=1" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="MainGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="MainGrid" UpdatePanelCssClass="" LoadingPanelID="RadAjaxLoadingPanel_MainGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>



    <div class="panel panel-primary" style="background-color: #1e326e">
        <div class="panel-heading">
            <h3 style="text-align: center">آمارنامه سازمان غذا و دارو</h3>
            &nbsp;

        </div>
        <div class="panel-body" style="padding:2%" >
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-3">
                            <label style="color: #ffffff">خروجی انبار : </label>
                            &nbsp; 
                    <telerik:RadListBox Height="200px" ID="ListHavaleh" runat="server" CheckBoxes="True" ShowCheckAll="True">
                        <Localization CheckAll="همه خروجی ها" />
                        <ButtonSettings TransferButtons="All"></ButtonSettings>

                    </telerik:RadListBox>
                        </div>
                        <div class="col-md-3">
                            <label style="color: #ffffff">ورودی انبار : </label>
                            &nbsp; 
                     <telerik:RadListBox Height="200px" ID="ListResid" runat="server" CheckBoxes="True" ShowCheckAll="True">
                         <Localization CheckAll="همه ورودی ها" />
                         <ButtonSettings TransferButtons="All"></ButtonSettings>
                     </telerik:RadListBox>
                        </div>
                        <div class="col-md-3">
                            <label style="color: #ffffff">تا تاریخ : </label>
                            &nbsp; 
                
                    <asp:TextBox ID="txtTaTarikh" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>

                            &nbsp; 
                    
                        </div>
                        <div class="col-md-3">
                            <label style="color: #ffffff">از تاریخ : </label>
                            &nbsp;  
                     
                    <asp:TextBox ID="txtAzTarikh" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>

                        </div>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            
                            <asp:ImageButton ID="btnDelete" ImageUrl="~/BootStrap/Image/Delete.png" ToolTip="حذف تمام رکوردهای خطادار" runat="server" OnClick="btnDelete_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:ImageButton ID="btnSafPardazesh" runat="server" ImageUrl="~/BootStrap/Image/ButtonSafPardazesh.png" ToolTip="بروزرسانی در صف پردازش" OnClick="btnSafPardazesh_Click" />
                        </div>
                        <div class="col-md-2">
                             <asp:ImageButton ID="btnRepeatVaset" runat="server" ImageUrl="~/BootStrap/Image/repeatVaset.png" ToolTip="ارسال مجدد" CssClass="img-responsive" OnClick="btnRepeatVaset_Click" />
                        </div>
                        <div class="col-md-2">
                              <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/BootStrap/Image/send.png" CssClass="img-responsive" ToolTip="ارسال اطلاعات" OnClick="btnSend_Click" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/BootStrap/Image/excel.png" CssClass="img-responsive" ToolTip="خروجی اکسل" ViewStateMode="Enabled" OnClick="btnExcel_Click1" />
                        </div>
                        <div class="col-md-2">
                                                <button type="button" data-toggle="modal" data-target="#Filter" style="background-color: transparent; border: none; outline: none">
                        <img src="../BootStrap/Image/Filter.png" alt="فیلتر های اضافه" class="img-responsive" /></button>
                        </div>
                        <div class="col-md-2">
                                                <button type="button" data-toggle="modal" data-target="#Config" style="background-color: transparent; border: none; outline: none" value="شرایط ثبت شده قبلی">
                     <%--   <h5><b>تنظیمات وب سرویس</b></h5>--%>
                        <img src="../BootStrap/Image/Tools64.png" alt="تنظیمات وب سرویس" />
                    </button>
                        </div>
                        <div class="col-md-2">
                            
                                                                      <button type="button" data-toggle="modal" data-target="#Help" style="background-color: transparent; border: none; outline: none">
                        <img src="../BootStrap/Image/Information2.png" alt="راهنما" class="img-responsive" /></button>
                              </div>
                        <div class="col-md-2"></div>
                       

                    </div>
                    
                </div>
                
            </div>
            <div class="row">

                <div class="col-md-1">

                    <%--  <asp:ImageButton ID="BtnHelp" runat="server" ImageUrl="~/BootStrap/Image/Information.png" ToolTip="راهنما" />--%>
                    <%--<asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/BootStrap/Image/excel.png" ToolTip="خروجی اکسل" OnClick="btnExcel_Click" ViewStateMode="Enabled" Visible="False" />--%>
                    <%-- <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="گزارش آمارنامه" ImageUrl="~/BootStrap/Image/see2.png" Width="75%" Height="75%" OnClick="ImageButton1_Click" />--%>
                </div>
                <div class="col-md-1">

                    <%--                                        <button type="button" data-toggle="modal" data-target="#warning" style="background-color: transparent; border: none; outline: none">
                        <img src="../BootStrap/Image/warning.png" alt="لیست خطا" /></button>--%>
                    <%-- <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/BootStrap/Image/warning.png" OnClick="btnRefresh_Click" Visible="False" />--%>
                </div>
                <div class="col-md-1">
                    <asp:ImageButton ID="btnShow" runat="server" ImageUrl="~/BootStrap/Image/Show.png" ToolTip="نمایش" OnClick="btnShow_Click" Visible="false" />

                </div>
                <div class="col-md-1">
                </div>

            </div>
            <br />
            <div class="row">

                <div class="col-md-1">
                    
                </div>
                <div class="col-md-1">
                    <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/BootStrap/Image/see2.png" Width="40%" Height="45" ToolTip="مشاهده آخرین شرایط " />--%>
                   <%-- <button type="button" data-toggle="modal" data-target="#WhereFilter" style="background-color: transparent; border: none; outline: none" value="شرایط ثبت شده قبلی">
                        <h5><b>مشاهده شرایط  قبلی</b></h5>
                        <img src="../BootStrap/Image/See.png" alt="شرایط ثبت شده قبلی" />
                    </button>--%>
                </div>
                <div class="col-md-1">
                    <asp:RadioButton ID="rbSave" runat="server" GroupName="Save&Repeat" Text="ذخیره شرایط ارسال" ForeColor="#ffffff" Visible="false" />
                </div>
                <div class="col-md-1">
                    <asp:RadioButton ID="rbRepeat" runat="server" GroupName="Save&Repeat" Text="آخرین شرایط ارسال شده" ForeColor="#ffffff" Visible="false" />
                </div>

                <div class="col-md-2">
                    <%-- <asp:ImageButton ID="btnSafPardazesh" runat="server" ImageUrl="~/BootStrap/Image/ButtonSafPardazesh.png" ToolTip="ارسال در صف پردازش" OnClick="btnSafPardazesh_Click" />--%>
                    <%--<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/BootStrap/Image/Delete.png" ToolTip="ابطال" OnClick="btnCancel_Click" Visible="False" />--%>
                </div>

                <div class="col-md-1">
                    
                </div>

                <div class="col-md-1">

                    <%--                    <br />
                    <asp:RadioButton ID="rbTarikhForm" ForeColor="#ffffff" runat="server" GroupName="TarikhEbtal" Checked="True" Text="تاریخ فرم"  Visible="False" />
                    <asp:RadioButton ID="rbTarikhErsal" ForeColor="#ffffff" runat="server"  GroupName="TarikhEbtal" Text="تاریخ ارسال" Visible="False" />--%>
                </div>



                <div class="col-md-1">

                    <asp:ImageButton ID="btnTekrari" runat="server" ImageUrl="~/BootStrap/Image/Tekrari.png" ToolTip="ابطال تکراری" OnClick="btnTekrari_Click" Visible="False" />
                </div>

                <div class="col-md-1">
                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/BootStrap/Image/Update.png" ToolTip="بروزرسانی کلی" OnClick="btnUpdate_Click" Visible="false"/>
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-1"></div>



            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">

                    <telerik:RadGrid ID="MainGrid" runat="server" AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" ShowGroupPanel="True" Visible="false">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" ></GroupingSettings>

                        <ClientSettings AllowDragToGroup="True">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False">
                        </MasterTableView>
                    </telerik:RadGrid>
                    <%--                    <telerik:RadGrid ID="MainGrid" runat="server" AutoGenerateColumns="False">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText="رکوردی برای نمایش یافت نشد" PageSize="100">

                        
                    </MasterTableView>
                </telerik:RadGrid>--%>

                    <br />

                </div>

                <div class="col-md-1"></div>


            </div>
        </div>
        <div class="panel-footer">
        </div>

    </div>
    <%--1e326e--%>
    <div class="panel panel-primary    " style="background-color: #1e326e">
        <div class="panel-heading">
            <h3 style="text-align: center">گزارش آمارنامه</h3>
        </div>
        <div class="panel-body">
                    <div class="row" style="margin-left: 1%; margin-right: 1%">
            <div class="col-md-2">
                 <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/BootStrap/Image/excel2.png" OnClick="ImageButton2_Click" ViewStateMode="Disabled" ToolTip="خروجی اکسل" />
            </div>
            <div class="col-md-2">
                <label style="color: #ffffff">نوع پاسخ دریافتی : </label>
                &nbsp; 
                <asp:DropDownList ID="DDlNoe" runat="server" ViewStateMode="Enabled">
                    <asp:ListItem Value="0">همه</asp:ListItem>
                    <asp:ListItem Value="1">در صف پردازش</asp:ListItem>
                    <asp:ListItem Value="2">صحیح پردازش شده</asp:ListItem>
                    <asp:ListItem Value="3">خطا</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label style="color: #ffffff">تا تاریخ فرم : </label>
                &nbsp; 
                
                   <%-- <asp:TextBox ID="txtTaTarikhForm" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>--%>
                <telerik:RadMaskedTextBox ID="txtTaTarikhForm" runat="server" Mask="####/##/##" CssClass="form-control"></telerik:RadMaskedTextBox>
                &nbsp; 
                    
            </div>
            <div class="col-md-2">
                <label style="color: #ffffff">از ناریخ فرم : </label>
                &nbsp;  
                     
                   <%-- <asp:TextBox ID="txtAzTarikhForm" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>--%>
                <telerik:RadMaskedTextBox ID="txtAzTarikhForm" runat="server" Mask="####/##/##" CssClass="form-control"></telerik:RadMaskedTextBox>

            </div>
            <div class="col-md-2">
                <label style="color: #ffffff">تا تاریخ ارسال : </label>
                &nbsp; 
                
                   <%-- <asp:TextBox ID="txtTaTarikhErsal" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>--%>
                <telerik:RadMaskedTextBox ID="txtTaTarikhErsal" runat="server" Mask="####/##/##" CssClass="form-control"></telerik:RadMaskedTextBox>

                &nbsp; 
                    
            </div>
            <div class="col-md-2">
                <label style="color: #ffffff">از تاریخ ارسال : </label>
                &nbsp;  
                     
                    <%--<asp:TextBox ID="txtAzTarikhErsal" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>--%>
                <telerik:RadMaskedTextBox ID="txtAzTarikhErsal" runat="server" Mask="####/##/##" CssClass="form-control"></telerik:RadMaskedTextBox>

            </div>


        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-2">
               
            </div>
            <div class="col-md-2">
                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="گزارش آمارنامه" ImageUrl="~/BootStrap/Image/see2.png" Width="50%" Height="50%" OnClick="ImageButton1_Click" Visible="False" />
            </div>
            <div class="col-md-4"></div>
        </div>
        </div>

    </div>

    <!-- Modal Filter-->
    <div class="modal fade" id="Filter" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #7C9EB2">
                    <h4 class="modal-title" style="text-align: center">فیلترهای اضافه</h4>
                </div>
                <div class="modal-body" style="background-color: #808080">
                    <div class="row">
                        <div class="col-md-4">
                            <label>تامین کننده</label>
                            &nbsp; 
                            <telerik:RadListBox ID="ListTaminKonandeh" runat="server" Height="200px" Width="150px" CheckBoxes="True" ShowCheckAll="True">
                                <Localization CheckAll="انتخاب همه" />
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                            </telerik:RadListBox>
                        </div>
                        <div class="col-md-4">
                            <label>لیست گروه کالا</label>
                            &nbsp; 
                            <telerik:RadListBox ID="ListKalaGoroh" runat="server" Height="200px" CheckBoxes="True" ShowCheckAll="True">
                                <Localization CheckAll="انتخاب همه" />
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                            </telerik:RadListBox>
                        </div>
                        <div class="col-md-4">
                            <label>مرکز پخش</label>
                            &nbsp; 
                            <telerik:RadListBox ID="ListMarkazPakhsh" runat="server" Height="200px" Width="150px" CheckBoxes="True" ShowCheckAll="True">
                                <Localization CheckAll="انتخاب همه" />
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                            </telerik:RadListBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">

                        <div class="col-md-6">
                            <%--      <label>گروه مشتری</label>
                            &nbsp; 
                            <telerik:RadListBox ID="ListMoshtaryGoroh" runat="server" Width="300px" Height="200px" CheckBoxes="True" ShowCheckAll="True">
                                <Localization CheckAll="انتخاب همه" />
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                            </telerik:RadListBox>--%>
                        </div>
                        <div class="col-md-6">
                            <label>لیست برند کالا</label>
                            &nbsp; 
                            <telerik:RadListBox ID="ListBrand" runat="server" Width="300px" Height="200px" CheckBoxes="True" ShowCheckAll="True">
                                <Localization CheckAll="انتخاب همه" />
                                <ButtonSettings TransferButtons="All"></ButtonSettings>
                            </telerik:RadListBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="background-color: #7C9EB2">

                    <div class="row">
                        <div class="col-md-5"></div>
                        <div class="col-md-1">
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">بستن</button>--%>
                        </div>
                        <div class="col-md-1">
                            <button type="button" class="btn btn-success" data-dismiss="modal">اعمال</button>


                        </div>
                        <div class="col-md-5"></div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <!-- Modal warning-->
    <div class="modal fade" id="warning" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #7C9EB2">
                    <h4 class="modal-title" style="text-align: center">لیست خطاها</h4>
                </div>
                <div class="modal-body" style="background-color: #808080">
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <telerik:RadGrid ID="Warning" runat="server" AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" ShowGroupPanel="True" DataSourceID="SqlDataSource1">
                                <ClientSettings AllowDragToGroup="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                </ClientSettings>
                                <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ccKardexUUID">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ccKardexUUID" ReadOnly="True" HeaderText="ccKardexUUID" SortExpression="ccKardexUUID" UniqueName="ccKardexUUID" DataType="System.Int64" FilterControlAltText="Filter ccKardexUUID column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ccKardex" HeaderText="ccKardex" SortExpression="ccKardex" UniqueName="ccKardex" DataType="System.Int64" FilterControlAltText="Filter ccKardex column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Time" HeaderText="Time" SortExpression="Time" UniqueName="Time" DataType="System.DateTime" FilterControlAltText="Filter Time column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UUID" HeaderText="UUID" SortExpression="UUID" UniqueName="UUID" FilterControlAltText="Filter UUID column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CodeNoeAction" HeaderText="CodeNoeAction" SortExpression="CodeNoeAction" UniqueName="CodeNoeAction" DataType="System.Int32" FilterControlAltText="Filter CodeNoeAction column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status" UniqueName="Status" DataType="System.Decimal" FilterControlAltText="Filter Status column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Comment" HeaderText="Comment" SortExpression="Comment" UniqueName="Comment" FilterControlAltText="Filter Comment column"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:PakhshConnectionString %>' SelectCommand="ttac" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:Parameter Name="AzTarikhShamsi" Type="String" />
                                    <asp:Parameter Name="TaTarikhShamsi" Type="String" />
                                    <asp:Parameter Name="strMarkazPakhsh" Type="String" />
                                    <asp:Parameter Name="strVorodi" Type="String" />
                                    <asp:Parameter Name="StrKHoroji" Type="String" />
                                    <asp:Parameter Name="strGorohKala" Type="String" />
                                    <asp:Parameter Name="strTaminKonandeh" Type="String" />
                                    <asp:Parameter Name="strBrand" Type="String" />
                                    <asp:Parameter Name="strGorohMoshtary" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                        <div class="col-md-1"></div>
                    </div>


                </div>
                <div class="modal-footer" style="background-color: #7C9EB2">

                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-2">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">بستن</button>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="Button2" runat="server" Text="ارسال مجدد" CssClass="btn btn-success" />

                        </div>
                        <div class="col-md-4"></div>
                    </div>

                </div>
            </div>
        </div>
    </div>



    <!-- Modal WhereFilter-->
    <div class="modal fade" id="WhereFilter" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #7C9EB2">
                    <h4 class="modal-title" style="text-align: center">دید سریع سیستم</h4>
                </div>
                <div class="modal-body" style="background-color: #808080">
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h4>مرکز پخش</h4>
                            <p>
                                <asp:Label ID="lblMarkazPakhsh" runat="server" Text=""></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>ورودی انبار</h4>
                            <p>
                                <asp:Label ID="lblVorodi" runat="server" Text=""></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>خروجی انبار</h4>
                            <p>
                                <asp:Label ID="lblKhoroji" runat="server" Text=""></asp:Label>
                            </p>
                            <hr class="hr-warning" />

                            <h4>گروه کالا</h4>
                            <p>
                                <asp:Label ID="lblKalaGoroh" runat="server" Text=""></asp:Label>
                            </p>
                            <hr class="hr-warning" />


                            <h4>تامین کننده</h4>
                            <p>
                                <asp:Label ID="lblTaminKonandeh" runat="server" Text=""></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>برند </h4>
                            <p>
                                <asp:Label ID="lblBrand" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                        <div class="col-md-1"></div>
                    </div>


                </div>
                <div class="modal-footer" style="background-color: #7C9EB2">
                    <div class="row">
                        <div class="col-md-5"></div>

                        <div class="col-md-2">
                            <button type="button" class="btn btn-warning btn-lg" data-dismiss="modal">خروج</button>
                        </div>
                        <div class="col-md-5"></div>


                    </div>

                </div>
            </div>
        </div>
    </div>



    <!-- Modal Config-->
    <div class="modal fade" id="Config" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #7C9EB2">
                    <h4 class="modal-title" style="text-align: center">تنظیمات</h4>
                </div>
                <div class="modal-body" style="background-color: #808080">
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h4>نام کاربری</h4>
                            <p>
                                <asp:Label ID="lblUserName" runat="server" Font-Bold="True"></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>رمز عبور</h4>
                            <p>
                                <asp:Label ID="lblPassword" runat="server" Font-Bold="True"></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>آدرس وب سرویس</h4>
                            <p>
                                <asp:Label ID="lblWebService" runat="server" Font-Bold="True"></asp:Label>
                            </p>

                        </div>
                        <div class="col-md-1"></div>
                    </div>


                </div>
                <div class="modal-footer" style="background-color: #7C9EB2">
                    <div class="row">
                        <div class="col-md-5"></div>

                        <div class="col-md-2">
                            <button type="button" class="btn btn-warning btn-lg" data-dismiss="modal">خروج</button>
                        </div>
                        <div class="col-md-5"></div>


                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="Help" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #7C9EB2">
                    <h4 class="modal-title" style="text-align: center">لیست آخرین شرایط ارسال</h4>
                </div>
                <div class="modal-body" style="background-color: #808080">
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <h4>رکوردهای در صف پردازش</h4>
                            <p>
                                <asp:Label ID="lblQueue" runat="server" Text="" Font-Bold="True" ForeColor="#00CC99"></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>رکوردهای خطا دار</h4>
                            <p>
                                <asp:Label ID="lblError" runat="server" Text="" Font-Bold="True" ForeColor="#00CC99"></asp:Label>
                            </p>
                            <hr class="hr-warning" />
                            <h4>رکوردهای ارسالی ثبت شده</h4>
                            <p>
                                <asp:Label ID="lblSuccess" runat="server" Text="" Font-Bold="True" ForeColor="#00CC99"></asp:Label>
                            </p>
                            <hr class="hr-warning" />

                            <h4>رکوردهای ارسالی ناقص</h4>
                            <p>
                                <asp:Label ID="lblWarning" runat="server" Text="" Font-Bold="True" ForeColor="#00CC99"></asp:Label>
                            </p>
                            <hr class="hr-warning" />


                            <h4>وضعیت برنامه</h4>
                            <p>
                                <asp:Label ID="lblOprationStatus" runat="server" Text="" Font-Bold="True" ForeColor="#00CC99"></asp:Label>
                            </p>

                        </div>
                        <div class="col-md-1"></div>
                    </div>


                </div>
                <div class="modal-footer" style="background-color: #7C9EB2">
                    <div class="row">
                        <div class="col-md-5"></div>

                        <div class="col-md-2">
                            <button type="button" class="btn btn-warning btn-lg" data-dismiss="modal">خروج</button>
                        </div>
                        <div class="col-md-5"></div>


                    </div>

                </div>
            </div>
        </div>
    </div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel_MainGrid" runat="server" Skin="Default"></telerik:RadAjaxLoadingPanel>
</asp:Content>


