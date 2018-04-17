using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syncfusion.JavaScript;

namespace GESTADexplorer.Controllers
{
    public class ExplorerController : Controller
    {
        // GET: Explorador
        public ActionResult Explorador(FileExplorerParams args)
        {
            FileExplorerOperations operation = new FileExplorerOperations();
            switch (args.ActionType)
            {
                case "Read":
                    return Json(operation.Read(args.Path, args.ExtensionsAllow));
                case "CreateFolder":
                    return RedirectToAction("AreaDoc", "Nivel1");
                case "Paste":
                    return null;
                case "Remove":
                    return null;
                case "Rename":
                    return RedirectToAction("Documento", "Documento");
                case "GetDetails":
                    return RedirectToAction("Documento", "Documento");
                case "Download":
                    RedirectToAction("Documento", "Documento");
                    break;
                case "Upload":
                    return null;
                case "Search":
                    return RedirectToAction("Documento", "Documento");
            }
            //return Json("", JsonRequestBehavior.AllowGet);
            ViewBag.MyExplorer = operation;
            return View(operation);

        }

    }
}