
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkFinalCase.Base.Response;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Api.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class DealerServicesController : ControllerBase
{
    private IMediator mediator;

    public DealerServicesController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("/products")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<ProductResponse>>> GetAllProductsByDealerId()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetAllProductsByDealerIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("/products/{productId}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<ProductResponse>> GetProductByDealerId(int productId)
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetProductByDealerIdQuery(int.Parse(id),productId);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("/orders")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<OrderResponse>>> GetAllOrdersByDealerId()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetAllOrdersByDealerIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpGet("/orders/{orderId}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<OrderResponse>> GetOrderByDealerId(int orderId)
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetOrderByDealerIdQuery(int.Parse(id),orderId);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpPost("/orders")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<OrderResponse>> CreateOrderByDealerId([FromBody] OrderRequest request)
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;

        var operation = new CreateOrderByDealerIdCommand(int.Parse(id),request);
        var result = await mediator.Send(operation);
        return result;
    }
    [HttpDelete("/orders/{orderId}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> DeleteOrderIdByDealerId(int orderId)
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new DeleteOrderIdByDealerIdCommand(int.Parse(id),orderId);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPost("/orderPayment")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<OrderPaymentResponse>> OrderPaymentByDealerId([FromBody] OrderPaymentRequest request)
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new OrderPaymentByDealerIdCommand(int.Parse(id),request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("/paymentMethods")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<PaymentMethodResponse>>> GetAllPaymentMethods()
    {
        var operation = new GetAllPaymentMethodQuery();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpGet("/informations")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<DealerResponse>> GetDealerInformationById()
    {
        var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
        var operation = new GetDealerInformationByIdQuery(int.Parse(id));
        var result = await mediator.Send(operation);
        return result;
    }
    
  
}