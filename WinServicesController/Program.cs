using pkb.winutils;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();
app.UseRouting();
IConfigurationRoot config;

if (app.Environment.IsDevelopment())
{
    config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();
}
else
{
    config = new ConfigurationBuilder()
.AddJsonFile("appsettings.Development.json", false)
.Build();
}

List<String> allowedOrigins = config.GetSection("CorsConfig:AllowedOrigins").Get<List<string>>()!;


/* app.UseCors(builder => builder
   .SetIsOriginAllowedToAllowWildcardSubdomains()
   .WithOrigins([..allowedOrigins])
   .AllowAnyHeader()
   .AllowAnyMethod()
   .AllowCredentials()
   .SetPreflightMaxAge(TimeSpan.FromSeconds(3600))
); */


app.MapControllers();
app.Run();



