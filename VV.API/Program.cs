using static System.Collections.Specialized.BitVector32;
using VV.Database.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureAutoMapper();

builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsAllAccessPolicy", opt => opt.AllowAnyOrigin()
													  .AllowAnyHeader()
													  .AllowAnyMethod());
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VVContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VVConnection")));
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureAutoMapper()
{
	var config = new AutoMapper.MapperConfiguration(cfg =>
	{
	});
	var mapper = config.CreateMapper();
	builder.Services.AddSingleton(mapper);
}