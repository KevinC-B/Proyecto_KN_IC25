using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KN_ProyectoClase.BaseDatos;
using KN_ProyectoClase.Models;

namespace KN_ProyectoClase.Controllers
{
    public class UsuarioController : Controller
    {
        RegistroErrores error = new RegistroErrores();
        Utilitarios util = new Utilitarios();

        #region CambiarAcceso
        [HttpGet]
        public ActionResult CambiarAcceso()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get CambiarAcceso");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult CambiarAcceso(UsuarioModel model)
        {
            try
            {
                if (model.Contrasenna != model.ConfirmarContrasenna)
                {
                    ViewBag.Mensaje = "Las contraseñas no concuerdan";
                    return View();
                }

                using (var context = new KN_DBEntities())
                {
                    long idSession = long.Parse(Session["IdUsuario"].ToString());
                    var info = context.Usuario.Where(x => x.Id == idSession).FirstOrDefault();

                    if (info != null)
                    {
                        if (model.ContrasennaAnterior != info.Contrasenna)
                        {
                            ViewBag.Mensaje = "Las contraseñas no concuerdan";
                            return View();
                        }

                        info.Contrasenna = model.Contrasenna;
                        context.SaveChanges();

                        string mensaje = $"Hola {info.Nombre}, se ha realizado el cambio de contraseña.";
                        var notificacion = util.EviarCorreo(info, mensaje, "Seguridad Sistema KN");

                        if (notificacion)
                            return RedirectToAction("Inicio", "Principal");
                    }
                    
                    ViewBag.Mensaje = "Su contraseña no se ha podido actualizar correctamente";
                    return View();
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post CambiarAcceso");
                return View("Error");
            }
        }
        #endregion

        #region ActualizarDatos
        [HttpGet]
        public ActionResult ActualizarDatos()
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    long idSession = long.Parse(Session["IdUsuario"].ToString());
                    var info = context.Usuario.Where(x => x.Id == idSession).FirstOrDefault();

                    return View(info);
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Get ActualizarDatos");
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ActualizarDatos(UsuarioModel model)
        {
            try
            {
                using (var context = new KN_DBEntities())
                {
                    long idSession = long.Parse(Session["IdUsuario"].ToString());
                    var info = context.Usuario.Where(x => x.Id == idSession).FirstOrDefault();

                    var infoCorreo = context.Usuario.Where(x => x.Correo == model.Correo 
                                                             && x.Id != idSession).FirstOrDefault();

                    if (infoCorreo == null)
                    {

                        info.Identificacion = model.Identificacion;
                        info.Nombre = model.Nombre;
                        info.Correo = model.Correo;
                        var result = context.SaveChanges();

                        if (result > 0)
                        {
                            Session["NombreUsuario"] = model.Nombre;
                            return RedirectToAction("Inicio", "Principal");
                        }
                    }

                    ViewBag.Mensaje = "Su información no se ha podido actualizar correctamente";
                    return View();
                }
            }
            catch (Exception ex)
            {
                error.RegistrarError(ex.Message, "Post ActualizarDatos");
                return View("Error");
            }
        }
        #endregion
    }
}