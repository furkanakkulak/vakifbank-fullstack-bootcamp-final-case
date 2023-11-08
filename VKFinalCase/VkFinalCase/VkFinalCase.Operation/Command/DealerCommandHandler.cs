using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Command;

public class DealerCommandHandler : 
    IRequestHandler<CreateDealerCommand,ApiResponse<DealerResponse>>,
    IRequestHandler<UpdateDealerCommand,ApiResponse>,
    IRequestHandler<DeleteDealerCommand,ApiResponse>

{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public DealerCommandHandler(VkDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    
    public async Task<ApiResponse<DealerResponse>> Handle(CreateDealerCommand request, CancellationToken cancellationToken)
    {
        Dealer mapped = mapper.Map<Dealer>(request.Model);

        var entity = await dbContext.Set<Dealer>().AddAsync(mapped,cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<DealerResponse>(entity.Entity);
        return new ApiResponse<DealerResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateDealerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Dealer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.UserId = request.Model.UserId;
        entity.Address = request.Model.Address;
        entity.TaxNumber = request.Model.TaxNumber;
        entity.CreditLimit = request.Model.CreditLimit;
        entity.Margin = request.Model.Margin;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteDealerCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Dealer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}