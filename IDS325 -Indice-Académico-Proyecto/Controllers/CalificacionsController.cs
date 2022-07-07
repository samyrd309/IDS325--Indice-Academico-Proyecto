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
    public class CalificacionsController : Controller
    {
        private IndiceEntities db = new IndiceEntities();

        // GET: Calificacions
        public ActionResult Index()
        {
            var calificacion = db.Calificacion.Include(c => c.Seccion).Include(c => c.Persona);
            return View(calificacion.ToList());
        }

        // GET: Calificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // GET: Calificacions/Create
        public ActionResult Create()
        {
            ViewBag.IdSeccion = new SelectList(db.Seccion, "IdSeccion", "Numero");
            ViewBag.Matricula = new SelectList(db.sp_ListarEstudiantes(), "Matricula", "Nombre");
            return View();
        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Matricula,CodigoAsignatura,Trimestre,Nota,IdSeccion,FechaIngresoCalificacion,VigenciaCalificacion")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Calificacion.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSeccion = new SelectList(db.Seccion, "IdSeccion", "CodigoAsignatura", calificacion.IdSeccion);
            ViewBag.Matricula = new SelectList(db.Persona, "Matricula", "Carrera", calificacion.Matricula);
            return View(calificacion);
        }

        // GET: Calificacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSeccion = new SelectList(db.Seccion, "IdSeccion", "CodigoAsignatura", calificacion.IdSeccion);
            ViewBag.Matricula = new SelectList(db.Persona, "Matricula", "Carrera", calificacion.Matricula);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Matricula,CodigoAsignatura,Trimestre,Nota,IdSeccion,FechaIngresoCalificacion,VigenciaCalificacion")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSeccion = new SelectList(db.Seccion, "IdSeccion", "CodigoAsignatura", calificacion.IdSeccion);
            ViewBag.Matricula = new SelectList(db.Persona, "Matricula", "Carrera", calificacion.Matricula);
            return View(calificacion);
        }

        // GET: Calificacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calificacion calificacion = db.Calificacion.Find(id);
            db.Calificacion.Remove(calificacion);
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
