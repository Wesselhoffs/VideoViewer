namespace VV.Database.Contexts;

public class VVContext : DbContext
{
	public virtual DbSet<Video> Videos { get; set; } = null!;
	public virtual DbSet<SimilarVideo> SimilarVideos { get; set; } = null!;
	public virtual DbSet<Director> Directors { get; set; } = null!;
	public virtual DbSet<Genre> Genres { get; set; } = null!;
	public virtual DbSet<VideoGenre> VideoGenres { get; set; } = null!;

	public VVContext(DbContextOptions<VVContext> options) : base(options) { }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<SimilarVideo>().HasKey(ci => new { ci.VideoId, ci.SimilarVideoId });
		modelBuilder.Entity<VideoGenre>().HasKey(ci => new { ci.VideoId, ci.GenreId });

		modelBuilder.Entity<Video>(entity =>
		{
			entity
				.HasMany(d => d.SimilarVideos)
				.WithOne(p => p.Video)
				.HasForeignKey(d => d.VideoId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasMany(d => d.Genres)
				  .WithMany(p => p.Videos)
				  .UsingEntity<VideoGenre>()
				  .ToTable("VideoGenres");
		});
	}
}

