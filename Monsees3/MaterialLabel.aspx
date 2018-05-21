<%@ Page Title="" Language="C#" MasterPageFile="~/Print.master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="Monsees.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 110px;
            font-size:smaller;
        }
        .auto-style2 {
            width: 26px;
            font-size: smaller;
        }
        .auto-style3 {
           
            width: 70px;
            height: 38px;
            font-size:smaller;
        }
        .auto-style4 {
            width: 97%;
        }
        .auto-style5 {
            float: left;
            width: 161px;
        }
        .auto-style6 {
            width: 30px;
        }
        .auto-style7 {
            width: 44px;
            font-size: smaller;
        }
        .auto-style8 {
            width: 163px;
        }
        .auto-style9 {
            width: 1.75in;
            height: 1.44in;
        }
        .auto-style10 {
            width: 1.75in;
            height: 1.64in;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style9"><div class="auto-style10">
        <table class="auto-style4">
            <tr><td class="auto-style7">Lot ID: </td><td class="auto-style1"><%=MaterialData.MatPriceID %></td></tr>
            <tr><td class="auto-style7">Material: </td><td class="auto-style1"><%=MaterialData.Material %></td></tr>
            <tr><td class="auto-style7">Vendor: </td><td class="auto-style1"><%=MaterialData.VendorName %></td></tr>
        </table>
       
        
        <div class="auto-style5">
             <table class="auto-style8">
                <tr><td class="auto-style2">Size: </td><td class="auto-style1"><%=MaterialData.Size %></td></tr>   
                <tr><td class="auto-style2">Len: </td><td class="auto-style1"><%=MaterialData.Length %></td>
                <td class="auto-style2">Qty: </td><td class="auto-style1"><%=MaterialData.Quantity %></td></tr>
            </table>
        </div>
         <div class="auto-style3">
            
            <table>
                <tr style="font-family:'Free 3 of 9'; font-size:36pt"><td class="auto-style6">*<%=MaterialData.MatPriceID %>*</td></tr>
            </table>
        </div>
    </div>
    </div>
</asp:Content>
