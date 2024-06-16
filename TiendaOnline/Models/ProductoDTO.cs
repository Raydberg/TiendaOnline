using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.Models
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Precio { get; set; }

        [DataType(DataType.Date)]
        public DateTime año { get; set; }
        public string Color { get; set; }

        // Cambia el nombre de la propiedad a ModeloIDMODELO
        public int ModeloIDMODELO { get; set; }
        // Propiedad de navegación hacia la clase Modelo
        public Modelo? Modelo { get; set; }
        public IFormFile? ArchivoImagen { get; set; }
        public int MarcaID { get; set; } // Asume que cada vehículo pertenece a una marca
    }
}
