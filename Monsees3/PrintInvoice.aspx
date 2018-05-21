<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintInvoice.aspx.cs" Inherits="PrintInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

    <style>
        .panel-default > .panel-heading-custom {
            background-color: white;
            font-size: smaller;
            border: none;
            border-width: 0;
            box-shadow: none;
        }

        .panel-default > .panel-body-custom {
            font-size: smaller;
        }

        .panel-default > .panel-footer-custom {
            background-color: white;
            font-size: xx-small;
            /*border:none;
              border-width:0;*/
        }

        .panel-default {
            border: none;
            border-width: 0;
            box-shadow: none;
        }

        .table th, .table td {
            border-top: none !important;
            padding: 0px !important;
            padding-bottom: 0px !important;
            margin-bottom: 0px !important;
        }

        h4 {
            font-weight: bold;
            padding: 0px 0px 5px 0px;
            margin: 0px;
        }

        h6 {
            font-weight: bold;
            padding: 0px 0px 5px 0px;
            margin-top: 5px;
            text-align:right;
            /*font-size: smaller;*/
        }

        .ftText {            
            padding: 0px 0px 5px 0px;
            margin-top: 5px;
            font-size:small;
        }

        .Logo {
            color: #7c93b7;
            /*color:#4b599c;*/
        }

        hr {
            height: 5px;
            background-color: #c2c2c2;
            margin-top: 0px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="panel panel-default">
                <%--Start  Header--%>
                <div class="panel-heading panel-heading-custom">
                    <div class="row">
                        <div class="col-xs-3">
                            <img src="Images/Logo.png" class="img-responsive" />
                        </div>
                        <div class="col-xs-5">
                            <asp:Table ID="Table1" runat="server" CssClass="table Logo">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                <asp:Label runat="server" Text="850 Saint Paul St. Rochester, NY 14605"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Phone:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="(585) 262-4180"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Fax:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="262-4199"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Email:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="mispurling@moonseetool.com"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Website:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="www.moonseetool.com"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                        <div class="col-xs-4">
                            <asp:Table ID="Table2" runat="server" CssClass="table">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                               <h4>Purchase Order</h4>
                                         <%--<asp:Label runat="server" Text="Purchase Order"></asp:Label>--%>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="PO Number:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="3949"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="PO Date:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="30/11/2015"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Terms:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Net 30"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="FOB:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Monsees"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>


                    <%--   Sender  -> Recipient--%>
                    <div class="row" id="rowSnd">
                        <div class="col-xs-2">
                        </div>
                        <div class="col-xs-4">
                            <asp:Table ID="Table4" runat="server" CssClass="table">
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="TO: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="MSC Industrial Supply"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                              
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="75 Maxess Road"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                               
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Melville NY 11747"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                             
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Attn:"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                        <div class="col-xs-4">
                            <asp:Table ID="Table5" runat="server" CssClass="table">
                                <asp:TableRow>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="SHIP TO: "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Monsees Group"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                              
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="850 Saint Paul St. Box 14"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                               
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Rochester NY 14605"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                             
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Attn: Kathy Hunt"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>                             
                                    </asp:TableCell>
                                    <asp:TableCell>
                                <asp:Label runat="server" Text="Pnone: 585-262-4180"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                        <div class="col-xs-2">
                        </div>
                    </div>

                    <div class="row" id="Descr">
                        <div class="col-xs-2">
                        </div>
                        <div class="col-xs-1">
                            <h6>Description: </h6>
                        </div>
                        <div class="col-xs-3">
                            <h6>Various shop tools</h6>
                        </div>
                    </div>
                </div>
                <%--End  Header--%>

                <%--Start  Body--%>
                <div class="panel-body panel-body-custom">
                    <asp:Table ID="Table3" runat="server" CssClass="table">
                        <asp:TableHeaderRow>
                            <asp:TableCell>
                               <h5>Line</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Item Number</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Description</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Each</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Qty</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Total</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Due Date</h5>
                            </asp:TableCell>
                            <asp:TableCell>
                               <h5>Notes</h5>
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                        <%--Start: You can put this area on the itemTemplate of a repeater Control--%>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblLine" runat="server" Text="1"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblItemNumber" runat="server" Text="7556566"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblDescription" runat="server" Text="# 42 Reamer"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblEach" runat="server" Text="14:29"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblQty" runat="server" Text="2"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblTotal" runat="server" Text="28:58"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblDueDate" runat="server" Text="20/11/2015"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblNotes" runat="server" Text="For cleaning holes in indy and jasper roughs"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <%--End--%>
                    </asp:Table>
                </div>
                <%--End  Body--%>


                <%--Start  Footer--%>
                <div class="panel-footer panel-footer-custom">
                    <div class="row">
                        <div class="col-sm-12">
                            <h5>Note(s)</h5>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-xs-2" style="padding:0px; margin-right:0px;">
                            <h6 class="ftText">Acknowledged by:</h6>
                        </div>

                        <div class="col-xs-2" style="padding:0px; margin-left:0px;">
                            <h6 class="ftText">
                                <asp:Label ID="lblAcknowledged" runat="server" Text="____________________"></asp:Label></h6>
                        </div>

                        <div class="col-xs-1" >
                            <h6 class="ftText" style="padding:0px; margin-left:0px;">Date:</h6>
                        </div>

                        <div class="col-xs-1">
                            <h6 class="ftText" >
                                <asp:Label ID="lblDate" runat="server" Text="__________"></asp:Label></h6>
                        </div>

                        <div class="col-xs-1" style="padding:0px; margin-right:0px;">
                            <h6 class="ftText">Buyer:</h6>
                        </div>

                        <div class="col-xs-2" >
                            <h6 class="ftText" style="padding:0px; margin-left:0px;">
                                <asp:Label ID="lblBuyer" runat="server" Text="____________________"></asp:Label></h6>
                        </div>

                        <div class="col-xs-1">
                            <h6 class="ftText">Date:</h6>
                        </div>

                        <div class="col-xs-1">
                            <h6 class="ftText">
                                <asp:Label ID="lblDateBuy" runat="server" Text="__________"></asp:Label></h6>
                        </div>

                    </div>
                    <hr />
                </div>
                <%--End  Footer--%>
            </div>
        </div>
    </form>
</body>
</html>
