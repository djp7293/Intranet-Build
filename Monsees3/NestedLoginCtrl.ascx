<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NestedLoginCtrl.ascx.cs" Inherits="Monsees.NestedLoginCtrl" %>

<style type="text/css">
    .tabletest thead {
        display: table-header-group;
    }
</style>


        
        <div style="background-color: #eeeeee; border: solid 1px #777777; padding: 10px; display: inline-block;">
            <b><u>Operations:</u></b>
            <br />
            <table>
               
	            <thead style="background-color:#000084; color:white; font-weight: bold; display:table-header-group; border:solid; border-width:1px" >
		            <tr>
			            <td  id="th1" style="width:30px">+/-</td>
			            <td  id="th2" style="width:20px">#</td>
			            <td  id="th3" style="width:150px">Description</td>
			            <td  id="th4" style="width:50px">Setup</td>
			            <td  id="th5" style="width:80px">Run(min)</td>
                        <td id="th6" style="width:200px">Comments</td>                        
                        <td  id="th7" style="width:50px">Done</td>			            
			            <td  id="th8" colspan="3"></td>
                        
		            </tr>
	            </thead>
                <tbody id ="mainoperationstable" style="border:solid; border-width:1px">
		            <% foreach(Montsees.Data.DataModel.OperationDetailedModel r in Setups) {
                            GetRowData1(r.JobSetupID, r.SetupID);
                            %>
                    
		            <tr><td colspan="10">
                        <table style="border:solid; border-width:1px"><tr id="setup<%=r.JobSetupID %>"><td class="Expander" headers="th1" style="width:30px; font-size:small"><a href="JavaScript:divexpandcollapse('div<%=r.JobSetupID %>', '<%=r.JobSetupID %>');">+/-</a></td>
                    <td headers="th2" style="width:20px"> <%=r.ProcessOrder %> </td>
			            <td headers="th3" style="width:150px"><%=r.Label %> </td>
			            <td headers="th4" style="width:50px"><%=r.Setup_Cost %></td>
			            <td headers="th5" style="width:80px"><%=r.Operation_Cost %></td>
			            <td headers="th6" style="width:200px"><%=r.Comments %> </td>
                        <td headers="th7" style="width:50px"><%=r.Completed %></td>
                        <td headers="th8" style="font-size:small"><a href="JavaScript:createfixture('<%=r.JobSetupID %>');">Order Fixture</a></td>
                        <td headers="th9" style="font-size:small"><a href="JavaScript:quickfixture('<%=r.JobSetupID %>');">Quick Fixture</a> </td>
                        <td headers="th10" style="font-size:small"><a href="JavaScript:setupsheet('<%=r.JobSetupID %>');">Setup Sheet</a> </td></tr>
                        <tr><td colspan="10"><div id='div<%=r.JobSetupID %>' style="display:none; font-size:small">
                            <table>
                                <tr><td><u><b>Active Log:</b></u></td></tr>
                              <%--  <tr>
                                    <td>
                                        <table style="border:solid; border-width:1px">
                                            <thead style="background-color:#000084; color:white; font-weight: bold; display:table-header-group;" >
                                                <tr>
                                                    <td>Employee</td>
                                                    <td>Machine</td>
                                                    <td>In</td>
                                                    <td>Out</td>
                                                    <td>Hours</td>
                                                    <td>Rework</td>
                                                    <td>Notes</td>
                                                    <td>In Time</td>
                                                    <td>Out Time</td>
                                                    <td>Done</td>
                                                </tr>
                                            </thead>
                                            <tbody id="logtable<%=r.JobSetupID %>">
                                                <% foreach (Montsees.Data.DataModel.SetupLogModel s in SetupLogs)
                                                    { %>
                                                <tr id="processline<%=s.ProcessID %>">
                                                    <td><%=s.Name %></td>
                                                    <td><%=s.MachineID %></td>
                                                    <td><%=s.QuantityIn %></td>
                                                    <td><%=s.QuantityOut %></td>
                                                    <td><%=s.Hours %></td>
                                                    <td><%=s.Fix %></td>
                                                    <td></td>
                                                    <td><%=s.Login %></td>
                                                    <td><%=s.Logout %></td>
                                                    <td></td>
                                                </tr> <% } %> 
                                                <tr><td><button id="addlogbutton">Test</button> </td></tr>                                                
                                            </tbody>
                                        </table>
                                        
                                    </td>
                                </tr>--%>
                                <tr><td><u><b><br />Pictures:</b></u></td></tr>
                                <tr>
                                    <td>
                                        <table >
                                            <tr><td>
                                                <asp:FileUpload id="filMyFiletest" runat="server"></asp:FileUpload><br /><asp:Button runat="server" CommandName="Attach" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Attach" />
                                            </td>
                                            <td>
                                                   </td></tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td><u><b><br />Fixtures:</b></u></td></tr>
                                <tr>
                                    <td>
                                        <table style="border:solid; border-width:1px">
                                            <thead style="background-color:#000084; color:white; font-weight: bold; display:table-header-group;" >
                                                <tr>
                                                    <td>Part Number</td>
                                                    <td>Description</td>
                                                    <td>Qty</td>
                                                    <td>Owner</td>
                                                    <td>Location & Notes</td>                                                    
                                                </tr>
                                            </thead>
                                            <tbody id="fixtures<%=r.SetupID %>">
                                            <% foreach (Montsees.Data.DataModel.SetupFixturesModel t in SetupFixtures)
                                                { %>
                                                <tr id="fixture<%=t.FixtureRevID %>">
                                                    <td><%=t.PartNumber %></td>
                                                    <td><%=t.DrawingNumber %></td>
                                                    <td><%=t.Quantity %></td>
                                                    <td><%=t.ContactName %></td>
                                                    <td></td>
                                                </tr>
                                                <% } %>
                                            </tbody>
                                        </table>                                        
                                    </td>
                                </tr>
                                <tr><td><u><b><br />Setup History:</b></u></td></tr>
                               <%-- <tr>
                                    <td>
                                        <table style="border:solid; border-width:1px">
                                            <thead style="background-color:#000084; color:white; font-weight: bold; display:table-header-group;" >
                                                <tr>
                                                    <td>Lot #</td>
                                                    <td>Job #</td>
                                                    <td>Name</td>
                                                    <td>Machine</td>
                                                    <td>Qty</td>
                                                    <td>In</td>
                                                    <td>Out</td> 
                                                    <td>Hrs</td>
                                                    <td>Timestamp</td>                                                   
                                                </tr>
                                            </thead>
                                            <tbody id="history<%=r.SetupID %>">
                                            <% foreach (Montsees.Data.DataModel.SetupLogHistoryModel v in SetupHistory)
                                            { %>
                                                <tr id="historyline<%=v.JobSetupID %>">
                                                    <td><%=v.JobItemID %></td>
                                                    <td><%=v.JobNumber %></td>
                                                    <td><%=v.Name %></td>
                                                    <td><%=v.Machine %></td>
                                                    <td><%=v.Quantity %></td>
                                                    <td><%=v.QuantityIn %></td>
                                                    <td><%=v.QuantityOut %></td>
                                                    <td><%=v.Hours %></td>
                                                    <td><%=v.Logout %></td>
                                                </tr>
                                                 <% } %>
                                            </tbody>
                                        </table>                                        
                                    </td>
                                </tr>--%>
                                <tr><td><u><b><br />Setup Comments (persists from lot to lot):</b></u></td></tr>
                              <%--  <tr>
                                    <td>
                                        <table>
                                            <thead>
                                                <tr>
                                                    <td>Employee</td>
                                                    <td>Comment</td>
                                                    <td>Time</td>
                                                </tr>
                                            </thead>
                                            <tbody id="comments<%=r.SetupID %>">
                                            <% foreach (Montsees.Data.DataModel.SetupEntriesModel x in SetupEntries)
                                                { %>
                                                <tr>
                                                    <td><%=x.Name %></td>
                                                    <td><%=x.Entry %></td>
                                                    <td><%=x.Timestamp %></td>
                                                </tr>
                                            <% } %>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td><b><u><span style="font-size:smaller">Employee</span></u></b></td>
                                                <td><b><u><span style="font-size:smaller">Entry</span></td>
                                            </tr>
                                            <tr>
                                                <td><asp:DropDownList runat="server" ID="EmployeeCommentDrop" DataValueField="EmployeeID" DataTextField="Name"></asp:DropDownList></td>
                                                <td><asp:TextBox runat="server" ID="EntryText" Width="557px" Height="31px" Wrap="true" TextMode="MultiLine" ></asp:TextBox></td>
                                            </tr>
                                        </table>
                                        <asp:Button runat="server" ID="CommentButton" Text="Add comment" CommandName="AddComment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                    </td>
                                </tr>--%>
                            </table>
                                </div></td></tr></table></td>
			           </tr>
		            <% } %>
	            </tbody>               
            </table>
        </div>
        

        <asp:SqlDataSource ID="populateopslist" runat="server"
	ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT
	OperationName From Operation">
    </asp:SqlDataSource>  
        <asp:SqlDataSource ID="EmployeeList" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Name, EmployeeID FROM Employees WHERE Active=1"></asp:SqlDataSource>
        <script type="text/javascript">

            
            function divexpandcollapse(divname, jobsetupid) {
             var div = document.getElementById(divname);

             if (div.style.display == "none") {
                 
                 div.style.display = "inline";

             } else {
                 div.style.display = "none";

             }
         }

         function createfixture(jobsetupid) {
             window.open("AddFixture.aspx?SourceSetup=" + jobsetupid)
         }

         function quickfixture(jobsetupid) {
             window.open("QuickFixture.aspx?SourceSetup=" + jobsetupid)
         }

         function setupsheet(jobsetupid) {
             window.open("SetupSheet.aspx?JobSetupID=" + jobsetupid)
         }

    </script>
    
