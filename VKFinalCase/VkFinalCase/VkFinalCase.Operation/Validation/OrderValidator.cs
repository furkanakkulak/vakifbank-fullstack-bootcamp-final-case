using FluentValidation;
using VkFinalCase.Data.Context;
using VkFinalCase.Operation.Cqrs;

namespace VkFinalCase.Operation.Validation;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly VkDbContext _dbContext;

    public CreateOrderCommandValidator(VkDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Model.DealerId).NotEmpty().WithMessage("DealerId is required.");
        RuleFor(x => x.Model.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Model.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.Model.DealerId)
            .Must(dealerId => IsValidDealerInfo(dealerId))
            .When(x => x.Model.DealerId > 0)
            .WithMessage("Dealer must fill in the info before creating an order.");
    }

    private bool IsValidDealerInfo(int dealerId)
    {
        return dealerId > 0 && _dbContext.Dealers.Any(d => d.UserId == dealerId);
    }

}
