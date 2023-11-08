using FluentValidation;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Validation;


    public class CreatePaymentMethodValidator : AbstractValidator<PaymentMethodRequest>
    {

        public CreatePaymentMethodValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name length min length is 3.");
        }
    }
