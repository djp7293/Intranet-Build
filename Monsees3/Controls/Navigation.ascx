<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navigation.ascx.cs" Inherits="Monsees.Controls.Navigation" %>
<div class="top-navigation">
 <ul class="menu-items">
	<li><span style="opacity:0.25"><b>MTD&nbsp;&nbsp;</b></span></li>
	<li><a href="/"><span>Home</span></a></li>
	<li><a href="/ActiveJobs.aspx"><span>Active Jobs</span></a></li>
	<li runat="server" id="inspectionItem"><a href="/Inspection.aspx"><span>Inspection</span></a></li>

	<li  runat="server" id="shippingItem"><a href="/ShippingReceiving.aspx"><span>Shipping &amp; Receiving</span></a></li>
   	<li  runat="server" id="officeItem"><a href="/monitorops.aspx"><span>Office</span></a></li>
 </ul>
 <div class="right-items">
	<span class ="user"><%= Page.User!=null ? Page.User.Identity.Name : "" %></span>
 </div>

	<div style="clear:both;height:0px;">&nbsp;</div>
</div>
