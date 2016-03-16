<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EWSD.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Welcome!</h1><br />
        <p>
            This is the Course Monitoring Reporting System for the University of Zootropolis.<br /><br />
            You will be redirected to the login page in 5 seconds.
        </p>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
</asp:Content>
