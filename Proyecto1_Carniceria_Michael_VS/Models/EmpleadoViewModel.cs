namespace Proyecto1_Carniceria_Michael_VS.Models
{
    using Modelos;
    using Modelos.Enums;
    using System.ComponentModel.DataAnnotations;
        public class EmpleadoViewModel
        {
            [Required(ErrorMessage = "Debe seleccionar un tipo de identificación.")]
            public TipoIdentificacion TipoIdentificacion { get; set; }

            [Required(ErrorMessage = "La identificación es obligatoria.")]
            public string Identificacion { get; set; } = string.Empty;

            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "El primer apellido es obligatorio.")]
            [StringLength(75, MinimumLength = 3, ErrorMessage = "El primer apellido debe tener entre 3 y 75 caracteres.")]
            public string PrimerApellido { get; set; } = string.Empty;

            [Required(ErrorMessage = "El segundo apellido es obligatorio.")]
            [StringLength(75, MinimumLength = 3, ErrorMessage = "El segundo apellido debe tener entre 3 y 75 caracteres.")]
            public string SegundoApellido { get; set; } = string.Empty;

            [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
            [DataType(DataType.Date)]
            public DateTime FechaNacimiento { get; set; }

            [Required(ErrorMessage = "El salario mensual es obligatorio.")]
            [Range(0, 5000000, ErrorMessage = "El salario debe estar entre ₡0 y ₡5,000,000.")]
            public decimal SalarioMensual { get; set; }

            [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
            [DataType(DataType.Date)]
            public DateTime FechaIngreso { get; set; }

            [Required(ErrorMessage = "Debe seleccionar un área de trabajo.")]
            public AreaTrabajo AreaTrabajo { get; set; }

            [Required(ErrorMessage = "Debe seleccionar un puesto.")]
            public Puesto Puesto { get; set; }

            [Required(ErrorMessage = "Debe seleccionar un turno.")]
            public Turno Turno { get; set; }
        }// fn class
}//fn space
