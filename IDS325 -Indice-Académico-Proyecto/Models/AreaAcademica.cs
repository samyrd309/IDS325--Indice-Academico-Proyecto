//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IDS325__Indice_Académico_Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AreaAcademica
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AreaAcademica()
        {
            this.Asignatura = new HashSet<Asignatura>();
            this.Persona = new HashSet<Persona>();
        }
    
        public string CodigoArea { get; set; }
        public string NombreArea { get; set; }
        public Nullable<System.DateTime> FechaIngresoArea { get; set; }
        public Nullable<bool> VigenciaArea { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignatura> Asignatura { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Persona> Persona { get; set; }
    }
}
