<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectionRpt.aspx.cs" Inherits="Monsees.InspectionRpt"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" xmlns:mso="urn:schemas-microsoft-com:office:office" xmlns:msdt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" >
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<head runat="server">
    <title>Production Schedule</title>
    <meta http-equiv="refresh" content="3600"/> 
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    


<style type="text/css">
@font-face {
    font-family: MyFont;
    src: url("/Verisurf.ttf")
}
body {
    
    font-size: medium;
    text-align: justify;
}

	input[type="text"]  {
		text-overflow:ellipsis;
		height:22px;
	}

	.data-entry input[type="text"] {
			height:22px;
			width:90px;
			text-align:right;
	}

	.data-entry {
		width:80px;

	}

	.form-table {
		width:1000px;
	}

	.button-accept {
		width:100%;
	}

	.reject {
		background:pink;
	}

	.measurement {
		width:100px;
	}

	.style1
        {
            width: 100%;
	    font-family: MyFont;
        }
        .style2
        {
            width: 11%;
        }
    </style>

<!--[if gte mso 9]><xml>
<mso:CustomDocumentProperties>
<mso:IsMyDocuments msdt:dt="string">1</mso:IsMyDocuments>
</mso:CustomDocumentProperties>
</xml><![endif]-->
</head>
<body>
<script src="jquery.js"></script>  
<script type="text/javascript" language="javascript"> 
	function CallMe(){
		var a=navigator.onLine;
		if (a) {
			function __doPostBack(eventTarget, eventArgument) {
				alert('test');
				if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
					theForm.__EVENTTARGET.value = eventTarget;
					theForm.__EVENTARGUMENT.value = eventArgument;
					theForm.submit();
				}
			}
		}
		else {
			alert('offline');
		}
	}

	$(function () {
		$('.data-entry input[type="text"]').keyup(function (e, n) {
			if (e.keyCode == 13) {
				e.preventDefault();
			};
		});

		$('.button-accept').click(function (e) {
			var $button = $(e.target),
				$prev = $button.prev();
			if ($button.text() == 'Accept') {
				val = $prev.val();
				$prev.val('Accept').data('initial', val);
				$button.text('Reject');
			}
			else {
				$prev.val($prev.data('initial'));
				$button.text('Accept');
			}

			e.preventDefault();
	 
		}).attr("type", "button");
	})
</script>

    <form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
    <div align="center">
    
      <table class="style1" width="50%">
            
            
<tr>
<td align="left">
                    


    <input type="button" id="Button45" name="Button45" onclick="javascript:__doPostBack('UpdateTableButton','')" value="Save" />&nbsp;<a href="/InspectionReportPrint.aspx?JobItemID=<%=JobItemID%>" target="_blank">Print Report</a>
	
	<br /><br />
    
                </td>
</tr>
</table>
<table align = "left" width="800px">
<tr width="800px">
<td align="left" width="200px">
 <asp:Label ID="JobItem" runat="server" 
                    Text="Lot Number : "></asp:Label>
</td> 
<td align="left" width="150px">
 <asp:Label ID="JobNumber" runat="server" 
                    Text="Job Number : "></asp:Label>
</td>    
<td align="left" width="250px">
 <asp:Label ID="PartNumber" runat="server"  
                    Text="Part Number : "  item-width="200px"></asp:Label>
</td>     
<td align="left" width="200px">
 <asp:Label ID="RevisionNumber" runat="server"  
                    Text="Revision Number : " item-width="200px"></asp:Label>
</td>                  
</tr>
<tr width="800px">
<td align="left" width="200px">
 <asp:Label ID="CompanyName" runat="server"  
                    Text="Company Name : " item-width="200px"></asp:Label>
</td> 
<td align="left" width="150px">
 
</td> 
<td align="left" width="250px">
 <asp:Label ID="DrawingNumber" runat="server"  
                    Text="Description : " item-width="200px"></asp:Label>
</td>    
<td align="left" width="200px">
 <asp:Label ID="qty" runat="server" 
                    Text="Quantity : " item-width="200px"></asp:Label>
                
