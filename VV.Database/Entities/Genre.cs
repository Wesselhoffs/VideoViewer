namespace VV.Database.Entities;

public class Genre : IEntity
{
	public Genre()
	{
		Videos = new HashSet<Video>();
	}
	public int Id { get; set; }
	[Required, MaxLength(50)]
	public string Name { get; set; }

	public ICollection<Video> Videos { get; set; }
}
