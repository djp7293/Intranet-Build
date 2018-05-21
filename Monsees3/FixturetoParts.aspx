<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FixturetoParts.aspx.cs" Inherits="Monsees.FixturetoParts"MasterPageFile="~/MasterPages/Monsees.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="headContent" >
    <title>Allocate Fixture</title>
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


 <asp:Content  runat="server" ContentPlaceHolderID="bodyContent">        
              

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
					AutoPostBack="true" DataValueField="JobNumber" runat="server" Width="60px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>

			</td>

			<td width="4%">
				
			</td>
			<td width="12%">
				    				<asp:DropDownList ID="CompanyNameList" DataSourceID="populatecompanylist"
					AutoPostBack="true" DataValueField="CompanyName" runat="server" Width="140px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
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
      	
                   
                    <table><tr><td align="Right">
    				<asp:Button ID="RefreshOpsButton" runat="server" Text="Add to Selected" onclick="RefreshOpsButton_Click"
                        />
			</td></tr></table>
                           <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataKeyNames="RevisionID" DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
			       
                               AllowSorting="True">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>
                                  
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
                                      				   <asp:TemplateField ItemStyle-Width="6%" HeaderText="Part Number">
					<ItemTemplate>
					<asp:LinkButton ID="lbPart" runat="server" CommandName="PartHistory"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text='<%#Eval("PartNumber") %>'></asp:LinkButton>
					</ItemTemplate>
					</asp:TemplateField>	
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
			           <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" SortExpression="LateStartInt" ItemStyle-Width="6%">
				   <ItemStyle HorizontalAlign="Left" />
				   </asp:BoundField>
                                   				   <asp:TemplateField ItemStyle-Width="3%" HeaderText="Select">
					<ItemTemplate >

					<asp:CheckBox ID="Allocate" Checked="false" runat="server"/>
					</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
				   </asp:TemplateField>

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
                
        
</td>
</tr>
</table>

    <p>
    <asp:Label ID="AllocateFixture" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>

    </div>

 
 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From ProductionViewWP1" EnableCaching="False" FilterExpression="Job_Number like '{0}%'
                and PartNumber like '{1}%' and NextOp like '{2}%' and Companyname like '{3}%'">
<FilterParameters>
        <asp:ControlParameter Name="Job" ControlID="AvailableJobList"
            PropertyName="SelectedValue" />
        <asp:ControlParameter Name="Part" ControlID="AvailablePartsList"
            PropertyName="SelectedValue" />
<asp:ControlParameter Name="NextOp" ControlID="NextOpsList"
            PropertyName="SelectedValue" />
<asp:ControlParameter Name="CompanyName" ControlID="CompanyNameList"
            PropertyName="SelectedValue" />
    </FilterParameters>


    </asp:SqlDataSource>



    <asp:SqlDataSource ID="populatejoblist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT JobNumber From Job">
    </asp:SqlDataSource>   

    <asp:SqlDataSource ID="populatepartslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT PartNumber From Detail">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="populatecompanylist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT CompanyName From CustomerDB">
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	DISTINCT NextOp From ProductionViewAndNotCleared">
    </asp:SqlDataSource>  



</asp:Content>
