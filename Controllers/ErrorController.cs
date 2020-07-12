using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace StudentManagement.Controllers
{
    public class ErrorController : Controller
    {

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            //获取异常细节
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }


        // GET: /<controller>/
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult =
               HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, your request page not exit";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
            }

            return View("NotFound");
        }
    }
}
