namespace VV.Database.Entities;

public class Video : IEntity
{
	public int Id { get; set; }
	[Required, MaxLength(100)]
	public string Title { get; set; }
	[MaxLength(1000)]
	public string Description { get; set; }
	[Required, MaxLength(1000)]
	public string Url { get; set; }
	[Required, MaxLength(1000)]
	public string ThumbnailUrl { get; set; }
	public DateTime Released { get; set; }
	public bool Free { get; set; } = true;
	public int? DirectorId { get; set; }
	public virtual Director Director { get; set; }
	public virtual ICollection<Genre> Genres { get; set; }
	public virtual ICollection<SimilarVideo> SimilarVideos { get; set; }

}
