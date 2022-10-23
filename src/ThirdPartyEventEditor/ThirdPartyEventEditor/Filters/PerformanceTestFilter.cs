using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

namespace ThirdPartyEventEditor.Filters
{
    public class PerformanceTestFilter : IActionFilter
    {
        private readonly string _pathToLogFile;

        private readonly Stopwatch _stopWatch = new Stopwatch();
        
        public PerformanceTestFilter(string pathToLogFile)
        {
            _pathToLogFile = pathToLogFile ?? throw new ArgumentNullException(nameof(pathToLogFile));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();
            
            var executionTime = _stopWatch.ElapsedMilliseconds;

            using (var stream = File.Open(_pathToLogFile, FileMode.Append))
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.WriteLine("Action name: " + filterContext.ActionDescriptor.ActionName + Environment.NewLine +
                    "Action time: " + executionTime + "ms." + Environment.NewLine);
            }
        }
    }
}