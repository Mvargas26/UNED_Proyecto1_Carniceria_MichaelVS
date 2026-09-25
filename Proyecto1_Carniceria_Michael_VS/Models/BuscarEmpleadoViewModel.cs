using Modelos.Entidades;

namespace Proyecto1_Carniceria_Michael_VS.Models
{
    public class BuscarEmpleadoViewModel
    {
        public string? Identificacion { get; set; }
        public Empleado? EmpleadoEncontrado { get; set; }
        public bool SeRealizoBusqueda { get; set; }
    }
}
