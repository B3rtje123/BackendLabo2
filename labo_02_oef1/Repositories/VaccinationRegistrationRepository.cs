using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace labo_02_oef1.repository;

public interface IVaccinationRegistrationRepository
{
    List<VaccinRegistration> GetRegistrations();
    void AddRegistration(VaccinRegistration registration);
}

public class VaccinationRegistrationRepository : IVaccinationRegistrationRepository
{
    private static List<VaccinRegistration> _registrations = new List<VaccinRegistration>();

    public List<VaccinRegistration> GetRegistrations()
    {
        return _registrations.ToList<VaccinRegistration>();
    }
    
    public void AddRegistration(VaccinRegistration registration)
    {
        _registrations.Add(registration);
    }
}

public class CsvVaccinationRegistrationRepository : IVaccinationRegistrationRepository
{
    private readonly CsvConfig _csvConfig;

    public CsvVaccinationRegistrationRepository(IOptions<CsvConfig> csvConfig)
    {
        _csvConfig = csvConfig.Value;
    }
    public void AddRegistration(VaccinRegistration registration){
        ArgumentNullException.ThrowIfNull(registration);
        var registrations = GetRegistrations();
        registrations.Add(registration);
        try
        {
            using var writer = new StreamWriter(_csvConfig.CsvRegistrations);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            using var csv = new CsvWriter(writer, config);
            csv.WriteRecords(registrations);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public List<VaccinRegistration> GetRegistrations()
    {
        try
        {
            using var reader = new StreamReader(_csvConfig.CsvRegistrations);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";"
            };
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<VaccinRegistration>();
            return records.ToList();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}