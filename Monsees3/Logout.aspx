<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Monsees.Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<head runat="server">
    <title>Monsees Tool &amp; Die Inc.</title>
    <style type="text/css">


        .style3
        {
            width: 100%;
        }
        .style4
        {
            width: 165px;
        }
        .style5
        {
            width: 165px;
            height: 22px;
        }
        .style6
        {
            height: 22px;
        }
        .style7
        {
            width: 244px;
        }
        .style8
        {
            height: 22px;
            width: 244px;
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
                <td class="style4">
                    Employee</td>
                <td>
                    <asp:DropDownList ID="EmployeeList" runat="server" Height="20px" 
                        Width="225px" DataTextField="Name" DataValueField="EmployeeID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Quantity In&nbsp;&nbsp;
                    
                </td>
                <td class="style7">
                    <asp:TextBox ID="QuanityIn" runat="server" Width="226px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style5">
                    Quantity Out
                    
                </td>
                <td class="style8">
                    <asp:TextBox ID="QuanityOut" runat="server" Width="226px"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td class="style4">
                    Hours</td>
                <td class="style7">
                    <asp:TextBox ID="Hours" runat="server" Width="226px"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="HoursValidator" runat="server" 
                        ControlToValidate="Hours" ErrorMessage="Only numeric values are allowed" 
                        ValidationExpression="^[0-9]*[.]?[0-9]+$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Program # (optional)</td>
                <td class="style7">
                    <asp:TextBox ID="ProgramNum" runat="server" Width="226px"></asp:TextBox>
                </td>
</tr>
<tr>
                <td class="style4">
                    Complete Operation</td>
                <td class="style7">
                    <asp:CheckBox ID="CheckMoveOn" Checked="True" runat="server" Width="226px"></asp:CheckBox>
                </td>
                
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Button ID="SaveButton" runat="server" onclick="SaveButton_Click" 
                        Text="Save" />
                &nbsp;
        <asp:Label ID="ResultMsg" runat="server" Text="Label" Font-Size="Large" 
            ForeColor="#990000"></asp:Label>
                </td>
                 <td class="style7">
                     <asp:Button ID="Fixtures" runat="server" onclick="SeeFixtures_Click" 
                        Text="Inventory Fixture(s)" />
                &nbsp;
       
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </form>
   
</body>
</html>
