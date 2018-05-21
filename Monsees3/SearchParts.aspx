<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchParts.aspx.cs" Inherits="Monsees._Default_SearchParts" MasterPageFile="~/MasterPages/Monsees.Master" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ContentPlaceHolderID="headContent" runat="server">
    <title>Part Search</title>
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
   <td align="center" valign="middle">
                    &nbsp;</td>
   
            </tr>
        </table>
         
              

	<table itemstyle-cssclass="GridviewTable">

			

		<tr>
	<td colspan="15">
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <div>
                   
                           <div style="align-content:flex-start"><table><tr><td>
                               Company: <asp:TextBox ID="CompanyFilter" runat="server" >
        				       
                               </asp:TextBox>
                               </td><td> PartNumber: <asp:TextBox ID="PartFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td> DrawingNumber: <asp:Textbox ID="DescFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />
                              </td><td> <asp:Button ID="exportdata" Text="Export" OnClick="btnDownload_Click" runat="server" /></td></tr></table></div>
                           <asp:GridView ID="PartGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="PartGrid_RowCommand" Width="100%" 			       
                               AllowSorting="True" PageSize="100" EnableModelValidation="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  
                                   
				   <asp:BoundField DataField="DetailID" HeaderText="Detail ID" 
                                       SortExpression="DetailID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   
				   <asp:BoundField DataField="CompanyName" HeaderText="Company" SortExpression="CompanyName" ItemStyle-Width="8%">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>

                                   <asp:BoundField DataField="PartNumber"  
                                       HeaderText="Part #" SortExpression="PartNumber" ItemStyle-Width="7%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="17%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                    <asp:BoundField DataField="Notes" 
                                        HeaderText="Notes" SortExpression="Notes" ItemStyle-Width="16%">
                                       <ItemStyle HorizontalAlign="Left
                                           " />
                                   </asp:BoundField>
                                    <asp:CheckBoxField DataField="ITAR" SortExpression="ITAR" HeaderText="ITAR" ItemStyle-HorizontalAlign="Center" />

				                            
			          
				   <asp:BoundField DataField="MaterialName" HeaderText="Material" SortExpression="MaterialName" ItemStyle-Width="5%">
				   	<ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>		    				   
				   
				   <asp:ButtonField CommandName="ViewHistory" Text="Prod. History" HeaderText="" ItemStyle-Width="3%">
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
                     
                 </div>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    
    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
       EnableCaching="False" >



    </asp:SqlDataSource>





</asp:Content>
