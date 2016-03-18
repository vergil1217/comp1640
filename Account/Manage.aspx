<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="EWSD.Account.Manage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />

    <script runat="server">
        protected void bShowPanelChangePassword_Click(object sender, EventArgs e)
        {
            if (!panelChangePassword.Visible)
            {
                panelChangePassword.Visible = true;
                bShowPanelChangePassword.Text = "Hide Panel";

                panelUpdateSecQA.Visible = false;
                bShowPanelUpdateSecQA.Text = "Show Panel";

                panelUpdateEmail.Visible = false;
                bShowPanelUpdateEmail.Text = "Show Panel";
            }
            else
            {
                panelChangePassword.Visible = false;
                bShowPanelChangePassword.Text = "Show Panel";
            }
        }

        protected void bShowPanelUpdateSecQA_Click(object sender, EventArgs e)
        {
            if (!panelUpdateSecQA.Visible)
            {
                panelUpdateSecQA.Visible = true;
                bShowPanelUpdateSecQA.Text = "Hide Panel";

                panelChangePassword.Visible = false;
                bShowPanelChangePassword.Text = "Show Panel";

                panelUpdateEmail.Visible = false;
                bShowPanelUpdateEmail.Text = "Show Panel";
            }
            else
            {
                panelUpdateSecQA.Visible = false;
                bShowPanelUpdateSecQA.Text = "Show Panel";
            }
        }

        protected void bShowPanelUpdateEmail_Click(object sender, EventArgs e)
        {
            if (!panelUpdateEmail.Visible)
            {
                panelUpdateEmail.Visible = true;
                bShowPanelUpdateEmail.Text = "Hide Panel";

                panelChangePassword.Visible = false;
                bShowPanelChangePassword.Text = "Show Panel";

                panelUpdateSecQA.Visible = false;
                bShowPanelUpdateSecQA.Text = "Show Panel";
            }
            else
            {
                panelUpdateEmail.Visible = false;
                bShowPanelUpdateEmail.Text = "Show Panel";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Account Control Panel</h3>
    <div class="jumbotron">
        <p class="text-success">
            <asp:Literal ID="literalActionSuccessful" runat="server"></asp:Literal>
        </p>
        <fieldset>
            <legend>Change Password&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:18px;"><asp:Button ID="bShowPanelChangePassword" runat="server" OnClick="bShowPanelChangePassword_Click" CausesValidation="false" Text="Hide Panel" /></span></legend>
            <asp:Panel runat="server" ID="panelChangePassword">
                <table>
                    <tr style="font-size:18px;">
                        <td>Current Password:</td>
                        <td><asp:TextBox ID="fieldCurrentPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldCurrentPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Current password is required."></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>New Password:</td>
                        <td><asp:TextBox ID="fieldNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldNewPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="New password is required."></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td>Confirm New Password:</td>
                        <td><asp:TextBox ID="fieldConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldConfirmNewPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Confirmation password is required."></asp:RequiredFieldValidator>
                            <asp:CompareValidator runat="server" ControlToCompare="fieldNewPassword" ControlToValidate="fieldConfirmNewPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="New passwords do not match."></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td></td>
                        <td style="font-size:14px;"><asp:Button ID="bChangePassword" runat="server" Text="Change Password" OnClick="bChangePassword_Click" /></td>
                        <td class="text-danger"><asp:Literal ID="literalPasswordChangeFailure" runat="server"></asp:Literal></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <hr />
        <fieldset>
            <legend>Update Security Question & Answer&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:18px;"><asp:Button ID="bShowPanelUpdateSecQA" runat="server" OnClick="bShowPanelUpdateSecQA_Click" CausesValidation="false" Text="Show Panel" /></span></legend>
            <asp:Panel runat="server" ID="panelUpdateSecQA" Visible="false">
                <table>
                    <tr style="font-size:18px;">
                        <td>
                            <asp:DropDownList ID="comboSecurityQuestion" runat="server">
                                <asp:ListItem>Select a question</asp:ListItem>
                                <asp:ListItem>What is your childhood friend's name?</asp:ListItem>
                                <asp:ListItem>What is your favourite food?</asp:ListItem>
                                <asp:ListItem>Who is your favourite superhero?</asp:ListItem>
                                <asp:ListItem>What is your mother's maiden name?</asp:ListItem>
                                <asp:ListItem>Where did you had your first kiss?</asp:ListItem>
                                <asp:ListItem>What is the name of your secondary school's 3rd Year teacher?</asp:ListItem>
                                <asp:ListItem>Where did you get married?</asp:ListItem>
                                <asp:ListItem>How did you meet your spouse?</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="comboSecurityQuestion" CssClass="text-danger" Display="Dynamic" InitialValue="Select a question" ErrorMessage="Please select a question." /></td>
                    </tr>
                    <tr style="font-size:18px;">
                        <td><asp:TextBox ID="fieldSecurityAnswer" Width="100%" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldSecurityAnswer" CssClass="text-danger" Display="Dynamic" ErrorMessage="Security answer is required." /></td>
                        <td><asp:Button ID="bUpdateSecQA" runat="server" Text="Update Security Q&A" OnClick="bUpdateSecQA_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <hr />
        <fieldset>
            <legend>Update Email&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:18px;"><asp:Button ID="bShowPanelUpdateEmail" runat="server" CausesValidation="false" OnClick="bShowPanelUpdateEmail_Click" Text="Show Panel" /></span></legend>
            <asp:Panel runat="server" ID="panelUpdateEmail" Visible="false">
                <table>
                    <tr style="font-size:18px;">
                        <td>New Email:</td>
                        <td><asp:TextBox ID="fieldNewEmail" runat="server" TextMode="Email"></asp:TextBox></td>
                        <td><td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldNewEmail" CssClass="text-danger" Display="Dynamic" ErrorMessage="Email is required." /></td></td>
                        <td><asp:Button ID="bUpdateEmail" runat="server" Text="Update Email" OnClick="bUpdateEmail_Click" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
