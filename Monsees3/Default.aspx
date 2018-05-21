<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Monsees.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <title>Monsees Tool and Die Intranet</title>
    <style>
        .directory
        {
            margin-left:20px;
            margin-right:20px;
        }
        .directory ul
        {
            list-style:none;
        }

        .directory  ul label
        {
            width:150px;
            display:inline-block;
        }


        .auto-style1 {
            text-decoration: underline;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

   
    <div class="directory">
        <h1>Monsees Tool and Die Intranet</h1>
        <table width="100%"><tr><td width="50%" style="vertical-align:top">
    <div id="productionDirectory" class="production" runat="server">
    <h3><a href="activejobs.aspx">Production</a></h3>	
    <ul >
        <li><label for="employeeHistoryInput">Employee History</label>&nbsp;<asp:DropDownList ID="empoyeeHistoryInput" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="EmployeeID" Height="23px" Width="126px"></asp:DropDownList>
            
        <a href="/employeehistory.aspx?Employee=" class="button go"><span>Go</span></a></li>
        <li><label for="partHistoryInput">Part History</label>&nbsp;<asp:DropDownList ID="parHistoryInput" runat="server" DataSourceID="SqlDataSource2" DataTextField="PartNumber" DataValueField="DetailID" Height="23px" Width="126px"></asp:DropDownList><a href='/parthistory.aspx?DetailId=<%# Eval("partHistoryInput") %>' class="button go"><span>Go</span></a></li>
        <li><label for="innspectionReport">Inspection Report</label>&nbsp;<input id="innspectionReport" type="text" placeholder="Lot #" /><a href="/InspectionReport.aspx?JobItemId=" class="button go"><span>Go</span></a></li>
        
        <li><a href="/DeptSchedule.aspx">Department Schedule</a></li>    
        <li><a href="/shippingreceiving.aspx">Shipping &amp; Receiving</a></li>  
        <li><a href="/toolingsupplypolist.aspx">Tooling &amp; Supplies Orders</a></li>
        <li><a href="/FixtureList.aspx">Fixture Library</a></li>
        <li><a href="/MaterialInventory.aspx">Raw Stock Inventory</a></li>
    </ul>			
	</div>

    <div id="inspectionDirectory" class="inspection" runat="server">
    <h3><a href="/inspection.aspx">Inspection</a></h3>	
    <ul>
        <li><label for="innspectionReport2">Inspection Report</label>&nbsp;<input id="innspectionReport2" type="text" placeholder="Lot #" /><a href="/InspectionReport.aspx?JobItemId=" class="button go"><span>Go</span></a></li>
        <li><label for="lotInput">Lot</label>&nbsp;<input id="lotInput" type="text" placeholder="Lot #" /><a href="/Lot.aspx?id=" class="button go"><span>Go</span></a></li>
        <li><a href="/clearconfirmitemsinsp.aspx">Items for Clear/Confirm</a></li>
           <li><a href="/inspectioninventory.aspx">Inspection Equipment Inventory</a></li>
        <li><a href="/CARLibrary.aspx">CAR Library</a></li>
    </ul>
    </div>
	 
    <div id="shippingDirectory" class="shipping" runat="server">
    <h3><a href="/shippingreceiving.aspx">Shipping &amp; Receiving</a></h3>
        <ul>
            <li><a href="/Inventory.aspx">Raw Stock Inventory</a></li>
        <li><a href="/DeliverySchedule.aspx">Delivery Schedule</a></li>
    </ul>
    </div>

    <div id="officeDirectory" class="shipping" runat="server">
    <h3>Production Management</h3>	
    <ul>
        <li><a href="/monitorops.aspx">Editable Production View</a></li>
        <li><a href="/deliveryschedule.aspx">Delivery Schedule</a></li>
        <li><label for="jobSummaryInput">Job Summary</label>&nbsp;<asp:DropDownList ID="jobSummaryInput" runat="server" DataSourceID="SqlDataSource3" DataTextField="JobNumber" DataValueField="JobID" Height="23px" Width="126px"></asp:DropDownList><a href='/JobSummary.aspx?JobID=<%# Eval("jobSummaryInput") %>' class="button go"><span>Go</span></a></li>
        <li><a href="/clearconfirmitems.aspx">Items for Clear/Confirm</a></li>
        <li><a href="/shippingreceiving.aspx">Shipping / Receiving</a></li>
         <li><a href="/materialpolist.aspx">Material PO list</a></li>
         <li><a href="/subcontractpolist.aspx">Subcontract PO list</a></li>
         <li><a href="/toolingsupplypolist.aspx">Tooling Supply PO list</a></li>
        <li><a href="/searchparts.aspx">Search Parts</a></li>
        <li><a href="/inventory.aspx">Customer Parts Inventory</a></li>

    </ul>
    </div>


 
    <div id="financialDirectory" class="shipping" runat="server">
    <h3>Financial Management</h3>	
    <ul>
        <li><a href="/POAudit.aspx">PO Audit</a></li>
        
    </ul>
    </div>
    </td><td style="vertical-align:top">

        <h3 class="auto-style1">Employee Authorization</h3>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" EnableModelValidation="True">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Employee" SortExpression="Name" />
                        <asp:BoundField DataField="Abbr" HeaderText="Code" SortExpression="Abbr" />
                        <asp:CheckBoxField DataField="ITAR" HeaderText="ITAR" SortExpression="ITAR" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Name], [Abbr], [ITAR] FROM [Employees] WHERE ([Active] = @Active)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="Active" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>


         </td></tr></table>
        </div>
    <script>
        $('.button.go').click(function (e) {
            var t = $(e.currentTarget).prev().val();
            if ((t + '').length > 0) {
                //console.log($(e.currentTarget).attr('href'));
                document.location = $(e.currentTarget).attr('href') + t;
            }
            
                return false;
            
        });
    </script>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [EmployeeID], [Name] FROM [Employees] WHERE ([Active] = @Active) ORDER BY [Name]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="True" Name="Active" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [DetailID], [PartNumber] FROM [Detail] ORDER BY [PartNumber]">
            </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [JobNumber], [JobID] FROM [Job] WHERE ([IsOpen] = @Active) ORDER BY [JobNumber]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="True" Name="Active" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>
</asp:Content>
