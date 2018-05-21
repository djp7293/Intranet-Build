<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Print.master"  AutoEventWireup="true" CodeBehind="MaterialPOPrint.aspx.cs" Inherits="Monsees.MaterialPOPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <table class="report-heading">
        <tr>
            <td style="width: 40%;">

            </td>
            <td style="width: 20%;">

            </td>
            <td style="width: 40%;">
                <span class="top">Purchase Order</span>
                <asp:FormView ID="ListView2" runat="server" DataKeyNames="MaterialPOID" EnableModelValidation="True" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" >
                       
                      
                       <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                       
                      
                       <EmptyDataTemplate>
                           <span>No data was returned.</span>
                       </EmptyDataTemplate>
                       
                       <FooterStyle BackColor="#CCCCCC" />
                       <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                       
                       <ItemTemplate>
                           <table><tr><td colspan="3" style="align-content:stretch">
                          PO #:
                           <asp:Label ID="MaterialPOIDLabel" runat="server" Text='<%# Eval("MaterialPOID") %>' />-M
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

                       </ItemTemplate>
                      
                       <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                      
                   </asp:FormView>
            </td>
        </tr>
    </table>
                   

                   <asp:SqlDataSource ID="MatlPurchOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

                   <br />
    
                           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="MatPriceID" EnableModelValidation="True" GridLines="Horizontal" ForeColor="Black">
                               <Columns>
                                   
                                   <asp:TemplateField HeaderText="Material" SortExpression="MaterialName" >
                                       <ItemTemplate>
                                           <asp:Label ID="MatlLbl" runat="server" Text='<%# Eval("MaterialName") %>' />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Dimension" SortExpression="Dimension" >
                                       <ItemTemplate>
                                           <asp:Label ID="DimLbl" runat="server" Text='<%# Eval("Dimension") %>' />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Size" SortExpression="Size" >
                                       <ItemTemplate>
                                           <asp:Label ID="SizeLbl" runat="server" Text='<%# Eval("Size") %>' />
                                       </ItemTemplate>
                                     
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Length" SortExpression="Length" >
                                       <ItemTemplate>
                                           <asp:Label ID="LengthLbl" runat="server" Text='<%# Eval("Length") %>' />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Qty" SortExpression="quantity" >
                                       <ItemTemplate>
                                           <asp:Label ID="QtyLbl" runat="server" Text='<%# Eval("quantity") %>' />
                                       </ItemTemplate>
                                     
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Due Date" SortExpression="DueDate" >
                                       <ItemTemplate>
                                           <asp:Label ID="DueDateLbl" runat="server" Text='<%# Eval("DueDate") %>' />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Item #" SortExpression="ItemNum" >
                                       <ItemTemplate>
                                           <asp:Label ID="ItemNumLbl" runat="server" Text='<%# Eval("ItemNum") %>' />
                                       </ItemTemplate>
                                     
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Shipping" SortExpression="Shipping" >
                                       <ItemTemplate>
                                           <asp:Label ID="ShippingLbl" runat="server" Text='<%# Eval("Shipping") %>' />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Ship Charge" SortExpression="ShippingCharge" >
                                       <ItemTemplate>
                                           <asp:Label ID="ShipCostLbl" runat="server" Text='<%# Eval("ShippingCharge") %>' />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total" SortExpression="cost" >
                                       <ItemTemplate>
                                           <asp:Label ID="CostLbl" runat="server" Text='<%# Eval("cost") %>' DataFormatString="${0:#,0}" />
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                 

                                   <asp:TemplateField HeaderText="Cert Reqd" SortExpression="MinOfMatlCertReqd" >
                                       <ItemTemplate>
                                           <asp:Checkbox ID="CertCheck" runat="server" Checked='<%# Eval("MinOfMatlCertReqd") %>' Enabled="false" />
                                       </ItemTemplate>
                                       
                                   </asp:TemplateField>
                       
                                  
                               </Columns>
                              
                           </asp:GridView>
                   
                           <asp:SqlDataSource ID="MatPOLineItems" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>

    </asp:Content>
