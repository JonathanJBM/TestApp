using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Test.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Clear();
        }
    }
}
