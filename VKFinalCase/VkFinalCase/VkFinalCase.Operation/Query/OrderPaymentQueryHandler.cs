using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Query;

public class OrderPaymentQueryHandler :
    IRequestHandler<GetAllOrderPaymentQuery, ApiResponse<List<OrderPaymentResponse>>>,
    IRequestHandler<GetOrderPaymentByIdQuery, ApiResponse<OrderPaymentResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public OrderPaymentQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<OrderPaymentResponse>>> Handle(GetAllOrderPaymentQuery request,
        CancellationToken cancellationToken)
    {
        List<OrderPayment> list = await dbContext.Set<OrderPayment>()
            .Include(x => x.Order)
            .ToListAsync(cancellationToken);
        
        List<OrderPaymentResponse> mapped = mapper.Map<List<OrderPaymentResponse>>(list);
        return new ApiResponse<List<OrderPaymentResponse>>(mapped);
    }

    public async Task<ApiResponse<OrderPaymentResponse>> Handle(GetOrderPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        OrderPayment? entity = await dbContext.Set<OrderPayment>()
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<OrderPaymentResponse>("Record not found!");
        }
        
        OrderPaymentResponse mapped = mapper.Map<OrderPaymentResponse>(entity);
        return new ApiResponse<OrderPaymentResponse>(mapped);
    }
}