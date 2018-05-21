<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="POAudit.aspx.cs" Inherits="Monsees.POAudit" MasterPageFile="~/MasterPages/Print.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Content" >
    <style>
        .report-heading, .report-body, .report {
		width:12in;
	}
        @media print {

		body {
			margin: 0px;
			padding: 0px;
			width: 10in;
		}

		 @page {
		  size: landscape;
		  margin: auto;
		}
	}
    </style>

<div class="report">
	

		<div style="clear:both;height:0px;">&nbsp;</div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" EnableModelValidation="True">
            <Columns>
                <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" SortExpression="ContactName" />
                <asp:BoundField DataField="MinOfCurrDelivery" HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" />
                <asp:BoundField DataField="JobNumber" HeaderText="Job #" SortExpression="JobNumber" />                
                <asp:BoundField DataField="PONumber" HeaderText="PO #" SortExpression="PONumber" />
                <asp:BoundField DataField="PmtTerm" HeaderText="Pmt Terms" SortExpression="PmtTerm" />
                <asp:BoundField DataField="PODate" HeaderText="Order Date" SortExpression="PODate" DataFormatString="{0:MM-dd-yyyy}"  />
                <asp:BoundField DataField="Total" HeaderText="Total Order" SortExpression="Total"  DataFormatString="${0:#,0}" />
                <asp:BoundField DataField="ShippedTotal" HeaderText="Shipped" SortExpression="ShippedTotal" DataFormatString="${0:#,0}" />
                <asp:BoundField DataField="Invoiced_Total" HeaderText="Invoiced" SortExpression="Invoiced_Total" DataFormatString="${0:#,0}" />
                <asp:BoundField DataField="QBRecord" HeaderText="QB Record" SortExpression="QBRecord" DataFormatString="${0:#,0}" />
            </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnSelecting="SqlDataSource1_Selecting" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [CompanyName], [ContactName], [MinOfCurrDelivery], [JobNumber], [MaxOfMaxOfShipped], [PONumber], [PmtTerm], [PODate], [Total], [Invoiced Total] AS Invoiced_Total, [ShippedTotal], [QBRecord] FROM [POAudit]"></asp:SqlDataSource>
        



	</div>
	




	</asp:Content>
