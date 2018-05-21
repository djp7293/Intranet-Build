<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixtureTag.aspx.cs" Inherits="Monsees._Default_FixtureTag"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            width: 64px;
        }
        .auto-style3 {
            width: 1033px;
        }
        .auto-style4 {
            height: 23px;
            width: 1033px;
        }
    </style>
</head>
<body style="width: 359px">
    <form id="form1" runat="server">
    <div align="center">
              

<table itemstyle-cssclass="TableBody">
	<tr width="576pt>
		<td width="50%">
			<td align="left" width="200pt"> 
				Company:</td>
		</td>
		<td 88pt" style="text-align: left" class="auto-style3"> 
				<asp:Label ID="CompanyName" runat="server"></asp:Label>
			</td>
			<td align="left" class="auto-style2">
				&nbsp;</td>
		</td>
	</tr>
	<tr width="576pt">
			<td align="left" width="60pt" class="auto-style1">Part #: 
			</td>
			<td align="left" class="auto-style4">
				<asp:Label ID="PartNumber" runat="server" ></asp:Label>
			</td>
		</td>
		</td>
	</tr>
	<tr width="576pt>
		<td align="left" width="50%">
			<td align="left"> 
				Desc.:</td>
		</td>
		<td align="left" class="auto-style3">
				<asp:Label ID="DrawingNumber" runat="server" ></asp:Label>
			</td>
	</tr>
	<tr width="576pt">
			<td align="left" width="60pt">Lot #: 
			</td>
			<td align="left" class="auto-style3">
				<asp:Label ID="JobItem" runat="server" ></asp:Label>
			</td>
			<td align="left" class="auto-style2">&nbsp;</td>
			<td align="left" width="68pt">
				&nbsp;</td>
		</td>
		</td>
	</tr>
	<tr width="576pt">
			<td align="left" width="60pt">Source Lot: 
			</td>
			<td align="left" class="auto-style3">
				<asp:Label ID="SourceLot" runat="server" ></asp:Label>
			</td>
			<td align="left" class="auto-style2">Loc: 
			</td>
			<td align="left" width="68pt">
				<asp:Label ID="Location1" runat="server" ></asp:Label>
			</td>
		</td>
		</td>
	</tr>
	<tr width="576pt">
			<td align="left" width="60pt">First Op.: 
			</td>
			<td align="left" width="100pt" colspan="3">
				<asp:Label ID="OperationName" runat="server" ></asp:Label>
			</td>
			
		</td>
		</td>
	</tr>
	<tr width="576pt">
			<td align="left" width="60pt">InventoryID: 
			</td>
			<td align="left" class="auto-style3">
				<asp:Label ID="InventoryID" runat="server" ></asp:Label>
			</td>
			
		</td>
		<td class="auto-style2">
			<td align="left" width="60pt">
			</td>
		</td>
	</tr>
		
</table>

    
</div>


    </form>
</body>
</html>
