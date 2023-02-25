namespace VV.Common.DTOs;

public class GenreDTO
{
	public int Id { get; set; }
	public string Name { get; set; }

}
public class GenreCreateDTO
{
	public string Name { get; set; }
}

public class GenreFullDTO : GenreDTO
{
	public virtual List<VideoDTO> Videos { get; set; } = new();
}
