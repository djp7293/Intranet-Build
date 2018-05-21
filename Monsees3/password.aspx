<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultInv.aspx.cs" Inherits="Monsees.Inventory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<head runat="server">
    <title>Monsees Tool & Die Inc.</title>
    <style type="text/css">
        .style1
        {
            width: 28%;
            height: 33px;
        }
        .style2
        {
            width: 83px;
        }
        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 540px;
        }
        .style5
        {
            width: 215px;
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
    
        <asp:Image ID="Image1" runat="server" ImageUrl="images/header01_mac_002.jpg" 
            ImageAlign="Middle" />
                <br />
        <br />
        <br />
        <br />
        <table class="style3">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    <table class="style3">
        <tr>
            <td width="33%">
                &nbsp;</td>
            <td class="style5">
        <table class="style1">
            <tr>
                <td class="style2">&nbsp;Username</td>
                <td class="style4">
                    <asp:TextBox ID="CompanyTextBox" runat="server" Width="135px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">&nbsp;Password</td>
                <td class="style4">
                    <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
 		<tr>
                <td class="style2">Forgot Username</td>
                <td class="style4">
                    <a href="password.aspx">Forgot Password</a>
                </td>
            </tr>
        </table>
<p>Please contact Kathy Tarver at 585-262-4180 x301 or kathy.hunt@monseestool.com for account registration and registration information. </p>
                </td>
                <td>
    <asp:Button ID="LoginButton" runat="server" onclick="LoginButton_Click" 
        Text="Log in" />
                </td>
            </tr>
        </table>
    <asp:Panel ID="LoginPanel" runat="server">
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="46px" HorizontalAlign="Center">
        <asp:Label ID="ErrorMsg" runat="server" Text="Label" Font-Size="Large" 
            ForeColor="#990000"></asp:Label>
    </asp:Panel>
	<br />
   
    </form>
</body>
</html>

