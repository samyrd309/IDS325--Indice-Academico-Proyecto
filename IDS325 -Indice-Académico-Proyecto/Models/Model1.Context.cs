﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class IndiceEntities : DbContext
    {
        public IndiceEntities()
            : base("name=IndiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AreaAcademica> AreaAcademica { get; set; }
        public virtual DbSet<Asignatura> Asignatura { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<Carrera> Carrera { get; set; }
        public virtual DbSet<Literal> Literal { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Seccion> Seccion { get; set; }
    
        public virtual int sp_EditarPersona(Nullable<int> matricula, string carrera, string codigoArea, string correoElectronico, string contraseña)
        {
            var matriculaParameter = matricula.HasValue ?
                new ObjectParameter("Matricula", matricula) :
                new ObjectParameter("Matricula", typeof(int));
    
            var carreraParameter = carrera != null ?
                new ObjectParameter("Carrera", carrera) :
                new ObjectParameter("Carrera", typeof(string));
    
            var codigoAreaParameter = codigoArea != null ?
                new ObjectParameter("CodigoArea", codigoArea) :
                new ObjectParameter("CodigoArea", typeof(string));
    
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contraseñaParameter = contraseña != null ?
                new ObjectParameter("Contraseña", contraseña) :
                new ObjectParameter("Contraseña", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EditarPersona", matriculaParameter, carreraParameter, codigoAreaParameter, correoElectronicoParameter, contraseñaParameter);
        }
    
        public virtual int sp_EliminarPersona(Nullable<int> matricula)
        {
            var matriculaParameter = matricula.HasValue ?
                new ObjectParameter("Matricula", matricula) :
                new ObjectParameter("Matricula", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_EliminarPersona", matriculaParameter);
        }
    
        public virtual int sp_GuardarPersona(Nullable<int> idRol, string carrera, string codigoArea, string nombre, string apellido, string correoElectronico, string contraseña)
        {
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(int));
    
            var carreraParameter = carrera != null ?
                new ObjectParameter("Carrera", carrera) :
                new ObjectParameter("Carrera", typeof(string));
    
            var codigoAreaParameter = codigoArea != null ?
                new ObjectParameter("CodigoArea", codigoArea) :
                new ObjectParameter("CodigoArea", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidoParameter = apellido != null ?
                new ObjectParameter("Apellido", apellido) :
                new ObjectParameter("Apellido", typeof(string));
    
            var correoElectronicoParameter = correoElectronico != null ?
                new ObjectParameter("CorreoElectronico", correoElectronico) :
                new ObjectParameter("CorreoElectronico", typeof(string));
    
            var contraseñaParameter = contraseña != null ?
                new ObjectParameter("Contraseña", contraseña) :
                new ObjectParameter("Contraseña", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_GuardarPersona", idRolParameter, carreraParameter, codigoAreaParameter, nombreParameter, apellidoParameter, correoElectronicoParameter, contraseñaParameter);
        }
    
        public virtual ObjectResult<sp_ListarAsignaturas_Result> sp_ListarAsignaturas()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ListarAsignaturas_Result>("sp_ListarAsignaturas");
        }
    
        public virtual ObjectResult<sp_ListarDocentes_Result> sp_ListarDocentes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ListarDocentes_Result>("sp_ListarDocentes");
        }
    
        public virtual ObjectResult<sp_ListarEstudiantes_Result> sp_ListarEstudiantes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ListarEstudiantes_Result>("sp_ListarEstudiantes");
        }
    
        public virtual ObjectResult<sp_ListarSeccion_Result> sp_ListarSeccion()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ListarSeccion_Result>("sp_ListarSeccion");
        }
    
        public virtual ObjectResult<sp_ObtenerDocentes_Result> sp_ObtenerDocentes(Nullable<int> matricula)
        {
            var matriculaParameter = matricula.HasValue ?
                new ObjectParameter("Matricula", matricula) :
                new ObjectParameter("Matricula", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerDocentes_Result>("sp_ObtenerDocentes", matriculaParameter);
        }
    
        public virtual ObjectResult<sp_ObtenerEstudiantes_Result> sp_ObtenerEstudiantes(Nullable<int> matricula)
        {
            var matriculaParameter = matricula.HasValue ?
                new ObjectParameter("Matricula", matricula) :
                new ObjectParameter("Matricula", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ObtenerEstudiantes_Result>("sp_ObtenerEstudiantes", matriculaParameter);
        }
    
        public virtual ObjectResult<sp_ValidarUsuario_Result> sp_ValidarUsuario(Nullable<int> matricula, string contraseña)
        {
            var matriculaParameter = matricula.HasValue ?
                new ObjectParameter("Matricula", matricula) :
                new ObjectParameter("Matricula", typeof(int));
    
            var contraseñaParameter = contraseña != null ?
                new ObjectParameter("Contraseña", contraseña) :
                new ObjectParameter("Contraseña", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_ValidarUsuario_Result>("sp_ValidarUsuario", matriculaParameter, contraseñaParameter);
        }
    }
}
