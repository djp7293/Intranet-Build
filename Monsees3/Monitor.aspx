<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="Monsees.Monitor" MasterPageFile="~/MasterPages/Monsees.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="headContent">


    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0,user-scalable=1.5;" />
    <meta http-equiv="Content-Type" content="application/vnd.wap.xhtml+xml; charset=utf-8" />
    <meta name="HandheldFriendly" content="true" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link id="lnkStylesheet" href="standard.css" rel="stylesheet" />

    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2 {
            width: 11%;
        }
	.GridViewContainer
        {           
            overflow-y: scroll;
        }
        /* to freeze column cells and its respecitve header*/
        .FrozenCell
        {
            background-color:Gray;
            position: relative;
            cursor: default;
            left: expression(document.getElementById("GridViewContainer").scrollLeft-2);
        }
        /* for freezing column header*/
        .FrozenHeader
        {
         background-color:Gray;
            position: relative;
            cursor: default;            
            top: expression(document.getElementById("GridViewContainer").scrollTop-2); 
	    Font-Size: small;
            z-index: 10;
        }
        /*for the locked columns header to stay on top*/
        .FrozenHeader.locked
        {
            z-index: 99;
        }
        

        }

    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="bodyContent">

       
         <table class="style1" width="100%">
            <tr width="100%">
                <td align="left" valign="middle" class="style2" width="20%">
                    <asp:Button ID="UserNameLabel" runat="server" onclick="UserNameLabel_Click" 
                        />
                <td  width="20%">
                    <asp:DropDownList ID="UsersDropDownList" runat="server" 
                        style="margin-left: 0px" AutoPostBack="True" DataTextField="Name" DataValueField="EmployeeID">
                    </asp:DropDownList>
                </td>

                
<td width="20%">
                    <asp:Button ID="DeptSchedule" runat="server" Text="Dept. Schedules" 
                        onclick="DeptSchedule_Click" />
                </td>
