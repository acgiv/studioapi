using API.Data;
using API.Mappings;
using API.Model.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// inietto la configurazione del db nel calderone
builder.Services.AddDbContext<ApiDbContext>(options=> 
{
    options.UseSqlServer(
    // Legge la stringa di connessione dal file di configurazione.
    builder.Configuration.GetConnectionString("Api"));
});

// inietto la configurazione dell automapper nel calderone
/* Quando l'applicazione viene eseguita, esegue una scansione di tutte la mappature 
 * che può trovare. Quelle che trova nel file.  le memorizza.
 * Quando ne avarà bisiono, verrà iniettato nel controllore, in modo da poter utilizzare
 * la mapputra ogni qualvota ne ha bisogno.
*/
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// initto i repositopry nel candeorine
builder.Services.AddScoped<RegionRepository, RegionRepositoryImp>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
