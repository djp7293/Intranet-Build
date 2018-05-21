<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToPO.aspx.cs" Inherits="Monsees.AddToPO" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
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
    <form id="form1" method="post" runat="server" enctype="multipart/form-data">
    <div align="center">
    
        <asp:Image ID="Image1" runat="server" ImageUrl="images/header01_mac_002.jpg" 
            ImageAlign="Middle" />
                <br/>
        <br/>
        <br/>
        <br/>
        <table class="style3">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
<table>
<tr><td align="left">
No Of Records : <asp:TextBox ID="txtNo" runat="server" width="30px"></asp:TextBox><asp:Button ID="btnSubmit" runat="server"  Text="Submit" OnClick="btnSubmit_Click"/></td></tr>
<tr><td colspan="3">
        <div>
	
    
    <br />
    <asp:GridView ID="GridView1" runat="server">
<RowStyle BackColor="#EEEEEE" Font-Size="Small" ForeColor="Black" />
        <Columns>
            <asp:TemplateField HeaderText="Service">
                <ItemTemplate>
                     
                    <asp:DropDownList ID="ServiceDropDownList1" runat="server" Width="150px" DataSourceID="ServiceDataSource"  DataTextField="Service" DataValueField="ServiceId"></asp:DropDownList>
            
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Part Number">
                <ItemTemplate>
                          
                    <asp:TextBox ID="PartNumber1" runat="server" Width="226px">NA</asp:TextBox>
               
                </ItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="Rev #">
                <ItemTemplate>
                          
                    <asp:TextBox ID="RevNumber1" runat="server" Width="50px">NA</asp:TextBox>
</ItemTemplate>
            </asp:TemplateField>
              
	    <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                          
                    <asp:TextBox ID="Description1" runat="server" Width="226px"></asp:TextBox>
</ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                          
                    <asp:TextBox ID="Quantity1" runat="server" Width="50px"></asp:TextBox>
</ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Requested Delivery">
                <ItemTemplate>
                          
                    <bdp:BDPLite ID="Delivery1" OnSelectionChanged="DeliveryChange" AutoPostBack="True" runat="server"></bdp:BDPLite>
                </ItemTemplate>
            </asp:TemplateField>
 		<asp:TemplateField HeaderText="Attach File">
                <ItemTemplate>
		<asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
<HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
    </asp:GridView>
    
    

</div>
<tr>
<td>&nbsp;</td></tr>
<tr><td align="left"><input id="filMyFile" type="file" runat="server"><br></td></tr>
<tr>
<td width="70px">E-mail Body:</td></tr><tr><td align="Left"><asp:TextBox ID="EmailText" runat="server" width="800px" TextMode="Multiline" rows="10"></asp:TextBox></td></tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                &nbsp;
        <asp:Label ID="ResultMsg" runat="server" Text="Label" Font-Size="Large" 
            ForeColor="#990000"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>


	<asp:SqlDataSource ID="ServiceDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        
        
        
        
        
        
        
        SelectCommand="Select [ServiceID],Service from [Services] where Active = 1 ORDER BY Service ASC"> </asp:SqlDataSource>

    </form>
</body>
</html>
