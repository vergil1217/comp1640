<%@ Page Title="Register" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EWSD.Admin.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="literalErrorMessage" />
    </p>
    <p class="text-success">
        <asp:Literal runat="server" ID="literalSuccessMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldUsername" CssClass="col-md-2 control-label">User Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldUsername" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldUsername" CssClass="text-danger" Display="Dynamic" ErrorMessage="The user name field is required." />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="fieldUsername" CssClass="text-danger" Display="Dynamic" ErrorMessage="Username needs a minimum length of 6 characters." ValidationExpression=".{6}.*" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldPassword" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="The password field is required." />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="fieldPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="Password requires a minimum length of 6 characters." ValidationExpression=".{6}.*" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldConfirmPassword" CssClass="col-md-2 control-label">Confirm Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="fieldPassword" ControlToValidate="fieldConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldFirstName" CssClass="col-md-2 control-label">First Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldFirstName" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldFirstName"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Your first name is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldLastName" CssClass="col-md-2 control-label">Last Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldLastName" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldLastName"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Your last name is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldEmail" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldEmail" TextMode="Email" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldEmail"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Your email is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="comboSecurityQuestion" CssClass="col-md-2 control-label">Security Question</asp:Label>
            <div class="col-md-10">
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
                <asp:RequiredFieldValidator runat="server" ControlToValidate="comboSecurityQuestion"
                    CssClass="text-danger" Display="Dynamic" InitialValue="Select a question" ErrorMessage="Please select a question." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fieldSecurityAnswer" CssClass="col-md-2 control-label">Security Answer</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="fieldSecurityAnswer" TextMode="SingleLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="fieldSecurityAnswer"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Security answer is required." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
