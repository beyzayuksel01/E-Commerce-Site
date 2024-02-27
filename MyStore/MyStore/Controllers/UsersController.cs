using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.ViewModels;
using MyStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace MyStore.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //[Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName, Email = model.Email, FullName = model.FullName };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();

                return View(new EditViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    SelectedRoles = await _userManager.GetRolesAsync(user)
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.FullName = model.FullName;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }

                    if (result.Succeeded)
                    {
                        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                        if (model.SelectedRoles != null)
                        {
                            await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                        }
                        return RedirectToAction("Index");
                    }

                    foreach (IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
            }

            return View(model);
        }

       


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }

    }

}





















//using System.Security.Claims;
//using MyStore.Repositories.Contract;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;
//using MyStore.Models.Entity;
//using MyStore.Models;

//namespace MyStore.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly IUserRepository _userRepository;

//        public UsersController(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public IActionResult Login()
//        {
//            if (User.Identity!.IsAuthenticated)
//            {
//                return RedirectToAction("Index");
//            }
//            return View();
//        }

//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName || x.Email == model.Email);
//                if (user == null)
//                {
//                    _userRepository.CreateUser(new User
//                    {
//                        UserName = model.UserName,
//                        Name = model.Name,
//                        Email = model.Email,
//                        Password = model.Password,
//                        Image = "User1.jpg"
//                    });
//                    return RedirectToAction("Login");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Username ya da Email kullanımda.");
//                }
//            }
//            return View(model);
//        }

//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("Login");
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var isUser = await _userRepository.Users.FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

//                if (isUser != null)
//                {
//                    var userClaims = new[]
//                    {
//                        new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()),
//                        new Claim(ClaimTypes.Name, isUser.UserName ?? ""),
//                        new Claim(ClaimTypes.GivenName, isUser.Name ?? ""),
//                        new Claim(ClaimTypes.UserData, isUser.Image ?? "")
//                    };

//                    if (isUser.Email == "admin@example.com")
//                    {
//                        userClaims.Append(new Claim(ClaimTypes.Role, "admin"));
//                    }

//                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

//                    var authProperties = new AuthenticationProperties
//                    {
//                        IsPersistent = true
//                    };

//                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

//                    await HttpContext.SignInAsync(
//                        CookieAuthenticationDefaults.AuthenticationScheme,
//                        new ClaimsPrincipal(claimsIdentity),
//                        authProperties);

//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
//                }
//            }

//            return View(model);
//        }

//        public IActionResult Profile(string username)
//        {
//            if (string.IsNullOrEmpty(username))
//            {
//                return NotFound();
//            }

//            var user = _userRepository
//            .Users
//            .Include(x => x.Comments)
//            .ThenInclude(x => x.Product)
//            .FirstOrDefault(x => x.UserName == username);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }
//    }
//}
