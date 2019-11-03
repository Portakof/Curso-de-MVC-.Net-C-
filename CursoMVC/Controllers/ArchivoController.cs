
using System.Web.Mvc;
using CursoMVC.Models.ViewModels;
using System.IO;

namespace CursoMVC.Controllers
{
    public class ArchivoController : Controller
    {
        // GET: Archivo
        public ActionResult Index()
        {
            //Se valida y se imprime el mensaje enviado desde el metodo "Save" cabe recordar que se esta en el metodo "Index"
            if (TempData["Message"] != null) 
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View();
        }
        //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        //"Save" es el encargado de recibir el modelo con los datos de la solicitud hecha por la vista para guardar los archivos
        // "[HttpPost]" se usa para realizar una petición a un servidor enviando información, sea para insertar o consultar algo y esperar
        // una respuesta luego de finalizada la operación recibir algún tipo de mensaje o información.
        [HttpPost]  //Esta etiqueta obliga a que el metodo solo entre en funcionamiento por "post"
        public ActionResult Save(ArchivoViewModels model)
        {
            //"MapPath" devuelve la ruta fisica del sitio web esn este caso donde esta guardado 
            //el proyecto "D:\01 UDI\02 HTML\06 Curso de MVC .Net C#"
            string RutaSitio = Server.MapPath("~/");

            //"Combine" combina cadenas de caracteres, en este caso ruta del sitio web con la carpeta 
            //creada en el proyecto para guardar los archivos seguido del nombre que se le asignara y su debida extension
            string PathArchivo1 = Path.Combine(RutaSitio + "/Files/archivo1.png");
            string PathArchivo2 = Path.Combine(RutaSitio + "/Files/archivo2.png");

            //De esta manera se valida o se ejecuta los "DataAnnotations" colocados en "ArchivoViewModels.cs"            
            if (!ModelState.IsValid)
            {
                //Si hubo un error con los "DataAnnotations" retorna a la vista el metodo a acceder "Index" 
                //y el model por si hay algun campo llenado lo deje tal como esta
                return View("Index", model);    
            }

            //"SaveAs" Se guradan los archivos segun la ruta especificada
            model.Archivo1.SaveAs(PathArchivo1);
            model.Archivo2.SaveAs(PathArchivo2);

            //"@TempData" envia un mensaje que no desaparece al cambiar de metodo o de vista en el controller en este caso de "Save a Index " 
            //es la diferencia del ViewBag que solo funciona en el mismo metodo 
            TempData["Message"] = "Se cargaron los archivos";

            //"RedirectToAction" permite redireccionar a distintos controller esa es la diferencia con "View"
            return RedirectToAction("Index");
        }
    }
}