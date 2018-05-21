<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="MonitorOps.aspx.cs" Inherits="Monsees.MonitorOps" MasterPageFile="~/MasterPages/Monsees.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="headContent" >
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

<script type="text/javascript">

function collapseExpand(obj) 
{

  var div = document.getElementById('JobItemIDTag');

  var theFlag = div.style.display == "inline";
  div.style.display = (theFlag) ? "none" : "inline";
 

}

</script>

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
                    <asp:Button ID="ProdViewButton" runat="server" Text="Production View" 
                        onclick="ProdViewButton_Click" />
		</td>
   <td align="center" valign="middle">
                    <asp:Button ID="SuspViewButton" runat="server" Text="Suspended View" 
                        onclick="SuspViewButton_Click" />
		</td>
   <td align="center" valign="middle">
                    <asp:Button ID="NCViewButton" runat="server" Text="Not Cleared View" 
                        onclick="NCViewButton_Click" />
		</td>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td>
               
                    </td>
            </tr>
        </table>
         
          

	
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   <asp:MultiView ID="ProductionMultiView" runat="server" ActiveViewIndex="0">
                       <asp:View ID="Active" runat="server">
                            <div style="align-content:flex-start"><table><tr><td> Lot #: <asp:Textbox ID="LotFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td>
                               Company: <asp:TextBox ID="CompanyFilter" runat="server" >
        				       
                               </asp:TextBox>
                               </td>
               <td> PartNumber: <asp:TextBox ID="PartFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td> Description: <asp:Textbox ID="DescFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td> PM: <asp:Textbox ID="PMFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />     </td></tr></table></div>    
                    <table><tr><td align="Right">
    				<asp:Button ID="RefreshOpsButton" runat="server" Text="Update Ops." onclick="RefreshOpsButton_Click"
                        />
			</td></tr></table>
                           <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="JobItemID" DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" onrowdatabound="ProductionViewGrid_RowDataBound" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
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
				  
				   <asp:BoundField DataField="JobSetupID" HeaderText="ID" ItemStyle-CssClass="hiddencol" ItemStyle-Width="3%">
				   <ItemStyle HorizontalAlign="Center" />
				   <HeaderStyle CssClass="hiddencol"/>
				   </asp:BoundField>                                
			           <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="9%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                    <asp:BoundField DataField="Comments" HeaderText="Op. Comments" SortExpression="Comments" ItemStyle-Width="13%">
                                         <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" SortExpression="LateStartInt" ItemStyle-Width="6%" DataFormatString="{0:MM-dd-yyyy}" >
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
                                   				   <asp:TemplateField ItemStyle-Width="3%" HeaderText="Update">
					<ItemTemplate >

					<asp:CheckBox ID="Update" Checked="false" runat="server"/>
					</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
				   </asp:TemplateField>
                                    <asp:ButtonField CommandName="InitCAR" Text="CAR Request" ItemStyle-Width="4%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                      
                                   </asp:ButtonField>
                                    <asp:ButtonField HeaderText="" Text="Add Fixture" CommandName="AddFixt" />
            <asp:ButtonField HeaderText="" Text="Allocate Fixture" CommandName="AllocFixt" />
   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>	
                                 <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%# Convert.ToString(Eval("NewPart")) %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Convert.ToString(Eval("Cabbr")) %>' />
                              </ItemTemplate>
                          </asp:TemplateField>
                                   <asp:ButtonField HeaderText="" Text="Hot or Not" CommandName="Hot" />   		
				<asp:TemplateField ItemStyle-Width="1%"><itemtemplate>
                <tr>
					<td colspan="22">
                        
					<div runat="server" id="div1" visible="false" >  
                        <table><tr>
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
                               </tr><tr><td style="width:50%; vertical-align:top"> <table><tr><td>    
                                   <br /><b><u>Part Summary:</u></b><br />    
                <asp:ListView ID="ListView2" runat="server" DataKeyNames="Expr1" EnableModelValidation="True" >

        
        <EmptyDataTemplate>
            <table style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        
        <ItemTemplate>
            <td runat="server" style="background-color:#DCDCDC;color: #000000;vertical-align:top" >Heat_Treat:
                <asp:Label ID="Heat_TreatLabel" runat="server" Text='<%# Eval("Heat_Treat") %>' />
                <br />Plating:
                <asp:Label ID="PlatingLabel" runat="server" Text='<%# Eval("Plating") %>' />
                 <br />Subcontract:
                <asp:Label ID="SubcontractLabel" runat="server" Text='<%# Eval("Subcontract") %>' />
                 <br />Subcontract2:
                <asp:Label ID="Subcontract2Label" runat="server" Text='<%# Eval("Subcontract2") %>' />
                <br />Hours:
                <asp:Label ID="EstimatedTotalHoursLabel" runat="server" Text='<%# Eval("EstimatedTotalHours") %>' />                                         

                
            </td><td runat="server" style="vertical-align:top" >Material: 
                <asp:Label ID="MaterialLabel" runat="server" Text='<%# Eval("Material") %>' />
                <br />Dimension:
                <asp:Label ID="MaterialDimLabel" runat="server" Text='<%# Eval("Dimension") %>' />
                <br />Size:
                <asp:Label ID="MaterialSizeLabel" runat="server" Text='<%# Eval("MaterialSize") %>' />
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

                 </td>
        </ItemTemplate>
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
               
    </asp:ListView></TD><td>

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

                             <br /><br />
        <u><b>Certifications:</b></u>
        <asp:GridView ID="CertGrid" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CheckBoxField DataField="CertCompReqd" HeaderText="C of C" SortExpression="CertCompReqd" />
            <asp:CheckBoxField DataField="PlateCertReqd" HeaderText="Plating" SortExpression="PlateCertReqd" />
            <asp:CheckBoxField DataField="MatlCertReqd" HeaderText="Material" SortExpression="MatlCertReqd" />
            <asp:CheckBoxField DataField="SerializationReqd" HeaderText="Serialized" SortExpression="SerializationReqd" />
           
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>


         </TD></TR></TABLE>
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
                    <asp:GridView ID="GridView5" runat="server" OnRowCommand="GridView5_RowCommand" AutoGenerateColumns="False" DataKeyNames="MatPriceID, MaterialPOID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="MatPriceID" HeaderText="Mat'l Lot ID" SortExpression="MatPriceID" />
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
                                   </BR>  <b><u>Fixtures:</u></b><br/>
                        <asp:GridView ID="RevisionFixtureOrders" runat="server" OnRowDataBound="RevisionFixtureOrders_RowDataBound" DataKeyNames="Location, FixtureRevID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                <asp:BoundField DataField="OperationName" HeaderText="Operation" SortExpression="OperationName" />
                <asp:TemplateField HeaderText="Location & Notes">
                    <ItemTemplate>
                        <div id="loclabeldiv" runat="server">
                            <asp:Label runat="server" Text='<%#Eval("Location") %>' /> - 
                            <asp:Label runat="server" Text='<%#Eval("Note") %>' />
                            </div>
                         <div id="loctextdiv" runat="server">
                            Location: <asp:TextBox ID="fixloctext"  runat="server" Text='<%#Eval("Location") %>' />
                             <br></br>
                            Note: <asp:TextBox ID="fixnotetext"  runat="server" Text='<%#Eval("Note") %>' />
                             <asp:Button ID="FixtureCloseButton2" runat="server" OnClick="FixtureCloseButton2_Click" Text="Close" />
                            </div>
                              </ItemTemplate>
                    </asp:TemplateField>
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
                        </td><td style="width:50%; vertical-align:top">
                
                <br /><b><u>Operations:</u></b><br />
                <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDelete" OnRowCancelingEdit="GridView2_RowCancel" OnRowUpdating="GridView2_RowUpdate" OnRowEditing="GridView2_RowEditing" AutoGenerateDeleteButton="true" AutoGenerateEditButton="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, JobItemID, ProcessOrder, SetupID" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
             <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					<ItemTemplate>
					<a href="JavaScript:divexpandcollapse('div<%# Eval("JobSetupID") %>');">
                            +</a>  
                        
					</ItemTemplate>

				   </asp:TemplateField>
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
            <asp:ButtonField Text="Order Fixture" CommandName="OrderFixture" />
            <asp:ButtonField Text="Quick Fixture" CommandName="QuickFixture" />
                      
            <asp:TemplateField>
               
                <ItemTemplate> <tr><td colspan="12">
             <div id='div<%# Eval("JobSetupID") %>'  style="display:none; left: 15px;">  
                            <table><tr>
                            <td>
                               <b><u>Pictures:</u></b><br/>
            
		            <asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload><asp:Button runat="server" CommandName="Attach" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Attach" />
                
                        <a href="javascript:void(window.open('<%# "FullImage.ashx?ImID="+ Eval("SetupID")%>','_blank','toolbar=no,menubar=no'))" > <asp:Image ID="Image1" runat="server" ImageUrl='<%# "Thumbnail.ashx?ImID="+ Eval("SetupImageID")  %>'/> </a>
                   
                                     </BR>  <b><u>Fixtures:</u></b><br/>
                        <asp:GridView ID="SetupFixtureOrders" runat="server" OnRowDataBound="SetupFixtureOrders_RowDataBound" DataKeyNames="Location, FixtureRevID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                <asp:TemplateField HeaderText="Location & Notes">
                    <ItemTemplate>
                        <div id="loclabeldiv" runat="server">
                            <asp:Label runat="server" Text='<%#Eval("Location") %>' />
                            <asp:Label runat="server" Text='<%#Eval("Note") %>' />
                            </div>
                         <div id="loctextdiv" runat="server">
                            Location: <asp:TextBox ID="fixloctext"  runat="server" Text='<%#Eval("Location") %>' />
                             Note: <asp:TextBox ID="fixnotetext"  runat="server" Text='<%#Eval("Note") %>' />
                             <asp:Button ID="FixtureCloseButton" runat="server" OnClick="FixtureCloseButton_Click" Text="Close" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
                                
                
                                </td></tr></table></div>  </ItemTemplate>
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
                            </td>
                 
                
                      </tr></table>  </div>
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
                      </asp:View>
                       <asp:View ID="Suspended" runat="server">
                                        <table><tr><td align="Right">
    				<asp:Button ID="ActivateLotsButton" runat="server" Text="Activate Lots" onclick="ActivateLotsButton_Click"
                        />
			</td></tr></table>
                           <asp:GridView ID="SuspendedViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="JobItemID" DataSourceID="MonseesSqlDataSourceSusp" GridLines="Vertical" 
                               onrowcommand="SuspendedViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					<ItemTemplate>
					<asp:Button runat="server" ID="ExpColMain" OnClick="ExpandCollapseSuspend" Text="+"  />
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
                                      				   <asp:TemplateField ItemStyle-Width="6%" HeaderText="Part Number">
					<ItemTemplate>
					<asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>'></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>	
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="15%">
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
                                   <asp:BoundField DataField="Notes" HeaderText="Process Notes" SortExpression="Notes" ItemStyle-Width="17%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
				   <asp:BoundField DataField="JobSetupID" HeaderText="ID" ItemStyle-CssClass="hiddencol" ItemStyle-Width="3%">
				   <ItemStyle HorizontalAlign="Center" />
				   <HeaderStyle CssClass="hiddencol"/>
				   </asp:BoundField>                                
			           <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="9%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           <asp:BoundField DataField="LateStart" HeaderText="Late Start" SortExpression="LateStart" ItemStyle-Width="6%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   				 
