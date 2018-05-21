<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inspection.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Monsees.Inspection" MasterPageFile="~/MasterPages/Monsees.master" %>
<%@ Import Namespace="Monsees.Security" %>

<asp:Content ContentPlaceHolderID="headContent"  runat="server">
    <title>Inspection Manager</title>
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
                    <asp:Button ID="ProdViewButton" runat="server" Text="Production View" 
                        onclick="ProdViewButton_Click" />
		</td>
   <td align="center" valign="middle">
                    <asp:Button ID="InventoryViewButton" runat="server" Text="Inventory View" 
                        onclick="InventoryViewButton_Click" />
		</td>
   <td align="center" valign="middle">
                    <asp:Button ID="DeliveriesViewButton" runat="server" Text="Deliveries View" 
                        onclick="DeliveriesViewButton_Click" />
		</td>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td>
               
                    </td>
            </tr>
        </table>
         
              

	<table itemstyle-cssclass="GridviewTable">


		<tr>
	<td colspan="15">
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   <asp:Multiview ID="InspectionMultiview" runat="server" ActiveViewIndex="0">
			<asp:View ID="Production" runat="server">
                    <table itemstyle-cssclass="GridviewTable">
                        
				<table itemstyle-cssclass="GridviewTable" >
<tr><td colspan="15">Current Production List:</td></tr>
		<tr><td > Lot #: <asp:Textbox ID="LotFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td >
                               Company: <asp:TextBox ID="CompanyFilter" runat="server" >
        				       
                               </asp:TextBox>
                               </td>
               <td > PartNumber: <asp:TextBox ID="PartFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td > Description: <asp:Textbox ID="DescFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td > PM: <asp:Textbox ID="PMFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td style="font-size:x-small"><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />     </td>
             <td>
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="x-Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
		</tr></table>
		<asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" EnableSortingAndPagingCallbacks="false"  
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="JobItemID" DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" Width="100%" OnRowDataBound="ProductionViewGrid_RowDataBound"
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					                    <ItemTemplate>
					                        <asp:Button runat="server" ID="ExpColMain" OnClick="ExpandCollapse" Text="+"  />
					                    </ItemTemplate>
				                   </asp:TemplateField>
				                   <asp:BoundField DataField="Job_Number" HeaderText="Job #" 
                                       SortExpression="Job_Number" ItemStyle-Width="6%">
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
                                   <asp:BoundField DataField="PartNumber" HeaderText="Part Number" 
                                       SortExpression="PartNumber" ItemStyle-Width="10%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				  
								    
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="15%">
									<ItemStyle HorizontalAlign="Left" />
							   </asp:BoundField>
				                   <asp:BoundField DataField="MinOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" 
                                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="10%">
                                                       <ItemStyle HorizontalAlign="Center" />
                                                   </asp:BoundField>                                   
                                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                                       <ItemStyle HorizontalAlign="Center" />
                                                   </asp:BoundField>
                                   
				                          <asp:BoundField DataField="Abbr" HeaderText="PM" SortExpression="Abbr" ItemStyle-Width="3%">
				                   <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
			                           <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="9%">
				                   <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
                                                   <asp:BoundField DataField="Comments" HeaderText="Process Notes" SortExpression="Comments" ItemStyle-Width="14%">
				                   <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
			                           <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" SortExpression="LateStartInt" ItemStyle-Width="6%" DataFormatString="{0:MM-dd-yyyy}">
				                   <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
                                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />

								   					                <asp:TemplateField>
						                <ItemTemplate>
							                <a href="/Lot.aspx?id=<%#Eval("JobItemID")%>">Lot</a>
						                </ItemTemplate>
					                </asp:TemplateField>
                                                   <asp:ButtonField CommandName="InitCAR" Text="CAR Request" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">                                 
                                                   </asp:ButtonField>           
                                   				   
				                   <asp:ButtonField CommandName="ViewReport" Text="Report Input" HeaderText="" ItemStyle-Width="5%">
                                                   <ControlStyle Font-Size="Small" />
                                                   </asp:ButtonField> 
                                                   <asp:ButtonField CommandName="PrintReport" Text="Print Report" HeaderText="" ItemStyle-Width="5%">
                                                   <ControlStyle Font-Size="Small" />
                                                   </asp:ButtonField>
                                    <asp:ButtonField CommandName="InitiateCA" Text="Initiate CA" HeaderText="" ItemStyle-Width="5%">
                                                   <ControlStyle Font-Size="Small" />
                                                   </asp:ButtonField>
   				                   <asp:TemplateField ItemStyle-Width="6%">
					                <ItemTemplate>
					                <asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					                </ItemTemplate>
					                </asp:TemplateField>			
                                    <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                          <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                          <asp:HiddenField runat="server" ID="NewPart" Value='<%#Eval("NewPart") %>' />
                                          <asp:HiddenField runat="server" ID="CAbbr" Value='<%#Eval("CAbbr") %>' />
                                      </ItemTemplate>
                                  </asp:TemplateField>
   				                <asp:TemplateField>
                                        <ItemTemplate></td></tr>
                                            <tr><td colspan="22" style="vertical-align:top">
                                            <div runat="server" id="div1" visible="false" >  <table><tr><td style="vertical-align:top">
                                                <b><u>Serial Numbers:</u></b><br />
                                            <asp:GridView ID="SerialNumbersMain" runat="server" AllowPaging="False" 
                                            AutoGenerateColumns="False" BackColor="White" EnableSortingAndPagingCallbacks="false"  
                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                            DataKeyNames="SerialNumID" GridLines="Vertical" 
                                            onrowcommand="SerialNumbersMain_RowCommand" Width="100%"
                                            AllowSorting="True">
                                                <Columns>
                                                    <asp:BoundField DataField="LotIncrement" HeaderText="Lot Inc." 
                                                        SortExpression="LotIncrement" />
                                                    <asp:BoundField DataField="InternalSerialNumber" HeaderText="Internal Serial" 
                                                        SortExpression="InternalSerialNumber" />
                                                    <asp:BoundField DataField="CustSerialNumber" HeaderText="Customer Serial" 
                                                        SortExpression="CustSerialNumber" />     
                                                    <asp:ButtonField Text="Report Input" CommandName="Input" />
                                                        <asp:ButtonField Text="View/Print" CommandName="View" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                                    ForeColor="White" />
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                            </asp:GridView>
                                              <br />  <b><u>Deliveries:</u></b><br />
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
                                                <br />
                                                <u><b>Certifications:</b></u>
                                                <asp:GridView ID="CertGrid" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                    <Columns>
                                                        <asp:CheckBoxField DataField="CertCompReqd" HeaderText="C of C" SortExpression="CertCompReqd" />
                                                        <asp:CheckBoxField DataField="MatlCertReqd" HeaderText="Plating" SortExpression="MatlCertReqd" />
                                                        <asp:CheckBoxField DataField="PlateCertReqd" HeaderText="Material" SortExpression="PlatingCertReqd" />
                                                        <asp:CheckBoxField DataField="SerializationReqd" HeaderText="Serialized" SortExpression="SerializationReqd" />
           
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView></td><td style="vertical-align:top">   
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
                                                <br /><b><u>Operations:</u></b><br />
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateEditButton="false" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, JobItemID, SetupID" EnableModelValidation="True" GridLines="Vertical">
                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <Columns>                                                    
                                                   <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:label id="ProcOrder" runat="server" Text='<%# Bind("[ProcessOrder]") %>' HeaderText="#" ReadOnly="True" SortExpression="ProcessOrder" />
                                                        </ItemTemplate>
                                                      </asp:TemplateField>
            
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:label id="ProcessDesc" runat="server" Text='<%# Bind("[Label]") %>' HeaderText="Description" ReadOnly="True" SortExpression="Label" />
                                                        </ItemTemplate>
                
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Setup">
                                                        <ItemTemplate>
                                                            <asp:label id="SetupCost" runat="server" Text='<%# Bind("[Setup_Cost]") %>' HeaderText="Setup" ReadOnly="True" SortExpression="Setup_Cost" />
                                                        </ItemTemplate>
                                                      </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Run(min)">
                                                        <ItemTemplate>
                                                            <asp:label id="OperationCost" runat="server" Text='<%# Bind("[Operation_Cost]") %>' HeaderText="Op.(mins)" ReadOnly="True" SortExpression="Operation_Cost" />
                                                        </ItemTemplate>
                                                      </asp:TemplateField> 
                                                    <asp:BoundField DataField="QtyIn" HeaderText="QTY In" SortExpression="QtyIn" />
                                                     <asp:BoundField DataField="QtyOut" HeaderText="QTY Out" SortExpression="QtyOut" />
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <asp:label id="Comment" runat="server" Text='<%# Bind("[Comments]") %>' HeaderText="Comments" ReadOnly="True" SortExpression="Comments" />
                                                        </ItemTemplate>                             
                                                         <EditItemTemplate>
                                                            <asp:Textbox id="CommentBox" runat="server" Text='<%# Eval("[Comments]") %>' HeaderText="Comments" Width="200px" />
                                                        </EditItemTemplate>
                                                      </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Done">
                                                        <ItemTemplate>
                                                            <asp:checkbox id="CompletedLbl" runat="server" Checked='<%# Bind("[Completed]") %>' HeaderText="Completed" ReadOnly="True" Enabled="false" SortExpression="Completed" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="Completed" Checked='<%# Eval("[Completed]") %>' runat="server" Enabled="true" />
                                                        </EditItemTemplate>
                                                      </asp:TemplateField>  
                                                    </Columns> 
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                <br /><b><u>Subcontract History:</u></b><br />
                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SubcontractID" HeaderText="PO #" SortExpression="SubcontractID" />
                                                        <asp:BoundField DataField="WorkCode" HeaderText="WorkCode" SortExpression="WorkCode" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
                                                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate"  DataFormatString="{0:MM-dd-yyyy}" />
                                                        <asp:CheckBoxField DataField="Received" HeaderText="Rec'd" SortExpression="Received" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>   </td></tr></table>   
                                            </div>
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
        </table>
                      </asp:view>

		
			<asp:View ID="InventoryB" runat="server">
                 Unconfirmed Inventory: <br />
                    		<asp:GridView ID="InventoryViewGridUC" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceInvUC" DataKeyNames="DeliveryItemID" GridLines="Vertical" 
                               onrowcommand="InventoryViewGridUC_RowCommand" Width="100%" 
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                   
				   <asp:BoundField DataField="LotNumber" HeaderText="Lot #" SortExpression="LotNumber" />
                                   <asp:BoundField DataField="CAbbr" HeaderText="Cust. Code" SortExpression="CAbbr" />
                                   <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
                <asp:BoundField DataField="Revision_Number" HeaderText="Rev." SortExpression="Revision_Number" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Assigned Qty" SortExpression="Quantity" />
                
                <asp:BoundField DataField="TotInvQty" HeaderText="Inventory Qty" SortExpression="TotInvQty" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="Location1" HeaderText="Loc." SortExpression="Location1" />
                
                
               
                                                                  
                                   										   					<asp:TemplateField>
						<ItemTemplate>
							<a href="/Lot.aspx?id=<%#Eval("LotNumber")%>">Lot</a>
						</ItemTemplate>
					</asp:TemplateField>		   
				   <asp:ButtonField CommandName="ViewReport" Text="Report" HeaderText="" ItemStyle-Width="5%">
                                   <ControlStyle Font-Size="Small" />
                   </asp:ButtonField> 
   				   


					<asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>

				   <asp:ButtonField CommandName="Confirm" Text="Confirm" HeaderText="" ItemStyle-Width="5%">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
				   <asp:ButtonField CommandName="Release" Text="Release" HeaderText="" ItemStyle-Width="5%">
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
<br><br>Ready-to-Ship Inventory: <br />
                           <asp:GridView ID="InventoryViewGridW" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceInvW" GridLines="Vertical" 
                               onrowcommand="InventoryViewGridW_RowCommand" Width="100%" 
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>

				   <asp:BoundField DataField="InventoryID" HeaderText="Inventory #" 
                                       SortExpression="InventoryID" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="JobNumber" HeaderText="Job #" 
                                       SortExpression="JobNumber" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CAbbr"  
                                       HeaderText="Cust. Code" SortExpression="CAbbr" ItemStyle-Width="9%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="PartNumber" HeaderText="Part Number" 
                                       SortExpression="PartNumber" ItemStyle-Width="10%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="15%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="NextDelivery" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="6%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                  
                                   												   					<asp:TemplateField>
						<ItemTemplate>
							<a href="/Lot.aspx?id=<%#Eval("JobItemID")%>">Lot</a>
						</ItemTemplate>
					</asp:TemplateField>   
				   <asp:ButtonField CommandName="ViewReport" Text="Report" HeaderText="" ItemStyle-Width="5%">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
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
                      </asp:view>
			<asp:View ID="Deliveries" runat="server">
                    Delivery Schedule: <br />
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
				   <asp:ButtonField CommandName="ViewReport" Text="View/Edit Report" HeaderText="">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>      
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
                      </asp:view>
		</asp:Multiview>
                       
                 </p>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    <p>
    <asp:Label ID="UpdateResults" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>

    </div>

  <asp:SqlDataSource ID="SqlDataSourceCert" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
       
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
    
        <asp:SqlDataSource ID="MonseesSqlDataSourceDeliveries" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
        
    </asp:SqlDataSource>

 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        EnableCaching="False" >



    </asp:SqlDataSource>

 <asp:SqlDataSource ID="MonseesSqlDataSourceInvW" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From InventoryWaiting" EnableCaching="False" >



    </asp:SqlDataSource>

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



<asp:SqlDataSource ID="MonseesSqlDataSourceInvUC" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From InventoryUnconfirmed" EnableCaching="False" >



    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SerialMainSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>


    <asp:SqlDataSource ID="MonseesSqlDataSourceLoggedin" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="populatejoblist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Job_Number From ProductionView">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="populatepartslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PartNumber From ProductionView">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT NextOp From ProductionView">
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