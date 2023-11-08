
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkFinalCase.Base.Response;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Api.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class PaymentMethodsController : ControllerBase
{
    private IMediator mediator;

    public PaymentMethodsController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<PaymentMethodResponse>>> GetAll()
    {
        var operation = new GetAllPaymentMethodQuery();
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<PaymentMethodResponse>> Get(int id)
    {
        var operation = new GetPaymentMethodByIdQuery(id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<PaymentMethodResponse>> Post([FromBody] PaymentMethodRequest request)
    {
        var operation = new CreatePaymentMethodCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] PaymentMethodRequest request)
    {
        var operation = new UpdatePaymentMethodCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeletePaymentMethodCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
}