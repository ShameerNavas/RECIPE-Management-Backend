using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RECIPE.DBContext;
using RECIPE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IConfiguration _configuration;
    public UserController(MyDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }


    // POST: api/User/signup
    [HttpPost("signup")]
    public IActionResult SignUp(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok("User registered successfully!");
    }


    // POST: api/User/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        try
        {
            if (loginUser == null)
                return BadRequest(new { message = "Request body was empty or invalid" });

            // Debug line to confirm input
            Console.WriteLine($"Login attempt: {loginUser.Email}, {loginUser.Password}");

            var user = _context.Users
                .FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            if (user.IsBlocked)
                return Unauthorized(new { message = "Your account has been blocked. Please contact admin." });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new
            {
                message = "Login successful",
                token = jwtToken,
                user = new
                {
                    userId = user.UserId,
                    name = user.Name,
                    email = user.Email,
                    isAdmin = user.IsAdmin
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Login error: " + ex.Message);
            return BadRequest(new { message = "Server error: " + ex.Message });
        }
    }





    [HttpGet("Feed")]
    public IActionResult GetFeed()
    {
        var recipes = _context.Recipes
            .Select(r => new
            {
                r.RecipeId,
                r.Title,
                r.Image,
                Author = r.User.Name
            })
            .ToList();

        if (recipes == null || recipes.Count == 0)
        {
            return NotFound(new { message = "Data not found" });
        }

        return Ok(new
        {
            message = "Success",
            data = recipes
        });
    }
 


    [HttpPost("add-recipe")]
    public IActionResult AddRecipe([FromBody] Recipe recipe)
    {
        if (recipe == null)
            return BadRequest("Recipe data is required.");

        // Don't attach user object, just assign the Author (UserId)
        var userExists = _context.Users.Any(u => u.UserId == recipe.Author);
        if (!userExists)
            return BadRequest("Invalid User ID.");
          recipe.User = null; // Ensure we don't accidentally add a new User
        _context.Recipes.Add(recipe);  // Only add the Recipe, not the User
        _context.SaveChanges();

        return Ok(new { message = "Recipe added successfully!" });
    }


    [HttpGet("UserProfile/{userId}")]
    public IActionResult GetUserProfile(int userId)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }

    [HttpPut("UpdatePassword/{userId}")]
    public IActionResult UpdatePassword(int userId, [FromBody] UpdatePasswordDto updatedUser)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
            return NotFound("User not found");

        user.Password = updatedUser.Password;
        _context.SaveChanges();

        return Ok(new { message = "Password updated successfully!" });
    }



    [HttpGet("RecipeProfile/{userId}")]
    public IActionResult GetUserRecipes(int userId)
    {

        var userExists = _context.Users.Any(u => u.UserId == userId);
        if (!userExists)
            return BadRequest("Invalid User ID.");
        var recipes = _context.Recipes.Where(r => r.Author == userId).ToList();

        return Ok(new { message = "Success", data = recipes });
    }


    [HttpGet("RecipeDetail/{recipeId}")]
    public IActionResult GetRecipeDetail(int recipeId)
    {
        var recipe = _context.Recipes
            .FirstOrDefault(r => r.RecipeId == recipeId);

        if (recipe == null)
            return NotFound("Recipe not found.");

        return Ok(recipe);
    }


    [HttpPut("EditRecipe/{recipeId}")]
    public IActionResult EditRecipe(int recipeId, [FromBody] Recipe updatedRecipe)
    {
        var recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
        if (recipe == null)
            return NotFound("Recipe not found.");

        recipe.Title = updatedRecipe.Title;
        
        recipe.Ingredients = updatedRecipe.Ingredients;
        recipe.Steps = updatedRecipe.Steps;
       

        _context.SaveChanges();
        return Ok("Recipe updated successfully.");
    }


    [HttpDelete("DeleteRecipe/{recipeId}")]
    public IActionResult DeleteRecipe(int recipeId)
    {
        var recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
        if (recipe == null)
            return NotFound("Recipe not found.");

        _context.Recipes.Remove(recipe);
        _context.SaveChanges();

        return Ok("Recipe deleted successfully.");
    }

    
  

}