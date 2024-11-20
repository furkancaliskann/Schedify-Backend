using Business.Dtos;
using FluentValidation;

namespace Business.FluentValidation.Todo
{
    public class UpdateTodoDtoValidator : AbstractValidator<UpdateTodoDto>
    {
        public UpdateTodoDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(20).WithMessage("Title can't exceed 20 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Description can't exceed 100 characters.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status can only be Pending, Approved, or Rejected.");
        }
    }
}
