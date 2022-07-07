using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IDS325__Indice_Académico_Proyecto.Models;

namespace IDS325__Indice_Académico_Proyecto.Controllers
{
    public class PersonasController : Controller
    {
        private IndiceEntities db = new IndiceEntities();

        // GET: Personas
        [Authorize]
        public ActionResult Index()
        {
            var personas = db.sp_ListarDocentes();
            return View(personas.ToList());
        }
        public ActionResult IndexEstudiante()
        {
            var personas = db.sp_ListarEstudiantes();
            return View(personas.ToList());
        }
       

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea");
            ViewBag.Carrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera");
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "DescripcionRol");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Matricula,IdRol,Carrera,CodigoArea,Nombre,Apellido,CorreoElectronico,FechaIngresoPersona,VigenciaPersona,Indice,Contraseña")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea", persona.CodigoArea);
            ViewBag.Carrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera", persona.Carrera);
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "DescripcionRol", persona.IdRol);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea", persona.CodigoArea);
            ViewBag.Carrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera", persona.Carrera);
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "DescripcionRol", persona.IdRol);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Matricula,IdRol,Carrera,CodigoArea,Nombre,Apellido,CorreoElectronico,FechaIngresoPersona,VigenciaPersona,Indice,Contraseña")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea", persona.CodigoArea);
            ViewBag.Carrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera", persona.Carrera);
            ViewBag.IdRol = new SelectList(db.Rol, "IdRol", "DescripcionRol", persona.IdRol);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
