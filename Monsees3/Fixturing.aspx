<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="Fixturing.aspx.cs" Inherits="Monsees._Fixturing"%>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<title><%=JobDetailModel.PartNumber %> Fixturing</title>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 

<div class="lot">

<div class="header">
	<div class="title">Lot #<%=JobItemID %> - <%=JobDetailModel.DrawingNumber %> Fixture Summary</div>

	

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
<div> &nbsp</div>
 <div style="clear:both;height:0px"><a href="AddFixture.aspx?SourceLot=<%= JobItemID %>">Add Fixture</a>  <a href="AllocateFixture.aspx?SourceLot=<%= JobItemID %>">Allocate Existing Fixture</a></div>
</div>

	<div class="heading-box">
		<div class="title">Current Fixture Orders</div></div>
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   
                       
                           <asp:GridView ID="FixtureOrderGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceFixtureOrders" GridLines="Vertical" 
EmptyDataText="There are currently no fixtures on order for this lot." EnableSortingAndPagingCallbacks="False"  
                               onrowcommand="FixtureOrderGrid_RowCommand" PageSize="50" Width="100%" 
                               AllowSorting="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobNumber" HeaderText="Job #" 
                                       SortExpression="JobNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				                   <asp:BoundField DataField="PartNumber" HeaderText="Part #" 
                                       SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="DrawingNumber"  
                                       HeaderText="Description" SortExpression="DrawingNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="MaterialName" HeaderText="Material" 
                                       SortExpression="MaterialName">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Plating" HeaderText="Plating" 
                                       SortExpression="Plating">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="HeatTreat" HeaderText="Heat Treat" 
                                       SortExpression="HeatTreat">
				       <ItemStyle HorizontalAlign="Center" />
				   </asp:BoundField>
				   
                                   <asp:BoundField DataField="OperationName" HeaderText="Operation Required" 
                                       SortExpression="OperationName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CurrDelivery" HeaderText="Due Date" SortExpression="CurrDelivery" DataFormatString="{0:MM-dd-yyyy}">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="ContactName" HeaderText="Owner" SortExpression="ContactName">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:CheckBoxField DataField="IsDrawing" SortExpression="IsDrawing" ItemStyle-Width="3%" />
   				   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("FixtureRevID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>  
                                   		    <asp:TemplateField HeaderText="New Drawing">
                <ItemTemplate>
		            <asp:FileUpload id="filMyFiletest" runat="server" Width="180px"></asp:FileUpload>
                </ItemTemplate>
            </asp:TemplateField>
                                   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbPostFile" runat="server" CommandName="AttachFile" CommandArgument='<%#Eval("FixtureRevID") %>' Text="Attach File"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>  
                                               <asp:TemplateField>
						<ItemTemplate>
							<a href="/CloseFixture.aspx?id=<%#Eval("JobItemID")%>">Close</a>
						</ItemTemplate>
					</asp:TemplateField>		                         
                                                 <asp:TemplateField>
						<ItemTemplate>
							<a href="/FixturetoParts.aspx?Id=<%#Eval("FixtureRevID")%>">Allocate to Add'l Parts</a>
						</ItemTemplate>
					</asp:TemplateField>    
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                      
                          
                 </p>
            </asp:Panel>
        <asp:Panel ID="Panel1" runat="server">
            <div class="heading-box">
		<div class="title">Fixture Inventory</div></div>
<asp:GridView ID="FixtureInventoryGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceFixtureInventory" GridLines="Vertical" 
EmptyDataText="There are currently no completed fixtures allocated to this lot." EnableSortingAndPagingCallbacks="False"  
                               onrowcommand="FixtureInventoryGrid_RowCommand" PageSize="50" Width="100%" 
                               AllowSorting="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                                  <asp:BoundField DataField="FixtureMapID" HeaderText="ID" 
                                       SortExpression="FixtureMapID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>

                                   <asp:BoundField DataField="PartNumber" HeaderText="Fixture #" 
                                       SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="Description" HeaderText="Description" 
                                       SortExpression="Description">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Loc"  
                                       HeaderText="Location" SortExpression="Loc">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                     
                                   <asp:BoundField DataField="OperationName" HeaderText="First Operation Needed" 
                                       SortExpression="OperationName">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
  
<asp:BoundField DataField="Material" HeaderText="Material" 
                                       SortExpression="Material">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   
                                   <asp:BoundField DataField="Plating" HeaderText="Plating" 
                                       SortExpression="Plating">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="HeatTreat" HeaderText="Heat Treat" 
                                       SortExpression="HeatTreat">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:ButtonField CommandName="Remove" Text="Unallocate from Part">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>
                                                      <asp:TemplateField>
						<ItemTemplate>
							<a href="/FixturetoParts.aspx?Id=<%#Eval("RevisionID")%>">Allocate to Add'l Parts</a>
						</ItemTemplate>
					</asp:TemplateField>
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
	</asp:Panel>
    
    </div>
  <asp:SqlDataSource ID="MonseesSqlDataSourceFixtureOrders" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>
    
      <asp:SqlDataSource ID="MonseesSqlDataSourceFixtureInventory" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>
</asp:Content>
 
 