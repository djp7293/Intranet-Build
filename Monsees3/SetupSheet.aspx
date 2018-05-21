<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SetupSheet.aspx.cs" Inherits="Monsees.SetupSheet" %>
<%@ Register TagPrefix="bdp" Namespace="BasicFrame.WebControls" Assembly="BasicFrame.WebControls.BasicDatePicker" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<script type="text/javascript" src="/Scripts/jquery.valuechanging.js"></script>

    <title>Setup Sheet</title>
    <h1>Setup Sheet - Lot #: <%=JobItemDetails.JobItemID %> (Job <%=JobItemDetails.JobNumber %>)</h1>
    
    <h2>Part # <%=JobItemDetails.PartNumber %>, Rev. <%=JobItemDetails.RevisionNumber %> - <%=JobItemDetails.DrawingNumber %></h2><br />
    
    <style type="text/css">
        @media screen
        {
             #header, #nav, footer, .print
            {
            display:none;
            }
        }
        @media print 
        {
            #header, #nav, footer, .noprint
            {
            display: none;
            }

             #header, #nav, footer, .print
            {
            display: inline-block;
            }

            .style1{
                display:none;
            }

            /* Ensure the content spans the full width */
            #content
            {
            width: 100%; margin: 0; float: none;
            }

            .GridView{
             Border-Color:#999999;
             Border-Style:none;
             Border-Width:1px;
             font-size:xx-small;             
             width:100%;
             
               
             }

            .alternate{
                background-color:white;
            }

            .rowstyle{
                background-color:white;
                  
            }
            
            /* Change text colour to black (useful for light text on a dark background) */
            .lighttext
            {
            color: #000 
            }

            /* Improve colour contrast of links */
            a:link, a:visited
            {
            color: #781351
            }

            
            @page {
                size:portrait;
                font-size:xx-small; 
                margin-left:0.25in;
                margin-right:0.25in;               
                
            }
        }

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
  
 

    <style type="text/css">
        .auto-style1 {
            width: 223px;
        }
        .auto-style2 {
            width: 202px;
        }
    </style>
  
 

    <style type="text/css">
        .auto-style1 {
            width: 79px;
        }
        .auto-style2 {
            width: 77px;
        }
        .auto-style3 {
            width: 132px;
        }
    </style>
  
 

    <style type="text/css">
        .auto-style1 {
            width: 108px;
        }
        .auto-style2 {
            width: 109px;
        }
        .auto-style3 {
            width: 110px;
        }
        .auto-style4 {
            width: 149px;
        }
    </style>
  
 <style type=”text/css”>

    .WordWrap { width:7.5in;word-break : break-all; table-layout:fixed; }
    .col
{
  word-wrap:break-word;
  flex-wrap:wrap;
}
    


</style>

    <style type='text/css'>
