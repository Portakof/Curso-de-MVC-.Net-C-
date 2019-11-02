using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CursoMVC.Controllers
{
    //Este conntroler se crea solo para menejar el cierre de la sesion, esto para no crear un conflicto con el filtro 
    //creado en "Filters/VerifySesion.cs"
    public class CerrarController : Controller
    {
        
        public ActionResult Logoff()
        {
            Session["User"] = null;     //Se cierra la sesion
            return RedirectToAction("Index", "Access");     //Se redirecciona para poder iniciar nuevo login o inicio de sesion
        }
    }
}