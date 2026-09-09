<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="GhazaVaDarouFararavand.Pages.Report" %>

<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
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
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" UpdatePanelCssClass="" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="row">
        <div class="col-md-12">
            <telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" AllowMultiRowSelection="True" CellSpacing="-1" GridLines="Both" PageSize="100" OnNeedDataSource="RadGrid1_NeedDataSource">

                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                    <Selecting CellSelectionMode="MultiCell" UseClientSelectColumnOnly="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>


                <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="UUID">






                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TempCol1" HeaderText="انتخاب">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" />
                            </ItemTemplate>
<%--                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </EditItemTemplate>--%>
                        </telerik:GridTemplateColumn>
                        <%--                        <telerik:GridCheckBoxColumn  AllowFiltering="false" AllowSorting="false"  DataType="System.Boolean" FilterControlAltText="Filter column column" UniqueName="column">
                        </telerik:GridCheckBoxColumn>  --%>
                    </Columns>

                </MasterTableView>
            </telerik:RadGrid>
            <%--<telerik:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="True" AllowPaging="True" AllowSorting="True" ShowGroupPanel="True" AllowMultiRowSelection="True" CellSpacing="-1" GridLines="Both" PageSize="100" DataSourceID="SqlDataSource1" OnItemCommand="radgrid1_ItemCommand" OnItemCreated="RadGrid1_ItemCreated">

                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                <ClientSettings AllowDragToGroup="True" AllowColumnsReorder="True">
                    <Selecting CellSelectionMode="MultiCell" UseClientSelectColumnOnly="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>


                <MasterTableView AllowMultiColumnSorting="True" DataSourceID="SqlDataSource1" PageSize="10">


                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="TempCol1" HeaderText="انتخاب">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" />
                            </ItemTemplate>
                            <%--                            <EditItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </EditItemTemplate>
                        </telerik:GridTemplateColumn>
                        <%--                        <telerik:GridCheckBoxColumn  AllowFiltering="false" AllowSorting="false"  DataType="System.Boolean" FilterControlAltText="Filter column column" UniqueName="column">
                        </telerik:GridCheckBoxColumn> 
                    </Columns>

                </MasterTableView>
            </telerik:RadGrid>--%>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PakhshConnectionString %>" SelectCommand="ReportAmarNameh" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="13960101" Name="AzTarikhErsaliSHamsi" QueryStringField="txtAzTarikhErsal" Type="String" />
                    <asp:QueryStringParameter DefaultValue="13961229" Name="TaTarikhErsaliSHamsi" QueryStringField="txtTaTarikhErsal" Type="String" />
                    <asp:QueryStringParameter DefaultValue="13960101" Name="AzTarikhFormSHamsi" QueryStringField="txtAzTarikhForm" Type="String" />
                    <asp:QueryStringParameter DefaultValue="13961229" Name="TaTarikhFormSHamsi" QueryStringField="txtTaTarikhForm" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
            <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/BootStrap/Image/back.png" ToolTip="بازگشت" OnClick="btnBack_Click" />
            <br />
            <asp:ImageButton ID="btnEbtal" runat="server" ToolTip="ابطال" ImageUrl="~/BootStrap/Image/Ebtal.png" OnClick="btnEbtal_Click" Visible="False" />
            <br />

        </div>
    </div>

</asp:Content>
