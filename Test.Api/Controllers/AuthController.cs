using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using Test.Business.Services;
using Test.Models;
using Test.Models.Communication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // POST: api/Users/Create
        [HttpPost]
        public async Task<ActionResult<Response>> Login([FromBody] Credential credential)
        {
            try
            {
                var response = AuthService.Login(credential);
                if (response.IsSuccess)
                {
                    HttpContext.Session.SetString("User",JsonSerializer.Serialize(response.Content));
                }
                return response;
            }
            catch (System.Exception ex)
            {
                return new Response() { IsSuccess = false, Status = Models.Communication.StatusCode.ERROR, Message = ex.Message, Content = ex };
            }
        }
    }
}
