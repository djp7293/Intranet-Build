<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobSummaryViewer.aspx.cs" Inherits="Monsees.JobSummaryViewer" MasterPageFile="~/MasterPages/Monsees.master" %>
<%@ Import Namespace="Monsees.Security" %>

<asp:Content ContentPlaceHolderID="headContent"  runat="server">
    <title>Job Summary Viewer</title>
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
    <table>
        <tr>
            <td>
                <asp:ListView ID="ListView1" runat="server" DataKeyNames="JobID" EnableModelValidation="True" EnableTheming="False" OnItemDataBound="ListView1_ItemDataBound">

                        <EmptyDataTemplate>
                            <span>No data was returned.</span>
                        </EmptyDataTemplate>

                        <ItemTemplate>
                           JobID:
                            <asp:Label ID="JobIDLabel" runat="server" Text='<%# Eval("JobID") %>' />
                            <br />
                            JobNumber:
                            <asp:Label ID="JobNumberLabel" runat="server" Text='<%# Eval("JobNumber") %>' />
                            <br />
                            <asp:CheckBox ID="IsOpenCheckBox" runat="server" Checked='<%# Eval("IsOpen") %>' Enabled="false" Text="IsOpen" />
                            <br />
                            CreateDate:
                            <asp:Label ID="CreateDateLabel" runat="server" Text='<%# Eval("CreateDate") %>' />
                            <br />
                            ClosedDate:
                            <asp:Label ID="ClosedDateLabel" runat="server" Text='<%# Eval("ClosedDate") %>' />
                            <br />
                            CompanyName:
                            <asp:Label ID="CompanyNameLabel" runat="server" Text='<%# Eval("CompanyName") %>' />
                            <br />
                            ContactName:
                            <asp:Label ID="ContactNameLabel" runat="server" Text='<%# Eval("ContactName") %>' />
                            <br />
                             Cleared:
                            <asp:Checkbox ID="ClearCheck" runat="server" Checked='<%# Eval("Clear") %>' />
                            <br />
                <br />
                        </ItemTemplate>
                        <LayoutTemplate>
                            <div id="itemPlaceholderContainer" runat="server" style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <span runat="server" id="itemPlaceholder" />
                            </div>
                            <div style="text-align: center; font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF;">
                            </div>
                        </LayoutTemplate>
        
                    </asp:ListView>
            </td>
           
        </tr>
    </table>
