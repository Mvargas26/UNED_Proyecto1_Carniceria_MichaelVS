using Modelos.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Entidades
{
    public class Empleado : Persona
    {
        public decimal SalarioMensual { get; set; }
        public DateTime FechaIngreso { get; set; }
        public AreaTrabajo AreaTrabajo { get; set; }
        public Puesto Puesto { get; set; }
        public Turno Turno { get; set; }


    }//fin class

}//fn space
