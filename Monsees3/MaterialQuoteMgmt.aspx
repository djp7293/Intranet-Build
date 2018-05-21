<%@ Page Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="MaterialQuoteMgmt.aspx.cs" Inherits="Monsees._Default_MatlQuoteMgmt"%>

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
              

	

    </div>
    <br /><b><u>Material Quote Request Queue:</u></b><br />
    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="MatQueueID" DataSourceID="MatlQuoteQueue" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="JobItemID" />
            <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="PartNumber" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="DrawingNumber" />
            <asp:BoundField DataField="MaterialName" HeaderText="Grade" ReadOnly="True" SortExpression="MaterialName" />
            <asp:BoundField DataField="Type" HeaderText="Material" SortExpression="Type" />
            <asp:BoundField DataField="Dimension" HeaderText="Dimension" SortExpression="Dimension" />
            <asp:BoundField DataField="Size" HeaderText="Size" ReadOnly="True" SortExpression="Size" />
            <asp:BoundField DataField="Length" HeaderText="Len." ReadOnly="True" SortExpression="Length" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
            <asp:CheckBoxField DataField="Cut" HeaderText="Cut" SortExpression="Cut" />
            <asp:CheckBoxField DataField="MatlCertReqd" HeaderText="Cert Reqd" SortExpression="MatlCertReqd" />
            <asp:BoundField DataField="VendorName" HeaderText="Sugg. Vendor" SortExpression="VendorName" />
            <asp:CheckBoxField DataField="SelectforQuote" HeaderText="Select" SortExpression="SelectforQuote" />
            <asp:CheckBoxField DataField="LineGroup" HeaderText="Group" SortExpression="LineGroup" />
            <asp:CheckBoxField DataField="Ordered" HeaderText="Ordered" SortExpression="Ordered" />
             <asp:BoundField DataField="MatQuoteID" HeaderText="Quote #" SortExpression="MatQuoteID" />
            <asp:CheckBoxField DataField="ReqdApproval" HeaderText="Approval Reqd." SortExpression="ReqdApproval" />
            <asp:CheckBoxField DataField="OrderPending" HeaderText="Order Pending" ReadOnly="True" SortExpression="OrderPending" />
            <asp:ButtonField Text="View Quote" CommandName="Quote" />  
            <asp:ButtonField Text="View Order" CommandName="Order" /> 
            <asp:ButtonField Text="Remove" CommandName="Remove" />           
            
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Size="Small" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="MatlQuoteQueue" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [MatQueueID], [PartNumber], [DrawingNumber], [MaterialName], [Dimension], [Length], [Quantity], [Cut], [SelectforQuote], [Ordered], [LineGroup], [ReqdApproval], [OrderPending], [MatlCertReqd], [Type], [Size], [MatQuoteID], [VendorName], [JobNumber], [JobItemID] FROM [MatQuoteQueue]"></asp:SqlDataSource>
    <br /><b><u>Open Material Quote Requests:</u></b><br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="MatQuoteID" DataSourceID="OpenQuotes" EnableModelValidation="True" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField><ItemTemplate>
            <a href="JavaScript:divexpandcollapse('div<%# Eval("MatQuoteID") %>');">
                            +</a>  
                        
					</ItemTemplate>

				   </asp:TemplateField>
            <asp:BoundField DataField="MatQuoteID" HeaderText="Quote #" ReadOnly="True" SortExpression="MatQuoteID" />
            <asp:BoundField DataField="Created" HeaderText="Created" SortExpression="Created"  DataFormatString="{0:MM-dd-yyyy}" />
            <asp:BoundField DataField="Due" HeaderText="Due" SortExpression="Due" DataFormatString="{0:MM-dd-yyyy}" />
            <asp:BoundField DataField="Owner" HeaderText="Owner" SortExpression="Owner" />
            <asp:BoundField DataField="Delvry Req" HeaderText="Delvry Req" SortExpression="Delvry Req" />
            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />
            <asp:CheckBoxField DataField="MaxOfOrdered" HeaderText="Ordered" ReadOnly="True" SortExpression="MaxOfOrdered" />
            <asp:BoundField DataField="CountOfMatQuoteLineID" HeaderText="Line Count" SortExpression="CountOfMatQuoteLineID" />
            <asp:TemplateField>
                <ItemTemplate><tr><td></td><td colspan="9">
                    <div id="div<%# Eval("MatQuoteID") %>"  style="display:none; left: 15px;">  <br />
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="MatQuoteID" EnableModelValidation="True" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField DataField="Vendor" HeaderText="Vendor" SortExpression="Vendor" />
                            <asp:BoundField DataField="Line" HeaderText="Line" SortExpression="Line" />
                            <asp:BoundField DataField="Material" HeaderText="Material" SortExpression="Material" />
                            <asp:BoundField DataField="Dimension" HeaderText="Dimension" SortExpression="Dimension" />
                            <asp:BoundField DataField="D" HeaderText="D" SortExpression="D" />
                            <asp:BoundField DataField="H" HeaderText="H" SortExpression="H" />
                            <asp:BoundField DataField="W" HeaderText="W" SortExpression="W" />
                            <asp:BoundField DataField="L" HeaderText="L" SortExpression="L" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                            <asp:BoundField DataField="Each" HeaderText="Each" SortExpression="Each"  DataFormatString="${0:#,0}"/>
                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total"  DataFormatString="${0:#,0}"/>
                            <asp:BoundField DataField="Ship_Chg" HeaderText="Ship_Chg" SortExpression="Ship_Chg"  DataFormatString="${0:#,0}"/>
                            <asp:BoundField DataField="Delivery" HeaderText="Delivery" SortExpression="Delivery" DataFormatString="{0:MM-dd-yyyy}"/>
                            <asp:BoundField DataField="Note" HeaderText="Note" SortExpression="Note" />          
                            <asp:CheckBoxField DataField="Chosen" HeaderText="Chosen" SortExpression="Chosen" />
                            <asp:CheckBoxField DataField="Ordered" HeaderText="Ordered" SortExpression="Ordered" />
                            
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:GridView><br /></td></tr></div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="OpenQuotes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [OpenMatlQuotes]"></asp:SqlDataSource>
    <br />

   
    <asp:SqlDataSource ID="QuoteRespForm" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

      <b><u>Material Orders Requiring Approval:</u></b><br />    
    <asp:GridView ID="MaterialOrders" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical" AutoGenerateColumns="False" DataKeyNames="MaterialPOID" DataSourceID="ClearMatlOrders" OnRowCommand="MaterialOrders_RowCommand">
        <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
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
                <asp:ButtonField CommandName="Reject" Text="Reject" >
                                       <ControlStyle Font-Size="Small" /></asp:ButtonField>
            </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Font-Size="Small" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Small" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>

    <asp:SqlDataSource ID="ClearMatlOrders" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [MaterialPOID], [Name], [VendorName], [DueDate], [Notes], [Contact], [ConfirmationNum], [ShippingCharge] FROM [ClearMaterialOrders]"></asp:SqlDataSource>

    <br />
    <b><u>Material Orders Pending Delivery:</u></b><asp:GridView ID="MaterialGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MaterialOrdersPending" GridLines="Vertical" 
EmptyDataText="There is currently no material on order." EnableSortingAndPagingCallbacks="True"  
                               onrowcommand="MaterialGrid_RowCommand" PageSize="50" Width="100%" 
                               AllowSorting="True" EnableModelValidation="True" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
                             <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" SortExpression="MaterialPOID" />
                                   <asp:BoundField DataField="Date" HeaderText="Date" 
                                       SortExpression="Date" DataFormatString="{0:MM-dd-yyyy}" >
                                   </asp:BoundField>
				   <asp:BoundField DataField="cost" HeaderText="cost" 
                                       SortExpression="Cost" DataFormatString="${0:#,0}">
                                   </asp:BoundField>
                                   <asp:BoundField DataField="MaterialName"  
                                       HeaderText="Material" SortExpression="MaterialName">
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Dimension" HeaderText="Dimension" 
                                       SortExpression="Dimension">
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Size" HeaderText="Size" ReadOnly="True" SortExpression="Size" />
                                   <asp:BoundField DataField="Length" HeaderText="Length" 
                                       SortExpression="Length">
                                   </asp:BoundField>
                                   <asp:BoundField DataField="quantity" HeaderText="Qty" 
                                       SortExpression="quantity">
                                   </asp:BoundField>
                                   <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" 
                                       SortExpression="VendorName">
                                   </asp:BoundField>
                                   <asp:CheckBoxField DataField="received" HeaderText="Rec'd" SortExpression="received" />
                                   
                                   
                                   <asp:BoundField DataField="DueDate" HeaderText="Due Date" SortExpression="DueDate" DataFormatString="{0:MM-dd-yyyy}"  />
                                   <asp:BoundField DataField="ItemNum" HeaderText="Item #" SortExpression="ItemNum" />
                                   <asp:BoundField DataField="Shipping" HeaderText="Shipping" SortExpression="Shipping" />
                                   <asp:BoundField DataField="ShippingCharge" HeaderText="Ship Charge" SortExpression="ShippingCharge"  DataFormatString="${0:#,0}" />
                                   <asp:BoundField DataField="ConfirmationNum" HeaderText="Conf Num" SortExpression="ConfirmationNum" />
                                   <asp:BoundField DataField="ContactName" HeaderText="Contact" SortExpression="ContactName" />
                                   <asp:BoundField DataField="JobNumber" HeaderText="Job #" SortExpression="JobNumber" />
                                   <asp:BoundField DataField="PostedToQB" HeaderText="Posted To QB" SortExpression="PostedToQB" />
                                   <asp:BoundField DataField="MinOfMatlCertReqd" HeaderText="Cert Reqd" SortExpression="MinOfMatlCertReqd" />
                                  
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                       
    <asp:SqlDataSource ID="MaterialOrdersPending" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [MaterialOrders2] WHERE received=0"></asp:SqlDataSource>
     

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