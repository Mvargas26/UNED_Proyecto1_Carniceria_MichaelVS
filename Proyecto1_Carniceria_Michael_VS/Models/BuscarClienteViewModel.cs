using Modelos.Entidades;

namespace Proyecto1_Carniceria_Michael_VS.Models
{
    public class BuscarClienteViewModel
    {
        public string? Identificacion { get; set; }
        public Cliente? ClienteEncontrado { get; set; }
        public bool SeRealizoBusqueda { get; set; }
    }//fn class
}//fn namespace
