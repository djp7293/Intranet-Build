<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="ActiveJobs.aspx.cs" Inherits="Monsees.ActiveJobsBase"  %>

<asp:Content ContentPlaceHolderID="headContent"  runat="server">
  <title>Production Schedule</title>
    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        .GridView{
         Border-Color:#999999;
             Border-Style:none;
             Border-Width:1px;
             padding-left:3px;

             width:7.5in;
              
        }

        .hiddencol
            {
            display: none;
            }


        .rowstyle{
              background-color:#EEEEEE;
             
        }

        .alternate{
            background-color:gainsboro;
        }

        @media print 
        {
            #header, #nav, footer, .noprint
            {
            display: none;
            }

            .style1{
                display:none;
            }

            /* Ensure the content spans the full width */
            #content
            {
            width: 7.5in; margin: 0; float: none;
            }

            .GridView{
             Border-Color:#999999;
             Border-Style:none;
             Border-Width:1px;
             font-size:xx-small;             
             width:7.5in;
             
               
             }

            .alternate{
                background-color:white;
            }

            .rowstyle{
                background-color:white;
                  
            }
            
            /* Change text colour to black (useful for light text on a dark background) */
            .lighttext
            {
            color: #000 
            }

            /* Improve colour contrast of links */
            a:link, a:visited
            {
            color: #781351
            }

            
            @page {
                size:landscape;
                font-size:xx-small; 
                margin-left:0.25in;
                margin-right:0.25in;               
                
            }
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
         <table class="noprint" width="100%">
            <tr >
                <td align="left" style="font-size:x-small" valign="middle" class="style2">
                    <asp:Button ID="UserNameLabel" runat="server" onclick="UserNameLabel_Click" text="Add Line(s) to PO"
                        /></td>
                <td style="font-size:x-small">
                    <asp:DropDownList ID="UsersDropDownList" runat="server" 
                        style="margin-left: 0px" Width="215px" AutoPostBack="True" DataTextField="Name" DataValueField="EmployeeID">
                    </asp:DropDownList>
                </td>

            <td width="25%">
                        <asp:Button ID="DeptSchedule" runat="server" Text="Dept. Schedules" 
                            onclick="DeptSchedule_Click" />
                    </td>
           
            </tr>
         </table>
              </br

	<table style="font-size:x-small">

		<tr><td > Lot #: <asp:Textbox ID="LotFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td style="font-size:x-small">
                               Company: <asp:TextBox ID="CompanyFilter" runat="server" >
        				       
                               </asp:TextBox>
                               </td>
               <td style="font-size:x-small"> PartNumber: <asp:TextBox ID="PartFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td style="font-size:x-small"> Description: <asp:Textbox ID="DescFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td style="font-size:x-small"> PM: <asp:Textbox ID="PMFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td style="font-size:x-small"><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />     </td>
             <td>
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="x-Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
		</tr></table>
		<table itemstyle-cssclass="GridviewTable">
		<tr>
			<td colspan="15">
      			        
				                  <asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   
                <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                    AutoGenerateColumns="False" BackColor="White" 
                    CssClass="GridView" CellPadding="3"   
                    DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                    onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
                    AllowSorting="True" OnRowDataBound="ProductionViewGrid_RowDataBound"  
                    onselectedindexchanged="ProductionViewGrid_SelectedIndexChanged" DataKeyNames="JobItemID">
                    <RowStyle CssClass="rowstyle" ForeColor="Black" Font-Size="Small"  />
                    <Columns>
                        <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
		                    <ItemTemplate>
					            <asp:Button runat="server" ID="ExpColMain" OnClick="ExpandCollapse" Text="+"  />
					        </ItemTemplate>

				       </asp:TemplateField>
				        <asp:BoundField DataField="Job_Number" HeaderText="Job #" 
                            SortExpression="Job_Number" ItemStyle-Width="5%">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                            SortExpression="JobItemID" ItemStyle-Width="3%">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CAbbr"  
                            HeaderText="Cust. Code" SortExpression="CAbbr" ItemStyle-Width="4%">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>				   
                        <asp:TemplateField ItemStyle-Width="8%" HeaderText="Part Number">
					        <ItemTemplate>
					            <asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>' HeaderText="Part Number"></asp:LinkButton>
					        </ItemTemplate>
					    </asp:TemplateField>
                        <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                            SortExpression="Revision Number" ItemStyle-Width="2%">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>				   
                        <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                            SortExpression="DrawingNumber" ItemStyle-Width="12%">
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
                        <asp:BoundField DataField="MaterialLOC" HeaderText="Matl" 
                            SortExpression="MaterialLoc" ItemStyle-Width="4%">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
				       <asp:BoundField DataField="JobSetupID" HeaderText="ID" ItemStyle-CssClass="hiddencol" ItemStyle-Width="0%" FooterStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
				           <ItemStyle HorizontalAlign="Left" />
				           <HeaderStyle CssClass="hiddencol"/>
				       </asp:BoundField> 			                       
			           <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="8%">
				            <ItemStyle HorizontalAlign="Left" />
				       </asp:BoundField>
                       <asp:BoundField DataField="Comments" HeaderText="Process Notes" SortExpression="Comments" ItemStyle-Width="9%">
				            <ItemStyle HorizontalAlign="Left" />
				       </asp:BoundField>
			           <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" 	DataFormatString="{0:MM-dd-yyyy}"	SortExpression="LateStartInt"  ItemStyle-Width="6%">
				            <ItemStyle HorizontalAlign="Left" />
				       </asp:BoundField>
                        <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-Width="3%"  />
                                   
                        <asp:ButtonField CommandName="Inspection" Text="Report" HeaderText="Inspection" ItemStyle-Width="4%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                      
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="InitCAR" Text="CAR Request" ItemStyle-Width="4%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                      
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="AddFixture" Text="Order Fixture" ItemStyle-Width="4%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                      
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="QuickFixture" Text="Quick Fixture" ItemStyle-Width="4%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                                      
                        </asp:ButtonField>
                        <asp:CheckBoxField DataField="IsDrawing" SortExpression="IsDrawing" ItemStyle-Width="3%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint" />
   				        <asp:TemplateField  FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					        <ItemTemplate>
					            <asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					        </ItemTemplate>                          
				       </asp:TemplateField>  
                        <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%# Convert.ToString(Eval("NewPart")) %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Convert.ToString(Eval("CAbbr")) %>' />
                              </ItemTemplate>
                          </asp:TemplateField>
                        <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					        <ItemTemplate>
                                <tr><td colspan="21">   
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
                                            </td></tr>
                                            <tr><td style="width:50%; vertical-align:top"> 
                                                <table><tr><td> 
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
                                                            <td runat="server" style="background-color:#DCDCDC;color: #000000;vertical-align:top" >
                                                                 Plating:
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Plating") %>' />
                                                                 <br />HeatTreatment:
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Heat_Treat") %>' />
                                                                <br /> Subcontract:
                                                                <asp:Label ID="SubcontractLabel" runat="server" Text='<%# Eval("Subcontract") %>' />
                                                                 <br />Subcontract 2:
                                                                <asp:Label ID="Subcontract2Label" runat="server" Text='<%# Eval("Subcontract2") %>' />
                                                                <br />Material: 
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
				                                            <asp:BoundField DataField="CurrDelivery" HeaderText="Delivry" SortExpression="CurrDelivery" DataFormatString="{0:MM-dd-yyyy}">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ControlStyle Font-Size="Small" />
                                                            </asp:BoundField>  
                                                            <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ControlStyle Font-Size="Small" />
                                                            </asp:BoundField>   
                                                            <asp:BoundField DataField="PONumber" HeaderText="PO #" SortExpression="PONumber">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ControlStyle Font-Size="Small" />
                                                            </asp:BoundField> 
                                                            <asp:BoundField DataField="Ready" HeaderText="RTS" SortExpression="Ready">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ControlStyle Font-Size="Small" />
                                                            </asp:BoundField> 
				                                            <asp:BoundField DataField="Shipped" HeaderText="Shipped" SortExpression="Shipped">
                                                                <ControlStyle Font-Size="Small" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>    
                                                            <asp:BoundField DataField="Suspended" HeaderText="Suspended" SortExpression="Suspended">
                                                                <ControlStyle Font-Size="Small" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>                 
							                            </Columns>
                                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
                                                </TD></TR>
                                            </TABLE>
                                            <br />
                                            <b><u>Mat'l Quote Request:</u></b>
                                            <br />
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
                                            <br />
                                            <b><u>Mat'l Order:</u></b>
                                            <br />
                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" OnRowCommand="StockRetGrid_RowCommand" DataKeyNames="MatPriceID, MatlAllocationID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
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
                                                <asp:BoundField DataField="pct" HeaderText="Pct Alloc." SortExpression="pct" DataFormatString="{0:P2}" />
                                                <asp:ButtonField HeaderText="" Text="Ret. Mat'l to Stock" CommandName="MatltoStock" />
                                            </Columns>
                                             <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                        <asp:MultiView runat="server" ID="MatAllocMulti" ActiveViewIndex="0">
                                            <asp:View runat="server" ID="ExistAllocView">
                                            </asp:View>
                                            <asp:View runat="server" ID="StockRetView">
                                                <br />
                                                <asp:GridView runat="server" ID="StockRetGrid" AutoGenerateColumns="False" DataKeyNames="MatPriceID" EnableModelValidation="True" BackColor="White" OnRowCommand="StockRetGrid_RowCommand1" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                                                    <Columns>
                                                        <asp:BoundField DataField="MatPriceID" HeaderText="Lot ID" SortExpression="MatPriceID" />   
                                                        <asp:BoundField DataField="MaterialName" HeaderText="Material" SortExpression="MaterialName" />
                                                        <asp:BoundField DataField="Dimension" HeaderText="Dim" SortExpression="Dimension" />
                                                        <asp:BoundField DataField="D" HeaderText="D" SortExpression="D" />
                                                        <asp:BoundField DataField="H" HeaderText="H" SortExpression="H" />
                                                        <asp:BoundField DataField="W" HeaderText="W" SortExpression="W" />
                                                        <asp:TemplateField HeaderText="Length">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="LengthBox" Width="80px" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="QtyBox" Width="80px" ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" ID="LocBox" Width="80px"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:ButtonField Text="Return to Inventory" HeaderText="" CommandName="Return" />
                                                    </Columns>
                                                </asp:GridView>                   
                                                <br />
                                                <asp:Button ID="Button1" Text="Cancel" runat="server" CommandArgument="ExistAllocView" CommandName="SwitchViewByID" /> <br />
                                            </asp:View>
                                        </asp:MultiView>
                                        </BR>
                                        <b><u>Fixtures:</u></b>
                                        <br/>
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
                                   
                                        <br />
                                        <b><u>Machined Assembly Components:</u></b>
                                        <br />
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
                                        <br />
                                        <b><u>Purchased Assembly Components:</u></b>
                                        <br />
                                        <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
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
    
                                    </td>
                                    <td style="width:50%; vertical-align:top">
                
                                        <br />
                                        <b><u>Operations:</u></b>
                                        <br />
                                        <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDelete" OnRowDataBound="GridView2_RowDataBound" OnRowCancelingEdit="GridView2_RowCancel" OnRowUpdating="GridView2_RowUpdate" OnRowEditing="GridView2_RowEditing" AutoGenerateEditButton="false" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, JobItemID, SetupID" EnableModelValidation="True" GridLines="Vertical">
                                            <AlternatingRowStyle BackColor="#DCDCDC" />
                                            <Columns>
                                                 <asp:TemplateField FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint">
					                                <ItemTemplate>
					                                <a href="JavaScript:divexpandcollapse('div<%# Eval("JobSetupID") %>');">
                                                            +</a>  
                        
					                                </ItemTemplate>

				                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdJobSetupID" runat="server" Value ='<%# Eval("[JobSetupID]") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                <asp:ButtonField Text="Order Fixture" CommandName="OrderFixture" />
                                                <asp:ButtonField Text="Quick Fixture" CommandName="QuickFixture" />
                                                <asp:ButtonField Text="Open Setup Sheet" CommandName="OpenSetupSheet" />
                                                <asp:TemplateField>
                                                    <ItemTemplate> 
                                                        <tr><td colspan="11">
                                                            <div id='div<%# Eval("JobSetupID") %>'  style="display:none; left: 15px;">  
                                                            <table><tr> <tr>
                                                                <td colspan="2">
                                                                    <b><u>Active Work:</u></b><br/>
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
                                                                                            <asp:DropDownList id="Empl" runat="server" HeaderText="Empl." DataValueField="EmployeeID" DataTextField="Name" />
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField>   
                                                                                    <asp:TemplateField HeaderText="Machine">
                                                                                        <ItemTemplate>
                                                                                            <asp:label id="MachineLbl" runat="server" Text='<%# Bind("[MachineID]") %>' HeaderText="Machine" ReadOnly="True" SortExpression="Machine" />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:HiddenField ID="hdMach" runat="server" Value ='<%# Eval("[MachineID]") %>' />    
                                                                                            <asp:DropDownList id="Machine" runat="server" DataValueField="MachineID" DataTextField="Machine">
                                                                                                <asp:ListItem Value="0" Text="---Select---" Selected="True"></asp:ListItem>
                                                                                            </asp:DropDownList>
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
                                                                                    <asp:TemplateField HeaderText="Rework">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" ID="Fixlbl" Checked='<%# Bind("[Fix]") %>'/>
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:CheckBox runat="server" ID="Fix" Checked='<%# Eval("[Fix]") %>' />
                                                                                        </EditItemTemplate>                                         
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Notes">
                                                                                        <ItemTemplate>
                                                                                            <asp:label id="ProcDescLbl" runat="server" Text='<%# Bind("[Description]") %>' HeaderText="Notes" ReadOnly="True" SortExpression="Hours" />
                                                                                        </ItemTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <asp:Textbox id="ProcDesc" Text='<%# Eval("[Description]") %>' runat="server" HeaderText="Notes" Width="150px" />
                                                                                        </EditItemTemplate>
                                                                                    </asp:TemplateField> 
                                                                                    <asp:TemplateField HeaderText="In Time">
                                                                                         <ItemTemplate>
                                                                                             <asp:Label Text='<%# Bind("[Login]") %>' ID="Loginlbl" runat="server"></asp:Label>
                                                                                         </ItemTemplate>                                         
                                                                                    </asp:TemplateField>                                     
                                                                                    <asp:TemplateField HeaderText="Out Time">
                                                                                         <ItemTemplate>
                                                                                             <asp:Label Text='<%# Bind("[Logout]") %>' ID="Logoutlbl" runat="server"></asp:Label>
                                                                                         </ItemTemplate>                                         
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Done">
                                                                                         <EditItemTemplate>
                                                                                             <asp:CheckBox runat="server" ID="MoveOn" Checked='<%# Bind("[Completed]") %>' />
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
                                                                                        <td>Machine</td>                                        
                                                                                        <td>Qty In</td>
                                                                                        <td>Qty Out</td>
                                                                                        <td>Hours</td>
                                                                                        <td>Runtime</td>
                                                                                        <td>Rework</td>
                                                                                        <td>Description</td>
                                                                                        <td>Move On</td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><asp:DropDownList runat="server" ID="EmployeeList2" DataValueField="EmployeeID" DataTextField="Name"></asp:DropDownList></td>
                                                                                        <td><asp:DropDownList runat="server" ID="MachineList" DataValueField="MachineID" DataTextField="Machine"></asp:DropDownList></td>
                                                                                        <td><asp:TextBox runat="server" ID="QtyInAdd" Width="60"></asp:TextBox></td>
                                                                                        <td><asp:TextBox runat="server" ID="QtyOutAdd" Width="60"></asp:TextBox></td>
                                                                                        <td><asp:TextBox runat="server" ID="HoursAdd" Width="60"></asp:TextBox></td>   
                                                                                        <td><asp:TextBox runat="server" ID="RuntimeAdd" Width="60"></asp:TextBox></td>  
                                                                                        <td><asp:CheckBox runat="server" ID="FixAdd" Checked="false" /></td> 
                                                                                        <td><asp:TextBox runat="server" ID="DescAdd" Width="250"></asp:TextBox></td>                                
                                                                                        <td><asp:CheckBox runat="server" ID="MoveOn" Checked="true" /></td></tr>
                                                                                    <tr>
                                                                                        <td><asp:Button ID="LogNewNow" text="Execute" runat="server" CommandArgument="AlreadyLogView" CommandName="SwitchViewByID" OnCommand="LogNewNow_Command" /></td>
                                                                                    <td><asp:Button runat="server" ID="CancelLog" Text="Cancel" CommandArgument="AlreadyLogView" CommandName="SwitchViewByID" OnCommand="CancelLog_Command" /></td><td colspan="2"> </td></tr>
                                                                                </table>                               
                                                                        <br />
                           
                                                                        </asp:View>
                                                                    </asp:MultiView>
                                                                </td></tr>
                                                                <td colspan="2">
                                                                    <br />
                                                                    <b><u>Pictures:</u></b>
                                                                    <br/>
                                                                    <table><tr><td>
		                                                                <asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload><br /><asp:Button runat="server" CommandName="Attach" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Attach" />
                                                                    </td>
                                                                    <td>
                                                                        <a href="javascript:void(window.open('<%# "FullImage.ashx?ImID="+ Eval("SetupID")%>','_blank','toolbar=no,menubar=no'))" > <asp:Image ID="Image1" runat="server" ImageUrl='<%# "Thumbnail.ashx?ImID="+ Eval("SetupImageID")  %>'/> </a>
                                                                    </td></tr>
                                                                </table>
                                                            </td></tr>
                                                            <tr><td colspan="2">
                                                                <b><u>Fixtures:</u></b><br/>
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
                                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black"  Font-Size="Small" />
                                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                </asp:GridView>
                                                            </td></tr>
                                                            <tr><td>
                                                            </td><td>
                                                            </BR>  
                                                            <b><u>Setup History:</u></b>
                                                            <br/>
                                                            <asp:GridView ID="SetupHistoryGrid" runat="server" DataKeyNames="JobSetupID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID" />
                                                                    <asp:BoundField DataField="JobNumber" HeaderText="Job #" SortExpression="JobNumber" />
                                                                    <asp:BoundField DataField="Name" HeaderText="Machinist" SortExpression="Name" />
                                                                    <asp:BoundField DataField="Machine" HeaderText="Machine" SortExpression="Machine" />
                                                                    <asp:BoundField DataField="Quantity" HeaderText="Final Qty" SortExpression="Quantity" />
                                                                    <asp:BoundField DataField="QuantityIn" HeaderText="In" SortExpression="QuantityIn" />
                                                                    <asp:BoundField DataField="QuantityOut" HeaderText="Out" SortExpression="QuantityOut" />
                                                                    <asp:BoundField DataField="Hours" HeaderText="Hrs" SortExpression="Hours" />
                                                                    <asp:BoundField DataField="Logout" HeaderText="TimeStamp" SortExpression="Logout" />                            
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black"  Font-Size="Small" />
                                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            </asp:GridView>
                                                        </td></tr>
                                                        <tr><td colspan="2">
                                                            <br /> 
                                                            <b><u>Setup Comments (persists from lot-to-lot):</u></b>
                                                            <br/>
                                                            <asp:GridView ID="SetupEntries" runat="server" AutoGenerateColumns="false" BackColor="White" DataKeyNames="SetupEntryID" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True">
                                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Name" HeaderText="Employee" SortExpression="Name" >
                                                                        <ItemStyle Font-Size="XX-Small" />
                                                                        </asp:BoundField>
                                                                    <asp:BoundField DataField="Entry" HeaderText="Comment" SortExpression="Entry" />
                                                                    <asp:BoundField DataField="Timestamp" HeaderText="Time" SortExpression="Timestamp">
                                                                        <ItemStyle Font-Size="XX-Small" />
                                                                        </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
                                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                            </asp:GridView>
                                                            <br />
                                                            <table>
                                                                <tr>
                                                                    <td><b><u><span style="font-size:smaller">Employee</span></u></b></td>
                                                                    <td><b><u><span style="font-size:smaller">Entry</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><asp:DropDownList runat="server" ID="EmployeeCommentDrop" DataValueField="EmployeeID" DataTextField="Name"></asp:DropDownList></td>
                                                                    <td><asp:TextBox runat="server" ID="EntryText" Width="557px" Height="31px" Wrap="true" TextMode="MultiLine" ></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                            <asp:Button runat="server" ID="CommentButton" Text="Add comment" CommandName="AddComment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                                        </td> </tr>
                                                    </table>
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
                                <asp:MultiView runat="server" ID="opsmultiview" ActiveViewIndex="0">
                                    <asp:View runat="server" ID="ExistView">
                                        <br />
                                        <asp:Button ID="InputOpButton" Text="Add Operation" runat="server" CommandArgument="addview" CommandName="SwitchViewByID" OnCommand="AddOp_Command" /> <br />
                                    </asp:View>
                                    <asp:View runat="server" ID="addview">
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
                                </asp:MultiView>
                                <br />
                                <b><u>Subcontract History:</u></b>
                                <br />
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
                                </asp:GridView>
                            </td></tr></table>  
                        </div>
				        </td></tr> 
			        </ItemTemplate>
		        </asp:TemplateField>  
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True"  
                ForeColor="White" />
            <AlternatingRowStyle  cssclass="alternate"  />
        </asp:GridView>
                       
        </p>
        </asp:Panel>
        </asp:Panel>
    </td></tr></table> 

</div>

<asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="Select * From ProductionViewWP1 ORDER BY LateStartInt" EnableCaching="False" >

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

<asp:SqlDataSource ID="RetMatlSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >       
</asp:SqlDataSource> 
   
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
</asp:SqlDataSource>
   
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="SetupFixtureSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >           
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSourceCert" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SetupHistorySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">       
</asp:SqlDataSource>

<asp:SqlDataSource ID="SetupEntrySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">       
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >        
</asp:SqlDataSource>

<asp:SqlDataSource ID="EmployeeList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Name, EmployeeID FROM Employees WHERE Active=1">
</asp:SqlDataSource>


<asp:SqlDataSource ID="populatejoblist" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT Job_Number From ProductionView">
</asp:SqlDataSource>   

<asp:SqlDataSource ID="populatepartslist" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT PartNumber From ProductionView">
</asp:SqlDataSource>
    
<asp:SqlDataSource ID="LogHoursGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >        
</asp:SqlDataSource>

<asp:SqlDataSource ID="populateopslist" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT NextOp From ProductionView">
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