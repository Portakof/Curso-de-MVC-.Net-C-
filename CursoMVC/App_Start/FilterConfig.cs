using System.Web;
using System.Web.Mvc;

namespace CursoMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.VerifySesion());    //Se activa el filtro creado en la ruta "Filters/VerifySesion"
        }
    }
}
