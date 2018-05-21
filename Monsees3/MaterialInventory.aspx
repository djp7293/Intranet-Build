<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MaterialInventory.aspx.cs" Inherits="Monsees.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Material Inventory</h1></br>
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1" EnableModelValidation="True" GridLines="Vertical" style="margin-right: 2px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            
            <asp:BoundField DataField="MatPriceID" HeaderText="ID #" SortExpression="MatPriceID" />
            <asp:BoundField DataField="MaterialPOID" HeaderText="PO #" SortExpression="MaterialPOID" />
            <asp:BoundField DataField="VendorName" HeaderText="Vendor" SortExpression="VendorName" />
            <asp:BoundField DataField="Date" HeaderText="Order Date" SortExpression="Date" DataFormatString="{0:MM-dd-yyyy}" />
            <asp:BoundField DataField="MaterialName" HeaderText="Grade" SortExpression="MaterialName" />
            <asp:BoundField DataField="Type" HeaderText="Material" SortExpression="Type" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="Dimension" HeaderText="Dimension" SortExpression="Dimension" />
            <asp:BoundField DataField="Diameter" HeaderText="Diameter" SortExpression="Diameter" />
            <asp:BoundField DataField="Height" HeaderText="Height" SortExpression="Height" />
            <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
            <asp:BoundField DataField="Length" HeaderText="Length" SortExpression="Length" />
            <asp:BoundField DataField="pct" HeaderText="% Remaining" SortExpression="pct" />
            <asp:CheckBoxField DataField="IsCert" SortExpression="IsCert" ItemStyle-Width="3%" FooterStyle-CssClass="noprint" HeaderStyle-CssClass="noprint" ItemStyle-CssClass="noprint" />
                                   
            <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("MatPriceID") %>' Text="Certification"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>	
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [MaterialInventoryList]"></asp:SqlDataSource>
    </div>
</asp:Content>
