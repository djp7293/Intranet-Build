<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="ClearConfirmItems.aspx.cs" Inherits="Monsees._Default_ClearConfirm"%>

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
      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
</asp:ScriptManager>
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
              

	

    </div>
    <br /><br /><b><u>Operations to Clear:</u></b><br />
    <asp:GridView ID="UpdatedOperations" EmptyDataText="There are no new operations logs to be cleared." runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" DataKeyNames="JobSetupID, ProcessID" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="UpdatedOps" OnRowCommand="UpdatedOperations_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID" />
            <asp:BoundField DataField="Job_Number" HeaderText="Job #" SortExpression="Job_Number" />
            <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
            <asp:BoundField DataField="Revision_Number" HeaderText="Rev." SortExpression="Revision_Number" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
            <asp:BoundField DataField="OperationName" HeaderText="Operation" SortExpression="OperationName" />
            <asp:BoundField DataField="Name" HeaderText="Employee" SortExpression="Name" />
            <asp:BoundField DataField="Logout" HeaderText="Timestamp" SortExpression="Logout" />
            <asp:BoundField DataField="QuantityIn" HeaderText="Qty In" SortExpression="QuantityIn" />
            <asp:BoundField DataField="QuantityOut" HeaderText="Qty Out" SortExpression="QuantityOut" />
            <asp:BoundField DataField="NextOp" HeaderText="Next Op." SortExpression="NextOp" />
            <asp:ButtonField CommandName="Clear" Text="Clear">
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
                <asp:ButtonField CommandName="Reverse" Text="Mark Operation Incomplete">
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
            <asp:ButtonField CommandName="Delete" Text="Remove Process Addition Completely">
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
                    <asp:ButtonField CommandName="ViewLot" Text="View Lot" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        <asp:SqlDataSource ID="UpdatedOps" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [JobSetupID], [ProcessID], [JobItemID], [Job_Number], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [CompanyName], [OperationName], [Name], [Logout], [QuantityIn], [QuantityOut], [NextOp] FROM [ClearOperations]"></asp:SqlDataSource>
    <br /><b><u>Operations to Authorize:</u></b><br />
    <asp:GridView ID="GridView1" EmptyDataText="There are no new operations to authorize." runat="server" AutoGenerateColumns="False" DataKeyNames="JobSetupID" DataSourceID="AddOperations" EnableModelValidation="True" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID" />
            <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
            <asp:BoundField DataField="Revision_Number" HeaderText="Rev." SortExpression="Revision_Number" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
            <asp:BoundField DataField="OperationName" HeaderText="New Operation" SortExpression="OperationName" />
            <asp:BoundField DataField="Setup_Cost" HeaderText="Setup" SortExpression="Setup_Cost" />
            <asp:BoundField DataField="Operation_Cost" HeaderText="Run (mins)" SortExpression="Operation_Cost" />
            <asp:BoundField DataField="ProcessOrder" HeaderText="Proc. #" SortExpression="ProcessOrder" />
            <asp:BoundField DataField="Name" HeaderText="Added By:" SortExpression="Name" />
            <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
             <asp:ButtonField CommandName="Authorize" Text="Authorize">
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
                <asp:ButtonField CommandName="Reject" Text="Reject">
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
             <asp:ButtonField CommandName="Permanent" Text="Make Permanent">
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="AddOperations" runat="server" ConnectionString="Data Source=SERVER1\SQLEXPRESS;Initial Catalog=Monsees2;Integrated Security=True;Connect Timeout=120" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [JobItemID], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [CompanyName], [OperationName], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [ProcessOrder], [Name], [Comments], [JobSetupID] FROM [ClearAddedOperations]"></asp:SqlDataSource>
    <br />
    <br /><b><u>Jobs to Clear:</u></b><br />    
    <asp:GridView ID="NewJobs" EmptyDataText="There are no new jobs to clear." runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="ClearJobs" DataKeyNames="JobID" OnRowCommand="NewJobs_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="JobNumber" HeaderText="Job #" SortExpression="JobNumber" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" SortExpression="ContactName" />
                <asp:BoundField DataField="CreateDate" HeaderText="Create Date" SortExpression="CreateDate"   DataFormatString="{0:MM-dd-yyyy}" />
                <asp:ButtonField CommandName="ViewJob" Text="View Job" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
            </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        <asp:SqlDataSource ID="ClearJobs" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [JobID], [JobNumber], [CompanyName], [ContactName], [CreateDate] FROM [ClearJobs]"></asp:SqlDataSource>
    <br /><br /><b><u>Work in Process Inventory to Clear (quantity additions to current lots):</u></b><br />    
    <asp:GridView ID="WIPInventory" EmptyDataText="There are no new WIP inventory assignments to confirm." runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="ClearWIP" DataKeyNames="DeliveryItemID" OnRowCommand="WIPInventory_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="LotNumber" HeaderText="Lot #" SortExpression="LotNumber" />
                <asp:BoundField DataField="JobNumber" HeaderText="Job #" SortExpression="JobNumber" />
                <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
                <asp:BoundField DataField="Revision_Number" HeaderText="Rev." SortExpression="Revision_Number" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="NewJobQty" HeaderText="New Job Qty" SortExpression="NewJobQty" />
                <asp:BoundField DataField="AddedQty" HeaderText="Qty Added" SortExpression="AddedQty" />
                <asp:BoundField DataField="CurrDelivery" HeaderText="New Delivery" SortExpression="CurrDelivery" />
                <asp:BoundField DataField="MinOfCurrDelivery" HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" />
                <asp:ButtonField CommandName="Accept" Text="Accept" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
                <asp:ButtonField CommandName="Reject" Text="Reject" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
                <asp:ButtonField CommandName="Modify" Text="Modify" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
            </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        <asp:SqlDataSource ID="ClearWIP" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [DeliveryItemID], [LotNumber], [JobNumber], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [NewJobQty], [AddedQty], [CurrDelivery], [MinOfCurrDelivery] FROM [ClearWIPInventory]"></asp:SqlDataSource>
    <br /><br /><b><u>Pending Inventory Confirmation:</u></b><br />    
    <asp:GridView ID="Inventory" EmptyDataText="There are no pending inventory confirmations." runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="ClearInventory" DataKeyNames="DeliveryItemID" OnRowCommand="Inventory_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="LotNumber" HeaderText="Lot #" SortExpression="LotNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Assigned Qty" SortExpression="Quantity" />
                <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
                <asp:BoundField DataField="Revision_Number" HeaderText="Rev." SortExpression="Revision_Number" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="TotInvQty" HeaderText="Inventory Qty" SortExpression="TotInvQty" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="Location1" HeaderText="Loc." SortExpression="Location1" />
                <asp:BoundField DataField="PONumber" HeaderText="PO #" SortExpression="PONumber" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
                <asp:BoundField DataField="OrderQty" HeaderText="Order Qty" SortExpression="OrderQty" />
                
            </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        <asp:SqlDataSource ID="ClearInventory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [DeliveryItemID], [LotNumber], [Quantity], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [TotInvQty], [Status], [Location1], [PONumber], [CompanyName], [OrderQty] FROM [ClearInventory]"></asp:SqlDataSource>
    <br /><br /><b><u>Material Orders Requiring Approval:</u></b><br />    
    <table width="100%"><tr><td style="vertical-align:top">
    <asp:GridView ID="MaterialOrders" EmptyDataText="There are no material orders pending approval." runat="server" BackColor="White" OnRowDataBound="MaterialOrders_RowDataBound" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataKeyNames="MaterialPOID" DataSourceID="ClearMatlOrders" OnRowCommand="MaterialOrders_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
				        <a href="JavaScript:divexpandcollapse('div<%# Eval("MaterialPOID") %>');">+</a>                         
				    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" ReadOnly="True" SortExpression="MaterialPOID" />
                <asp:BoundField DataField="Name" HeaderText="Owner" SortExpression="Name" />
                <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName" />
                <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate"   DataFormatString="{0:MM-dd-yyyy}" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact" />
                <asp:BoundField DataField="ConfirmationNum" HeaderText="Confirmation #" SortExpression="ConfirmationNum" />
                <asp:BoundField DataField="ShippingCharge" HeaderText="Shipping Charge" SortExpression="ShippingCharge" />
                <asp:ButtonField CommandName="Approve" Text="Approve" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
              
                <asp:TemplateField>
                    <itemtemplate>
                        <tr>
					        <td colspan="12">                        
					        <div id="div<%# Eval("MaterialPOID") %>"  style="display:none; left: 15px;"> 
                                <asp:GridView ID="MaterialItems" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataKeyNames="MatPriceID">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Material" SortExpression="MaterialName" >
                                       <ItemTemplate>
                                           <asp:Label ID="MatlLbl" runat="server" Text='<%# Eval("MaterialName") %>' />
                                       </ItemTemplate>                                       
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Dimension" SortExpression="Dimension" >
                                       <ItemTemplate>
                                           <asp:Label ID="DimLbl" runat="server" Text='<%# Eval("Dimension") %>' />
                                       </ItemTemplate>                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Size" SortExpression="Size" >
                                       <ItemTemplate>
                                           <asp:Label ID="SizeLbl" runat="server" Text='<%# Eval("Size") %>' />
                                       </ItemTemplate>                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Length" SortExpression="Length" >
                                       <ItemTemplate>
                                           <asp:Label ID="LengthLbl" runat="server" Text='<%# Eval("Length") %>' />
                                       </ItemTemplate>                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty" SortExpression="quantity" >
                                       <ItemTemplate>
                                           <asp:Label ID="QtyLbl" runat="server" Text='<%# Eval("quantity") %>' />
                                       </ItemTemplate>                                     
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate" >
                                       <ItemTemplate>
                                           <asp:Label ID="DueDateLbl" runat="server" Text='<%# Eval("DueDate") %>' />
                                       </ItemTemplate>                                     
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Item #" SortExpression="ItemNum" >
                                       <ItemTemplate>
                                           <asp:Label ID="ItemNumLbl" runat="server" Text='<%# Eval("ItemNum") %>' />
                                       </ItemTemplate>                                    
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Shipping" SortExpression="Shipping" >
                                       <ItemTemplate>
                                           <asp:Label ID="ShippingLbl" runat="server" Text='<%# Eval("Shipping") %>' />
                                       </ItemTemplate>                                    
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ship Charge" SortExpression="ShippingCharge" >
                                       <ItemTemplate>
                                           <asp:Label ID="ShipCostLbl" runat="server" Text='<%# Eval("ShippingCharge") %>' />
                                       </ItemTemplate>                                    
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="cost" >
                                       <ItemTemplate>
                                           <asp:Label ID="CostLbl" runat="server" Text='<%# Eval("cost") %>' DataFormatString="${0:#,0}" />
                                       </ItemTemplate>                                     
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Conf. #" SortExpression="ConfirmationNum" >
                                       <ItemTemplate>
                                           <asp:Label ID="ConfLbl" runat="server" Text='<%# Eval("ConfirmationNum") %>' />
                                       </ItemTemplate>                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Cert Reqd" SortExpression="MinOfMatlCertReqd" >
                                       <ItemTemplate>
                                           <asp:Checkbox ID="CertCheck" runat="server" Checked='<%# Eval("MinOfMatlCertReqd") %>' Enabled="false" />
                                       </ItemTemplate>                                       
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Rec'd" SortExpression="received">
                                       <ItemTemplate>
                                           <asp:CheckBox ID="RecdCheck" runat="server" Checked='<%# Eval("received") %>' Enabled="false" />
                                       </ItemTemplate>                                      
                                   </asp:TemplateField>         
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
					        </div>
					        </td>
                        </tr>
                    </itemtemplate>
                </asp:TemplateField>
            </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
