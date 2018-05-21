<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialPOList.aspx.cs" Inherits="Monsees._Default_Orders" MasterPageFile="~/MasterPages/Monsees.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="headContent">

    <title>Subcontract PO Manager</title>
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

<asp:Content runat="server" ContentPlaceHolderID="bodyContent">
 
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
                    <asp:Button ID="ItemViewButton" runat="server" Text="PO Item View" 
                        onclick="ItemViewButton_Click" />
		</td>
   <td align="center" valign="middle">
                    <asp:Button ID="OrderViewButton" runat="server" Text="Order View" 
                        onclick="OrderViewButton_Click" />
		</td>
   
            </tr>
        </table>
         
              

	<table itemstyle-cssclass="GridviewTable">

			

		<tr>
	<td colspan="15">
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   <asp:MultiView ID="OrdersMultiView" runat="server" ActiveViewIndex="0">
                       <asp:View ID="POItems" runat="server">
                           <asp:GridView ID="ItemViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceItem" GridLines="Vertical" 
                               onrowcommand="ItemViewGrid_RowCommand" Width="100%" 			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  
				   <asp:BoundField DataField="SubcontractID" HeaderText="PO #" 
                                       SortExpression="SubcontractID">
                                    </asp:BoundField>
				   <asp:BoundField DataField="SubcontractItemID" HeaderText="Item #" 
                                       SortExpression="subcontractItemID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   
				   <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="LineItem" HeaderText="Item #" 
                                       SortExpression="LineItem">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Workcode"  
                                       HeaderText="Workcode" SortExpression="Workcode">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                    <asp:BoundField DataField="Notes" 
                                        HeaderText="Notes" SortExpression="Notes">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
<asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>     				   
                                   <asp:BoundField DataField="Each" HeaderText="Each" 
                                       SortExpression="Each">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Total" HeaderText="Total" 
                                       SortExpression="Total">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				                            
			           <asp:BoundField DataField="DateSent" HeaderText="Issued" SortExpression="DateSent">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           <asp:BoundField DataField="DateReturned" HeaderText="Returned" SortExpression="Returned">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate">
				   	<ItemStyle HorizontalAlign="Left" />
</asp:BoundField>
				   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>	
<asp:BoundField DataField="Revision Number" HeaderText="Rev #" SortExpression="Revision Number">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>					   
     				   <asp:TemplateField HeaderText="Open">
    				   	<ItemTemplate>
        					<asp:CheckBox ID="ItemRecdCheck" runat="server" HeaderText="Open" 
        						Checked='<%#bool.Parse(Eval("HasDetail").ToString())%>' />
    				   	</ItemTemplate>
				   </asp:TemplateField>

				   
				  
				   <asp:ButtonField CommandName="Attach" Text="Attach" HeaderText="Cert.">
                                   	<ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
				   	
				   <asp:ButtonField CommandName="ViewPOSub" Text="P.O." HeaderText="">
                                   	<ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 	
   				                                             
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                      </asp:View>
                       <asp:View ID="Orders" runat="server">                                       
                           <asp:GridView ID="OrderViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePO" GridLines="Vertical" 
                               onrowcommand="OrderViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>                                   
				   <asp:BoundField DataField="SubcontractID" HeaderText="PO #" 
                                       SortExpression="SubcontractID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="VendorName" HeaderText="Vendor" 
                                       SortExpression="VendorName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   				   
                                   <asp:BoundField DataField="Name"  
                                       HeaderText="Written By" SortExpression="Name">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
<asp:BoundField DataField="Description" HeaderText="Description" 
                                       SortExpression="Description">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                   <asp:BoundField DataField="Note" HeaderText="Notes" 
                                       SortExpression="Note">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="ShipVia" HeaderText="ShipVia" 
                                       SortExpression="ShipVia">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Total" HeaderText="Total" 
                                       SortExpression="Total">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="IssueDate" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Issued" SortExpression="IssueDate">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>        
				   <asp:BoundField DataField="DueDate" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Due Date" SortExpression="DueDate">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:TemplateField HeaderText="Open"> 
    					<ItemTemplate>
        					<asp:CheckBox ID="IsOpenChk" runat="server" HeaderText="Open" 
        						Checked='<%#bool.Parse(Eval("IsOpen").ToString())%>' />
    					</ItemTemplate>
				   </asp:TemplateField>	 
				   <asp:TemplateField HeaderText="To QB"> 
    					<ItemTemplate>
        					<asp:CheckBox ID="POQBPosted" runat="server" HeaderText="To QB" 
        						Checked='<%#bool.Parse(Eval("PostedToQB").ToString())%>' />
    					</ItemTemplate>
				   </asp:TemplateField>	 
<asp:ButtonField CommandName="ViewPOSub" Text="View" HeaderText="">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>    				                                             
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                      </asp:View>

</asp:Multiview>
                 </p>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    
    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSourceItem" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SubcontractItems Order By SubcontractItemID DESC" EnableCaching="False" FilterExpression="SubcontractID like '{0}%'">
<FilterParameters>
        
        <asp:Parameter Name="SubcontractPOFilter" DefaultValue=""
             Type="Int32" ConvertEmptyStringToNull="true"/>    

    </FilterParameters>


    </asp:SqlDataSource>

<asp:SqlDataSource ID="MonseesSqlDataSourcePO" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SubcontractPOs Order By SubcontractID DESC" EnableCaching="False" >



    </asp:SqlDataSource>



    <asp:SqlDataSource ID="SubcontractPOList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT SubcontractID From Subcontract">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="WorkcodeList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Workcode From Workcode">
    </asp:SqlDataSource> 

    
    <asp:SqlDataSource ID="IssueDateList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT IssueDate From Subcontract">
    </asp:SqlDataSource>  

   
    <asp:SqlDataSource ID="ReceivedList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Received From SubcontractItems">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="PostedToQBList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PostedToQB From SubcontractItems">
    </asp:SqlDataSource>  

</asp:Content>