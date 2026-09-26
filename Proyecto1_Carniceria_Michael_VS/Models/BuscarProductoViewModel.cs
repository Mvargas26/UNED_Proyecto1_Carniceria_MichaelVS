using Modelos.Entidades;

namespace Proyecto1_Carniceria_Michael_VS.Models
{
    public class BuscarProductoViewModel
    {
        public string? TextoBusqueda { get; set; }
        public string CriterioBusqueda { get; set; } = "Codigo"; // "Codigo" o "Nombre"
        public Producto? ProductoEncontrado { get; set; }
        public bool SeRealizoBusqueda { get; set; }
    
    
    }//fin class
    
}//fin class
