<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Monsees.master" AutoEventWireup="true" CodeBehind="Lot.aspx.cs" Inherits="Monsees.Lot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 

<div class="lot">

<div class="header">
	<div class="title">Lot #<%=JobItemID %> - <%=JobDetailModel.DrawingNumber %></div>

	<div class="navigation">
		<a href="/ClosePart.aspx?id=<%=JobItemID%>" class="button"><span>Close Part</span></a>
		<a href="/Reports/Label.aspx?id=<%=JobItemID%>" class="button" target="_blank" ><span>View Tags</span></a>
        <a href="/Reports/InspectionReportPrint.aspx?id=<%=JobItemID%>" class="button" target="_blank" ><span>View Report</span></a>
	</div>

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

 <div style="clear:both;height:0px">&nbsp;
    </div>
   

	<div class="heading-box">
		<div class="title">Delivery</div>
		<div class="info"><span id="deliveriesShipped"></span>/<span id="totalDeliveries"></span> Ready to ship</div>
	</div>
    <br /><br />
	<asp:GridView ID="DeliveryDataGrid" runat="server" AutoGenerateColumns="false" CssClass="grid" >
		<Columns>
			<asp:BoundField DataField="CurrDelivery" HeaderText="Delivery"  DataFormatString="{0:d}"  />
			<asp:BoundField DataField="Quantity" HeaderText="Qty" />
			<asp:BoundField DataField="PONumber" HeaderText="PO Number"/> 
		
			<asp:TemplateField HeaderText="Suspended">
				<ItemTemplate>
					<input type="checkbox" class="suspended suspended-<%#Eval("DeliveryItemID")%>"  <%#(bool) Eval("Suspend") ? "checked" : ""%>  />
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="Ready To Ship">
				<ItemTemplate>
					<input type="checkbox" class="shipped shipped-<%#Eval("DeliveryItemID")%>"  <%#(bool) Eval("ReadyToShip") ? "checked" : ""%>  />
				</ItemTemplate>
			</asp:TemplateField>


			<asp:BoundField DataField="ShipDate" HeaderText="Ship Date"  DataFormatString="{0:d}"  />

			<asp:TemplateField>
				<ItemTemplate>
					<a href="#" class="button save" onclick="inventoryService.updateDeliveryItem(<%#Eval("DeliveryItemID")%>)"><span>Save</span></a>		
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

	<div class="heading-box">
		<div class="title">Operations</div>
		<div class="info"><span id="operationsCompleted"></span>/<span id="totalOperations"></span> Completed</div>
	</div>
    <br /><br />
		<asp:GridView ID="OperationsGridView" runat="server" AutoGenerateColumns="false" CssClass="grid operations" >
		<Columns>
			<asp:BoundField DataField="ProcessOrder" HeaderText="#"   />
			<asp:BoundField DataField="Label" HeaderText="Description" />
			<asp:BoundField DataField="SetupCost" HeaderText="Setup (Hours)"/> 
		
			<asp:BoundField DataField="OperationCost" HeaderText="Setup (Minutes)" />
			<asp:TemplateField HeaderText="Completed" >
				<ItemTemplate>
					<span class="completed completed-<%#Eval("JobSetupID")%> <%# (bool) Eval("Completed") ? "yes" : "" %>"><%# (bool) Eval("Completed") ? "Yes" : "No" %></span>
				</ItemTemplate>
			</asp:TemplateField>
 
			<asp:TemplateField>
				<ItemTemplate>
					<a href="#" class="button completed-<%#Eval("JobSetupID")%> <%# (bool) Eval("Completed") ? "" : "action" %>" onclick="inventoryService.setCompletionStatus(<%#Eval("JobSetupID")%>,'<%#Eval("OperationName")%>')"><span><%# (bool) Eval("Completed") ? "Mark Incomplete" : "Mark Complete" %> </span></a>		
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
      <br /><div class="heading-box">
		<div class="title">Corrective Actions Library</div></div><br /><br />
    <asp:GridView ID="CARView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" OnRowCommand="CARView_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="CARID" HeaderText="CAR #" SortExpression="CARID" />
            <asp:BoundField DataField="Revision Number" HeaderText="Rev" SortExpression="Revision Number" />
            <asp:BoundField DataField="InitiationDate" HeaderText="Init. Date" SortExpression="InitiationDate" />
            <asp:BoundField DataField="DueDate" HeaderText="Due" SortExpression="DateDate" />
            <asp:BoundField DataField="Definition" HeaderText="Issue" SortExpression="Definition" />
            <asp:BoundField DataField="ImpEmployee" HeaderText="Owner" SortExpression="ImpEmployee" />
            <asp:ButtonField CommandName="ViewCAR" Text="View" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
            <br />
        <asp:Button runat="server" Text="Initiate New CAR" OnClick="button_click" />
    <br /><div class="heading-box">
		<div class="title">Certification Requirements</div></div><br /><br />
             <asp:ListView ID="ListView1" runat="server" EnableModelValidation="True">
        <ItemTemplate>
            <table><tr><td>
           Material Cert Available:
            <asp:CheckBox ID="MCertLabel" runat="server" Enabled="false" Checked='<%# Eval("MCert") %>' />
            </td><td>Material Cert Required:
                <asp:CheckBox ID="MCertReqd" runat="server" Enabled="false" Checked='<%# Eval("MatlCertReqd") %>' /></td></tr>
            <tr><td>Plating Cert Available:
            <asp:CheckBox ID="PCertLabel" runat="server" Enabled="false" Checked='<%# Eval("PCert") %>' />
            </td><td>Plating Cert Required:
                <asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%# Eval("MatlCertReqd") %>' />
                </td></tr></table>

        </ItemTemplate>
        <LayoutTemplate>
            <div id="itemPlaceholderContainer" runat="server" style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                <span runat="server" id="itemPlaceholder" />
            </div>
            <div style="text-align: left;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;">
            </div>
        </LayoutTemplate>
     </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
   
</div>

	<script type="text/javascript">
		var inventoryService = new CRUDService(
		{
			baseUrl: '/LotService.aspx',

			setCompletionStatus: function (id, num) {
				var status = !lotView.getJobStatus(id);
				this.sendAjax({
					url: "SetCompletionStatus",
					data: { jobSetupId: id, status: status },
					success: function () {
						$.toast('Saved Operation ' + num);						
						lotView.updateJobStatus(id, status);
						lotView.updateView();
					}
				});
			},

			updateDeliveryItem: function (id) {
				var readyToShipStatus = $('input.shipped-' + id).is(':checked'),
					suspendedStatus = $('input.suspended-' + id).is(':checked');
				
					this.sendAjax({
						url: "SetShipStatus",
						data: { deliveryItemId: id, readyToShip: readyToShipStatus, suspended: suspendedStatus },
						success: function () {
							$.toast('Updated Delivery '+ id);						
							lotView.updateView();
						}
					});
			}

		});

		function LotView() {

			this.updateView = function () {
				$('#operationsCompleted').html($('span.yes').length);
				$('#totalOperations').html( $('span.completed').length );

				$('#deliveriesShipped').html( $('input.shipped:checked').length );
				$('#totalDeliveries').html($('input.shipped').length);
			}

			this.updateDeliveryItem = function(id)
			{

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

