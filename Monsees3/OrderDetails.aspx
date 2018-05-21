<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Monsees._Default_Order"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<head runat="server">
    <title>Purchase Order Details</title>
    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    
    <style type="text/css">

body {
    
    font-size: small;
    text-align: justify;
}        

	.style1
        {
            width: 100%;
	    
        }
        .style2
        {
            width: 11%;
        }
    </style>

<!--[if gte mso 9]><xml>
<mso:CustomDocumentProperties>
<mso:IsMyDocuments msdt:dt="string">1</mso:IsMyDocuments>
</mso:CustomDocumentProperties>
</xml><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    
      <table class="style1" width="50%">
            
            

</table>
<table align = "left" width="800px">
<tr width="800px">

<td align="left" width="150px">
 <asp:Label ID="PONumber" runat="server" 
                    Text="PO Number : "></asp:Label>
</td>    
<td align="left" width="250px">
 <asp:Label ID="ContactName" runat="server"  
                    Text="Contact : "  item-width="200px"></asp:Label>
</td>     
<td align="left" width="200px">
 <asp:Label ID="PODate" runat="server"  
                    Text="PO Date : " item-width="200px"></asp:Label>
</td>                  

<td align="left" width="200px">
 <asp:Label ID="NextDelivery" runat="server"  
                    Text="Next Delivery : " item-width="200px" DataFormatString="{0:d}"></asp:Label>
</td> 
</tr>
<tr width="800px">
<td align="left" width="150px">
 
</td> 
<td align="left" width="250px">
 <asp:Label ID="Total" runat="server"  
                    Text="Total : " item-width="200px" DataFormatString="{0:C2}"></asp:Label>
</td>    
<td align="left" width="200px">
 <asp:Label ID="ShippedTotal" runat="server" 
                    Text="Shipped : " item-width="200px" DataFormatString="{0:C2}"></asp:Label>
                <td align="left" width="200px">
 <asp:Label ID="InvoiceTotal" runat="server" 
                    Text="Invoiced : " DataFormatString="{0:C2}"></asp:Label>
</td> 
</tr>

        </table>
         <table class="style1" width="80%%">
            <tr>
                
                
                <td align="left" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
              

	<table class="style1" align="left">
<tr>
		<td align="left" valign="middle" class="style2">
                    <asp:Button ID="AddLines" runat="server" onclick="AddLines_Click" text="Add Line(s) to PO"
                        /></td>		
		

		<tr>
	<td colspan="15" align="left">
      	<asp:Panel ID="Panel1" runat="server" align="left">
            <asp:Panel ID="Panel2" runat="server" align="left">
               <p>
                   
                    
                           <asp:GridView ID="POItemViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               Width="100%" 
                               AllowSorting="True" align="left">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>

	<asp:BoundField DataField="LineItem"  
                                       HeaderText="Line #" SortExpression="LineItem"  ItemStyle-Width="20px">
                                       <ItemStyle HorizontalAlign="Center"/>
        </asp:BoundField> 
	<asp:BoundField DataField="PartNumber" HeaderText="Part Number" SortExpression="PartNumber"  ItemStyle-Width="100px">
                                   <ItemStyle HorizontalAlign="Left" font-name=MyFont />
        </asp:BoundField> 
	<asp:BoundField DataField="Revision Number" HeaderText="Rev #" SortExpression="Revision Number"  ItemStyle-Width="20px">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
                            	<asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber"  ItemStyle-Width="175px">
                                   <ItemStyle HorizontalAlign="Left" font-name=MyFont />
        </asp:BoundField> 
      	<asp:BoundField DataField="UnitPriced" HeaderText="Unit Price" SortExpression="UnitPriced"  ItemStyle-Width="50px" DataFormatString="{0:C2}">

                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
  
                            	<asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity"  ItemStyle-Width="20px">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
		
                            	<asp:BoundField DataField="NextQuantity" HeaderText="Remain" SortExpression="NextQuantity"  ItemStyle-Width="20px">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 

                            	<asp:BoundField DataField="NextDelivery" HeaderText="Next Delivery" SortExpression="NextDelivery"  ItemStyle-Width="75px" DataFormatString="{0:d}">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
				<asp:BoundField DataField="MaxOfCurrDelivery" HeaderText="LastDelivery" SortExpression="MaxOfCurrDelivery"  ItemStyle-Width="75px" DataFormatString="{0:d}">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
          <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total"  ItemStyle-Width="70px" DataFormatString="{0:C2}">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
    <asp:BoundField DataField="ShippedTotal" HeaderText="Shipped" SortExpression="ShippedTotal"  ItemStyle-Width="70px" DataFormatString="{0:C2}">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
        </asp:BoundField> 
    <asp:BoundField DataField="InvoicedTotal" HeaderText="Invoiced" SortExpression="InvoicedTotal"  ItemStyle-Width="70px" DataFormatString="{0:C2}">
                                   <ItemStyle HorizontalAlign="Center" font-name=MyFont />
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
</td>
</tr>
</table>



    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>



    </form>
</body>
</html>
