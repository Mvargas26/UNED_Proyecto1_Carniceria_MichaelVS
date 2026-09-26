using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.Entidades;
using Modelos.Enums;
using Modelos.Enums.Helpers;
using Proyecto1_Carniceria_Michael_VS.Models;
using Proyecto1_Carniceria_MichaelVS.Datos;

namespace Proyecto1_Carniceria_Michael_VS.Controllers
{
    public class ProductoController : Controller
    {
        private readonly DatosEnMemoria _datos;

        public ProductoController(DatosEnMemoria datos)
        {
            _datos = datos;
        }

        public IActionResult ListarProductos()
        {

            return View(_datos.Productos);
        }

        [HttpGet]
        public IActionResult RegistrarProducto()
        {
            CargarListasDesplegables();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProducto(ProductoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_datos.Productos.Any(p => p.CodigoProducto == viewModel.CodigoProducto))
                {
                    ModelState.AddModelError("CodigoProducto", "Ya existe un producto registrado con este código.");
                }
                else
                {
                    var producto = new Producto
                    {
                        CodigoProducto = viewModel.CodigoProducto,
                        NombreProducto = viewModel.NombreProducto,
                        Categoria = viewModel.Categoria,
                        UnidadMedida = viewModel.UnidadMedida,
                        PrecioPorUnidadMedida = viewModel.PrecioPorUnidadMedida,
                        ExistenciaActual = viewModel.ExistenciaActual,
                        ExistenciaMinima = viewModel.ExistenciaMinima
                    };

                    _datos.Productos.Add(producto);
                    return RedirectToAction("ListarProductos");
                }
            }

            CargarListasDesplegables();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditarProducto(Guid id)
        {
            var producto = _datos.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            var viewModel = new ProductoViewModel
            {
                CodigoProducto = producto.CodigoProducto,
                NombreProducto = producto.NombreProducto,
                Categoria = producto.Categoria,
                UnidadMedida = producto.UnidadMedida,
                PrecioPorUnidadMedida = producto.PrecioPorUnidadMedida,
                ExistenciaActual = producto.ExistenciaActual,
                ExistenciaMinima = producto.ExistenciaMinima
            };

            ViewBag.ProductoId = producto.Id;

            CargarListasDesplegables();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditarProducto(Guid id, ProductoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var producto = _datos.Productos.FirstOrDefault(p => p.Id == id);
                if (producto == null)
                {
                    return NotFound();
                }

                if (_datos.Productos.Any(p => p.CodigoProducto == viewModel.CodigoProducto && p.Id != id))
                {
                    ModelState.AddModelError("CodigoProducto", "Ya existe un producto registrado con este código.");
                }
                else
                {
                    producto.CodigoProducto = viewModel.CodigoProducto;
                    producto.NombreProducto = viewModel.NombreProducto;
                    producto.Categoria = viewModel.Categoria;
                    producto.UnidadMedida = viewModel.UnidadMedida;
                    producto.PrecioPorUnidadMedida = viewModel.PrecioPorUnidadMedida;
                    producto.ExistenciaActual = viewModel.ExistenciaActual;
                    producto.ExistenciaMinima = viewModel.ExistenciaMinima;
                    return RedirectToAction("ListarProductos");
                }
            }

            ViewBag.ProductoId = id;

            CargarListasDesplegables();

            return View(viewModel);
        }



        #region Metodos Internos
        // Llena los dropdowns de Categoria y UnidadMedida para el formulario
        private void CargarListasDesplegables()
        {
            var categorias = Enum.GetValues(typeof(Categoria)).Cast<Categoria>()
                .Select(c => new { Valor = c, Texto = c.ObtenerNombreVisible() });
            ViewBag.Categorias = new SelectList(categorias, "Valor", "Texto");

            var unidades = Enum.GetValues(typeof(UnidadMedida)).Cast<UnidadMedida>()
                .Select(u => new { Valor = u, Texto = u.ObtenerNombreVisible() });
            ViewBag.UnidadesMedida = new SelectList(unidades, "Valor", "Texto");
        }



        #endregion

    }//fin class
}//fn space
