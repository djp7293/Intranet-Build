using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Monsees.Security;
using Monsees.DataModel;
using Monsees.Database;
using Dapper;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using Monsees.Data;
using Monsees.Pages;
using BasicFrame.WebControls;

namespace Monsees
{
    public partial class InspectionInventory : DataPage
    {
        protected List<DecommissionModel> DecommList;
        protected BDPLite Delivery1;

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "SELECT * FROM [Gauges] WHERE InspOfficeEquip = 1";
            SqlDataSource2.SelectCommand = "SELECT * FROM [Gauges] WHERE InspOfficeEquip = 0";
            GetData();
            
            DecommFilter.DataSource = DecommList;
            DecommFilter.DataBind();
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);

            DecommList = inspectionRepository.GetDecommissionList();

            this.UnitOfWork.End();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query = new StringBuilder("Select * From Gauges WHERE InspOfficeEquip = 1");


            if (!String.IsNullOrEmpty(DescFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And Description LIKE '%" + DescFilter.Text + "%'");
                }
            }

            if (!String.IsNullOrEmpty(LocFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And Location LIKE '%" + LocFilter.Text + "%'");
                }
                else query.Append(" And Location LIKE '%" + LocFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(TypeFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And GageType LIKE '%" + TypeFilter.Text + "%'");
                }
                else query.Append(" And GageType LIKE '%" + TypeFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(OwnerFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And EmployeeID LIKE '%" + OwnerFilter.Text + "%'");
                }
                else query.Append(" And EmployeeID LIKE '%" + OwnerFilter.Text + "%'");
            }

            
            if (toggle == false)
            {
                
                if (ActiveFilter.Text == "1")
                {
                    query.Append(" And Active = 1");
                    toggle = true;
                } 
                if (ActiveFilter.Text == "0")
                {
                    query.Append(" And Active = 0");
                    toggle = true;
                }
            }
            else
            {
                if (ActiveFilter.Text == "1") query.Append(" And  Active = 1");
                if (ActiveFilter.Text == "0") query.Append(" And  Active = 0");
            }

            if (DecommFilter.Text != "0")
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And DecommCodeID = " + DecommFilter.Text);
                }
                else query.Append(" And DecommCodeID = " + DecommFilter.Text);
            }

            /*if (!String.IsNullOrEmpty(Delivery1.SelectedDateFormatted.ToString()))
            {
                string DateCal = ((DateTime)Convert.ToDateTime(Delivery1.SelectedDateFormatted.ToString())).ToShortDateString();
                
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE CalibrationDue < #" + DateCal + "#");
                }
                else query.Append(" And CalibrationDue < #" + DateCal + "#");
            }*/

            SqlDataSource1.SelectCommand = query.ToString();
            GridView1.DataBind();

        }

        protected void btnUpdateShop_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query = new StringBuilder("Select * From Gauges WHERE InspOfficeEquip = 0");


            if (!String.IsNullOrEmpty(TextBox1.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And Description LIKE '%" + TextBox1.Text + "%'");
                }
            }

            if (!String.IsNullOrEmpty(TextBox2.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And Location LIKE '%" + TextBox2.Text + "%'");
                }
                else query.Append(" And Location LIKE '%" + TextBox2.Text + "%'");
            }

            if (!String.IsNullOrEmpty(Textbox3.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And GageType LIKE '%" + Textbox3.Text + "%'");
                }
                else query.Append(" And GageType LIKE '%" + Textbox3.Text + "%'");
            }

            if (!String.IsNullOrEmpty(Textbox4.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And EmployeeID LIKE '%" + Textbox4.Text + "%'");
                }
                else query.Append(" And EmployeeID LIKE '%" + Textbox4.Text + "%'");
            }


            if (toggle == false)
            {

                if (DropDownList1.Text == "1")
                {
                    query.Append(" And Active = 1");
                    toggle = true;
                }
                if (DropDownList1.Text == "0")
                {
                    query.Append(" And Active = 0");
                    toggle = true;
                }
            }
            else
            {
                if (DropDownList1.Text == "1") query.Append(" And  Active = 1");
                if (DropDownList1.Text == "0") query.Append(" And  Active = 0");
            }

            if (DropDownList2.Text != "0")
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" And DecommCodeID = " + DropDownList2.Text);
                }
                else query.Append(" And DecommCodeID = " + DropDownList2.Text);
            }

            /*if (!String.IsNullOrEmpty(Delivery1.SelectedDateFormatted.ToString()))
            {
                string DateCal = ((DateTime)Convert.ToDateTime(Delivery1.SelectedDateFormatted.ToString())).ToShortDateString();
                
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE CalibrationDue < #" + DateCal + "#");
                }
                else query.Append(" And CalibrationDue < #" + DateCal + "#");
            }*/

            SqlDataSource2.SelectCommand = query.ToString();
            GridView2.DataBind();

        }
    }
}