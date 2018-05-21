using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using Monsees.Security;

namespace Monsees
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            //this.productionDirectory.Visible = Page.IsInMappedRole("Production");
            this.inspectionDirectory.Visible = Page.IsInMappedRole("Inspection");
            this.shippingDirectory.Visible = Page.IsInMappedRole("Warehouse");
            this.officeDirectory.Visible = Page.IsInMappedRole("Admin");
            this.financialDirectory.Visible = Page.IsInMappedRole("Office");

        }
    }
}