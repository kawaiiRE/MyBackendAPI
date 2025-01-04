using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
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
        // _logger.LogInformation("getting cars");

        var cars = await _carService.GetCarsAsync();
        return Ok(cars);
    }

    [HttpPost]
    public async Task<ActionResult<Car>> AddCar([FromBody] Car car)
    {
        // _logger.LogInformation("new car: {Brand} {Model}, {Year}", car.Brand, car.Model, car.Year);

        var addedCar = await _carService.AddCarAsync(car);

        // _logger.LogInformation("id: {CarId}", addedCar.Id);

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


