using FluentValidation;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Validation;


    public class CreateOrderPaymentValidator : AbstractValidator<OrderPaymentRequest>
    {

        public CreateOrderPaymentValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("OrderId value min value is 1.");
            
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount is required.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount value min value is 0.");
        }
    }
