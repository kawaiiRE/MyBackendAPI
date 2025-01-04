using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public interface ICarService
{
    Task<IEnumerable<Car>> GetCarsAsync();
    Task<Car> AddCarAsync(Car car);
    Task<Car> UpdateCarAsync(int id, Car updatedCar);
}

public class CarService : ICarService
{
    // Declare the list as a static field to persist the data across requests
    private readonly List<Car> _cars;
    private readonly ILogger<CarService> _logger;

    // Constructor: This is called only once, and it will initialize the list only the first time
    public CarService(ILogger<CarService> logger)
    {
        _logger = logger;

        // Initialize the list only if it is empty
        if (_cars == null || !_cars.Any())
        {
            _cars = new List<Car>
            {
                new Car { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2020, Hidden=false },
                new Car { Id = 2, Brand = "Honda", Model = "Civic", Year = 2021, Hidden=false  },
                new Car { Id = 3, Brand = "Ford", Model = "Mustang", Year = 2022, Hidden=false  }
            };
            _logger.LogInformation("Car list initialized with predefined cars.");
        }
    }

    // Fetch all cars
    public Task<IEnumerable<Car>> GetCarsAsync()
    {
        var sortedCars = _cars.OrderByDescending(car => car.Id);
        return Task.FromResult<IEnumerable<Car>>(sortedCars);
        // return Task.FromResult<IEnumerable<Car>>(_cars);
    }

    // Add a new car
    public Task<Car> AddCarAsync(Car car)
    {
        // Assign a new ID based on the current count
        car.Id = _cars.Count + 1;
        _cars.Add(car);

        // Log all cars after adding the new car
        _logger.LogInformation("All cars after adding new car:");
        foreach (var c in _cars)
        {
            _logger.LogInformation("Car ID: {Id}, Brand: {Brand}, Model: {Model}, Year: {Year}", c.Id, c.Brand, c.Model, c.Year);
        }

        return Task.FromResult(car);
    }

    // edit car
    public Task<Car> UpdateCarAsync(int id, Car updatedCar)
    {
        // Find the car by ID
        var existingCar = _cars.FirstOrDefault(c => c.Id == id);

        if (existingCar == null)
        {
            return Task.FromResult<Car>(null);  // If car not found, return null
        }

        // Update the car details
        existingCar.Brand = updatedCar.Brand;
        existingCar.Model = updatedCar.Model;
        existingCar.Year = updatedCar.Year;
        existingCar.Hidden = updatedCar.Hidden;

        // Log the update
        _logger.LogInformation("Updated car with ID: {Id}, Brand: {Brand}, Model: {Model}, Year: {Year}",
            existingCar.Id, existingCar.Brand, existingCar.Model, existingCar.Year);

        return Task.FromResult(existingCar);
    }
}
