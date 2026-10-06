using Modelos.Entidades;

namespace Proyecto1_Carniceria_Michael_VS.Models
{
    public class BuscarProveedorViewModel
    {
        public string? TextoBusqueda { get; set; }
        public string CriterioBusqueda { get; set; } = "Identificacion"; // "Identificacion" o "RazonSocial"
        public Proveedor? ProveedorEncontrado { get; set; }
        public bool SeRealizoBusqueda { get; set; }
    }//fn class
}//fn space
