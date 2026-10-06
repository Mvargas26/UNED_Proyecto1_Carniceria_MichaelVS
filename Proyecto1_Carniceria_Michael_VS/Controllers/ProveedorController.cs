using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos;
using Modelos.Entidades;
using Modelos.Enums;
using Modelos.Enums.Helpers;
using Proyecto1_Carniceria_Michael_VS.Models;
using Proyecto1_Carniceria_MichaelVS.Datos;
using System.Text.RegularExpressions;

namespace Proyecto1_Carniceria_Michael_VS.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly DatosEnMemoria _datos;

        public ProveedorController(DatosEnMemoria datos) //le inyectamos DatosEnMemoria al constru
        {
            _datos = datos;
        }

        public IActionResult ListarProveedores()
        {
            return View(_datos.Proveedores);
        }

        [HttpGet]
        public IActionResult RegistrarProveedor()
        {
            CargarListasDesplegables();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarProveedor(ProveedorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //validamos que cumpla los formatos de identificacion segun el tipo seleccionado
                bool formatoValido;
                switch (viewModel.TipoIdentificacion)
                {
                    case TipoIdentificacion.CedulaNacional:
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^\d-\d{4}-\d{4}$");
                        break;
                    case TipoIdentificacion.CedulaJuridica:
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^3-\d{3}-\d{6}$"); //segun req. siempre empieza con 3
                        break;
                    case TipoIdentificacion.Dimex:
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^\d{12}$");
                        break;
                    default:
                        formatoValido = false;
                        break;
                }

                if (!formatoValido)
                {
                    ModelState.AddModelError("Identificacion", "El formato de la identificación no es válido para el tipo seleccionado.");
                }
                else if (_datos.Proveedores.Any(p => p.Identificacion == viewModel.Identificacion))
                {
                    ModelState.AddModelError("Identificacion", "Ya existe un proveedor registrado con esta identificación.");
                }
                else if (!Regex.IsMatch(viewModel.Telefono, @"^\d{8}$"))
                {
                    ModelState.AddModelError("Telefono", "El teléfono debe contener exactamente 8 dígitos numéricos.");
                }
                else
                {
                    var proveedor = new Proveedor
                    {
                        TipoIdentificacion = viewModel.TipoIdentificacion,
                        Identificacion = viewModel.Identificacion,
                        RazonSocial = viewModel.RazonSocial,
                        NombreContacto = viewModel.NombreContacto,
                        Telefono = viewModel.Telefono,
                        CorreoElectronico = viewModel.CorreoElectronico,
                        TipoSuministro = viewModel.TipoSuministro,
                        FrecuenciaEntrega = viewModel.FrecuenciaEntrega
                    };

                    _datos.Proveedores.Add(proveedor);
                    return RedirectToAction("ListarProveedores");
                }
            }

            CargarListasDesplegables();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditarProveedor(Guid id)
        {
            var proveedor = _datos.Proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            var viewModel = new ProveedorViewModel
            {
                TipoIdentificacion = proveedor.TipoIdentificacion,
                Identificacion = proveedor.Identificacion,
                RazonSocial = proveedor.RazonSocial,
                NombreContacto = proveedor.NombreContacto,
                Telefono = proveedor.Telefono,
                CorreoElectronico = proveedor.CorreoElectronico,
                TipoSuministro = proveedor.TipoSuministro,
                FrecuenciaEntrega = proveedor.FrecuenciaEntrega
            };

            ViewBag.ProveedorId = proveedor.Id;
            CargarListasDesplegables();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditarProveedor(Guid id, ProveedorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var proveedor = _datos.Proveedores.FirstOrDefault(p => p.Id == id);
                if (proveedor == null)
                {
                    return NotFound();
                }

                //validamos que cumpla los formatos de identificacion segun el tipo seleccionado
                bool formatoValido;
                switch (viewModel.TipoIdentificacion)
                {
                    case TipoIdentificacion.CedulaNacional:
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^\d-\d{4}-\d{4}$");
                        break;
                    case TipoIdentificacion.CedulaJuridica:
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^3-\d{3}-\d{6}$");
                        break;
                    case TipoIdentificacion.Dimex:
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^\d{12}$");
                        break;
                    default:
                        formatoValido = false;
                        break;
                }

                if (!formatoValido)
                {
                    ModelState.AddModelError("Identificacion", "El formato de la identificación no es válido para el tipo seleccionado.");
                }
                else if (_datos.Proveedores.Any(p => p.Identificacion == viewModel.Identificacion && p.Id != id))
                {
                    ModelState.AddModelError("Identificacion", "Ya existe un proveedor registrado con esta identificación.");
                }
                else if (!Regex.IsMatch(viewModel.Telefono, @"^\d{8}$"))
                {
                    ModelState.AddModelError("Telefono", "El teléfono debe contener exactamente 8 dígitos numéricos.");
                }
                else
                {
                    proveedor.TipoIdentificacion = viewModel.TipoIdentificacion;
                    proveedor.Identificacion = viewModel.Identificacion;
                    proveedor.RazonSocial = viewModel.RazonSocial;
                    proveedor.NombreContacto = viewModel.NombreContacto;
                    proveedor.Telefono = viewModel.Telefono;
                    proveedor.CorreoElectronico = viewModel.CorreoElectronico;
                    proveedor.TipoSuministro = viewModel.TipoSuministro;
                    proveedor.FrecuenciaEntrega = viewModel.FrecuenciaEntrega;
                    return RedirectToAction("ListarProveedores");
                }
            }

            ViewBag.ProveedorId = id;
            CargarListasDesplegables();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EliminarProveedor(Guid id)
        {
            var proveedor = _datos.Proveedores.FirstOrDefault(p => p.Id == id);

            if (proveedor is null)
            {
                return NotFound();
            }

            _datos.Proveedores.Remove(proveedor);
            return RedirectToAction("ListarProveedores");
        }

        [HttpGet]
        public IActionResult BuscarProveedor()
        {
            return View(new BuscarProveedorViewModel());
        }

        [HttpPost]
        public IActionResult BuscarProveedor(BuscarProveedorViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.TextoBusqueda))
            {
                ModelState.AddModelError("TextoBusqueda", "Debe ingresar una identificación o razón social para buscar.");
                return View(viewModel);
            }

            viewModel.SeRealizoBusqueda = true;

            if (viewModel.CriterioBusqueda == "Identificacion")
            {
                viewModel.ProveedorEncontrado = _datos.Proveedores
                    .FirstOrDefault(p => p.Identificacion == viewModel.TextoBusqueda);
            }
            else
            {
                viewModel.ProveedorEncontrado = _datos.Proveedores
                    .FirstOrDefault(p => p.RazonSocial == viewModel.TextoBusqueda);
            }

            return View(viewModel);
        }


        #region Métodos internos
        // Llena los dropdowns de TipoIdentificacion, TipoSuministro y FrecuenciaEntrega
        private void CargarListasDesplegables()
        {
            var tiposIdentificacion = new List<TipoIdentificacion>
    {
        TipoIdentificacion.CedulaNacional,
        TipoIdentificacion.CedulaJuridica,
        TipoIdentificacion.Dimex
    };
            ViewBag.TiposIdentificacion = new SelectList(tiposIdentificacion);

            var suministros = Enum.GetValues(typeof(TipoSuministro)).Cast<TipoSuministro>()
                .Select(s => new { Valor = s, Texto = s.ObtenerNombreVisible() });
            ViewBag.TiposSuministro = new SelectList(suministros, "Valor", "Texto");

            var frecuencias = Enum.GetValues(typeof(FrecuenciaEntrega)).Cast<FrecuenciaEntrega>()
                .Select(f => new { Valor = f, Texto = f.ObtenerNombreVisible() });
            ViewBag.FrecuenciasEntrega = new SelectList(frecuencias, "Valor", "Texto");
        }

        #endregion





    }//fin class
}//fin space
