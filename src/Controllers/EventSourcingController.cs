using Demo.MongoDB.Transactions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MongoDB.Transactions.Controllers;

[ApiController]
[Route("[controller]")]
public class EventSourcingController : ControllerBase
{
    private readonly IEventSourcingRepository _repository;

    public EventSourcingController(IEventSourcingRepository repository)
        => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repository.GetAllAsync());
}
