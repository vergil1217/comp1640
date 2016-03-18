<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="EWSD.Account.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Forgot Password</h3>
    <div class="jumbotron">
        <p class="text-success">
            <asp:Literal ID="literalActionSuccessful" runat="server"></asp:Literal>
        </p>
        <p class="text-danger">
            <asp:Literal ID="literalActionFailure" runat="server"></asp:Literal>
        </p><br />
        <asp:Panel runat="server" ID="panelSecQA">
            <p>
                Please provide your security question and answer.
            </p><br />
            <table>
                <tr style="font-size:18px;">
                    <td>Username:</td>
                    <td><asp:TextBox ID="fieldUsername" runat="server" Width="100%"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldUsername" CssClass="text-danger" Display="Dynamic" ErrorMessage="Username is required." /></td>
                </tr>
                <tr style="font-size:18px;">
                    <td>Security Question:</td>
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
                    <td>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="comboSecurityQuestion" CssClass="text-danger" Display="Dynamic" InitialValue="Select a question" ErrorMessage="Please select a question." />
                    </td>
                </tr>
                <tr style="font-size:18px;">
                    <td>Security Answer:</td>
                    <td><asp:TextBox ID="fieldSecurityAnswer" runat="server" Width="100%"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldSecurityAnswer" CssClass="text-danger" Display="Dynamic" ErrorMessage="Security answer is required." /></td>
                    <td><asp:Button ID="bAuthSecQA" runat="server" Text="Submit" OnClick="bAuthSecQA_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelResetPassword" Visible="false">
            <p>
                Please enter your new password.
            </p><br />
            <table>
                <tr style="font-size:18px;">
                    <td>New Password:</td>
                    <td><asp:TextBox ID="fieldNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator runat="server" ControlToValidate="fieldNewPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Password is required." /></td>
                </tr>
                <tr style="font-size:18px;">
                    <td>Confirm Password:</td>
                    <td><asp:TextBox ID="fieldConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldConfirmNewPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Confirmation password is required." />
                        <asp:CompareValidator runat="server" ControlToCompare="fieldNewPassword" ControlToValidate="fieldConfirmNewPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                    </td>
                </tr>
                <tr style="font-size:18px;">
                    <td></td>
                    <td>
                        <asp:Button ID="bResetPassword" runat="server" Text="Reset Password" OnClick="bResetPassword_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
