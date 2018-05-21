<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Monsees.Master" CodeBehind="AddFixture.aspx.cs" Inherits="Monsees.AddFixture" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<title><%=JobDetailModel.PartNumber %> Fixturing</title>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 

<div class="lot">

<div class="header">
	<div class="title">Lot #<%=SourceLot %> - <%=JobDetailModel.DrawingNumber %> Add Fixture(s)</div>

	

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
</div></div>
    <div>

<table>
<tr><td>
        
	
    
    <br />
    <asp:FormView ID="FormView1" runat="server" OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True" Width="587px">
<RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
        
            
                <ItemTemplate>
                     <table> 
                            <tr><td><table><tr><td>Required For: </td><td colspan="2">
                    <asp:DropDownList ID="OperationDropDownList" runat="server" Width="120px" DataTextField="Label" DataValueField="SetupID"></asp:DropDownList>
               </td></tr>                           
                         <tr><td>Part #: </td><td colspan="2">
                    <asp:TextBox ID="PartNumber1" runat="server" Width="120px">NA</asp:TextBox>
               </td></tr>
           
              <tr><td>Description: </td><td colspan="2">
              
	    
                
                          
                    <asp:TextBox ID="Description1" runat="server" Width="150px"></asp:TextBox>
                
            </td></tr>
           
              <tr><td>Qty: </td><td colspan="2">
               
                
                          
                    <asp:TextBox ID="Quantity1" runat="server" Width="30px"></asp:TextBox>
               
            
                </td></tr>
           
              <tr><td>Req. Delivery: </td><td colspan="2">
               
                          
                    <bdp:BDPLite ID="Delivery1" OnSelectionChanged="DeliveryChange" AutoPostBack="True" runat="server" style="width:80px" Width="80px"></bdp:BDPLite>
                
            
                </td></tr>
           
              <tr><td>Owner: </td><td colspan="2">
                      
                    <asp:DropDownList ID="Owner" runat="server" Width="100px"   DataTextField="ContactName" DataValueField="ContactID"></asp:DropDownList>
          
            
           </td></tr>
           
              <tr><td>Material: </td><td colspan="2">
               
                         
                    <asp:DropDownList ID="Material" runat="server" Width="100px"  DataTextField="Material" DataValueField="MaterialID" AppendDataBoundItems="true"><asp:ListItem Text="NA" Value="0"></asp:ListItem></asp:DropDownList>
              
            
            </td></tr>
           
              <tr><td>Material Dim.: </td><td colspan="2">
              
                         
                    <asp:DropDownList ID="MaterialDim" runat="server" Width="100px"  DataTextField="Dimension" DataValueField="MaterialDimID" AppendDataBoundItems="true"><asp:ListItem Text="NA" Value="0"></asp:ListItem></asp:DropDownList>
              
            </td></tr>
           
              <tr><td>Material Size: </td><td colspan="2">
                       
               
                         
                    <asp:DropDownList ID="MaterialSize" runat="server" Width="100px"  DataTextField="Size" DataValueField="MaterialSizeID" AppendDataBoundItems="true"><asp:ListItem Text="NA" Value="0"></asp:ListItem></asp:DropDownList>
                
           
               </td></tr>
           
              <tr><td>Length: </td><td colspan="2">        
               
                          
                    <asp:TextBox ID="Length" runat="server" Width="30px"></asp:TextBox>
               </td></tr>
           </table></td><td><table><tr><td></td></tr>
              <tr><td>Plating: </td><td>
                     <asp:CheckBox ID="IsPlating" runat="server" Width="20px" />   </td><td>      
                    <asp:DropDownList ID="Plating" runat="server" Width="100px"  DataTextField="Workcode" DataValueField="WorkcodeID" AppendDataBoundItems="true"><asp:ListItem Text="None" Value="0"></asp:ListItem></asp:DropDownList>
               </td></tr>
           
              <tr><td>Heat Treat: </td><td>
                     <asp:CheckBox ID="IsHeatTreat" runat="server" Width="20px" />    </td><td>     
                    <asp:DropDownList ID="HeatTreat" runat="server" Width="100px"  DataTextField="Workcode" DataValueField="WorkcodeID" AppendDataBoundItems="true"><asp:ListItem Text="None" Value="0"></asp:ListItem></asp:DropDownList>
               </td></tr>
           
              <tr><td>Turn: </td><td>
                    <asp:CheckBox ID="IsTurn" runat="server" Width="20px" />    </td><td>    
                    <asp:TextBox ID="Turn" runat="server" Width="30px"></asp:TextBox>
               </td></tr>
           
              <tr><td>Mill: </td><td>
                     <asp:CheckBox ID="IsMIll" runat="server" Width="20px" />    </td><td>     
                    <asp:TextBox ID="Mill" runat="server" Width="30px"></asp:TextBox>
               </td></tr>
           
              <tr><td>Multiaxis: </td><td>
                    <asp:CheckBox ID="IsMultiaxis" runat="server" Width="20px" />         </td><td> 
                    <asp:TextBox ID="Multiaxis" runat="server" Width="30px"></asp:TextBox>
               </td></tr>
           
              <tr><td>Grind: </td><td>
                    <asp:CheckBox ID="IsGrind" runat="server" Width="20px" />    </td><td>      
                    <asp:TextBox ID="Grind" runat="server" Width="30px"></asp:TextBox>
               </td></tr>
           
              <tr><td>Wire EDM: </td><td>
                     <asp:CheckBox ID="IsWireEDM" runat="server" Width="20px" /> 
                  </td><td>      
                    <asp:TextBox ID="WireEDM" runat="server" Width="30px"></asp:TextBox>
              </td></tr>
           
              <tr><td>Drawing/File: </td><td colspan="2">
		            <asp:FileUpload id="filMyFiletest" runat="server" Width="150px"></asp:FileUpload>
                         
                   </td></tr></table> </td>
                
                <td class="style7">
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
             
                </td>
                
            </tr>  </table> 
                </ItemTemplate>
          
        
<HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
    </asp:FormView>
    
    </td></tr><tr>

    <td>
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
    
    </td></tr>

<tr>
<td>&nbsp;</td></tr>


          
        </table>
    </div>
  <asp:SqlDataSource ID="MonseesSqlDataSourceFixtureOrders" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>
    
      <asp:SqlDataSource ID="MonseesSqlDataSourceFixtureInventory" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>

</asp:Content>
