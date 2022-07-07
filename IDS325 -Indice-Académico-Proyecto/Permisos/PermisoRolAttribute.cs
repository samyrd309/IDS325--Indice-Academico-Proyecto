using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IDS325__Indice_Académico_Proyecto.Models;

namespace IDS325__Indice_Académico_Proyecto.Permisos
{
    public class PermisoRolAttribute : ActionFilterAttribute
    {
        private Roles idRol;

        public PermisoRolAttribute(Roles _idRol)
        {
            idRol = _idRol;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                sp_ValidarUsuario_Result usuario = HttpContext.Current.Session["Usuario"] as sp_ValidarUsuario_Result;

                if(usuario.IdRoles != idRol)
                {
                    filterContext.Result = new RedirectResult("~/Home/SinPermisos"); 
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}