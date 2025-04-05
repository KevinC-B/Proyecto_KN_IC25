using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;

namespace KN_ProyectoClase.Controllers
{
    public class PrincipalController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();    

        #region RegistrarCuenta
        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RegistrarCuenta");
                return View("Error");
            }
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

            try
            {
                //EF utilizando SP
                using (var context = new KN_DBEntities())
                {
                    var result = context.RegistrarCuenta(model.Identificacion, model.Contrasenna, model.Nombre, model.Correo);

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
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post RegistrarCuenta");
                return View("Error");
            }
        }
        #endregion

        #region IniciarSesion
        [HttpGet]
        public ActionResult IniciarSesion()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get IniciarSesion");
                return View("Error");
            }
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

            try
            {
                //EF SP
                using (var context = new KN_DBEntities())
                {
                    var info = context.IniciarSesion(model.Identificacion, model.Contrasenna).FirstOrDefault();

                    if (info != null)
                    {
                        Session["IdUsuario"] = info.Id;
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
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post IniciarSesion");
                return View("Error");
            }
        }
        #endregion

        #region RecuperarContrasena
        [HttpGet]
        public ActionResult RecuperarContrasena()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RecuperarContrasena");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioModel model)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.Usuario.Where(x => x.Correo == model.Correo
                                                       && x.Estado == true).FirstOrDefault();
                    if (info != null)
                    {
                        var codigoTemporal = CrearCodigo();

                        info.Contrasenna = codigoTemporal;
                        context.SaveChanges();

                        string mensaje = $"Hola {info.Nombre}, por favor utilice el siguiente código para ingresar al sistema: {codigoTemporal}";
                        var notificacion = util.EviarCorreo(info, mensaje, "Acceso al sistema KN");

                        if (notificacion)
                        {
                            return RedirectToAction("IniciarSesion", "Principal");
                        }
                        else
                        {
                            ViewBag.Mensaje = "Su información no se ha podido validar correctamente";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Su información no se ha podido validar correctamente";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post RecuperarContrasena");
                return View("Error");
            }
        }

        #endregion

        [HttpGet]
        public ActionResult Inicio()
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    var info = context.ConsultarOfertas().Where(x => x.Disponible == true).ToList();
                    return View(info);

                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get RegistrarCuenta");
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult CerrarSession()
        {
            try
            {
                Session.Clear();
                return RedirectToAction("Inicio", "Principal");
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get CerrarSession");
                return View("Error");
            }
        }

        private string CrearCodigo()
        {
            int length = 5;
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}