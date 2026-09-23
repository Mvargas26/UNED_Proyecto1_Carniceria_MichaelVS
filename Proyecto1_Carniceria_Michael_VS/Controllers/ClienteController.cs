using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View(_datos.Clientes);
        }//fn index




    }//fn controller
}//fn space
