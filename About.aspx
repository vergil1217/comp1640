<%@ Page Title="About" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EWSD.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h2><%: Title %>.</h2>
        <h3>Course Monitoring Reporting System for University of Zootropolis</h3>
        <p>An application to allow faculty staff to generate course reports for the year.</p>
    </div>
</asp:Content>
