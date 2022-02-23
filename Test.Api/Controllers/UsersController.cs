using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Test.Business;
using Test.Models;
using Test.Models.Communication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Api.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users/GetAll
        [HttpGet]
        public async Task<ActionResult<Response>> GetAll()
        {
            try
            {
                var response = UsersService.GetAllUsers();
                return response;
            }
            catch (System.Exception ex)
            {
                return new Response() { IsSuccess = false, Status = Models.Communication.StatusCode.ERROR, Message = ex.Message, Content = ex };
            }
        }

        // GET: api/Users/Get
        [HttpGet]
        public async Task<ActionResult<Response>> Get([FromQuery]int id)
        {
            try
            {
                var response = UsersService.GetUser(id);
                return response;
            }
            catch (System.Exception ex)
            {
                return new Response() { IsSuccess = false, Status = Models.Communication.StatusCode.ERROR, Message = ex.Message, Content = ex };
            }
        }

        // POST: api/Users/Create
        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] User user)
        {
            try
            {
                var response = UsersService.InsertUser(user);
                return response;
            }
            catch (System.Exception ex)
            {
                return new Response() { IsSuccess = false, Status = Models.Communication.StatusCode.ERROR, Message = ex.Message, Content = ex };
            }
        }

        // PUT: api/Users/Edit
        [HttpPut]
        public async Task<ActionResult<Response>> Edit([FromBody] User user)
        {
            try
            {
                var response = UsersService.UpdateUser(user);
                return response;
            }
            catch (System.Exception ex)
            {
                return new Response() { IsSuccess = false, Status = Models.Communication.StatusCode.ERROR, Message = ex.Message, Content = ex };
            }
        }

        // DELETE: api/Users/Delete
        [HttpDelete]
        public async Task<ActionResult<Response>> Delete([FromQuery] int id)
        {
            try
            {
                var response = UsersService.DeleteUser(id);
                return response;
            }
            catch (System.Exception ex)
            {
                return new Response() { IsSuccess = false, Status = Models.Communication.StatusCode.ERROR, Message = ex.Message, Content = ex };
            }
        }

    }
}
