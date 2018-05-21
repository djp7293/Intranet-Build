<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="Monsees._Inventory"%>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
         
              

	  <div style="align-content:flex-start"><table><tr><td>
                               Company: <asp:TextBox ID="CompanyFilter" runat="server" >
        				       
                               </asp:TextBox>
                               </td><td> PartNumber: <asp:TextBox ID="PartFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td> DrawingNumber: <asp:Textbox ID="DescFilter" runat="server" >
        				</asp:Textbox>

<td align="left">Status: 
				<asp:DropDownList ID="StatusList" DataSourceID="populatestatuslist"
					DataValueField="Status" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
</td><td><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />
                              </td><td> <asp:Button ID="exportdata" Text="Export" OnClick="btnDownload_Click" runat="server" /></td></tr></table><table><tr>
		
			<td colspan="2">
      			        
				                  <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   
                           <asp:GridView ID="InventoryViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="InventoryViewGrid_RowCommand" Width="100%" AllowSorting="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
                                   
				   <asp:BoundField DataField="InventoryID" HeaderText="InventoryID" 
                                       SortExpression="InventoryID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="LotNumber" HeaderText="Lot #" 
                                       SortExpression="LotNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CompanyName" HeaderText="Company" 
                                       SortExpression="CompanyName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="PartNumber"  
                                       HeaderText="Part #" SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Revision Number"  
                                       HeaderText="Rev #" SortExpression="Revision Number">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                   <asp:BoundField DataField="DrawingNumber"  
                                       HeaderText="Description" SortExpression="DrawingNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
				   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Status" HeaderText="Status" 
                                       SortExpression="Status">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Location1" HeaderText="Location" 
                                       SortExpression="Location1">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Note1" HeaderText="Note" 
                                       SortExpression="Note1">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                    <asp:ButtonField CommandName="Inspection" Text="Inspection Report" />
				   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
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
        </asp:Panel>
</td>
</tr>
</table>

    

    </div>

 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">

    </asp:SqlDataSource>

   
    <asp:SqlDataSource ID="populatestatuslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Status From InvStatus">
    </asp:SqlDataSource>  	

    </form>
</body>
</html>
