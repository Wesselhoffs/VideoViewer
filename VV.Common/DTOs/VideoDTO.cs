using System.ComponentModel.DataAnnotations;

namespace VV.Common.DTOs;

public class VideoDTO
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public string ThumbnailUrl { get; set; }
	public DateTime Released { get; set; }
	public bool Free { get; set; } = true;
	public int DirectorId { get; set; }
	public virtual DirectorDTO? Director { get; set; }
	public virtual List<GenreDTO> Genres { get; set; } = new();
	public virtual List<SimilarVideoDTO> SimilarVideos { get; set; } = new();
}

public class VideoCreateDTO
{
	public string Title { get; set; }
	public string Description { get; set; }
	public string Url { get; set; }
	public string ThumbnailUrl { get; set; }
	public DateTime Released { get; set; }
	public bool Free { get; set; } = true;
	public int? DirectorId { get; set; }
	public List<int> GenreId { get; set; } = new();
}

public class VideoEditDTO : VideoCreateDTO
{
	public int Id { get; set; }
}
