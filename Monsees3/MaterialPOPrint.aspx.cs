using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BasicFrame.WebControls;
using Monsees.Security;
using Monsees.DataModel;
using Monsees.Database;
using Dapper;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using Monsees.Data;
using Monsees.Pages;

namespace Monsees
{
	public partial class MaterialPOPrint : DataPage
	{
        public Int32 POID = 0;
       

		protected void Page_Load(object sender, EventArgs e)
		{
           
            Int32.TryParse(Request["POID"], out POID);
            ViewState["POID"] = POID;
           
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            
               
               
               
            MatlPurchOrder.SelectCommand = "SELECT * FROM [MaterialPOs] WHERE MaterialPOID=" + (int)ViewState["POID"];
            ListView2.DataSource = MatlPurchOrder;
            ListView2.DataBind();
            MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + (int)ViewState["POID"];
            GridView1.DataSource = MatPOLineItems;
            GridView1.DataBind();
              
           
		}

       

       

	}


}