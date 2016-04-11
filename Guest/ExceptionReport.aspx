<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ExceptionReport.aspx.cs" Inherits="EWSD.Guest.ExceptionReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Exception Report</h3>
    <div class="jumbotron">
        <asp:Panel ID="panelXCLCM" runat="server" Visible="false">
            <h3><asp:Literal ID="literalXCLCM" runat="server"></asp:Literal></h3>
            <asp:BulletedList ID="listXCLCM" runat="server"></asp:BulletedList>
        </asp:Panel>
        <asp:Panel ID="panelXCMRAY" runat="server" Visible="false">
            <h3><asp:Literal ID="literalXCMRAY" runat="server"></asp:Literal></h3>
            <asp:Panel ID="panelXCMRAYSelectAcademicYear" runat="server">
                <table>
                    <tr>
                        <td>Select Academic Year</td>
                        <td><asp:TextBox ID="fieldXCMRAYAcademicYear" TextMode="Number"  min="2000" step="1" runat="server"></asp:TextBox></td>
                        <td><asp:Button ID="bSelectXCMRAYAcademicYear" runat="server" Text="Select Academic Year" OnClick="bSelectAcademicYear_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="panelXCMRAYBody" runat="server">
                <asp:BulletedList ID="listXCMRAY" runat="server"></asp:BulletedList>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="panelXRESP" runat="server" Visible="false">
            <h3><asp:Literal ID="literalXRESP" runat="server"></asp:Literal></h3>
            <asp:BulletedList ID="listXRESP" runat="server"></asp:BulletedList>
        </asp:Panel>
    </div>
</asp:Content>