<td align="right" valign="middle" width="20%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
              

	<table width="800px">

		<tr BackColor="#000084" Font-Bold="True" Font-Size="Large" 
                                   ForeColor="White">		
			<td width="5%" align="left">
    				<asp:DropDownList ID="AvailableJobList" DataSourceID="populatejoblist"
					AutoPostBack="true" DataValueField="Job_Number" runat="server" Width="60px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>

			</td>
			<td width="1%">
				
			</td>
			<td width="10%">
				
			</td>
			<td width="8%" align="left">
    				<asp:DropDownList ID="AvailablePartsList" DataSourceID="populatepartslist"
					AutoPostBack="true" DataValueField="PartNumber" runat="server" Width="110px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
			</td>
			<td width="3%">
				
			</td>
			<td width="13%">
				
			</td>
			<td width="7%">
				
			</td>
			<td width="3%">
				
			</td>
			
			<td width="13%">
				
			</td>
			<td width="8%" align="left">
    				<asp:DropDownList ID="NextOpsList" DataSourceID="populateopslist"
					AutoPostBack="true" DataValueField="NextOp" runat="server" Width="100px" Font-Size="11px"
					AppendDataBoundItems="true">
        				<asp:ListItem Text="All" Value="%"></asp:ListItem>
    				</asp:DropDownList>
			</td>
			<td width="6%">
				
			</td>
			<td width="4%">
				
			</td>
			<td width="4%">

			</td>
		</tr>
		</table>
    
   <div id="GridViewContainer" class="GridViewContainer" width="800px" height="500px" Font-Size="Small">    			        
		

                
                      
                          <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                               onrowcommand="ProductionViewGrid_RowCommand" Width="100%" 
                               AllowSorting="True" 
                               onselectedindexchanged="ProductionViewGrid_SelectedIndexChanged" DataKeyNames="JobItemID">
				
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
                               <Columns>
                                    <asp:TemplateField>
					<ItemTemplate>
					<a href="JavaScript:divexpandcollapse('div<%# Eval("JobItemID") %>');">
                            +</a>  
                        
					</ItemTemplate>

				   </asp:TemplateField>
				                   <asp:BoundField DataField="Job_Number" HeaderText="Job #" 
                                       SortExpression="Job_Number" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="JobItemID" HeaderText="Lot #" 
                                       SortExpression="JobItemID" ItemStyle-Width="4%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   <asp:BoundField DataField="CompanyName"  
                                       HeaderText="Customer" SortExpression="CompanyName" ItemStyle-Width="11%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="PartNumber"  
                                       HeaderText="Part Number" SortExpression="PartNumber" ItemStyle-Width="8%">
                                       <ItemStyle HorizontalAlign="Left" />
                                   </asp:BoundField>				 
                                   <asp:BoundField DataField="Revision Number" HeaderText="Rev" 
                                       SortExpression="Revision Number" ItemStyle-Width="2%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>				   
                                   <asp:BoundField DataField="DrawingNumber" HeaderText="Description" 
                                       SortExpression="DrawingNumber" ItemStyle-Width="13%">
				                       <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
				                   <asp:BoundField DataField="MinOfCurrDelivery" DataFormatString="{0:MM-dd-yyyy}" 
                                       HeaderText="Next Delivery" SortExpression="MinOfCurrDelivery" ItemStyle-Width="7%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                                   
                                   <asp:BoundField DataField="Quantity" HeaderText="Qty" 
                                       SortExpression="Quantity" ItemStyle-Width="3%">
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>
                                   
                                   <asp:BoundField DataField="Notes" HeaderText="Process Notes" SortExpression="Notes" ItemStyle-Width="13%">
				                   <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
                                   <asp:BoundField DataField="MaterialLoc" HeaderText="Matl" SortExpression="MaterialLoc"  ItemStyle-Width="5%">
				                    <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>           
				   
			                       <asp:BoundField DataField="NextOp" HeaderText="Next Op" SortExpression="NextOp" ItemStyle-Width="8%">
				                    <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>
			                       <asp:BoundField DataField="LateStartDate" HeaderText="Late Start" 	SortExpression="LateStartInt"  ItemStyle-Width="6%" DataFormatString="{0:d}">
				                    <ItemStyle HorizontalAlign="Left" />
				                   </asp:BoundField>

                                   <asp:ButtonField CommandName="Other" Text="Login" HeaderText="" ItemStyle-Width="4%">
                                       
                                   </asp:ButtonField> 
                                   <asp:ButtonField CommandName="Inspection" Text="Report" HeaderText="Inspection" ItemStyle-Width="4%">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>
                                   <asp:CheckBoxField DataField="AreFixtures" SortExpression="AreFixtures" ItemStyle-Width="3%" />
                                   <asp:ButtonField CommandName="Fixturing" Text="Fixtures" ItemStyle-Width="4%">
                                       <ControlStyle Font-Size="Small" />
                                   </asp:ButtonField>
                                   <asp:CheckBoxField DataField="IsDrawing" SortExpression="IsDrawing" ItemStyle-Width="3%" />
   				                   <asp:TemplateField>
					                <ItemTemplate>
					                <asp:LinkButton ID="lbGetFile" runat="server" CommandName="GetFile" CommandArgument='<%#Eval("RevisionID") %>' Text="Drawing"></asp:LinkButton>
					                </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Font-Size="Large" />
				                   </asp:TemplateField>  
                                   <asp:TemplateField>
					                <ItemTemplate>
					

					    <div id='JobItemIDTag' style="display:inline;position:relative;left:25px;" >
                            <tr><td colspan="10">
						        <asp:GridView ID="DeliveryViewGrid" runat="server" AllowPaging="False" 
                               				AutoGenerateColumns="False" BackColor="White" 
                               				BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               				DataKeyNames="JobItemID" GridLines="Vertical"  
                               				AllowSorting="True" Font-Size="Large">
                               				<RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="Large" />
                               	<Columns>
				                   <asp:BoundField DataField="CurrDelivery"  
                                       HeaderText="Delivery" SortExpression="CurrDelivery">
                                       <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Size="Small" />
                                   </asp:BoundField>             
                                       
                                   <asp:BoundField DataField="Quantity"  
                                       HeaderText="Quantity" SortExpression="Quantity">
                                       <ItemStyle HorizontalAlign="Center" />
                                        <ControlStyle Font-Size="Small" />
                                   </asp:BoundField>   
                                   <asp:BoundField DataField="PONumber"  
                                       HeaderText="PO Number" SortExpression="PONumber">
                                       <ItemStyle HorizontalAlign="Center" />
                                        <ControlStyle Font-Size="Small" />
                                   </asp:BoundField> 
				                   <asp:BoundField DataField="Shipped"  
                                       HeaderText="Shipped" SortExpression="Shipped">
                                        <ControlStyle Font-Size="Small" />
                                       <ItemStyle HorizontalAlign="Center" />
                                   </asp:BoundField>                 
							    </Columns>
						    </asp:Gridview>
                        </td></tr>
					</div>
					
					</ItemTemplate>
				</asp:TemplateField>                                      
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" CssClass="FrozenHeader" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                       
          
            
        
        </div>
    
         

 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="--Use monsees2

declare @true bit
declare @false bit
SET @true = 1
SET @false = 0

Select * From ProductionViewWP1 ORDER BY LateStartInt" EnableCaching="False" FilterExpression="Job_Number like '{0}%'
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

    <asp:SqlDataSource ID="MonseesSqlDataSourceLoggedin" runat="server"      
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="MonseesSqlDataSourceDeliveries" runat="server"      
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
     <script type="text/javascript">
            function divexpandcollapse(divname) {
                var div = document.getElementById(divname);
                
                if (div.style.display == "none") {
                    div.style.display = "inline";
                    
                } else {
                    div.style.display = "none";
                    
                }
            }
    </script>
</asp:Content>
