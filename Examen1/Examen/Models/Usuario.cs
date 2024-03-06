using System.ComponentModel.DataAnnotations;
namespace Examen.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string Contraseña { get; set;}
    }
}
