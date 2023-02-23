namespace labo_02_oef1.Validators;
public class RegistrationValidator : AbstractValidator<VaccinRegistration>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.YearOfBirth).NotEmpty().WithMessage("Year of birth is required");
        RuleFor(x => x.VaccinationDate).NotEmpty().WithMessage("Vaccination date is required");
        RuleFor(x => x.VaccinTypeId).NotEmpty().WithMessage("Vaccin type is required");
        RuleFor(x => x.VaccinationLocationId).NotEmpty().WithMessage("Vaccination location is required");
    }
}