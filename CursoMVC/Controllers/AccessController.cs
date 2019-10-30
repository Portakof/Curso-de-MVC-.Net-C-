using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;

namespace CursoMVC.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                //"using" se usa para crear la conexion con la base de datos una vez terminada la consulta se destruye el proceso
                using (cursomvcEntities db = new cursomvcEntities())    //objeto "db" para manejar la base datos
                {
                    //Se selecciona la tabla "user" de la bsase datos "cursomvc "
                    var lst = from d in db.user 
                              
                              //Se realiza una comparacion con los datos provenientes de la vista con los existentes en la base datos
                              //"idState == 1" para trater solo los que esten activos o coincidan
                              where d.email == user && d.password == password && d.idState == 1
                              select d; //"d" significa que los seleccione todos

                    //Se verifica que existe el usuario
                    if (lst.Count() > 0)
                    {
                        //Se crea la sesion y se le agrega los datos de la consulta realizada a la base datos
                        //"user" es el modelo de la tabla "user" creado por el Entity Framework
                        user OUser = lst.First();
                        Session["User"] = OUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Ocurrio un error");
                    }
                }

                
            }
            catch (Exception ex)
            {
                //"Content" es una funcion que regresa un string en vez de una vista
                return Content("Ocurrio un error" + ex.Message); 
            }
        }
    }
}