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
         <table class="style1" width="100%">
            <tr>
                <td align="left" valign="middle" class="style2">
                    <asp:Button ID="UserNameLabel" runat="server" onclick="UserNameLabel_Click" text="Add Line(s) to PO"
                        /></td>
                <td >
                    <asp:DropDownList ID="UsersDropDownList" runat="server" 
                        style="margin-left: 0px" Width="215px" AutoPostBack="True" DataTextField="Name" DataValueField="EmployeeID">
                    </asp:DropDownList>
                </td>

<td width="25%">
                    <asp:Button ID="DeptSchedule" runat="server" Text="Dept. Schedules" 
                        onclick="DeptSchedule_Click" />
                </td>
<td align="right" valign="middle" width="25%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label><asp:Button runat="server" ID="reload" Text="Refresh" OnClick="reload_Click" />
                </td>
            </tr>
         </table>
              

	<table itemstyle-cssclass="GridviewTable">

		<tr BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White">		
			<td width="5%" align="left">
    				<asp:DropDownList ID="AvailableJobList" DataSourceID="populatejoblist"
					AutoPostBack="true" DataValueField="Job_Number" runat="server" Width="60px" Font-Size="11px"
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
    				<asp:DropDownList ID="NextOpsList" DataSourceID="populateopslist"
					AutoPostBack="true" DataValueField="NextOp" runat="server" Width="100px" Font-Size="11px"
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
                   
                           <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
                               AllowSorting="True" 
                               onselectedindexchanged="ProductionViewGrid_SelectedIndexChanged" OnRowDataBound="ProductionViewGrid_RowDataBound" DataKeyNames="JobItemID">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
                                   <asp:TemplateField>
					<ItemTemplate>
					<a href="JavaScript:divexpandcollapse('div<%# Eval("JobItemID") %>');">
                            +</a>  
                        
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
                                   <asp:BoundField DataField="CompanyName"  
                                       HeaderText="Customer" SortExpression="CompanyName" ItemStyle-Width="8%">
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
                                   <asp:BoundField DataField="Heat Treat" HeaderText="Heat Treat" 
                                       SortExpression="Heat Treat" ItemStyle-Width="8%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Plating" HeaderText="Plating" 
                                       SortExpression="Plating" ItemStyle-Width="8%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                  
				   <asp:BoundField DataField="JobSetupID" HeaderText="ID" ItemStyle-CssClass="hiddencol" ItemStyle-Width="0%">
				   <ItemStyle HorizontalAlign="Left" />
				   <HeaderStyle CssClass="hiddencol"/>
				   </asp:BoundField> 
			                       
			           <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="8%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                    <asp:BoundField DataField="Comments" HeaderText="Process Notes" SortExpression="Comments" ItemStyle-Width="12%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" 	DataFormatString="{0:MM-dd-yyyy}"	SortExpression="LateStartInt"  ItemStyle-Width="6%">
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
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>  
                                   <asp:TemplateField>
					<ItemTemplate>
                                   <tr><td colspan="20">   
                    <div id="div<%# Eval("JobItemID") %>"  style="display:none; left: 15px;">  
                        <table><tr><td style="width:33%; vertical-align:top">      
                <asp:ListView ID="ListView2" runat="server" DataKeyNames="Expr1" EnableModelValidation="True" >

        
        <EmptyDataTemplate>
            <table style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        
        <ItemTemplate>
            <td runat="server" style="background-color:#DCDCDC;color: #000000;vertical-align:top" >Lot #: 
                <asp:Label ID="Expr1Label" runat="server" Text='<%# Eval("Expr1") %>' />
                <br />Revision:
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rev") %>' />
                <br />Quantity:
                <asp:Label ID="QuantityLabel" runat="server" Text='<%# Eval("Quantity") %>' />
                <br />Heat_Treat:
                <asp:Label ID="Heat_TreatLabel" runat="server" Text='<%# Eval("Heat_Treat") %>' />
                <br />Plating:
                <asp:Label ID="PlatingLabel" runat="server" Text='<%# Eval("Plating") %>' />
                 <br />Subcontract:
                <asp:Label ID="SubcontractLabel" runat="server" Text='<%# Eval("Subcontract") %>' />
                 <br />Subcontract2:
                <asp:Label ID="Subcontract2Label" runat="server" Text='<%# Eval("Subcontract2") %>' />
                <br />Hours:
                <asp:Label ID="EstimatedTotalHoursLabel" runat="server" Text='<%# Eval("EstimatedTotalHours") %>' />
                <br />Notes:
                <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />                             
                <br />Comments:
                <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                
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
                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" >
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
                <asp:GridView ID="GridView2" runat="server" OnRowDeleting="GridView2_RowDelete" OnRowDataBound="GridView2_RowDataBound" OnRowCancelingEdit="GridView2_RowCancel" OnRowUpdating="GridView2_RowUpdate" OnRowEditing="GridView2_RowEditing" AutoGenerateEditButton="false" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="JobSetupID" EnableModelValidation="True" GridLines="Vertical">
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
                                            <asp:DropDownList id="Empl" runat="server" HeaderText="Empl." DataValueField="EmployeeID" DataTextField="Name" />
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
                </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
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
					</td></tr> </ItemTemplate>
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


 

 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From ProductionViewWP1 ORDER BY LateStartInt" EnableCaching="False" FilterExpression="Job_Number like '{0}%'
                and PartNumber like '{1}%' and NextOp like '{2}%'">
<FilterParameters>
        <asp:ControlParameter Name="Job" ControlID="AvailableJobList"
            PropertyName="SelectedValue" />
        <asp:ControlParameter Name="Part" ControlID="AvailablePartsList"
            PropertyName="SelectedValue" />
<asp:ControlParameter Name="NextOp" ControlID="NextOpsList"
            PropertyName="SelectedValue" />
    </FilterParameters>


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

    <asp:SqlDataSource ID="SqlDataSource12" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
    </asp:SqlDataSource>

     <asp:SqlDataSource ID="EmployeeList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Name, EmployeeID FROM Employees WHERE Active=1"></asp:SqlDataSource>


    <asp:SqlDataSource ID="populatejoblist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Job_Number From ProductionView">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="populatepartslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PartNumber From ProductionView">
    </asp:SqlDataSource> 

    
      <asp:SqlDataSource ID="LogHoursGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" >
        
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