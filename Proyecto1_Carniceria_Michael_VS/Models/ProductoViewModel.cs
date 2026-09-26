using Modelos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Proyecto1_Carniceria_Michael_VS.Models
{
    public class ProductoViewModel
    {
        [Required(ErrorMessage = "El código del producto es obligatorio.")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "El código debe tener entre 1 y 20 caracteres.")]
        public string CodigoProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres.")]
        public string NombreProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una unidad de medida.")]
        public UnidadMedida UnidadMedida { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal PrecioPorUnidadMedida { get; set; }

        [Required(ErrorMessage = "La existencia actual es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La existencia actual debe ser igual o mayor que cero.")]
        public decimal ExistenciaActual { get; set; }

        [Required(ErrorMessage = "La existencia mínima es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La existencia mínima debe ser igual o mayor que cero.")]
        public decimal ExistenciaMinima { get; set; }

    }//fn class
}//fn space
