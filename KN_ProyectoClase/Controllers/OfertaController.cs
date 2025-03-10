using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KN_ProyectoClase.BaseDatos;

namespace KN_ProyectoClase.Controllers
{
    public class OfertaController : Controller
    {
        [HttpGet]
        public ActionResult ConsultarOfertas()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarOfertas().ToList();
                return View(info);

            }
        }

        [HttpGet]
        public ActionResult ConsultarOfertasDisponibles()
        {
            using (var context = new KN_DBEntities())
            {
                var info = context.ConsultarOfertas().ToList();
                return View(info);

            }
        }

        [HttpGet]
        public ActionResult AgregarOferta()
        {
            using (var context = new KN_DBEntities())
            {
                ViewBag.PuestoCombo = context.ConsultarPuestos().ToList();

            }

            return View();
        }
    }
}