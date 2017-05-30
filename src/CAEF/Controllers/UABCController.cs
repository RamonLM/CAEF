using CAEF.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAEF.Controllers
{
    public class UABCController : Controller
    {
        private IUABCRepository _repositorioUABC;

        public UABCController(IUABCRepository repositorio)
        {
            _repositorioUABC = repositorio;
        }

        [Authorize]
        [HttpGet("UABC/Usuarios")]
        public IActionResult ObtenerUsuarios()
        {
            return Ok(_repositorioUABC.ObtenerUsuarios());
        }
    }
}
