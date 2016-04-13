<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="CMREntry.aspx.cs" Inherits="EWSD.Course.CMREntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <style type="text/css">
        .tableStyle{
            text-align:center;
            margin-left:auto;
            margin-right:auto;
            font-size:18px;
        }
    </style>
    <script type="text/javascript">
        function calculateTotalMean() {
            document.getElementById("overallMean").innerHTML = "";
            var total = 0;

            <%if(rowStatCw1.Visible) {%>
            var cw1Mean = parseFloat(((document.getElementById('<%=fieldCw1Mean.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw1Mean.ClientID%>').value));
            total += cw1Mean;
            <%}%>
            <%if(rowStatCw2.Visible) {%>
            var cw2Mean = parseFloat(((document.getElementById('<%=fieldCw2Mean.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw2Mean.ClientID%>').value));
            total += cw2Mean;
            <%}%>
            <%if(rowStatExam.Visible) {%>
            var examMean = parseFloat(((document.getElementById('<%=fieldExamMean.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldExamMean.ClientID%>').value));
            total += examMean;
            <%}%>

            document.getElementById("overallMean").innerHTML = total.toFixed(3);
        }

        function calculateTotalMedian() {
            document.getElementById("overallMedian").innerHTML = "";
            var total = 0;

            <%if(rowStatCw1.Visible) {%>
            var cw1Median = parseFloat(((document.getElementById('<%=fieldCw1Median.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw1Median.ClientID%>').value));
            total += cw1Median;
            <%}%>
            <%if(rowStatCw2.Visible) {%>
            var cw2Median = parseFloat(((document.getElementById('<%=fieldCw2Median.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw2Median.ClientID%>').value));
            total += cw2Median;
            <%}%>
            <%if(rowStatExam.Visible) {%>
            var examMedian = parseFloat(((document.getElementById('<%=fieldExamMedian.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldExamMedian.ClientID%>').value));
            total += examMedian;
            <%}%>
            
            document.getElementById("overallMedian").innerHTML = total.toFixed(3);
        }

        function calculateTotalStdDev() {
            document.getElementById("overallStdDev").innerHTML = "";
            var total = 0;

            <%if(rowStatCw1.Visible) {%>
            var cw1StdDev = parseFloat(((document.getElementById('<%=fieldCw1StdDev.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw1StdDev.ClientID%>').value));
            total += cw1StdDev;
            <%}%>
            <%if(rowStatCw2.Visible) {%>
            var cw2StdDev = parseFloat(((document.getElementById('<%=fieldCw2StdDev.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw2StdDev.ClientID%>').value));
            total += cw2StdDev;
            <%}%>
            <%if(rowStatExam.Visible) {%>
            var examStdDev = parseFloat(((document.getElementById('<%=fieldExamStdDev.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldExamStdDev.ClientID%>').value));
            total += examStdDev;
            <%}%>

            document.getElementById("overallStdDev").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup1() {
            document.getElementById("overallGroup1").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible) {%>
            var cw1Group1 = ((document.getElementById('<%=fieldGddCw1Group1.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group1.ClientID%>').value);
            total += parseFloat(cw1Group1);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group1 = ((document.getElementById('<%=fieldGddCw2Group1.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group1.ClientID%>').value);
            total += parseFloat(cw2Group1);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup1 = ((document.getElementById('<%=fieldGddExamGroup1.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup1.ClientID%>').value);
            total += parseFloat(examGroup1);
            <%}%>

            document.getElementById("overallGroup1").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup2() {
            document.getElementById("overallGroup2").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group2 = ((document.getElementById('<%=fieldGddCw1Group2.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group2.ClientID%>').value);
            total += parseFloat(cw1Group2);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group2 = ((document.getElementById('<%=fieldGddCw2Group2.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group2.ClientID%>').value);
            total += parseFloat(cw2Group2);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup2 = ((document.getElementById('<%=fieldGddExamGroup2.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup2.ClientID%>').value);
            total += parseFloat(examGroup2);
            <%}%>

            document.getElementById("overallGroup2").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup3() {
            document.getElementById("overallGroup3").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group3 = ((document.getElementById('<%=fieldGddCw1Group3.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group3.ClientID%>').value);
            total += parseFloat(cw1Group3);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group3 = ((document.getElementById('<%=fieldGddCw2Group3.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group3.ClientID%>').value);
            total += parseFloat(cw2Group3);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup3 = ((document.getElementById('<%=fieldGddExamGroup3.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup3.ClientID%>').value);
            total += parseFloat(examGroup3);
            <%}%>

            document.getElementById("overallGroup3").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup4() {
            document.getElementById("overallGroup4").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group4 = ((document.getElementById('<%=fieldGddCw1Group4.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group4.ClientID%>').value);
            total += parseFloat(cw1Group4);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group4 = ((document.getElementById('<%=fieldGddCw2Group4.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group4.ClientID%>').value);
            total += parseFloat(cw2Group4);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup4 = ((document.getElementById('<%=fieldGddExamGroup4.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup4.ClientID%>').value);
            total += parseFloat(examGroup4);
            <%}%>

            document.getElementById("overallGroup4").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup5() {
            document.getElementById("overallGroup5").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group5 = ((document.getElementById('<%=fieldGddCw1Group5.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group5.ClientID%>').value);
            total += parseFloat(cw1Group5);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group5 = ((document.getElementById('<%=fieldGddCw2Group5.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group5.ClientID%>').value);
            total += parseFloat(cw2Group5);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup5 = ((document.getElementById('<%=fieldGddExamGroup5.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup5.ClientID%>').value);
            total += parseFloat(examGroup5);
            <%}%>

            document.getElementById("overallGroup5").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup6() {
            document.getElementById("overallGroup6").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group6 = ((document.getElementById('<%=fieldGddCw1Group6.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group6.ClientID%>').value);
            total += parseFloat(cw1Group6);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group6 = ((document.getElementById('<%=fieldGddCw2Group6.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group6.ClientID%>').value);
            total += parseFloat(cw2Group6);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup6 = ((document.getElementById('<%=fieldGddExamGroup6.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup6.ClientID%>').value);
            total += parseFloat(examGroup6);
            <%}%>

            document.getElementById("overallGroup6").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup7() {
            document.getElementById("overallGroup7").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group7 = ((document.getElementById('<%=fieldGddCw1Group7.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group7.ClientID%>').value);
            total += parseFloat(cw1Group7);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group7 = ((document.getElementById('<%=fieldGddCw2Group7.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group7.ClientID%>').value);
            total += parseFloat(cw2Group7);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup7 = ((document.getElementById('<%=fieldGddExamGroup7.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup7.ClientID%>').value);
            total += parseFloat(examGroup7);
            <%}%>

            document.getElementById("overallGroup7").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup8() {
            document.getElementById("overallGroup8").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group8 = ((document.getElementById('<%=fieldGddCw1Group8.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group8.ClientID%>').value);
            total += parseFloat(cw1Group8);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group8 = ((document.getElementById('<%=fieldGddCw2Group8.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group8.ClientID%>').value);
            total += parseFloat(cw2Group8);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup8 = ((document.getElementById('<%=fieldGddExamGroup8.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup8.ClientID%>').value);
            total += parseFloat(examGroup8);
            <%}%>

            document.getElementById("overallGroup8").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup9() {
            document.getElementById("overallGroup9").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group9 = ((document.getElementById('<%=fieldGddCw1Group9.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group9.ClientID%>').value);
            total += parseFloat(cw1Group9);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group9 = ((document.getElementById('<%=fieldGddCw2Group9.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group9.ClientID%>').value);
            total += parseFloat(cw2Group9);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup9 = ((document.getElementById('<%=fieldGddExamGroup9.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup9.ClientID%>').value);
            total += parseFloat(examGroup9);
            <%}%>

            document.getElementById("overallGroup9").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup10() {
            document.getElementById("overallGroup10").innerHTML = "";
            var total = 0;

            <%if(rowGddCw1.Visible){%>
            var cw1Group10 = ((document.getElementById('<%=fieldGddCw1Group10.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group10.ClientID%>').value);
            total += parseFloat(cw1Group10);
            <%}%>
            <%if(rowGddCw2.Visible) {%>
            var cw2Group10 = ((document.getElementById('<%=fieldGddCw2Group10.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group10.ClientID%>').value);
            total += parseFloat(cw2Group10);
            <%}%>
            <%if(rowGddExam.Visible) {%>
            var examGroup10 = ((document.getElementById('<%=fieldGddExamGroup10.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddExamGroup10.ClientID%>').value);
            total += parseFloat(examGroup10);
            <%}%>

            document.getElementById("overallGroup10").innerHTML = total.toFixed(3);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Course Monitoring Report (CMR) Entry</h3>
    <div class="jumbotron">
        <p style="font-size:18px;">
            <span class="text-danger"><asp:Literal ID="literalWarning" runat="server"></asp:Literal></span><br />
            <span class="text-success"><asp:Literal ID="literalActionSuccess" runat="server"></asp:Literal></span>
        </p>
        <asp:Panel ID="panelSelectCourse" runat="server">
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Select your Course: </td>
                    <td><asp:DropDownList ID="comboCourse" runat="server"></asp:DropDownList></td>
                    <td><asp:Button ID="bSelectCourse" runat="server" Text="Select Course" OnClick="bSelectCourse_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="panelCMRHead" runat="server" Visible="false">
            <h3 style="text-align:center;">Course Monitoring Report</h3>
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Academic Year: </td>
                    <td><asp:TextBox ID="fieldAcademicYear" TextMode="Number"  min="2000" step="1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Coursework Title: </td>
                    <td>
                        <asp:DropDownList ID="comboCoursework" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCoursework_SelectedIndexChanged">
                            <asp:ListItem>Select a coursework</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Coursework Code: </td>
                    <td><asp:Literal ID="literalCourseworkCode" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Student Count: </td>
                    <td><asp:TextBox ID="fieldStudentCount" TextMode="Number" min="0" step="1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        <asp:Panel ID="panelCMRBody" runat="server">
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
                    <asp:TableCell><asp:TextBox ID="fieldCw1Mean" runat="server" style="text-align:center;" onchange="calculateTotalMean()"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw1Median" style="text-align:center;" onchange="calculateTotalMedian()" runat="server"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw1StdDev" style="text-align:center;" onchange="calculateTotalStdDev()" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowStatCw2" Visible="false">
                    <asp:TableCell>Coursework 2</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw2Mean" style="text-align:center;" runat="server" onchange="calculateTotalMean()"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw2Median" style="text-align:center;" onchange="calculateTotalMedian()" runat="server"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldCw2StdDev" style="text-align:center;" onchange="calculateTotalStdDev()" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowStatExam" Visible="false">
                    <asp:TableCell>Exam</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldExamMean" style="text-align:center;" runat="server" onchange="calculateTotalMean()"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldExamMedian" style="text-align:center;" onchange="calculateTotalMedian()" runat="server"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldExamStdDev" style="text-align:center;" onchange="calculateTotalStdDev()" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowOverallScore">
                    <asp:TableCell><strong>Overall</strong></asp:TableCell>
                    <asp:TableCell><span id="overallMean"></span></asp:TableCell>
                    <asp:TableCell><span id="overallMedian"></span></asp:TableCell>
                    <asp:TableCell><span id="overallStdDev"></span></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group1" style="text-align:center;" onchange="calculateGddGroup1()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group2" style="text-align:center;" onchange="calculateGddGroup2()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group3" style="text-align:center;" onchange="calculateGddGroup3()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group4" style="text-align:center;" onchange="calculateGddGroup4()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group5" style="text-align:center;" onchange="calculateGddGroup5()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group6" style="text-align:center;" onchange="calculateGddGroup6()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group7" style="text-align:center;" onchange="calculateGddGroup7()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group8" style="text-align:center;" onchange="calculateGddGroup8()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group9" style="text-align:center;" onchange="calculateGddGroup9()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw1Group10" style="text-align:center;" onchange="calculateGddGroup10()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddCw2" Visible="false">
                    <asp:TableCell>Coursework 2</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group1" style="text-align:center;" onchange="calculateGddGroup1()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group2" style="text-align:center;" onchange="calculateGddGroup2()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group3" style="text-align:center;" onchange="calculateGddGroup3()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group4" style="text-align:center;" onchange="calculateGddGroup4()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group5" style="text-align:center;" onchange="calculateGddGroup5()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group6" style="text-align:center;" onchange="calculateGddGroup6()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group7" style="text-align:center;" onchange="calculateGddGroup7()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group8" style="text-align:center;" onchange="calculateGddGroup8()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group9" style="text-align:center;" onchange="calculateGddGroup9()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddCw2Group10" style="text-align:center;" onchange="calculateGddGroup10()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddExam" Visible="false">
                    <asp:TableCell>Exam</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup1" style="text-align:center;" onchange="calculateGddGroup1()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup2" style="text-align:center;" onchange="calculateGddGroup2()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup3" style="text-align:center;" onchange="calculateGddGroup3()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup4" style="text-align:center;" onchange="calculateGddGroup4()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup5" style="text-align:center;" onchange="calculateGddGroup5()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup6" style="text-align:center;" onchange="calculateGddGroup6()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup7" style="text-align:center;" onchange="calculateGddGroup7()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup8" style="text-align:center;" onchange="calculateGddGroup8()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup9" style="text-align:center;" onchange="calculateGddGroup9()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="fieldGddExamGroup10" style="text-align:center;" onchange="calculateGddGroup10()" runat="server" Columns="5"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowGddOverall">
                    <asp:TableCell><strong>Overall</strong></asp:TableCell>
                    <asp:TableCell><span id="overallGroup1"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup2"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup3"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup4"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup5"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup6"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup7"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup8"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup9"></span></asp:TableCell>
                    <asp:TableCell><span id="overallGroup10"></span></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            <h3 style="text-align:center;">General Comments</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>
                        <asp:TextBox id="fieldGeneralComments" TextMode="multiline" Columns="100" Rows="5" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <h3 style="text-align:center;">Action to be taken</h3>
            <table border="1" style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>
                        <asp:TextBox id="fieldActionTaken" TextMode="multiline" Columns="100" Rows="5" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <table style="text-align:center;margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td><asp:Button ID="bSubmitReport" runat="server" Text="Submit Report" OnClick="bSubmitReport_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
