using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using KN_ProyectoClase.BaseDatos;

namespace KN_ProyectoClase.Models
{
    public class Utilitarios
    {
        public bool EviarCorreo(Usuario info, string mensaje, string titulo)
        {
            string cuenta = ConfigurationManager.AppSettings["CorreoNotificaciones"].ToString();
            string contrasenna = ConfigurationManager.AppSettings["ContrasennaNotificaciones"].ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(cuenta);
            mail.To.Add(new MailAddress(info.Correo));
            mail.Subject = titulo;
            mail.Body = mensaje;
            mail.Priority = MailPriority.Normal;
            mail.IsBodyHtml = true;

            SmtpClient MailClient = new SmtpClient("smtp.office365.com", 587);
            MailClient.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
            MailClient.EnableSsl = true;
            MailClient.Send(mail);
            return true;
        }

    }
}