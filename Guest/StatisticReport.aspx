<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="StatisticReport.aspx.cs" Inherits="EWSD.Guest.StatisticReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Statistical Report</h3>
    <div class="jumbotron">
        <asp:Panel ID="panelSCCMRFAY" runat="server" Visible="false">
            <h3><asp:Literal ID="literalSCCMRFAY" runat="server"></asp:Literal></h3>
            <asp:BulletedList ID="listSCCMRFAY" runat="server"></asp:BulletedList>
        </asp:Panel>
        <asp:Panel ID="panelSPSCMRFAY" runat="server" Visible="false">
            <h3><asp:Literal ID="literalSPSCMRFAY" runat="server"></asp:Literal></h3>
            <asp:BulletedList ID="listSPSCMRFAY" runat="server"></asp:BulletedList>
        </asp:Panel>
        <asp:Panel ID="panelSPSCMRR" runat="server" Visible="false">
            <h3><asp:Literal ID="literalSPSCMRR" runat="server"></asp:Literal></h3>
            <asp:BulletedList ID="listSPSCMRR" runat="server"></asp:BulletedList>
        </asp:Panel>
    </div>
</asp:Content>
