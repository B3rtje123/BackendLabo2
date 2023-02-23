namespace labo_02_oef1.Services
{
    public interface IVaccinationService
    {
        VaccinRegistration AddRegistration(VaccinRegistration registration);
        List<VaccinationLocation> GetLocations();
        List<VaccinRegistration> GetRegistrations();
        List<VaccinType> GetVaccins();
    }

    public class VaccinationService : IVaccinationService
    {
        private readonly IVaccinationLocationRepository _locationRepository;
        private readonly IVaccinationRegistrationRepository _registrationRepository;
        private readonly IVaccinTypeRepository _vaccinTypeRepository;

        public VaccinationService(IVaccinationLocationRepository locationRepository, IVaccinationRegistrationRepository registrationRepository, IVaccinTypeRepository vaccinTypeRepository)
        {
            _locationRepository = locationRepository;
            _registrationRepository = registrationRepository;
            _vaccinTypeRepository = vaccinTypeRepository;
        }

        public VaccinRegistration AddRegistration(VaccinRegistration registration)
        {
            ArgumentNullException.ThrowIfNull(registration);
            _registrationRepository.AddRegistration(registration);
            return registration;
        }

        public List<VaccinationLocation> GetLocations()
        {
            return _locationRepository.GetLocations();
        }

        public List<VaccinRegistration> GetRegistrations()
        {
            return _registrationRepository.GetRegistrations();
        }

        public List<VaccinType> GetVaccins()
        {
            return _vaccinTypeRepository.GetVaccins();
        }
    }
}