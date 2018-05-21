<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PVAInventory.aspx.cs" Inherits="Monsees._PVAInv" MasterPageFile="~/MasterPages/Monsees.Master" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
 
    <title>Available Inventory - Monsees Tool & Die</title>
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
                
                <td align="left" valign="bottom">
    
        <asp:Image ID="Image1" runat="server" ImageUrl="images/header01_mac_002.jpg" 
            ImageAlign="Middle" />
                </td>
                
                
            </tr>
</table>
<br>          
<table  class="style1" width="100%">
<tr>
                <td align="Left" valign="middle" >
                    <Font Face="Century Gothic" size="5" color="#420505"><b><% =CompanyName %> - Inventory Estimates</b></Font></td>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td>
               
                    </td>

            </tr>

        </table>
<p>
<table  class="style1" width="100%">
<tr align="Left" valign="middle"><td>
Inventory listings are estimates.  Please inquire to confirm quantity and price...</tr>
</td>
<tr>
                
<td align="Left" valign="middle">
                    Phone: 585-262-4180</td>
                

            </tr>
<tr>
	<td align="left"> E-mail:  <a href="mailto:sales@monseestool.com"><Font face="Times New Roman"> sales@monseestool.com</font></a>
</td></tr>
        </table>
<p>

         <table class="style1" width="100%">
            <tr>
                
                <td align="left" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
		         </table>
         
            <asp:Panel ID="Panel2" runat="server" align="left">
                         <asp:GridView ID="PVAInventoryGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePVAInventory" GridLines="Vertical" 
			       EmptyDataText="There is currently no inventory available for sale." EnableSortingAndPagingCallbacks="True"  
                               PageSize="50" Width="640" 
                               AllowSorting="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                                   <asp:BoundField DataField="PartNumber" HeaderText="Part Number" 
                                       SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev #" 
                                       SortExpression="[Revision Number]">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Quantity" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="FinalPrice" HeaderText="Proposed Ea." 
                                       SortExpression="FinalPrice">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Total" HeaderText="Total" 
                                       SortExpression="Total">
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
       
    
    </div>
    <asp:SqlDataSource ID="MonseesSqlDataSourcePVAInventory" runat="server" 
        
       
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        EnableCaching="False">
    </asp:SqlDataSource>
    
 </asp:Content>
