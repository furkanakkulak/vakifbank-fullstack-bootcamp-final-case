using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Query;

public class OrderQueryHandler :
    IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>,
    IRequestHandler<GetAllOrdersByDealerIdQuery,ApiResponse<List<OrderResponse>>>,
    IRequestHandler<GetOrderByDealerIdQuery,ApiResponse<OrderResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public OrderQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request,
        CancellationToken cancellationToken)
    {
        List<Order> list = await dbContext.Set<Order>()
            .Include(x => x.PaymentMethod)
            .ToListAsync(cancellationToken);
        
        List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);
        return new ApiResponse<List<OrderResponse>>(mapped);
    }

    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        Order? entity = await dbContext.Set<Order>()
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<OrderResponse>("Record not found!");
        }
        
        OrderResponse mapped = mapper.Map<OrderResponse>(entity);
        return new ApiResponse<OrderResponse>(mapped);
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrdersByDealerIdQuery request, CancellationToken cancellationToken)
    {
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);

        List<Order> list = await dbContext.Set<Order>()
            .Include(x => x.PaymentMethod)
            .Where(x => x.DealerId == dealer.Id)
            .ToListAsync( cancellationToken);
        
        List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);
        return new ApiResponse<List<OrderResponse>>(mapped);
    }
    
    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByDealerIdQuery request, CancellationToken cancellationToken)
    {
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);
        
        Order? order = await dbContext.Set<Order>()
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.DealerId == dealer.Id && x.Id == request.orderId, cancellationToken);
        if (order == null)
        {
            return new ApiResponse<OrderResponse>("Record not found");
        }
        OrderResponse mapped = mapper.Map<OrderResponse>(order);
        return new ApiResponse<OrderResponse>(mapped);
    }
}