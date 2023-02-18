namespace VV.Database.Entities;

public class VideoGenre : IReferenceEntity
{
	public int VideoId { get; set; }
	public int GenreId { get; set; }

	public virtual Video Video { get; set; }
	public virtual Genre Genre { get; set; }

}
