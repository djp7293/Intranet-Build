<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="ShippingReceiving.aspx.cs" Inherits="Monsees._Default2"%>

<asp:Content ContentPlaceHolderID="headContent" runat="server">

    <title>Material Orders</title>
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

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
     <div align="left">
    
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
                

                <td width="33%">
                   
                    
                </td>
                <td align="right" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
         
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   <asp:MultiView ID="ShipReceiveMultiView" runat="server" ActiveViewIndex="0">
                       <asp:View ID="Materials" runat="server">
                           <asp:Button ID="Button1" Text="Subcontract Outstanding" runat="server" CommandArgument="Subcontract" CommandName="SwitchViewByID" OnClick="Button1_Click"  />
                    <asp:Button ID="Button2" Text="Tooling/Supplies Outstanding" runat="server" CommandArgument="Toolingsupplies" CommandName="SwitchViewByID" OnClick="Button1_Click"  /><br />
                           <br/><b><u style="text-align: left">Outstanding Material Orders:<br /> </u></b>
                           <asp:GridView ID="MaterialGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" DataKeyNames="MatPriceID" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceMaterial" GridLines="Vertical" 
EmptyDataText="There is currently no material on order." EnableSortingAndPagingCallbacks="True"  
                               onrowcommand="MaterialGrid_RowCommand" PageSize="50" Width="100%" 
                               AllowSorting="True" 
                               onselectedindexchanged="MaterialGrid_SelectedIndexChanged">
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                                   <asp:BoundField DataField="MatPriceID" HeaderText="ID" 
                                       SortExpression="MatPriceID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobNumber" HeaderText="Job #" 
                                       SortExpression="JobNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" 
                                       SortExpression="MaterialPOID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ItemNum"  
                                       HeaderText="Item #" SortExpression="ItemNum">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="MaterialName" HeaderText="Material" 
                                       SortExpression="MaterialName">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Dimension" HeaderText="Dimension" 
                                       SortExpression="Dimension">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Size" HeaderText="Size" 
                                       SortExpression="Size">
				       <ItemStyle HorizontalAlign="Center" />
				   </asp:BoundField>
				   
                                   <asp:BoundField DataField="Length" HeaderText="Length" 
                                       SortExpression="Length">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Date" HeaderText="Order Date" SortExpression="Date" DataFormatString="{0:MM-dd-yyyy}">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate" DataFormatString="{0:MM-dd-yyyy}">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="VendorName" HeaderText="Vendor" 
                                       SortExpression="VendorName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="ContactName" HeaderText="Contact" 
                                       SortExpression="ContactName">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Shipping" HeaderText="Shipping" 
                                       SortExpression="Shipping">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:ButtonField CommandName="Received" Text="Received">
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
                                   <br />
                            <br />
                            <b><u style="text-align: left">Material Pending Certification:<br /> </u></b>
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EmptyDataText="There is currently no material needing certifications."  BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="ClearMatlCerts" EnableModelValidation="True" GridLines="Vertical" OnRowCommand="GridView2_RowCommand" style="text-align: left">
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
                       </asp:View>
                       <asp:View ID="Subcontract" runat="server">
                           <asp:Button ID="Button4" Text="Materials Outstanding" runat="server" CommandArgument="Materials" CommandName="SwitchViewByID" Onclick="Button1_Click" />
                    <asp:Button ID="ToolingTab" Text="Tooling/Supplies Outstanding" runat="server" CommandArgument="Toolingsupplies" CommandName="SwitchViewByID" Onclick="Button1_Click"  /><br />
                           <br /><b><u style="text-align: left">Outstanding Subcontract Orders:<br /> </u></b>
                           <asp:GridView ID="SubcontractGrid" runat="server" 
                               AutoGenerateColumns="False" BorderColor="#999999" 
                               BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourceSubcontract"  DataKeyNames="SubcontractItemID"
                               EmptyDataText="There are currently no parts out to subcontractors." 
                               EnableSortingAndPagingCallbacks="False" GridLines="Vertical" 
                               onrowcommand="SubcontractGrid_RowCommand" OnRowDataBound="SubcontractGrid_RowDataBound" PageSize="50" Width="100%" 
                               AllowPaging="True" AllowSorting="True">
                               <RowStyle Font-Size="Medium" ForeColor="Black" />
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
                                   <asp:HiddenField runat="server" ID="NewPart" Value='<%#Eval("NewPart") %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Eval("CAbbr") %>' />
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

                       </asp:View>
                        <asp:View ID="ToolingSupplies" runat="server">
                            <asp:Button ID="MaterialTab" Text="Materials Outstanding" runat="server" CommandArgument="Materials" CommandName="SwitchViewByID" Onclick="Button1_Click" />
                            <asp:Button ID="Button3" Text="Subcontract Outstanding" runat="server" CommandArgument="Subcontract" CommandName="SwitchViewByID" OnClick="Button1_Click" /><br />
                            <br /><b><u style="text-align: left">Outstanding Tooling/Supplies Orders:<br /> </u></b>
                            <asp:GridView ID="GridView3" runat="server" OnRowCommand="GridView3_RowCommand" DataKeyNames="SuppliesPOItemID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1" EnableModelValidation="True" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="SuppliesPONum" HeaderText="SuppliesPONum" SortExpression="SuppliesPONum" />
                                    <asp:BoundField DataField="VendorName" HeaderText="VendorName" SortExpression="VendorName" />
                                    <asp:BoundField DataField="LineItem" HeaderText="LineItem" SortExpression="LineItem" />
                                    <asp:BoundField DataField="ItemNum" HeaderText="ItemNum" SortExpression="ItemNum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                                    <asp:BoundField DataField="DueDate" HeaderText="DueDate" SortExpression="DueDate" />
                                    <asp:BoundField DataField="JobNumber" HeaderText="JobNumber" SortExpression="JobNumber" />
                                    <asp:BoundField DataField="LotNumber" HeaderText="LotNumber" SortExpression="LotNumber" />
                                    <asp:BoundField DataField="PartNumber" HeaderText="PartNumber" SortExpression="PartNumber" />
                                    <asp:BoundField DataField="DrawingNumber" HeaderText="DrawingNumber" SortExpression="DrawingNumber" />
                                    <asp:CheckBoxField DataField="Received" HeaderText="Received" SortExpression="Received" />
                                    <asp:ButtonField CommandName="Received" Text="Receive" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [SuppliesPOItemID], [SuppliesPONum], [VendorName], [LineItem], [ItemNum], [Description], [Notes], [DueDate], [JobNumber], [LotNumber], [Received], [PartNumber], [DrawingNumber] FROM [ToolingSupplyItems] WHERE ([Received] = @Received)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="false" Name="Received" Type="Boolean" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                        </asp:View>
                   </asp:MultiView>
                 </p>
            </asp:Panel>
        <asp:Panel ID="Panel1" runat="server">

    
            <br />
    <asp:SqlDataSource ID="ClearMatlCerts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [MaterialCertQueue]"></asp:SqlDataSource>
    
