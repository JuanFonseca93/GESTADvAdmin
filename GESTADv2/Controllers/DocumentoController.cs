using Core.Dominio;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class DocumentoController : Controller
    {
        Documento Uppd;
        UnitOfWork unitOfWork;
        BDContext _context;
        // GET: Documento
        public ActionResult Documento(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 1))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult DocumentoA(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 2))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult DocumentoR(List<Documento> obj, int? pagina)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            List<Documento> area = new List<Documento>();
            if (obj == null)
            {
                foreach (var nusuario in _context.Documento.Where(d => d.estadoDocumento == 3))
                {
                    area.Add(nusuario);
                }
            }
            else
            {
                area = TempData["area"] as List<Documento>;
            }
            int tamanoPagina = 15;
            int numeroPagina = (pagina ?? 1);

            return View(area.ToPagedList(numeroPagina, tamanoPagina));
        }

        public ActionResult CrearArea(Documento obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);

            return View(obj);
        }

        public ActionResult AceptarDocumento(Documento obj)
        {
            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.Docs.Get(obj.idDocumento);
            unitOfWork.Docs.Update(Uppd);



            Uppd.estadoDocumento = 2;

            _context.Configuration.ValidateOnSaveEnabled = false;

            unitOfWork.Complete();

            Usuario Uppd2 = unitOfWork.Usuarios.Get(obj.idUsuario);
            var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
            var toAddress = new MailAddress(Uppd2.correoUsuario, Uppd2.nombreUsuario);
            const string fromPassword = "Gestad00";
            const string subject = "Estatus del Documento";

            string mensaje = "Felicidades, Apartir de ahora Puede agregar Colaboraciones al Documento " + obj.nombreDocumento + ", Cualquier duda favor de responder a este correo.";


            string body = mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return RedirectToAction("DocumentoA");


        }

        public ActionResult RechazarDocumento(Documento obj)
        {


            _context = new BDContext();
            unitOfWork = new UnitOfWork(_context);
            Uppd = unitOfWork.Docs.Get(obj.idUsuario);
            unitOfWork.Docs.Update(Uppd);

            Uppd.estadoDocumento = 3;

            _context.Configuration.ValidateOnSaveEnabled = false;

            unitOfWork.Complete();

            Usuario Uppd2 = unitOfWork.Usuarios.Get(obj.idUsuario);
            var fromAddress = new MailAddress("gestadutsoe@gmail.com", "GESTAD");
            var toAddress = new MailAddress(Uppd2.correoUsuario, Uppd2.nombreUsuario);
            const string fromPassword = "Gestad00";
            const string subject = "Estatus del Documento";

            string mensaje = "Lamentablemente el documento " + obj.nombreDocumento + " fue rechazado, Cualquier duda favor de responder a este correo.";


            string body = mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return RedirectToAction("DocumentoR");

        }
    }
}