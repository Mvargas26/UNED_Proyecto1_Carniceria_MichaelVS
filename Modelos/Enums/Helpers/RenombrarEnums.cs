using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Enums.Helpers
{
    //Esta clase permite mostrar los nombres de los enum de manera más amigable para el usuario final.
    public static class RenombrarEnums
    {
        public static string ObtenerNombreVisible(this AreaTrabajo area)
        {
            switch (area)
            {
                case AreaTrabajo.Administracion:
                    return "Administración";
                case AreaTrabajo.VentasYCaja:
                    return "Ventas y Caja";
                case AreaTrabajo.CorteYPreparacion:
                    return "Corte y Preparación";
                case AreaTrabajo.BodegaEInventario:
                    return "Bodega e Inventario";
                case AreaTrabajo.Distribucion:
                    return "Distribución";
                case AreaTrabajo.Limpieza:
                    return "Limpieza";
                default:
                    return area.ToString();
            }
        }

        public static string ObtenerNombreVisible(this Puesto puesto)
        {
            switch (puesto)
            {
                case Puesto.Administrador:
                    return "Administrador";
                case Puesto.AsistenteAdministrativo:
                    return "Asistente Administrativo";
                case Puesto.Cajero:
                    return "Cajero";
                case Puesto.Vendedor:
                    return "Vendedor";
                case Puesto.ServicioAlCliente:
                    return "Servicio al Cliente";
                case Puesto.Carnicero:
                    return "Carnicero";
                case Puesto.Despostador:
                    return "Despostador";
                case Puesto.Empacador:
                    return "Empacador";
                case Puesto.Bodeguero:
                    return "Bodeguero";
                case Puesto.AuxiliarDeInventario:
                    return "Auxiliar de Inventario";
                case Puesto.Conductor:
                    return "Conductor";
                case Puesto.AyudanteDeDistribucion:
                    return "Ayudante de Distribución";
                case Puesto.PersonalDeLimpieza:
                    return "Personal de Limpieza";
                default:
                    return puesto.ToString();
            }
        }

        public static string ObtenerNombreVisible(this Turno turno)
        {
            switch (turno)
            {
                case Turno.Manana:
                    return "Mañana";
                case Turno.Tarde:
                    return "Tarde";
                case Turno.Mixto:
                    return "Mixto";
                default:
                    return turno.ToString();
            }
        }
    
    
    }//fn class
}//fn space
