using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        #region RegistrarCuenta
        [HttpGet]
        public ActionResult RegistrarCuenta() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioModel model)
        {
            //EF(EntityFramework)

            //EF utilizando LinQ
            //using (var context = new KN_DBEntities())
            //{
            //    Usuario tabla = new Usuario();
            //    tabla.Identificacion = model.Identificacion;
            //    tabla.Contrasena = model.Contrasena;
            //    tabla.Nombre = model.Nombre;
            //    tabla.Correo = model.Correo;
            //    tabla.Estado = true;
            //    tabla.IdPerfil = 2;

            //    context.Usuario.Add(tabla); //Añade los atributos(info) que le pasamos por vista a la BD
            //    context.SaveChanges(); //Guarda los cambios
            //}

            //EF utilizando SP
            using (var context = new KN_DBEntities())
            {
                var result = context.RegistrarCuenta(model.Identificacion, model.Contrasena, model.Nombre, model.Correo);

                if (result > 0)
                {
                    return RedirectToAction("IniciarSesion", "Principal");
                } 
                else
                {
                    ViewBag.Mensaje = "Su información no se ha podido registrar correctamente";
                    return View();
                }
            }

            return View();
        }
        #endregion

        #region IniciarSesion
        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioModel model)
        {
            //EF Linq
            //using(var context = new KN_DBEntities())
            //{
            //    var info = context.Usuario.
            //        Where(x => x.Identificacion == model.Identificacion 
            //                && x.Contrasena == model.Contrasena
            //                && x.Estado == true).FirstOrDefault();

            //    if (info != null)
            //    {
            //        return RedirectToAction("Inicio", "Principal");
            //    }
            //}

            //EF SP
            using(var context = new KN_DBEntities())
            {
                var info = context.IniciarSesion(model.Identificacion, model.Contrasena).FirstOrDefault();

                if (info != null)
                {
                    Session["NombreUsuario"] = info.NombreUsuario;
                    Session["NombrePerfilUsuario"] = info.NombrePerfil;
                    Session["IdPerfilUsuario"] = info.IdPerfil;
                    return RedirectToAction("Inicio", "Principal");
                }
                else
                {
                    ViewBag.Mensaje = "Su información no se ha podido validar correctamente";
                    return View();
                }
            }
        }
        #endregion

        [HttpGet]
        public ActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSession()
        {
            Session.Clear();
            return RedirectToAction("Inicio", "Principal");
        }

        [HttpGet]
        public ActionResult RecuperarContrasena()
        {
            return View();
        }

    }
}