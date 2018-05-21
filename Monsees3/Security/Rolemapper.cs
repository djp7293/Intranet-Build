using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;

namespace Monsees.Security
{
	public static class RoleMapper
	{
		public static string WindowsRoleName(string commonName)
		{		
			return ConfigurationManager.AppSettings[commonName + "Role"];
		}

        public static void RequireRole(this Page p, string role)
        {
            if (p.User != null && p.User.IsInRole(WindowsRoleName(role)))
            {
                return;
            }

            throw new HttpException(401, "Access Denied.");
        }

		public static void RequireRole(this Page p, string role,string redirectUrl)
		{
			if (p.User!=null && p.User.IsInRole(WindowsRoleName(role)) )
			{
				return;
			}

			p.Response.Redirect(redirectUrl);
		}

		public static bool IsInMappedRole(this Page p, string role)
		{
			return (p.User != null && p.User.IsInRole(WindowsRoleName(role)));
		}

 

	}
}