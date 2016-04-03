<%@ Page Title="Home" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="EWSD.Admin.AdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h3>Administrator Control Panel</h3>
    </div>
    <div class="jumbotron">
        Available Actions:<br /><br />
        <p>
            Staff Management
        </p>
        <table border="1">
            <tr>
                <td style="padding:10px;"><a href="Register.aspx">Register Staff</a></td>
                <td style="padding:10px;"><a href="ManageStaffRoles.aspx">Manage Staff Roles</a></td>
                <td style="padding:10px;"><a href="ManageRoles.aspx">Manage Roles</a></td>
            </tr>
        </table>
        <br />
        <p>
            Faculty Management
        </p>
        <table border="1">
            <tr>
                <td style="padding:10px;"><a href="ManageFaculty.aspx">Manage Faculties</a></td>
                <td style="padding:10px;"><a href="AssignFacultyRole.aspx?role=pvc">Assign Pro-Vice Chancellor</a></td>
                <td style="padding:10px;"><a href="AssignFacultyRole.aspx?role=dlt">Assign Director of Learning & Quality</a></td>
            </tr>
            <tr>
                <td style="border-left:hidden; border-bottom:hidden;"></td>
                <td style="padding:10px;"><a href="RemoveFacultyRole.aspx?role=pvc">Remove Pro-Vice Chancellor</a></td>
                <td style="padding:10px;"><a href="RemoveFacultyRole.aspx?role=dlt">Remove Director of Learning & Quality</a></td>
            </tr>
        </table>
        <br />
        <p>
            Course Management
        </p>
        <table border="1">
            <tr>
                <td style="padding:10px;"><a href="ManageCourse.aspx">Manage Courses</a></td>
            </tr>
        </table>
    </div>
</asp:Content>
