<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EWSD.Course.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h3><asp:Label ID="labelRoleCP" runat="server" Text=""></asp:Label></h3>
    </div>
    <div class="jumbotron">
        <p>
            Course Monitoring Report (CMR)
        </p>
        <asp:Panel ID="panelCLCP" runat="server" Visible="false">
            <table style="font-size:18px;" border="1">
                <tr>
                    <td style="padding:10px;"><a href="CMREntry.aspx">Create CMR</a></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelCMCP" runat="server" Visible="false">
            <table style="font-size:18px;" border="1">
                <tr>
                    <td style="padding:10px;"><a href="ViewCMR.aspx">View CMR</a></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
