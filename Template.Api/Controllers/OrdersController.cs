using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Orders.Commands.CreateOrder;
using Template.Application.Orders.Queries.GetOrders;

namespace Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetOrdersQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}
