using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkFinalCase.Base.Response;
using VkFinalCase.Base.Status;
using VkFinalCase.Data.Context;
using VkFinalCase.Data.Domain;
using VkFinalCase.Operation.Cqrs;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Command;

public class OrderCommandHandler : 
    IRequestHandler<CreateOrderCommand,ApiResponse<OrderResponse>>,
    IRequestHandler<UpdateOrderCommand,ApiResponse>,
    IRequestHandler<DeleteOrderCommand,ApiResponse>,
    IRequestHandler<UpdateOrderStatusCommand,ApiResponse<OrderResponse>>,
    IRequestHandler<CreateOrderByDealerIdCommand,ApiResponse<OrderResponse>>,
    IRequestHandler<DeleteOrderIdByDealerIdCommand,ApiResponse>

{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public OrderCommandHandler(VkDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    
    public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order mapped = mapper.Map<Order>(request.Model);
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.Id == request.Model.DealerId,cancellationToken);
        Product? product = await dbContext.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == mapped.ProductId,cancellationToken);
        mapped.OrderDate = DateTime.Now;

        if (product != null && product.StockQuantity >= mapped.Quantity && dealer != null && product.MinStockQuantity<mapped.Quantity)
        {
            mapped.Status = OrderStatus.Pending.ToString();
            mapped.TotalPrice = mapped.Quantity * (product.Price + (product.Price * dealer.Margin / 100));

            // Stok miktarını güncelle
            product.StockQuantity -= mapped.Quantity;
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            mapped.Status = OrderStatus.Denied.ToString();
            mapped.TotalPrice= 0;
            return new ApiResponse<OrderResponse>("Something went wrong: Please make sure the product exists, check the product stock or minimum order stock.");

        }

        var entity = await dbContext.Set<Order>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<OrderResponse>(entity.Entity);
        return new ApiResponse<OrderResponse>(response);
    }


    public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }


        entity.DealerId = request.Model.DealerId;
        entity.ProductId = request.Model.ProductId;
        entity.Quantity = request.Model.Quantity;
        entity.PaymentMethodId = Convert.ToInt16(request.Model.PaymentMethodId);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        
        Product? product = await dbContext.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == entity.ProductId);

        entity.Status = OrderStatus.Denied.ToString();
        product.StockQuantity += entity.Quantity;
        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
    
    public async Task<ApiResponse<OrderResponse>> Handle(UpdateOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        Order? entity = await dbContext.Set<Order>()
            .Include(x => x.PaymentMethod)
            .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
       
        if (entity == null)
        {
            return new ApiResponse<OrderResponse>("Record not found!");
        }


        entity.Status = OrderStatus.Payment.ToString();

        await dbContext.SaveChangesAsync(cancellationToken);
        
        OrderResponse mapped = mapper.Map<OrderResponse>(entity);
        return new ApiResponse<OrderResponse>(mapped);
    }

    public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderByDealerIdCommand request, CancellationToken cancellationToken)
    {
        Order mapped = mapper.Map<Order>(request.Model);
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id,cancellationToken);
        Product? product = await dbContext.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == mapped.ProductId,cancellationToken);
        
        if (dealer.Id != request.Model.DealerId)
        {
            return new ApiResponse<OrderResponse>("Something went wrong: Please make sure DealerId and AccountID match");
        }
      
        mapped.OrderDate = DateTime.Now;
        if (product != null && product.StockQuantity >= mapped.Quantity && dealer != null && product.MinStockQuantity<mapped.Quantity)
        {
            mapped.Status = OrderStatus.Pending.ToString();
            mapped.TotalPrice = mapped.Quantity * (product.Price + (product.Price * dealer.Margin / 100));
            product.StockQuantity -= mapped.Quantity;
        }
        else
        {
            mapped.Status = OrderStatus.Denied.ToString();
            mapped.TotalPrice= 0;
            return new ApiResponse<OrderResponse>("Something went wrong: Please make sure the product exists, check the product stock or minimum order stock.");
        }
        var entity = await dbContext.Set<Order>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        var response = mapper.Map<OrderResponse>(entity.Entity);
        return new ApiResponse<OrderResponse>(response);
    }

    public async Task<ApiResponse> Handle(DeleteOrderIdByDealerIdCommand request, CancellationToken cancellationToken)
    {
     
        Dealer? dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id,cancellationToken);
        var entity = await dbContext.Set<Order>()
            .FirstOrDefaultAsync(x => x.Id == request.orderId, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        Product? product = await dbContext.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == entity.ProductId);
        
        if (entity.DealerId != dealer.Id)
        {
            return new ApiResponse("This order is not yours!");

        }

        if (entity.Status != OrderStatus.Pending.ToString())
        {
            return new ApiResponse("You can only cancel the order while it is in the pending stage.");
        }
        entity.IsActive = false;
        entity.Status = OrderStatus.Denied.ToString();
        product.StockQuantity += entity.Quantity;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}