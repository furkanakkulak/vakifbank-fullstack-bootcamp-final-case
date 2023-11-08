
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkFinalCase.Base.Response;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Api.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class OrderPaymentController : ControllerBase
{
    private IMediator mediator;

    public OrderPaymentController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<OrderPaymentResponse>>> GetAll()
    {
        var operation = new GetAllOrderPaymentQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<OrderPaymentResponse>> Get(int id)
    {
        var operation = new GetOrderPaymentByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<OrderPaymentResponse>> Post([FromBody] OrderPaymentRequest request)
    {
        var operation = new CreateOrderPaymentCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] OrderPaymentRequest request)
    {
        var operation = new UpdateOrderPaymentCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteOrderPaymentCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}