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
    public class AsignaturasController : Controller
    {
        private IndiceEntities db = new IndiceEntities();

        // GET: Asignaturas
        public ActionResult Index()
        {
            var asignaturas = db.Asignatura.Include(a => a.AreaAcademica).Include(a => a.Carrera);
            return View(asignaturas.ToList());
        }

        // GET: Asignaturas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignatura asignatura = db.Asignatura.Find(id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            return View(asignatura);
        }

        // GET: Asignaturas/Create
        public ActionResult Create()
        {
            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea");
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera");
            return View();
        }

        // POST: Asignaturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoAsignatura,CodigoCarrera,CodigoArea,Credito,NombreAsignatura,FechaIngresoAsignatura,VigenciaAsignatura")] Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                db.Asignatura.Add(asignatura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea", asignatura.CodigoArea);
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera", asignatura.CodigoCarrera);
            return View(asignatura);
        }

        // GET: Asignaturas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignatura asignatura = db.Asignatura.Find(id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea", asignatura.CodigoArea);
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera", asignatura.CodigoCarrera);
            return View(asignatura);
        }

        // POST: Asignaturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoAsignatura,CodigoCarrera,CodigoArea,Credito,NombreAsignatura,FechaIngresoAsignatura,VigenciaAsignatura")] Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignatura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoArea = new SelectList(db.AreaAcademica, "CodigoArea", "NombreArea", asignatura.CodigoArea);
            ViewBag.CodigoCarrera = new SelectList(db.Carrera, "CodigoCarrera", "NombreCarrera", asignatura.CodigoCarrera);
            return View(asignatura);
        }

        // GET: Asignaturas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignatura asignatura = db.Asignatura.Find(id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            return View(asignatura);
        }

        // POST: Asignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Asignatura asignatura = db.Asignatura.Find(id);
            db.Asignatura.Remove(asignatura);
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
