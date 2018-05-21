<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Monsees.Master" CodeBehind="Login.aspx.cs" Inherits="Monsees.Login" %>


<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    
    
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
    <div>
        <table class="style3" title="Monsees Tool &amp; Die Inc.">
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
                    Setup</td>
                <td>
                    <asp:DropDownList ID="SetupsDropDownList" runat="server" Height="20px" 
                        Width="225px"  DataTextField="OperationName" DataValueField="JobSetupID">
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
                <td>
                    <asp:Button ID="SaveButton" runat="server" onclick="SaveButton_Click" 
                        Text="Save" />
                &nbsp;
        <asp:Label ID="ResultMsg" runat="server" Text="Label" Font-Size="Large" 
            ForeColor="#990000"></asp:Label>
                </td>
                <td>
                    
                    <asp:button ID="Fixtures" runat="server" onclick="SeeFixtures_Click" 
                        Text="Inventory Fixture(s)" />               
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>