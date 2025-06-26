namespace aspnetcore_identity_example.Models;

public class Post
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class CreatePostDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
}
