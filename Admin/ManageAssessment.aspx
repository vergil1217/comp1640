<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ManageAssessment.aspx.cs" Inherits="EWSD.Admin.ManageAssessment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h3>Manage Assessment</h3>
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalActionFailure" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <br />
        <fieldset>
            <legend>Manage Assessment</legend>
            <asp:Panel ID="panelhideAssessment" runat="server">
                <asp:Panel ID="panelSelectFaculty" runat="server">
                    <table style="font-size:18px;">
                        <tr>
                            <td>Select Faculty: </td>
                            <td><asp:DropDownList ID="comboFaculty" runat="server"></asp:DropDownList></td>
                            <td><asp:Button ID="bSelectFaculty" runat="server" Text="Select Faculty" OnClick="bSelectFaculty_Click" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelSelectCourse" runat="server" Visible="false">
                    <table style="font-size:18px;">
                        <tr>
                            <td>Select Course: </td>
                            <td><asp:DropDownList ID="comboCourse" runat="server"></asp:DropDownList></td>
                            <td><asp:Button ID="bSelectCourse" runat="server" Text="Select Course" OnClick="bSelectCourse_Click" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelSelectCoursework" runat="server" Visible="false">
                    <table style="font-size:18px;">
                        <tr>
                            <td>Select Coursework: </td>
                            <td><asp:DropDownList ID="comboCoursework" runat="server"></asp:DropDownList></td>
                            <td><asp:Button ID="bSelectCoursework" runat="server" Text="Select Coursework" OnClick="bSelectCoursework_Click" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelManageAssessment" runat="server" Visible="false">
                    <table style="font-size:18px;">
                        <tr>
                            <td>Select Assessment</td>
                            <td style="padding-left:15px; padding-right:15px;">
                                <asp:CheckBox ID="checkCW1" runat="server" Text="Coursework 1"/><br />
                                <asp:CheckBox ID="checkCW2" runat="server" Text="Coursework 2"/><br />
                                <asp:CheckBox ID="checkExam" runat="server" Text="Exams"/>
                            </td>
                            <td><asp:Button ID="bUpdateAssessment" runat="server" Text="Update Assessment" OnClick="bUpdateAssessment_Click" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
