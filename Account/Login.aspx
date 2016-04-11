<%@ Page Title="Login" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EWSD.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>Use a local account to log in.</h4>
                    <hr />
                    <p class="text-danger">
                        <asp:Literal runat="server" ID="literalLoginFail" />
                    </p><br />
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="fieldUsername" CssClass="col-md-2 control-label">Username</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="fieldUsername" CssClass="form-control" TextMode="SingleLine" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldUsername"
                                CssClass="text-danger" ErrorMessage="Username is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="fieldPassword" CssClass="col-md-2 control-label">Password</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="fieldPassword" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldPassword" CssClass="text-danger" ErrorMessage="Password is required." />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="checkRememberMe" />
                                <asp:Label runat="server" AssociatedControlID="checkRememberMe">Remember me?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-default" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<a href="ForgotPassword.aspx">Forgot Password?</a>
                            &nbsp;&nbsp;&nbsp;&nbsp;<a href="/Guest/Default.aspx">Login as Guest</a>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </div>
</asp:Content>