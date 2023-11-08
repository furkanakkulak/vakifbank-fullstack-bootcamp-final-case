using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Query;

public class ProductQueryHandler :
    IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
    IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>,
    IRequestHandler<GetAllProductsByDealerIdQuery, ApiResponse<List<ProductResponse>>>,
    IRequestHandler<GetProductByDealerIdQuery, ApiResponse<ProductResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public ProductQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request,
        CancellationToken cancellationToken)
    {
        List<Product> list = await dbContext.Set<Product>()
            .Include(x => x.Orders)
            .Where(x=>x.IsActive==true)
            .ToListAsync(cancellationToken);
        
        List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
        return new ApiResponse<List<ProductResponse>>(mapped);
    }

    public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        Product? entity = await dbContext.Set<Product>()
            .Include(x => x.Orders)
            .Where(x=>x.IsActive==true)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        
        if (entity == null)
        {
            return new ApiResponse<ProductResponse>("Record not found!");
        }
        
        ProductResponse mapped = mapper.Map<ProductResponse>(entity);
        return new ApiResponse<ProductResponse>(mapped);
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductsByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Product> list = await dbContext.Set<Product>()
            .Where(x=>x.IsActive==true)
            .ToListAsync(cancellationToken);
        
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id,cancellationToken);

        foreach (var product in list)
        {
            product.Price = (product.Price + (product.Price * dealer.Margin / 100));
        }
        
        List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);
        return new ApiResponse<List<ProductResponse>>(mapped);
    }
    
    public async Task<ApiResponse<ProductResponse>> Handle(GetProductByDealerIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Set<Product>()
            .Where(x=>x.IsActive==true)
            .FirstOrDefaultAsync(x=>x.Id==request.productId,cancellationToken);

        if (product==null)
        {
            return new ApiResponse<ProductResponse>("Record not found");
        }
        
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id,cancellationToken);
        
        product.Price = (product.Price + (product.Price * dealer.Margin / 100));
        
        ProductResponse mapped = mapper.Map<ProductResponse>(product);
        return new ApiResponse<ProductResponse>(mapped);
    }
}