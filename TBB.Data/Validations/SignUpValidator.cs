using FluentValidation;
using TBB.Data.Requests.User;

namespace TBB.Data.Validations;

public class SignUpValidator : AbstractValidator<SignUpRequest>
{
    public SignUpValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}