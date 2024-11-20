using Business.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.FluentValidation.Todo
{
    public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
    {
        public CreateTodoDtoValidator()
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
