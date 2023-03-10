namespace VV.Common.DTOs;

public class DirectorDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
}
public class DirectorCreateDTO
{
	public string Name { get; set; }
}

public class DirectorFullDTO : DirectorDTO
{
	public virtual List<VideoDTO> Videos { get; set; } = new();
}
