<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptSchedule.aspx.cs" Inherits="Monsees._Default_Dept" MasterPageFile="~/MasterPages/Monsees.Master" EnableViewState="false" %>
<%@ Register src="~/NestedActiveJobsCtrl.ascx" tagname="NestedActiveJobs" tagprefix="mon" %>
<%@ Register src="~/NestedLoginCtrl.ascx" tagname="NestedLoginControl" tagprefix="mon" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>Custom Department Schedule</title>
    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <link id="lnkStylesheet" href="standard.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 11%;
        }
        
        .atable{
         Border-Color:#999999;
             Border-Style:none;
             Border-Width:1px;
             padding-left:3px;
             font-size: small;
             width:100%;
              
        }

        .atable th {
            background: darkblue;
            
            color: white;
           
        }

        .atable tr {
            background: white;
            
        }

        .atable tr:nth-child(4n) {
            background:#EEEEEE;
        }

        .atable tr:hover {
            background: #DCDCDC;
            cursor: pointer;
        }

        @media print 
        {
            #header, #nav, footer, .noprint
            {
            display: none;
            }

            .style1{
                display:none;
            }

            /* Ensure the content spans the full width */
            #content
            {
            width: 100%; margin: 0; float: none;
            }

            .GridView{
             Border-Color:#999999;
             Border-Style:none;
             Border-Width:1px;
             font-size:xx-small;             
             width:100%;
             padding: inherit;
               
             }

            .alternate{
                background-color:white;
            }

            .rowstyle{
                background-color:white;
                  
            }
            
            /* Change text colour to black (useful for light text on a dark background) */
            .lighttext
            {
            color: #000 
            }

            /* Improve colour contrast of links */
            a:link, a:visited
            {
            color: #781351
            }

            
            @page {
                size:landscape;
                font-size:xx-small;                
                
            }
        }
    </style>


</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="bodyContent" runat="server">


    <div align="center">
    
        <table class="style1" width="100%">
            <tr>
                <td align="right" valign="middle">
                </td>
                <td align="center" valign="bottom">
    
        <asp:Image ID="Image1" runat="server" ImageUrl="images/header01_mac_002.jpg" 
            ImageAlign="Middle" />
                </td>
                
                
            </tr>
            <tr>
                <td align="left" valign="middle">
                    <input type="button" id="Reload" value="Load Report"/></td>
                
                <td align="right">
               <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" Text="Last Refreshed : "></asp:Label>
                    </td>
            </tr>
        </table>
         
              
        <!-- operation choice boxes -->
	<table align="left" width="50%>
