using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace labo_02_oef1.repository;

public interface IVaccinTypeRepository
{
    List<VaccinType> GetVaccins();
}

public class VaccinTypeRepository : IVaccinTypeRepository
{
    private static List<VaccinType> _vaccins = new List<VaccinType>();

    public VaccinTypeRepository()
    {
        if (!(_vaccins.Any()))
        {
            _vaccins.Add(new VaccinType()
            {
                VaccinTypeId = Guid.Parse("2774e3d1-2b0f-47ab-b391-8ea43e6f9d80"),
                Name = "Pfizer"
            });
            _vaccins.Add(new VaccinType()
            {
                VaccinTypeId = Guid.Parse("0bb537ea-8209-422f-a9e1-2c1e37d0cb4d"),
                Name = "Moderna"
            });
        }
    }

    public List<VaccinType> GetVaccins()
    {
        return _vaccins.ToList<VaccinType>();
    }
}


public class CsvVaccinTypeRepository : IVaccinTypeRepository
{
    private readonly CsvConfig _csvConfig;

    public CsvVaccinTypeRepository(IOptions<CsvConfig> csvConfig)
    {
        _csvConfig = csvConfig.Value;
    }


    public List<VaccinType> GetVaccins()
    {
        try
        {
            using var reader = new StreamReader(_csvConfig.CsvVaccins);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<VaccinType>();
            return records.ToList();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}