<asp:ButtonField CommandName="ViewOps" Text="All Setups" HeaderText="" ItemStyle-Width="5%">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
 <asp:TemplateField ItemStyle-Width="3%" HeaderText="Activate">
					<ItemTemplate >

					<asp:CheckBox ID="Activate" Checked="false" runat="server"/>
					</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
				   </asp:TemplateField>
   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>
                                    <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%# Convert.ToString(Eval("NewPart")) %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Convert.ToString(Eval("Cabbr")) %>' />
                              </ItemTemplate>
                          </asp:TemplateField>			
				<asp:TemplateField>
					<ItemTemplate>
					</td>
</tr>
<tr>
					<td colspan="100%">
					<div runat="server" id="div1" visible="false" >  
						<asp:GridView ID="DeliveryViewGrid" runat="server" AllowPaging="False" 
                               				AutoGenerateColumns="False" BackColor="White" 
                               				BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               				DataKeyNames="JobItemID" GridLines="Vertical"  
                               				AllowSorting="True">
                               				<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               				<Columns>
				<asp:BoundField DataField="CurrDelivery"  
                                       HeaderText="Delivery" SortExpression="CurrDelivery">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Quantity" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="PONumber"  
                                       HeaderText="PO Number" SortExpression="PONumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField> 
				   <asp:BoundField DataField="Shipped"  
                                       HeaderText="Shipped" SortExpression="Shipped">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                 
							</Columns>
						</asp:GridView>
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
<asp:View ID="NotCleared" runat="server">
                     <table><tr><td align="Right">
    				<asp:Button ID="ClearLotsButton" runat="server" Text="Clear Lots" onclick="ClearLotsButton_Click"
                        />
			</td></tr></table>
                           <asp:GridView ID="NotClearViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="JobItemID" DataSourceID="MonseesSqlDataSourceNC" GridLines="Vertical" 
                               onrowdatabound="NotClearViewGrid_RowDataBound" onrowcommand="NotClearViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
                               <Columns>

                                   <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					<ItemTemplate>
					<asp:Button runat="server" ID="ExpColMain" OnClick="ExpandCollapseNC" Text="+"  />
					</ItemTemplate>

				   </asp:TemplateField>
				   <asp:BoundField DataField="Job_Number" ReadOnly="True" HeaderText="Job #" 
                                       SortExpression="Job_Number" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" ReadOnly="True" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CAbbr" ReadOnly="True"  
                                       HeaderText="Cust. Code" SortExpression="CAbbr" ItemStyle-Width="5%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                      				   <asp:TemplateField ItemStyle-Width="6%" HeaderText="Part Number">
					<ItemTemplate>
					<asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>'></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>	
                                   <asp:BoundField DataField="Revision Number" ReadOnly="True" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" ReadOnly="True" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="15%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="MinOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" ReadOnly="True" 
                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="6%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="Quantity" ReadOnly="True" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Notes" ReadOnly="True" HeaderText="Process Notes" SortExpression="Notes" ItemStyle-Width="17%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
				   <asp:TemplateField HeaderText="Mat'l Source"  HeaderStyle-HorizontalAlign="Center" itemStyle-Width="20px" HeaderStyle-Width="20px"> 

                <EditItemTemplate> 
             
