<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Monsees.Master" CodeBehind="QuickFixture.aspx.cs" Inherits="Monsees.QuickFixture" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

<title><%=JobDetailModel.PartNumber %> Fixturing</title>
<link rel="stylesheet" type="text/css" href="/css/lot.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
 

<div class="lot">

<div class="header">
	<div class="title">Lot #<%=SourceLot %> - <%=JobDetailModel.DrawingNumber %> Add Fixture(s)</div>

	

	<div style="clear:both;height:0px">&nbsp;</div>
</div>

<div class="summary">

<div class="part" >
	Part #<%=JobDetailModel.PartNumber %>
	<div class="comments">
		<span class="label">Comments:</span>
		<span><%=JobDetailModel.Comments %></span>
	</div>

</div>

 <div class="job">
	<div><%= JobDetailModel.CompanyName %></div>
	<div>Job #<%= JobDetailModel.JobNumber%></div>
</div></div>
    <div>

<table>
<tr><td>
        
	
    
    <br />
    <asp:FormView ID="FormView1" runat="server" OnRowDataBound="GridView1_RowDataBound" EnableModelValidation="True" Width="427px">
<RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
        
            
                <ItemTemplate>
                     <table> 
                            <tr><td><table><tr><td>Required For: </td><td colspan="2">
                    <asp:DropDownList ID="OperationDropDownList" runat="server" Width="120px" DataTextField="Label" DataValueField="SetupID"></asp:DropDownList>
               </td></tr>                           
                         <tr><td>Part #: </td><td colspan="2">
                    <asp:TextBox ID="PartNumber1" runat="server" Width="120px">NA</asp:TextBox>
               </td></tr>
           
              <tr><td>Description: </td><td colspan="2">
              
	    
                
                          
                    <asp:TextBox ID="Description1" runat="server" Width="150px"></asp:TextBox>
                
            </td></tr>
           
              
           
              
              <tr><td>Owner: </td><td colspan="2">
                      
                    <asp:DropDownList ID="Owner" runat="server" Width="100px"   DataTextField="ContactName" DataValueField="ContactID"></asp:DropDownList>
          
            
           </td></tr>
           
              <tr><td>Material: </td><td colspan="2">
               
                         
                    <asp:DropDownList ID="Material" runat="server" Width="100px"  DataTextField="Material" DataValueField="MaterialID" AppendDataBoundItems="true"><asp:ListItem Text="NA" Value="0"></asp:ListItem></asp:DropDownList>
              
            
            </td></tr>
           
              
           
              <tr><td>Drawing/File: </td><td colspan="2">
		            <asp:FileUpload id="filMyFiletest" runat="server" Width="150px"></asp:FileUpload>
                         
                    </td>
                
                <td class="style7">
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
             
                </td>
                
            </tr> 

                         <tr><td>Location: </td><td colspan="2">
               
                
                          
                    <asp:TextBox ID="Location" runat="server" Width="50px"></asp:TextBox>
               
            
                </td></tr>

                         <tr><td>Notes: </td><td colspan="2">
               
                
                          
                    <asp:TextBox ID="Notes" runat="server" Width="250px"></asp:TextBox>
               
            
                </td></tr>

                     </table> </td></tr></table>
                </ItemTemplate>
          
        
<HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
    </asp:FormView>
    
    </td></tr>


          
        </table>
    </div>
  <asp:SqlDataSource ID="MonseesSqlDataSourceFixtureOrders" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>
    
      <asp:SqlDataSource ID="MonseesSqlDataSourceFixtureInventory" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  EnableCaching="False">
        
 </asp:SqlDataSource>

</asp:Content>
