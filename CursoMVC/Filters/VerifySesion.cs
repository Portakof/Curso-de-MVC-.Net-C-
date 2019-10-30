using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CursoMVC.Models;
using CursoMVC.Controllers;

namespace CursoMVC.Filters
{
    //"Esta clase manejara un filtro que indica donde se puede acceder si se tiene o no sesion"
    //Para que el filtro funciones debe heredar de este clase "ActionFilterAttribute" 
    public class VerifySesion : ActionFilterAttribute
    {
        //"OnActionExecuting" es un metodo heredado de "ActionFilterAttribute" cuando se coloca "override" se le dice
        //al programa que primero ejecute mi siguiente codigo y luego haga el suyo propio
        //Una vez hecho mi codigo se utiliza "base.OnActionExecuting(filterContext);" el cual ejecuta el codigo propio del metodo sobreescrito

        //Este metodo se ejecuta de la siguiente manera, primero se ejecuta antes de ejecutarse el controller, se valida que exista sesion con 
        //el primer "if", acto seguido se valida a cual controller es el que se desea ingresar, dado el caso sea diferente a "AccessController"
        //te redireciona a este para que inicies sesion en el login, una vez creado el filtro es necesario activarlo en la ruta "App_Star/FilterConfig.cs"
        //El "else" se usa para validar cuando ya se tiene sesion iniciada no se ingrese nuevamente a "AccessController" si no que se redireccione a "Home/Index"

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //"user" es el modelo de la tabla "user" creado por el Entity Framework
            var oUser = (user)HttpContext.Current.Session["User"];

            if (oUser == null)
            {
                if(filterContext.Controller is AccessController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
            else
            {
                if (filterContext.Controller is AccessController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}