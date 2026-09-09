<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Site.Master" AutoEventWireup="true" CodeBehind="karshenas.aspx.cs" Inherits="GhazaVaDarouFararavand.karshenas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h3 style="text-align: center">ارسال رکورد</h3>
                </div>
                <div class="panel-body">
                    <div class="row" >
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:ImageButton ID="btnSendUUID" runat="server" ImageUrl="~/BootStrap/Image/send.png" CssClass="img-responsive" ToolTip="ارسال تکی اطلاعات" OnClick="btnSendUUID_Click"  />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtUUID" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>UUID : </label>
                        </div>

                        <div class="col-md-2"></div>
                    </div>
                    <hr />
                    <hr />
                    <div class="row" >
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:ImageButton ID="btnSendccKardexSatr" runat="server" ImageUrl="~/BootStrap/Image/send.png" CssClass="img-responsive" ToolTip="ارسال تکی اطلاعات" OnClick="btnSendccKardexSatr_Click" />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtccKardexsatr" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>شماره سطر کاردکس : </label>
                        </div>

                        <div class="col-md-2"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>

    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 style="text-align: center">بروزرسانی رکورد</h3>
                </div>
                                <div class="panel-body">
                    <div class="row" >
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/BootStrap/Image/ButtonSafPardazesh.png" CssClass="img-responsive" ToolTip="بروزرسانی اطلاعات" OnClick="btnUpdate_Click" />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtUpdate" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>UUID : </label>
                        </div>

                        <div class="col-md-2"></div>
                    </div></div>
            </div>
        </div>
        <div class="col-md-1"></div>
    </div>
</asp:Content>
