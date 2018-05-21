<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CARInitiate.aspx.cs" Inherits="Monsees.WebForm4" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Corrective Action Initiation</title>
    <h1>Corrective Action Initiation</h1>
    <h2>Lot #: <%=JobItemDetails.JobItemID %></h2>
    <h2><%=JobItemDetails.PartNumber %> - <%=JobItemDetails.DrawingNumber %></h2>
    
    <style type="text/css">
        .auto-style1 {
            width: 1.5in;
        }
        .auto-style2 {
            width: 2in;
        }
        .auto-style3 {
            width: 1.5in;
        }
        .auto-style4 {
            width: 2in;
        }
    </style>
  
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:7.5in">
        <table style="width:100%">
            <tr>
                <td class="auto-style1">Customer CAR: </td><td class="auto-style3"><asp:checkbox checked="false" ID="CustCarCtrl" runat="server"></asp:checkbox></td>
                <td class="auto-style2">Customer CAR#: </td><td class="auto-style4"><asp:TextBox ID="CustNumCtrl" runat="server" style="width:80px" Width="80px"></asp:TextBox></td><td></td>
            </tr>
            <tr>
                <td class="auto-style1">Initiation Date: </td><td class="auto-style3"><bdp:BDPLite ID="InitDateCtrl" OnSelectionChanged="DeliveryChange" AutoPostBack="True" runat="server" style="width:80px" Width="80px"></bdp:BDPLite></td>
                <td class="auto-style2">Due Date: </td><td class="auto-style4"><bdp:BDPLite ID="DueDateCtrl" OnSelectionChanged="DeliveryChange" AutoPostBack="True" runat="server" style="width:80px" Width="80px"></bdp:BDPLite></td><td></td>
            </tr>
            <tr>
                <td class="auto-style1">Initiating Employee: </td><td class="auto-style3"><asp:DropDownList ID="InitEmplCtrl" runat="server" DataValueField="EmployeeID" DataTextField="Name" Width="136px" ></asp:DropDownList></td>
                <td class="auto-style2">Implementing Employee: </td><td class="auto-style4"><asp:DropDownList ID="ImplEmplCtrl" runat="server" DataValueField="EmployeeID" DataTextField="Name" Width="135px" ></asp:DropDownList></td><td></td>
            </tr>
           
            <tr>
                <td class="auto-style1" style="vertical-align:top">Problem Definition: </td><td colspan="4"><asp:TextBox ID="ProblemCtrl" runat="server" Height="55px" Width="500px"></asp:TextBox></td>
                
            </tr>
        </table>
        <asp:Button ID="Initiate" runat="server" OnClick="Initiate_Click" Text="Initiate CAR" />
    </div>
</asp:Content>
