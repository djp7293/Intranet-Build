<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receipt.aspx.cs" Inherits="Monsees._Default_Receipt"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<head runat="server">
    <title>Order Receipt</title>
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

<!--[if gte mso 9]><xml>
<mso:CustomDocumentProperties>
<mso:IsMyDocuments msdt:dt="string">1</mso:IsMyDocuments>
</mso:CustomDocumentProperties>
</xml><![endif]-->
</head>
<body>


    <form id="form1" runat="server">
    <div align="center">
    
        <table class="style1" width="800px">
<tr width="800px"><td>
	This is your receipt for the purchase order item submitted to Monsees Tool. <br><br>
	An e-mail has been sent to appropriate team members at Monsees Tool for review and/or execution.  <br><br>
	The following are details for the purchase order item(s) submitted and to be processed by Monsees Tool:<br><br>

</td></tr>

<tr><td>

 <asp:Label ID="PONumber" runat="server" 
                    Text="PO Number : "></asp:Label>
</td></tr>
<tr><td>
 <asp:Label ID="ContactName" runat="server"  
                    Text="Contact : " ></asp:Label>
</td></tr>
<tr><td>
 <asp:Label ID="PODate" runat="server"  
                    Text="PO Date : " ></asp:Label>
</td></tr>
               



       
<tr><td>
<div width="800px">
				<asp:GridView ID="ReceiptGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               Width="100%" AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
	<asp:BoundField DataField="POItemID" HeaderText="Item #" 
                                       SortExpression="POItemID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
<asp:BoundField DataField="PartNumber" HeaderText="Part #" 
                                       SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
<asp:BoundField DataField="ActiveVersion" HeaderText="Rev #" 
                                       SortExpression="ActiveVersion">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
<asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
<asp:BoundField DataField="NextDelivery" HeaderText="Requested Delivery" 
                                       SortExpression="NextDelivery" DataFormatString="{0:d}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
</columns>
</asp:gridview>
</div>
<br>
<br>
</td></tr>
<tr><td> If you have any questions or issues, please do not hesitate to give us a call - 585-262-4180.  
</td></tr>
</table>




 
</div>


 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>


</form>
</body></html>
