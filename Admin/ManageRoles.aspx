<%@ Page Title="Manage Roles" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ManageRoles.aspx.cs" Inherits="EWSD.Admin.ManageRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Manage Roles</h3>
    <div class="jumbotron">
        <fieldset>
            <legend style="font-size:18px;">Add / Delete Roles</legend>
            <table style="font-size:18px;">
                <tr>
                    <td><asp:TextBox ID="fieldNewRole" runat="server"></asp:TextBox></td>
                    <td><asp:Button ID="bAddRole" runat="server" Text="Add New Role" OnClick="bAddRole_Click" /></td>
                    <td class="text-danger"><asp:Literal ID="literalAddFailure" runat="server"></asp:Literal></td>
                    <td class="text-success"><asp:Literal ID="literalAddSuccess" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:DropDownList ID="comboRoles" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bDeleteRole" runat="server" Text="Delete Role" OnClick="bDeleteRole_Click" /></td>
                    <td class="text-danger"><asp:Literal ID="literalDeleteFailure" runat="server"></asp:Literal></td>
                    <td class="text-success"><asp:Literal ID="literalDeleteSuccess" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
