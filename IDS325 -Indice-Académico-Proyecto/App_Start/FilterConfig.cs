using System.Web;
using System.Web.Mvc;

namespace IDS325__Indice_Académico_Proyecto
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
