using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicPortal.Filters
{
    using MusicPortal.Services;
    using MusicPortal.Models;
    using System.Globalization;

    public class CultureAttribute() : Attribute, IActionFilter 
    {
        public void OnActionExecuted(ActionExecutedContext context) { }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string code = context.HttpContext.Request.Cookies["lang"] ?? "uk";
            IRead languageRead = context.HttpContext.RequestServices.GetService<IRead>()!;
            IEnumerable<string> languages = languageRead.GetList().Select(l => l.Code);
            if (!languages.Contains(code))
            {
                code = "uk";
            }
            context.HttpContext.Session.SetString("lang", code);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(code);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(code);
        }
    }
}
