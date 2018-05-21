<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master"   AutoEventWireup="true" CodeBehind="DeliverySchedule.aspx.cs" Inherits="Monsees._Delivery"  %>
<asp:Content ContentPlaceHolderID="headContent"  runat="server">
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

<asp:Content ContentPlaceHolderID="bodyContent"  runat="server">

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
      			        
				                 <asp:GridView ID="DeliveryViewGrid" runat="server" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceDel" GridLines="Vertical" DataKeyNames="DeliveryID"
                               onrowcommand="DeliveryViewGrid_RowCommand" Width="100%" AllowSorting="True" 
                               onselectedindexchanged="DeliveryViewGrid_SelectedIndexChanged" onrowdatabound="DeliveryViewGrid_RowDataBound" EnableModelValidation="True">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
                                  <asp:TemplateField><ItemTemplate>
					            <a href="JavaScript:divexpandcollapse('div<%# Eval("DeliveryID") %>');">
                                    +</a>  
                        
					            </ItemTemplate></asp:TemplateField>
				   <asp:CheckBoxField DataField="MaxOfRTS" HeaderText="RTS" ReadOnly="True" SortExpression="MaxOfRTS" />
                                      
				   <asp:BoundField DataField="CurrDelivery" HeaderText="Delivery" 
                                       SortExpression="CurrDelivery" DataFormatString="{0:MM-dd-yyyy}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CAbbr"  
                                       HeaderText="Cust. Code" SortExpression="CAbbr">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="PONumber"  
                                       HeaderText="PO Number" SortExpression="PONumber">
                                       <ItemStyle HorizontalAlign="Left" />
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
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
                             <asp:CheckBoxField DataField="CertCompReqd" HeaderText="C" ReadOnly="True" SortExpression="CertCompReqd" />
                                   <asp:CheckBoxField DataField="PlateCertReqd" HeaderText="P" ReadOnly="True" SortExpression="PlateCertReqd" />
                                   <asp:CheckBoxField DataField="MatlCertReqd" HeaderText="M" ReadOnly="True" SortExpression="MatlCertReqd" />
                                   <asp:CheckBoxField DataField="SerializationReqd" HeaderText="S" ReadOnly="True" SortExpression="SerializationReqd" />      
				   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>  
<asp:TemplateField>
					<ItemTemplate>
				
<tr>
					<td colspan="100%">
					<div id="div<%# Eval("DeliveryID") %>"  style="display:none; left: 15px;">                         
						<asp:gridview ID="LotViewGrid" runat="server" AllowPaging="False" 
                               				AutoGenerateColumns="False" BackColor="White" 
                               				BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               				GridLines="Vertical" onrowcommand="LotViewGrid_RowCommand"  
                               				AllowSorting="True" Font-Size="Small">
                               				<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               				<Columns>
				<asp:BoundField DataField="LotNumber"  
                                       HeaderText="Lot #" SortExpression="LotNumber">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Quantity" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="JobNumber"  
                                       HeaderText="Job #" SortExpression="JobNumber">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
				   <asp:BoundField DataField="RTS"  
                                       HeaderText="RTS" SortExpression="RTS">
<ControlStyle Font-Size="Small" />
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>  
                                                   <asp:CheckBoxField DataField="PCert" HeaderText="P" ReadOnly="True" SortExpression="PCert" />
                                                   <asp:CheckBoxField DataField="MCert" HeaderText="M" ReadOnly="True" SortExpression="MCert" />
	 				                <asp:TemplateField>
						                <ItemTemplate>
							                <a href="/Lot.aspx?id=<%#Eval("LotNumber")%>">Lot</a>
						                </ItemTemplate>
					                </asp:TemplateField>
				  
                                   <asp:ButtonField CommandName="Print Report" Text="Print Report" HeaderText="">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>                  
							</Columns>
						</asp:gridview>
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

<asp:SqlDataSource ID="MonseesSqlDataSourceDel" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From Deliveries WHERE Shipped = 0 ORDER BY CurrDelivery" EnableCaching="False" >



    </asp:SqlDataSource>

   

    <asp:SqlDataSource ID="MonseesSqlDataSourceLots" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>

     <script type="text/javascript">
            function divexpandcollapse(divname) {
                var div = document.getElementById(divname);
                
                if (div.style.display == "none") {
                    div.style.display = "inline";
                    
                } else {
                    div.style.display = "none";
                    
                }
            }
    </script>


</asp:Content>