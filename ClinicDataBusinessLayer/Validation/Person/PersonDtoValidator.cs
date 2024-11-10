namespace ClinicDataBusinessLayer.Validation.Person;

public class PersonDtoValidator : AbstractValidator<IPersonDto>
{
    public PersonDtoValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(Resources.PersonMessages.NameRequired)
            .MaximumLength(25)
            .WithMessage(Resources.PersonMessages.NameMaxLength);

        RuleFor(p => p.Phone)
            .NotEmpty()
            .WithMessage(Resources.PersonMessages.PhoneRequired)
            .Matches(@"^\d+$")
            .WithMessage(Resources.PersonMessages.PhoneInvalid)
            .Length(11, 11)
            .WithMessage(Resources.PersonMessages.PhoneLength);

        RuleFor(p => p.Address)
            .NotEmpty()
            .WithMessage(Resources.PersonMessages.AddressRequired)
            .MaximumLength(500)
            .WithMessage(Resources.PersonMessages.AddressMaxLength);

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(Resources.PersonMessages.EmailRequired)
            .EmailAddress()
            .WithMessage(Resources.PersonMessages.EmailInvalid)
            .MaximumLength(25)
            .WithMessage(Resources.PersonMessages.EmailMaxLength);

        RuleFor(p => p.DateOfBirth)
            .NotEmpty()
            .WithMessage(Resources.PersonMessages.DateOfBirthRequired)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage(Resources.PersonMessages.DateOfBirthInvalid);

        RuleFor(p => p.Gender)
            .NotEmpty()
            .WithMessage(Resources.PersonMessages.GenderRequired)
            .IsEnumName(typeof(Gender), false)
            .WithMessage(Resources.PersonMessages.GenderInvalid);

    }

}