<asp:DropDownList ID="Source" runat="server" DataSourceID="MaterialSourceList"  DataTextField="Description" DataValueField="MatSourceId" SelectedValue='<%#Bind("MaterialSource") %>' AppendDataBoundItems="True">
<asp:ListItem Text="" Value=""></asp:ListItem>
</asp:DropDownList> 
           </EditItemTemplate> 
<ItemTemplate><asp:Label ID="SourceLbl" runat="server" Text='<%# Bind("[MatlSourceTxt]") %>'></asp:Label></ItemTemplate>
           </asp:TemplateField>      
                        
			           <asp:TemplateField HeaderText="Heat Treat"  HeaderStyle-HorizontalAlign="Center" itemStyle-Width="20px" HeaderStyle-Width="20px"> 

                <EditItemTemplate> 
             
<asp:DropDownList ID="Heat" runat="server" DataSourceID="WorkcodeList"  DataTextField="WorkCode" DataValueField="WorkCodeID" SelectedValue='<%#Bind("HeatTreatID") %>' AppendDataBoundItems="True">
<asp:ListItem Text="" Value=""></asp:ListItem>
</asp:DropDownList> 
           </EditItemTemplate> 
<ItemTemplate><asp:Label ID="HeatLbl" runat="server" Text='<%# Bind("[Heat Treat]") %>'></asp:Label></ItemTemplate>
           </asp:TemplateField>      
