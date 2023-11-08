using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Query;

public class DealerQueryHandler :
    IRequestHandler<GetAllDealerQuery, ApiResponse<List<DealerResponse>>>,
    IRequestHandler<GetDealerByIdQuery, ApiResponse<DealerResponse>>,
    IRequestHandler<GetDealerInformationByIdQuery,ApiResponse<DealerResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public DealerQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<DealerResponse>>> Handle(GetAllDealerQuery request,
        CancellationToken cancellationToken)
    {
        List<Dealer> list = await dbContext.Set<Dealer>()
            .Include(x => x.Orders)
            .ToListAsync(cancellationToken);
        
        List<DealerResponse> mapped = mapper.Map<List<DealerResponse>>(list);
        return new ApiResponse<List<DealerResponse>>(mapped);
    }

    public async Task<ApiResponse<DealerResponse>> Handle(GetDealerByIdQuery request,
        CancellationToken cancellationToken)
    {
        Dealer? entity = await dbContext.Set<Dealer>()
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<DealerResponse>("Record not found!");
        }
        
        DealerResponse mapped = mapper.Map<DealerResponse>(entity);
        return new ApiResponse<DealerResponse>(mapped);
    }

    public async Task<ApiResponse<DealerResponse>> Handle(GetDealerInformationByIdQuery request, CancellationToken cancellationToken)
    {
        Dealer? entity = await dbContext.Set<Dealer>()
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.UserId == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<DealerResponse>("Record not found!");
        }
        
        DealerResponse mapped = mapper.Map<DealerResponse>(entity);
        return new ApiResponse<DealerResponse>(mapped);
    }
}