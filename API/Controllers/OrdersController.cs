using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService service, ILogger<OrdersController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(dto);
            _logger.LogInformation("Creating order for CustomerName {CustomerName}", order.CustomerName);
            var created = await _service.CreateAsync(order);
            _logger.LogInformation("Order created with Id {OrderId}", created.OrderId);
            var response = _mapper.Map<OrderResponseDto>(created);
            return Ok(response);
        }

   

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Fetching order {OrderId}", id);
            var order = await _service.GetOrderAsync(id);

            if (order is null)
            {
                _logger.LogWarning("Order {OrderId} not found", id);
                return NotFound();
            }
            return Ok(_mapper.Map<OrderResponseDto>(order));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all orders");
            var orders = await _service.GetAllAsync();
            var dtos = _mapper.Map<List<OrderResponseDto>>(orders);
            return Ok(dtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting order {OrderId}", id);
            bool deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                _logger.LogWarning("Order {OrderId} could not be deleted", id);
                return NotFound();
            }

            _logger.LogInformation("Order {OrderId} deleted", id);
            return Ok();
        }
    }
}
