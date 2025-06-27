using aspnetcore_identity_example.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore_identity_example.Controllers;

[Route("[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {
        return await _context.Posts.ToListAsync();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Post>> CreatePost(CreatePostDto newPostPayload)
    {
        Post post = new Post
        {
            Id = Guid.NewGuid().ToString(),
            Title = newPostPayload.Title,
            Description = newPostPayload.Description,
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAll", new { id = post.Id }, post);
    }
}
