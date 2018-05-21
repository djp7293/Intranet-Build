<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActiveJobs.aspx.cs" Inherits="Monsees._Default_Manager"%>

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
                
                <td>
                    <asp:Button ID="LogOutButton" runat="server" onclick="LogOutButton_Click" 
                        Text="Log Out" />
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
    				<asp:Button ID="RefreshOpsButton" runat="server" Text="Update Ops." onclick="RefreshOpsButton_Click"
                        />
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
    				<asp:DropDownList ID="AvailableJobList" DataSourceID="populatejoblist"
					AutoPostBack="true" DataValueField="Job_Number" runat="server" Width="60px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>

			</td>

			<td width="4%">
				
			</td>
			<td width="12%">
				
			</td>
			<td width="9%" align="left">
    				<asp:DropDownList ID="AvailablePartsList" DataSourceID="populatepartslist"
					AutoPostBack="true" DataValueField="PartNumber" runat="server" Width="110px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
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
    				<asp:DropDownList ID="NextOpsList" DataSourceID="populateopslist"
					AutoPostBack="true" DataValueField="NextOp" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
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
                               DataKeyNames="JobSetupID" DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                   <asp:ButtonField CommandName="Deliveries" Text="+"  ItemStyle-Width="1%">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
				   <asp:BoundField DataField="Job_Number" HeaderText="Job #" 
                                       SortExpression="Job_Number" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CompanyName"  
                                       HeaderText="Customer" SortExpression="CompanyName" ItemStyle-Width="9%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="PartNumber" HeaderText="Part Number" 
                                       SortExpression="PartNumber" ItemStyle-Width="10%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="15%">
				       <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="MinOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="6%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Notes" HeaderText="Process Notes" SortExpression="Notes" ItemStyle-Width="17%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
				   <asp:BoundField DataField="JobSetupID" HeaderText="ID" ItemStyle-CssClass="hiddencol" ItemStyle-Width="3%">
				   <ItemStyle HorizontalAlign="Center" />
				   <HeaderStyle CssClass="hiddencol"/>
				   </asp:BoundField>                                
			           <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="9%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
			           <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" SortExpression="LateStart" ItemStyle-Width="6%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   				   <asp:TemplateField ItemStyle-Width="3%" HeaderText="Update">
					<ItemTemplate >

					<asp:CheckBox ID="Update" Checked="false" runat="server"/>
					</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
				   </asp:TemplateField>
<asp:ButtonField CommandName="ViewOps" Text="All Setups" HeaderText="" ItemStyle-Width="5%">
                                   <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField> 
   				   <asp:TemplateField ItemStyle-Width="6%">
					<ItemTemplate>
					<asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile"  CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>			
				<asp:TemplateField>
					<ItemTemplate>
					</td>
					</tr>
					<tr>
					<td colspan="100%">
					<div id="div<%# Eval("CustomerID") %>" style="display:none;position:relative;left:25px;" >
						<asp:GridView ID="DeliveryViewGrid" runat="server" AllowPaging="False" 
                               				AutoGenerateColumns="False" BackColor="White" 
                               				BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               				DataKeyNames="JobSetupID" DataSourceID="MonseesSqlDataSourceDeliveries" GridLines="Vertical" 
                               				onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
                               				AllowSorting="True">
                               				<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               				<Columns>

							</Columns>
						</asp:Gridview>
					</div>
					</td>
					</tr>
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

Select * From ProductionView" EnableCaching="False" FilterExpression="Job_Number like '{0}%'
                and PartNumber like '{1}%' and NextOp like '{2}%'">
<FilterParameters>
        <asp:ControlParameter Name="Job" ControlID="AvailableJobList"
            PropertyName="SelectedValue" />
        <asp:ControlParameter Name="Part" ControlID="AvailablePartsList"
            PropertyName="SelectedValue" />
<asp:ControlParameter Name="NextOp" ControlID="NextOpsList"
            PropertyName="SelectedValue" />
    </FilterParameters>


    </asp:SqlDataSource>

    <asp:SqlDataSource ID="MonseesSqlDataSourceDelivies" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="populatejoblist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT Job_Number From ProductionView">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="populatepartslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PartNumber From ProductionView">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT NextOp From ProductionView">
    </asp:SqlDataSource>  

    </form>
</body>
</html>
