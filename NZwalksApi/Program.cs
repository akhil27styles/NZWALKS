// Fix the namespace mismatch issue by ensuring the namespaces match between the interface and the implementation.  
// Update the using directive for SQLRegionRepository to match the correct namespace.  

using Microsoft.EntityFrameworkCore;
using NZwalksApi.Data;
using NZwalksApi.Mappings;
using NZwalksApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"));
});

// Fix for CS0311: Ensure SQLRegionRepository implements IRegionRepository  
builder.Services.AddScoped<NZwalksApi.Repositories.IRegionRepository, SQLRegionRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));  

// Uncomment the line below to use the in-memory repository instead of the SQL repository  
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();  

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
