using MediatR;
using VkFinalCase.Base.Response;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;