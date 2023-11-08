using MediatR;
using VkFinalCase.Base.Response;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Cqrs;

public record CreatePaymentMethodCommand(PaymentMethodRequest Model) : IRequest<ApiResponse<PaymentMethodResponse>>;
public record UpdatePaymentMethodCommand(PaymentMethodRequest Model,int Id) : IRequest<ApiResponse>;
public record DeletePaymentMethodCommand(int Id) : IRequest<ApiResponse>;
public record GetAllPaymentMethodQuery() : IRequest<ApiResponse<List<PaymentMethodResponse>>>;
public record GetPaymentMethodByIdQuery(int Id) : IRequest<ApiResponse<PaymentMethodResponse>>;