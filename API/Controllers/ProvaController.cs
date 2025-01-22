using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // https://localhost:portnumber/api/prova = prende il nome della classe preceduta da Controller
    [Route("api/[controller]")]
    [ApiController]
    public class ProvaController : ControllerBase
    {



        // IActionResult rappresenta il risultato di azione del metodo
        [HttpGet(Name = "GetProve")]
        public IActionResult GetAllStudenti() {

            string[] studenti = new string[] { "Matteo", "Giovanni", "Andrea", "Filippo" };
            return Ok(studenti);
        }
    }
}
