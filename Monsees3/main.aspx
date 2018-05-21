<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Monsees._main"%>

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
    
        <table class="style1" width="70%">
            <tr>
                <td align="left" valign="middle">
                    &nbsp;</td>
                <td align="left" valign="bottom">
    
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
                <asp:Button ID="LogoutButton" runat="server" Text="Log Out" 
                        onclick="LogoutButton_Click" />
                    </td>
            </tr>
        </table>
	
<table  class="style1" width="100%" align="left">
<tr><td align="left" width="50px">&nbsp;</td>
                <td align="Left" valign="middle" >
                    <Font Face="Century Gothic" size="5" color="#420505"><b><% =CompanyName %> - Customer Information Portal</b></Font><br><br></td>
                <td align="right" valign="middle">
                    &nbsp;</td>
                <td>
               
                    </td>

            </tr>

        </table>
<table align="left"><tr><td width="50px" align="left">&nbsp;</td><td align="left">
<asp:Panel ID="Panel2" runat="server" align="left">
                         <asp:GridView ID="LinkGrid" runat="server" AllowPaging="True" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSourcePermissions" GridLines="Vertical" 

EnableSortingAndPagingCallbacks="False"
			       EmptyDataText="You have no resource permissions for this site.  Please see your company relationship manager to sign up for specific permissions."  
                               PageSize="50" Width="640" 
                               AllowSorting="False" 
                               >
                               <RowStyle BackColor="#EEEEEE" Font-Size="Small	" ForeColor="Black" />
                               <Columns>
				<asp:TemplateField HeaderText="Available Resources">
    <ItemTemplate>
      <asp:LinkButton ID="LinkText" runat="server" OnClick="LinkButton1_Click" CommandArgument='<%#Eval("Link") %>' Text='<%# Bind("Description") %>' ></asp:LinkButton>
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
</td></tr><tr><td width="50px" align="left">&nbsp;</td></tr></table>
<asp:SqlDataSource ID="MonseesSqlDataSourcePermissions" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" >
 </asp:SqlDataSource>
</form>

</td>
                                 <td align="right"></td>
                              </tr>
                           <tr>
                              <td>
                                 <h2><font size="3"></font>&nbsp;</h2></td>
                           </tr>
                           </tbody>
                        </table>
                
</body>
</html>
