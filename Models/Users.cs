using System.ComponentModel.DataAnnotations;

namespace RECIPE.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full Name")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        public bool IsBlocked { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Display(Name = "Admin User")]
        public bool IsAdmin { get; set; } = false;

    }
}