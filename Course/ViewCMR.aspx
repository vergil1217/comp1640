<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="ViewCMR.aspx.cs" Inherits="EWSD.Course.ViewCMR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>View Course Monitoring Report (CMR)</h3>
    <div class="jumbotron">
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalWarning" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <asp:Panel ID="panelSelectAcademicYear" runat="server">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Select Academic Year:</td>
                    <td><asp:DropDownList ID="comboAcademicYear" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bSelectAcademicYear" runat="server" Text="Select Academic Year" OnClick="bSelectAcademicYear_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelSelectReportId" Visible="false">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Select Report ID:</td>
                    <td><asp:DropDownList ID="comboReportId" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bSelectReport" runat="server" Text="Select Report" OnClick="bSelectReport_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelCMRBody" runat="server" Visible="false">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Course Monitoring Report</td>
                </tr>
            </table>
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Academic Year: </td>
                    <td><asp:Literal ID="literalAcademicYear" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Course Title: </td>
                    <td><asp:Literal ID="literalCourseTitle" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Student Count: </td>
                    <td><asp:Literal ID="literalStudentCount" runat="server"></asp:Literal></td>
                </tr>
            </table>
            <h3 style="text-align:center;">Subject List</h3>
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td style="color:blue;">
                        <asp:Literal ID="literalSubjectList" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <h3 style="text-align:center;">Statistical Data</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>&nbsp;</td>
                    <td>Mean</td>
                    <td>Median</td>
                    <td>Standard Deviation</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="fieldCw1" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw1Mean" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw1Median" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw1StdDev" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="fieldCw2" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw2Mean" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw2Median" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw2StdDev" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="fieldCw3" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw3Mean" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw3Median" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw3StdDev" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><strong>Overall</strong></td>
                    <td><asp:Label ID="overallMean" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallMedian" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallStdDev" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <br />
            <h3 style="text-align:center;">Grade Distribution Data</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>&nbsp;</td>
                    <td>0 - 9</td>
                    <td>10- 19</td>
                    <td>20 - 29</td>
                    <td>30 - 39</td>
                    <td>40 - 49</td>
                    <td>50 - 59</td>
                    <td>60 - 69</td>
                    <td>70 - 79</td>
                    <td>80 - 89</td>
                    <td>90+</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="comboGddCw1" runat="server" style="text-align:center;" ReadOnly="true" Columns="10"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group1" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group2" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group3" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group4" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group5" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group6" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group7" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group8" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group9" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group10" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="comboGddCw2" runat="server" style="text-align:center;" ReadOnly="true" Columns="10"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group1" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group2" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group3" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group4" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group5" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group6" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group7" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group8" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group9" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group10" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="comboGddCw3" runat="server" style="text-align:center;" ReadOnly="true" Columns="10"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group1" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group2" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group3" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group4" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group5" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group6" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group7" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group8" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group9" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group10" runat="server" style="text-align:center;" ReadOnly="true" Columns="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><strong>Overall</strong></td>
                    <td><asp:Label ID="overallGroup1" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup2" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup3" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup4" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup5" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup6" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup7" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup8" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup9" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="overallGroup10" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
            <br />
            <h3 style="text-align:center;">General Comments</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>
                        <asp:TextBox id="fieldGeneralComments" TextMode="multiline" Columns="100" Rows="5" runat="server" ReadOnly="true" />
                    </td>
                </tr>
            </table>
            <br />
            <h3 style="text-align:center;">Action to be taken</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>
                        <asp:TextBox id="fieldActionTaken" TextMode="multiline" Columns="100" Rows="5" runat="server" ReadOnly="true" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelDLTComments" runat="server" Visible="false">
            <h3 style="text-align:center;">Director of Learning and Quality Comments</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>
                        <asp:TextBox id="fieldDLTComments" TextMode="multiline" Columns="100" Rows="5" runat="server" ReadOnly="true" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelApproveCMR" runat="server" Visible="false">
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td><asp:Button ID="bApprove" runat="server" Text="Approve CMR" OnClick="bApprove_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
