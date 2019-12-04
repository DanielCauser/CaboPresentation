using CaboAPI.DTOs;
using FluentValidation;

namespace CaboAPI.Validations
{
    public class TodoCaboCreateDtoValidator : AbstractValidator<TodoCaboCreateDto>
    {
        public TodoCaboCreateDtoValidator()
        {
            RuleFor(x => x.NameActivity).NotNull().NotEmpty();
            RuleFor(x => x.Summary).Length(0, 10).NotEmpty().NotEmpty();
            RuleFor(x => x.DateEnded).NotNull().NotEmpty();
            RuleFor(x => x.DateStarted).NotNull().NotEmpty();
        }

////         Injecting Child Validadtions
//        public PersonValidator(IValidator<Address> addressValidator)
//        {
//            RuleFor(x => x.Address).SetValidator(addressValidator);
////         Inject services for validation
//            RuleFor(x => x.Address).InjectValidator((services, context) => services.GetService<MyAddressValidator>());
//        }
    }
}