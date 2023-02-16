namespace VV.Database.Entities;

public class VideoGenre : IReferenceEntity
{
	public int VideoId { get; set; }
	public int GenreId { get; set; }

}
