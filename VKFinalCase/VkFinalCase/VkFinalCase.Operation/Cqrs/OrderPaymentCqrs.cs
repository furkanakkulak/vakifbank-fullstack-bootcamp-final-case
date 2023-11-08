using MediatR;
using VkFinalCase.Base.Response;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Cqrs;

public record CreateOrderPaymentCommand(OrderPaymentRequest Model) : IRequest<ApiResponse<OrderPaymentResponse>>;
public record UpdateOrderPaymentCommand(OrderPaymentRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteOrderPaymentCommand(int Id) : IRequest<ApiResponse>;
public record GetAllOrderPaymentQuery() : IRequest<ApiResponse<List<OrderPaymentResponse>>>;
public record GetOrderPaymentByIdQuery(int Id) : IRequest<ApiResponse<OrderPaymentResponse>>;
public record OrderPaymentByDealerIdCommand(int Id,OrderPaymentRequest Model) : IRequest<ApiResponse<OrderPaymentResponse>>;

