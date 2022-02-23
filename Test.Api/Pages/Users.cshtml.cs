using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Test.Pages
{
    public class UsersModel : PageModel
    {
        private readonly ILogger<UsersModel> _logger;

        public UsersModel(ILogger<UsersModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                Response.Redirect("/Login");
            }
        }
    }
}
