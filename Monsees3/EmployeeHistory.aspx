<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="EmployeeHistory.aspx.cs" Inherits="Monsees._Default_Employee"%>

<asp:Content ContentPlaceHolderID="headContent"  runat="server">
    <title>Production Schedule</title>
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
         <table class="style1" width="100%">
            <tr>
                <td align="left" valign="middle" class="style2">
                    <asp:Label ID="UserNameLabel" runat="server" Text='<%#Eval("EmployeeName") %>'></asp:Label>
					
                </td>
                
                
                <td align="right" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
              

	<table itemstyle-cssclass="GridviewTable">

		<tr BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White">		
			<td width="5%" align="left">
    				<asp:DropDownList ID="AvailableJobList" DataSourceID="populatejoblist"
					AutoPostBack="true" DataValueField="JobNumber" runat="server" Width="60px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>

			</td>
			<td width="1%">
				
			</td>
			<td width="10%">
				
			</td>
			<td width="8%" align="left">
    				<asp:DropDownList ID="AvailablePartsList" DataSourceID="populatepartslist"
					AutoPostBack="true" DataValueField="PartNumber" runat="server" Width="110px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
			</td>
			<td width="3%">
				
			</td>
			<td width="13%">
				
			</td>
			<td width="7%">
				
			</td>
			<td width="3%">
				
			</td>
			<td width="8%">
				
			</td>
			<td width="8%">
				
			</td>
			<td width="13%">
				
			</td>
			<td width="8%" align="left">
    				<asp:DropDownList ID="OperationList" DataSourceID="populateopslist"
					AutoPostBack="true" DataValueField="OperationName" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
			</td>
			<td width="6%">
				
			</td>
			<td width="4%">
				
			</td>
			<td width="4%">

			</td>
		</tr>
		<tr>
			<td colspan="15">
      			        
				                  <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                  
                                  
	
                      
 
                    
                           <asp:GridView ID="ProductionViewGrid" runat="server" 
                               AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                               BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" 
                               EnableSortingAndPagingCallbacks="False" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" PageSize="50" Width="100%" 
                               AllowPaging="True" AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
                                    <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="Expand" runat="server" CommandName="Deliveries" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="+" ></asp:LinkButton>
					</ItemTemplate>
<ItemStyle HorizontalAlign="Center" width="0%" />
				   </asp:TemplateField>
				   <asp:BoundField DataField="JobNumber" HeaderText="Job #" 
                                       SortExpression="JobNumber" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CompanyName"  
                                       HeaderText="Customer" SortExpression="CompanyName" ItemStyle-Width="16%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Part Number">
					<ItemTemplate>
					<asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>' HeaderText="Part Number"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="13%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="MinOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="First Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="7%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="SumOfQuantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   
				   <asp:BoundField DataField="OperationName" HeaderText="Operation" SortExpression="OperationName" ItemStyle-Width="15%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>       
				   <asp:BoundField DataField="Hours" HeaderText="Hours" SortExpression="Hours" ItemStyle-Width="4%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>    
				   <asp:BoundField DataField="Login" HeaderText="Time Stamp" SortExpression="Login" ItemStyle-Width="15%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>                                
			       <asp:ButtonField CommandName="Inspection" Text="Report" HeaderText="Inspection" ItemStyle-Width="4%">
                    <ControlStyle Font-Size="Small" />
                    </asp:ButtonField>
                    <asp:CheckBoxField DataField="AreFixtures" SortExpression="AreFixtures" ItemStyle-Width="3%" />
                    <asp:ButtonField CommandName="Fixturing" Text="Fixtures" ItemStyle-Width="4%">
                        <ControlStyle Font-Size="Small" />
                    </asp:ButtonField>
                    <asp:CheckBoxField DataField="IsDrawing" SortExpression="IsDrawing" ItemStyle-Width="3%" />
   				   <asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("[Revision Number]") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>      
			           
                                   
                    		   <asp:TemplateField>
					<ItemTemplate>
					</td>
					</tr>
					<tr>
					<td colspan="100%">
					<div id='JobItemIDTag' style="display:inline;position:relative;left:25px;" >
					<asp:GridView ID="DeliveryViewGrid" runat="server" AllowPaging="False" 
                               			AutoGenerateColumns="False" BackColor="White" 
                               			BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               			DataKeyNames="JobItemID" GridLines="Vertical"  
                               			AllowSorting="True" Font-Size="Small">
                               			<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               			<Columns>
				   <asp:BoundField DataField="CurrDelivery"  
                                       HeaderText="Delivery" SortExpression="CurrDelivery">
                                       <ItemStyle HorizontalAlign="Center" />
					<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Quantity" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
					<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="PONumber"  
                                       HeaderText="PO Number" SortExpression="PONumber">
                                       <ItemStyle HorizontalAlign="Center" />
					<ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
				   <asp:BoundField DataField="Shipped"  
                                       HeaderText="Shipped" SortExpression="Shipped">
					<ControlStyle Font-Size="Small" />
                                       <ItemStyle HorizontalAlign="Center" />
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
                       </asp:View>
                   </asp:MultiView>
                 </p>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    

    </div>

 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
         EnableCaching="False" FilterExpression="JobNumber like '{0}%'
                and PartNumber like '{1}%' and OperationName like '{2}%'">
<FilterParameters>
        <asp:ControlParameter Name="Job" ControlID="AvailableJobList"
            PropertyName="SelectedValue" />
        <asp:ControlParameter Name="Part" ControlID="AvailablePartsList"
            PropertyName="SelectedValue" />
<asp:ControlParameter Name="Operation" ControlID="OperationList"
            PropertyName="SelectedValue" />
    </FilterParameters>


    </asp:SqlDataSource>

   <asp:SqlDataSource ID="MonseesSqlDataSourceDeliveries" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="populatejoblist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT JobNumber From Job">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="populatepartslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PartNumber From Detail WHERE PartNumber Is Not Null">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	OperationName From Operation">
    </asp:SqlDataSource>  

</asp:Content>