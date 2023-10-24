using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp_UnderTheHood.Models;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public CredentialModel Credential { get; set; }
       
        public void OnGet()
        {
           
        }
       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Credential.Username == "admin" && Credential.Password == "password") // Verify the credential
            {
                //Creating the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Credential.Username),
                    new Claim("role","HR"),
                    new Claim("Admin","true"),  
                    new Claim("Manager","true"),       
                    new Claim("EmploymentDate","2023-7-01")

                };
                
                var identity = new ClaimsIdentity(claims, "CookieAuth");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe
                };

                await HttpContext.SignInAsync("CookieAuth", claimsPrincipal, authProperties);


                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
