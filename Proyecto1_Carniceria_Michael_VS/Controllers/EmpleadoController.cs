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
    public class EmpleadoController : Controller
    {
        private readonly DatosEnMemoria _datos;

        public EmpleadoController(DatosEnMemoria datos)
        {
            _datos = datos;
        }

        public IActionResult ListarEmpleados()
        {
            return View(_datos.Empleados);
        }

        public IActionResult RegistrarEmpleado()
        {
            CargarListasDesplegables();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarEmpleado(EmpleadoViewModel viewModel)
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
                else if (_datos.Empleados.Any(e => e.Identificacion == viewModel.Identificacion))
                {
                    ModelState.AddModelError("Identificacion", "Ya existe un empleado registrado con esta identificación.");
                }
                else if (viewModel.FechaNacimiento > DateTime.Now)
                {
                    ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento no puede ser una fecha futura.");
                }
                else if (!PuestoPerteneceAArea(viewModel.AreaTrabajo, viewModel.Puesto))
                {
                    ModelState.AddModelError("Puesto", "El puesto seleccionado no corresponde al área de trabajo elegida.");
                }
                else
                {
                    var empleado = new Empleado
                    {
                        TipoIdentificacion = viewModel.TipoIdentificacion,
                        Identificacion = viewModel.Identificacion,
                        Nombre = viewModel.Nombre,
                        PrimerApellido = viewModel.PrimerApellido,
                        SegundoApellido = viewModel.SegundoApellido,
                        FechaNacimiento = viewModel.FechaNacimiento,
                        SalarioMensual = viewModel.SalarioMensual,
                        FechaIngreso = viewModel.FechaIngreso,
                        AreaTrabajo = viewModel.AreaTrabajo,
                        Puesto = viewModel.Puesto,
                        Turno = viewModel.Turno
                    };

                    _datos.Empleados.Add(empleado);
                    return RedirectToAction("ListarEmpleados");
                }
            }

            CargarListasDesplegables();
            return View(viewModel);
        }

        public IActionResult EditarEmpleado (Guid id)
        {
            var empleado = _datos.Empleados.FirstOrDefault(e => e.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }
            var viewModel = new EmpleadoViewModel
            {
                TipoIdentificacion = empleado.TipoIdentificacion,
                Identificacion = empleado.Identificacion,
                Nombre = empleado.Nombre,
                PrimerApellido = empleado.PrimerApellido,
                SegundoApellido = empleado.SegundoApellido,
                FechaNacimiento = empleado.FechaNacimiento,
                SalarioMensual = empleado.SalarioMensual,
                FechaIngreso = empleado.FechaIngreso,
                AreaTrabajo = empleado.AreaTrabajo,
                Puesto = empleado.Puesto,
                Turno = empleado.Turno
            };

            ViewBag.EmpleadoId = empleado.Id;
            CargarListasDesplegables();

            return View(viewModel);
        }//fn Get EditarEmpleado

        [HttpPost]
        public IActionResult EditarEmpleado(Guid id, EmpleadoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var empleado = _datos.Empleados.FirstOrDefault(e => e.Id == id);
                if (empleado == null)
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
                else if (_datos.Empleados.Any(e => e.Identificacion == viewModel.Identificacion && e.Id != id))
                {
                    ModelState.AddModelError("Identificacion", "Ya existe un empleado registrado con esta identificación.");
                }
                else if (viewModel.FechaNacimiento > DateTime.Now)
                {
                    ModelState.AddModelError("FechaNacimiento", "La fecha de nacimiento no puede ser una fecha futura.");
                }
                else if (!PuestoPerteneceAArea(viewModel.AreaTrabajo, viewModel.Puesto))
                {
                    ModelState.AddModelError("Puesto", "El puesto seleccionado no corresponde al área de trabajo elegida.");
                }
                else
                {
                    empleado.TipoIdentificacion = viewModel.TipoIdentificacion;
                    empleado.Identificacion = viewModel.Identificacion;
                    empleado.Nombre = viewModel.Nombre;
                    empleado.PrimerApellido = viewModel.PrimerApellido;
                    empleado.SegundoApellido = viewModel.SegundoApellido;
                    empleado.FechaNacimiento = viewModel.FechaNacimiento;
                    empleado.SalarioMensual = viewModel.SalarioMensual;
                    empleado.FechaIngreso = viewModel.FechaIngreso;
                    empleado.AreaTrabajo = viewModel.AreaTrabajo;
                    empleado.Puesto = viewModel.Puesto;
                    empleado.Turno = viewModel.Turno;
                    return RedirectToAction("ListarEmpleados");
                }
            }

            ViewBag.EmpleadoId = id;
            CargarListasDesplegables();
            return View(viewModel);
        }// fn Post EditarEmpleado

        [HttpPost]
        public IActionResult EliminarEmpleado(Guid id)
        {
            var empleado = _datos.Empleados.FirstOrDefault(e => e.Id == id);

            if (empleado is null)
            {
                return NotFound();
            }

            _datos.Empleados.Remove(empleado);
            return RedirectToAction("ListarEmpleados");
        }//fn EliminarEmpleado



        #region Metodos Internos

        // Valida que el puesto pertenezca al área de trabajo (según Orient Academica)
        private bool PuestoPerteneceAArea(AreaTrabajo area, Puesto puesto)
        {
            return area switch
            {
                AreaTrabajo.Administracion => puesto is Puesto.Administrador or Puesto.AsistenteAdministrativo,
                AreaTrabajo.VentasYCaja => puesto is Puesto.Cajero or Puesto.Vendedor or Puesto.ServicioAlCliente,
                AreaTrabajo.CorteYPreparacion => puesto is Puesto.Carnicero or Puesto.Despostador or Puesto.Empacador,
                AreaTrabajo.BodegaEInventario => puesto is Puesto.Bodeguero or Puesto.AuxiliarDeInventario,
                AreaTrabajo.Distribucion => puesto is Puesto.Conductor or Puesto.AyudanteDeDistribucion,
                AreaTrabajo.Limpieza => puesto is Puesto.PersonalDeLimpieza,
                _ => false
            };
        }


        // Llena los dropdowns de TipoIdentificacion, AreaTrabajo y Turno para el formulario con nombres bien escritos
        private void CargarListasDesplegables()
        {
            //filtramos solo los tipos que aplican a Empleado (CedulaNacional y Dimex)
            var tiposIdentificacion = new List<TipoIdentificacion>
            {
                TipoIdentificacion.CedulaNacional,
                TipoIdentificacion.Dimex
            };

            ViewBag.TiposIdentificacion = new SelectList(tiposIdentificacion);

            //armamos pares (valor real, texto legible) usando el helper para mostrar tildes correctamente
            var areas = Enum.GetValues(typeof(AreaTrabajo)).Cast<AreaTrabajo>()
                .Select(a => new { Valor = a, Texto = a.ObtenerNombreVisible() });
            ViewBag.AreasTrabajo = new SelectList(areas, "Valor", "Texto");

            var puestos = Enum.GetValues(typeof(Puesto)).Cast<Puesto>()
                .Select(p => new { Valor = p, Texto = p.ObtenerNombreVisible() });
            ViewBag.Puestos = new SelectList(puestos, "Valor", "Texto");

            var turnos = Enum.GetValues(typeof(Turno)).Cast<Turno>()
                .Select(t => new { Valor = t, Texto = t.ObtenerNombreVisible() });
            ViewBag.Turnos = new SelectList(turnos, "Valor", "Texto");

            //Pasamos el mapeo Área/Puestos al JS para que pueda filtrar los puestos según el área seleccionada
            ViewBag.MapeoAreaPuesto = System.Text.Json.JsonSerializer.Serialize(ObtenerMapeoAreaPuesto());
        }

        // Armamos el mapeo Área/Puestos y luego lo pasamos al JS
        private Dictionary<string, List<string>> ObtenerMapeoAreaPuesto()
        {
            var mapeo = new Dictionary<string, List<string>>();

            foreach (AreaTrabajo area in Enum.GetValues(typeof(AreaTrabajo)))
            {
                var puestosDelArea = Enum.GetValues(typeof(Puesto))
                    .Cast<Puesto>()
                    .Where(p => PuestoPerteneceAArea(area, p))
                    .Select(p => p.ToString())
                    .ToList();

                mapeo[area.ToString()] = puestosDelArea;
            }

            return mapeo;
        }


        #endregion


    }//fin class
}//fn spcae