</td></tr></table>
    <br /><br /><b><u>Completed Lots Pending Review:</u></b><br />
    <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="JobItemID" DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" onrowdatabound="ProductionViewGrid_RowDataBound" OnPreRender="ProductionViewGrid_PreRender" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                   <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					<ItemTemplate>
					<asp:Button runat="server" ID="ExpColMain" OnClick="ExpandCollapse" Text="+"  />
					</ItemTemplate>

				   </asp:TemplateField>
				   <asp:BoundField DataField="Job_Number" HeaderText="Job #" 
                                       SortExpression="Job_Number" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CAbbr"  
                                       HeaderText="Cust. Code" SortExpression="CAbbr" ItemStyle-Width="5%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                      				   <asp:TemplateField ItemStyle-Width="9%" HeaderText="Part Number">
					<ItemTemplate>
					<asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>'></asp:LinkButton>
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
                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="6%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                 <asp:BoundField DataField="Abbr" HeaderText="PM" 
                                       SortExpression="Abbr" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>  
				   <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
				   <asp:ButtonField CommandName="InitCAR" Text="CAR Request" ItemStyle-Width="4%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                      
                                   </asp:ButtonField>
                                   


   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>
                                   <asp:TemplateField  HeaderText="Locked" >
                                       <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="lockflag"  Checked='<%# Eval("Locked")%>'/>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="RootLot" HeaderText="Root Lot" />
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <asp:Button runat="server" ID="lockbutton" CommandName="Lock" CommandArgument='<%# Container.DataItemIndex %>' Text="Lock Revision to This Lot" />
                                      
                                           <asp:Button runat="server" ID="unlockbutton" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Unlock" Text="Unlock" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:ButtonField CommandName="Clear" Text="Clear" />
                                   	 <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%# Convert.ToString(Eval("NewPart")) %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Convert.ToString(Eval("CAbbr")) %>' />
                              </ItemTemplate>
                          </asp:TemplateField>	
				<asp:TemplateField ItemStyle-Width="1%"><itemtemplate>
                <tr>
					<td colspan="16">
                        
					 <div runat="server" id="div1" visible="false" >  
                        <table>
                            <tr>
                            <td colspan="2">
                                <br /><b><u>Corrective Actions:</u></b><br />
                    <asp:GridView ID="CARView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" OnRowCommand="CARView_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="CARID" HeaderText="CAR #" SortExpression="CARID" />
            <asp:BoundField DataField="Revision Number" HeaderText="Rev" SortExpression="Revision Number" />
            <asp:BoundField DataField="InitiationDate" HeaderText="Init. Date" SortExpression="InitiationDate" />
            <asp:BoundField DataField="DueDate" HeaderText="Due" SortExpression="DateDate" />
            <asp:BoundField DataField="Definition" HeaderText="Issue" SortExpression="Definition" />
            <asp:BoundField DataField="ImpEmployee" HeaderText="Owner" SortExpression="ImpEmployee" />
            <asp:ButtonField CommandName="ViewCAR" Text="View" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                            </td>
                               </tr>
                            <tr><td style="width:33%; vertical-align:top">      
                <asp:ListView ID="ListView2" runat="server" DataKeyNames="Expr1, Active Version" EnableModelValidation="True" OnItemCommand="ListView3_ItemCommand" OnItemDataBound="ListView3_ItemDataBound" OnItemEditing="ListView3_ItemEditing" OnItemUpdating="ListView3_ItemUpdating" OnItemCanceling="ListView3_ItemCanceling" >

        
        <EmptyDataTemplate>
            <table style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        
        <ItemTemplate>
            <td runat="server" style="background-color:#DCDCDC;color: #000000;vertical-align:top" >Heat Treat:
                <asp:Label ID="Heat_TreatLabel" runat="server" Text='<%# Eval("Heat_Treat") %>' />
                <br />Plating:
                <asp:Label ID="PlatingLabel" runat="server" Text='<%# Eval("Plating") %>' />
                 <br />Subcontract:
                <asp:Label ID="SubcontractLabel" runat="server" Text='<%# Eval("Subcontract") %>' />
                 <br />Subcontract2:
                <asp:Label ID="Subcontract2Label" runat="server" Text='<%# Eval("Subcontract2") %>' />
                <br />Scrap Rate:
                <asp:Label ID="ScrapLabel" runat="server" Text='<%# Eval("ScrapRate") %>' />
                <br />Hours:
                <asp:Label ID="EstimatedTotalHoursLabel" runat="server" Text='<%# Eval("EstimatedTotalHours") %>' />
                <br />Project Mgr:
                <asp:Label ID="PMLabel" runat="server" Text='<%# Eval("Abbr") %>' />
                
                
            </td><td runat="server" style="vertical-align:top" >Material: 
                <asp:Label ID="MaterialLabel" runat="server" Text='<%# Eval("Material") %>' />
                <br />Dimension:
                <asp:Label ID="MaterialDimLabel" runat="server" Text='<%# Eval("Dimension") %>' />
                <br />Size:
                <asp:Label ID="MaterialSizeLabel" runat="server" Text='<%# Eval("MaterialSize") %>' />
                <br />Length per Part:
                <asp:Label ID="LengthLabel" runat="server" Text='<%# Eval("Length") %>' />
                <br />Cut Length:
                <asp:Label ID="StockCutLabel" runat="server" Text='<%# Eval("StockCut") %>' />
                <br />Parts per Cut:
                <asp:Label ID="PartsPerCutLabel" runat="server" Text='<%# Eval("PartsPerCut") %>' />
                 <br />Purchased Cut:
                <asp:Checkbox ID="PurchaseCutLabel" Enable="false" runat="server" Checked='<%# Eval("PurchaseCut") %>' />
                <br />Drill:
                <asp:Checkbox ID="DrillLabel" Enable="false" runat="server" Checked='<%# Eval("Drill") %>' />
                 <br />Drill Size:
                <asp:Label ID="DrillSizeLabel" runat="server" Text='<%# Eval("DrillSize") %>' />
                 <br />Mat'l Source:
                <asp:Label ID="MatlSourceLabel" runat="server" Text='<%# Eval("MaterialSource") %>' /><br /><br />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" /><br /><br />
                <div  id="matlpurchase" runat="server">
                    <asp:Button runat="server" CommandName="MaterialQuote" ID="OnNewQuote" Text="Mat'l Assign / RFQ" /> Approval Req'd: <asp:CheckBox ID="Approval" runat="server" Checked="true" /><br />
                    Sugg. Vendor: <asp:DropDownList ID="VendorList" runat="server" AppendDataBoundItems="true" DataValueField="SubcontractID" DataTextField="VendorName" >
                                    <asp:ListItem Text="None Selected" Value=0 Selected="True" />
                                  </asp:DropDownList>
                 </div>
                 </td>
            </ItemTemplate>
            <EditItemTemplate>
            <td runat="server" style="background-color:#DCDCDC;color: #000000;vertical-align:top" >Heat Treat:
                <asp:DropDownList ID="HeatTreatList" runat="server" AppendDataBoundItems="true" DataValueField="WorkcodeID" DataTextField="Workcode"  >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                  <asp:HiddenField ID="hdHeatTreat" runat="server"
                        Value ='<%# Eval("HeatTreatID") %>' />  
                <br />Plating:
                <asp:DropDownList ID="PlatingList" runat="server" DataValueField="WorkcodeID" DataTextField="Workcode" >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdPlating" runat="server"
                        Value ='<%# Eval("PlatingID") %>' />
                 <br />Subcontract:
                <asp:DropDownList ID="SubcontractList" runat="server" DataValueField="WorkcodeID" DataTextField="Workcode"  >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdSubcontract" runat="server"
                        Value ='<%# Eval("SubcontractID") %>' />
                 <br />Subcontract2:
                <asp:DropDownList ID="Subcontract2List" runat="server" DataValueField="WorkcodeID" DataTextField="Workcode"  >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdSubcontract2" runat="server"
                        Value ='<%# Eval("SubcontractID2") %>' />
                 <br />Project Mgr:
                <asp:DropDownList ID="PMList" runat="server" DataValueField="EmployeeID" DataTextField="Name"  >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdPM" runat="server"
                        Value ='<%# Eval("ProjectManager") %>' />
                <br />Scrap Rate:
                <asp:Label ID="ScrapLabel" runat="server" Text='<%# Eval("ScrapRate") %>' />
                <br />Hours:
                <asp:Label ID="EstimatedTotalHoursLabel" runat="server" Text='<%# Eval("EstimatedTotalHours") %>' />

                
            </td><td runat="server" style="vertical-align:top" >Material: 
                <asp:DropDownList ID="MaterialList" runat="server" DataValueField="MaterialID" DataTextField="Material"  >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdMaterial" runat="server"
                        Value ='<%# Eval("MaterialID") %>' />
                <br />Dimension:
                <asp:DropDownList ID="MaterialDimList" runat="server" DataValueField="MaterialDimID" DataTextField="Dimension" AutoPostBack="true" OnSelectedIndexChanged="MaterialDimList_SelectedIndexChanged" >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdDimension" runat="server"
                        Value ='<%# Eval("[Material Dimension]") %>' />
                <br />Size:
                <asp:DropDownList ID="MaterialSizeList" runat="server" DataValueField="MaterialSizeID" DataTextField="Size"  >
                    <asp:ListItem Text="None Selected" Value="0" />
                    </asp:DropDownList>
                <asp:HiddenField ID="hdSize" runat="server"
                        Value ='<%# Eval("[Material Size]") %>' />
                <br />Length per Part:
                <asp:TextBox ID="LengthBox" runat="server" Text='<%# Eval("Length") %>' />
                <br />Cut Length:
                <asp:TextBox ID="StockCutBox" runat="server" Text='<%# Eval("StockCut") %>' />
                <br />Parts per Cut:
                <asp:Textbox ID="PartsPerCutBox" runat="server" Text='<%# Eval("PartsPerCut") %>' />
                 <br />Purchased Cut:
                <asp:Checkbox ID="PurchaseCutBox" Enable="true" runat="server" Checked='<%# Eval("PurchaseCut") %>' />
                <br />Drill:
                <asp:Checkbox ID="DrillBox" Enable="true" runat="server" Checked='<%# Eval("Drill") %>' />
                 <br />Drill Size:
                <asp:Textbox ID="DrillSizeBox" runat="server" Text='<%# Eval("DrillSize") %>' /><br />
                  <br />Mat'l Source: <asp:DropDownList ID="MatlSourceList" runat="server" AppendDataBoundItems="true"  >
                    <asp:ListItem Text="None Selected" Value="0" Selected="True" />
                      <asp:ListItem Text="Stock" Value="1" />
                      <asp:ListItem Text="Customer" Value="2" />
                      <asp:ListItem Text="Purchased" Value="3" />
                    </asp:DropDownList>
                 <asp:Button ID="UpdateButton" runat="server" CommandName="Update"
    Text="Update" />
