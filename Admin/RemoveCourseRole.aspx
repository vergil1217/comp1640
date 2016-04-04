<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="RemoveCourseRole.aspx.cs" Inherits="EWSD.Admin.RemoveCourseRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Remove <asp:Label ID="labelRole" runat="server" Text=""></asp:Label></h3>
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
                        <td><asp:Button ID="bSelectFaculty" runat="server" Text="Select Faculty" OnClick="bSelectFaculty_Click"/></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelSelectCourse" Visible="false">
            <fieldset>
                <table>
                    <tr style="font-size:18px;">
                        <td>Select Course: </td>
                        <td><asp:DropDownList ID="comboCourse" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bSelectCourse" runat="server" Text="Select Course" OnClick="bSelectCourse_Click"/></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelSelectStaff" Visible="false">
            <fieldset>
                <table>
                    <tr style="font-size:18px;">
                        <td>Current Staff: </td>
                        <td style="color:blue;"><asp:Literal ID="literalCurrentStaff" runat="server"></asp:Literal></td>
                        <td><asp:Button ID="bRemoveStaff" runat="server" Text="Remove Staff" OnClick="bRemoveStaff_Click"/></td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:TextBox ID="fieldStaffId" Visible="false" ReadOnly="true" runat="server"></asp:TextBox>
    </div>
</asp:Content>
