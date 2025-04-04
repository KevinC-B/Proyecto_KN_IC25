using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_ProyectoClase.Models
{
    public class UsuarioModel
    {
        public string Identificacion { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public string ContrasenaAnterior { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}