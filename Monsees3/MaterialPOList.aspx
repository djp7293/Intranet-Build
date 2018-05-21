<%@ Page Language="C#"  MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="MaterialPOList.aspx.cs" Inherits="Monsees._Default_Orders"%>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
 
    <title>Material PO Manager</title>
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
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 
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
               <p>
                   <asp:MultiView ID="OrdersMultiView" runat="server" ActiveViewIndex="0">
                       <asp:View ID="POItems" runat="server">
                           <asp:GridView ID="ItemViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceItem" GridLines="Vertical" 
                               onrowcommand="ItemViewGrid_RowCommand" Width="100%" 			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  
				   <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" 
                                       SortExpression="MaterialPOID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   
				   <asp:BoundField DataField="MatPriceID" HeaderText="Item #" 
                                       SortExpression="MatPriceID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ItemNum" HeaderText="Item #" 
                                       SortExpression="ItemNum">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="MaterialName"  
                                       HeaderText="Material" SortExpression="MaterialName">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                      				   
                                   <asp:BoundField DataField="Dimension" HeaderText="Dimension" 
                                       SortExpression="Dimension">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Diameter" HeaderText="Dia." 
                                       SortExpression="Diameter">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="Height" 
                                        HeaderText="Height" SortExpression="Height">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="Width" HeaderText="Width" 
                                       SortExpression="Width">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Length" HeaderText="Length" SortExpression="Length">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity">
				   	<ItemStyle HorizontalAlign="Center" />				  
				   </asp:BoundField>                                
			           <asp:BoundField DataField="Cost" HeaderText="Cost" SortExpression="Cost">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           <asp:BoundField DataField="Date" HeaderText="Order Date" SortExpression="Date">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="Shipping" HeaderText="Shipping" SortExpression="Shipping">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="ConfirmationNum" HeaderText="Conf. Num" SortExpression="ConfirmationNum">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>				   
     				   <asp:TemplateField HeaderText="Received">
    				   	<ItemTemplate>
        					<asp:CheckBox ID="ItemRecdCheck" runat="server" HeaderText="Received" 
        						Checked='<%#bool.Parse(Eval("Received").ToString())%>' />
    				   	</ItemTemplate>
				   </asp:TemplateField>

				   
				  
				   <asp:ButtonField CommandName="Attach" Text="Attached" HeaderText="Cert.">
                                   	<ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
				   <asp:ButtonField CommandName="Alloc" Text="Alloc." HeaderText="">
                                   	<ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 		
				   <asp:ButtonField CommandName="ViewPO" Text="P.O." HeaderText="">
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
                           <asp:GridView ID="OrderViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePO" GridLines="Vertical" 
                               onrowcommand="OrderViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>                                   
				   <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" 
                                       SortExpression="MaterialPOID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="VendorName" HeaderText="Vendor" 
                                       SortExpression="VendorName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ContactName"  
                                       HeaderText="Contact" SortExpression="ContactName">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Name"  
                                       HeaderText="Written By" SortExpression="Name">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                   <asp:BoundField DataField="Notes" HeaderText="Notes" 
                                       SortExpression="Notes" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="ConfirmationNum" HeaderText="Conf. #" 
                                       SortExpression="ConfirmationNum" ItemStyle-Width="15%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="DueDate" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Due Date" SortExpression="DueDate">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="ShippingCharge" HeaderText="Shipping Charge" 
                                       SortExpression="ShippingCharge">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Total" HeaderText="Total Cost" SortExpression="Total">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   
				   <asp:TemplateField HeaderText="To QB"> 
    					<ItemTemplate>
        					<asp:CheckBox ID="POQBPosted" runat="server" HeaderText="To QB" 
        						Checked='<%#bool.Parse(Eval("PostedToQB").ToString())%>' />
    					</ItemTemplate>
				   </asp:TemplateField>	 
<asp:ButtonField CommandName="ViewPO" Text="View" HeaderText="">
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
                 </p>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    
    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSourceItem" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From MaterialOrders2 Order By MatPriceID DESC" EnableCaching="False" FilterExpression="MaterialPOID like '{0}%'">
<FilterParameters>
        
        <asp:Parameter Name="MaterialPOFilter" DefaultValue=""
             Type="Int32" ConvertEmptyStringToNull="true"/>    

    </FilterParameters>


    </asp:SqlDataSource>

<asp:SqlDataSource ID="MonseesSqlDataSourcePO" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From MaterialPOs Order By MaterialPOID DESC" EnableCaching="False" >



    </asp:SqlDataSource>



    <asp:SqlDataSource ID="MaterialPOList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT MaterialPOID From MaterialPO">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="MaterialList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT MaterialName From Material">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="DimensionList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Dimension From [Material Dimension]">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="DateList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Date From Mat_Price2">
    </asp:SqlDataSource>  

    <asp:SqlDataSource ID="SizeList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Size From MaterialOrders2">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="ReceivedList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Received From MaterialOrders2">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="PostedToQBList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PostedToQB From MaterialOrders2">
    </asp:SqlDataSource>  

</asp:Content>