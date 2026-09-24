using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos;
using Modelos.Entidades;
using Proyecto1_Carniceria_Michael_VS.Models;
using Proyecto1_Carniceria_MichaelVS.Datos;
using System.Text.RegularExpressions;

namespace Proyecto1_Carniceria_Michael_VS.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DatosEnMemoria _datos;

        public ClienteController(DatosEnMemoria datos) //le inyectamos DatosEnMemoria al constru
        {
            _datos = datos;
        }

        public IActionResult ListarClientes()
        {
            return View(_datos.Clientes);
        }//fn index

       public IActionResult Registrar()
        {
            CargarTiposIdentificacion();
            return View();
        }//fn registrar


        [HttpPost]
        public IActionResult Registrar(ClienteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //validamos que cumpla los formatos de identificacion segun el tipo seleccionado
                bool formatoValido;

                switch (viewModel.TipoIdentificacion)
                {
                    case TipoIdentificacion.CedulaNacional:
                        //se lee: "un dígito, guion, 4 dígitos, guion, 4 dígitos
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^\d-\d{4}-\d{4}$");
                        break;

                    case TipoIdentificacion.Dimex:
                        //se lee: "12 dígitos"
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^\d{12}$");
                        break;

                    case TipoIdentificacion.Pasaporte:
                        //se lee: "1 a 50 caracteres alfanuméricos"
                        formatoValido = Regex.IsMatch(viewModel.Identificacion, @"^[a-zA-Z0-9]{1,50}$");
                        break;

                    default:
                        //si no cumple, cae aqui en el default
                        formatoValido = false;
                        break;
                }

                //verificamos si el formato es válido, si no lo es, agregamos un error al ModelState
                //para cambiar el ModelState.IsValid
                if (!formatoValido)
                {
                    ModelState.AddModelError("Identificacion", "El formato de la identificación no es válido para el tipo seleccionado.");
                }
                //validamos que no exista un cliente con la misma identificacion
                else if (_datos.Clientes.Any(c => c.Identificacion == viewModel.Identificacion))
                {
                    ModelState.AddModelError("Identificacion", "Ya existe un cliente registrado con esta identificación.");
                }
                else
                {
                    var cliente = new Cliente
                    {
                        TipoIdentificacion = viewModel.TipoIdentificacion,
                        Identificacion = viewModel.Identificacion,
                        Nombre = viewModel.Nombre,
                        PrimerApellido = viewModel.PrimerApellido,
                        SegundoApellido = viewModel.SegundoApellido,
                        FechaNacimiento = viewModel.FechaNacimiento
                    };

                    _datos.Clientes.Add(cliente);
                    return RedirectToAction("ListarClientes");
                }
            }

            CargarTiposIdentificacion(); //lo pasamos de nuevo para que se cargue de nuevo en caso de fallo
            return View(viewModel);
        }//fn registrar post 

        #region Metodos Privados

        //filtramos los tipos de identificacion validos para clientes (CedulaJuridica no)
        private void CargarTiposIdentificacion()
        {
            var opciones = new List<TipoIdentificacion>
    {
        TipoIdentificacion.CedulaNacional,
        TipoIdentificacion.Dimex,
        TipoIdentificacion.Pasaporte
    };

            ViewBag.TiposIdentificacion = new SelectList(opciones);
        }
        #endregion
    }//fn controller
}//fn space