<b><u style="text-align: left">Material Pending Preparation:<br /> </u></b>
<asp:GridView ID="MaterialGrid2" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White"  DataKeyNames="MatPriceID"
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePrepare" OnRowDataBound="MaterialGrid2_RowDataBound" GridLines="Vertical" 
EmptyDataText="There is currently no material needing preparation." EnableSortingAndPagingCallbacks="False"  
                               onrowcommand="MaterialGrid2_RowCommand" PageSize="50" Width="100%"   
                               AllowSorting="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                                  
<asp:BoundField DataField="JobNumber" HeaderText="Job #" 
                                       SortExpression="JobNumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                                                     <asp:BoundField DataField="ProjectManager" HeaderText="PM" 
                                       SortExpression="ProjectManager">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="CAbbr" HeaderText="Cust." 
                                       SortExpression="CAbbr">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="PartNumber"  
                                       HeaderText="Part Number" SortExpression="PartNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>	
                                     
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
  <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>

<asp:BoundField DataField="MaterialName" HeaderText="Material" 
                                       SortExpression="MaterialName">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Dimension" HeaderText="Dimension" 
                                       SortExpression="Dimension">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Size" HeaderText="Size" 
                                       SortExpression="Size">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   <asp:BoundField DataField="StockCut" HeaderText="Cut" 
                                       SortExpression="Cut">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="PartsPercut" HeaderText="Parts Per" 
                                       SortExpression="Parts Per">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="Drill" HeaderText="Drill" 
                                       SortExpression="Drill">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="DrillSize" HeaderText="Drill Size" 
                                       SortExpression="DrillSize">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
<asp:BoundField DataField="EstTotalLength" HeaderText="Tot. Length" 
                                       SortExpression="EstTotalLength">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Description" HeaderText="Source" 
                                       SortExpression="Description">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="MatPriceID" HeaderText="Mat'l Lot ID" SortExpression="MatPriceID" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                                   <asp:BoundField DataField="LatestStart" HeaderText="Late Start" 
                                       SortExpression="LatestStart" DataFormatString="{0:MM-dd-yyyy}">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                    <asp:ButtonField CommandName="Morelabels" Text="More Labels">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>
                                   <asp:TemplateField HeaderText="Location"  HeaderStyle-HorizontalAlign="Center" itemStyle-Width="20px" HeaderStyle-Width="20px"> 

                <ItemTemplate> 
