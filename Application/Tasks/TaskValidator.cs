using Domain;
using FluentValidation;

namespace Application.Tasks
{
    public class TaskValidator : AbstractValidator<Task>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CreatedAt).NotEmpty();
        }
    }
}