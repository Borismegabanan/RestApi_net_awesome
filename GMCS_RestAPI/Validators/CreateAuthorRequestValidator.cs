using FluentValidation;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestApi.Domain.Common;

namespace GMCS_RestAPI.Validators
{
    public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequest>
    {
        public CreateAuthorRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(DisplayMessages.Validation.RequireProperty);
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage(DisplayMessages.Validation.RequireProperty);
            RuleFor(x => x.MiddleName).NotNull().NotEmpty().WithMessage(DisplayMessages.Validation.RequireProperty);
        }
    }
}
