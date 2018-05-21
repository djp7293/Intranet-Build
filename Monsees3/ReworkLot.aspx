<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReworkLot.aspx.cs" Inherits="Monsees.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Rework Lot</title>
    <h1>Rework Lot # 1</h1>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <br />
        Print this record before scrapping or assigning to rework so that parts can be located and collected.
    <div style="width:7.5in"><table><tr><td>
         <br/>
         <span style="text-decoration: underline;"><strong>
     Lot Inventory:</strong></span><br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1" DataKeyNames="" EnableModelValidation="True" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:BoundField DataField="Location1" HeaderText="Location" SortExpression="Location1" />
                    <asp:BoundField DataField="Note1" HeaderText="Note" SortExpression="Note1" />
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                           <asp:CheckBox ID="InventoryRework" Checked="true" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selected Qty"  HeaderStyle-HorizontalAlign="Left"> 		
                            <ItemTemplate>                         
							                    <asp:TextBox ID="InvQtySelected" runat="server" Text='<%# Bind("Quantity") %>'  style="width:50px"  ></asp:TextBox> 					
		                    </ItemTemplate>
                       </asp:TemplateField> 
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </td></tr><tr>
        <td>
              <br />
         <span style="text-decoration: underline;"><strong>
     Lot Deliveries:</strong></span><br />
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="DeliveryItemID" DataSourceID="SqlDataSource2" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="DeliveryItemID" HeaderText="ID" ReadOnly="True" SortExpression="DeliveryItemID" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                    
                    <asp:CheckBoxField DataField="RTS" HeaderText="RTS" SortExpression="RTS" />
                    <asp:BoundField DataField="CurrDelivery" HeaderText="Date" SortExpression="CurrDelivery" />
                    <asp:CheckBoxField DataField="Shipped" HeaderText="Shipped" SortExpression="Shipped" />
                    <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" SortExpression="ShipDate" />
                    <asp:CheckBoxField DataField="Return" HeaderText="Return" SortExpression="Return" />
                    <asp:BoundField DataField="RMANum" HeaderText="RMA #" SortExpression="RMA#" />
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                           <asp:CheckBox ID="DeliveryRework" Checked="true" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selected Qty"  HeaderStyle-HorizontalAlign="Left"> 		
                            <ItemTemplate>                         
							                    <asp:TextBox ID="DeliveryQtySelected" runat="server" Text='<%# Bind("Quantity") %>' style="width:50px" ></asp:TextBox> 					
		                    </ItemTemplate>
                       </asp:TemplateField> 
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            </asp:GridView>


            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [DelivryItemSummary] WHERE ([LotNumber] = @LotNumber)">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="LotNumber" SessionField="ID" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>

        </td>
           </tr></table></div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand = "SELECT [Quantity], [Status], [Location1], [Note1], InventoryID FROM [Inventory] WHERE LotNumber=@LotNumber And Quantity>0">
        <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="LotNumber" SessionField="ID" Type="String" />
                </SelectParameters>
    </asp:SqlDataSource>
    
    <br />
    <asp:Button ID="Button1" runat="server" Text="Scrap Selected Lines" OnClick="Button1_Click" />
&nbsp;
    <asp:Button ID="Button2" runat="server" Text="Rework Selected Lines" OnClick="Button2_Click" />
    
</asp:Content>
