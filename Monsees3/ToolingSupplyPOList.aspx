<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToolingSupplyPOList.aspx.cs" Inherits="Monsees._Default_ToolingSupplyOrders" MasterPageFile="~/MasterPages/Monsees.Master" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <title>Tooling and Supply PO Manager</title>
    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <link id="lnkStylesheet" href="standard.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 11%;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">

    <div align="center">
    
        <table class="style1" width="100%">
            <tr>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td align="center" valign="bottom">
    
        <asp:Image ID="Image1" runat="server" ImageUrl="images/header01_mac_002.jpg" 
            ImageAlign="Middle" />
                </td>
                
               
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <asp:Button ID="ItemViewButton" runat="server" Text="PO Item View" 
                        onclick="ItemViewButton_Click" />
		</td>
   <td align="center" valign="middle">
                    <asp:Button ID="OrderViewButton" runat="server" Text="Order View" 
                        onclick="OrderViewButton_Click" />
		</td>
   
            </tr>
        </table>
         
              

	<table itemstyle-cssclass="GridviewTable">

			

		<tr>
	<td colspan="15">
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <div>
                   <asp:MultiView ID="OrdersMultiView" runat="server" ActiveViewIndex="0">
                       <asp:View ID="POItems" runat="server">
                           <div style="align-content:flex-start"><table><tr><td>
                               Job: <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource3" DataTextField="JobNumber" DataValueField="JobID" AppendDataBoundItems="true">
        				        <asp:ListItem Text="All" Value=""></asp:ListItem>
                               </asp:DropDownList>
                               </td><td> Vendor: <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="VendorName" DataValueField="SubcontractID" AppendDataBoundItems="true">
        				        <asp:ListItem Text="All" Value=""></asp:ListItem>
                               </asp:DropDownList>
                               
                              </td>
                               <td> Description: <asp:TextBox ID="DescFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td> Expense Type: <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Account" DataValueField="ListID" AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value=""></asp:ListItem></asp:DropDownList>
                              </td><td> Earliest: <bdp:BDPLite ID="DeliveryFirst" OnSelectionChanged="DeliveryChange" AutoPostBack="True" runat="server"></bdp:BDPLite>
                              </td><td> Latest: <bdp:BDPLite ID="DeliveryLast" OnSelectionChanged="DeliveryChange" AutoPostBack="True" runat="server"></bdp:BDPLite>
                           </td><td><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />
                              </td><td> <asp:Button ID="exportdata" Text="Export" OnClick="btnDownload_Click" runat="server" /></td></tr></table></div>
                           <asp:GridView ID="ItemViewGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceItem" GridLines="Vertical" 
                               onrowcommand="ItemViewGrid_RowCommand" Width="100%" 			       
                               AllowSorting="True" PageSize="50">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  <asp:BoundField DataField="SuppliesPONum" HeaderText="PO #" 
                                       SortExpression="SuppliesPONum" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   
				 
                                   
				   <asp:BoundField DataField="SuppliesPOItemID" HeaderText="Item #" 
                                       SortExpression="SuppliesPOItemID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   
				   <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName" ItemStyle-Width="8%">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="LineItem" HeaderText="Line Item" 
                                       SortExpression="LineItem" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ItemNum"  
                                       HeaderText="Item #" SortExpression="ItemNum" ItemStyle-Width="7%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
<asp:BoundField DataField="Description" HeaderText="Description" 
                                       SortExpression="Description" ItemStyle-Width="17%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                    <asp:BoundField DataField="Notes" 
                                        HeaderText="Notes" SortExpression="Notes" ItemStyle-Width="16%">
                                       <ItemStyle HorizontalAlign="Left
                                           " />
                                   </asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>     				   
                                   <asp:BoundField DataField="PrEach" HeaderText="Each" 
                                       SortExpression="PrEach" ItemStyle-Width="5%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Total" HeaderText="Total" 
                                       SortExpression="Total" ItemStyle-Width="5%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				                            
			           <asp:BoundField DataField="Date" HeaderText="Issued" SortExpression="Date" DataFormatString="{0:MM-dd-yyyy}" ItemStyle-Width="7%">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           
				   <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate" DataFormatString="{0:MM-dd-yyyy}" ItemStyle-Width="7%">
				   	<ItemStyle HorizontalAlign="Left" />
