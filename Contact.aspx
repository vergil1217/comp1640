<%@ Page Title="Contact" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="EWSD.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>University of Zootropolis Contacts</h3>
    
    <br /><br />
    <p>
        <strong>Faculty of Applied Sciences and Computing (FASC)</strong>
    </p>
    <address>
        Block A, University of Zootropolis,<br />
        Ricky Hopps Way, 15400 Zootropolis<br />
        P: 456.855.1000<br /><br />
        <strong>Dean: Prof. Judy Hopps<br />
        Ext: 50</strong>
    </address>

    <br /><br />
    <p>
        <strong>Faculty of Accounting, Finance and Business (FAFB)</strong>
    </p>
    <address>
        Block B, University of Zootropolis,<br />
        Ricky Hopps Way, 15400 Zootropolis<br />
        P: 456.855.1001<br /><br />
        <strong>Dean: Prof. Nicky Wilde<br />
        Ext: 102</strong>
    </address>

    <br /><br />
    <p>
        <strong>Faculty of Social Science, Arts and Humanities (FSAH)</strong>
    </p>
    <address>
        Block C, University of Zootropolis,<br />
        Ricky Hopps Way, 15400 Zootropolis<br />
        P: 456.855.1002<br /><br />
        <strong>Dean: Prof. Gideon Grey<br />
        Ext: 180</strong>
    </address>
</asp:Content>
