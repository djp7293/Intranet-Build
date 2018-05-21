<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Print.master" AutoEventWireup="true" CodeBehind="Label.aspx.cs" Inherits="Monsees.Reports.Label" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

	<style>
		body
		{
			width:8.5in;
			margin:10px;
			font-family:Arial;
            vertical-align: text-top;
		}



		.label
		{
			font-weight:bold;
			margin-left:8pt;

		}

        @page { size:9in 5.5in; margin: 50px 0px 0px 0px }

		@media print
		{
			.page-break	
            { 
                display: block; 
                page-break-before: always; 

			}
            body
			{
				margin:0px;
			}
		}

		.left {
			float:left;
			width:4.2in;
		}

		.right {
			float:right;
			width:4.2in;
		}

		.border-dotted
		{
			border-left:1px dashed;
		}

		.right {
			float:right;
			width:50%;
		}

		.col-label {
			width:1.20in;
			margin-right:10pt;
		}

		.col-label.right-label {
			width:0.9in;
			text-align:right;

		}

		.right-label span
		{
						margin-right:10pt;
		}

		.label-table table{
			border-collapse:collapse;
			width:100%;
		}

		td {
			height:0.28in;
			padding:0px;
			margin:0px;
			vertical-align:middle;
		}

		.delivery td
		{
			height:10pt;
		}

		tr.v-seperator td {
			border-bottom:1px solid;
		}

		tr.v-seperator.top td
		{
			border-top:1px solid;
			border-bottom:none;
		}

		table.left {
			width:50%;
		}

		table.right
		{
			width:50%;
			float:right;
			font-size:8pt;
			text-align:center;
		}

		table.right thead
		{
			font-weight:bold;
			text-align:center;
			text-decoration:underline;
		}

		.divider {
			border-top:1px dashed;
			height:2px;
			width:100%;
			clear:both;
		}

		.small-label {
			border-bottom:1px dashed;
		}

		.small-label .container{
			margin:5pt;
			margin-bottom:20pt;
		}


		.small-label  h2 {
			margin:0;
		}

		.small-label .part-number {
			float:left;
			font-size:30pt;
			font-weight:bold;
		}

		.small-label .qty {
			float:right;
			font-size:30pt;
			font-weight:bold;

		}
		 
	    .auto-style1 {
            width: 135px;
        }
        .auto-style2 {
            width: 139px;
        }
		 
	</style>

	<%foreach(Montsees.Data.DataModel.LabelModel LabelModel in LabelModelList ) {%>

	<div class="label-table">
        
		<div class="left">
			<table>
				<tr class="v-seperator">
					<td colspan="1" class="col-label" ><span class="label" >Company</span></td>
					<td  colspan="1" class="auto-style1"  ><span><%=LabelModel.CAbbr %></span></td>
                    <td colspan="2"><div runat="server" id="ITARInvTag"><strong>ITAR Controlled</strong></div></td>
				</tr>
				<tr>
					<td class="col-label"><span class="label">Part Number</span></td>
					<td class="auto-style1"><span><%=LabelModel.PartNumber %></span></td>
					<td class="col-label right-label"><span class="label">Rev. #</span></td>
					<td><span><%=LabelModel.RevisionNumber %></span></td>
				</tr>
				<tr>
					<td colspan="1" class="col-label" ><span class="label" >Description</span></td>
					<td  colspan="3"  ><span><%=LabelModel.DrawingNumber %></span></td>
				</tr>
			</table>

			<table>
				<tr>
					<td class="col-label">
						<span class="label">Lot Number</span>
					</td>
					<td>
						<span><%=LabelModel.JobItemID %></span>
					</td>
					<td class="col-label right-label">
						<span class="label">Status</span>
					</td>
					<td>
						<span><%=LabelModel.InvStatus %></span>
					</td>
				</tr>
			    <tr>
					<td class="col-label">
						<span class="label">Job Number</span>
					</td>
					<td>
						<span><%=LabelModel.JobItemID %></span>
					</td>
					<td class="col-label right-label">
						<span class="label">Location</span>
					</td>
					<td>
						<span><%=LabelModel.Location1 %></span>
					</td>
				</tr>
				<tr  class="v-seperator top">
					<td class="col-label"><span class="label qty">Total QTY</span> </td>
					<td colspan="3" class="qty"><span><%=LabelModel.Quantity %></span></td>
				</tr>
				 <tr>
					<td class="col-label"><span class="label">Note</span> </td>
					<td colspan="3"><span><%=LabelModel.Note1 %></span></td>
				</tr>
				 <tr>
					<td class="col-label"><span class="label">Inventory</span> </td>
					<td colspan="3"><span><%=LabelModel.InventoryID %></span></td>
				</tr>
			</table>
		</div>

		<div class="right  border-dotted">
			<table>
				<tr class="v-seperator">
					<td colspan="1" class="col-label" ><span class="label" >Company</span></td>
					<td  colspan="1" class="auto-style2"  ><span><%=LabelModel.CAbbr %></span></td>
                    <td colspan="2"><div runat="server" id="ITARShipTag"><strong>ITAR Controlled</strong></div></td>
				</tr>
				<tr>
					<td class="col-label"><span class="label">Part Number</span></td>
					<td class="auto-style2"><span><%=LabelModel.PartNumber %></span></td>
					<td class="col-label right-label"><span class="label">Rev. #</span></td>
					<td><span><%=LabelModel.RevisionNumber %></span></td>
				</tr>
				<tr>
					<td colspan="1" class="col-label" ><span class="label" >Description</span></td>
					<td  colspan="3"  ><span><%=LabelModel.DrawingNumber %></span></td>
				</tr>
			</table>

			<div>
				<table class="left">
					<tr>
						<td class="col-label">
							<span class="label">Lot Number</span>
						</td>
						<td>
							<span><%=JobItemID %></span>
						</td>					
					</tr>
					<tr>
						<td class="col-label">
							<span class="label">Job Number</span>
						</td>
						<td>
							<span><%=LabelModel.JobNumber %></span>
						</td>
					</tr>
					<tr >
						<td class="col-label"><span class="label qty">QTY</span> </td>
						<td colspan="3" class="qty"><span><%=LabelModel.SumOfQuantity %></span></td>
					</tr>
					 <tr>
						<td class="col-label"> </td>
						<td colspan="3"></td>
					</tr>
					 <tr>
						<td class="col-label"> </td>
						<td colspan="3"></td>
					</tr>
				</table>
				<table class="right delivery"  >
					<thead>
						<tr>
							<td>Qty</td>
							<td>Delivery</td>
							<td>PO Number</td>
							<td>Ship'd</td>
						</tr>
					</thead>
					<% foreach (Montsees.Data.DataModel.LabelDeliveryItem i in this.DeliveryItems) { %>
							<tr>
							<td><%=i.SumOfQuantity %></td>
							<td><%=i.CurrDelivery.ToShortDateString() %></td>
							<td><%=i.PONumber %></td>
							<td><%=i.Shipped ? "Yes" : "No" %></td>
						</tr>
					<%} %>
			 
				</table>
			</div>
		</div>

		<div class="divider">&nbsp;</div>

		<div class="right border-dotted small-label">
			<div class="container">
				<table><tr><td><h2><%=LabelModel.CAbbr %></h2></td><td><div runat="server" id="ITARLabel"><strong>ITAR Controlled</strong> </div></td></tr></table>
				<div class="part-number"><%=LabelModel.PartNumber %></div>
				<div class="qty"><%=LabelModel.RTSQuantity %></div>
			</div>
		</div>

	</div>
    <div class="page-break"></div>
	<% } %>

</asp:Content>
