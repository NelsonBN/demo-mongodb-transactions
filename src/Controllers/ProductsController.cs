using Demo.MongoDB.Transactions.Interfaces;
using Demo.MongoDB.Transactions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MongoDB.Transactions.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private static uint _testCount = 0;

    private readonly IUnitOfWork _uow;
    private readonly IProductsRepository _productsRrepository;
    private readonly IEventSourcingRepository _eventSourcingRepository;

    public ProductsController(
        IUnitOfWork uow,
        IProductsRepository productsRrepository,
        IEventSourcingRepository eventSourcingRepository
    )
    {
        _uow = uow;
        _productsRrepository = productsRrepository;
        _eventSourcingRepository = eventSourcingRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _productsRrepository.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Add(ProductRequest request)
    {
        _testCount++;

        var product = new Product();
        product.Number = _testCount;

        product.Name = request.Name;
        product.Quantity = request.Quantity;


        await _productsRrepository.AddAsync(product);

        var eventSourcing = new EventSourcing();
        eventSourcing.Description = $"Added product > Id: {product.Id} - {product.Number} > {product.Name} - Q:{product.Quantity}";

        await _eventSourcingRepository.AddAsync(eventSourcing);


        if(_testCount % 3 == 0)
        {
            await _uow.Rollback();

            throw new Exception("Exception to test transaction of the UoW");
        }

        await _uow.Commit();

        return Ok(product.Id);
    }
}