<asp:TemplateField HeaderText="Plating"  HeaderStyle-HorizontalAlign="Center" itemStyle-Width="20px" HeaderStyle-Width="20px"> 

                <EditItemTemplate> 
             
<asp:DropDownList ID="Plate" runat="server" DataSourceID="WorkcodeList"  DataTextField="WorkCode" DataValueField="WorkCodeID" SelectedValue='<%#Bind("PlatingID") %>' AppendDataBoundItems="True">
<asp:ListItem Text="" Value=""></asp:ListItem>
</asp:DropDownList> 
           </EditItemTemplate> 
<ItemTemplate><asp:Label ID="PlateLbl" runat="server" Text='<%# Bind("[Plating]") %>'></asp:Label></ItemTemplate>
           </asp:TemplateField>      
			           <asp:BoundField DataField="LateStart" ReadOnly="True" HeaderText="Late Start" SortExpression="LateStartInt" ItemStyle-Width="6%" DataFormatString="{0:MM-dd-yyyy}">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   			
                                               <asp:ButtonField HeaderText="" Text="Add Fixture" CommandName="AddFixt" />
            <asp:ButtonField HeaderText="" Text="Allocate Fixture" CommandName="AllocFixt" />
 <asp:TemplateField ItemStyle-Width="3%" HeaderText="Clear">
					<ItemTemplate >

					<asp:CheckBox ID="Clear" Checked="false" runat="server"/>
					</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
				   </asp:TemplateField>
   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>
                                    <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%# Convert.ToString(Eval("NewPart")) %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Convert.ToString(Eval("Cabbr")) %>' />
                              </ItemTemplate>
                          </asp:TemplateField>			
				<asp:TemplateField><itemtemplate>
                <tr>
					<td colspan="100%">
                        
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
                               </tr><tr><td style="width:50%; vertical-align:top"> <table><tr><td>     
                 <asp:ListView ID="ListView3" runat="server" DataKeyNames="Expr1, Active Version" EnableModelValidation="True" OnItemCommand="ListView3_ItemCommand" OnItemDataBound="ListView3_ItemDataBound" OnItemEditing="ListView3_ItemEditing" OnItemUpdating="ListView3_ItemUpdating" OnItemCanceling="ListView3_ItemCanceling" >

        
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
               
    </asp:ListView></td><td>
        <b><u>Deliveries:</u></b><br />
                       <asp:gridview ID="DeliveryViewGridUC" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" >
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

                            <br /><br />
        <u><b>Certifications:</b></u>
        <asp:GridView ID="CertGridNC" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CheckBoxField DataField="CertCompReqd" HeaderText="C of C" SortExpression="CertCompReqd" />
            <asp:CheckBoxField DataField="PlateCertReqd" HeaderText="Plating" SortExpression="PlateCertReqd" />
            <asp:CheckBoxField DataField="MatlCertReqd" HeaderText="Material" SortExpression="MatlCertReqd" />
            <asp:CheckBoxField DataField="SerializationReqd" HeaderText="Serialized" SortExpression="SerializationReqd" />
           
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        </td></tr></table>
                            <br /><b><u>Mat'l Quote Request:</u></b><br />
                            <asp:HiddenField ID="HdnID" runat="server" Value='<%# Eval("JobItemID") %>' />
                    <asp:GridView ID="GridView12" runat="server" OnRowDataBound="GridView12_RowDataBound" DataKeyNames="MatQueueID" AutoGenerateColumns="False" EnableModelValidation="True" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"  OnRowEditing="GridView12_RowEditing" OnRowUpdating="GridView12_RowUpdate" OnRowCancelingEdit="GridView12_RowCancel" OnRowDeleting="GridView12_RowDelete">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

            <asp:TemplateField  HeaderText="Mat'l" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteMatLabel" Text='<%# Eval("MaterialName") %>' SortExpression="MaterialName" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:HiddenField ID="hdQuoteMatl" runat="server" Value ='<%# Eval("MaterialID") %>' />
                    <asp:DropDownList runat="server" ID="QuoteMatList" DataValueField="MaterialID" DataTextField="Material" Width="130px"/>
                </EditItemTemplate>
            </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Dimension" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteDimLabel" Text='<%# Eval("Dimension") %>' SortExpression="Dimension" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:HiddenField ID="hdQuoteDim" runat="server" Value ='<%# Eval("MatDimID") %>' />
                    <asp:DropDownList runat="server" ID="QuoteDimList" DataValueField="MaterialDimID" DataTextField="Dimension"/>
                </EditItemTemplate>
            </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Size" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteSizeLabel" Text='<%# Eval("Size") %>' SortExpression="Size" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:HiddenField ID="hdQuoteSize" runat="server" Value ='<%# Eval("MatSizeID") %>' />
                    <asp:DropDownList runat="server" ID="QuoteSizeList" DataValueField="MaterialSizeID" DataTextField="Size" Width="60px"/>
                </EditItemTemplate>
            </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Length" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteLengthLabel" Text='<%# Eval("Length") %>' SortExpression="Length" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:Textbox runat="server" ID="QuoteLengthBox" Text='<%# Eval("Length") %>'  Width="32px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Qty" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteQtyLabel" Text='<%# Eval("Quantity") %>' SortExpression="Quantity" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:Textbox runat="server" ID="QuoteQtyBox" Text='<%# Eval("Quantity") %>' Width="32px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Cut" >
                <ItemTemplate>
                    <asp:Checkbox runat="server" ID="QuoteCutCheck" Checked='<%# Eval("Cut") %>' SortExpression="Cut" Enabled="false" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:Checkbox runat="server" ID="QuoteCutCheck" Checked='<%# Eval("Cut") %>' SortExpression="Cut" Enabled="true" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Sugg. Vendor" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteVendorLabel" Text='<%# Eval("VendorName") %>' SortExpression="SuggVendor" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:HiddenField ID="hdQuoteVendor" runat="server" Value ='<%# Eval("SuggVendor") %>' />
                    <asp:DropDownList runat="server" ID="QuoteVendorList" DataValueField="SubcontractID" DataTextField="VendorName"  Width="150px"><asp:ListItem Value="0" Text="None Selected" Selected="True"></asp:ListItem></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField  HeaderText="Appr. Req'd" >
                <ItemTemplate>
                    <asp:Checkbox runat="server" ID="QuoteApprCheck" Checked='<%# Eval("ReqdApproval") %>' SortExpression="ReqdApproval" Enabled="false" />
                </itemtemplate>
                <EditItemTemplate>
                    <asp:Checkbox runat="server" ID="QuoteApprCheck" Checked='<%# Eval("ReqdApproval") %>' SortExpression="ReqdApproval" Enabled="true" />
                </EditItemTemplate>
            </asp:TemplateField>

            
             <asp:CheckBoxField DataField="OrderPending" HeaderText="Order" SortExpression="OrderPending" />
        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                 <br /><b><u>Mat'l Allocation:</u></b><br />
                    <asp:GridView ID="GridView13" runat="server" AutoGenerateColumns="False" DataKeyNames="MatlAllocationID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="MatlPriceID" HeaderText="Mat'l Lot ID" SortExpression="MatPriceID" />
            <asp:BoundField DataField="MaterialName" HeaderText="Material" SortExpression="MaterialName" />
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
            <asp:BoundField DataField="pct" HeaderText="Pct Alloc." SortExpression="pct" DataFormatString="{0:P2}" />
        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                            <asp:MultiView runat="server" ID="MatAllocMulti" ActiveViewIndex="0"><asp:View runat="server" ID="ExistAllocView">
                                 <br />
                                <asp:Button ID="AddAlloc" Text="Add Allocation" runat="server" CommandArgument="addallocview" CommandName="SwitchViewByID" OnCommand="AddAlloc_Command" /> <br />
                </asp:View>
                    <asp:View runat="server" ID="addallocview">
                        <br />
                                <asp:GridView runat="server" ID="StockMatlGrid" OnRowCommand="StockMatlGrid_RowCommand" AutoGenerateColumns="False" DataKeyNames="MatPriceID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                                    <Columns>
                                        <asp:BoundField DataField="MatPriceID" HeaderText="Mat'l Lot ID" SortExpression="MatPriceID" />
                                        <asp:BoundField DataField="MaterialName" HeaderText="Material" SortExpression="MaterialName" />
                                        <asp:BoundField DataField="Dimension" HeaderText="Dim" SortExpression="Dimension" />
                                        <asp:BoundField DataField="D" HeaderText="D" SortExpression="D" />
                                        <asp:BoundField DataField="H" HeaderText="H" SortExpression="H" />
                                        <asp:BoundField DataField="W" HeaderText="W" SortExpression="W" />
                                        <asp:BoundField DataField="L" HeaderText="L" SortExpression="L" />
                                        <asp:BoundField DataField="Qty" HeaderText="Q" SortExpression="Qty" />
                                        <asp:CheckBoxField DataField="PurchasedCut" HeaderText="Cut" SortExpression="Purchased Cut" />     
                                        <asp:BoundField DataField="pct" HeaderText="Pct Avail." SortExpression="pct" DataFormatString="{0:P2}" />                                   
                                        <asp:ButtonField CommandName="Allocate" Text="Allocate"/>
                                    </Columns>
                                </asp:GridView>                   
                        <br />
                           <asp:Button runat="server" ID="AllocCancel" Text="Cancel" CommandArgument="ExistAllocView" CommandName="SwitchViewByID" OnCommand="CancelAddAlloc_Command" />
                    </asp:View>
                </asp:MultiView>

