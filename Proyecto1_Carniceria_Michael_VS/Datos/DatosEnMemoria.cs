using Modelos.Entidades;

namespace Proyecto1_Carniceria_MichaelVS.Datos
{
    public class DatosEnMemoria //aqui funciona como el almacenamiento solo q en memoria.
    {
        public List<Producto> Productos { get; set; } = new List<Producto>();
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<Empleado> Empleados { get; set; } = new List<Empleado>();
        public List<Proveedor> Proveedores { get; set; } = new List<Proveedor>();

    }//fn class
}//fn space
