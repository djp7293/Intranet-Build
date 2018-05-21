<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectionReport.aspx.cs" Inherits="Monsees.InspectionReport" MasterPageFile="~/MasterPages/Monsees.master" %>
<asp:Content runat="server" ContentPlaceHolderID="headContent">

<script type="text/javascript" src="/Scripts/jquery.toast.js"></script>
<script type="text/javascript" src="/Scripts/CRUDService.js"></script>
<script type="text/javascript" src="/Scripts/jquery.valuechanging.js"></script>

<style>
    @font-face {
    font-family: MyFont;
    src: url("/Verisurf.ttf")
    }

    	input[type="text"]  {
		text-overflow:ellipsis;
		height:20px;
	}

	.data-entry input[type="text"] {
			height:22px;
			width:90px;
			text-align:right;
	}

	.data-entry {
		width:80px;

	}

    .report
    {
        margin-left:20px;
        margin-right:20px;
    }

	.report, .report-body {
		min-width:1000px;
        width:100%;
        border-collapse:collapse;
	}

	.button-accept {
		width:90px;
        display:block;
	}

    .check-change {
		
	}

	.reject {
		background:pink;
	}

    .report-body thead td {
        border-bottom:2px solid;
        color:white;
        background:#000084;
        font-weight:bold;
    }
    .inspection-item td 
    {
        vertical-align:top;
        border-bottom:1px solid;
        padding-bottom:10px;
        padding-top:10px;
    }

     .inspection-item td.dimension-label,
     .inspection-item td.dimension-number
     {
         vertical-align:middle;
     }

     thead td.dimension-number,
     .inspection-item td.dimension-number
     {
         min-width:100px;
        padding-left:5px;
     }

     .inspection-item td.dimension-number
     {
        background:#EEE;
     }

    .inspection-item td.dimension-label
    {
        font-family:MyFont;
        padding-left:10px;
    }
	.inspection-item .measurement {
		width:100px;
	}

    .report-heading, .report-body, .report {
		width:12in;
	}

	.report
	{
		border:1px solid;
	}

	.report-heading {
		border-bottom:0px;
		padding:0pt;
	}

	.report-heading .top.left {
		width:60%;
		margin-left:16pt;	
		margin-bottom:5pt;
	}

	.report-heading .top.right {
		margin-right:16pt;
		margin-bottom:5pt;
	}

	.report-heading h4 {
		text-align:center;
		color:red;
		margin:5pt;
	}

	.report-heading h4 span
	{
		font-weight:normal;
	}

	.report-heading .label {
		font-weight:bold;
		text-align:right;
		vertical-align:top;
	}

    .lotnum,.jobnum,.revnum {
        min-width:200px;
    }

    .partnum
    {
        min-width:300px;
    }
</style>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="bodyContent" >


<div class="report">
	<div class="report-heading">
        
        <table>
        <tbody>
           <tr>
                <td class="lotnum">
                 <span id="JobItem">Lot # : <%=HeaderData.JobItemID %></span>
                </td> 
                <td class="jobnum">
                 <span id="JobNumber">Job # :  <%=HeaderData.JobNumber %></span>
                </td>    
                <td class="partnum">
                 <span id="PartNumber" >Part # : <%=HeaderData.PartNumber %></span>
                </td>     
                <td class="revnum">
                 <span id="RevisionNumber" >Rev # :  <%=HeaderData.Revision_Number %></span>
                </td>                  
           </tr>
        <tr>
        <td>
         <span id="CompanyName" ><%=HeaderData.CompanyName %></span></td> 
        <td>
 
       Serial # : <%=String.IsNullOrEmpty(HeaderData.CustSerialNumber) ? HeaderData.InternalSerialNumber : HeaderData.CustSerialNumber %></td> 
        <td>
         <span id="DrawingNumber">Description : <%=HeaderData.DrawingNumber %></span>
        </td>    
        <td>
         <span id="qty">Quantity : <%=HeaderData.Quantity %></span>
                
        </td></tr>

        </tbody></table>
 
		<br style="clear:both;" />
	</div>
    <br/>
    <asp:Button ID="Button1" runat="server" Text="Print Final" OnClick="Button1_Click" />&nbsp;
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Print Draft" />
    &nbsp;
    <asp:Button ID="Button3" runat="server" Text="Print In-Process" OnClick="Button3_Click" />
    <br/><br />
	<table class="report-body" id="inspectionForm"  >
	<thead>
		<tr>
			<td class="dimension-number">#</td>
			<td class="dimension-label">DRAWING DIM</td>
			<td class="dimension" colspan="2">Msmnt 1</td>
			<td class="dimension thick-left" colspan="2">Msmnt 2</td>
			<td class="dimension" colspan="2">Msmnt 3</td>
			<td class="dimension thick-left" colspan="2">Final</td>
			<td>Remarks</td>
            <td>Critical</td>
		</tr>
	</thead>
	<tbody>
		<% foreach(Monsees.DataModel.InspectionReportView r in ReportData) { %>
		<tr class="inspection-item" data-id="<%=r.DimensionNumber %>" style="padding:initial;" >
			<td class="dimension-number"><%=r.DimensionNumber %> <%=String.IsNullOrEmpty(r.Sheet) ? "": "(SH" + r.Sheet + ", " + r.Zone + ")" %></td>
			<td class="dimension-label"><%=r.Description %></td>
			<td class="dimension">
                <input type="text" name="measurement1" value="<%=r.Measurement1 %>" style="width:100px;" /><button class="button-accept" tabindex="-1"><%=String.Concat(r.Measurement1).ToLower() =="accept" ? "Reject" : "Accept" %></button></td><td><input type="text" name="M1Rec" value="<%=r.M1Recording + "-" + ((r.M1Date.ToString("d") == "1/1/0001") ? "" : r.M1Date.ToString("d")) %>" readonly="true"  style="width:65px; font-size:x-small;"  /><br />
                
			</td>
			<td class="thick-left dimension">
                <input type="text" name="measurement2" value="<%=r.Measurement2 %>" style="width:100px;"  />
                <button class="button-accept" tabindex="-1" ><%= String.Concat(r.Measurement2).ToLower() =="accept" ? "Reject" : "Accept" %></button></td><td><input type="text" name="M2Rec" value="<%=r.M2Recording + "-" + ((r.M2Date.ToString("d") == "1/1/0001") ? "" : r.M2Date.ToString("d")) %>" readonly="true" style="width:65px; font-size:x-small;"  />
			</td>
			<td class="dimension">
                <input type="text" name="measurement3" value="<%=r.Measurement3 %>" style="width:100px;"  />
                <button class="button-accept" tabindex="-1"><%= String.Concat(r.Measurement3).ToLower() =="accept" ? "Reject" : "Accept" %></button></td><td><input type="text" name="M3Rec" value="<%=r.M3Recording + "-" + ((r.M3Date.ToString("d") == "1/1/0001") ? "" : r.M3Date.ToString("d")) %> " readonly="true" style="width:65px; font-size:x-small;"  />

			</td>
			<td class="thick-left dimension"><input type="text" name="final" value="<%=r.FinalMeasurement %>" style="width:100px;" />
                <button class="button-accept" tabindex="-1"><%=String.Concat(r.FinalMeasurement).ToLower() =="accept" ? "Reject" : "Accept" %></button></td><td><input type="text" name="FinalRec" value="<%=r.FinalRecording + "-" + ((r.FDate.ToString("d") == "1/1/0001") ? "" : r.FDate.ToString("d")) %> " readonly="true"  style="width:65px; font-size:x-small;" />

			</td>
			<td class="remarks"><input type="text" name="remarks" value="<%=r.Remarks%>" /></td>
            <td ><input class="check-change" type="checkbox" name="critical" <%=((r.Critical == 1) ? "checked" : "")%> /><input type="hidden" value="<%=r.Critical%>" name="critHid" /></td>
		</tr>
		<% } %>
	</tbody>
