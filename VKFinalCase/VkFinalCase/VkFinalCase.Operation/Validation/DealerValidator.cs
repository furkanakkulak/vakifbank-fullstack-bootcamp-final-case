using FluentValidation;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Validation;


    public class CreateDealerValidator : AbstractValidator<DealerRequest>
    {

        public CreateDealerValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.Address).MaximumLength(100).WithMessage("Address length max value is 100.");
            
            RuleFor(x => x.TaxNumber).NotEmpty().WithMessage("TaxNumber is required.");
            RuleFor(x => x.TaxNumber).Length(10).WithMessage("TaxNumber length min and max value is 10.");
            
            RuleFor(x => x.CreditLimit).NotEmpty().WithMessage("CreditLimit is required.");
            RuleFor(x => x.CreditLimit).GreaterThan(0).WithMessage("CreditLimit length min value is 0.");
            
            RuleFor(x => x.Margin).NotEmpty().WithMessage("Margin is required.");
            RuleFor(x => x.Margin).GreaterThan(0).WithMessage("Margin length min value is 0.");
            
        }
    }
