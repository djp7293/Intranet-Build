<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectionReportPrint.aspx.cs" Inherits="Monsees.InspectionReportPrint" MasterPageFile="~/MasterPages/Print.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Content" >


<div class="report">
	<div class="report-heading">
		<h4>FINISH:<span> <%=HeaderData.WorkCode %></span></h4>

		<div class="top left" style="width: 64%">
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
			<td>DIM</td>
			<td>DRAWING DIMENSION</td>
			<td>Msmnt 1</td>
			<td class="thick-left">Msmnt 2</td>
			<td>Msmnt 3</td>
			<td class="thick-left">Final</td>
			<td>Remarks</td>
		</tr>
	</thead>
	<tbody>
		<% foreach(Monsees.DataModel.InspectionReportView r in ReportData) { %>
		<tr>
			<td><%=r.DimensionNumber %></td>
			<td class="dimension"><%=r.Description %></td>
			<td class="dimension"><%=r.Measurement1 %></td>
			<td class="thick-left dimension"><%=r.Measurement2 %></td>
			<td class="dimension"><%=r.Measurement3 %></td>
			<td class="thick-left dimension"><%=r.FinalMeasurement %></td>
			<td class="remarks"><%=r.Remarks%></td>
		</tr>
		<% } %>
	</tbody>
</table>
</div>



	</asp:Content>
