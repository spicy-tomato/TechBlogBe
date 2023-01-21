using FluentValidation;
using TBB.Data.Models;
using TBB.Data.Requests.Post;

namespace TBB.Data.Validations;

public class CreatePostValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Body).NotEmpty();
        RuleFor(x => x.Mode).Must(value => Enum.IsDefined(typeof(PostMode), value));
    }
}