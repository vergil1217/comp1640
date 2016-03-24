<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="RemoveFacultyRole.aspx.cs" Inherits="EWSD.Admin.RemoveFacultyRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">
        protected void bRemoveStaff_Click(object sender, EventArgs e)
        {
            bRemoveStaff.Visible = false;
            bConfirmRemoveStaff.Visible = true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Remove <asp:Label ID="labelRole" runat="server" Text=""></asp:Label></h3>
    <div class="jumbotron">
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalWarning" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <fieldset>
            <asp:Panel runat="server" ID="panelSelectFaculty">
                <table>
                    <tr style="font-size:18px;">
                        <td>Select Faculty: </td>
                        <td><asp:DropDownList ID="comboFaculties" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bSelectFaculty" runat="server" Text="Select Faculty" OnClick="bSelectFaculty_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <br />
        <fieldset>
            <asp:Panel runat="server" ID="panelRemoveStaff" Visible="false">
                <table>
                    <tr style="font-size:18px;">
                        <td>Confirm Removal?</td>
                        <td style="font-size:14px;padding:10px;color:blue;">
                            Faculty: <asp:Literal ID="literalConfirmFaculty" runat="server"></asp:Literal><br />
                            Staff Name: <asp:Literal ID="literalStaffName" runat="server"></asp:Literal><br />
                        </td>
                        <td><asp:Button ID="bRemoveStaff" runat="server" Text="Remove" OnClick="bRemoveStaff_Click" /></td>
                        <td><asp:Button ID="bConfirmRemoveStaff" runat="server" Text="Confirm Removal" OnClick="bConfirmRemoveStaff_Click" Visible="false"/></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </div>
    <asp:TextBox ID="fieldStaffId" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
</asp:Content>
