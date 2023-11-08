using MediatR;
using VkFinalCase.Base.Response;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Cqrs;

public record CreateProductCommand(ProductRequest Model) : IRequest<ApiResponse<ProductResponse>>;
public record UpdateProductCommand(ProductRequest Model,int Id) : IRequest<ApiResponse>;
public record DeleteProductCommand(int Id) : IRequest<ApiResponse>;
public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
public record GetProductByIdQuery(int Id) : IRequest<ApiResponse<ProductResponse>>;


public record GetAllProductsByDealerIdQuery(int Id) : IRequest<ApiResponse<List<ProductResponse>>>;
public record GetProductByDealerIdQuery(int Id,int productId) : IRequest<ApiResponse<ProductResponse>>;