</asp:BoundField>
				   <asp:BoundField DataField="JobNumber" HeaderText="Job #" SortExpression="JobNumber" ItemStyle-Width="5%">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>					   
<asp:BoundField DataField="LotNumber" HeaderText="Lot #" SortExpression="LotNumber" ItemStyle-Width="5%">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>

			   
     				   <asp:TemplateField HeaderText="Received" ItemStyle-Width="3%">
    				   	<ItemTemplate>
        					<asp:CheckBox ID="ItemRecdCheck" runat="server" HeaderText="Received" 
        						Checked='<%#bool.Parse(Eval("Received").ToString())%>' />
    				   	</ItemTemplate>
				   </asp:TemplateField>

				   
				  
				   
				   <asp:ButtonField CommandName="ViewPOSupp" Text="P.O." HeaderText="" ItemStyle-Width="3%">
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
                      </asp:View>
                       <asp:View ID="Orders" runat="server">                                       
                           <asp:GridView ID="OrderViewGrid" runat="server" AllowPaging="True" PageSize="50"
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePO" GridLines="Vertical" 
                               onrowcommand="OrderViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>                                   
				   <asp:BoundField DataField="SuppliesPONum" HeaderText="PO #" 
                                       SortExpression="SuppliesPONum" ItemStyle-Width="5%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="VendorName" HeaderText="Vendor" 
                                       SortExpression="VendorName" ItemStyle-Width="10%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   				   
                                   
<asp:BoundField DataField="Description" HeaderText="Description" 
                                       SortExpression="Description" ItemStyle-Width="20%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                   <asp:BoundField DataField="Notes" HeaderText="Notes" 
                                       SortExpression="Notes" ItemStyle-Width="20%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="5%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Shipping Charge" HeaderText="Shipping" 
                                       SortExpression="Shipping Charge" ItemStyle-Width="5%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Cost" HeaderText="Total" 
                                       SortExpression="Cost" ItemStyle-Width="5%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="Date" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Issued" SortExpression="Date" ItemStyle-Width="10%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>        
				   <asp:BoundField DataField="DueDate" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Due Date" SortExpression="DueDate" ItemStyle-Width="10%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:TemplateField HeaderText="Received" ItemStyle-Width="5%"> 
    					<ItemTemplate>
        					<asp:CheckBox ID="IsOpenChk" runat="server" HeaderText="Received" 
        						Checked='<%#bool.Parse(Eval("Received").ToString())%>'  ItemStyle-Width="5%"/>
    					</ItemTemplate>
				   </asp:TemplateField>	 
				   <asp:TemplateField HeaderText="To QB"> 
    					<ItemTemplate>
        					<asp:CheckBox ID="POQBPosted" runat="server" HeaderText="To QB" 
        						Checked='<%#bool.Parse(Eval("PostedToQB").ToString())%>'  ItemStyle-Width="5%"/>
    					</ItemTemplate>
				   </asp:TemplateField>	 
<asp:ButtonField CommandName="ViewPOSupp" Text="View" HeaderText="" ItemStyle-Width="5%">
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
                      </asp:View>

</asp:Multiview>
                 </div>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    
    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSourceItem" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
       EnableCaching="False" >



    </asp:SqlDataSource>

<asp:SqlDataSource ID="MonseesSqlDataSourcePO" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SupplyOrderPOs Order By SuppliesPONum DESC" EnableCaching="False" >



    </asp:SqlDataSource>



    <asp:SqlDataSource ID="SupplyPOList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT SuppliesPONum From SupplyOrderPOs">
    </asp:SqlDataSource>   


    <asp:SqlDataSource ID="VendorNameList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT VendorName From Subcontractors">
    </asp:SqlDataSource>   

    
    
    <asp:SqlDataSource ID="IssueDateList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Date From SupplyOrders">
    </asp:SqlDataSource>  

   
    <asp:SqlDataSource ID="ReceivedList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Received From ToolingSuppliesItems">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="PostedToQBList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PostedToQB From SupplyOrderPOs">
    </asp:SqlDataSource>  

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [JobID], [JobNumber] FROM [Job]"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [SubcontractID], [VendorName] FROM [Subcontractors]"></asp:SqlDataSource>                           

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [ListID], [Account] FROM [QBAccountLink]"></asp:SqlDataSource>

</asp:Content>
