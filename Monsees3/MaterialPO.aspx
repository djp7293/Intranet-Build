<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Monsees.Master" AutoEventWireup="true" CodeBehind="MaterialPO.aspx.cs" Inherits="Monsees.MaterialPO" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
       <title>Material Purchase Order</title>
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
                   <asp:ListView ID="ListView2" runat="server" DataKeyNames="MaterialPOID" EnableModelValidation="True">
                       
                      
                       <EmptyDataTemplate>
                           <span>No data was returned.</span>
                       </EmptyDataTemplate>
                       
                       <ItemTemplate>
                           <table style="width: 800px"><tr><td style="width: 25%">
                           <span style="">PO #:
                           <asp:Label ID="MaterialPOIDLabel" runat="server" Text='<%# Eval("MaterialPOID") %>' />-M
                           </td><td style="width: 25%">
                           Due Date:
                           <asp:Label ID="DueDateLabel" runat="server" Text='<%# Eval("DueDate") %>' />
                           </td><td style="width: 50%" colspan="2">
                           Notes:
                           <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
                           
                           
                           <br />
                           </td></tr><tr><td colspan="2">
                           Vendor Name:
                           <asp:Label ID="VendorNameLabel" runat="server" Text='<%# Eval("VendorName") %>' />
                           
                           
                           </td><td>
                           Conf. Num:
                           <asp:Label ID="ConfirmationNumLabel" runat="server" Text='<%# Eval("ConfirmationNum") %>' />
                           </td><td>
                           Contact Name:
                           <asp:Label ID="ContactNameLabel" runat="server" Text='<%# Eval("ContactName") %>' />
                           </td></tr><tr><td>
                            Buyer:
                           <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                           
                           </td><td>
                          Ship Charge:
                           <asp:Label ID="ShippingChargeLabel" runat="server" Text='<%# Eval("ShippingCharge") %>' />
                           </td><td>
                           Received:
                           <asp:Label ID="ReceivedLabel" runat="server" Text='<%# Eval("Received") %>' />
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
            <span style="font-size: large;"><B>Initiate New Material Purchase Order:</B></span><br /><br />
            <asp:FormView ID="FormView1" runat="server" OnDataBound="FormView1_DataBound" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                <ItemTemplate>
                    <table style="width: 1000px"><tr>                           
                           <td colspan="2">
                           Vendor:
                           <asp:DropDownList ID="VendorNameList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VendorNameList_SelectedIndexChanged" DataTextField="VendorName" DataValueField="SubcontractID" Width="350px" AppendDataBoundItems="true" >
                               <asp:ListItem Selected="True" Text="None Selected" Value="0" Enabled="true"></asp:ListItem></asp:DropDownList>
                           
                           
                           </td><td>Contact:
                               <asp:DropDownList ID="ContactList" runat="server" DataTextField="ContactName" DataValueField="ContactID" Width="200px" AppendDataBoundItems="true" >
                               <asp:ListItem Selected="True" Text="None Selected" Value="0" Enabled="true"></asp:ListItem></asp:DropDownList>
                                </td></tr><tr>
                               <td>Due:
                           <bdp:BDPLite ID="DueDateBox" runat="server" />
                           </td>
                               <td>
                           Ship Charge:
                           <asp:TextBox ID="ShipChargeBox" Text="0" runat="server" Width="60px"  />
                               
                           
                           
                           </td>
                               <td>
                          Ship Method:
                           <asp:DropDownList ID="ShipMethodList" runat="server" DataTextField="Name" DataValueField="ShipMethodID" Width="200px" AppendDataBoundItems="true" >
                               <asp:ListItem Enabled="true" Selected ="True" Text="None Selected" Value="0"></asp:ListItem>
                           </asp:DropDownList>
                           </td>
                                      </tr><tr><td colspan="3">
                            Notes:
                           <asp:TextBox ID="NotesBox" runat="server" Width="400px" />
                           
                           </td></tr><tr><td>
                            Buyer:
                           <asp:DropDownList ID="NameList" runat="server" DataValueField="EmployeeID" DataTextField="Name" Width="250px" AppendDataBoundItems="true" >
                               <asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem>
                           </asp:DropDownList>                           
                           </td><td>
                           Conf. #:
                           <asp:TextBox ID="ConfirmationBox" runat="server" Width="150px"  />
                               
                           
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
                   <asp:SqlDataSource ID="MatlPurchOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

                   <br />
     <asp:MultiView runat="server" ID="CreateItemsViewMulti" ActiveViewIndex="0" EnableViewState="true">
                       <asp:View runat="server" ID="ViewEditView" >
                           <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateEditButton="true" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="MatPriceID" EnableModelValidation="True" GridLines="Vertical">
                               <AlternatingRowStyle BackColor="#DCDCDC" />
                               <Columns>
                                   
                                   <asp:TemplateField HeaderText="Material" SortExpression="MaterialName" >
                                       <ItemTemplate>
                                           <asp:Label ID="MatlLbl" runat="server" Text='<%# Eval("MaterialName") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hfmatl" runat="server" Value='<%# Eval("MaterialID") %>'/>
                                           <asp:DropDownList ID="MatlList" runat="server" DataTextField="Material" DataValueField="MaterialID" Width="150px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Dimension" SortExpression="Dimension" >
                                       <ItemTemplate>
                                           <asp:Label ID="DimLbl" runat="server" Text='<%# Eval("Dimension") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hfdim" runat="server" Value='<%# Eval("MaterialDimID") %>' />
                                           <asp:DropDownList ID="DimList" runat="server" DataTextField="Dimension" DataValueField="MaterialDimID" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Size" SortExpression="Size" >
                                       <ItemTemplate>
                                           <asp:Label ID="SizeLbl" runat="server" Text='<%# Eval("Size") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:HiddenField ID="hfsize" runat="server" Value='<%# Eval("MaterialSizeID") %>' />
                                           <asp:DropDownList ID="SizeList" runat="server" DataTextField="Size" DataValueField="MaterialSizeID" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Length" SortExpression="Length" >
                                       <ItemTemplate>
                                           <asp:Label ID="LengthLbl" runat="server" Text='<%# Eval("Length") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="LengthBox" runat="server" Text='<%# Eval("Length")  %>' Width="32px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty" SortExpression="quantity" >
                                       <ItemTemplate>
                                           <asp:Label ID="QtyLbl" runat="server" Text='<%# Eval("quantity") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="QtyBox" runat="server" Text='<%# Eval("quantity") %>' Width="32px" />
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
                                   <asp:TemplateField HeaderText="Item #" SortExpression="ItemNum" >
                                       <ItemTemplate>
                                           <asp:Label ID="ItemNumLbl" runat="server" Text='<%# Eval("ItemNum") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="ItemNumBox" runat="server" Text='<%# Eval("ItemNum") %>' Width="100px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Shipping" SortExpression="Shipping" >
                                       <ItemTemplate>
                                           <asp:Label ID="ShippingLbl" runat="server" Text='<%# Eval("Shipping") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="ShippingBox" runat="server" Text='<%# Eval("Shipping") %>' Width="60px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ship Charge" SortExpression="ShippingCharge" >
                                       <ItemTemplate>
                                           <asp:Label ID="ShipCostLbl" runat="server" Text='<%# Eval("ShippingCharge") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="ShipCostBox" runat="server" Text='<%# Eval("ShippingCharge") %>' Width="60px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="cost" >
                                       <ItemTemplate>
                                           <asp:Label ID="CostLbl" runat="server" Text='<%# Eval("cost") %>' DataFormatString="${0:#,0}" />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="CostBox" runat="server" Text='<%# Eval("cost") %>' Width="60px"   DataFormatString="${0:#,0}"/>
                                       </EditItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Conf. #" SortExpression="ConfirmationNum" >
                                       <ItemTemplate>
                                           <asp:Label ID="ConfLbl" runat="server" Text='<%# Eval("ConfirmationNum") %>' />
                                       </ItemTemplate>
                                       <EditItemTemplate>
                                           <asp:TextBox ID="ConfBox" runat="server" Text='<%# Eval("ConfirmationNum") %>' Width="100px" />
                                       </EditItemTemplate>
                                   </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Cert Reqd" SortExpression="MinOfMatlCertReqd" >
                                       <ItemTemplate>
                                           <asp:Checkbox ID="CertCheck" runat="server" Checked='<%# Eval("MinOfMatlCertReqd") %>' Enabled="false" />
                                       </ItemTemplate>
                                       
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Rec'd" SortExpression="received">
                                       <ItemTemplate>
                                           <asp:CheckBox ID="RecdCheck" runat="server" Checked='<%# Eval("received") %>' Enabled="false" />
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
                           <asp:GridView ID="GridView2" runat="server" OnRowDataBound="GridView2_RowDataBound" AutoGenerateColumns="False"  BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" EnableModelValidation="True" GridLines="Vertical">
                               <AlternatingRowStyle BackColor="#DCDCDC" />
                               <Columns>
                                   
                                   <asp:TemplateField HeaderText="Material" SortExpression="MaterialName" >
                                     
                                       <ItemTemplate>
                                           
                                           <asp:DropDownList ID="MatlList" runat="server" DataTextField="Material" DataValueField="MaterialID" Width="150px" AppendDataBoundItems="true" ><asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem></asp:DropDownList>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Dimension" SortExpression="Dimension" >
                                      
                                       <ItemTemplate>
                                           
                                           <asp:DropDownList ID="DimList" runat="server" DataTextField="Dimension" DataValueField="MaterialDimID" AppendDataBoundItems="true" ><asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem></asp:DropDownList>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Size" SortExpression="Size" >
                                       
                                       <ItemTemplate>
                                           
                                           <asp:DropDownList ID="SizeList" runat="server" DataTextField="Size" DataValueField="MaterialSizeID" AppendDataBoundItems="true" ><asp:ListItem Enabled="true" Selected="True" Text="None Selected" Value="0"></asp:ListItem></asp:DropDownList>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Length" SortExpression="Length" >
                                      
                                       <ItemTemplate>
                                           <asp:TextBox ID="LengthBox" Text="0" runat="server" Width="32px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty" SortExpression="quantity" >
                                      
                                       <ItemTemplate>
                                           <asp:TextBox ID="QtyBox" Text="0" runat="server" Width="32px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="cost" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="CostBox" Text="0" runat="server"  DataFormatString="${0:#,0}"/>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate" >
                                       
                                       <ItemTemplate>
                                           
                                           <bdp:BDPLite ID="DueDateBox" runat="server" Width="75px" ></bdp:BDPLite>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Item #" SortExpression="ItemNum" >
                                      
                                       <ItemTemplate>
                                           <asp:TextBox ID="ItemNumBox" runat="server" Width="100px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ship Method" SortExpression="Shipping" >
                                      
                                       <ItemTemplate>
                                           <asp:DropDownList ID="ShipMethodList" runat="server" DataTextField="Name" DataValueField="ShipMethodID" Width="200px" AppendDataBoundItems="true" >
                               <asp:ListItem Enabled="true" Selected ="True" Text="None Selected" Value="0"></asp:ListItem>
                           </asp:DropDownList>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ship Charge"  SortExpression="ShippingCharge" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="ShipCostBox" Text="0" runat="server" Width="60px" />
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   
                                   <asp:TemplateField HeaderText="Conf. #" SortExpression="ConfirmationNum" >
                                       
                                       <ItemTemplate>
                                           <asp:TextBox ID="ConfBox" runat="server" Width="100px" />
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
                           <asp:SqlDataSource ID="MatPOLineItems" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

</asp:Content>
