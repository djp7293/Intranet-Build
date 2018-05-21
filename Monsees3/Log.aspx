<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Monsees.master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="Monsees.Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />

</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
<div class="lot">

<div class="header">
	<div class="title">Lot #<%=JobItemID %> - <%=JobDetailModel.DrawingNumber %></div>

	
	<div style="clear:both;height:0px">&nbsp;</div>
</div>

<div class="summary">

<div class="part" >
	Part #<%=JobDetailModel.PartNumber %>
	<div class="comments">
		<span class="label">Comments:</span>
		<span><%=JobDetailModel.Comments %></span>
	</div>

</div>

 <div class="job">
	<div><%= JobDetailModel.CompanyName %></div>
	<div>Job #<%= JobDetailModel.JobNumber%></div>
</div>

 <div style="clear:both;height:0px">&nbsp;</div>
</div>
    <br />

     <table >
                     <tr>
                        <td class="style4">
                            Employee</td>
                        <td>
                        <asp:DropDownList ID="EmployeeList" runat="server" Height="20px" 
                            Width="225px" DataTextField="Name" DataValueField="EmployeeID">
                        </asp:DropDownList>
                        </td>
                    </tr>
					<tr><td>Hours: </td><td><asp:textbox runat="server" id="hours" size="5" Text="0" /></td></tr>
			
					<tr><td>Quantity In: </td><td><asp:textbox runat="server" id="qtyin"  size="5" Text="0" /></td></tr>
				
					<tr><td>Quantity Out: </td><td><asp:textbox runat="server" id="qtyout" size="5"  Text="0"/></td></tr>
			</table>
	<div class="heading-box">   
		<div class="title">Operations</div>
		<div class="info"><span id="operationsCompleted"></span>/<span id="totalOperations"></span> Completed</div>
	</div>
    <br /><br />
		<asp:UpdatePanel ID="GridUpdatePanel" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:GridView ID="OperationsGridView" runat="server" AutoGenerateColumns="false" CssClass="grid operations" >
		<Columns>
			<asp:BoundField DataField="ProcessOrder" HeaderText="#"   />
			<asp:BoundField DataField="Label" HeaderText="Description" />
			<asp:BoundField DataField="SetupCost" HeaderText="Setup (Hours)"/> 		
			<asp:BoundField DataField="OperationCost" HeaderText="Runtime (Minutes)" />
            <asp:BoundField DataField="Hours" HeaderText="Logged Hrs" />
            <asp:BoundField DataField="QuantityIn" HeaderText="Quantity In" />
            <asp:BoundField DataField="QuantityOut" HeaderText="Quantity Out" />
           
			<asp:TemplateField HeaderText="Completed" >
				<ItemTemplate>
					<span class="completed completed-<%#Eval("JobSetupID")%> <%# (bool) Eval("Completed") ? "yes" : "" %>"><%# (bool) Eval("Completed") ? "Yes" : "No" %></span>
				</ItemTemplate>
			</asp:TemplateField>
 
            <asp:TemplateField>
					<ItemTemplate>
					<asp:linkbutton runat="Server" onclick="onclick_MarkComplete(<%#Eval("JobSetupID")%>)"></asp:linkbutton>		
	</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView></ContentTemplate>
            </asp:UpdatePanel>
    </div>
</div>

	<script type="text/javascript">
		var inventoryService = new CRUDService(
		{
			baseUrl: '/LotService.aspx',

			setCompletionStatusLog: function (id, jobitem, employee, hours, qtyin, qtyout, num) {
				var status = !lotView.getJobStatus(id);
				this.sendAjax({
					url: "SetCompletionStatusLog",
					data: { jobSetupId: id, jobItemId: jobitem, employeeId: employee, hours: hours, qtyin: qtyin, qtyout: qtyout, status: status },
					success: function () {
					    $.toast('Saved Operation ' + num + ', ' + hours + ', ' + qtyin + ', ' + qtyout);
						lotView.updateJobStatus(id, status);
						lotView.updateView();
					}
				});
			},
			

		});

		function LotView() {

			this.updateView = function () {
				$('#operationsCompleted').html($('span.yes').length);
				$('#totalOperations').html( $('span.completed').length );

			}

			this.updateJobStatus = function (id, status) {


				$('span.completed-' + id)
					.toggleClass("yes", status)
					.html(status ? "Yes" : "No");

				$('a.completed-' + id)
					.toggleClass("action", !status)
					.html(status ? "<span>Mark Incomplete</span>" : "<span>Mark Complete</span>");
			}

			this.getJobStatus = function (id) {
				return $('.completed-' + id).hasClass("yes") == true;
			}
		}

		var lotView = new LotView();
		lotView.updateView();
</script>
</asp:Content>

