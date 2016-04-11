<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EWSD.Guest.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Guest Menu</h3>
    <div class="jumbotron">
        <h3>View Approved Course Monitoring Reports (CMR)</h3>
        <table style="font-size:18px;">
            <tr>
                <td>Approved CMRs: </td>
                <td><asp:DropDownList ID="comboApprovedCMR" runat="server"></asp:DropDownList></td>
                <td><asp:Button ID="bViewReport" runat="server" Text="View Report" OnClick="bViewReport_Click" /></td>
            </tr>
        </table>
        <h3>Statistical Reports</h3>
        <ul>
            <li><a href="StatisticReport.aspx?type=sccmrfay">Number of completed CMR for each Faculty for each Academic Year</a></li>
            <li><a href="StatisticReport.aspx?type=spscmrfay">Percentage of completed CMR for each Faculty for any Academic Year</a></li>
            <li><a href="StatisticReport.aspx?type=spscmrr">Percentage of CMR with Responses for each Faculty</a></li>
        </ul>
        <h3>Exception Reports</h3>
        <ul>
            <li><a href="ExceptionReport.aspx?type=xclcm">Courses without CL or CM</a></li>
            <li><a href="ExceptionReport.aspx?type=xcmray">Courses without CMR for an Academic Year</a></li>
            <li><a href="ExceptionReport.aspx?type=xresp">CMR without response</a></li>
        </ul>
    </div>
</asp:Content>