P.pagebreakhere {page-break-before: always}
</style>

    <style type="text/css">
        .auto-style2 {
            width: 17px;
        }
        .auto-style3 {
            width: 18px;
        }
        .auto-style4 {
            width: 19px;
        }
        .auto-style5 {
            width: 199px;
        }
        .auto-style6 {
            width: 131px;
        }
    </style>
  
 

    <style type="text/css">
        .auto-style2 {
            width: 101px;
        }
        .auto-style3 {
            width: 102px;
        }
        .auto-style4 {
            width: 103px;
        }
        .auto-style5 {
            width: 104px;
        }
        .auto-style6 {
            width: 105px;
        }
        .auto-style7 {
            width: 170px;
        }
    </style>
  
 

    <style type="text/css">
        .auto-style1 {
            width: 80px;
        }
        .auto-style2 {
            width: 88px;
        }
        .auto-style3 {
            width: 89px;
        }
        .auto-style4 {
            width: 91px;
        }
        .auto-style5 {
            width: 93px;
        }
        .auto-style6 {
            width: 95px;
        }
        .auto-style7 {
            width: 1096px;
        }
    </style>
  
 

    <style type="text/css">
        .auto-style1 {
            width: 141px;
        }
        .auto-style2 {
            width: 142px;
        }
        .auto-style3 {
            width: 143px;
        }
        .auto-style4 {
            width: 145px;
        }
        .auto-style5 {
            width: 146px;
        }
        .auto-style6 {
            width: 147px;
        }
        .auto-style8 {
            width: 216px;
        }
        .auto-style9 {
            width: 417px;
        }
    </style>
  
 

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:7.5in">
   
    
      
        <table style="width:100%">
           
            <tr style="height:35px">
                <td class="auto-style6" style="vertical-align:central; width:200px" ><strong>Demand Quantity: </strong> </td><td class="auto-style5"><%=SetupDetails.Quantity %></td>
                
            </tr>
            
            <tr style="height:35px">
                <td class="auto-style6" style="vertical-align:central"><strong>Operation: </strong></td><td style="vertical-align:central" class="auto-style5"><%=SetupDetails.OperationName %></td><td class="auto-style4" style="vertical-align:central"><strong>Description: </strong></td><td style="vertical-align:central"><input type="text" name="DescBox" value="<%=SetupDetails.Description %>"  style="width:300px;"/></td>
                
            </tr>
            

           
        </table>
        
      
        <br /> <b><u>Setup Comments (persists from lot-to-lot):</u></b><br/>
                               <asp:GridView CssClass="WordWrap" ID="SetupEntries" runat="server"  AutoGenerateColumns="false" BackColor="White" DataKeyNames="SetupEntryID" EnableModelValidation="True" width="98%">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="Employee" SortExpression="Name" >
                                            <ItemStyle Font-Size="XX-Small" Width="1in" />
                                            </asp:BoundField>
                                        <asp:BoundField DataField="Entry" HeaderText="Comment" SortExpression="Entry"  >
                                            <ItemStyle Wrap="true" Width="5.5in" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Timestamp" HeaderText="Time" SortExpression="Timestamp"  ItemStyle-Width="1in">
                                            <ItemStyle Font-Size="XX-Small" />
                                            </asp:BoundField>
                                    </Columns>
                                    <RowStyle Wrap="true" />
                                </asp:GridView>
                                <br />
                                <table class="noprint">
                                    <tr>
                                        <td><b><u><span style="font-size:smaller">Employee</span></u></b></td>
                                        <td><b><u><span style="font-size:smaller">Entry</span></td>
                                    </tr>
                                    <tr>
                                        <td><asp:DropDownList runat="server" ID="EmployeeCommentDrop" DataValueField="EmployeeID" DataTextField="Name" Height="19px" Width="135px"></asp:DropDownList></td>
                                        <td><asp:TextBox runat="server" ID="EntryText" Width="557px" Height="31px" Wrap="true" TextMode="MultiLine" ></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    <asp:Button runat="server" ID="CommentButton" Text="Add comment" OnClick="CommentButton_Click"/>
                                  
        <br />
        
        <br />
        <h3>Fixture(s) for Setup</h3>
        <asp:GridView ID="GridView1" runat="server" Width="706px" EnableModelValidation="True" >
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>                
               
            </Columns>
        </asp:GridView>
        <br />
        <div class="noprint">
        <asp:button ID="Fixtures" runat="server" onclick="SeeFixtures_Click" 
                        Text="Quick Fixture" />
        <br /></div>
        <h3>Special Tooling for Setup</h3>
        <asp:GridView ID="GridView2" runat="server" Width="705px">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>                
               
            </Columns>
        </asp:GridView>
        <br />
        <p class="pagebreakhere"></p>
        <div class="print">
             <h1>Setup Sheet - Lot #: <%=JobItemDetails.JobItemID %> (Job <%=JobItemDetails.JobNumber %>)</h1>
    
    <h2>Part # <%=JobItemDetails.PartNumber %>, Rev. <%=JobItemDetails.RevisionNumber %> - <%=JobItemDetails.DrawingNumber %></h2><br />
    
        <table style="width:100%" >
           
            <tr style="height:35px">
                <td class="auto-style6" style="vertical-align:central; width:200px" ><strong>Demand Quantity: </strong> </td><td class="auto-style5"><%=SetupDetails.Quantity %></td>
                
            </tr>
            
            <tr style="height:35px">
                <td class="auto-style6" style="vertical-align:central"><strong>Operation: </strong></td><td style="vertical-align:central" class="auto-style5"><%=SetupDetails.OperationName %></td><td class="auto-style4" style="vertical-align:central"><strong>Description: </strong></td><td style="vertical-align:central"><input type="text" name="DescBox" value="<%=SetupDetails.Description %>"  style="width:300px;"/></td>
                
            </tr>
            

           
        </table>
            </div>
        <h3>Process Sheet</h3>
        <asp:MultiView ID="ProcessMultiView" runat="server" ActiveViewIndex="0">
            <asp:View ID="StartProcessView" runat="server">
               <table style="width:100%">
                   <tr>
                       <td>Est. # of Tools: <asp:TextBox runat="server" ID="NumToolSlots" Width="50px" /></td>
                       <td>Machinist: <asp:DropDownList ID="EmployeeDropDown" runat="server" DataTextField="Name" DataValueField="EmployeeID" Width="150px"></asp:DropDownList></td>
                       <td>Machine: <asp:DropDownList ID="MachineDropDown" runat="server" DataTextField="Machine" DataValueField="MachineID" Width="150px"></asp:DropDownList></td>
                       <td><asp:Button ID="StartProcButton" runat="server" CommandArgument="ProcessView"  CommandName="SwitchViewByID" OnCommand="StartProcButton_Command" Text="Start Process" /></td>
                   </tr></table>   
                 
            </asp:View>
            <asp:View ID="ProcessView" runat="server">
                <table   id="setupInfoForm">
                    <tr style="height:35px">
                <td class="auto-style6" style="vertical-align:central"><strong>Setup Machinist: </strong></td><td style="vertical-align:central"><%=ProcessDetails.Name %></td>
                <td class="auto-style6" style="vertical-align:central"><strong>Setup Machine: </strong></td><td style="vertical-align:central"><%=ProcessDetails.Machine %></td>
                </tr>
            
                <tr style="height:35px">
                    <div>
                    <td class="auto-style6" style="vertical-align:central"><strong>Process-specific setup information:</strong> </td><td style="vertical-align:central" colspan="3"><input name="Comments" type="text" style="Height:55px;Width:500px" value="<%=SetupDetails.Comments %>"/></td>
                    </div>
                </tr>
                <tr><td colspan="4">
                <table class="report-body" id="worksheetForm">
	                <thead>
		                <tr>
			                <td class="dimension-number" style="width:50px"><strong><u>Tool #</u></strong></td>
			                <td class="dimension-label" style="width:150px"><strong><u>Tool Name</u></strong></td>
			                <td class="dimension" style="width:450px"><strong><u>Tool Details</u></strong></td>			
		                </tr>
	                </thead>
	            <tbody>
		        <% foreach(Monsees.DataModel.SetupWorksheetView w in WorksheetData) { %>
		        <tr class="inspection-item" style="padding:initial;" data-id="<%=w.SetupWorksheetItemID %>" >
			        <td class="dimension">
                        <input type="text" name="toolnumber" value="<%=w.ToolNumber %>" style="width:50px;" /><br />
                
			        </td>
			        <td class="dimension">
                        <input type="text" name="toolname" value="<%=w.ToolName %>" style="width:150px;" /><br />
                
			        </td>
			        <td class="thick-left dimension">
                        <input type="text" name="tooldetails" value="<%=w.ToolDetails %>" style="width:450px;"  /><br />
               
			        </td>
			
		                </tr>
		                <% } %>
	                </tbody>
                </table>
                    </td></tr>
                </table>
            </asp:View>
        </asp:MultiView>
    
    <br />
        <div class="noprint" style="border:thick">
        <strong>Close / &quot;Log&quot; Process</strong><div>
        <table class="style3" >
            <tr>
                <td class="auto-style9">
                    Employee</td>
                <td colspan="2">
                    <asp:DropDownList ID="EmployeeDropList" runat="server" Height="20px" 
                        Width="150px" DataTextField="Name" DataValueField="EmployeeID">
                    </asp:DropDownList>
                </td>
            
                <td class="auto-style8">
                    Setup</td>
                <td>
                    <asp:DropDownList ID="SetupsDropDownList" runat="server" Height="20px" 
                        Width="150px"  DataTextField="OperationName" DataValueField="JobSetupID">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">
                    Quantity In&nbsp;&nbsp;
                    
                </td>
                <td class="auto-style2" colspan="2">
                    <asp:TextBox ID="QuanityIn" runat="server" Width="80px"></asp:TextBox>
                </td>
                
            
                <td class="auto-style8">
                    Quantity Out
                    
                </td>
                <td class="style8">
                    <asp:TextBox ID="QuanityOut" runat="server" Width="80px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="auto-style9">
                    Hours</td>  
                <td colspan="2">
                    <asp:TextBox ID="Hours" runat="server" Width="80px"></asp:TextBox>
                
                    <asp:RegularExpressionValidator ID="HoursValidator" runat="server" 
                        ControlToValidate="Hours" ErrorMessage="Only numbers allowed" 
                        ValidationExpression="^[0-9]*[.]?[0-9]+$"></asp:RegularExpressionValidator>
                </td>
           
                <td class="auto-style8">
                    Program # (optional)</td>
                <td class="auto-style2">
                    <asp:TextBox ID="ProgramNum" runat="server" Width="125 px"></asp:TextBox>
                </td>
