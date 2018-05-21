<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectionInventory.aspx.cs" Inherits="Monsees.InspectionInventory" MasterPageFile="~/MasterPages/Monsees.Master"    %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>
<asp:Content ContentPlaceHolderID="bodyContent"  runat="server">

    <title>Inspection Equipment Inventory</title>
    <h1>Insepction Equipment Inventory</h1>
     <br />
    <asp:MultiView runat="server" ID="InspInvMultiview" ActiveViewIndex="0"><asp:View runat="server" ID="InspectionView">
    <br /><b><u>Inspection Office Equipment</u></b><br /> 
    
        <asp:Button ID="ShopViewButton" Text="Shop Tools View" runat="server" CommandArgument="ShopView" CommandName="SwitchViewByID" />
    <div style="align-content:flex-start"><table><tr><td>
                               Description: <asp:TextBox ID="DescFilter" runat="server" >
        				       
                               </asp:TextBox>
                               </td>
               <td> Location: <asp:TextBox ID="LocFilter" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td> Gage Type: <asp:Textbox ID="TypeFilter" runat="server" >
        				</asp:Textbox>
                             
                           </td><td> Owner: <asp:Textbox ID="OwnerFilter" runat="server" >
        				</asp:Textbox>                           
                           
                             
                           </td>
        <td> Active: <asp:DropDownList ID="ActiveFilter" AppendDataBoundItems="true" runat="server" >
            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
            <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
        				</asp:DropDownList>                           
                           
                             
                           </td>

        <td> Decomm. Code: <asp:DropDownList ID="DecommFilter" AppendDataBoundItems="true" runat="server" DataValueField="DecommCodeID" DataTextField="DecommissionCode" >            
            <asp:ListItem Text="None Selected" Value="0" Selected="True"></asp:ListItem>
        				</asp:DropDownList>                           
                           
                             
                           </td>
       
        <td><asp:Button ID="updatetable" Text="Update Table" OnClick="btnUpdate_Click" runat="server" />     </td></tr></table>

    </div>
   
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="GaugeID" DataSourceID="SqlDataSource1" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="Serial" HeaderText="Serial #" SortExpression="Serial" />
                <asp:BoundField DataField="Description" HeaderText="Desc." SortExpression="Description" />
                <asp:BoundField DataField="Location" HeaderText="Loc." SortExpression="Location" />
                <asp:BoundField DataField="GageType" HeaderText=".Type" SortExpression="GageType" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                <asp:BoundField DataField="Resolution" HeaderText="Res." SortExpression="Resolution" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="DecommisionCode" HeaderText="Decomm. Code" SortExpression="DecommisionCode" />                
                <asp:BoundField DataField="LastCalibrationDate" HeaderText="Last Cal." SortExpression="LastCalibrationDate" DataFormatString="{0:MM-dd-yyyy}" />                
                <asp:BoundField DataField="CalSequence" HeaderText="Cal Seq (Days)" SortExpression="CalSequence" />
                <asp:BoundField DataField="CalibrationDue" HeaderText="Cal Due" ReadOnly="True" SortExpression="CalibrationDue" DataFormatString="{0:MM-dd-yyyy}" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        
        </asp:View><asp:View runat="server" ID="ShopView">
    
           
    <br /><b><u>Shop Equipment</u></b><br />  <asp:Button ID="Button2" Text="Inspection Tools View" runat="server" CommandArgument="InspectionView" CommandName="SwitchViewByID" />
    <div style="align-content:flex-start"><table><tr><td>
                               Description: <asp:TextBox ID="TextBox1" runat="server" >
        				       
                               </asp:TextBox>
                               </td>
               <td> Location: <asp:TextBox ID="TextBox2" runat="server" >
        				        
                               </asp:TextBox>
                               
                              </td><td> Gage Type: <asp:Textbox ID="Textbox3" runat="server" >
        				</asp:Textbox>
                             
                           </td><td> Owner: <asp:Textbox ID="Textbox4" runat="server" >
        				</asp:Textbox>
                             
                           
                             
                           </td><td> Active: <asp:DropDownList ID="DropDownList1" AppendDataBoundItems="true" runat="server" >
            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
            <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
        				</asp:DropDownList>                           
                           
                             
                           </td>

        <td> Decomm. Code: <asp:DropDownList ID="DropDownList2" AppendDataBoundItems="true" runat="server" DataValueField="DecommCodeID" DataTextField="DecommissionCode" >            
            <asp:ListItem Text="None Selected" Value="0" Selected="True"></asp:ListItem>
        				</asp:DropDownList>                           
                           
                             
                           </td>
        <td><asp:Button ID="Button1" Text="Update Table" OnClick="btnUpdateShop_Click" runat="server" />     </td></tr></table>

    </div>
   
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="GaugeID" DataSourceID="SqlDataSource2" EnableModelValidation="True" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="Serial" HeaderText="Serial #" SortExpression="Serial" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                <asp:BoundField DataField="GageType" HeaderText="Gage Type" SortExpression="GageType" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                <asp:BoundField DataField="Resolution" HeaderText="Resolution" SortExpression="Resolution" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="DecommisionCode" HeaderText="DecommisionCode" SortExpression="DecommisionCode" />
                <asp:BoundField DataField="Name" HeaderText="Owner" SortExpression="Name" />
                <asp:BoundField DataField="LastCalibrationDate" HeaderText="Last Calibration" SortExpression="LastCalibrationDate" DataFormatString="{0:MM-dd-yyyy}" />
                
                <asp:BoundField DataField="CalSequence" HeaderText="CalSequence" SortExpression="CalSequence" />
                <asp:BoundField DataField="CalibrationDue" HeaderText="Calibration Due" ReadOnly="True" SortExpression="CalibrationDue" DataFormatString="{0:MM-dd-yyyy}" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </asp:View></asp:MultiView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>    
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ></asp:SqlDataSource>
   
</asp:Content>