<asp:Button ID="CancelButton" runat="server" CommandName="Cancel"
    Text="Cancel" />
                 </td>
        </EditItemTemplate>
        <LayoutTemplate>
            <table runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                <tr runat="server" id="itemPlaceholderContainer">
                    <td runat="server" id="itemPlaceholder">
                    </td>
                </tr>
            </table>
            <div style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
            </div>
        </LayoutTemplate>        
               
    </asp:ListView>
                            <br /><b><u>Mat'l Quote Request:</u></b><br />
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="MaterialName" HeaderText="Mat'l" SortExpression="MaterialName" />
            <asp:BoundField DataField="Dimension" HeaderText="Dim" SortExpression="Dimension" />
            <asp:BoundField DataField="Diameter" HeaderText="D" SortExpression="Diameter" />
            <asp:BoundField DataField="Height" HeaderText="H" SortExpression="Height" />
            <asp:BoundField DataField="Width" HeaderText="W" SortExpression="Width" />
            <asp:BoundField DataField="Length" HeaderText="L" SortExpression="Length" />
            <asp:BoundField DataField="Quantity" HeaderText="Q" SortExpression="Quantity" />
            <asp:CheckBoxField DataField="Cut" HeaderText="Cut" SortExpression="Cut" />
            <asp:CheckBoxField DataField="OrderPending" HeaderText="Order" SortExpression="OrderPending" />
        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                 <br /><b><u>Mat'l Order:</u></b><br />
                    <asp:GridView ID="GridView5" runat="server" OnRowCommand="GridView5_RowCommand" AutoGenerateColumns="False" DataKeyNames="MatlPriceID, MaterialPOID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="MaterialName" HeaderText="Mat'l" SortExpression="MaterialName" />
            <asp:BoundField DataField="Dimension" HeaderText="Dim" SortExpression="Dimension" />
            <asp:BoundField DataField="D" HeaderText="D" SortExpression="D" />
            <asp:BoundField DataField="H" HeaderText="H" SortExpression="H" />
            <asp:BoundField DataField="W" HeaderText="W" SortExpression="W" />
            <asp:BoundField DataField="L" HeaderText="L" SortExpression="L" />
            <asp:BoundField DataField="Qty" HeaderText="Q" SortExpression="Qty" />
            <asp:CheckBoxField DataField="Cut" HeaderText="Cut" SortExpression="Cut" />
            <asp:CheckBoxField DataField="received" HeaderText="Rec'd" SortExpression="received" />
            <asp:CheckBoxField DataField="Prepared" HeaderText="Prep'd" SortExpression="Prepared" />
            <asp:BoundField DataField="Location" HeaderText="Loc" SortExpression="Location" />
            <asp:BoundField DataField="MaterialSource" HeaderText="Source" SortExpression="MaterialSource" />
            <asp:ButtonField Text="View Order" CommandName="ViewOrder" />
        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                        </td><td style="width:33%; vertical-align:top">
                <b><u>Deliveries:</u></b><br />
                       <asp:gridview ID="DeliveryViewGrid" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" >
                           <AlternatingRowStyle BackColor="#DCDCDC" />    				
                           <Columns>
				<asp:BoundField DataField="CurrDelivery"  
                                       HeaderText="Delivry" SortExpression="CurrDelivery" DataFormatString="{0:MM-dd-yyyy}">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Qty" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="PONumber"  
                                       HeaderText="PO #" SortExpression="PONumber">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
 <asp:BoundField DataField="Ready"  
                                       HeaderText="RTS" SortExpression="Ready">
                                       <ItemStyle HorizontalAlign="Center" />
<ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
				   <asp:BoundField DataField="Shipped"  
                                       HeaderText="Shipped" SortExpression="Shipped">
<ControlStyle Font-Size="Small" />
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>    
 <asp:BoundField DataField="Suspended"  
                                       HeaderText="Suspended" SortExpression="Suspended">
<ControlStyle Font-Size="Small" />
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                 
							</Columns>
                           <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:gridview>
                <br /><b><u>Operations:</u></b><br />
                <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDelete" OnRowCancelingEdit="GridView2_RowCancel" OnRowUpdating="GridView2_RowUpdate" OnRowEditing="GridView2_RowEditing" AutoGenerateDeleteButton="true" AutoGenerateEditButton="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, JobItemID, ProcessOrder" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
           <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:label id="ProcOrder" runat="server" Text='<%# Bind("[ProcessOrder]") %>' HeaderText="#" ReadOnly="True" SortExpression="ProcessOrder" />
                </ItemTemplate>
              </asp:TemplateField>
            
           <asp:ButtonField CommandName="Up" ButtonType="Image" ImageUrl="/images/up_arrow.png" />
            <asp:ButtonField CommandName="Down" ButtonType="Image" ImageUrl="/images/dwn_arrow.png"  />
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:label id="ProcessDesc" runat="server" Text='<%# Bind("[Label]") %>' HeaderText="Description" ReadOnly="True" SortExpression="Label" />
                </ItemTemplate>
                 <EditItemTemplate>
                     <asp:HiddenField ID="hdProcDescList" runat="server" Value ='<%# Eval("[ID]") %>' />
                     <asp:HiddenField ID="hdProcIDList" runat="server" Value ='<%# Eval("[OperationID]") %>' />
                    <asp:DropDownList id="ProcDescList" runat="server" HeaderText="Description" DataTextField="OperationName" DataValueField="OperationID" />                     
                     <asp:HiddenField ID="hdWCIDList" runat="server" Value ='<%# Eval("[WorkcodeID]") %>' />
                    <asp:DropDownList id="WCDescList" runat="server" HeaderText="Description" DataTextField="Workcode" DataValueField="WorkcodeID" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Setup">
                <ItemTemplate>
                    <asp:label id="SetupCost" runat="server" Text='<%# Bind("[Setup_Cost]") %>' HeaderText="Setup" ReadOnly="True" SortExpression="Setup_Cost" />
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:Textbox id="SetupBox" runat="server" Text='<%# Eval("[Setup_Cost]") %>'  HeaderText="Setup" Width="32px" />
                </EditItemTemplate>
              </asp:TemplateField>
            <asp:TemplateField HeaderText="Op.(mins)">
                <ItemTemplate>
                    <asp:label id="OperationCost" runat="server" Text='<%# Bind("[Operation_Cost]") %>' HeaderText="Op.(mins)" ReadOnly="True" SortExpression="Operation_Cost" />
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:Textbox id="OperationBox" runat="server" Text='<%# Eval("[Operation_Cost]") %>' HeaderText="Op.(mins)" Width="32px" />
                </EditItemTemplate>
              </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Comments">
                <ItemTemplate>
                    <asp:label id="CommentGV2" runat="server" Text='<%# Bind("[Comments]") %>' HeaderText="Comments" ReadOnly="True" SortExpression="Comments" />
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:Textbox id="CommentBoxGV2" runat="server" Text='<%# Eval("[Comments]") %>' HeaderText="Comments" Width="200px" />
                </EditItemTemplate>
              </asp:TemplateField> 
            <asp:TemplateField HeaderText="Done">
                <ItemTemplate>
                    <asp:checkbox id="CompletedLbl" runat="server" Checked='<%# Bind("[Completed]") %>' HeaderText="Completed" ReadOnly="True" Enabled="false" SortExpression="Completed" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="Completed" Checked='<%# Eval("[Completed]") %>' runat="server" Enabled="true"/>
                </EditItemTemplate>
              </asp:TemplateField>            
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:MultiView runat="server" ID="logmultiviewGV2" ActiveViewIndex="0">
                        <asp:View ID="AlreadyLogViewGV2" runat="server">                        
                             <asp:GridView ID="LogHoursGridGV2" runat="server" OnRowDeleting="LogHoursGridGV2_RowDelete" OnRowCancelingEdit="LogHoursGridGV2_RowCancel" OnRowUpdating="LogHoursGridGV2_RowUpdate" OnRowEditing="LogHoursGridGV2_RowEditing" OnRowDataBound="LogHoursGridGV2_RowDataBound" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, ProcessID" EnableModelValidation="True" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                 <Columns>
                                     <asp:TemplateField HeaderText="Empl.">
                                        <ItemTemplate>
                                            <asp:label id="EmplLblGV2" runat="server" Text='<%# Bind("[Name]") %>' HeaderText="Empl." ReadOnly="True" SortExpression="Name" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hdEmplGV2" runat="server" Value ='<%# Eval("[EmployeeID]") %>' />                    
                                            <asp:DropDownList id="EmplGV2" runat="server" HeaderText="Empl." DataValueField="EmployeeID" DataTextField="Name" />
                                        </EditItemTemplate>
                                      </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="In">
                                                         <ItemTemplate>
                                            <asp:label id="QtyInLblGV2" runat="server" Text='<%# Bind("[QuantityIn]") %>' HeaderText="In" ReadOnly="True" SortExpression="QtyIn" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Textbox id="QtyInGV2" runat="server" Text='<%# Eval("[QuantityIn]") %>' HeaderText="Qty In" Width="32px" />
                                        </EditItemTemplate>
                                      </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Out">
                                                         <ItemTemplate>
                                            <asp:label id="QtyOutLblGV2" runat="server" Text='<%# Bind("[QuantityOut]") %>' HeaderText="Qty Out" ReadOnly="True" SortExpression="QtyOut" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Textbox id="QtyOutGV2" Text='<%# Eval("[QuantityOut]") %>' runat="server" HeaderText="Qty Out" Width="32px" />
                                        </EditItemTemplate>
                                      </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Hours">
                                                         <ItemTemplate>
                                            <asp:label id="HoursLblGV2" runat="server" Text='<%# Bind("[Hours]") %>' HeaderText="Hours" ReadOnly="True" SortExpression="Hours" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Textbox id="HoursGV2" Text='<%# Eval("[Hours]") %>' runat="server" HeaderText="Hours" Width="32px" />
                                        </EditItemTemplate>
                                      </asp:TemplateField> 
                                      <asp:TemplateField>
                                         <EditItemTemplate>
                                             <asp:CheckBox runat="server" ID="MoveOnGV2" Checked="true" />
                                         </EditItemTemplate>
                                         
                                     </asp:TemplateField>
                                     
                                 </Columns>
                                 <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#000084" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                            </asp:GridView>
                            <asp:Button ID="LogNewGV2" Text="Log New" runat="server" CommandArgument="NewLogViewGV2" CommandName="SwitchViewByID" OnCommand="LogNewGV2_Command" />
                        </asp:View>
                        <asp:View ID="NewLogViewGV2" runat="server">
                             <br />
                                <table>
                                    <tr>
                                        <td>Employee</td>
                                        <td>Qty In</td>
                                        <td>Qty Out</td>
                                        <td>Hours</td>
                                        <td>Move On</td>
                                    </tr>
                                    <tr>
                                        <td><asp:DropDownList runat="server" ID="EmployeeListGV2" DataValueField="EmployeeID" DataTextField="Name"></asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="QtyInAddGV2" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="QtyOutAddGV2" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="HoursAddGV2" Width="75"></asp:TextBox></td>                                    
                                        <td><asp:CheckBox runat="server" ID="MoveOnGV2" Checked="true" /></td></tr>
                                    <tr>
                                        <td><asp:Button ID="LogNewNowGV2" text="Execute" runat="server" CommandArgument="AlreadyLogViewGV2" CommandName="SwitchViewByID" OnCommand="LogNewNowGV2_Command" /></td>
                                    <td><asp:Button runat="server" ID="CancelLogGV2" Text="Cancel" CommandArgument="AlreadyLogViewGV2" CommandName="SwitchViewByID" OnCommand="CancelLogGV2_Command" /></td><td colspan="2"> </td></tr>
                                </table>                               
                            <br />
                           
                        </asp:View>
                    </asp:MultiView>
                </ItemTemplate>
            </asp:TemplateField>
                </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                             <asp:MultiView runat="server" ID="opsmultiviewGV2" ActiveViewIndex="0"><asp:View runat="server" ID="ExistViewGV2">
                                 <br />
                                <asp:Button ID="InputOpButtonGV2" Text="Add Operation" runat="server" CommandArgument="addviewGV2" CommandName="SwitchViewByID" OnCommand="AddOpGV2_Command" /> <asp:Button ID="InputSubButtonGV2" Text="Add Subcontract" runat="server" CommandArgument="addSubGV2" CommandName="SwitchViewByID" OnCommand="AddSubGV2_Command" /><br />
                </asp:View>
                    <asp:View runat="server" ID="addviewGV2">
                        <br />
                                <table >
                                    <tr>
                                         <td>Process #</td>
                                        <td>Operation</td>
                                        <td>Setup (hrs)</td>
                                        <td>Run (mins ea.)</td>
                                        <td>Description</td>
                                        <td>Employee Requesting</td>

                                    </tr>
                                    <tr>
                                        <td><asp:TextBox runat="server" ID="RequestedOrderBox" Width="75" Text="1"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="DropDownList2GV2" DataValueField="OperationID" DataTextField="OperationName"></asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="TextBox3GV2" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="TextBox4GV2" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="OpCommentBoxGV2" Width="250"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="EmployeeAddListGV2" Width="150" DataTextField="Name" DataValueField="EmployeeID"></asp:DropDownList></td>
                                    </tr>
                                    <tr><td><asp:Button runat="server" ID="CancelAddOpGV2" Text="Cancel" CommandArgument="ExistViewGV2" CommandName="SwitchViewByID" OnCommand="CancelAddOpGV2_Command" /></td><td><asp:Button runat="server" ID="AddNowGV2" Text="Add" CommandArgument="ExistViewGV2" CommandName="SwitchViewByID" OnCommand="AddNowOpGV2_Command" /></td></tr>
                                </table>                               
                        <br />
                           
                    </asp:View>
                                 <asp:View runat="server" ID="AddSubGV2">
                        <br />
                                <table >
                                    <tr>
                                         <td>Process #</td>
                                        <td>Description</td>
                                        
                                        <td>Comments</td>
                                        <td>Employee Requesting</td>

                                    </tr>
                                    <tr>
                                        <td><asp:TextBox runat="server" ID="ReqOrderBoxSub" Width="75" Text="1"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="DropDownList3GV2" DataValueField="WorkcodeID" DataTextField="Workcode"></asp:DropDownList></td>
                                        
                                        <td><asp:TextBox runat="server" ID="SubCommentBoxGv2" Width="250"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="EmployeeAddList2GV2" Width="150" DataTextField="Name" DataValueField="EmployeeID"></asp:DropDownList></td>
                                    </tr>
                                    <tr><td><asp:Button runat="server" ID="CancelAddSubGV2" Text="Cancel" CommandArgument="ExistViewGV2" CommandName="SwitchViewByID" OnCommand="CancelAddSubGV2_Command" /></td><td><asp:Button runat="server" ID="AddSubNowGV2" Text="Add" CommandArgument="ExistViewGV2" CommandName="SwitchViewByID" OnCommand="AddNowSubGV2_Command" /></td></tr>
                                </table>                               
                        <br />
                           
                    </asp:View>
                </asp:MultiView>
                 <br /><b><u>Subcontract History:</u></b><br />
                <asp:GridView ID="GridView3" runat="server" OnRowCommand="GridView3_RowCommand" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="SubcontractID" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="SubcontractID" HeaderText="PO #" SortExpression="SubcontractID" />
            <asp:BoundField DataField="WorkCode" HeaderText="WorkCode" SortExpression="WorkCode" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
            <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate"  DataFormatString="{0:MM-dd-yyyy}" />
            <asp:CheckBoxField DataField="Received" HeaderText="Rec'd" SortExpression="Received" />
            <asp:ButtonField Text="View Order" CommandName="ViewOrder" />
                
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                            </td><td style="width:33%; vertical-align:top">
                 
                <b><u>Fixture Orders:</u></b><br />
                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
                <br /><b><u>Fixture Inventory:</u></b><br />
                <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Loc" HeaderText="Loc" SortExpression="Loc" />
            <asp:BoundField DataField="Material" HeaderText="Material" SortExpression="Material" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                 <br /><b><u>Machined Assembly Components:</u></b><br />
                 <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
            <asp:BoundField DataField="Revision_Number" HeaderText="Revision_Number" SortExpression="Revision_Number" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="PerAssembly" HeaderText="PerAssembly" SortExpression="PerAssembly" />
            <asp:BoundField DataField="NextOp" HeaderText="NextOp" SortExpression="NextOp" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                <br /><b><u>Purchased Assembly Components:</u></b><br />
                    <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="PerAssy" HeaderText="PerAssy" SortExpression="PerAssy" />
            <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" SortExpression="ItemNumber" />
            <asp:BoundField DataField="VendorName" HeaderText="VendorName" SortExpression="VendorName" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                      </td></tr></table>  </div>
					</td>
					</tr>
                               </itemtemplate></asp:TemplateField>
   				                                             
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
    <asp:SqlDataSource ID="ClearMatlOrders" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [MaterialPOID], [Name], [VendorName], [DueDate], [Notes], [Contact], [ConfirmationNum], [ShippingCharge] FROM [ClearMaterialOrders]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="MatPOLineItems" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>
     <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM ProductionLotsClear ORDER BY JobItemID" ></asp:SqlDataSource>
 
     <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
       
    </asp:SqlDataSource>
   
    
    

   
    <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
       
    </asp:SqlDataSource>
   
    
    


    <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
   
    
    

    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
       
    </asp:SqlDataSource>
   
    <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
       
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
        
    </asp:SqlDataSource>
   
    
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="MonseesSqlDataSourceDeliveries" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
    </asp:SqlDataSource>

   
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
           
        </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT NextOp From ProductionViewAndNotCleared">
    </asp:SqlDataSource>  

 <asp:SqlDataSource ID="MaterialSourceList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	* From MaterialSource">
    </asp:SqlDataSource>  

 <asp:SqlDataSource ID="WorkcodeList" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	* From WorkCode">
    </asp:SqlDataSource>  
      <asp:SqlDataSource ID="StockInventorySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
       
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="LogHoursGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
   

     <asp:SqlDataSource ID="EmployeeList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Name, EmployeeID FROM Employees WHERE Active=1"></asp:SqlDataSource>


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