</BR>  <b><u>Fixtures:</u></b><br/>
                        <asp:GridView ID="RevisionFixtureOrdersUC" runat="server" OnRowDataBound="RevisionFixtureOrdersUC_RowDataBound" DataKeyNames="Location, FixtureRevID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                <asp:BoundField DataField="OperationName" HeaderText="Operation" SortExpression="OperationName" />
                <asp:TemplateField HeaderText="Location & Notes">
                    <ItemTemplate>
                        <div id="loclabeldiv" runat="server">
                            <asp:Label runat="server" Text='<%#Eval("Location") %>' /> - 
                            <asp:Label runat="server" Text='<%#Eval("Note") %>' />
                            </div>
                         <div id="loctextdiv" runat="server">
                            Location: <asp:TextBox ID="fixloctext"  runat="server" Text='<%#Eval("Location") %>' />
                             <br></br>
                            Note: <asp:TextBox ID="fixnotetext"  runat="server" Text='<%#Eval("Note") %>' />
                             <asp:Button ID="FixtureCloseButton2" runat="server" OnClick="FixtureCloseButton2_Click" Text="Close" />
                            </div>
                              </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />

        </asp:GridView>
                 <br /><b><u>Machined Assembly Components:</u></b><br />
                 <asp:GridView ID="GridView16" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
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
                    <asp:GridView ID="GridView17" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="PerAssy" HeaderText="PerAssy" SortExpression="PerAssy" />
            <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" SortExpression="ItemNumber" />
            <asp:BoundField DataField="VendorName" HeaderText="VendorName" SortExpression="VendorName" />
            <asp:BoundField DataField="Each" HeaderText="Each" SortExpression="Each" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%#Eval("Weblink") %>' target="_blank">Link</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>

                        </td><td style="width:50%; vertical-align:top">
                

                <br /><b><u>Operations:</u></b><br />
                
                            <asp:GridView ID="GridView10" runat="server" OnRowCommand="GridView10_RowCommand" OnRowDeleting="GridView10_RowDelete" OnRowCancelingEdit="GridView10_RowCancel" OnRowUpdating="GridView10_RowUpdate" OnRowEditing="GridView10_RowEditing" OnRowDataBound="GridView10_RowDataBound" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, JobItemID, ProcessOrder, SetupID" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
             <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					<ItemTemplate>
					<a href="JavaScript:divexpandcollapse('div<%# Eval("JobSetupID") %>');">
                            +</a>  
                        
					</ItemTemplate>

				   </asp:TemplateField>
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
                    <asp:DropDownList id="ProcDescList" runat="server" HeaderText="Description" />                     
                     <asp:HiddenField ID="hdWCDescList" runat="server" Value ='<%# Eval("[WorkcodeID]") %>' />
                    <asp:DropDownList id="WCDescList" runat="server" HeaderText="Description" />
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
                    <asp:label id="Comment" runat="server" Text='<%# Bind("[Comments]") %>' HeaderText="Comments" ReadOnly="True" SortExpression="Comments" />
                </ItemTemplate>
                 <EditItemTemplate>
                    <asp:Textbox id="CommentBox" runat="server" Text='<%# Eval("[Comments]") %>' HeaderText="Comments" Width="200px" />
                </EditItemTemplate>
              </asp:TemplateField> 
            <asp:TemplateField HeaderText="Done">
                <ItemTemplate>
                    <asp:checkbox id="CompletedLbl" runat="server" Checked='<%# Bind("[Completed]") %>' HeaderText="Done" ReadOnly="True" Enabled="false" SortExpression="Completed" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="Done" Checked='<%# Eval("[Completed]") %>' runat="server" Enabled="true" />
                </EditItemTemplate>
              </asp:TemplateField>            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:MultiView runat="server" ID="logmultiview" ActiveViewIndex="0">
                        <asp:View ID="AlreadyLogView" runat="server">                        
                             <asp:GridView ID="LogHoursGrid" runat="server" OnRowCommand="LogHoursGrid_RowCommand" OnRowDeleting="LogHoursGrid_RowDelete" OnRowCancelingEdit="LogHoursGrid_RowCancel" OnRowUpdating="LogHoursGrid_RowUpdate" OnRowEditing="LogHoursGrid_RowEditing" OnRowDataBound="LogHoursGrid_RowDataBound" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, ProcessID" EnableModelValidation="True" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                 <Columns>
                                     <asp:TemplateField HeaderText="Empl.">
                                        <ItemTemplate>
                                            <asp:label id="EmplLbl" runat="server" Text='<%# Bind("[Name]") %>' HeaderText="Empl." ReadOnly="True" SortExpression="Name" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hdEmpl" runat="server" Value ='<%# Eval("[EmployeeID]") %>' />                    
                                            <asp:DropDownList id="Empl" runat="server" HeaderText="Empl." DataValueField="EmployeeID" DataTextField="Name" OnSelectedIndexChanged="Empl_SelectedIndexChanged" />
                                        </EditItemTemplate>
                                      </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="In">
                                                         <ItemTemplate>
                                            <asp:label id="QtyInLbl" runat="server" Text='<%# Bind("[QuantityIn]") %>' HeaderText="In" ReadOnly="True" SortExpression="QtyIn" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Textbox id="QtyIn" runat="server" Text='<%# Eval("[QuantityIn]") %>' HeaderText="Qty In" Width="32px" />
                                        </EditItemTemplate>
                                      </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Out">
                                                         <ItemTemplate>
                                            <asp:label id="QtyOutLbl" runat="server" Text='<%# Bind("[QuantityOut]") %>' HeaderText="Qty Out" ReadOnly="True" SortExpression="QtyOut" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Textbox id="QtyOut" Text='<%# Eval("[QuantityOut]") %>' runat="server" HeaderText="Qty Out" Width="32px" />
                                        </EditItemTemplate>
                                      </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Hours">
                                                         <ItemTemplate>
                                            <asp:label id="HoursLbl" runat="server" Text='<%# Bind("[Hours]") %>' HeaderText="Hours" ReadOnly="True" SortExpression="Hours" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Textbox id="Hours" Text='<%# Eval("[Hours]") %>' runat="server" HeaderText="Hours" Width="32px" />
                                        </EditItemTemplate>
                                      </asp:TemplateField> 
                                      <asp:TemplateField>
                                         <EditItemTemplate>
                                             <asp:CheckBox runat="server" ID="MoveOn" Checked="true" />
                                         </EditItemTemplate>
                                         
                                     </asp:TemplateField>
                                     
                                 </Columns>
                                 <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#000084" Font-Bold="False" Font-Size="Small" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" Font-Size="Small" ForeColor="White" />
                            </asp:GridView>
                            <asp:Button ID="LogNew" Text="Log New" runat="server" CommandArgument="NewLogView" CommandName="SwitchViewByID" OnCommand="LogNew_Command" />
                        </asp:View>
                        <asp:View ID="NewLogView" runat="server">
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
                                        <td><asp:DropDownList runat="server" ID="EmployeeList2" DataValueField="EmployeeID" DataTextField="Name"></asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="QtyInAdd" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="QtyOutAdd" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="HoursAdd" Width="75"></asp:TextBox></td>                                    
                                        <td><asp:CheckBox runat="server" ID="MoveOn" Checked="true" /></td></tr>
                                    <tr>
                                        <td><asp:Button ID="LogNewNow" text="Execute" runat="server" CommandArgument="AlreadyLogView" CommandName="SwitchViewByID" OnCommand="LogNewNow_Command" /></td>
                                    <td><asp:Button runat="server" ID="CancelLog" Text="Cancel" CommandArgument="AlreadyLogView" CommandName="SwitchViewByID" OnCommand="CancelLog_Command" /></td><td colspan="2"> </td></tr>
                                </table>                               
                        <br />
                           
                        </asp:View>
                    </asp:MultiView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField Text="Order Fixture" CommandName="OrderFixture" />
            <asp:ButtonField Text="Quick Fixture" CommandName="QuickFixture" />
                      
            <asp:TemplateField>
               
                <ItemTemplate> <tr><td colspan="9">
             <div id='div<%# Eval("JobSetupID") %>'  style="display:none; left: 15px;">  
                            <table><tr>
                            <td>
                               <b><u>Pictures:</u></b><br/>
            
		            <asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload><asp:Button runat="server" CommandName="Attach" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Attach" />
                
                        <a href="javascript:void(window.open('<%# "FullImage.ashx?ImID="+ Eval("SetupID")%>','_blank','toolbar=no,menubar=no'))" > <asp:Image ID="Image1" runat="server" ImageUrl='<%# "Thumbnail.ashx?ImID="+ Eval("SetupImageID")  %>'/> </a>
                   
                                     </BR>  <b><u>Fixtures:</u></b><br/>
                        <asp:GridView ID="SetupFixtureOrdersNC" runat="server" OnRowDataBound="SetupFixtureOrdersNC_RowDataBound" DataKeyNames="Location, FixtureRevID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                <asp:TemplateField HeaderText="Location & Notes">
                    <ItemTemplate>
                        <div id="loclabeldiv" runat="server">
                            <asp:Label runat="server" Text='<%#Eval("Location") %>' />
                            <asp:Label runat="server" Text='<%#Eval("Note") %>' />
                            </div>
                         <div id="loctextdiv" runat="server">
                            Location: <asp:TextBox ID="fixloctext"  runat="server" Text='<%#Eval("Location") %>' />
                             Note: <asp:TextBox ID="fixnotetext"  runat="server" Text='<%#Eval("Note") %>' />
                             <asp:Button ID="FixtureCloseButton" runat="server" OnClick="FixtureCloseButton_Click" Text="Close" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
                                
                
                                </td></tr></table></div>  </ItemTemplate>
                </asp:TemplateField> 
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                            <asp:MultiView runat="server" ID="opsmultiview" ActiveViewIndex="0"><asp:View runat="server" ID="ExistView">
                                 <br />
                                <asp:Button ID="InputOpButton" Text="Add Operation" runat="server" CommandArgument="addview" CommandName="SwitchViewByID" OnCommand="AddOp_Command" /><asp:Button ID="InputSubButton" Text="Add Subcontract" runat="server" CommandArgument="addSub" CommandName="SwitchViewByID" OnCommand="AddSub_Command" /> <br />
                </asp:View>
                    <asp:View runat="server" ID="addview">
                        <br />
                                <table>
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
                                        <td><asp:DropDownList runat="server" ID="DropDownList2" DataValueField="OperationID" DataTextField="OperationName"></asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="TextBox3" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="TextBox4" Width="75"></asp:TextBox></td>
                                        <td><asp:TextBox runat="server" ID="OpCommentBox" Width="250"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="EmployeeAddList" Width="150" DataTextField="Name" DataValueField="EmployeeID"></asp:DropDownList></td>
                       
                                    </tr>
                                    <tr><td><asp:Button runat="server" ID="CancelAddOp" Text="Cancel" CommandArgument="ExistView" CommandName="SwitchViewByID" OnCommand="CancelAddOp_Command" /></td><td><asp:Button runat="server" ID="AddNow" Text="Add" CommandArgument="ExistView" CommandName="SwitchViewByID" OnCommand="AddNowOp_Command" /></td></tr>
                                </table>                               
                        <br />
                           
                    </asp:View>
                                 <asp:View runat="server" ID="AddSub">
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
                                        <td><asp:DropDownList runat="server" ID="DropDownList3" DataValueField="WorkcodeID" DataTextField="Workcode"></asp:DropDownList></td>
                                        
                                        <td><asp:TextBox runat="server" ID="SubCommentBox" Width="250"></asp:TextBox></td>
                                        <td><asp:DropDownList runat="server" ID="EmployeeAddList2" Width="150" DataTextField="Name" DataValueField="EmployeeID"></asp:DropDownList></td>
                                    </tr>
                                    <tr><td><asp:Button runat="server" ID="CancelAddSub" Text="Cancel" CommandArgument="ExistView" CommandName="SwitchViewByID" OnCommand="CancelAddSub_Command" /></td><td><asp:Button runat="server" ID="AddSubNow" Text="Add" CommandArgument="ExistView" CommandName="SwitchViewByID" OnCommand="AddNowSub_Command" /></td></tr>
                                </table>                               
                        <br />
                           
                    </asp:View>
                </asp:MultiView>
                 <br /><b><u>Subcontract History:</u></b><br />
                <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
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
    </asp:GridView>
                            </td>
                 
                
                      </tr></table>  </div>
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
                      </asp:View>
