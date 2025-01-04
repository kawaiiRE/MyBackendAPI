using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{

    // private static List<Car> Cars = new List<Car>
    // {
    //     new Car { Id = 1, Brand = "Toyot1a", Model = "Corolla1", Year = 2011 },
    //     new Car { Id = 2, Brand = "Honda2", Model = "Civic2", Year = 2012 }
    // };

    // [HttpGet]
    // public IActionResult GetCars() => Ok(Cars);

    // [HttpPost]
    // public IActionResult AddCar(Car car)
    // {
    //     car.Id = Cars.Count + 1;
    //     Cars.Add(car);
    //     return CreatedAtAction(nameof(GetCars), new { id = car.Id }, car);
    // }

    private readonly ICarService _carService;
    private readonly ILogger<CarsController> _logger;

    public CarsController(ICarService carService, ILogger<CarsController> logger)
    {
        _carService = carService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars()
    {
        _logger.LogInformation("getting cars");

        var cars = await _carService.GetCarsAsync();
        return Ok(cars);
    }

    [HttpPost]
    public async Task<ActionResult<Car>> AddCar([FromBody] Car car)
    {
        _logger.LogInformation("Adding a new car: {Brand} {Model}, {Year}", car.Brand, car.Model, car.Year); // Log the incoming car details

        var addedCar = await _carService.AddCarAsync(car);

        _logger.LogInformation("Car added successfully with ID: {CarId}", addedCar.Id); // Log after the car is added

        return CreatedAtAction(nameof(GetCars), new { id = addedCar.Id }, addedCar);
    }





    [HttpPut("{id}")]
    public async Task<ActionResult<Car>> UpdateCar(int id, [FromBody] Car car)
    {
        var updatedCar = await _carService.UpdateCarAsync(id, car);

        if (updatedCar == null)
        {
            return NotFound(); 
        }

        return Ok(updatedCar);
    }
}


