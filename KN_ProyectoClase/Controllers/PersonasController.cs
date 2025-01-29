using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KN_ProyectoClase.Models;

namespace KN_ProyectoClase.Controllers
{
    public class PersonasController : Controller
    {
        [HttpGet]
        public ActionResult RegistrarPersona()
        {
            var personasModelo = new PersonasModel();
            personasModelo.identificacion = "305430780";
            personasModelo.nombre = "Kevin";

            return View(personasModelo);
        }

        [HttpPost]
        public ActionResult RegistrarPersona(PersonasModel modelo)
        { 
            return View();
        }
    }
}