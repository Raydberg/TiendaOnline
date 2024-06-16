using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TiendaOnline.Models
{
    public class Modelo
    {
        [Key]
        public int IdModelo { get; set; }

        public string Nom_Modelo { get; set; }

        public int MarcaIDMARCA { get; set; }

        // Propiedad de navegación hacia la clase Marca
        public Marca Marca { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}
