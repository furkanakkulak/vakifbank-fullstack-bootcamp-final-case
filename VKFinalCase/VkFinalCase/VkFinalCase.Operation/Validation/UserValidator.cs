using FluentValidation;
using VkFinalCase.Schema;

namespace VkFinalCase.Operation.Validation;


    public class CreateUserValidator : AbstractValidator<UserRequest>
    {

        public CreateUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Username).MinimumLength(3).WithMessage("Username length min value is 3.");
            
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required.");
        
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Email).MinimumLength(10).WithMessage("Email length min value is 10.");
        
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password length min value is 8.");
        }
    }
