using IDS325__Indice_Académico_Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IDS325__Indice_Académico_Proyecto.Controllers
{
    public class DocenteController : Controller
    {

        IndiceEntities indiceEntities = new IndiceEntities();
        // GET: Docente
        public ActionResult Index()
        { 
            var docentes = indiceEntities.sp_ListarDocentes();
            return View(docentes);
        }

        // GET: Docente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Docente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Docente/Create
        [HttpPost]
        public ActionResult Create(string Carrera,  string CodigoArea, string Nombre, string Apellido, string Correo, string Contraseña)
        {
            try
            {
                var x = indiceEntities.sp_GuardarPersona(3,Carrera,CodigoArea,Nombre,Apellido,Correo,Contraseña);
                indiceEntities.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Docente/Edit/5
        public ActionResult Edit(int id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            sp_ObtenerDocentes_Result db = new sp_ObtenerDocentes_Result();
            var per = indiceEntities.sp_ObtenerDocentes(id);
            foreach (var item in per)
            {
                db = new sp_ObtenerDocentes_Result()
                {
                    Carrera = item.Carrera,
                    CodigoArea = item.CodigoArea,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    CorreoElectronico = item.CorreoElectronico,
                 
                };
            }
            return View(db);
        }

        // POST: Docente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Docente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Docente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
