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
    public class SeccionsController : Controller
    {
        private IndiceEntities db = new IndiceEntities();

        // GET: Seccions
        public ActionResult Index()
        {
            var seccion = db.Seccion.Include(s => s.Asignatura).Include(s => s.Persona);
            return View(seccion.ToList());
        }

        // GET: Seccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // GET: Seccions/Create
        public ActionResult Create()
        {
            ViewBag.CodigoAsignatura = new SelectList(db.Asignatura, "CodigoAsignatura", "NombreAsignatura");
            ViewBag.Matricula = new SelectList(db.sp_ListarDocentes(), "Matricula", "Nombre");
            return View();
        }

        // POST: Seccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoAsignatura,IdSeccion,Matricula,FechaIngresoSeccion,VigenciaSección,NumeroSección")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Seccion.Add(seccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoAsignatura = new SelectList(db.Asignatura, "CodigoAsignatura", "CodigoCarrera", seccion.CodigoAsignatura);
            ViewBag.Matricula = new SelectList(db.Persona, "Matricula", "Carrera", seccion.Matricula);
            return View(seccion);
        }

        // GET: Seccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoAsignatura = new SelectList(db.Asignatura, "CodigoAsignatura", "CodigoCarrera", seccion.CodigoAsignatura);
            ViewBag.Matricula = new SelectList(db.Persona, "Matricula", "Carrera", seccion.Matricula);
            return View(seccion);
        }

        // POST: Seccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoAsignatura,IdSeccion,Matricula,FechaIngresoSeccion,VigenciaSección,NumeroSección")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoAsignatura = new SelectList(db.Asignatura, "CodigoAsignatura", "CodigoCarrera", seccion.CodigoAsignatura);
            ViewBag.Matricula = new SelectList(db.Persona, "Matricula", "Carrera", seccion.Matricula);
            return View(seccion);
        }

        // GET: Seccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // POST: Seccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seccion seccion = db.Seccion.Find(id);
            db.Seccion.Remove(seccion);
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
