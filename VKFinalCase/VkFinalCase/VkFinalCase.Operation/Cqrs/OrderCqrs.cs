using MediatR;
using VkFinalCase.Base.Response;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Cqrs;

public record CreateOrderCommand(OrderRequest Model) : IRequest<ApiResponse<OrderResponse>>;
public record UpdateOrderCommand(OrderRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteOrderCommand(int Id) : IRequest<ApiResponse>;
public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
public record GetOrderByIdQuery(int Id) : IRequest<ApiResponse<OrderResponse>>;

public record UpdateOrderStatusCommand(int Id) : IRequest<ApiResponse<OrderResponse>>;


public record CreateOrderByDealerIdCommand(int Id,OrderRequest Model) : IRequest<ApiResponse<OrderResponse>>;
public record GetOrderByDealerIdQuery(int Id,int orderId) : IRequest<ApiResponse<OrderResponse>>;
public record GetAllOrdersByDealerIdQuery(int Id) : IRequest<ApiResponse<List<OrderResponse>>>;

public record DeleteOrderIdByDealerIdCommand(int Id,int orderId) : IRequest<ApiResponse>;



