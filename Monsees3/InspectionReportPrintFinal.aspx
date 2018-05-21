<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectionReportPrintFinal.aspx.cs" Inherits="Monsees.InspectionReportPrintFinal" MasterPageFile="~/MasterPages/Print.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Content" >


<div class="report">
	<div class="report-heading">
		<h4>FINISH:<span> <%=HeaderData.WorkCode %></span></h4>

		<div class="top left">
			<table>
				<tr>
					<td class="label">VENDOR:</td>
					<td>
						<div>MONSEES TOOL &AMP; DIE, INC</div>
						<div>&nbsp;</div>
					</td>
				</tr>
				<tr>
					<td class="label">TO:</td>
					<td>
						<div><%=HeaderData.CompanyName%></div>
						<div><%=HeaderData.StreetAddress%></div>
						<div><%=HeaderData.City%>, <%=HeaderData.State%> <%=HeaderData.PostalCode%></div>
						<div>&nbsp;</div>
					</td>
				</tr>
				<tr>
					<td class="label">Part/Desc:</td>
					<td><div><%=HeaderData.PartNumber %>, <%=HeaderData.DrawingNumber %> - REV. <%=HeaderData.Revision_Number %></div>
						<div>&nbsp;</div>
					</td>
				</tr>
			</table>
		</div>

		<div class="top right">
			<table>
				<tr>
					<td class="label">JOB NUMBER:</td>
					<td><div><%=HeaderData.JobNumber %>
						<div>&nbsp;</div>
					    </div>
						
					</td>
				</tr>
				<tr>
					<td class="label">LOT NUMBER:</td>
					<td><div><%=HeaderData.JobItemID %></div>
						<div>&nbsp;</div>
					</td>
				</tr>
				<tr>
					<td class="label">QUANTITY:</td>
					<td><div><%=HeaderData.Quantity %></div>
						<div>&nbsp;</div>
					</td>
				</tr>
				<tr>
					<td class="label">SERIAL #:</td>
					<td><div><%=String.IsNullOrEmpty(HeaderData.CustSerialNumber) ? HeaderData.InternalSerialNumber : HeaderData.CustSerialNumber %></div>
						<div>&nbsp;</div>
					</td>
				</tr>
			</table>
		</div>

		<div style="clear:both;height:0px;">&nbsp;</div>
	</div>
	<table class="report-body"  >
	<thead>
		<tr>
			<td class="auto-style3">DIM</td>
			<td class="auto-style1">DRAWING DIMENSION</td>			
			<td class="thick-left" style="width: 163px">Final</td>
			<td>Remarks</td>
		</tr>
	</thead>
	<tbody>
		<% foreach(Monsees.DataModel.InspectionReportView r in ReportData) { %>
		<tr>
			<td class="auto-style3"><%=r.DimensionNumber %></td>
			<td class="auto-style2"><%=r.Description %></td>
			
			<td class="thick-left dimension" style="width: 163px"><%=r.FinalMeasurement %></td>
			<td class="remarks"><%=r.Remarks%></td>
		</tr>
		<% } %>
	</tbody>
</table>
</div>



	</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 281px;
        }
        .auto-style2 {
            font-family: Verisurf;
            width: 281px;
        }
        .auto-style3 {
            width: 40px;
        }
    </style>
</asp:Content>

