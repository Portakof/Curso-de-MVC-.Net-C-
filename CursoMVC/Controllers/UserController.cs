using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;
using CursoMVC.Models.TableViewModels;

namespace CursoMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<UserTableViewModel> lst = null;    //Se crea una lista del modelo "UserTableViewModel"

            using (cursomvcEntities db = new cursomvcEntities())    //Se crea el objeto para acceder a la base datos
            {
                lst = (from d in db.user        //Se accedera a la tabla "user" de la base datos
                       where d.idState == 1     //Se Toman solo los registros que coincidan con "1" en el campo "idState"
                       orderby d.email          //Se ordenan en base al campo "email"
                       select new UserTableViewModel    //Se llena el objeto en base al modelo
                       {
                           Email = d.email,
                           Id = d.id,
                           Edad = d.edad
                       }).ToList();     //Se devuelven como tipo lista
            }


            return View(lst);   //Se envia a la vista los datos obtenidos de la tabla "user" cargados en un modelo de tipo "UserTableViewModel"
        }
    }
}