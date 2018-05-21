<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NestedCtrlTest.aspx.cs" Inherits="Monsees.NestedCtrlTest" MasterPageFile="~/MasterPages/Monsees.Master"  %>
<%@ Register src="NestedLoginCtrl.ascx" tagname="NestedLoginControl" tagprefix="mon" %>



<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<style type="text/css">
    .tabletest thead {
        display: table-header-group;
    }
    .auto-style1 {
        width: 398px;
    }
    .auto-style2 {
        width: 111px;
    }

    .droptable {
        Border-Color:#999999;
             Border-Style:none;
             Border-Width:1px;
             padding-left:3px;
             font-size: small;
             width:100%;
              
        }

        .droptable .th {
            background: darkblue;
            
            color: white;
           
        }

        .droptable .tr {
            background: white;
            
        }
    
</style>

    <script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<div >
        
          <table class="droptable">
            <tr><td><br /><b><u>Corrective Actions:</u></b><br /></td></tr>
              <tr>
                  <td colspan="2">
                <table>
                    <thead>
                        <tr>
                            <td>Car #</td>
                            <td>Rev</td>
                            <td>Init. Date</td>
                            <td>Due</td>
                            <td>Issue</td>
                            <td>Owner</td>
                            <td></td>
                        </tr>
                    </thead>
                    <tbody>
                            <% foreach (Montsees.Data.DataModel.CorrectiveActionModel a in CorrectiveActions)
                                {  %>
                        <tr>
                            <td><%=a.CARID %></td>
                            <td><%=a.Revision_Nmber %></td>
                            <td><%=a.InitiationDate %></td>
                            <td><%=a.DueDate %></td>
                            <td><%=a.Definition %></td>
                            <td><%=a.ImpEmployee %></td>
                            <td></td>

                        </tr>
                        <% } %>
                    </tbody>
                </table>
                                                       
                  </td>
              </tr>
              <tr style="vertical-align:top">
                  <td>
                      <table>
                          <tr style="vertical-align:top">
                              <td>
                                 <table><tr><td><br /><b><u>Part Summary:</u></b><br /></td></tr>
                  <tr style="vertical-align:top"><td>
                      <table>
                          <tr><td>Plating: </td><td class="auto-style2"><%=Summary.PlatingLabel %></td></tr>
                          <tr><td>Heat Treatment: </td><td class="auto-style2"><%=Summary.HeatTreatLabel %></td></tr>
                          <tr><td>Subcontract: </td><td class="auto-style2"><%=Summary.SubcontractLabel %></td></tr>
                          <tr><td>Subcontract 2: </td><td class="auto-style2"><%=Summary.Subcontract2Label %></td></tr>
                          <tr><td>Material: </td><td class="auto-style2"><%=Summary.Material %></td></tr>
                          <tr><td>Dimension: </td><td class="auto-style2"><%=Summary.Dimension %></td></tr>
                          <tr><td>Size: </td><td class="auto-style2"><%=Summary.MaterialSize %></td></tr>
                          <tr><td>Cut: </td><td class="auto-style2"><%=Summary.StockCut %></td></tr>
                          <tr><td>Parts per Cut: </td><td class="auto-style2"><%=Summary.PartsPerCut %></td></tr>
                          <tr><td>Purchase Cut: </td><td class="auto-style2"><%=Summary.PurchaseCut %></td></tr>
                          <tr><td>Drill: </td><td class="auto-style2"><%=Summary.Drill %></td></tr>
                          <tr><td>Drill Size: </td><td class="auto-style2"><%=Summary.DrillSize %></td></tr>
                      </table>
                  </td>
                      <td class="auto-style1">
                          <table><tr><td><b><u>Deliveries:</u></b><br /></td></tr>
                              <tr>
                                  <td>Delivry</td>
                                  <td>Qty</td>
                                  <td>PO #</td>
                                  <td>RTS</td>
                                  <td>Shipped</td>
                                  <td>Suspended</td>
                              </tr>
                              <% foreach (Montsees.Data.DataModel.DeliveryModel b in Deliveries)
                                  {  %>
                              <tr>
                                  <td><%=b.CurrDelivery %></td>
                                  <td><%=b.Quantity %></td>
                                  <td><%=b.PONumber %></td>
                                  <td><%=b.ReadyToShip %></td>
                                  <td><%=b.Shipped %></td>
                                  <td><%=b.Suspend %></td>
                              </tr>
                                <% } %>
                              <tr><td><b><u>Certifications: </u></b><br /></td></tr>
                              <tr>
                                  <td>C of C</td>
                                  <td>Plating</td>
                                  <td>Material</td>
                                  <td>Serialized</td>
                              </tr>
                              <% foreach (Montsees.Data.DataModel.CertificationSummary c in Certifications)
                                 { %>
                              <tr>
                                  <td><%=c.CertCompReqd %></td>
                                  <td><%=c.PlateCertReqd %></td>
                                  <td><%=c.MatlCertReqd %></td>
                                  <td><%=c.SerializationReqd %></td>
                              </tr>
                              <% } %>
                        </table>                        
                    </TD>
                  </tr>
            </TABLE>
                              </td>  
                          </tr>
                           <tr>
                               <td>
                                   <table>
                                       <tr><td><b><u>Mat&#39;l Quote Request:</u></b></td></tr>
                                       <tr>
                                           <td>Material</td>
                                           <td>Dim</td>
                                           <td>D</td>
                                           <td>H</td>
                                           <td>W</td>
                                           <td>L</td>
                                           <td>Q</td>
                                           <td>Cut</td>
                                           <td>Order</td>
                                       </tr>
                                       <% foreach (Montsees.Data.DataModel.MatlQuoteModel d in MatlQuotes)
                                           {  %>
                                       <tr>
                                           <td><%=d.MaterialName %></td>
                                           <td><%=d.Dimension %></td>
                                           <td><%=d.Diameter %></td>
                                           <td><%=d.Height %></td>
                                           <td><%=d.Width %></td>
                                           <td><%=d.Length %></td>
                                           <td><%=d.Quantity %></td>
                                           <td><%=d.Cut %></td>
                                           <td><%=d.OrderPending %></td>
                                       </tr>
                                       <% } %>
                                   </table>
                                </td>
                           </tr>           
                           <tr><td><b><u>Mat&#39;l Order:</u></b></td></tr>
                          <tr>
                              <td>
                                  <table>
                                      <tr>
                                          <td>Matl Lot ID</td>
                                          <td>Matl</td>
                                          <td>Dim</td>
                                          <td>D</td>
                                          <td>H</td>
                                          <td>W</td>
                                          <td>L</td>
                                          <td>Qty</td>
                                          <td>Cut</td>
                                          <td>Recd</td>
                                          <td>Prepd</td>
                                          <td>Loc</td>
                                          <td>Source</td>
                                          <td>Pct</td>
                                          <td></td>
                                      </tr>
                                      <% foreach (Montsees.Data.DataModel.MatlOrderModel e in MatlOrders)
                                          {  %>
                                      <tr>
                                          <td><%=e.MatPriceID %></td>
                                          <td><%=e.MaterialName %></td>
                                          <td><%=e.Dimension %></td>
                                          <td><%=e.D %></td>
                                          <td><%=e.H %></td>
                                          <td><%=e.W %></td>
                                          <td><%=e.L %></td>
                                          <td><%=e.Qty %></td>
                                          <td><%=e.Cut %></td>
                                          <td><%=e.received %></td>
                                          <td><%=e.Prepared %></td>
                                          <td><%=e.Location %></td>
                                          <td><%=e.MaterialSource %></td>
                                          <td><%=e.pct %></td>
                                          <td></td>
                                      </tr>
                                      <% } %>
                          
                                  </table>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <table>
                                      <tr>
                                          <td><br><b><u>Fixtures:</u></b></td>
                                      </tr>
                                      <tr>
                                          <td>Part #</td>
                                          <td>Decription</td>
                                          <td>Qty</td>
                                          <td>Owner</td>
                                          <td>For Setup</td>
                                          <td>Location</td>
                                      </tr>
                                      <% foreach (Montsees.Data.DataModel.FixtureJobItemModel f in Fixtures)
                                          {  %>
                                      <tr>
                                          <td><%=f.PartNumber %></td>
                                          <td><%=f.DrawingNumber %></td>
                                          <td><%=f.Quantity %></td>
                                          <td><%=f.ContactName %></td>
                                          <td><%=f.OperationName %></td>
                                          <td><%=f.Location %></td>
                                      </tr>
                                    <% } %>
                                  </table>
                              </td>
                          </tr>
                          <tr>
                              <td>
                                  <table>
                                      <tr>
                                          <td><b><u>Machined Assembly Components:</u></b></td>
                                      </tr>
                                      <tr>
                                          <td>Part #</td>
                                          <td>Rev #</td>
                                          <td>Description</td>
                                          <td>Per Assy</td>
                                          <td>Next Op</td>
                                      </tr>
                                       <% foreach (Montsees.Data.DataModel.AssyMachinedCompModel g in MachinedComponenets)
                                           {  %>
                                      <tr>
                                          <td><%=g.PartNumber %></td>
                                          <td><%=g.Revision_Number %></td>
                                          <td><%=g.DrawingNumber %></td>
                                          <td><%=g.PerAssembly %></td>
                                          <td><%=g.NextOp %></td>
                                      </tr>
                                      <% } %>
                                  </table>
                              </td>
                          </tr>
                          <tr style="vertical-align:top">
                              <td>
                                  <table>
                                      <tr><td><b><u>Purchased Assembly Components:</u></b></td></tr>
                                      <tr>
                                          <td>Description</td>
                                          <td>Per Assy</td>
                                          <td>Item #</td>
                                          <td>Vendor</td>
                                          <td>Each</td>
                                          <td>Weblink</td>
                                      </tr>
                                        <% foreach (Montsees.Data.DataModel.AssyPurchasedCompModel h in PurchasedComponents)
                                            {  %>
                                      <tr>
                                          <td><%=h.DrawingNumber %></td>
                                          <td><%=h.PerAssy %></td>
                                          <td><%=h.ItemNumber %></td>
                                          <td><%=h.VendorName %></td>
                                          <td><%=h.Each %></td>
                                          <td><a href='<%=h.Weblink %>' target="_blank">Link</a></td>
                                      </tr>
                                      <% } %>
                                  </table>
                              </td>
                          </tr>
            
                        </table>
                  </td>
                  <td style="vertical-align:top">
                      <table>                    
                          <tr><td style="width:50%; vertical-align:top"><br /><b><u>Operations:</u></b></td></tr>
                          <tr>
                              <td>         
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
                    
		                                    <tr>
                                                <td colspan="10">
                                                <table style="border:solid; border-width:1px">
                                                    <tr id="setup<%=r.JobSetupID %>">
                                                        <td class="Expander" headers="th1" style="width:30px; font-size:small"><% if (r.SetupID != 0) { %><a href="JavaScript:divexpandcollapse('div<%=r.JobSetupID %>', '<%=r.JobSetupID %>');">+/-</a><% } %></td>
                                                        <td headers="th2" style="width:20px"> <%=r.ProcessOrder %> </td>
			                                            <td headers="th3" style="width:150px"><%=r.Label %> </td>
			                                            <td headers="th4" style="width:50px"><%=r.Setup_Cost %></td>
			                                            <td headers="th5" style="width:80px"><%=r.Operation_Cost %></td>
			                                            <td headers="th6" style="width:200px"><%=r.Comments %> </td>
                                                        <td headers="th7" style="width:50px"><%=r.Completed %></td>
                                                        <td headers="th8" style="font-size:small"><a href="JavaScript:createfixture('<%=r.JobSetupID %>');">Order Fixture</a></td>
                                                        <td headers="th9" style="font-size:small"><a href="JavaScript:quickfixture('<%=r.JobSetupID %>');">Quick Fixture</a> </td>
                                                        <td headers="th10" style="font-size:small"><a href="JavaScript:setupsheet('<%=r.JobSetupID %>');">Setup Sheet</a> </td>
                                                    </tr>
                                                <tr>
                                                    <td colspan="10">
                                                    <div id='div<%=r.JobSetupID %>' style="display:none; font-size:small">
                                                    <% if (r.SetupID != 0)
                                                    { %>
                                                    <table>
                                                        <tr><td><u><b>Active Log:</b></u></td></tr>
                                                        <tr>
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
                                                                        <tr><td></td></tr>                                                
                                                                    </tbody>
                                                                </table>
                                                                <div id="moveToInventoryForm">
                                                                    <table>
                                                                        <tr>
                                                                            <td>Employee</td>
                                                                            <td>Machine</td>
                                                                            <td>In</td>
                                                                            <td>Out</td>
                                                                            <td>Hours</td>
                                                                            <td>Rework</td>
                                                                            <td>Notes</td>                                                    
                                                                            <td>Done</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><select type="text" id="Machinist" >
                                                                                    <% foreach (Montsees.Data.DataModel.EmployeeModel e in Employees)
                                                                          { %>
                                                                                    <option value="<%=e.EmployeeID%>"><%=e.Name %></option>
                                                                                <%} %>
                                                                                </select>
                                                                            </td>
                                                                            <td><select id="Machine">
                                                                                <% foreach (Montsees.Data.DataModel.MachineModel m in MachineList)
                                                                          { %>
                                                                                    <option value="<%=m.MachineID%>"><%=m.Machine %></option>
                                                                                <%} %>
                                                                                </select>
                                                                            </td>
                                                                            <td><input type="text" id="In" size="3" /></td>
                                                                            <td><input type="text" id="Out" size="3"/></td>
                                                                            <td><input type="text" id="Hours" size="3" /></td>
                                                                            <td><input type="checkbox" id="Rework" /></td>
                                                                            <td><textarea id="Notes" /></td>
                                                                            <td><input type="checkbox" id="Done" /></td>
                                                                         </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr><td><u><b><br />Pictures:</b></u></td></tr>
                                                        <tr>
                                                            <td>
                                                                <table >
                                                                    <tr><td>
                                                                        
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
                                                        <tr>
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
                                                        </tr>
                                                        <tr><td><u><b><br />Setup Comments (persists from lot to lot):</b></u></td></tr>
                                                        <tr>
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
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td><b><u><span style="font-size:smaller">Employee</span></u></b></td>
                                                                        <td><b><u><span style="font-size:smaller">Entry</span></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><select id="CommentEmployee"></select></td>
                                                                        <td><textarea id="Comment" /></td>
                                                                    </tr>
                                                                </table>
                                                             </td>
                                                        </tr>
                                                    </table><% } %>
                                                </div>

                                                </td>

                                                </tr>

                                                </table>
		                                        </td>
			                                   </tr>
		                                    <% } %>
	                                    </tbody>               
                                    </table>
                              
                                  <br />
                              </td>
                           </tr>
                           <tr><td><b><u>Subcontract History:</u></b></td></tr>  
                            <tr>
                                <td>                           
                                        <br />
                                    <table>
                                        <tr>
                                            <td>PO #</td>
                                            <td>Workcode</td>
                                            <td>Qty</td>
                                            <td>Due Date</td>
                                            <td>Recd</td>
                                        </tr>
                                        <% foreach (Montsees.Data.DataModel.SubcontractLineModel i in Subcontracting)
                                            {  %>
                                            <tr>
                                                <td><%=i.SubcontractID %></td>
                                                <td><%=i.WorkCode %></td>
                                                <td><%=i.Quantity %></td>
                                                <td><%=i.DueDate %></td>
                                                <td><%=i.Received %></td>
                                            </tr>
                                            <% } %>
               
                                    </table>
                                </td>
                            </tr>
                    </table>
                  </td>                  
              </tr>
                                      
        </table>
    </div>

 <script type="text/javascript">

     //$(document).ready(function () {
     //    $.ajax({
     //        type: "POST",
     //        url: "ProdScheduleService.aspx/LoadControl2",
     //        data: JSON.stringify(obj),
     //        contentType: "application/json; charset=utf-8",
     //        success: function (data) { $("#loginctrldiv").append(data.d) },
     //        error: function (msg) {
     //            alert('Failure: ' + msg);
     //        },
     //        dataType: 'json',
     //        async: false
     //    });
     //});

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
    </asp:Content>