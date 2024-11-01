using System.Text.Json.Serialization;

namespace BlogApi.Models;

public class Post
{
    public  int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Conteudo { get; set; }
    public int BlogId { get; set; }
    [JsonIgnore]
    public Blog Blog { get; set; } = null!;
}
