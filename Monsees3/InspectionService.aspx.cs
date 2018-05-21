using Monsees.Security;
using Monsees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Monsees
{
    public partial class InspectionService : InspectionController
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Page.RequireRole("Inspection");

        }
    }
}