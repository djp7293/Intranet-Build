<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="ClearConfirmItemsInsp.aspx.cs" Inherits="Monsees._Default_ClearConfirmInsp"%>

<asp:Content ContentPlaceHolderID="headContent"  runat="server">
    <title>Clear and Confirm Schedules

    </title>
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
        .auto-style1 {
            text-decoration: underline;
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

                
                
                <td align="right" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
              

	

    </div>
    <table style="width:100%">
        <tr>
            <td style="width:40%">
                <span class="auto-style1"><strong>
     <br />
         
     Open RMA List:</strong></span><br />
     <table itemstyle-cssclass="GridviewTable">

		<tr>
			<td colspan="15">
                <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BackColor="White" DataKeyNames="DeliveryID" BorderColor="#999999" OnRowDataBound="GridView8_RowDataBound" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource2" EnableModelValidation="True" GridLines="Vertical">
         <AlternatingRowStyle BackColor="#DCDCDC" />
         <Columns>   
             <asp:TemplateField>
                <ItemTemplate>
				    <a href="JavaScript:divexpandcollapse('divlv<%# Eval("DeliveryID") %>');">+</a>                          
				</ItemTemplate>
             </asp:TemplateField>          
             <asp:BoundField DataField="RMANum" HeaderText="RMA #" SortExpression="RMANum" />    
             <asp:BoundField DataField="DeliveryID" HeaderText="Delvry ID" SortExpression="DeliveryID" />           
             <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
             <asp:BoundField DataField="Revision Number" HeaderText="Rev" SortExpression="Revision Number" />
             <asp:BoundField DataField="DrawingNumber" HeaderText="Desc." SortExpression="DrawingNumber" />
             <asp:BoundField DataField="CAbbr" HeaderText="Cust." SortExpression="CAbbr" />
             <asp:BoundField DataField="Quantity" HeaderText="Reject Qty" SortExpression="Quantity" />
             <asp:BoundField DataField="ReshipDescription" HeaderText="Reship" SortExpression="ReshipDescription" />   
             <asp:TemplateField>
                 <ItemTemplate>
                     <tr><td colspan="100%">
                     <div id='divlv<%# Eval("DeliveryID") %>'  style="display:none; left: 15px;">
                         <asp:GridView ID="RMALotView" runat="server" AutoGenerateColumns="False" BackColor="White" DataKeyNames="LotNumber" BorderColor="#999999" OnRowCommand="RMALotView_RowCommand" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                             <AlternatingRowStyle BackColor="#DCDCDC" />
                             <Columns>
                                 <asp:BoundField DataField="LotNumber"  
                                       HeaderText="Lot #" SortExpression="LotNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                        <ControlStyle Font-Size="Small" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Rejected Qty" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                        <ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
                                 <asp:BoundField DataField="InactiveInventory"  
                                       HeaderText="Inventory Qty" SortExpression="InactiveInventory">
                                       <ItemStyle HorizontalAlign="Center" />
                                        <ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
                                   <asp:BoundField DataField="RTSInventory"  
                                       HeaderText="RTS Inventory Qty" SortExpression="RTSInventory">
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
                                 			  
                                   <asp:ButtonField CommandName="InitiateCA" Text="Initiate CA" HeaderText="">
                                        <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
			  
                                   <asp:ButtonField CommandName="ReworkLot" Text="Disposition Lot" HeaderText="">
                                        <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
                                   
                                 	<asp:TemplateField>
						                <ItemTemplate>
							                <a href="/Lot.aspx?id=<%#Eval("LotNumber")%>"><span style="font-size:small">Lot</span></a>
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
                          </td></tr>
                 </ItemTemplate>
             </asp:TemplateField>        
         </Columns>
         <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
         <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
         <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
     </asp:GridView> </td></tr></table>
            </td></tr><tr>
            <td  style="width:60%">
                <span class="auto-style1"><strong>
     <br />
         
     CAR List Pending Assignment:</strong></span><br />
                <asp:GridView ID="GridView9" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView9_RowCommand" DataSourceID="SqlDataSource3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataKeyNames="CARID">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="CARID" HeaderText="CAR #" ReadOnly="True" SortExpression="CARID" />
                        <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID" />
                        <asp:BoundField DataField="CAbbr" HeaderText="Cust." SortExpression="CAbbr" />
                        <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
                        <asp:BoundField DataField="DrawingNumber" HeaderText="Desc." SortExpression="DrawingNumber" />
                        <asp:CheckBoxField DataField="CustomerCAR" HeaderText="Cust." SortExpression="CustomerCAR" />
                        <asp:BoundField DataField="CustomerCARNum" HeaderText="Cust CAR #" SortExpression="CustomerCARNum" />
                        <asp:BoundField DataField="InitiationDate" HeaderText="Init. Date" SortExpression="InitiationDate" DataFormatString="{0:MM-dd-yyyy}" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate" DataFormatString="{0:MM-dd-yyyy}" />
                        <asp:BoundField DataField="Definition" HeaderText="Problem" SortExpression="Definition" />                      
                       
                        <asp:BoundField DataField="InitEmployee" HeaderText="InitEmployee" SortExpression="InitEmployee" />                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:MultiView runat="server" ID="AssignCARMulti" ActiveViewIndex="0">
                                    <asp:View runat="server" ID="AssignDropdownButton" >
                                        <asp:Button runat="server" ID="GoToImpEmplDrowdown" CommandName="SwitchViewByID" Text="Assign" CommandArgument="AssignDropdownView" OnCommand="Unnamed_Command"/>
                                    </asp:View>
                                    <asp:View runat="server" ID="AssignDropdownView">
                                        <asp:DropDownList DataValueField="EmployeeID" ID="ImpEmplDropdown" DataTextField="Name" runat="server"></asp:DropDownList><br />
                                        <asp:Button runat="server" Text="Assign" ID="EmployeeToCAR" CommandName="SwitchViewByID" CommandArgument="AssignDropdownButton" OnCommand="EmployeeToCAR_Command" />
                                        <asp:Button runat="server" Text="Cancel" ID="CancelCARAssign" CommandName="SwitchViewByID" CommandArgument="AssignDropdownButton" />
                                    </asp:View>
                                </asp:MultiView>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField Text="Reject" CommandName="Reject" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [CorrectiveActionView] WHERE ([Completed] = 0 And [Initiated] = 0)">
                   
                </asp:SqlDataSource>
                <br />
                <span><strong>Open CAR List:</strong></span><br />
                <asp:GridView ID="GridView10" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView10_RowCommand" DataSourceID="SqlDataSource5" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataKeyNames="CARID">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="CARID" HeaderText="CAR #" ReadOnly="True" SortExpression="CARID" />
                        <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID" />
                        
                        <asp:CheckBoxField DataField="CustomerCAR" HeaderText="Cust." SortExpression="CustomerCAR" />
                        <asp:BoundField DataField="CustomerCARNum" HeaderText="Cust CAR #" SortExpression="CustomerCARNum" />
                        <asp:BoundField DataField="InitiationDate" HeaderText="Init. Date" SortExpression="InitiationDate" DataFormatString="{0:MM-dd-yyyy}" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate" DataFormatString="{0:MM-dd-yyyy}" />
                        <asp:BoundField DataField="Definition" HeaderText="Problem" SortExpression="Definition" />                      
                       
                        <asp:BoundField DataField="InitEmployee" HeaderText="InitEmployee" SortExpression="InitEmployee" />
                        <asp:BoundField DataField="ImpEmployee" HeaderText="ImpEmployee" SortExpression="ImpEmployee" />
                        <asp:ButtonField CommandName="ManageCAR" Text="Manage/View" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [CorrectiveActionView] WHERE ([Completed] = 0 And [Initiated] = 1)">
                   
                </asp:SqlDataSource>

            </td>
        </tr>
    </table>
     
     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [OpenRMASchedule]"></asp:SqlDataSource>
    
    <br /><b><u>Jobs to Clear:</u></b><br />    
    <asp:GridView ID="NewJobs" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="ClearJobs" DataKeyNames="JobID" OnRowCommand="NewJobs_RowCommand">
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
    
     <b><u style="text-align: left">
     <br />
     Outstanding Subcontract Orders:<br /> </u></b>
                           <asp:GridView ID="SubcontractGrid" runat="server" 
                               AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                               BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceSubcontract"  DataKeyNames="SubcontractItemID"
                               EmptyDataText="There are currently no parts out to subcontractors." 
                               EnableSortingAndPagingCallbacks="False" GridLines="Vertical" 
                               onrowcommand="SubcontractGrid_RowCommand" OnRowDataBound="SubcontractGrid_RowDataBound" PageSize="50" Width="100%" 
                               AllowPaging="True" AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Medium" ForeColor="Black" />
                               <Columns>
                                   <asp:BoundField DataField="SubcontractID" HeaderText="PO #" 
                                       SortExpression="SubcontractID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   
                                   <asp:BoundField DataField="VendorName" HeaderText="Vendor" 
                                       SortExpression="VendorName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                    <asp:BoundField DataField="LineItem" HeaderText="Line" 
                                       SortExpression="LineItem">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                                                       <asp:BoundField DataField="PartNumber" HeaderText="Part #" 
                                       SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                                                       <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                                                       <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="DateSent" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Issue Date" SortExpression="IssueDate">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   
                                   <asp:BoundField DataField="Name" HeaderText="Issued By" 
                                       SortExpression="Name">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ShipVia" HeaderText="Ship Via" 
                                       SortExpression="ShipVia">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Workcode" HeaderText="Work Code" 
                                       SortExpression="Workcode">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Notes" HeaderText="Note" 
                                       SortExpression="Notes">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
				   <asp:BoundField DataField="DueDate" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Due Date" SortExpression="DueDate">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField> 
                                   <asp:ButtonField CommandName="Received" Text="Received">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>  
                                    <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%# Convert.ToString(Eval("NewPart")) %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%#Eval("CAbbr") %>' />
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
    <br />
    <br /><b><u>Plating Subcontract Pending Certification<br /></u> </b>
    <asp:GridView ID="GridView1" EmptyDataText="There are no subcontract items pending certification." runat="server" AutoGenerateColumns="False" BackColor="White" DataKeyNames="SubcontractItemID" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView1_RowCommand" DataSourceID="ClearPlatingCerts" EnableModelValidation="True" GridLines="Vertical" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="SubcontractID" HeaderText="PO #" SortExpression="SubcontractID" />
            <asp:BoundField DataField="SubcontractItemID" HeaderText="Line ID" SortExpression="SubcontractItemID" />
            <asp:BoundField DataField="LineItem" HeaderText="Line" SortExpression="LineItem" />
            <asp:BoundField DataField="WorkCode" HeaderText="WorkCode" SortExpression="WorkCode" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
            <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="Revision Number" HeaderText="Rev" SortExpression="Revision Number" />
            <asp:BoundField DataField="DateSent" HeaderText="Sent" SortExpression="DateSent" DataFormatString="{0:MM-dd-yyyy}" />
            <asp:CheckBoxField DataField="HasDetail" HeaderText="Out" SortExpression="HasDetail" />
            <asp:BoundField DataField="DateReturned" HeaderText="Date Ret." SortExpression="DateReturned"  DataFormatString="{0:MM-dd-yyyy}"/>
             <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
            <asp:CheckBoxField DataField="PlateCertReqd" HeaderText="Cert Reqd" SortExpression="PlateCertReqd" />   
            <asp:TemplateField HeaderText="Attach Cert">
                <ItemTemplate>
		            <asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload><asp:Button runat="server" CommandName="Attach" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Attach" />
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:ButtonField CommandName="Ignore" Text="Ignore Requirement" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="ClearPlatingCerts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [PlatingCertQueue]"></asp:SqlDataSource>
    <br />
    <br />
    <b><u>Material Pending Certification:<br /> </u></b>
    <asp:GridView ID="GridView2" EmptyDataText="There are no material lots pending certification." runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ClearMatlCerts" EnableModelValidation="True" GridLines="Vertical" OnRowCommand="GridView2_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" SortExpression="MaterialPOID" />
            <asp:BoundField DataField="MatPriceID" HeaderText="Line ID" SortExpression="MatPriceID" />
            <asp:BoundField DataField="MaterialName" HeaderText="Mat'l Code" SortExpression="MaterialName" />
            <asp:BoundField DataField="Type" HeaderText="Desc." SortExpression="Type" />
            <asp:BoundField DataField="Dimension" HeaderText="Dimension" SortExpression="Dimension" />
            <asp:BoundField DataField="Diameter" HeaderText="D" SortExpression="Diameter" />
            <asp:BoundField DataField="Height" HeaderText="H" SortExpression="Height" />
            <asp:BoundField DataField="Width" HeaderText="W" SortExpression="Width" />
            <asp:BoundField DataField="Length" HeaderText="L" SortExpression="Length" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />            
            <asp:BoundField DataField="CertReqd" HeaderText="Cert Reqd" SortExpression="CertReqd" />
            <asp:BoundField DataField="Allocated" HeaderText="Pct Alloc." SortExpression="Allocated" />
            <asp:TemplateField HeaderText="Attach Cert">
                <ItemTemplate>
		            <asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload><asp:Button runat="server" CommandName="Attach" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Attach" />
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:ButtonField CommandName="Ignore" Text="Ignore Requirement" />
            
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="ClearMatlCerts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [MaterialCertQueue]"></asp:SqlDataSource>
    <br />
    <br />
    <b><u>Blank Inspection Reports Pending Completion:<br /> </u></b>
    <asp:GridView ID="GridView3" runat="server" EmptyDataText="There are no inspection reports pending completion." AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ClearInspectionReports" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" />
            <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
            <asp:BoundField DataField="Revision_Number" HeaderText="Rev." SortExpression="Revision_Number" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
             <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="ClearInspectionReports" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [CompanyName], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], ITAR FROM [BlankInspectionToDo]"></asp:SqlDataSource>
    <br />
    <br />
    <b><u>Shipments Pending Confirmation:<br />
    </u> </b>
    <table width="100%"><tr><td style="vertical-align:top">
    <asp:GridView ID="GridView4" runat="server" OnRowDataBound="GridView4_RowDataBound" EmptyDataText="There are no new shipments pending confirmation." OnRowCommand="GridView4_RowCommand" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Packing List #" DataSourceID="ClearShipments" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="JavaScript:divexpandcollapse('div<%# Eval("Packing List #") %>');">+</a>               
				</ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Packing List #" HeaderText="PL #" ReadOnly="True" SortExpression="Packing List #" />
            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
            <asp:BoundField DataField="PONumber" HeaderText="PO #" SortExpression="PONumber" />
            <asp:BoundField DataField="Ship Date" HeaderText="Ship Date" SortExpression="Ship Date" DataFormatString="{0:MM-dd-yyyy}" />
            <asp:BoundField DataField="Detailed" HeaderText="Detailed" SortExpression="Detailed" />             
            <asp:ButtonField CommandName="Clear" Text="Clear"   />
            <asp:ButtonField CommandName="MailZip" Text="MailZip" />
			<asp:TemplateField>
                <ItemTemplate>
                    <tr><td colspan="7">
                    <div id="div<%# Eval("[Packing List #]") %>"  style="display:none; left: 15px;"> 
                        <asp:GridView ID="GridView5" OnRowDataBound="GridView5_RowDataBound" OnRowCommand="GridView5_RowCommand" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="DeliveryID" EnableModelValidation="True" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="JavaScript:divexpandcollapse('divDel<%# Eval("DeliveryID") %>');">+</a>               
				                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LineItem" HeaderText="Item #" ReadOnly="True" SortExpression="LineItem" />
                                <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
                                <asp:BoundField DataField="Revision Number" HeaderText="Rev." SortExpression="Revision Number" />
                                <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />                                  
                                <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Qty" />  
                                <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" SortExpression="ShipDate" DataFormatString="{0:MM-dd-yyyy}" />
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
                                <asp:CheckBoxField DataField="CertCompReqd" HeaderText="CC Reqd" SortExpression="CertCompReqd" />   
                                <asp:CheckBoxField DataField="PlateCertReqd" HeaderText="Plate Cert Reqd" SortExpression="PlateCertReqd" />   
                                <asp:CheckBoxField DataField="MatlCertReqd" HeaderText="Mat Cert Reqd" SortExpression="MatlCertReqd" />                                
                                <asp:ButtonField CommandName="Clear" Text="Clear"   />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div id="divDel<%# Eval("[DeliveryID]") %>"  style="display:none; left: 15px;"> 
                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="DeliveryItemID" EnableModelValidation="True" GridLines="Vertical" OnRowCommand="GridView6_RowCommand">
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
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
                                                     <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:CheckBoxField DataField="PCert" HeaderText="P" ReadOnly="True" SortExpression="PCert" />
                                                    <asp:ButtonField CommandName="PQueue" Text="Ret to Plate Queue" HeaderText=""/>
                                                    <asp:CheckBoxField DataField="MCert" HeaderText="M" ReadOnly="True" SortExpression="MCert" />
                                                    <asp:ButtonField CommandName="MQueue" Text="Ret to Matl Queue" HeaderText=""/>
	 								   		       <asp:TemplateField>
						                            <ItemTemplate>
							                            <a href="/Lot.aspx?id=<%#Eval("LotNumber")%>">Lot</a>
						                            </ItemTemplate>
					                            </asp:TemplateField>
				                                    <asp:ButtonField CommandName="ViewReport" Text="Report" HeaderText="">
                                                   <ControlStyle Font-Size="Small" />
                                                   </asp:ButtonField>  
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
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
			        </div>
                        </td></tr>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
        </td></tr></table>
    <asp:SqlDataSource ID="ClearShipments" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ShipmentClearQueue]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="ClearShipmentSub" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="DeliveryLots" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
   
    <br />
    <br /><b><u>Inventory Pending Confirmation:</u></b><br />    
    
    <asp:GridView ID="Inventory" runat="server" EmptyDataText="There is no inventory pending confirmation." OnRowDataBound="Inventory_RowDataBound" AutoGenerateEditButton="true" OnRowEditing="Inventory_RowEditing" OnRowUpdating="Inventory_RowUpdating" OnRowCancelingEdit="Inventory_RowCancelingEdit"  BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="ClearInventory" DataKeyNames="InventoryID, DeliveryItemID" OnRowCommand="Inventory_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
               <asp:TemplateField HeaderText="Lot #" SortExpression="LotNumber">
                   <ItemTemplate>
                       <asp:Label runat="server" ID="LotLabel" Text='<%# Eval("LotNumber") %>' />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("LotNumber") %>' />
                   </EditItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Part #" SortExpression="PartNumber" >
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("PartNumber") %>'/>
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("PartNumber") %>'/>
                   </EditItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField  HeaderText="Rev." SortExpression="Revision_Number" >
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("Revision_Number") %>' />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("Revision_Number") %>' />
                   </EditItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Description" SortExpression="DrawingNumber">
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("DrawingNumber") %>'  />    
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("DrawingNumber") %>'  />  
                   </EditItemTemplate>
               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Status" SortExpression="Status">
                   <ItemTemplate>
                       <asp:Label runat="server" ID="StatusLabel" Text='<%# Eval("Status") %>'  />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:DropDownList ID="StatusList" runat="server" DataTextField="Status" DataValueField="InvStatusID"  />
                       <asp:HiddenField runat="server" ID="hdStatusList" Value='<%# Eval("InvStatusID") %>' />
                   </EditItemTemplate>
               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Loc." SortExpression="Location1">
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("Location1") %>'  />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("Location1") %>'  />
                   </EditItemTemplate>
               </asp:TemplateField>
                               <asp:TemplateField HeaderText="PO #" SortExpression="PONumber">
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("PONumber") %>'  />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("PONumber") %>'  />
                   </EditItemTemplate>
               </asp:TemplateField>
                               <asp:TemplateField  HeaderText="Company" SortExpression="CompanyName" >
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("CompanyName") %>'/>
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("CompanyName") %>'/>
                   </EditItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField  HeaderText="Assigned Qty" SortExpression="Quantity">
                   <ItemTemplate>
                       <asp:Label runat="server" ID="QtyLabel" Text='<%# Eval("Quantity") %>' />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:TextBox runat="server" ID="Qty" Text='<%# Eval("Quantity") %>' />
                   </EditItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField  HeaderText="Total Inventory Qty" SortExpression="Quantity">
                   <ItemTemplate>
                       <asp:Label runat="server" ID="InvQtyLabel" Text='<%# Eval("TotInvQty") %>' />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:TextBox runat="server" ID="InventoryQty" Text='<%# Eval("TotInvQty") %>' />
                   </EditItemTemplate>
               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Order Qty" SortExpression="OrderQty" >
                   <ItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("OrderQty") %>' />
                   </ItemTemplate>
                   <EditItemTemplate>
                       <asp:Label runat="server" Text='<%# Eval("OrderQty") %>' />
                   </EditItemTemplate>
               </asp:TemplateField>
               
                <asp:ButtonField CommandName="Accept" Text="Accept" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
                <asp:ButtonField CommandName="Reject" Text="Reject" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
               
            </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <br />
    <br /><b><u>Calibration Required</u></b><br />    <br />
    <asp:MultiView runat="server" ID="CalAddMulti" ActiveViewIndex="0">
        <asp:View runat="server" ID="AddCalButtonView">
            <asp:Button runat="server" ID="AddCalButton" Text="Add Inspection Equipment" CommandArgument="NewCalView" CommandName="SwitchViewByID" OnCommand="AddCalButton_Command" />
        </asp:View>
        <asp:View runat="server" ID="NewCalView">
            <table>
                <tr>
                    <td>Serial #</td><td>Description</td><td>Location</td><td>Gauge Type</td><td>Notes</td><td>Owner</td><td>Resolution</td><td>Active</td><td>Inspection Equip.</td>
                </tr>
                <tr>
                    <td><asp:TextBox runat="server" ID="SerialBox" Width="150px"></asp:TextBox></td>
                    <td><asp:TextBox runat="server" ID="DescBox" Width="150px"></asp:TextBox></td>
                    <td><asp:TextBox runat="server" ID="LocBox" Width="150px"></asp:TextBox></td>
                    <td><asp:DropDownList runat="server" ID="TypeDropDown" Width="150px" DataTextField="Description" DataValueField="GageTypeID"></asp:DropDownList></td>
                    <td><asp:TextBox runat="server" ID="NotesBox" Width="150px"></asp:TextBox></td>
                    <td><asp:DropDownList runat="server" ID="OwnerDropDown" Width="150px"></asp:DropDownList></td>
                    <td><asp:TextBox runat="server" ID="ResBox" Width="150px"></asp:TextBox></td>
                    <td><asp:CheckBox runat="server" ID="activecheck" Checked="true" Width="150px"></asp:CheckBox></td>
                    <td><asp:CheckBox runat="server" ID="inspequipcheck" Checked="true" Width="150px"></asp:CheckBox></td>
                </tr>
            </table>
            <asp:Button runat="server" ID="addequipbutton" CommandArgument="AddCalButtonView" CommandName="SwitchViewByID" OnCommand="addequipbutton_Command" />
        </asp:View>
    </asp:MultiView> <br /><br/>
    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="GaugeID" DataSourceID="SqlDataSource1" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="GaugeID" HeaderText="ID" SortExpression="GaugeID" />
                <asp:BoundField DataField="Serial" HeaderText="Serial #" SortExpression="Serial" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                <asp:BoundField DataField="GageType" HeaderText="Gage Type" SortExpression="GageType" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                <asp:BoundField DataField="Resolution" HeaderText="Resolution" SortExpression="Resolution" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="DecommisionCode" HeaderText="DecommisionCode" SortExpression="DecommisionCode" />
                <asp:BoundField DataField="Name" HeaderText="Owner" SortExpression="Name" />    
                <asp:BoundField DataField="CalibrationDue" HeaderText="Calibration Due" ReadOnly="True" SortExpression="CalibrationDue" DataFormatString="{0:MM-dd-yyyy}" />
                <asp:TemplateField><ItemTemplate>
                <asp:MultiView runat="server" ID="opsmultiview" ActiveViewIndex="0"><asp:View runat="server" ID="ExistView">
                <asp:Button ID="CompleteCalButton" Text="Complete Calibration" runat="server" CommandArgument="completecal" CommandName="SwitchViewByID" OnCommand="CompleteCal_Command" /> <br />
                </asp:View>
                    <asp:View runat="server" ID="completecal">
                        <br />
                                <table >
                                    <tr>
                                        <td>Performed By</td> 
                                        <td>Traceable to NIST</td>
                                        <td>Successful</td>
                                        <td>Decommission</td>
                                        <td>Decommission Code</td>                                        

                                    </tr>
                                    <tr>
                                        <td><asp:DropDownList runat="server" ID="PerformedBy" DataValueField="EmployeeID" DataTextField="Name"></asp:DropDownList></td>
                                        <td><asp:CheckBox runat="server" ID="NIST"></asp:CheckBox></td>
                                        <td><asp:CheckBox runat="server" ID="Success"></asp:CheckBox></td>
                                        <td><asp:CheckBox runat="server" ID="Decomm"></asp:CheckBox></td>
                                        <td><asp:DropDownList runat="server" ID="DecommCode" AppendDataBoundItems="true" DataValueField="DecommCodeID" DataTextField="DecommissionCode" >
                                            <asp:ListItem Selected="True" Text="None" Value="0"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        
                                    </tr>
                                    <tr><td><asp:Button runat="server" ID="CancelCompleteCal" Text="Cancel" CommandArgument="ExistView" CommandName="SwitchViewByID" OnCommand="CancelCompleteCal_Command" /></td><td><asp:Button runat="server" ID="AddNow" Text="Add" CommandArgument="ExistView" CommandName="SwitchViewByID" OnCommand="CompleteCalNow_Command" /></td></tr>
                                </table>                               
                        <br />
                           
                    </asp:View></asp:MultiView>
                    </ItemTemplate></asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [UpcomingCalibration] ORDER BY [CalibrationDue]"></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
   
  
        <asp:SqlDataSource ID="ClearInventory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" UpdateCommand="UPDATE Inventory SET Quantity=1 WHERE 1=0" SelectCommand="SELECT [DeliveryItemID], [InventoryID], [LotNumber], [Quantity], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [TotInvQty], [Status], [Location1], [PONumber], [CompanyName], [OrderQty], [InvStatusID] FROM [ClearInventory]"></asp:SqlDataSource>
              <asp:SqlDataSource ID="MonseesSqlDataSourceSubcontract" runat="server" 
        
       
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SubcontractItems WHERE HasDetail = 1" EnableCaching="False">

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