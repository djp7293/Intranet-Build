﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace Monsees
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class jQueryHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // We add control in Page tree collection
            using (var dummyPage = new Page())
            {
                dummyPage.Controls.Add(GetControl(context));
                context.Server.Execute(dummyPage, context.Response.Output, true);
            }
        }

        private Control GetControl(HttpContext context)
        {
            // URL path given by load(fn) method on click of button
            string strPath = context.Request.Url.LocalPath;
            UserControl userctrl = null;
            using (var dummyPage = new Page())
            {
                userctrl = dummyPage.LoadControl(strPath) as UserControl;
            }
            // Loaded user control is returned
            return userctrl;
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}