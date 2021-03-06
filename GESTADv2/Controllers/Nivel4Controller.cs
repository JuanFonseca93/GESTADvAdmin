﻿using Core.Dominio;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class Nivel4Controller : Controller
    {
        Nivel4 Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: AreaDocumento
        public ActionResult AreaDoc(List<Nivel4> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel4> area = new List<Nivel4>();
            if (obj == null)
            {
                foreach (var nusuario in unitOfWork.Nivel4.GetAll())
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Nivel4>;
            }
            int tamanoPagina = 5;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        [HttpPost]
        public ActionResult BuscarAreaD(string nombre)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context = new BDContext();


                    var busqueda = from nombreUsu in _context.Nivel4 select nombreUsu;

                    if (nombre != null || !nombre.Equals(""))
                    {

                        busqueda = from nombreUsu in _context.Nivel4 where nombreUsu.nombreN.Contains(nombre) select nombreUsu;
                    }
                    List<Nivel4> usu = new List<Nivel4>();

                    foreach (var item in busqueda)
                    {
                        usu.Add(item);
                    }

                    TempData["area"] = usu;

                    return RedirectToAction("AreaDoc", new { obj = usu });

                }
                else
                {
                    return RedirectToAction("AreaDoc");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("AreaDoc");
            }
        }

        [HttpGet]
        public ActionResult CrearAreaD(Nivel4 obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel3> Areas = new List<Nivel3>();
            foreach (var nusuario in unitOfWork.Nivel3.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            return View(obj);
        }

        [HttpPost]
        public ActionResult ActionCrearAreaD(Nivel4 obj)
        {

            if (ModelState.IsValid)
            {
                _context = new BDContext();
                unitOfWork = new UnitOfWork(_context);
                unitOfWork.Nivel4.Add(obj);
                unitOfWork.Complete();
                var n3 = unitOfWork.Nivel3.Get(Int32.Parse(obj.nivel3));
                var n2 = unitOfWork.Nivel2.Get(Int32.Parse(n3.nivel2));
                var n1 = unitOfWork.Nivel1.Get(Int32.Parse(n2.nivel1));

                string path = Path.Combine("C:/Gestad", n1.nombreN, n2.nombreN, n3.nombreN, obj.nombreN);
                Directory.CreateDirectory(path);
                return RedirectToAction("AreaDoc");
            }
            else
            {
                return RedirectToAction("CrearAreaD", obj);
            }
        }

        [HttpGet]
        public ActionResult ActualizarAreaD(int idArea)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Nivel3> Areas = new List<Nivel3>();
            foreach (var nusuario in unitOfWork.Nivel3.GetAll())
            {
                Areas.Add(nusuario);
            }
            ViewBag.Areas = Areas;
            Uppd = unitOfWork.Nivel4.Get(idArea);
            return View(Uppd);
        }

        [HttpPost]
        public ActionResult ActionActualizarAreaD(Nivel4 obj)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context = new BDContext();
                    unitOfWork = new UnitOfWork(_context);
                    Uppd = unitOfWork.Nivel4.Get(obj.idN);
                    unitOfWork.Nivel4.Update(Uppd);
                    Uppd.nombreN = obj.nombreN;
                    Uppd.descripcionN = obj.descripcionN;

                    _context.Configuration.ValidateOnSaveEnabled = false;

                    unitOfWork.Complete();

                    var n3 = unitOfWork.Nivel3.Get(Int32.Parse(obj.nivel3));
                    var n2 = unitOfWork.Nivel2.Get(Int32.Parse(n3.nivel2));
                    var n1 = unitOfWork.Nivel1.Get(Int32.Parse(n2.nivel1));
                    string path = Path.Combine("C:/Gestad", n1.nombreN, n2.nombreN, n3.nombreN, obj.nombreN);
                    Directory.CreateDirectory(path);

                    return RedirectToAction("AreaDoc");
                }
                else
                {
                    return View("ActualizarAreaD", obj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Un error ha ocurrido '{0}'", ex);
                return View("ActualizarAreaD");
            }

        }
    }
}