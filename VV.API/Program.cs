using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureAutoMapper();

builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsAllAccessPolicy", opt => opt.AllowAnyOrigin()
													  .AllowAnyHeader()
													  .AllowAnyMethod());
});
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
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
	var mapConfig = new AutoMapper.MapperConfiguration(config =>
	{
		config.CreateMap<Video, VideoDTO>().ReverseMap();
		config.CreateMap<VideoEditDTO, Video>();    //.ForMember(vid => vid.Director, src => src.Ignore())
													//											.ForMember(vid => vid.Genres, src => src.Ignore())
													//											.ForMember(vid => vid.SimilarVideos, src => src.Ignore()); 
		config.CreateMap<VideoCreateDTO, Video>();
		//   .ForMember(vid => vid.Id, src => src.Ignore())
		//.ForMember(vid => vid.Director, src => src.Ignore())
		//.ForMember(vid => vid.Genres, src => src.Ignore())
		//.ForMember(vid => vid.SimilarVideos, src => src.Ignore());

		config.CreateMap<VideoGenre, VideoGenreDTO>().ReverseMap();

		config.CreateMap<SimilarVideo, SimilarVideoDTO>().ReverseMap();

		config.CreateMap<Director, DirectorDTO>().ReverseMap();
		config.CreateMap<Director, DirectorFullDTO>().ReverseMap();
		config.CreateMap<DirectorCreateDTO, Director>().ForMember(dir => dir.Id, src => src.Ignore()).ForMember(dir => dir.Videos, src => src.Ignore());

		config.CreateMap<Genre, GenreDTO>().ReverseMap();
		config.CreateMap<Genre, GenreFullDTO>().ReverseMap();
		config.CreateMap<GenreCreateDTO, Genre>().ForMember(g => g.Id, src => src.Ignore()).ForMember(g => g.Videos, src => src.Ignore());

	});
	//mapConfig.AssertConfigurationIsValid();
	var mapper = mapConfig.CreateMapper();
	builder.Services.AddSingleton(mapper);
}