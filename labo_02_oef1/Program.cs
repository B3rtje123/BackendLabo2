var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IVaccinationLocationRepository, CsvVaccinationLocationRepository>();
builder.Services.AddTransient<IVaccinationRegistrationRepository, CsvVaccinationRegistrationRepository>();
builder.Services.AddTransient<IVaccinTypeRepository, CsvVaccinTypeRepository>();

var csvSettings = builder.Configuration.GetSection("CsvConfig");
builder.Services.Configure<CsvConfig>(csvSettings);


builder.Services.AddTransient<IVaccinationService, VaccinationService>();

builder.Services.AddValidatorsFromAssemblyContaining<RegistrationValidator>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/locations", (IVaccinationLocationRepository repository) => { 
    return Results.Ok(repository.GetLocations());
});


app.MapGet("/registrations", (IMapper mapper, IVaccinationService service) => { 
    var mapped = mapper.Map<List<VaccinRegistrationDTO>>(service.GetRegistrations(), opts => {
        opts.Items["locations"] = service.GetLocations();
        opts.Items["vaccins"] = service.GetVaccins();
    });
    return Results.Ok(mapped);
});



app.MapPost("/registrations", (IVaccinationService service, VaccinRegistration registration) => { 
    return Results.Ok(service.AddRegistration(registration));
});

app.MapGet("/vaccins", (IVaccinationService service) => { 
    return Results.Ok(service.GetVaccins());
});


app.Run("http://localhost:5000");
