using System;
using System.IO;
using System.Web.Mvc;

namespace ThirdPartyEventEditor.Filters
{
    public class ExceptionHandlerFilterAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly string _pathToLogFile;

        public ExceptionHandlerFilterAttribute(string pathToLogFile)
        {
            _pathToLogFile = pathToLogFile ?? throw new ArgumentNullException(nameof(pathToLogFile));
        }

        public void OnException(ExceptionContext filterContext)
        {
            var errorMessage = LogError(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            
            var viewResult = new ViewResult
            {
                ViewName = "Error",
            };

            viewResult.ViewBag.ErrorMessage = errorMessage;

            filterContext.Result = viewResult;
        }

        private string LogError(Exception ex)
        {
            var errorMessage = "Error message: " + ex.Message + Environment.NewLine +
                "Stack trace: " + ex.StackTrace + Environment.NewLine;

            using (var stream = File.Open(_pathToLogFile, FileMode.Append))
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.WriteLine(errorMessage);
            }

            return "Error message: " + ex.Message + Environment.NewLine +
                "Stack trace: " + ex.StackTrace + Environment.NewLine;
        }
    }
}