</table>
</div>

<script >
    var inventoryService = new CRUDService(
{
    baseUrl: '/InspectionService.aspx',

    setCompletionStatus: function (dimensionId, column, measurement1,measurement2,measurement3,final,remarks,critical,cb) {
 
        this.sendAjax({
            url: "UpdateMeasurement",
            data: {
                    jobItemId: <%=HeaderData.JobItemID%>,
                    serialNumId: <%=serialId%>,
                    dimensionId: Number(dimensionId),
                    column: column,
                    measurement1:measurement1,
                    measurement2:measurement2,
                    measurement3:measurement3,
                    final:final,
                    remarks: remarks,
                    critical: critical
                   },
            success: function () {
                cb();
            }
        });
    },

    
});

    $('#inspectionForm input').listenForValueChanging().bind('change valueChanging', function (e) {
        //inspection item TR
        var $inspectionRow = $(e.target).parent().parent(),
            dimensionId = $inspectionRow.attr('data-id');
        var $check = $inspectionRow.find("[name='critical']"),
            critical = $check.attr('checked');
        clearTimeout($inspectionRow.data('saveTimeout'));
        $inspectionRow.addClass('dirty');
        $inspectionRow.data('saveTimeout', setTimeout(function () {
            inventoryService.setCompletionStatus(dimensionId, $(e.target).attr('name'),
                                                 $inspectionRow.find("[name='measurement1']").val(),
                                                 $inspectionRow.find("[name='measurement2']").val(),
                                                 $inspectionRow.find("[name='measurement3']").val(),
                                                 $inspectionRow.find("[name='final']").val(),
                                                 $inspectionRow.find("[name='remarks']").val() ,
                                                 critical ,function(){
                                                     $inspectionRow.removeClass('dirty');   
                                                 });
        
        }, 1000));
        
    }).keyup(function (e, n) {
        if (e.keyCode == 13) {
            e.preventDefault();
        };
    });
 
    $('.button-accept').click(function (e) {
        var $button = $(e.target),
            $prev = $button.prev();
        if ($button.text() == 'Accept') {
            val = $prev.val();
            $prev.val('Accept').data('initial', val).trigger("change");
            $button.text('Reject');
        }
        else {
            $prev.val($prev.data('initial')).trigger("change");
            $button.text('Accept');
        }

        e.preventDefault();
	 
    }).attr("type", "button");

    $('.check-change').click(function (e) {
        var $check = $(e.target),
            $prev = $check.prev(),
            $inspectionrow = $(e.target).parent().parent(),
            $hid =$inspectionrow.find("name=critHid");
        if ($hid.val() == 1) {
            $hid.val()=0;
        }
        else {
            $hid.val()=1;
        }

        e.preventDefault();
	 
    }).attr("type", "checkbox");
</script>

	</asp:Content>
