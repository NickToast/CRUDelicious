#pragma warning disable CS8618

//Allows you use a feature of C# to do validations
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Name must be longer than 2 characters")]
    [MaxLength(45, ErrorMessage = "Name cannot be longer than 45 characters")]
    public string Name { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Chef's name must be longer than 2 characters")]
    [MaxLength(45, ErrorMessage = "Chef's name cannot be longer than 45 characters")]
    public string Chef { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "Description must be longer than 5 characters")]
    public string Description { get; set; }

    [Required]
    [Range(1, Int32.MaxValue, ErrorMessage = "Calories must be greater than 0")]
    public int Calories { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Tastiness must be between 1 and 5")]
    public int Tastiness { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}