</asp:Multiview>
                 </p>
            </asp:Panel>
        </asp:Panel>


    <p>
    <asp:Label ID="UpdateResults" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>

    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM ProductionViewWP1 ORDER BY LateStartInt" >
        
        
        
        
        
        
       

    </asp:SqlDataSource>

<asp:SqlDataSource ID="MonseesSqlDataSourceNC" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From NotClearedProduction ORDER BY LateStartInt" OnSelecting="MonseesSqlDataSourceNC_Selecting" UpdateCommand="UPDATE [Job Item] SET [MaterialSource] = @MaterialSource WHERE [JobItemID] = @JobItemID; UPDATE [Version] SET PlatingID = @Plate, HeatTreatID = @Heat WHERE RevisionID = @RevisionID;" EnableCaching="False" >

            <UpdateParameters>
                <asp:Parameter Name="MaterialSource" Type="Int32" ></asp:Parameter>
<asp:Parameter Name="Heat" Type="Int32" ></asp:Parameter>
<asp:Parameter Name="Plate" Type="Int32" ></asp:Parameter>
                
                <asp:Parameter Type="Int32" 
                  Name="JobItemID"></asp:Parameter>   
<asp:Parameter Type="Int32" 
                  Name="RevisionID"></asp:Parameter>              
            </UpdateParameters>


    </asp:SqlDataSource>

<asp:SqlDataSource ID="MonseesSqlDataSourceSusp" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SuspendedProduction" EnableCaching="False" >



    </asp:SqlDataSource>

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

    <asp:SqlDataSource ID="SetupFixtureSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
    
     <asp:SqlDataSource ID="SqlDataSourceCert" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
       
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
