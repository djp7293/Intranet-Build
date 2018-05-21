<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="ActivePartList.aspx.cs" Inherits="Monsees._ActivePartsList"%>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <title>Delivery Schedule</title>
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
         
              

	<table itemstyle-cssclass="GridviewTable">

		
			<td colspan="15">
      			        
				                  <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   
                           <asp:GridView ID="PartViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="PartViewGrid_RowCommand" Width="100%" AllowSorting="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
                                   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="Expand" runat="server" CommandName="Deliveries" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="+" ></asp:LinkButton>
					</ItemTemplate>

				   </asp:TemplateField>
<asp:BoundField DataField="RevisionID" HeaderText="Item #" 
                                       SortExpression="RevisionID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="PartNumber"  
                                       HeaderText="Part Number" SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
				   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   		   <asp:BoundField DataField="FirstDelivery" HeaderText="FirstDelivery" 
                                       SortExpression="FirstDelivery" DataFormatString="{0:d}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
<asp:BoundField DataField="LastDelivery" HeaderText="LastDelivery" 
                                       SortExpression="LastDelivery" DataFormatString="{0:d}">
                                       <ItemStyle HorizontalAlign="Center"/>
                                   </asp:BoundField>
		
                                   
                                   
				   
<asp:TemplateField>
					<ItemTemplate>
					</td>
</tr>
<tr>
					<td colspan="100%">
					<div id='DeliveryIDTag' style="display:inline;position:relative;left:25px;" >
						<asp:GridView ID="DeliveryViewGrid" runat="server" AllowPaging="False" 
                               				AutoGenerateColumns="False" BackColor="White" 
                               				BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               				GridLines="Vertical"  
                               				AllowSorting="True" Font-Size="Small">
                               				<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               				<Columns>
				<asp:BoundField DataField="DeliveryID"  
                                       HeaderText="Item #" SortExpression="DeliveryID">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Quantity" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
<asp:BoundField DataField="Delivery"  
                                       HeaderText="Delivery" SortExpression="CurrDelivery">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="PONumber"  
                                       HeaderText="PO #" SortExpression="PONumber">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
				               
							</Columns>
						</asp:Gridview>
					</div>
					</td>
					</tr>
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


		 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" ></asp:SqlDataSource>

</asp:Content>
