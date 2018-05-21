<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="SubcontractPO.aspx.cs" Inherits="Monsees.SubcontractPO" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
       <title>Subcontract Purchase Order</title>
    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    
    <style type="text/css">

body {
    
    font-size: small;
    text-align: justify;
}        

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

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <asp:MultiView runat="server" ID="CreateViewMulti" EnableViewState="true" ActiveViewIndex="0">
        <asp:View runat="server" ID="ViewEditHeader">
            <asp:ListView ID="ListView2" runat="server" DataKeyNames="SubcontractID" EnableModelValidation="True">
                       
                      
                       <EmptyDataTemplate>
                           <span>No data was returned.</span>
                       </EmptyDataTemplate>
                       
                       <ItemTemplate>
                           <table style="width: 800px"><tr><td style="width: 25%">
                           <span style="">PO #:
                           <asp:Label ID="SubcontractIDLabel" runat="server" Text='<%# Eval("SubcontractID") %>' />-M
                           </td><td style="width: 25%">
                           Due Date:
                           <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate") %>' />
                           </td><td style="width: 50%" colspan="2">
                           Description:
                           <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                               
                           
                           <br />
                           </td></tr><tr><td colspan="2">
                           Vendor Name:
                           <asp:Label ID="VendorNameLabel" runat="server" Text='<%# Eval("VendorName") %>' />
                           
                           
                           </td><td colspan="2">
                            Notes:
                           <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Note") %>' />
                           
                           </td></tr><tr><td>
                            Buyer:
                           <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                           
                           </td><td>
                          Ship Charge:
                           <asp:Label ID="ShipMethodLabel" runat="server" Text='<%# Eval("ShipMethod") %>' />
                           </td><td>
                           Received:
                           <asp:Label ID="IssueDateLabel" runat="server" Text='<%# Eval("IssueDate") %>' />
                           </td><td>
                           Total:
                           <asp:Label ID="TotalLabel" runat="server" Text='<%# Eval("Total") %>' />
                           </td></tr></table>
                        <br /></span>
                       </ItemTemplate>
                       <LayoutTemplate>
                           <div id="itemPlaceholderContainer" runat="server" style="">
                               <span runat="server" id="itemPlaceholder" />
                           </div>
                           <div style="">
                           </div>
                       </LayoutTemplate>
                       
                   </asp:ListView>
        </asp:View>
        <asp:View runat="server" ID="CreateNewHeader">
            <span style="font-size: large;"><B>Initiate New Subcontract Purchase Order:</B></span><br /><br />
            <asp:FormView ID="FormView1" runat="server" OnDataBound="FormView1_DataBound" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                <ItemTemplate>
                    <table style="width: 1000px"><tr>                           
                           <td colspan="2">
                           Vendor:
                           <asp:DropDownList ID="VendorNameList" runat="server" DataTextField="VendorName" DataValueField="SubcontractID" Width="350px" AppendDataBoundItems="true" >
                               <asp:ListItem Selected="True" Text="None Selected" Value="0" Enabled="true"></asp:ListItem></asp:DropDownList>
                           
                           
                           </td><td>Due:
                           <bdp:BDPLite ID="DueDateBox" runat="server" />
                           </td>></tr><tr><td colspan="3">
                           Description:
                           <asp:TextBox ID="DescriptionBox" runat="server" Width="400px"  />
                               
                           
                           <br />
                           </td></tr><tr><td colspan="3">
                            Notes:
                           <asp:TextBox ID="NotesBox" runat="server" Width="400px" />
                           
                           </td></tr><tr><td>
                            Buyer:
                           <asp:DropDownList ID="NameList" runat="server" DataValueField="EmployeeID" DataTextField="Name" Width="250px" AppendDataBoundItems="true" >
                               <asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem>
                           </asp:DropDownList>                           
                           </td><td>
                          Ship Method:
                           <asp:DropDownList ID="ShipMethodList" runat="server" DataTextField="Name" DataValueField="ShipMethodID" Width="200px" AppendDataBoundItems="true" >
                               <asp:ListItem Enabled="true" Selected ="True" Text="None Selected" Value="0"></asp:ListItem>
                           </asp:DropDownList>
                           </td><td>
                           
                           </td></tr></table>
                </ItemTemplate>
                <EditRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />

            </asp:FormView><br />
            No Of Records : <asp:TextBox ID="txtNo" runat="server" width="30px">  </asp:TextBox><asp:Button runat="server" ID="InitiateOrder" OnClick="InitiateOrder_Click" Text="Intiate Order" />
        </asp:View>
    </asp:MultiView>
                   
                   <asp:SqlDataSource ID="SubPurchOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

                   <br />
                   <asp:MultiView runat="server" ID="CreateItemsViewMulti" ActiveViewIndex="0" EnableViewState="true">
                       <asp:View runat="server" ID="ViewEditView" >
                           <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateEditButton="true" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="SubcontractItemID" EnableModelValidation="True" GridLines="Vertical">
                               <AlternatingRowStyle BackColor="#DCDCDC" />
                               <Columns>
                                   <asp:TemplateField HeaderText="Line #" SortExpression="LineItem" >
                                       <ItemTemplate>
                                           <asp:Label ID="LineItemLbl" runat="server" Text='<%# Eval("LineItem") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>                                           
                                           <asp:TextBox ID="LineItemBox" runat="server" Text='<%# Eval("LineItem") %>' Width="50px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Work Code" SortExpression="WorkCode" >
                                       <ItemTemplate>
                                           <asp:Label ID="WorkCodeLbl" runat="server" Text='<%# Eval("WorkCode") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hfworkcode" runat="server" Value='<%# Eval("WorkCodeID") %>'/>
                                           <asp:DropDownList ID="WorkCodeList" runat="server" DataTextField="WorkCode" DataValueField="WorkCodeID" Width="150px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty" SortExpression="Quantity" >
                                       <ItemTemplate>
                                           <asp:Label ID="QtyLbl" runat="server" Text='<%# Eval("Quantity") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="QtyBox" runat="server" Text='<%# Eval("Quantity") %>' Width="32px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Each" SortExpression="Each" >
                                       <ItemTemplate>
                                           <asp:Label ID="EachLbl" runat="server" Text='<%# Eval("Each") %>' DataFormatString="${0:#,0}" />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="EachBox" runat="server" Text='<%# Eval("Each") %>' Width="60px" AutoPostBack="true" OnTextChanged="EachBox_TextChanged" DataFormatString="${0:#,0}"/>
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="Total" >
                                       <ItemTemplate>
                                           <asp:Label ID="TotalLbl" runat="server" Text='<%# Eval("Total") %>' DataFormatString="${0:#,0}" />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="TotalBox" AutoPostBack="true" OnTextChanged="TotalBox_TextChanged" runat="server" Text='<%# Eval("Total") %>' Width="60px"   DataFormatString="${0:#,0}"/>
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate" >
                                       <ItemTemplate>
                                           <asp:Label ID="DueDateLbl" runat="server" Text='<%# Eval("DueDate") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hfDate" runat="server" Value='<%# Eval("DueDate") %>' />
                                           <bdp:BDPLite ID="DueDateBox" runat="server" Width="75px" ></bdp:BDPLite>
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Notes" SortExpression="Notes" >
                                       <ItemTemplate>
                                           <asp:Label ID="NotesLbl" runat="server" Text='<%# Eval("Notes") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="NotesBox" runat="server" Text='<%# Eval("Notes") %>' Width="300px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Monsees Lot" SortExpression="LotDescription" >
                                       <ItemTemplate>
                                           <asp:Label ID="LotLbl" runat="server" Text='<%# Eval("LotDescription") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hflot" runat="server" Value='<%# Eval("JobItemID") %>'/>
                                           <asp:DropDownList ID="LotList" runat="server" DataTextField="LotDescription" DataValueField="JobItemID" Width="400px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Date Sent" SortExpression="DateSent" >
                                       <ItemTemplate>
                                           <asp:Label ID="DateSentLbl" runat="server" Text='<%# Eval("DateSent") %>' />
                                       </ItemTemplate>
                                   </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="Date Returned" SortExpression="DateReturned" >
                                       <ItemTemplate>
                                           <asp:Label ID="DateReturnedLbl" runat="server" Text='<%# Eval("DateReturned") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hfDateReturned" runat="server" Value='<%# Eval("DateReturned") %>' />
                                           <bdp:BDPLite ID="DateReturnedBox" runat="server" Width="75px" ></bdp:BDPLite>
                                       </EditItemTemplate>  
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Cert Reqd" SortExpression="PlateCertReqd" >
                                       <ItemTemplate>
                                           <asp:Checkbox ID="CertCheck" runat="server" Checked='<%# Eval("PlateCertReqd") %>' Enabled="false" />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:Checkbox ID="CertCheckEdit" runat="server" Checked='<%# Eval("PlateCertReqd") %>' Enabled="true" />
                                       </EditItemTemplate>                                       
                                   </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Waiting" SortExpression="HasDetail">
                                       <ItemTemplate>
                                           <asp:CheckBox ID="RecdCheck" runat="server" Checked='<%# Eval("HasDetail") %>' Enabled="false" />
                                       </ItemTemplate>                                      
                                   </asp:TemplateField>                            
                                                                     
                                  
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                           </asp:GridView>
                       </asp:View>
                       <asp:View runat="server" ID="CreateItemsView">
                           <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                               <AlternatingRowStyle BackColor="#DCDCDC" />
                               <Columns>
                                   <asp:TemplateField HeaderText="Line #" SortExpression="LineItem" >
                                       
                                       <ItemTemplate>                                           
                                           <asp:TextBox ID="LineItemBox" runat="server" Width="50px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Work Code" SortExpression="WorkCode" >
                                       
                                       <ItemTemplate>
                                          
                                           <asp:DropDownList ID="WorkCodeList" runat="server" DataTextField="WorkCode" DataValueField="WorkCodeID" Width="150px" AppendDataBoundItems="true" ><asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem></asp:DropDownList>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty" SortExpression="Quantity" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="QtyBox" runat="server" Width="32px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Each" SortExpression="Each" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="EachBox" runat="server" Width="60px" AutoPostBack="true" OnTextChanged="EachBox_TextChanged" DataFormatString="${0:#,0}"/>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="Total" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="TotalBox" AutoPostBack="true" OnTextChanged="TotalBox_TextChanged" runat="server" Width="60px"   DataFormatString="${0:#,0}"/>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate" >
                                      
                                       <ItemTemplate>                                           
                                           <bdp:BDPLite ID="DueDateBox" runat="server" Width="75px" ></bdp:BDPLite>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Notes" SortExpression="Notes" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="NotesBox" runat="server" Width="300px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Monsees Lot" SortExpression="LotDescription" >
                                       
                                       <ItemTemplate>
                                           
                                           <asp:DropDownList ID="LotList" runat="server" DataTextField="LotDescription" DataValueField="JobItemID" Width="400px" AppendDataBoundItems="true" ><asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem></asp:DropDownList>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Date Sent" SortExpression="DateSent" >
                                       <ItemTemplate>
                                           <BDP:BDPLite ID="DateSentBox" runat="server" />
                                       </ItemTemplate>
                                   </asp:TemplateField>                                    
                                   <asp:TemplateField HeaderText="Cert Reqd" SortExpression="PlateCertReqd" >
                                       <ItemTemplate>
                                           <asp:Checkbox ID="CertCheck" runat="server" Checked='false' Enabled="true" />
                                       </ItemTemplate>                                                                 
                                   </asp:TemplateField>                                      
                                  
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                           </asp:GridView>
                           <asp:Button runat="server" ID="Button1" OnClick="Button1_Click" Text="Execute Order" Visible="false" />
                       </asp:View>
                   </asp:MultiView>
                           

                           <asp:SqlDataSource ID="SubPOLineItems" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

</asp:Content>
