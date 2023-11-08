using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Command;

public class PaymentMethodCommandHandler : 
    IRequestHandler<CreatePaymentMethodCommand,ApiResponse<PaymentMethodResponse>>,
    IRequestHandler<UpdatePaymentMethodCommand,ApiResponse>,
    IRequestHandler<DeletePaymentMethodCommand,ApiResponse>
    
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentMethodCommandHandler(VkDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    
    public async Task<ApiResponse<PaymentMethodResponse>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        PaymentMethod mapped = mapper.Map<PaymentMethod>(request.Model);

        var entity = await dbContext.Set<PaymentMethod>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<PaymentMethodResponse>(entity.Entity);
        return new ApiResponse<PaymentMethodResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<PaymentMethod>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<PaymentMethod>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}