</tr>

            <tr>
                <td class="auto-style9">
                    Complete Operation</td>
                <td>
                    <asp:CheckBox ID="CheckMoveOn" Checked="True" runat="server" Width="226px"></asp:CheckBox>
                </td>
                
            
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style8" style="vertical-align:central">
                    <asp:Button ID="SaveButton" runat="server" onclick="SaveButton_Click" 
                        Text="Close Process" />
               
       
                </td>
                <td class="auto-style3">
                     <asp:Label ID="ResultMsg" runat="server" Text="Label" Font-Size="Large" 
            ForeColor="#990000"></asp:Label>
                                   
                </td>
            </tr>
        </table></div>    
    </div>

    </div>

     <asp:SqlDataSource ID="SetupEntrySource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
       
    </asp:SqlDataSource>
    <script >
    var inventoryService = new CRUDService(
{
    baseUrl: '/InspectionService.aspx',

    setToolDetails: function (SetupWorksheetItemID, column, ToolNumber, ToolName, ToolDetails) {
 
        this.sendAjax({
            url: "UpdateToolDetails",
            data: {
                    ProcessID: <%=ProcessID%>,
                    SetupWorksheetItemID: SetupWorksheetItemID,
                    column: column,
                    ToolNumber: Number(ToolNumber),
                    ToolName: ToolName,
                    ToolDetails: ToolDetails,                    
                   },
            success: function () {
                cb();
            }
        });
    },

     setSetupInfo: function (Comments) {
 
        this.sendAjax({
            url: "UpdateJobSetupInfo",
            data: {
                    JobSetupID: <%=JobSetupID%>,    
                    Comments: Comments,                    
                   },
            success: function () {
                cb();
            }
        });
    },

    
});

    $('#worksheetForm input').listenForValueChanging().bind('change valueChanging', function (e) {
        //inspection item TR
        var $toolRow = $(e.target).parent().parent(),
            SetupWorksheetItemID = $toolRow.attr('data-id');        
        clearTimeout($toolRow.data('saveTimeout'));
        $toolRow.addClass('dirty');
        $toolRow.data('saveTimeout', setTimeout(function () {
            inventoryService.setToolDetails(SetupWorksheetItemID, $(e.target).attr('name'),
                                                 $toolRow.find("[name='toolnumber']").val(),
                                                 $toolRow.find("[name='toolname']").val(),
                                                 $toolRow.find("[name='tooldetails']").val(), function(){
                                                     $toolRow.removeClass('dirty');   
                                                 });
        
        }, 1000));
        
    }).keyup(function (e, n) {
        if (e.keyCode == 13) {
            e.preventDefault();
        };
    });

    $('#setupInfoForm input').listenForValueChanging().bind('change valueChanging', function (e) {
        
           
        clearTimeout($toolRow.data('saveTimeout'));
        $comment.addClass('dirty');
        $comment.data('saveTimeout', setTimeout(function () {
            inventoryService.setSetupInfo($(e.target).attr('value'), function(){
                                                     $toolRow.removeClass('dirty');   
                                                 });
        
        }, 1000));
        
    }).keyup(function (e, n) {
        if (e.keyCode == 13) {
            e.preventDefault();
        };
    });
 
   
</script>
</asp:Content>
