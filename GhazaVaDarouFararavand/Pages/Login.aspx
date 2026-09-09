<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GhazaVaDarouFararavand.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>صفحه ورود</title>
    <link href="../BootStrap/Image/FaraIcon.ico" rel="shortcut icon" />
    <link href="../BootStrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../BootStrap/Content/bootstrap-theme.css" rel="stylesheet" />
    <script src="../BootStrap/Scripts/jquery-1.9.1.min.js"></script>
    <script src="../BootStrap/Scripts/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#LogIn").hide();
            $("#LogIn").fadeIn(5000);
            //$("#Logo").hide();
            //$("#Logo").fadeIn(4000);

            $("#divRight").show();
            $("#divRight").fadeOut(4000);
            $("#divLeft").show();
            $("#divLeft").fadeOut(4000);

            divRight
        });

    </script>
</head>
<body style="background-color: #1e326e;">
    <form id="form1" runat="server">
        <div class="panel panel-info ">
  <div class="panel-heading"><h2 style="text-align:center">برنامه ارسال آمارنامه نسخه تابستان 1397</h2> </div>
  <div class="panel-body" style="background-color: #1e326e;">

      <div id="main" dir="rtl">
            <div class="row">
                <div class="col-md-4">
                    <div id="divRight" style="margin:1%">
                        <asp:Image ID="Right" runat="server" ImageUrl="~/BootStrap/Image/TTAC.png" Width="75%" Height="75%" />
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="row" style="margin-left: 15%; margin-right: 15%">
                        <div class="col-md-12">
                            <div id="Logo">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/BootStrap/Image/logo UltraData.jpg" Width="100%" Height="100%" />
                            </div>
                        </div>
                    </div>
                    <div class="row" >

                        <div class="col-md-12">

                            <div id="LogIn">
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h3 style="text-align: center">ورود به سیستم</h3>
                                    </div>
                                    <div class="panel-body">

                                        <div class="form-group">
                                            <label for="exampleInputEmail1">نام کاربری</label>
                                            <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">کلمه ورود</label>

                                            <asp:TextBox ID="txtPassword" class="form-control" runat="server" type="password"></asp:TextBox>
                                        </div>

                                        <asp:Button ID="btnVorod" runat="server" Text="ورود" CssClass="btn btn-primary" Style="width: 100%" OnClick="btnVorod_Click" />
                                    </div>
                                    <div class="panel-footer">
                                        <asp:Label ID="lblError" ForeColor="Fuchsia" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    
                </div>
                <div class="col-md-4">
                    <div id="divLeft"  style="margin:1%">
                        <asp:Image ID="Left" runat="server" ImageUrl="~/BootStrap/Image/TTAC.png" Width="75%" Height="75%" />
                    </div>
                </div>

            </div>
        </div>
  </div>

</div>
        
    </form>
</body>
</html>
