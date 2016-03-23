<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ManageFaculty.aspx.cs" Inherits="EWSD.Admin.ManageFaculty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">
        protected void bHidePanelAddFaculty_Click(object sender, EventArgs e)
        {
            if (panelAddFaculty.Visible)
            {
                panelAddFaculty.Visible = false;
                bHidePanelAddFaculty.Text = "Show Panel";
            }
            else
            {
                panelAddFaculty.Visible = true;
                panelRemoveFaculty.Visible = false;
                bHidePanelAddFaculty.Text = "Hide Panel";
                bHidePanelRemoveFaculties.Text = "Show Panel";
            }
        }

        protected void bHidePanelRemoveFaculties_Click(object sender, EventArgs e)
        {
            if (panelRemoveFaculty.Visible)
            {
                panelRemoveFaculty.Visible = false;
                bHidePanelRemoveFaculties.Text = "Show Panel";
            }
            else
            {
                panelRemoveFaculty.Visible = true;
                panelAddFaculty.Visible = false;
                bHidePanelRemoveFaculties.Text = "Hide Panel";
                bHidePanelAddFaculty.Text = "Show Panel";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h3>Manage Faculties</h3>
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalActionFailure" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <fieldset>
            <legend>Add Faculties&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="bHidePanelAddFaculty" runat="server" Text="Hide Panel" CausesValidation="false" OnClick="bHidePanelAddFaculty_Click" /></legend>
            <asp:Panel runat="server" ID="panelAddFaculty">
                <table>
                    <tr style="font-size:18px;">
                        <td>Faculty Code:</td>
                        <td><asp:TextBox ID="fieldFacultyCode" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldFacultyCode" CssClass="text-danger" Display="Dynamic" ErrorMessage="Faculty Code is required." /></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>Faculty Name:</td>
                        <td><asp:TextBox ID="fieldFacultyName" runat="server" Width="400px"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldFacultyName" CssClass="text-danger" Display="Dynamic" ErrorMessage="Faculty Name is required." /></td>
                        <td><asp:Button ID="bAddFaculty" runat="server" Text="Add Faculty" OnClick="bAddFaculty_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <br /><br />
        <fieldset>
            <legend>Remove Faculties&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="bHidePanelRemoveFaculties" runat="server" Text="Show Panel" CausesValidation="false" OnClick="bHidePanelRemoveFaculties_Click" /></legend>
            <asp:Panel runat="server" ID="panelRemoveFaculty" Visible="false">
                <table>
                    <tr style="font-size:18px;">
                        <td>Removable Faculties: </td>
                        <td><asp:DropDownList ID="comboRemovableFaculties" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bRemoveFaculty" runat="server" Text="Remove Faculty" OnClick="bRemoveFaculty_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
