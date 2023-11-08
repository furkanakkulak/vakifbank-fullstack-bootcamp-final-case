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

public class OrderPaymentCommandHandler : 
    IRequestHandler<CreateOrderPaymentCommand,ApiResponse<OrderPaymentResponse>>,
    IRequestHandler<UpdateOrderPaymentCommand,ApiResponse>,
    IRequestHandler<DeleteOrderPaymentCommand,ApiResponse>,
    IRequestHandler<OrderPaymentByDealerIdCommand,ApiResponse<OrderPaymentResponse>>

{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public OrderPaymentCommandHandler(VkDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    
    public async Task<ApiResponse<OrderPaymentResponse>> Handle(CreateOrderPaymentCommand request, CancellationToken cancellationToken)
    {
        OrderPayment mapped = mapper.Map<OrderPayment>(request.Model);
        mapped.PaymentDate=DateTime.Now;
        
        Order? order = await dbContext.Set<Order>()
            .FirstOrDefaultAsync(x => x.Id == request.Model.OrderId,cancellationToken);
        if (order.Status != "Payment")
        {
            return new ApiResponse<OrderPaymentResponse>("In order to make payment, your order status must be in the payment step. Your order status: " + OrderStatus.Approved.ToString());
        }
        if (order.TotalPrice != request.Model.Amount)
        {
            return new ApiResponse<OrderPaymentResponse>("TotalPrice is not equal to Payment Amount!");
        }

        order.Status = OrderStatus.Approved.ToString();
        
        var entity = await dbContext.Set<OrderPayment>().AddAsync(mapped,cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<OrderPaymentResponse>(entity.Entity);
        return new ApiResponse<OrderPaymentResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateOrderPaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<OrderPayment>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.OrderId = request.Model.OrderId;
        entity.Amount = request.Model.Amount;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteOrderPaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<OrderPayment>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        entity.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse<OrderPaymentResponse>> Handle(OrderPaymentByDealerIdCommand request, CancellationToken cancellationToken)
    {
        Dealer dealer = await dbContext.Set<Dealer>()
            .FirstOrDefaultAsync(x => x.UserId == request.Id);
        OrderPayment mapped = mapper.Map<OrderPayment>(request.Model);
        mapped.PaymentDate=DateTime.Now;
        
        Order? order = await dbContext.Set<Order>()
            .FirstOrDefaultAsync(x => x.Id == request.Model.OrderId && x.DealerId==dealer.Id,cancellationToken);
        if (order==null)
        {
            return new ApiResponse<OrderPaymentResponse>("Record not found");

        }
        if (order.Status != "Payment")
        {
            return new ApiResponse<OrderPaymentResponse>("In order to make payment, your order status must be in the payment step. Your order status: " + OrderStatus.Approved.ToString());
        }
        if (order.TotalPrice != request.Model.Amount)
        {
            return new ApiResponse<OrderPaymentResponse>("TotalPrice is not equal to Payment Amount!");
        }

        order.Status = OrderStatus.Approved.ToString();
        
        var entity = await dbContext.Set<OrderPayment>().AddAsync(mapped,cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<OrderPaymentResponse>(entity.Entity);
        return new ApiResponse<OrderPaymentResponse>(response);
    }
}