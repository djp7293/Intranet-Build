<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CARLibrary.aspx.cs" Inherits="Monsees.CARLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>CAR Library</h1></br>
    <div>
    <asp:GridView ID="CARView" runat="server" AutoGenerateColumns="False" OnRowCommand="CARView_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDataSource1" EnableModelValidation="True" GridLines="Vertical" style="margin-right: 2px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            
            <asp:BoundField DataField="CARID" HeaderText="ID #" SortExpression="MatPriceID" />
            <asp:BoundField DataField="JobItemID" HeaderText="Lot #" SortExpression="MaterialPOID" />
            <asp:BoundField DataField="PartNumber" HeaderText="Part #" SortExpression="VendorName" />
            <asp:BoundField DataField="Revision Number" HeaderText="Rev" SortExpression="Date" DataFormatString="{0:MM-dd-yyyy}" />
            <asp:BoundField DataField="DrawingNumber" HeaderText="Description" SortExpression="MaterialName" />
            <asp:CheckBoxField DataField="CustomerCAR" HeaderText="Is Cust. CAR" />
            <asp:BoundField DataField="CustomerCARNum" HeaderText="Cust. CAR #" SortExpression="State" />
            <asp:BoundField DataField="InitiationDate" HeaderText="Init. Date" SortExpression="Dimension" />
            <asp:BoundField DataField="DueDate" HeaderText="Due" SortExpression="Diameter" />
            <asp:BoundField DataField="Definition" HeaderText="Issue" SortExpression="Height" />
            <asp:BoundField DataField="RootCause" HeaderText="Root Cause" SortExpression="Width" />
            <asp:ButtonField CommandName="ViewCAR" Text="View" />
            
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [CorrectiveActionView]"></asp:SqlDataSource>
    </div>
</asp:Content>
