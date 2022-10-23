using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ThirdPartyEventEditor.Filters.Providers
{
    public class PerformanceTestFilterProvider : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var logFileName = WebConfigurationManager.AppSettings["LogFileName"];

            var pathToFolder = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\";

            var pathToFile = Path.Combine(pathToFolder, logFileName);

            return new[] { new Filter(new PerformanceTestFilter(pathToFile), FilterScope.Global, 0) };
        }
    }
}