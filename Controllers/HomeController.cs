using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RECIPE.DBContext;
using RECIPE.Models;
using System.Diagnostics;

namespace RECIPE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string Email, string Password)
        {
            var admin = _context.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (admin != null && admin.IsAdmin && !admin.IsBlocked)

            {
                HttpContext.Session.SetString("AdminId", admin.UserId.ToString());
                HttpContext.Session.SetString("AdminName", admin.Name);
                HttpContext.Session.SetString("UserRole", "Admin");

                return RedirectToAction("ViewRecipe");


            }

            ViewBag.ErrorMessage = "Invalid credentials";
            return View();
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
            {
                return Unauthorized("Access denied. Admins only.");
            }

            return View();
        }

        [HttpGet]
        public IActionResult RecipeList()
        {
            var recipes = _context.Recipes.ToList();
            return View(recipes);
        }


        public IActionResult UserList()
        {
            var users = _context.Users.Where(u => !u.IsBlocked).ToList();
            return View(users);
        }


        [HttpPost]
        public IActionResult BlockUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user != null)
            {
                user.IsBlocked = true;
                _context.SaveChanges();
            }
            return RedirectToAction("UserList");
        }

        [HttpPost]
        public IActionResult UnblockUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            if (user != null)
            {
                user.IsBlocked = false;
                _context.SaveChanges();
            }
            return RedirectToAction("UserList");
        }
        public IActionResult Index()
        {
            return View("ViewRecipe");
        }

        public IActionResult ViewRecipe()
        {
            var recipes = _context.Recipes.ToList();
            return View(recipes);
        }

        public IActionResult UserRecipeList(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                return NotFound("User not found.");

            var recipes = _context.Recipes
                .Where(r => r.Author == userId)
                .ToList();

            ViewBag.UserName = user.Name;
            return View(recipes);
        }



        public IActionResult RecipeDetail(int id)
        {
            var recipe = _context.Recipes
                .Include(r => r.User)
                .FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound("Recipe not found.");
            }

            return View(recipe);
        }



        public IActionResult MostViewed()
        {
            var recipes = _context.Recipes
                .OrderByDescending(r => r.LikeCount) // Sort by most viewed
                .ToList();

            return View(recipes);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("AdminLogin");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
