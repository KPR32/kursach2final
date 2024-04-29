using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;
using FurnitureStore3.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;


namespace FurnitureStore3.Controllers
{
    public class UserController : Controller
    {

        
        private readonly IUserService userService;
        private const int adminRoleId = 2;
        private const int clientRoleId = 1;

        public UserController(IUserService userService)
        {
            this.userService = userService;            
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Products");
                // redirect на книги
            }

            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            User? user = await userService.GetUserAsync(loginViewModel.Username, loginViewModel.Password);
            if (user is not null)
            {
                await SignIn(user);
                
                return RedirectToAction("Index", "Products"); // redirect на
            }
            else
            {
                
                ModelState.AddModelError("user_null", "Пользователь не найден");
                return View(loginViewModel);
            }
        }
        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        public IActionResult RegistrationSuccess()
        {
          
            return View();
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registration)
        {
            if (!ModelState.IsValid)
            {
               
                return View(registration);
            }

            if (await userService.IsUserExistsAsync(registration.Username))
            {
                
                ModelState.AddModelError("user_exists", $"Имя пользователя {registration.Username} уже существует!");
                return View(registration);
            }

            try
            {
                await userService.RegistrationAsync(registration.Fullname, registration.Username, registration.Phone,registration.Password);
                
                return RedirectToAction("RegistrationSuccess", "User");
            }
            catch
            {
           
                
                return View(registration);
            }
        }

        private async Task SignIn(User user)
        {
            string role = user.RoleId switch
            {
                adminRoleId => "admin",
                clientRoleId => "client",
                _ => throw new ApplicationException("invalid user role")
            };

            List<Claim> claims = new List<Claim>
    {
        new Claim("fullname", user.Fullname),
        new Claim("id", user.Id.ToString()),
        new Claim("role", role),
        new Claim("username", user.Login)
    };
            string authType = CookieAuthenticationDefaults.AuthenticationScheme;
            IIdentity identity = new ClaimsIdentity(claims, authType, "username", "role");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}