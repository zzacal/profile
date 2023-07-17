using Microsoft.AspNetCore.Mvc;
using Profile.Blog;

namespace Profile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<BlogController> _logger;
    private readonly BlogService _blogs;

    public BlogController(ILogger<BlogController> logger, BlogService blogs)
    {
        _logger = logger;
        _blogs = blogs;
    }

    [HttpGet]
    public async Task<BlogPage> Get()
    {
        return await _blogs.GetBlogPage();
    }
}
