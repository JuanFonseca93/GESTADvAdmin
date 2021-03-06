﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string path = Path.Combine("C:/Gestad");
            Directory.CreateDirectory(path);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}