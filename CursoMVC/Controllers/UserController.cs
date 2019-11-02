using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;
using CursoMVC.Models.TableViewModels;
using CursoMVC.Models.ViewModels;

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
                       orderby d.id          //Se ordenan en base al campo "email"
                       select new UserTableViewModel    //Se llena el objeto en base al modelo
                       {
                           Email = d.email,
                           Id = d.id,
                           Edad = d.edad
                       }).ToList();     //Se devuelven como tipo lista
            }


            return View(lst);   //Se envia a la vista los datos obtenidos de la tabla "user" cargados en un modelo de tipo "UserTableViewModel"
        }

//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&


        [HttpGet]   //Esta etiqueta obliga a que el metodo solo entre en funcionamiento por "get"
        public ActionResult Add()
        {
            return View();
        }

 //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
         // "[HttpPost]" se usa para realizar una petición a un servidor enviando información, sea para insertar o consultar algo y esperar
        // una respuesta luego de finalizada la operación recibir algún tipo de mensaje o información.
        [HttpPost]  //Esta etiqueta obliga a que el metodo solo entre en funcionamiento por "post"
        public ActionResult Add(UserViewModels model)
        {
            //De esta manera se valida o se ejecuta los "DataAnnotations" colocados en "UserViewModels.cs"            
            if (!ModelState.IsValid)    //se valida los "DataAnnotations"
            {
                return View(model);//si no se cumple los "DataAnnotations" se devuelve la vista con el modelo para que se corrijan o se llenen los campos de ser necesario
            }

            using (var db = new cursomvcEntities())     //se crea el objeto "db" con la instancia para porder manejar la base de datos "cursomvc"
            {
                user oUser = new user();    //"user" es un modelo creado por el Entity frameword que maneja la tabla "user" de la base de datos "cursomvc"
                oUser.idState = 1;          //Se llenan los valores con los recibidos de la vista
                oUser.email = model.Email;
                oUser.edad = model.Edad;
                oUser.password = model.Password;

                db.user.Add(oUser);     //se agrega el objeto oUser que es la representacion del "Entities Framework"

                db.SaveChanges();   //se guardan los cambios para agregar el usuario nuevo en la base datos
            }

            //Si todo esta bien se redirecciona al "UserUserController" en el cual se mostrara el nuevo usuario en la vista
            return Redirect(Url.Content("~/User/"));     
        }


//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        //Si no tiene una etiqueta [Http] significa que el metodo va a funcionar por default por [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditUserViewModels model = new EditUserViewModels();

            using (var db = new cursomvcEntities())
            {
                var oUser = db.user.Find(Id);     //"Find" trae un objeto de la base datos con ese "Id"
                model.Edad = (int)oUser.edad;
                model.Email = oUser.email;
                model.Id = oUser.id;
            }

            return View(model);
        }

//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        [HttpPost]
        public ActionResult Edit(EditUserViewModels model)
        {

            //De esta manera se valida o se ejecuta los "DataAnnotations" colocados en "EditUserViewModels.cs"            
            if (!ModelState.IsValid)    //se valida los "DataAnnotations"
            {
                return View(model);//si no se cumple los "DataAnnotations" se devuelve la vista con el modelo para que se corrijan o se llenen los campos de ser necesario
            }

            using (var db = new cursomvcEntities())
            {
                var oUser = db.user.Find(model.Id);     //"Find" trae un objeto de la base datos con ese "Id"
                oUser.email = model.Email;
                oUser.edad = model.Edad;

                //Entra solo si se escribio un nuevo "Password" de lo contrario se deja el ya existente en la base datos
                if (model.Password != null && model.Password.Trim() != "")
                {
                    oUser.password = model.Password;
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;    //De esta manera se le dice al Entity Framework que el regitro se modifico
                db.SaveChanges(); //Se actualiza en la base datos
            }

            //Si todo esta bien se redirecciona al "UserUserController" en el cual se mostrara el nuevo usuario en la vista
            return Redirect(Url.Content("~/User/"));
        }

//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            using (var db = new cursomvcEntities())
            {
                var oUser = db.user.Find(Id);     //"Find" trae un objeto de la base datos con ese "Id"

                //En el metodo "Index" al tomar los datos de la base datos, solo se tienen en cuenta los registros
                //que esten en "uno(1)" osea "d.idState == 1" al realizar el cambio por el numero "tres(3)" cuando
                //se vuelva a tomar los datos lo deja por fuera
                oUser.idState = 3;  

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;    //De esta manera se le dice al Entity Framework que el regitro se modifico
                db.SaveChanges(); //Se actualiza en la base datos
            }

            //"Content" en vez de regresar una vista regresa un contenido en este caso un string "1"
            return Content("1");
        }
    }
}

















