<%@ Page Title="Manage Course" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ManageCourse.aspx.cs" Inherits="EWSD.Admin.ManageCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script runat="server">
        protected void bHidePanelAddCourse_Click(object sender, EventArgs e)
        {
            if (panelAddCourse.Visible)
            {
                panelAddCourse.Visible = false;
                bHidePanelAddCourse.Text = "Show Panel";
            }
            else
            {
                panelAddCourse.Visible = true;
                panelRemoveCourse.Visible = false;
                bHidePanelAddCourse.Text = "Hide Panel";
                bHidePanelRemoveCourse.Text = "Show Panel";
            }
        }

        protected void bHidePanelRemoveCourse_Click(object sender, EventArgs e)
        {
            if (panelRemoveCourse.Visible)
            {
                panelRemoveCourse.Visible = false;
                bHidePanelRemoveCourse.Text = "Show Panel";
            }
            else
            {
                panelRemoveCourse.Visible = true;
                panelAddCourse.Visible = false;
                bHidePanelRemoveCourse.Text = "Hide Panel";
                bHidePanelAddCourse.Text = "Show Panel";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h3>Manage Course</h3>
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalActionFailure" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <br />
        <fieldset>
            <legend>Add Course&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="bHidePanelAddCourse" runat="server" Text="Hide Panel" CausesValidation="false" OnClick="bHidePanelAddCourse_Click" /></legend>
            <asp:Panel runat="server" ID="panelAddCourse">
                <table>
                    <tr style="font-size:18px;">
                        <td>Course Code: </td>
                        <td><asp:TextBox ID="fieldCourseCode" runat="server" Width="400px"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldCourseCode" CssClass="text-danger" Display="Dynamic" ErrorMessage="Course Code is required." /></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>Course Title: </td>
                        <td><asp:TextBox ID="fieldCourseTitle" runat="server" Width="400px"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldCourseTitle" CssClass="text-danger" Display="Dynamic" ErrorMessage="Course Title is required." /></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>Course Prefix: </td>
                        <td><asp:TextBox ID="fieldCoursePrefix" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldCoursePrefix" CssClass="text-danger" Display="Dynamic" ErrorMessage="Course Prefix is required." /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="font-size:14px;color:blue;">*Prefix for subjects e.g. COMP1440, Prefix: COMP</td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>Parent Faculty: </td>
                        <td><asp:DropDownList ID="comboParentFaculty" runat="server"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align:center"><asp:Button ID="bAddCourse" runat="server" Text="Add Course" OnClick="bAddCourse_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <br /><br />
        <fieldset>
            <legend>Remove Course&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="bHidePanelRemoveCourse" runat="server" Text="Show Panel" CausesValidation="false" OnClick="bHidePanelRemoveCourse_Click" /></legend>
            <asp:Panel runat="server" ID="panelRemoveCourse" Visible="false">
                <table>
                    <tr style="font-size:18px;">
                        <td>Removable Courses: </td>
                        <td><asp:DropDownList ID="comboRemovableCourses" runat="server"></asp:DropDownList></td>
                        <td><asp:Button ID="bRemoveCourse" runat="server" Text="Remove Course" OnClick="bRemoveCourse_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
