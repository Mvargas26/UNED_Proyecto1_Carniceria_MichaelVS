using Microsoft.AspNetCore.Mvc;
using Modelos.Entidades;
using Proyecto1_Carniceria_Michael_VS.Models;
using Proyecto1_Carniceria_MichaelVS.Datos;

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
            return View();
        }//fn registrar


        [HttpPost]
        public IActionResult Registrar(ClienteViewModel viewModel)
        {
            if (ModelState.IsValid)
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

            return View(viewModel);
        }//fn registrar post 


    }//fn controller
}//fn space
