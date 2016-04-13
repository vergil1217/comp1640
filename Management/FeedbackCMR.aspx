<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="FeedbackCMR.aspx.cs" Inherits="EWSD.Management.FeedbackCMR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tableStyle{
            text-align:center;
            margin-left:auto;
            margin-right:auto;
            font-size:18px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Feedback Course Monitoring Report (CMR)</h3>
    <div class="jumbotron">
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalWarning" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <asp:Panel ID="panelSelectFaculty" runat="server">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Select Faculty: </td>
                    <td><asp:DropDownList ID="comboFaculties" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bSelectFaculty" runat="server" Text="Select Faculty" OnClick="bSelectFaculty_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelSelectAcademicYear" runat="server" Visible="false">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Select Academic Year:</td>
                    <td><asp:DropDownList ID="comboAcademicYear" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bSelectAcademicYear" runat="server" Text="Select Academic Year" OnClick="bSelectAcademicYear_Click"/></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="panelSelectReportId" Visible="false">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Select Report ID:</td>
                    <td><asp:DropDownList ID="comboReportId" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bSelectReport" runat="server" Text="Select Report" OnClick="bSelectReport_Click"/></td>
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
                    <td>Coursework Title: </td>
                    <td><asp:Literal ID="literalCourseworkTitle" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Student Count: </td>
                    <td><asp:Literal ID="literalStudentCount" runat="server"></asp:Literal></td>
                </tr>
            </table>
            <h3 style="text-align:center;">Assessment List</h3>
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td style="color:blue;">
                        <asp:Literal ID="literalAssessmentList" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
            <br />
            <h3 style="text-align:center;">Statistical Data</h3>
            <asp:Table ID="tableStatData" runat="server" BorderStyle="Solid" GridLines="Both" CssClass="tableStyle">
                <asp:TableRow runat="server" ID="rowHeader">
                    <asp:TableCell>&nbsp;</asp:TableCell>
                    <asp:TableCell>Mean</asp:TableCell>
                    <asp:TableCell>Median</asp:TableCell>
                    <asp:TableCell>Standard Deviation</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowStatCw1" Visible="false">
                    <asp:TableCell>Coursework 1</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw1Mean" runat="server" style="text-align:center;" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw1Median" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw1StdDev" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowStatCw2" Visible="false">
                    <asp:TableCell>Coursework 2</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw2Mean" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw2Median" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw2StdDev" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowStatExam" Visible="false">
                    <asp:TableCell>Exam</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldExamMean" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldExamMedian" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldExamStdDev" style="text-align:center;" runat="server" ReadOnly="true"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowOverallScore">
                    <asp:TableCell><strong>Overall</strong></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallMean" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallMedian" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallStdDev" runat="server" Text=""></asp:Label></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <h3 style="text-align:center;">Grade Distribution Data</h3>
            <asp:Table ID="tableGradeDistData" runat="server" BorderStyle="Solid" GridLines="Both" CssClass="tableStyle">
                <asp:TableRow runat="server" ID="rowGddHeader">
                    <asp:TableCell>&nbsp;</asp:TableCell>
                    <asp:TableCell>0 - 9</asp:TableCell>
                    <asp:TableCell>10 - 19</asp:TableCell>
                    <asp:TableCell>20 - 29</asp:TableCell>
                    <asp:TableCell>30 - 39</asp:TableCell>
                    <asp:TableCell>40 - 49</asp:TableCell>
                    <asp:TableCell>50 - 59</asp:TableCell>
                    <asp:TableCell>60 - 69</asp:TableCell>
                    <asp:TableCell>70 - 79</asp:TableCell>
                    <asp:TableCell>80 - 89</asp:TableCell>
                    <asp:TableCell>90+</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddCw1" Visible="false">
                    <asp:TableCell>Coursework 1</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group1" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group2" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group3" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group4" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group5" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group6" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group7" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group8" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group9" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group10" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddCw2" Visible="false">
                    <asp:TableCell>Coursework 2</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group1" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group2" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group3" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group4" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group5" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group6" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group7" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group8" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group9" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group10" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddExam" Visible="false">
                    <asp:TableCell>Exam</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup1" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup2" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup3" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup4" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup5" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup6" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup7" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup8" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup9" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup10" style="text-align:center;" runat="server" Columns="5" ReadOnly="true"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddOverall">
                    <asp:TableCell><strong>Overall</strong></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup1" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup2" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup3" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup4" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup5" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup6" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup7" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup8" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup9" runat="server" Text=""></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="overallGroup10" runat="server" Text=""></asp:Label></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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
                        <asp:TextBox id="fieldDLTComments" TextMode="multiline" Columns="100" Rows="5" runat="server"/>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td><asp:Button ID="bSubmitDLTComment" runat="server" Text="Submit Feedback" OnClick="bSubmitDLTComment_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
