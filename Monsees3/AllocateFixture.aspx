<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="AllocateFixture.aspx.cs" Inherits="Monsees._Default_AllocateFixture"%>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 

<div class="lot">

<div class="header">
	<div class="title">Fixture #<%=JobDetailModel.PartNumber %> - <%=JobDetailModel.DrawingNumber %></div>

	

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
         
  <div>            

	<table itemstyle-cssclass="GridviewTable">

				
	
		

		<tr>
	<td colspan="15">
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   
                    
                           <asp:GridView ID="AllocateViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="FixtureInventoryID" DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="AllocateViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  <asp:BoundField DataField="FixtureInventoryID" HeaderText="Inventory ID" 
                                       SortExpression="FixtureInventoryID" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>       
                                   <asp:BoundField DataField="PartNumber" HeaderText="Fixture #" 
                                       SortExpression="PartNumber" ItemStyle-Width="8%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
				                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="12%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                 
                                   <asp:BoundField DataField="Location" HeaderText="Location" 
                                       SortExpression="Location" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
<asp:BoundField DataField="Material" HeaderText="Material" SortExpression="Material" ItemStyle-Width="12%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="Dimension" HeaderText="Dimension" SortExpression="Dimension" ItemStyle-Width="10%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" ItemStyle-Width="6%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="HeatTreat" HeaderText="Heat Treat" SortExpression="HeatTreat" ItemStyle-Width="8%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Plating" HeaderText="Plating" SortExpression="Plating" ItemStyle-Width="8%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>

                                   <asp:CheckBoxField DataField="IsDrawing" SortExpression="IsDrawing" ItemStyle-Width="2%" />
   				   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing" Width="6%"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>  
                                   
				   <asp:ButtonField CommandName="Allocate" Text="Allocate"  HeaderText="" ItemStyle-Width="6%">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>
   				                                             
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
        </asp:Panel>
</td>
</tr>
</table>

    <p>
    <asp:Label ID="UpdateResults" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>

    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        EnableCaching="False">



    </asp:SqlDataSource>

    <asp:SqlDataSource ID="MonseesSqlDataSourceSetups" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="populatejoblist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Job_Number From ProductionView">
    </asp:SqlDataSource>   

    
</asp:Content>
