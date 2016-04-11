<%@ Page Title="" Language="C#" MasterPageFile="~/EWSD.Master" AutoEventWireup="true" CodeBehind="CMREntry.aspx.cs" Inherits="EWSD.Course.CMREntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/css/Site.css" />
    <script type="text/javascript">
        function calculateTotalMean() {
            document.getElementById("overallMean").innerHTML = "";
            var cw1Mean = parseFloat(((document.getElementById('<%=fieldCw1Mean.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw1Mean.ClientID%>').value));
            var cw2Mean = parseFloat(((document.getElementById('<%=fieldCw2Mean.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw2Mean.ClientID%>').value));
            var cw3Mean = parseFloat(((document.getElementById('<%=fieldCw3Mean.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw3Mean.ClientID%>').value));
            var total = cw1Mean + cw2Mean + cw3Mean;

            document.getElementById("overallMean").innerHTML = total.toFixed(3);
        }

        function calculateTotalMedian() {
            document.getElementById("overallMedian").innerHTML = "";
            var cw1Median = parseFloat(((document.getElementById('<%=fieldCw1Median.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw1Median.ClientID%>').value));
            var cw2Median = parseFloat(((document.getElementById('<%=fieldCw2Median.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw2Median.ClientID%>').value));
            var cw3Median = parseFloat(((document.getElementById('<%=fieldCw3Median.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw3Median.ClientID%>').value));
            var total = cw1Median + cw2Median + cw3Median;

            document.getElementById("overallMedian").innerHTML = total.toFixed(3);
        }

        function calculateTotalStdDev() {
            document.getElementById("overallStdDev").innerHTML = "";
            var cw1StdDev = parseFloat(((document.getElementById('<%=fieldCw1StdDev.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw1StdDev.ClientID%>').value));
            var cw2StdDev = parseFloat(((document.getElementById('<%=fieldCw2StdDev.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw2StdDev.ClientID%>').value));
            var cw3StdDev = parseFloat(((document.getElementById('<%=fieldCw3StdDev.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldCw3StdDev.ClientID%>').value));
            var total = cw1StdDev + cw2StdDev + cw3StdDev;

            document.getElementById("overallStdDev").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup1() {
            document.getElementById("overallGroup1").innerHTML = "";
            var cw1Group1 = ((document.getElementById('<%=fieldGddCw1Group1.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group1.ClientID%>').value);
            var cw2Group1 = ((document.getElementById('<%=fieldGddCw2Group1.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group1.ClientID%>').value);
            var cw3Group1 = ((document.getElementById('<%=fieldGddCw3Group1.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group1.ClientID%>').value);
            var total = parseFloat(cw1Group1) + parseFloat(cw2Group1) + parseFloat(cw3Group1);

            document.getElementById("overallGroup1").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup2() {
            document.getElementById("overallGroup2").innerHTML = "";
            var cw1Group2 = ((document.getElementById('<%=fieldGddCw1Group2.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group2.ClientID%>').value);
            var cw2Group2 = ((document.getElementById('<%=fieldGddCw2Group2.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group2.ClientID%>').value);
            var cw3Group2 = ((document.getElementById('<%=fieldGddCw3Group2.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group2.ClientID%>').value);
            var total = parseFloat(cw1Group2) + parseFloat(cw2Group2) + parseFloat(cw3Group2);

            document.getElementById("overallGroup2").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup3() {
            document.getElementById("overallGroup3").innerHTML = "";
            var cw1Group3 = ((document.getElementById('<%=fieldGddCw1Group3.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group3.ClientID%>').value);
            var cw2Group3 = ((document.getElementById('<%=fieldGddCw2Group3.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group3.ClientID%>').value);
            var cw3Group3 = ((document.getElementById('<%=fieldGddCw3Group3.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group3.ClientID%>').value);
            var total = parseFloat(cw1Group3) + parseFloat(cw2Group3) + parseFloat(cw3Group3);

            document.getElementById("overallGroup3").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup4() {
            document.getElementById("overallGroup4").innerHTML = "";
            var cw1Group4 = ((document.getElementById('<%=fieldGddCw1Group4.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group4.ClientID%>').value);
            var cw2Group4 = ((document.getElementById('<%=fieldGddCw2Group4.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group4.ClientID%>').value);
            var cw3Group4 = ((document.getElementById('<%=fieldGddCw3Group4.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group4.ClientID%>').value);
            var total = parseFloat(cw1Group4) + parseFloat(cw2Group4) + parseFloat(cw3Group4);

            document.getElementById("overallGroup4").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup5() {
            document.getElementById("overallGroup5").innerHTML = "";
            var cw1Group5 = ((document.getElementById('<%=fieldGddCw1Group5.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group5.ClientID%>').value);
            var cw2Group5 = ((document.getElementById('<%=fieldGddCw2Group5.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group5.ClientID%>').value);
            var cw3Group5 = ((document.getElementById('<%=fieldGddCw3Group5.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group5.ClientID%>').value);
            var total = parseFloat(cw1Group5) + parseFloat(cw2Group5) + parseFloat(cw3Group5);

            document.getElementById("overallGroup5").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup6() {
            document.getElementById("overallGroup6").innerHTML = "";
            var cw1Group6 = ((document.getElementById('<%=fieldGddCw1Group6.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group6.ClientID%>').value);
            var cw2Group6 = ((document.getElementById('<%=fieldGddCw2Group6.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group6.ClientID%>').value);
            var cw3Group6 = ((document.getElementById('<%=fieldGddCw3Group6.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group6.ClientID%>').value);
            var total = parseFloat(cw1Group6) + parseFloat(cw2Group6) + parseFloat(cw3Group6);

            document.getElementById("overallGroup6").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup7() {
            document.getElementById("overallGroup7").innerHTML = "";
            var cw1Group7 = ((document.getElementById('<%=fieldGddCw1Group7.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group7.ClientID%>').value);
            var cw2Group7 = ((document.getElementById('<%=fieldGddCw2Group7.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group7.ClientID%>').value);
            var cw3Group7 = ((document.getElementById('<%=fieldGddCw3Group7.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group7.ClientID%>').value);
            var total = parseFloat(cw1Group7) + parseFloat(cw2Group7) + parseFloat(cw3Group7);

            document.getElementById("overallGroup7").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup8() {
            document.getElementById("overallGroup8").innerHTML = "";
            var cw1Group8 = ((document.getElementById('<%=fieldGddCw1Group8.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group8.ClientID%>').value);
            var cw2Group8 = ((document.getElementById('<%=fieldGddCw2Group8.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group8.ClientID%>').value);
            var cw3Group8 = ((document.getElementById('<%=fieldGddCw3Group8.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group8.ClientID%>').value);
            var total = parseFloat(cw1Group8) + parseFloat(cw2Group8) + parseFloat(cw3Group8);

            document.getElementById("overallGroup8").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup9() {
            document.getElementById("overallGroup9").innerHTML = "";
            var cw1Group9 = ((document.getElementById('<%=fieldGddCw1Group9.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group9.ClientID%>').value);
            var cw2Group9 = ((document.getElementById('<%=fieldGddCw2Group9.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group9.ClientID%>').value);
            var cw3Group9 = ((document.getElementById('<%=fieldGddCw3Group9.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group9.ClientID%>').value);
            var total = parseFloat(cw1Group9) + parseFloat(cw2Group9) + parseFloat(cw3Group9);

            document.getElementById("overallGroup9").innerHTML = total.toFixed(3);
        }

        function calculateGddGroup10() {
            document.getElementById("overallGroup10").innerHTML = "";
            var cw1Group10 = ((document.getElementById('<%=fieldGddCw1Group10.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw1Group10.ClientID%>').value);
            var cw2Group10 = ((document.getElementById('<%=fieldGddCw2Group10.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw2Group10.ClientID%>').value);
            var cw3Group10 = ((document.getElementById('<%=fieldGddCw3Group10.ClientID%>').value === "") ? 0 : document.getElementById('<%=fieldGddCw3Group10.ClientID%>').value);
            var total = parseFloat(cw1Group10) + parseFloat(cw2Group10) + parseFloat(cw3Group10);

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
        <asp:Panel ID="panelCMRHead" runat="server">
            <h3 style="text-align:center;">Course Monitoring Report</h3>
            <table style="margin-left:auto;margin-right:auto;font-size:18px;">
                <tr>
                    <td>Academic Year: </td>
                    <td><asp:TextBox ID="fieldAcademicYear" TextMode="Number"  min="2000" step="1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Course Title: </td>
                    <td><asp:DropDownList ID="comboCourses" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCourses_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Course Code: </td>
                    <td><asp:Literal ID="literalCourseCode" runat="server"></asp:Literal></td>
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
                    <td><asp:DropDownList ID="comboCw1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCw1_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="fieldCw1Mean" runat="server" style="text-align:center;" onchange="calculateTotalMean()"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw1Median" style="text-align:center;" onchange="calculateTotalMedian()" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw1StdDev" style="text-align:center;" onchange="calculateTotalStdDev()" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:DropDownList ID="comboCw2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCw2_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="fieldCw2Mean" style="text-align:center;" runat="server" onchange="calculateTotalMean()"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw2Median" style="text-align:center;" onchange="calculateTotalMedian()" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw2StdDev" style="text-align:center;" onchange="calculateTotalStdDev()" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:DropDownList ID="comboCw3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCw3_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="fieldCw3Mean" style="text-align:center;" runat="server" onchange="calculateTotalMean()"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw3Median" style="text-align:center;" onchange="calculateTotalMedian()" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldCw3StdDev" style="text-align:center;" onchange="calculateTotalStdDev()" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><strong>Overall</strong></td>
                    <td><span id="overallMean"></span></td>
                    <td><span id="overallMedian"></span></td>
                    <td><span id="overallStdDev"></span></td>
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
                    <td><asp:DropDownList ID="comboGddCw1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCw1_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="fieldGddCw1Group1" style="text-align:center;" onchange="calculateGddGroup1()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group2" style="text-align:center;" onchange="calculateGddGroup2()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group3" style="text-align:center;" onchange="calculateGddGroup3()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group4" style="text-align:center;" onchange="calculateGddGroup4()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group5" style="text-align:center;" onchange="calculateGddGroup5()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group6" style="text-align:center;" onchange="calculateGddGroup6()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group7" style="text-align:center;" onchange="calculateGddGroup7()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group8" style="text-align:center;" onchange="calculateGddGroup8()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group9" style="text-align:center;" onchange="calculateGddGroup9()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw1Group10" style="text-align:center;" onchange="calculateGddGroup10()" runat="server" Columns="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:DropDownList ID="comboGddCw2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCw2_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="fieldGddCw2Group1" style="text-align:center;" onchange="calculateGddGroup1()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group2" style="text-align:center;" onchange="calculateGddGroup2()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group3" style="text-align:center;" onchange="calculateGddGroup3()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group4" style="text-align:center;" onchange="calculateGddGroup4()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group5" style="text-align:center;" onchange="calculateGddGroup5()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group6" style="text-align:center;" onchange="calculateGddGroup6()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group7" style="text-align:center;" onchange="calculateGddGroup7()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group8" style="text-align:center;" onchange="calculateGddGroup8()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group9" style="text-align:center;" onchange="calculateGddGroup9()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw2Group10" style="text-align:center;" onchange="calculateGddGroup10()" runat="server" Columns="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:DropDownList ID="comboGddCw3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="comboCw3_SelectedIndexChanged"></asp:DropDownList></td>
                    <td><asp:TextBox ID="fieldGddCw3Group1" style="text-align:center;" onchange="calculateGddGroup1()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group2" style="text-align:center;" onchange="calculateGddGroup2()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group3" style="text-align:center;" onchange="calculateGddGroup3()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group4" style="text-align:center;" onchange="calculateGddGroup4()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group5" style="text-align:center;" onchange="calculateGddGroup5()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group6" style="text-align:center;" onchange="calculateGddGroup6()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group7" style="text-align:center;" onchange="calculateGddGroup7()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group8" style="text-align:center;" onchange="calculateGddGroup8()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group9" style="text-align:center;" onchange="calculateGddGroup9()" runat="server" Columns="5"></asp:TextBox></td>
                    <td><asp:TextBox ID="fieldGddCw3Group10" style="text-align:center;" onchange="calculateGddGroup10()" runat="server" Columns="5"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><strong>Overall</strong></td>
                    <td><span id="overallGroup1"></span></td>
                    <td><span id="overallGroup2"></span></td>
                    <td><span id="overallGroup3"></span></td>
                    <td><span id="overallGroup4"></span></td>
                    <td><span id="overallGroup5"></span></td>
                    <td><span id="overallGroup6"></span></td>
                    <td><span id="overallGroup7"></span></td>
                    <td><span id="overallGroup8"></span></td>
                    <td><span id="overallGroup9"></span></td>
                    <td><span id="overallGroup10"></span></td>
                </tr>
            </table>
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
