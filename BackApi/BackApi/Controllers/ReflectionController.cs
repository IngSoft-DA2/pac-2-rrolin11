using BackApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReflectionController(IReflectionService reflectionService) : ControllerBase
    {

        [HttpGet("importers")]
        public IActionResult GetImporters()
        {
            string[] dllNames = reflectionService.GetImporterInterfaceImplementorDllsNames("reflection");
            return Ok(dllNames.ToArray());
        }
    }
}
