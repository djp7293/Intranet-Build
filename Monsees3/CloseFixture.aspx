<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloseFixture.aspx.cs" Inherits="Monsees.CloseFixture"  MasterPageFile="~/MasterPages/Monsees.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />
<link rel="stylesheet" type="text/css" href="/css/closepart.css" />
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div>

		<div class="header">
			<div class="title">Lot #<%=JobItemID %> - <%=JobDetailModel.DrawingNumber %></div>
			<div class="revision">REV <%=this.JobDetailModel.RevisionNumber %></div>
			<div style="clear:both;height:0px">&nbsp;</div>
		</div>


	
			
	 
	 
		

	
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
			<th scope="col" class="auto-style1">Lot #</th><th scope="col" class="auto-style1">Location</th><th scope="col" class="auto-style1">Qty</th>
		</tr>
		</thead>
		<tbody>
		  <%foreach(Montsees.Data.DataModel.InventoryStatusModel ism in InventoryStatusList) {  %>	
			<tr>
			<td><%=ism.LotNumber %></td><td><%=ism.Location1 %></td><td><%=ism.Quantity %></td>
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
					<div id="moveToFixtureInventoryForm">
					<table>
						<tr>
							<td class="label">Qty</td>
							<td>
								<input type="text" id="closePartQty" />
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
						</table>
                         <a href="#" id="closePartButton" class="button"><span>Complete to Inventory</span></a>
		 <a href="/FixtureTag.aspx?id=<%=JobItemID%>" target="_blank" id="printLabelButton" class="button action hidden"><span>Print Label</span></a>
					
					</div>

				</td>
			</tr>
		 </tfoot>
		</table>
 
	

	</div>

	<script>
		var jobItemId = <%=JobItemID%>,
			rowNum = 0,
			closePartService = new CRUDService(
		{
			baseUrl: '/ClosePartService.aspx',

			moveToFixtureInventory: function (qty,location,notes) {
				this.sendAjax({
					url: "MoveToFixtureInventory",
					data: { jobItemId: jobItemId, qty: qty, location:location,notes:notes },
					success: function (data) {
					    $.toast('inventory moved!');
					    $('.existingInventoryGrid .placeholder').hide();
					    var first = $('.existingInventoryGrid tbody').first().append( $('#locationsTemplate').text() )

					    $('.existingInventoryGrid .new').last().attr('id', 'row' + (rowNum));
					    closePartService.htmlByClass('#row' + rowNum,
                        {
                            location:location,
                            qty:qty,
                            
                        });
						
						
					}
				});
			},

			closePart: function() {
				this.sendAjax({
					url: "ClosePart",
					data: {jobItemId: jobItemId},
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
		    
		    closePartService.moveToFixtureInventory($('#closePartQty').val(), $('#closePartLocation').val(), $('#closePartNotes').val());
		    closePartService.closePart();
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