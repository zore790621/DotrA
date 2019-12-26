using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Index()
        {
            this.Response.StatusCode = 500;

            return this.View();
        }
        public ActionResult Forbidden()
        {
            this.Response.StatusCode = 403;

            return this.View();
        }

        public ActionResult NotFound()
        {
            this.Response.StatusCode = 404;

            return this.View();
        }

        public ActionResult Other(int? code)
        {
            this.Response.StatusCode = code ?? 500;

            return this.View();
        }
    }
}