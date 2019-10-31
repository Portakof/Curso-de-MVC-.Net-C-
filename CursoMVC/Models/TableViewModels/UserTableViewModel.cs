using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoMVC.Models.TableViewModels
{
    public class UserTableViewModel
    {        
        public int Id { get; set; }    
        public string Email { get; set; }

        //Se usa "?" en el tipo "int" para especificar que acepte valores nulos y asi no genere error con la base datos
        public int? Edad { get; set; }
    }
}