<table width="100%"><tr><td style="vertical-align:top">
    
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" onrowcommand="GridView1_RowCommand" DataKeyNames="JobItemID, Active Version" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Height="177px" >
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
                                              <asp:TemplateField><ItemTemplate>
					            <a href="JavaScript:divexpandcollapse('div<%# Eval("JobItemID") %>');">
                                    +</a>  
                        
					            </ItemTemplate></asp:TemplateField>
            <asp:BoundField HeaderText="Lot" DataField="JobItemID" ReadOnly="True" SortExpression="JobItemID" />
            <asp:BoundField HeaderText="Part #" DataField="PartNumber" SortExpression="PartNumber" />
            <asp:BoundField HeaderText="Rev." DataField="Revision_Number" SortExpression="Revision_Number" />
            <asp:BoundField HeaderText="Description" DataField="DrawingNumber" SortExpression="DrawingNumber" />
            <asp:BoundField HeaderText="Qty" DataField="Quantity" SortExpression="Quantity" />
            <asp:BoundField HeaderText="NextDelivry" DataField="NextDelivery" DataFormatString="{0:MM-dd-yyyy}" SortExpression="NextDelivery" />
            <asp:BoundField HeaderText="Last Delivry" DataField="MaxOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" SortExpression="MaxOfCurrDelivery" />
            <asp:BoundField HeaderText="Notes" DataField="Notes" SortExpression="Notes" />
             <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
            <asp:CheckBoxField HeaderText="Open" DataField="IsOpen" SortExpression="IsOpen" />            
			<asp:ButtonField CommandName="GetFile" Text="Drawing" HeaderText="Drawing"></asp:ButtonField>					
            <asp:ButtonField HeaderText="" Text="Add Fixture" CommandName="AddFixt" />
            <asp:ButtonField HeaderText="" Text="Allocate Fixture" CommandName="AllocFixt" />
            <asp:CheckBoxField HeaderText="RTS" DataField="RTS" SortExpression="RTS" />
            <asp:CheckBoxField HeaderText="Cleared" DataField="Clear" SortExpression="Clear" />
             <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%#Eval("NewPart") %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%#Eval("CAbbr") %>' />
                              </ItemTemplate>
                          </asp:TemplateField>
            <asp:TemplateField><itemtemplate>
                <tr>
					<td colspan="100%">
                        
					<div id="div<%# Eval("JobItemID") %>"  style="display:none; left: 15px;">  
                        <table><tr><td style="width:35%; vertical-align:top">      
                <asp:ListView ID="ListView2" runat="server" DataKeyNames="Expr1, Active Version" EnableModelValidation="True" OnItemCommand="ListView2_ItemCommand" OnItemDataBound="ListView2_ItemDataBound" >

        
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
                 </td>
            </ItemTemplate>
            
        <LayoutTemplate>
            <table runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: small;">
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
                            <asp:HiddenField ID="HdnID" runat="server" Value='<%# Eval("JobItemID") %>' />
                    <asp:GridView ID="GridView4" runat="server" OnRowDataBound="GridView4_RowDataBound" DataKeyNames="MatQueueID" AutoGenerateColumns="False" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

            <asp:TemplateField  HeaderText="Mat'l" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteMatLabel" Text='<%# Eval("MaterialName") %>' SortExpression="MaterialName" />
                </itemtemplate>
                
            </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Dimension" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteDimLabel" Text='<%# Eval("Dimension") %>' SortExpression="Dimension" />
                </itemtemplate>
                
            </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Size" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteSizeLabel" Text='<%# Eval("Size") %>' SortExpression="Size" />
                </itemtemplate>
               
            </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Length" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteLengthLabel" Text='<%# Eval("Length") %>' SortExpression="Length" />
                </itemtemplate>
               
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Qty" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteQtyLabel" Text='<%# Eval("Quantity") %>' SortExpression="Quantity" />
                </itemtemplate>
               
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Cut" >
                <ItemTemplate>
                    <asp:Checkbox runat="server" ID="QuoteCutCheck" Checked='<%# Eval("Cut") %>' SortExpression="Cut" Enabled="false" />
                </itemtemplate>
              
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Sugg. Vendor" >
                <ItemTemplate>
                    <asp:Label runat="server" ID="QuoteVendorLabel" Text='<%# Eval("VendorName") %>' SortExpression="SuggVendor" />
                </itemtemplate>
                
            </asp:TemplateField>
             <asp:TemplateField  HeaderText="Appr. Req'd" >
                <ItemTemplate>
                    <asp:Checkbox runat="server" ID="QuoteApprCheck" Checked='<%# Eval("ReqdApproval") %>' SortExpression="ReqdApproval" Enabled="false" />
                </itemtemplate>
              
            </asp:TemplateField>

            
             <asp:CheckBoxField DataField="OrderPending" HeaderText="Order" SortExpression="OrderPending" />
        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                 <br /><b><u>Mat'l Allocation:</u></b><br />
                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
                        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
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
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                           
                        
                          
                        </td><td style="width:35%; vertical-align:top">
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
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:gridview>
                <br /><b><u>Operations:</u></b><br />
                
                            <asp:GridView ID="GridView2" runat="server" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDelete" OnRowCancelingEdit="GridView2_RowCancel" OnRowUpdating="GridView2_RowUpdate" OnRowEditing="GridView2_RowEditing" OnRowDataBound="GridView2_RowDataBound" AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, JobItemID, ProcessOrder" EnableModelValidation="True" GridLines="Vertical">
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
                    <asp:DropDownList id="ProcDescList" runat="server" HeaderText="Description" />
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
                             <asp:GridView ID="LogHoursGrid" runat="server" OnRowCommand="LogHoursGrid_RowCommand" OnRowDataBound="LogHoursGrid_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID, ProcessID" EnableModelValidation="True" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                 <Columns>
                                     <asp:TemplateField HeaderText="Empl.">
                                        <ItemTemplate>
                                            <asp:label id="EmplLbl" runat="server" Text='<%# Bind("[Name]") %>' HeaderText="Empl." ReadOnly="True" SortExpression="Name" />
                                        </ItemTemplate>
                                       
                                      </asp:TemplateField>   
                                     <asp:TemplateField HeaderText="In">
                                                         <ItemTemplate>
                                            <asp:label id="QtyInLbl" runat="server" Text='<%# Bind("[QuantityIn]") %>' HeaderText="In" ReadOnly="True" SortExpression="QtyIn" />
                                        </ItemTemplate>
                                       
                                      </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Out">
                                                         <ItemTemplate>
                                            <asp:label id="QtyOutLbl" runat="server" Text='<%# Bind("[QuantityOut]") %>' HeaderText="Qty Out" ReadOnly="True" SortExpression="QtyOut" />
                                        </ItemTemplate>
                            
                                      </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Hours">
                                                         <ItemTemplate>
                                            <asp:label id="HoursLbl" runat="server" Text='<%# Bind("[Hours]") %>' HeaderText="Hours" ReadOnly="True" SortExpression="Hours" />
                                        </ItemTemplate>
                                       
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
            
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                            <asp:MultiView runat="server" ID="opsmultiview" ActiveViewIndex="0"><asp:View runat="server" ID="ExistView">
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
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                            </td><td style="width:30%; vertical-align:top">
                 
                <b><u>Fixture Orders:</u></b><br />
                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="ContactName" HeaderText="ContactName" SortExpression="ContactName" />
                <asp:TemplateField>
						<ItemTemplate>
							<a href="/CloseFixture.aspx?id=<%#Eval("JobItemID")%>">Close</a>
						</ItemTemplate>
				</asp:TemplateField>		                         
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
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
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
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
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
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
        <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
                      </td></tr></table>  </div>
					</td>
					</tr>
                               </itemtemplate></asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
</asp:GridView>
   </tr></table>
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

      <asp:SqlDataSource ID="StockInventorySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
       
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
        
    </asp:SqlDataSource>
   
    
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="LogHoursGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>
   
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

        <asp:SqlDataSource ID="MonseesSqlDataSourceDeliveries" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
           
        </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
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