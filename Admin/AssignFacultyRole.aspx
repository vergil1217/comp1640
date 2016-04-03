<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="AssignFacultyRole.aspx.cs" Inherits="EWSD.Admin.AssignFacultyRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">
        protected void bSelectFaculty_Click(object sender, EventArgs e)
        {
            panelSelectFaculty.Visible = false;
            panelSelectStaff.Visible = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Assign <asp:Label ID="labelRole" runat="server" Text=""></asp:Label></h3>
    <div class="jumbotron">
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalWarning" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <asp:Panel runat="server" ID="panelSelectFaculty">
            <fieldset>
                <table>
                    <tr style="font-size:18px;">
                        <td>Select Faculty: </td>
                        <td><asp:DropDownList ID="comboFaculties" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bSelectFaculty" runat="server" Text="Select Faculty" OnClick="bSelectFaculty_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelSelectStaff" Visible="false">
            <fieldset>
                <table>
                    <tr style="font-size:18px;">
                        <td>Select Staff: </td>
                        <td><asp:DropDownList ID="comboStaff" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bSelectStaff" runat="server" Text="Elect Staff" OnClick="bSelectStaff_Click" /></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
