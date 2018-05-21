<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CARComplete.aspx.cs" Inherits="Monsees.CARComplete" %>
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
        <asp:MultiView ID="EditableMulti" runat="server" ActiveViewIndex="0"><asp:View ID="EditView" runat="server">
        <table style="width:100%">
            <tr>
                <td class="auto-style1">Initiation Date: </td><td class="auto-style3"><asp:Label ID="InitDateLbl" runat="server" Text="<%# CADetails.InitiationDate %>" style="width:80px" Width="80px"></asp:Label></td>
                <td class="auto-style2">Due Date: </td><td class="auto-style4"><asp:Label ID="ImplDateLbl" runat="server" Text="<%# CADetails.DueDate %>" style="width:80px" Width="80px"></asp:Label></td><td></td>
            </tr>
            <tr>
                <td class="auto-style1">Initiating Employee: </td><td class="auto-style3"><asp:Label ID="InitEmplLbl" runat="server" Width="136px" Text="<%#CADetails.InitEmployee %>" ></asp:Label></td>
                <td class="auto-style2">Implementing Employee: </td><td class="auto-style4"><asp:Label ID="ImplEmplLbl" runat="server" Width="135px" Text="<%#CADetails.ImpEmployee %>" ></asp:Label></td><td></td>
            </tr>
           
            <tr>
                <td class="auto-style1" style="vertical-align:top">Problem Definition: </td><td colspan="4"><asp:Label ID="ProblemCtrl" runat="server" Height="55px" Width="500px" Text="<%#CADetails.Definition %>"></asp:Label></td>
                
            </tr>

            <tr>
                <td class="auto-style1" style="vertical-align:top">Root Cause: </td><td colspan="4"><asp:TextBox ID="RootCtrl" TextMode="MultiLine" Wrap="true" runat="server" Height="55px" Width="500px" Text="<%#CADetails.RootCause %>"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td class="auto-style1" style="vertical-align:top">Corrective Action: </td><td colspan="4"><asp:TextBox ID="CorrectionCtrl" TextMode="MultiLine" Wrap="true" runat="server" Height="55px" Width="500px" Text="<%#CADetails.ImmediateCorrective %>"></asp:TextBox></td>
                
            </tr>
            <tr>
                <td class="auto-style1" style="vertical-align:top">Preventive Action: </td><td colspan="4"><asp:TextBox ID="PreventiveCtrl" TextMode="MultiLine" Wrap="true" runat="server" Height="55px" Width="500px" Text="<%#CADetails.PreventiveAction %>"></asp:TextBox></td>
                
            </tr>
        </table>
        <asp:Button ID="Save" runat="server" OnClick="Save_Click" Text="Save" /><asp:Button ID="Complete" runat="server" OnClick="Complete_Click" Text="Save and Complete" />
        </asp:View>
            <asp:View ID="ViewView" runat="server">
        <table style="width:100%">
            <tr>
                <td class="auto-style1">Initiation Date: </td><td class="auto-style3"><asp:Label ID="Label1" runat="server" Text="<%# CADetails.InitiationDate %>" style="width:80px" Width="80px"></asp:Label></td>
                <td class="auto-style2">Due Date: </td><td class="auto-style4"><asp:Label ID="Label2" runat="server" Text="<%# CADetails.DueDate %>" style="width:80px" Width="80px"></asp:Label></td><td></td>
            </tr>
            <tr>
                <td class="auto-style1">Initiating Employee: </td><td class="auto-style3"><asp:Label ID="Label3" runat="server" Width="136px" Text="<%#CADetails.InitEmployee %>" ></asp:Label></td>
                <td class="auto-style2">Implementing Employee: </td><td class="auto-style4"><asp:Label ID="Label4" runat="server" Width="135px" Text="<%#CADetails.ImpEmployee %>" ></asp:Label></td><td></td>
            </tr>
           
            <tr>
                <td class="auto-style1" style="vertical-align:top">Problem Definition: </td><td colspan="4"><asp:Label ID="Label5" runat="server" Height="55px" Width="500px" Text="<%#CADetails.Definition %>"></asp:Label></td>
                
            </tr>

            <tr>
                <td class="auto-style1" style="vertical-align:top">Root Cause: </td><td colspan="4"><asp:Label ID="Label6" runat="server" Height="55px" Width="500px" Text="<%#CADetails.RootCause %>"></asp:Label></td>
                
            </tr>
            <tr>
                <td class="auto-style1" style="vertical-align:top">Corrective Action: </td><td colspan="4"><asp:Label ID="Label7" runat="server" Height="55px" Width="500px" Text="<%#CADetails.ImmediateCorrective %>"></asp:Label></td>
                
            </tr>
            <tr>
                <td class="auto-style1" style="vertical-align:top">Preventive Action: </td><td colspan="4"><asp:Label ID="Label8" runat="server" Height="55px" Width="500px" Text="<%#CADetails.PreventiveAction %>"></asp:Label></td>
                
            </tr>
        </table>        
        </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
