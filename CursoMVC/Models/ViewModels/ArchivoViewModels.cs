
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CursoMVC.Models.ViewModels
{
    public class ArchivoViewModels
    {
        //"HttpPostedFileBase" tipo de dato para poder cargar a subir a el proyecto archivos ya sea cadenas texto, imagenes, documentos

        [Required]                      //Hace que el campo o textbox se obligatorio ingresarle datos
        [DisplayName("Mi archivo")]     //ES el nombre que mostrara la vista
        public HttpPostedFileBase Archivo1 { get; set; }

        [Required]
        [DisplayName("Mi archivo2")]
        public HttpPostedFileBase Archivo2 { get; set; }

        [Required]
        [DisplayName("Mi Cadena")]
        public string Cadena { get; set; }
    }
}