</tr>

        </table>
         <table class="style1" width="100%">
            <tr>
                
                
                <td align="left" valign="middle" width="33%">
                 <asp:Label ID="Last_Refreshed" runat="server" Font-Size="Small" 
                    Text="Last Refreshed : "></asp:Label>
                </td>
            </tr>
         </table>
              

	<table class="style1" align="left">

				
		

		<tr>
	<td colspan="15" align="left">
      	<asp:Panel ID="Panel1" runat="server" align="left">
            <asp:Panel ID="Panel2" runat="server" align="left">
               <p>
                   
                    
                           <asp:GridView ID="ProductionViewGrid" runat="server" AllowPaging="False" 
                               AutoGenerateColumns="False" BackColor="White" 
                               BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                               DataSourceID="MonseesSqlDataSource" GridLines="Vertical" 
                                CssClass="form-table"
                               AllowSorting="True" align="left">
                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                               <Columns>

	<asp:BoundField DataField="DimensionNumber"  
                                       HeaderText="DIM" SortExpression="DimensionNumber"  ItemStyle-Width="20px">
                                       <ItemStyle HorizontalAlign="Center"/>
        </asp:BoundField> 
	<asp:BoundField DataField="Description" HeaderText="DRAWING DIMENSION" SortExpression="Description"  ItemStyle-Width="200px">
                                   <ItemStyle HorizontalAlign="Left" font-name=MyFont />
        </asp:BoundField> 
	<asp:TemplateField HeaderText="1st Msrmnt"  HeaderStyle-HorizontalAlign="Left" ControlStyle-CssClass="measurement" ItemStyle-CssClass="measurement" > 
		
        <ItemTemplate> 
                        <div class="data-entry">
							<asp:TextBox ID="lblId1" runat="server" Text='<%# Bind("Measurement1") %>'  ></asp:TextBox> 
							<button class="button-accept" runat="server" ><%# Eval("Measurement1").ToString().ToLower() =="accept" ? "Reject" : "Accept" %></button>
						</div>
		</ItemTemplate>
   </asp:TemplateField> 

	<asp:TemplateField HeaderText="2nd Msrmnt"  HeaderStyle-HorizontalAlign="Left" > 
		
                <ItemTemplate> 
                        <div class="data-entry">
							<asp:TextBox ID="lblId2" runat="server" Text='<%# Bind("Measurement2") %>' ></asp:TextBox> 
							<button class="button-accept" runat="server" ><%# Eval("Measurement2").ToString().ToLower() =="accept" ? "Reject" : "Accept" %></button>
						</div>
           	</ItemTemplate> 
           </asp:TemplateField> 
<asp:TemplateField HeaderText="3rd Msrmnt"  HeaderStyle-HorizontalAlign="Left"  > 

                <ItemTemplate> 
                        <div class="data-entry">
							<asp:TextBox ID="lblId3" runat="server" Text='<%# Bind("Measurement3") %>' ></asp:TextBox> 
							<button class="button-accept" runat="server" ><%# Eval("Measurement3").ToString().ToLower() =="accept" ? "Reject" : "Accept" %></button>
						</div>


           </ItemTemplate> 
           </asp:TemplateField> 

	<asp:TemplateField HeaderText="Final"  HeaderStyle-HorizontalAlign="Left" > 
		
                <ItemTemplate>
					 <div class="data-entry">
						<asp:TextBox ID="lblIdf" runat="server" Text='<%# Bind("FinalMeasurement") %>' ></asp:TextBox> 
						<button class="button-accept" runat="server"   ><%# Eval("FinalMeasurement").ToString().ToLower() =="accept" ? "Reject" : "Accept" %></button>
					</div>
            	</ItemTemplate> 
           </asp:TemplateField> 

<asp:TemplateField HeaderText="REMARKS"  HeaderStyle-HorizontalAlign="Left"   > 

                <ItemTemplate> 
<div class="remarks" >             
<asp:TextBox ID="lblIdr" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox> 
</div>
           </ItemTemplate>

           </asp:TemplateField> 
                                   
 
                               </Columns>
                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                               <HeaderStyle BackColor="#000084" Font-Bold="True" Font-Size="Small" 
                                   ForeColor="White" />
                               <AlternatingRowStyle BackColor="Gainsboro" />
                           </asp:GridView>
                      
                       
                 </p>
            </asp:Panel>
        </asp:Panel>
</td>
</tr>
</table>


    <p>
    <asp:Label ID="UpdateResults" runat="server" EnableViewState="False" 
        Visible="False"></asp:Label>
</p>

    </div>

 <asp:Button ID="UpdateTableButton" runat="server" Text="" onclick="UpdateTableButton_Click"/>
           	

 <asp:SqlDataSource ID="MonseesSqlDataSource" runat="server" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"  UpdateCommand="UPDATE [InspectionItems] SET [Measurement1] = @Measure1, [Measurement2] = @Measure2, [Measurement3] = @Measure3, [FinalMeasurement] = @Final, [Remarks] = @Remark WHERE [JobItemID] = @ItemID AND [Dimension] = @Dim" EnableCaching="False">
        <updateparameters>
                <asp:parameter name="Measure1" type="String" ConvertEmptyStringToNull = true />
                <asp:parameter name="Measure2" type="String" ConvertEmptyStringToNull = true />
		<asp:parameter name="Measure3" type="String" ConvertEmptyStringToNull = true />
		<asp:parameter name="Final" type="String" ConvertEmptyStringToNull = true />
		<asp:parameter name="Remark" type="String" ConvertEmptyStringToNull = true />
		<asp:parameter name="ItemID" type="String" />
		<asp:parameter name="Dim" type="String" />
        </updateparameters>
 </asp:SqlDataSource>



    </form>
</body>
</html>
