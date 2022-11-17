using user_service.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.UseDbContextConfiguration(builder.Configuration.GetConnectionString("UserContext"));
builder.Services.UseEurekaConfiguration();
builder.Services.UseHttpConfiguration("user-service", "lb://user-service");
//builder.Services.UseRabbitConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

///app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
