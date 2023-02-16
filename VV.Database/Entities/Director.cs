
namespace VV.Database.Entities;

public class Director : IEntity
{
	public int Id { get; set; }
	[Required, MaxLength(100)]
	public string Name { get; set; } 
}
