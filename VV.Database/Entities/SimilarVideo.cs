
using System.Text.Json.Serialization;

namespace VV.Database.Entities;

public class SimilarVideo : IReferenceEntity
{
	public int VideoId { get; set; }
	public int SimilarVideoId { get; set; }

	public virtual Video Video { get; set; } = null!;
	[ForeignKey("SimilarVideoId")]
	public virtual Video Similar { get; set; } = null!;
}