<tr BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" align="left"><td colspan="5" align="left">Operation Parameters:</td></tr>
				<tr BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White">		
			<td>
    				<asp:DropDownList ID="Op0" DataSourceID="populateopslist"
					DataValueField="OperationName" AutoPostBack="false" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="Blank" Value="%"></asp:ListItem>
    				</asp:DropDownList></td>
			
			<td align="left">
    				<asp:DropDownList ID="Op1" DataSourceID="populateopslist"
					DataValueField="OperationName" utoPostBack="false" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="Blank" Value="%"></asp:ListItem>
    				</asp:DropDownList></td>
				<td>
    				<asp:DropDownList ID="Op2" DataSourceID="populateopslist"
					DataValueField="OperationName" AutoPostBack="false" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="Blank" Value="%"></asp:ListItem>
    				</asp:DropDownList></td>
				<td>
    				<asp:DropDownList ID="Op3" DataSourceID="populateopslist"
					DataValueField="OperationName" AutoPostBack="false" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="Blank" Value="%"></asp:ListItem>
    				</asp:DropDownList></td>
				<td>
    				<asp:DropDownList ID="Op4" DataSourceID="populateopslist"
					DataValueField="OperationName" AutoPostBack="false" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="Blank" Value="%"></asp:ListItem>
    				</asp:DropDownList></td>
			</tr>
		<tr><td colspan =" 15">
        <div id="maindiv"></div>
       </td></tr> </table>
         <!-- main grid -->
    </div> 

    <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	OperationName From Operation">
    </asp:SqlDataSource>
      
     <script type="text/javascript">

         $(document).ready(function () {

             $("#Reload").click(function () {                 
                 
                 var obj = {};
                 obj.fil1 = $.trim($('#<%= Op0.ClientID %> option:selected').text());
                 obj.fil2 = $.trim($('#<%= Op1.ClientID %> option:selected').text());
                 obj.fil3 = $.trim($('#<%= Op2.ClientID %> option:selected').text());
                 obj.fil4 = $.trim($('#<%= Op3.ClientID %> option:selected').text());
                 obj.fil5 = $.trim($('#<%= Op4.ClientID %> option:selected').text());
                 
                 $.ajax({
                     
                     type: "POST",
                     url: "DeptSchedule.aspx/LoadData",
                     data: JSON.stringify(obj),
                     contentType: "application/json; charset=utf-8",
                     success: function (data) { createTable(data) },
                     error: function (msg) {
                         alert('Failure: ' + msg);
                     },
                     dataType: 'json',
                     async: false

                 });


                 hideColumn(8);
                 hideColumn(17); 
                 hideColumn(18);
                 hideColumn(19);
             });

             $("#maindiv").on('click', '.expander, .inspection', function() {
                 var IDTxt = this.id;
                 var ID = $.trim(IDTxt.split('_')[1]);
                 var source = $.trim(IDTxt.split('_')[0]);
                 var obj = {};
                 obj.id = ID;
                 switch (source) {
                     case "exp":
                         $.ajax({
                             type: "POST",
                             url: "DeptSchedule.aspx/LoadControl",
                             data: JSON.stringify(obj),
                             contentType: "application/json; charset=utf-8",
                             success: function (data) { $("#div" + ID).append(data.d) },
                             error: function (msg) {
                                 alert('Failure: ' + msg);
                             },
                             dataType: 'json',
                             async: false
                         });
                         break;
                     case "insp":
                         window.open("InspectionReportPrint.aspx?JobItemID=" + ID);
                         break;
                     case "dwg":
                         $.ajax({
                             type: "POST",
                             url: "pdfhandler.ashx?FileID=" + ID + "&PartNumber=Test&RevNumber=Test",
                             contentType: "application/octet-stream",
                             success: OnComplete,
                             error: OnFail
                         });
                 }

                 
             });

             
                
             

             
         });

         function OnComplete(result) {
             alert(result);
         }

         function OnFail(result) {
             alert('Drawing Request Failed');
         }

         function hideColumn(column) {
             {
                 $('td:nth-child(' + column + '),th:nth-child( ' + column + ')').hide();

                 //$('tr').find('td:nth-child(' + column + '):contains(4)').parent().css('backgroundColor', 'LightGreen'); // Could be an hexadecimal value as #EE3B3B 
             }
         }



       

         function createTable(data) {
             var objdata = $.parseJSON(data.d);
             
            //var eTable = "";

             // EXTRACT VALUE FOR HTML HEADER. 
            
            var col = [];
            for (var i = 0; i < objdata.length; i++) {
                for (var key in objdata[i]) {
                    if (col.indexOf(key) === -1) {
                        col.push(key);
                    }
                }
            }

             // CREATE DYNAMIC TABLE.   
            var table = document.createElement("table");
            table.className = "atable";
            var filter1drop = $('#<%= Op0.ClientID %> option:selected').text();
             var filter2drop = $('#<%= Op1.ClientID %> option:selected').text();
             var filter3drop = $('#<%= Op2.ClientID %> option:selected').text();
             var filter4drop = $('#<%= Op3.ClientID %> option:selected').text();
             var filter5drop = $('#<%= Op4.ClientID %> option:selected').text();
             // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.
             var hide1 = 0;
             var hide2 = 0;
             var hide3 = 0;
             var hide4 = 0;
             var hide5 = 0;
            var tr = table.insertRow(-1);                   // TABLE ROW.
            var expander = document.createElement("th");
            expander.innerHTML = " ";
            tr.appendChild(expander)
            for (var i = 0; i < col.length; i++) {
                

                switch (col[i]) {
                    case "filter1":
                        var th = document.createElement("th");      // TABLE HEADER.
                        th.innerHTML = filter1drop;
                        if (filter1drop == "Blank") {
                            hide1 = i+2;
                        }
                        break;
                    case "filter2":
                        var th = document.createElement("th");      // TABLE HEADER.
                        th.innerHTML = filter2drop;
                        if (filter2drop == "Blank") {
                            hide2 = i+2;
                        }
                        break;
                    case "filter3":
                        var th = document.createElement("th");      // TABLE HEADER.
                        th.innerHTML = filter3drop;
                        if (filter3drop == "Blank") {
                            hide3 = i+2;
                        }
                        break;
                    case "filter4":
                        var th = document.createElement("th");      // TABLE HEADER.
                        th.innerHTML = filter4drop;
                        if (filter4drop == "Blank") {
                            hide4 = i+2;
                        }
                        break;
                    case "filter5":
                        var th = document.createElement("th");      // TABLE HEADER.
                        th.innerHTML = filter5drop;
                        if (filter5drop == "Blank") {
                            hide5 = i+2;
                        }
                        break;
                    default:
                        var th = document.createElement("th");      // TABLE HEADER.
                        th.innerHTML = col[i];
                }
                tr.appendChild(th);
            }

             // ADD JSON DATA TO THE TABLE AS ROWS.
            for (var i = 0; i < objdata.length; i++) {

                tr = table.insertRow(-1);
                var expandercell = tr.insertCell(-1);                
                expandercell.innerHTML = '<a href"#" class="expander" id="exp_' + objdata[i].JobItemID + '">+/-</a>';
               
                for (var j = 0; j < col.length; j++) {
                    var tabCell = tr.insertCell(-1);
                    tabCell.innerHTML = objdata[i][col[j]];
                }
                
                var reportcell = tr.insertCell(-1);
                reportcell.innerHTML = '<a href="#" class="inspection" id="insp_' + objdata[i].JobItemID + '">Report</a>';
                var reportcell = tr.insertCell(-1);
                reportcell.innerHTML = '<a href="#" class="inspection" id="dwg_' + objdata[i].RevisionID + '">Drawing</a>';
                tr = table.insertRow(-1);
                var divcell = tr.insertCell(-1)
                var ctrl = "";                
                divcell.innerHTML = '<div id="div' + objdata[i].JobItemID + '"></div>';
                divcell.colSpan = 18;
            }

             // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.
            var divContainer = document.getElementById("maindiv");
            divContainer.innerHTML = "";
            divContainer.appendChild(table);
            if (hide1 >> 0) {
                hideColumn(hide1);
            }
            if (hide2 >> 0) {
                hideColumn(hide2);
            }
            if (hide3 >> 0) {
                hideColumn(hide3);
            }
            if (hide4 >> 0) {
                hideColumn(hide4);
            }
            if (hide5 >> 0) {
                hideColumn(hide5);
            }

         }

        

         

         function quickfixture(jobsetupid) {
             window.open("QuickFixture.aspx?SourceSetup=" + jobsetupid)
         }

         function setupsheet(jobsetupid) {
             window.open("SetupSheet.aspx?JobSetupID=" + jobsetupid)
         }

    </script>
</asp:Content>