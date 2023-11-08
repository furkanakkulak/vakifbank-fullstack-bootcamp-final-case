using FluentValidation;
using VkFinalCase.Data.Context;
using VkFinalCase.Operation.Cqrs;

namespace VkFinalCase.Operation.Validation;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly VkDbContext _dbContext;

    public CreateProductCommandValidator(VkDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Model.Name).NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x => x.Model.Price).NotEmpty().WithMessage("Price is required.");
        RuleFor(x => x.Model.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Model.StockQuantity).NotEmpty().WithMessage("StockQuantity is required.");
        RuleFor(x => x.Model.StockQuantity).GreaterThan(0).WithMessage("StockQuantity must be greater than zero.");

        RuleFor(x => x.Model.MinStockQuantity).NotEmpty().WithMessage("MinStockQuantity is required.");
        RuleFor(x => x.Model.MinStockQuantity).GreaterThan(0).WithMessage("MinStockQuantity must be greater than zero.");
    }
}
