using FluentValidation;
using GMCS_RestAPI.Contracts.Request;
using GMCS_RestApi.Domain.Common;

namespace GMCS_RestAPI.Validators
{
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage(DisplayMessages.Validation.RequireProperty);
            RuleFor(x => x.AuthorId).NotEmpty().NotNull().WithMessage(DisplayMessages.Validation.RequireProperty);
        }
    }
}
