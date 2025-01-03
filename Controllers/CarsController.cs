using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private static List<Car> Cars = new List<Car>
    {
        new Car { Id = 1, Brand = "Toyot1a", Model = "Corolla1", Year = 2011 },
        new Car { Id = 2, Brand = "Honda2", Model = "Civic2", Year = 2012 }
    };

    [HttpGet]
    public IActionResult GetCars() => Ok(Cars);

    [HttpPost]
    public IActionResult AddCar(Car car)
    {
        car.Id = Cars.Count + 1;
        Cars.Add(car);
        return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
    }
}
