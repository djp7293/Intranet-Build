<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClosePart.aspx.cs" Inherits="Monsees.ClosePart"  MasterPageFile="~/MasterPages/Monsees.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />
<link rel="stylesheet" type="text/css" href="/css/closepart.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
	<div>

		<div class="header">
			<div class="title">Lot #<%=JobItemID %> - <%=JobDetailModel.DrawingNumber %></div>
			<div class="revision">REV <%=this.JobDetailModel.RevisionNumber %></div>
			<div style="clear:both;height:0px">&nbsp;</div>
		</div>


	 <div class="close-part">
		 <h4>Close Part</h4>
		 	<div>			 
				<div class="label">Qty Produced</div>
				<div class="data"><input id="produced" type="text" /></div>
				 <div style="clear:both;height:0px">&nbsp;</div>
			</div>

			<div class="data computed">
				<div class="computed">Delivered: <span id="deliveredQty"><%=this.LotPartTotals.Delivered %></span></div>
				<div  class="computed">Inventoried:<span id="inventoriedQty"><%=this.LotPartTotals.Inventoried %></span></div>
				<div class="total-allocated">Total: <span id="totalAllocated"><%=this.LotPartTotals.Delivered + this.LotPartTotals.Inventoried %></span></div>
				<div >Balance: <span id="balance"></span></div>
			</div>

	 
	 
		 <a href="#" id="closePartButton" class="button"><span>Close Part</span></a>
		 <a href="/Reports/Label.aspx?id=<%=JobItemID%>" target="_blank" id="printLabelButton" class="button action hidden"><span>Print Label</span></a>
	 </div>

	<div class="v-divider">&nbsp;</div>
	<div id="moveToInventory">
		<h4>Move To Inventory</h4>

		<script type="text/html" id="locationsTemplate">
			<tr class="new">
			<td class="lotNumber"> <%=JobItemID%></td><td class="location"></td><td class="status">&nbsp;</td><td class=""></td><td class="qty"></td>
			</tr>
		</script>

 

		<table class="existingInventoryGrid" border="1" >
		<thead>
		<tr>
			<th scope="col">Lot #</th><th scope="col">Location</th><th scope="col">Status</th><th scope="col">Gross</th><th scope="col">Qty</th>
		</tr>
		</thead>
		<tbody>
		  <%foreach(Montsees.Data.DataModel.InventoryStatusModel ism in InventoryStatusList) {  %>	
			<tr>
			<td><%=ism.LotNumber %></td><td><%=ism.Location1 %></td><td><%=ism.InvStatus %></td><td><%=ism.AssignedQuantity %></td><td><%=ism.Quantity %></td>
			</tr>
		<%} %>

		<%if (InventoryStatusList==null || InventoryStatusList.Count==0) { %>
			<tr class="placeholder">
				<td colspan="5">
					<div>No existing inventory.  </div>
				</td>
			</tr>
			<% } %>
		</tbody>
		<tfoot>
			<tr>
				<td colspan="5">
					<div id="moveToInventoryForm">
					<table>
						<tr>
							<td class="label">Qty</td>
							<td>
								<input type="text" id="closePartQty" />
							</td>
						</tr>
						<tr>
							<td  class="label">Status</td>
							<td>
								<select id="closePartStatus">
									<%foreach(Montsees.Data.InvStatus i in InventoryStatusTypeList){ %>
									<option value="<%=i.InvStatusID %>"  ><%=i.Status %></option>
									<%} %>
								</select>
		 
							</td>
						</tr>
						<tr>
							<td  class="label">Location</td>
							<td>
								<input type="text" id="closePartLocation" />
							</td>
						</tr>
						<tr>
							<td class="label">Notes</td>
							<td><textarea id="closePartNotes"></textarea></td>
						</tr>
						<tr>
							<td colspan="2">
								<a href="#" class="button" id="moveToInventoryButton"><span>Move To Inventory</span></a>

							</td>
						</tr>
					</table>

					
					</div>
				</td>
			</tr>
		 </tfoot>
		</table>
 
	</div>

	</div>

	<script>
		var jobItemId = <%=JobItemID%>,
			rowNum = 0,
			closePartService = new CRUDService(
		{
			baseUrl: '/ClosePartService.aspx',

			moveToInventory: function (qty,status,statusName,location,notes) {
				this.sendAjax({
					url: "MoveToInventory",
					data: { jobItemId: jobItemId, qty: qty, status:status,location:location,notes:notes },
					success: function (data) {
						$.toast('inventory moved!');
						$('.existingInventoryGrid .placeholder').hide();
						var first = $('.existingInventoryGrid tbody').first().append( $('#locationsTemplate').text() )

						$('.existingInventoryGrid .new').last().attr('id', 'row' + (rowNum));

						$('#deliveredQty').html(data.Delivered);
						$('#inventoriedQty').html(data.Inventoried);
						$('#totalAllocated').html(data.Delivered + data.Inventoried);
						updateBalance();

						closePartService.htmlByClass('#row' + rowNum,
						{
								location:location,
								notes:notes,
								qty:qty,
								status:statusName
						});
						
					}
				});
			},

			closePart: function(allocated,produced) {
				this.sendAjax({
					url: "ClosePart",
					data: {jobItemId: jobItemId,allocated:allocated,produced:produced},
					success: function() {
						$.toast('Part closed.');
						$('#printLabelButton').removeClass('hidden');
						$('#closePartButton').hide();
					}
				});
			},

			htmlByClass:function(id,items)
			{
				for (var i in items) {
					$(id + ' .'+i).html(items[i]);
					console.log(id + ' .'+i);
				}
			},

			 
	});

		$('#closePartButton').click(function(){
			closePartService.closePart();
		});

 
		$('#moveToInventoryButton').click(function() {
			closePartService.moveToInventory( $('#closePartQty').val(),
												  $('#closePartStatus').val(),
												  $('#closePartStatus option:selected').text(),
												  $('#closePartLocation').val(),
												  $('#closePartNotes').text()
												 );
		});
	
		function updateBalance()
		{
			var balance = Number($('#produced').val()) - Number($('#totalAllocated').text()) 
			$('#balance').html(balance);
		}
		$('#produced').keyup(function(){
			updateBalance();
		});

	</script>
 </asp:Content>