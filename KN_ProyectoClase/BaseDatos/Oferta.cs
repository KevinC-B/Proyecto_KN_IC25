//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KN_ProyectoClase.BaseDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oferta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oferta()
        {
            this.UsuariosOferta = new HashSet<UsuariosOferta>();
        }
    
        public long Id { get; set; }
        public long IdPuesto { get; set; }
        public int Cantidad { get; set; }
        public decimal Salario { get; set; }
        public string Horario { get; set; }
        public bool Disponible { get; set; }
        public string Imagen { get; set; }
    
        public virtual Puesto Puesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuariosOferta> UsuariosOferta { get; set; }
    }
}
