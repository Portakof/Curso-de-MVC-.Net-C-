using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CursoMVC.Models.ViewModels
{
    //Esta clase se crea porque son los unicos datos que el cliente podra crear y que seran ingresados a la base datos
    //Estos parametros se llenan por el formulario creado en "Register.cshtml"
    //"Register.cshtml" es la vista creada para que el usuario pueda ingresar los datos de registro

    public class UserViewModels
    {
        [Required]                      //Hace que el campo o textbox se obligatorio ingresarle datos
        [EmailAddress]                  //valida que el texto incluido se de tipo mail osea "@mail.com"
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {1} caracteres",MinimumLength = 1)]//restringe o permite solo cierta cantidad de caracteres "(100)"
        [Display(Name = "Correo electronico")]//ES el nombre que mostrara la vista
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]   //De esta maneja cuando se envia el dato no es visible, como si estubiera encriptado
        [Display(Name = "Contraseña")]
        public string Password  { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        //realiza una comparacion entre dos campos dentro de los parantesis debe ir el nombre de la variable a comparar
        [Compare("Password", ErrorMessage ="Las contraseñas no son correctas")]              //asi se compara "Password" y "ConfirmPassword"
        public string ConfirmPassword { get; set; }

        [Required]
        public int Edad { get; set; }
    }

    public class EditUserViewModels
    {
        public int Id { get; set; }

        [Required]                      //Hace que el campo o textbox se obligatorio ingresarle datos
        [EmailAddress]                  //valida que el texto incluido se de tipo mail osea "@mail.com"
        [StringLength(100, ErrorMessage = "El {0} debe tener al menos {1} caracteres", MinimumLength = 1)]//restringe o permite solo cierta cantidad de caracteres "(100)"
        [Display(Name = "Correo electronico")]//ES el nombre que mostrara la vista
        public string Email { get; set; }

        //Se quita la etiqueta "[Required]" porque en la vista si no se escribe un nuevo "Password" se deja el ya existente en la base datos
        [DataType(DataType.Password)]   //De esta maneja cuando se envia el dato no es visible, como si estubiera encriptado
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        //realiza una comparacion entre dos campos dentro de los parantesis debe ir el nombre de la variable a comparar
        [Compare("Password", ErrorMessage = "Las contraseñas no son correctas")]              //asi se compara "Password" y "ConfirmPassword"
        public string ConfirmPassword { get; set; }

        [Required]
        public int Edad { get; set; }
    }
}