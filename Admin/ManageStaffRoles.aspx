<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ManageStaffRoles.aspx.cs" Inherits="EWSD.Admin.ManageStaffRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Manage Staff Roles</h3>
    <div class="jumbotron">
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalActionFailure" runat="server"></asp:Literal></span><br/>
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <asp:Panel runat="server" ID="panelSelectStaff">
            <fieldset>
                <legend>Staff Selection</legend>
                    <table>
                        <tr style="font-size:18px;">
                            <td>Select Staff: </td>
                            <td><asp:DropDownList ID="comboStaff" runat="server"></asp:DropDownList></td>
                            <td><asp:Button ID="bSelectStaff" runat="server" Text="Select Staff" OnClick="bSelectStaff_Click"/></td>
                        </tr>
                    </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelAssignRoleToStaff" Visible="false">
            <fieldset>
                <legend>Assign Role</legend>
                <table>
                    <tr style="font-size:18px;">
                        <td>Staff Name:</td>
                        <td><asp:Label ID="labelStaffName" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>Staff Role:</td>
                        <td><asp:DropDownList ID="comboRole" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bAssignRole" runat="server" Text="Confirm Assignment" OnClick="bAssignRole_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:TextBox ID="fieldUserRole" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
    </div>
</asp:Content>
