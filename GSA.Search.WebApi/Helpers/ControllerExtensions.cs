using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GSA.Search.WebApi.Helpers
{
    /// <summary>
    /// Extensions for Controllers
    /// </summary>
    internal static class ControllerExtensions
    {
        public static string GetHostFromSystemName(this ApiController controller, string systemname)
        {
            var configSection = ConfigurationManager.GetSection("systemNames") as NameValueCollection;

            if(configSection==null)
                throw new Exception("The configurationsection for systemNames could not be found.");

            return configSection[systemname];
        } 
    }
}