using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RECIPE.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "Author ID is required.")]
        [ForeignKey(nameof(User))]
        public int Author { get; set; }

        // Make it nullable so it’s optional in request
        public User? User { get; set; }


        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Recipe Title")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Please upload an image.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Ingredients are required.")]
        [Display(Name = "Ingredients")]
        [StringLength(500, ErrorMessage = "Ingredients cannot exceed 500 characters.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Steps are required.")]
        [Display(Name = "Preparation Steps")]
        [StringLength(1000, ErrorMessage = "Steps cannot exceed 1000 characters.")]
        public string Steps { get; set; }

        [Required(ErrorMessage = "Cooking time is required.")]
        [Display(Name = "Cooking Time (e.g., 30 mins, 1 hour)")]
        [StringLength(50, ErrorMessage = "Cooking time cannot exceed 50 characters.")]
        public string CookingTime { get; set; }

        [Required(ErrorMessage = "Difficulty level is required.")]
        [Display(Name = "Difficulty Level")]
        [StringLength(20, ErrorMessage = "Difficulty level cannot exceed 20 characters.")]
        public string DifficultyLevel { get; set; }

        [Display(Name = "Like Count")]
        public int LikeCount { get; set; }
    }
}
