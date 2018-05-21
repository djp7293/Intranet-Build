<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="PurchaseOrders.aspx.cs" Inherits="Monsees._PurchaseOrders"%>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <title>Open Purchase Orders</title>
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
                    &nbsp;</td>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td>
               
                    </td>
            </tr>
        </table>
         <table class="style1" width="100%">
            <tr>
                <td align="left" valign="middle" class="style2">
                    
                </td>

               
                <td align="right" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
         <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                           <asp:GridView ID="PurchaseOrderGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePurchaseOrders" GridLines="Vertical" 
			       EmptyDataText="There is currently no inventory available for sale." EnableSortingAndPagingCallbacks="False"  
                               onrowcommand="PurchaseOrderGrid_RowCommand" PageSize="50" Width="60%" 
                               AllowSorting="True" 
                               onselectedindexchanged="PurchaseOrderGrid_SelectedIndexChanged">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                                 
				   <asp:BoundField DataField="POID" HeaderText="Item ID" 
                                       SortExpression="POID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:TemplateField  HeaderText="PO Number" SortExpression="PONumber">
<ItemStyle HorizontalAlign="Center" />					
<ItemTemplate>
					<asp:LinkButton ID="POItems" runat="server" CommandName="Details"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PONumber") %>'></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>
                                   <asp:BoundField DataField="ContactName" HeaderText="Contact" 
                                       SortExpression="ContactName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				                    <asp:BoundField DataField="PODate" HeaderText="PO Date" 
                                       SortExpression="PODate" DataFormatString="{0:d}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="MinOfCurrDelivery"  
                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" DataFormatString="{0:d}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Total" HeaderText="Total" 
                                       SortExpression="Total" DataFormatString="{0:C2}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ShippedTotal" HeaderText="Shipped" 
                                       SortExpression="ShippedTotal" DataFormatString="{0:C2}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="InvoicedTotal" HeaderText="Invoiced" 
                                       SortExpression="InvoicedTotal" DataFormatString="{0:C2}" >
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>	
				   <asp:BoundField DataField="OpenLinePO" HeaderText="Open Line" 
                                       SortExpression="OpenLinePO" DataFormatString="{0:C2}" >
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>	                                  
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
    
    </div>
    <asp:SqlDataSource ID="MonseesSqlDataSourcePurchaseOrders" runat="server" 
        
       
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
    EnableCaching="False">

    </asp:SqlDataSource>
    
</asp:Content>