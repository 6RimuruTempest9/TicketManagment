using System;
using System.IO;
using System.Web.Configuration;
using System.Web.Mvc;
using ThirdPartyEventEditor.Filters;

namespace ThirdPartyEventEditor
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var logFileName = WebConfigurationManager.AppSettings["LogFileName"];

            var pathToFolder = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\";

            var pathToFile = Path.Combine(pathToFolder, logFileName);

            filters.Add(new ExceptionHandlerFilterAttribute(pathToFile));
        }
    }
}