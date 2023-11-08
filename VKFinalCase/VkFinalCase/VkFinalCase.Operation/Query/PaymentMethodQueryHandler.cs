using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Query;

public class PaymentMethodQueryHandler :
    IRequestHandler<GetAllPaymentMethodQuery, ApiResponse<List<PaymentMethodResponse>>>,
    IRequestHandler<GetPaymentMethodByIdQuery, ApiResponse<PaymentMethodResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentMethodQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<PaymentMethodResponse>>> Handle(GetAllPaymentMethodQuery request,
        CancellationToken cancellationToken)
    {
        List<PaymentMethod> list = await dbContext.Set<PaymentMethod>()
            .Include(x => x.Orders)
            .ToListAsync(cancellationToken);
        
        List<PaymentMethodResponse> mapped = mapper.Map<List<PaymentMethodResponse>>(list);
        return new ApiResponse<List<PaymentMethodResponse>>(mapped);
    }

    public async Task<ApiResponse<PaymentMethodResponse>> Handle(GetPaymentMethodByIdQuery request,
        CancellationToken cancellationToken)
    {
        PaymentMethod? entity = await dbContext.Set<PaymentMethod>()
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<PaymentMethodResponse>("Record not found!");
        }
        
        PaymentMethodResponse mapped = mapper.Map<PaymentMethodResponse>(entity);
        return new ApiResponse<PaymentMethodResponse>(mapped);
    }
}