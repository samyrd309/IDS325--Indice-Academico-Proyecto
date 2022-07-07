using IDS325__Indice_Académico_Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IDS325__Indice_Académico_Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private IndiceEntities db = new IndiceEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(sp_ValidarUsuario_Result validar)
        {
            if (validar.Contraseña != null)
            {
                var ob = db.sp_ValidarUsuario(validar.Matricula, validar.Contraseña);
                FormsAuthentication.SetAuthCookie(validar.Matricula.ToString(), false);

                Session["Usuario"] = ob;
                Session["Rol"] = validar.IdRol;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
                     
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tiene rango mio";
            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session["Usuario"] = null;

            return RedirectToAction("Login", "Home");
        }

    }
}