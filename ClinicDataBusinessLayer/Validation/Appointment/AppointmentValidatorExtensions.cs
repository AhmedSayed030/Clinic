namespace ClinicDataBusinessLayer.Validation.Appointment;

public static class AppointmentValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> AppointmentId<T>(this IRuleBuilder<T, int> ruleBuilder)  
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(Resources.AppointmentMessages.KeyGreaterThanZero);
    }

    public static IRuleBuilderOptions<T, DateTime> AppointmentDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.AppointmentMessages.DateRequired)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage(Resources.AppointmentMessages.DateInvalid);
    }

    public static IRuleBuilderOptions<T, string> AppointmentStatus<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(Resources.AppointmentMessages.StatusRequired)
            .IsEnumName(typeof(AppointmentStatus), false)
            .WithMessage(Resources.AppointmentMessages.StatusInvalid);
    }
}