<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptSchedule.aspx.cs" Inherits="Monsees._Default_Dept"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<head runat="server">
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

<script type="text/javascript">

function collapseExpand(obj) 
{

  var div = document.getElementById('JobItemIDTag');

  var theFlag = div.style.display == "inline";
  div.style.display = (theFlag) ? "none" : "inline";
 

}

</script>


<!--[if gte mso 9]><xml>
<mso:CustomDocumentProperties>
<mso:IsMyDocuments msdt:dt="string">1</mso:IsMyDocuments>
</mso:CustomDocumentProperties>
</xml><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
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
         
              

	<table itemstyle-cssclass="GridviewTable">

				<tr BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White">		
			<td width="1%">
    				
			</td>
			<td width="4%">
				
			</td>
			<td width="4%">
				
			</td>
			<td width="12%">
    				
			</td>
			<td width="9%">
				
			</td>
			<td width="3%">
				
			</td>
			<td width="15%">
				
			</td>
			<td width="8%">
				
			</td>
			<td width="3%">
				
			</td>
			<td width="12%">
				
			</td>
			<td width="0%">
				
			</td>
			<td width="9%" align="Left">
    				
			</td>
			<td width="8%">
				
			</td>
			<td width="5%">
				
			</td>
			<td width="6%">

			</td>
		</tr>
		<tr BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White">		
			<td width="1%">
				
			</td>
			<td width="4%" align="left">


			</td>

			<td width="4%">
				
			</td>
			<td width="12%">
				
			</td>
			<td width="9%" align="left">

			</td>
			<td width="3%">
				
			</td>
			<td width="15%">
				
			</td>
			<td width="8%">
				
			</td>
			<td width="3%">
				
			</td>
			<td width="12%">
				
			</td>
			<td width="0%">
			</td>

			<td width="9%" align="left">
    				
			</td>
			<td width="8%">
				
			</td>
			<td width="5%">
				
			</td>
			<td width="6%">

			</td>
			<td width="3%">

			</td>
		</tr>
		</tr>

		<tr>
	<td colspan="15">
      	<asp:Panel ID="Panel1" runat="server">
            <asp:Panel ID="Panel2" runat="server">
               <p>
                   
                    
                           <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                 
				   <asp:BoundField DataField="Job #" HeaderText="Job #" 
                                       SortExpression="Job #" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Lot #" HeaderText="Lot #" 
                                       SortExpression="Lot #" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   				   <asp:BoundField DataField="NxtDelivry" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Next Delivery" SortExpression="NxtDelivry" ItemStyle-Width="6%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
<asp:BoundField DataField="CompanyName"  
                                       HeaderText="Customer" SortExpression="CompanyName" ItemStyle-Width="9%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                      				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>'></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>	
                                   <asp:BoundField DataField="Rev #" HeaderText="Rev" 
                                       SortExpression="Rev #" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="Description" HeaderText="Description" 
                                       SortExpression="Description" ItemStyle-Width="15%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>

                                   <asp:BoundField DataField="Notes" HeaderText="Process Notes" SortExpression="Notes" ItemStyle-Width="17%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="Qty" HeaderText="Qty" 
                                       SortExpression="Qty" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
				   <asp:BoundField DataField="Mat'l" HeaderText="Mat'l" SortExpression="Mat'l" ItemStyle-Width="3%">
				   <ItemStyle HorizontalAlign="Center" />
				    </asp:BoundField>               
			           
			           <asp:BoundField DataField="Late Start" HeaderText="Late Start" SortExpression="Late Start" ItemStyle-Width="6%" DataFormatString="{0:MM-dd-yyyy}" >
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="Multiaxis" HeaderText="Multiaxis" SortExpression="Multiaxis" ItemStyle-Width="5%">
				   <ItemStyle HorizontalAlign="Center" />
				   </asp:BoundField>            
				   <asp:BoundField DataField="CNC Lathe" HeaderText="CNC Lathe" SortExpression="CNC Lathe" ItemStyle-Width="5%">
				   <ItemStyle HorizontalAlign="Center" />
				   </asp:BoundField>      
				   <asp:BoundField DataField="Hard Turn" HeaderText="Hard Turn" SortExpression="Hard Turn" ItemStyle-Width="5%">
				   <ItemStyle HorizontalAlign="Center" />
				   </asp:BoundField>      
				   <asp:BoundField DataField="Turn" HeaderText="Turn" SortExpression="Turn" ItemStyle-Width="5%">
				   <ItemStyle HorizontalAlign="Center" />
				   </asp:BoundField>                             				   
				   <asp:ButtonField CommandName="ViewOps" Text="All Setups" HeaderText="" ItemStyle-Width="5%">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
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
                      
                       
                 </p>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>

    <p>
    <asp:Label ID="UpdateResults" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>

    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From SetupCrosstabPivot WHERE MultiAxis Is Not Null OR [CNC Lathe] Is Not Null OR [Hard Turn] Is Not Null OR Turn Is Not Null ORDER BY [Late Start]" EnableCaching="False">



    </asp:SqlDataSource>

    <asp:SqlDataSource ID="MonseesSqlDataSourceDeliveries" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>


    

    </form>
</body>
</html>
