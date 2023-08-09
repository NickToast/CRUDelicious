using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace EFLecture.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;

    private MyContext db;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> allDishes = db.Dishes.ToList();
        return View("Dashboard", allDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("New");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            return View("New");
        }
        db.Dishes.Add(newDish);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("dishes/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("ViewDish", dish);
    }

    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult Edit(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("Edit", dish);
    }

    [HttpPost("dishes/{dishId}/update")]
    public IActionResult Update(Dish editDish, int dishId)
    {
        if (!ModelState.IsValid)
        {
            return Edit(dishId);
        }
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("Index");
        }
        dish.Name = editDish.Name;
        dish.Chef = editDish.Chef;
        dish.Description = editDish.Description;
        dish.Calories = editDish.Calories;
        dish.Tastiness = editDish.Tastiness;
        db.Dishes.Update(dish);
        db.SaveChanges();
        return RedirectToAction("ViewDish", new {dishId = dishId});
    }

    [HttpPost("dishes/{dishId}")]
    public IActionResult Delete(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        db.Dishes.Remove(dish);
        db.SaveChanges();
        return RedirectToAction("Index");
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
