using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monsees.Security;

namespace Monsees.Controls
{
	public partial class Navigation : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                this.inspectionItem.Visible = Page.IsInMappedRole("Inspection");
                this.officeItem.Visible = Page.IsInMappedRole("Admin");
                this.shippingItem.Visible = Page.IsInMappedRole("Admin");
            }
            //this.shippingItem.Visible = Page.IsInMappedRole("Admin") || Page.IsInMappedRole("Inspection"); //either admin or inspection
		}
	}
}