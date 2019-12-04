using CaboAPI.DTOs;
using FluentValidation;

namespace CaboAPI.Validations
{
    public class TodoCaboUpdateDtoValidator : AbstractValidator<TodoCaboUpdateDto>
    {
        public TodoCaboUpdateDtoValidator()
        {
            RuleFor(x => x.NameActivity).NotNull().NotEmpty();
            RuleFor(x => x.Summary).Length(0, 10).NotEmpty().NotEmpty();
            RuleFor(x => x.DateEnded).NotNull().NotEmpty();
            RuleFor(x => x.DateStarted).NotNull().NotEmpty();
        }
    }
}