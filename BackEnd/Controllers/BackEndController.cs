﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001.Controllers
{
    public class BackEndController : Controller
    {
        // GET: BackEnd
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
    }
}