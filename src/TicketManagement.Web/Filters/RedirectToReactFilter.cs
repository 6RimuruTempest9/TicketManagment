using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;

namespace TicketManagement.Web.Filters
{
    public class RedirectToReactFilter : IDisabledFeaturesHandler
    {
        private readonly string _pathToReact;

        public RedirectToReactFilter(string pathToReact)
        {
            _pathToReact = pathToReact;
        }

        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path.ToString();

            context.Result = new RedirectResult(_pathToReact + path);

            return Task.CompletedTask;
        }
    }
}