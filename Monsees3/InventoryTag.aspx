<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryTag.aspx.cs" Inherits="Monsees._Default_InventoryTag"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<head runat="server">
    <title>Inventory</title>
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
              

<table itemstyle-cssclass="TableBody">
	<tr width="576pt>
		<td width="50%">
			<td width="88pt">Company: 
			</td>
			<td align="left" width="200pt"> 
				<asp:Label ID="CompanyName" runat="server"></asp:Label>
			</td>
		</td>
		<td width="288pt>
			<td width="88pt">Company: 
			</td>
			<td align="left">
				<asp:Label ID="CompanyName2" runat="server" ></asp:Label>
			</td>
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			<td align="left" width="60pt">Part #: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="PartNumber" runat="server" ></asp:Label>
			</td>
			<td align="left" width="60pt">Rev #: 
			</td>
			<td align="left" width="68pt">
				<asp:Label ID="RevisionNumber" runat="server" ></asp:Label>
			</td>
		</td>
		<td width="50%">
			<td align="left" width="60pt">Part #: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="PartNumber2" runat="server" ></asp:Label>
			</td>
			<td align="left" width="60pt">Rev #: 
			</td>
			<td align="left" width="68pt">
				<asp:Label ID="RevisionNumber2" runat="server" ></asp:Label>
			</td>
		</td>
	</tr>
	<tr width="576pt>
		<td align="left" width="50%">
			<td width="88pt">Desc: 
			</td>
			<td align="left"> 
				<asp:Label ID="DrawingNumber" runat="server" ></asp:Label>
			</td>
		</td>
		<td align="left" width="50%">
			<td width="88pt">Desc: 
			</td>
			<td align="left" width="200pt">
				<asp:Label ID="DrawingNumber2" runat="server" ></asp:Label>
			</td>
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			<td align="left" width="60pt">Lot #: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="JobItem" runat="server" ></asp:Label>
			</td>
			<td align="left" width="60pt">Status: 
			</td>
			<td align="left" width="68pt">
				<asp:Label ID="Status" runat="server" ></asp:Label>
			</td>
		</td>
		<td width="50%">
			<td align="left" width="60pt">Lot #: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="JobItem2" runat="server" ></asp:Label>
			</td>
			<td align="left" width="128pt" colspan="2" rowspan="4">Place Gridview here
			</td>			
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			<td align="left" width="60pt">Job #: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="JobNumber" runat="server" ></asp:Label>
			</td>
			<td align="left" width="60pt">Loc: 
			</td>
			<td align="left" width="68pt">
				<asp:Label ID="Location1" runat="server" ></asp:Label>
			</td>
		</td>
		<td width="50%">
			<td align="left" width="60pt">Job #: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="JobNumber2" runat="server" ></asp:Label>
			</td>					
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			<td align="left" width="60pt">Qty: 
			</td>
			<td align="left" width="100pt" colspan="3">
				<asp:Label ID="qty" runat="server" ></asp:Label>
			</td>
			
		</td>
		<td width="50%">
			<td align="left" width="60pt">Quantity: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="SumOfQuantity" runat="server" ></asp:Label>
			</td>					
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			<td align="left" width="60pt">InventoryID: 
			</td>
			<td align="left" width="100pt">
				<asp:Label ID="InventoryID" runat="server" ></asp:Label>
			</td>
			
		</td>
		<td width="50%">
			<td align="left" width="60pt">
			</td>
			<td align="left" width="100pt">
				
			</td>					
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			
		</td>
		<td width="50%">
			<asp:Label ID="CompanyName3" runat="server" colspan="4"></asp:Label>				
		</td>
	</tr>
	<tr width="576pt">
		<td width="50%">
			
		</td>
		<td width="50%">
			<td align="left" width="60pt">Part Number: 
			</td>
			<td align="left" width="228pt">
				<asp:Label ID="PartNumber3" runat="server" ></asp:Label>
			</td>					
		</td>
	</tr>
	
</table>

    
</div>


    </form>
</body>
</html>
