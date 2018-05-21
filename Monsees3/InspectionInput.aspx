<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectionInput.aspx.cs" Inherits="Monsees.InspectionInput" MasterPageFile="~/MasterPages/Monsees.master" %>
<%@ Import Namespace="Monsees.Security" %>

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

    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="48px" Width="257px">
        <asp:ListItem Text="All" Value="1"></asp:ListItem>
        <asp:ListItem Text="Active" Value="2"></asp:ListItem>
        <asp:ListItem Text="Inactive" Value="3"></asp:ListItem>
        <asp:ListItem Text="To-Do" Value="4"></asp:ListItem>
    </asp:RadioButtonList>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="RevisionID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateSelectButton="True" Width="445px">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" />
            <asp:BoundField DataField="Part #" HeaderText="Part #" SortExpression="Part #" />
            <asp:BoundField DataField="Rev #" HeaderText="Rev #" SortExpression="Rev #" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Rpt" HeaderText="Rpt" ReadOnly="True" SortExpression="Rpt" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
</asp:GridView>
   
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="RevisionID" DataSourceID="SqlDataSource2" EnableModelValidation="True" GroupItemCount="3">
        <AlternatingItemTemplate>
            <td runat="server" style="background-color: #FFFFFF;color: #284775;">RevisionID:
                <asp:Label ID="RevisionIDLabel" runat="server" Text='<%# Eval("RevisionID") %>' />
                <br />DetailID:
                <asp:Label ID="DetailIDLabel" runat="server" Text='<%# Eval("DetailID") %>' />
                <br />PartNumber:
                <asp:Label ID="PartNumberLabel" runat="server" Text='<%# Eval("PartNumber") %>' />
                <br />Revision_Number:
                <asp:Label ID="Revision_NumberLabel" runat="server" Text='<%# Eval("Revision_Number") %>' />
                <br />DrawingNumber:
                <asp:Label ID="DrawingNumberLabel" runat="server" Text='<%# Eval("DrawingNumber") %>' />
                <br />CompanyName:
                <asp:Label ID="CompanyNameLabel" runat="server" Text='<%# Eval("CompanyName") %>' />
                <br />PlatingID:
                <asp:Label ID="PlatingIDLabel" runat="server" Text='<%# Eval("PlatingID") %>' />
                <br />
                <asp:CheckBox ID="MetricCheckBox" runat="server" Checked='<%# Eval("Metric") %>' Enabled="false" Text="Metric" />
                <br />StandardTolerance:
                <asp:Label ID="StandardToleranceLabel" runat="server" Text='<%# Eval("StandardTolerance") %>' />
                <br /></td>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <td runat="server" style="background-color: #999999;">RevisionID:
                <asp:Label ID="RevisionIDLabel1" runat="server" Text='<%# Eval("RevisionID") %>' />
                <br />DetailID:
                <asp:TextBox ID="DetailIDTextBox" runat="server" Text='<%# Bind("DetailID") %>' />
                <br />PartNumber:
                <asp:TextBox ID="PartNumberTextBox" runat="server" Text='<%# Bind("PartNumber") %>' />
                <br />Revision_Number:
                <asp:TextBox ID="Revision_NumberTextBox" runat="server" Text='<%# Bind("Revision_Number") %>' />
                <br />DrawingNumber:
                <asp:TextBox ID="DrawingNumberTextBox" runat="server" Text='<%# Bind("DrawingNumber") %>' />
                <br />CompanyName:
                <asp:TextBox ID="CompanyNameTextBox" runat="server" Text='<%# Bind("CompanyName") %>' />
                <br />PlatingID:
                <asp:TextBox ID="PlatingIDTextBox" runat="server" Text='<%# Bind("PlatingID") %>' />
                <br />
                <asp:CheckBox ID="MetricCheckBox" runat="server" Checked='<%# Bind("Metric") %>' Text="Metric" />
                <br />StandardTolerance:
                <asp:TextBox ID="StandardToleranceTextBox" runat="server" Text='<%# Bind("StandardTolerance") %>' />
                <br />
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                <br />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                <br /></td>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <EmptyItemTemplate>
<td runat="server" />
        </EmptyItemTemplate>
        <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <InsertItemTemplate>
            <td runat="server" style="">RevisionID:
                <asp:TextBox ID="RevisionIDTextBox" runat="server" Text='<%# Bind("RevisionID") %>' />
                <br />DetailID:
                <asp:TextBox ID="DetailIDTextBox" runat="server" Text='<%# Bind("DetailID") %>' />
                <br />PartNumber:
                <asp:TextBox ID="PartNumberTextBox" runat="server" Text='<%# Bind("PartNumber") %>' />
                <br />Revision_Number:
                <asp:TextBox ID="Revision_NumberTextBox" runat="server" Text='<%# Bind("Revision_Number") %>' />
                <br />DrawingNumber:
                <asp:TextBox ID="DrawingNumberTextBox" runat="server" Text='<%# Bind("DrawingNumber") %>' />
                <br />CompanyName:
                <asp:TextBox ID="CompanyNameTextBox" runat="server" Text='<%# Bind("CompanyName") %>' />
                <br />PlatingID:
                <asp:TextBox ID="PlatingIDTextBox" runat="server" Text='<%# Bind("PlatingID") %>' />
                <br />
                <asp:CheckBox ID="MetricCheckBox" runat="server" Checked='<%# Bind("Metric") %>' Text="Metric" />
                <br />StandardTolerance:
                <asp:TextBox ID="StandardToleranceTextBox" runat="server" Text='<%# Bind("StandardTolerance") %>' />
                <br />
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                <br />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                <br /></td>
        </InsertItemTemplate>
        <ItemTemplate>
            <td runat="server" style="background-color: #E0FFFF;color: #333333;">RevisionID:
                <asp:Label ID="RevisionIDLabel" runat="server" Text='<%# Eval("RevisionID") %>' />
                <br />DetailID:
                <asp:Label ID="DetailIDLabel" runat="server" Text='<%# Eval("DetailID") %>' />
                <br />PartNumber:
                <asp:Label ID="PartNumberLabel" runat="server" Text='<%# Eval("PartNumber") %>' />
                <br />Revision_Number:
                <asp:Label ID="Revision_NumberLabel" runat="server" Text='<%# Eval("Revision_Number") %>' />
                <br />DrawingNumber:
                <asp:Label ID="DrawingNumberLabel" runat="server" Text='<%# Eval("DrawingNumber") %>' />
                <br />CompanyName:
                <asp:Label ID="CompanyNameLabel" runat="server" Text='<%# Eval("CompanyName") %>' />
                <br />PlatingID:
                <asp:Label ID="PlatingIDLabel" runat="server" Text='<%# Eval("PlatingID") %>' />
                <br />
                <asp:CheckBox ID="MetricCheckBox" runat="server" Checked='<%# Eval("Metric") %>' Enabled="false" Text="Metric" />
                <br />StandardTolerance:
                <asp:Label ID="StandardToleranceLabel" runat="server" Text='<%# Eval("StandardTolerance") %>' />
                <br /></td>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="groupPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr id="groupPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #5D7B9D;font-family: Verdana, Arial, Helvetica, sans-serif;color: #FFFFFF"></td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <td runat="server" style="background-color: #E2DED6;font-weight: bold;color: #333333;">RevisionID:
                <asp:Label ID="RevisionIDLabel" runat="server" Text='<%# Eval("RevisionID") %>' />
                <br />DetailID:
                <asp:Label ID="DetailIDLabel" runat="server" Text='<%# Eval("DetailID") %>' />
                <br />PartNumber:
                <asp:Label ID="PartNumberLabel" runat="server" Text='<%# Eval("PartNumber") %>' />
                <br />Revision_Number:
                <asp:Label ID="Revision_NumberLabel" runat="server" Text='<%# Eval("Revision_Number") %>' />
                <br />DrawingNumber:
                <asp:Label ID="DrawingNumberLabel" runat="server" Text='<%# Eval("DrawingNumber") %>' />
                <br />CompanyName:
                <asp:Label ID="CompanyNameLabel" runat="server" Text='<%# Eval("CompanyName") %>' />
                <br />PlatingID:
                <asp:Label ID="PlatingIDLabel" runat="server" Text='<%# Eval("PlatingID") %>' />
                <br />
                <asp:CheckBox ID="MetricCheckBox" runat="server" Checked='<%# Eval("Metric") %>' Enabled="false" Text="Metric" />
                <br />StandardTolerance:
                <asp:Label ID="StandardToleranceLabel" runat="server" Text='<%# Eval("StandardTolerance") %>' />
                <br /></td>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [RevisionID], [DetailID], [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [CompanyName], [PlatingID], [Metric], [StandardTolerance] FROM [VersionwDetailInfo] WHERE ([RevisionID] = @RevisionID)">
        <SelectParameters>
            <asp:ControlParameter ControlID="GridView1" Name="RevisionID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
   
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [BlankInspectionList] ORDER BY [Company], [Part #]"></asp:SqlDataSource>
   
</asp:Content>