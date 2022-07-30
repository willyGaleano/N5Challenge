using FluentValidation;

namespace N5.Challenge.Core.Application.Features.Permission.Commands.CreateDataFake.Validators
{
    public class CreateDataFakeCommandValidator : AbstractValidator<CreateDataFakeCommand>
    {
        const int MIN = 5;
        const int MAX = 50;
        public CreateDataFakeCommandValidator()
        {
            RuleFor(p => p.Cant)               
                .NotEmpty().WithMessage("El campo {PropertyName} no debe estar vacío.")
                .NotNull().WithMessage("El campo {PropertyName} no debe de ser nulo.")
                .InclusiveBetween(MIN, MAX).WithMessage("El campo {PropertyName} " + $"debe de estar entre {MIN} y {MAX} inclusive");                
        }
    }
}