<div style="width: 40px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">             
<asp:TextBox ID="Loc" runat="server" Text='<%# Bind("Location") %>'></asp:TextBox> </div>
           </ItemTemplate> 
           </asp:TemplateField> 
                                   <asp:ButtonField CommandName="Prepared" Text="Prepared">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>

<asp:TemplateField>
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("Active Version") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
				   </asp:TemplateField>  
                                     <asp:TemplateField>
                              <ItemTemplate>
                                  <asp:HiddenField runat="server" ID="NewRenew" Value='<%#Eval("NewRenew") %>' />
                                  <asp:HiddenField runat="server" ID="Hot" Value='<%# Convert.ToString(Eval("Hot")) %>' />
                                  <asp:HiddenField runat="server" ID="NewPart" Value='<%#Eval("NewPart") %>' />
                                  <asp:HiddenField runat="server" ID="CAbbr" Value='<%# Convert.ToString(Eval("CAbbr")) %>' />
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
	</asp:Panel>
    
    </div>
    <asp:SqlDataSource ID="MonseesSqlDataSourceMaterial" runat="server" 
        
       
       ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From MaterialOrders2 WHERE Received = 0" EnableCaching="False">
    </asp:SqlDataSource>

     <br />
     <b><u style="text-align: left">Returned Stock Material:<br /> </u></b>
   
     <asp:GridView ID="GridView4" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="StockInventoryID" OnRowCommand="GridView4_RowCommand" DataSourceID="SqlDataSource2" GridLines="Vertical" AutoGenerateColumns="False" EnableModelValidation="True">
         <Columns>
             <asp:BoundField DataField="MatPriceID" HeaderText="ID" SortExpression="MatPriceID" />
             <asp:BoundField DataField="MaterialName" HeaderText="Grade" SortExpression="MaterialName" />
             <asp:BoundField DataField="State" HeaderText="Variant" SortExpression="State" />
             <asp:BoundField DataField="Type" HeaderText="Desc." SortExpression="Type" />
             <asp:BoundField DataField="Dimension" HeaderText="Dimension" SortExpression="Dimension" />
             <asp:BoundField DataField="Diameter" HeaderText="D" SortExpression="Diameter" />
             <asp:BoundField DataField="Height" HeaderText="H" SortExpression="Height" />
             <asp:BoundField DataField="Width" HeaderText="W" SortExpression="Width" />
             <asp:BoundField DataField="Length" HeaderText="L" SortExpression="Length" />
             <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
             <asp:BoundField DataField="pct" HeaderText="Pct." SortExpression="pct" />
             <asp:BoundField DataField="location" HeaderText="Loc." SortExpression="location" />
             <asp:ButtonField CommandName="Labels" Text="Print Label(s)" />
             <asp:ButtonField CommandName="Clear" Text="Clear" />
         </Columns>         
         <AlternatingRowStyle BackColor="#DCDCDC" />
         <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
         <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
         <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
         <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
     </asp:GridView>
     </b>

     <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        
       
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From ReturnedMaterialStock" EnableCaching="False" UpdateCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

UPDATE [StockInventory] SET clear = 1 WHERE StockInventoryID = @StockInventoryID">
<updateparameters>
                
		<asp:parameter name="StockInventoryID" type="String" ConvertEmptyStringToNull = true />
</updateparameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="MonseesSqlDataSourceSubcontract" runat="server" 
        
       
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SubcontractItems WHERE HasDetail = 1"  EnableCaching="False" >

    </asp:SqlDataSource>
 <asp:SqlDataSource ID="MonseesSqlDataSourcePrepare" OnSelecting="MonseesSqlDataSourcePrepare_Selecting" runat="server" 
        
       
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From PurchMatlPrepare ORDER BY LateStartSort"  EnableCaching="False" 

UpdateCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

UPDATE [Job Item] SET Prepared = 1, Location = @Location WHERE JobItemID = @JobItemID">
<updateparameters>
                <asp:parameter name="Location" type="String" ConvertEmptyStringToNull = true />
		<asp:parameter name="JobItemID" type="String" ConvertEmptyStringToNull = true />
</updateparameters>
    </asp:SqlDataSource>
</asp:Content>
 
 