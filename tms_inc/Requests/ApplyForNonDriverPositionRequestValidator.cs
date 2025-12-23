using FluentValidation;

namespace tms.Requests
{
    public class ApplyForNonDriverPositionRequestValidator: AbstractValidator<ApplyForNonDriverPositionRequest>
    {
        public ApplyForNonDriverPositionRequestValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .MinimumLength(3).WithMessage("{PropertyName} is empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(3).WithMessage("{PropertyName} is empty.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(10).WithMessage("{PropertyName} is empty.");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }
    }
}
