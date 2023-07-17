using Profile.Github;

namespace Profile.Blog;

public class BlogService
{
    GithubService _github;
    public BlogService(GithubService github)
    {
        _github = github;
    }

    public async Task<IEnumerable<BlogMeta>> GetBlogList()
    {
        var nodes = await _github.GetTree("zzacal", "blog", "main", true, "entries", ".md");
        return nodes.Select(n => new BlogMeta(Path: n.Path, Url: n.Url));
    }
}

public record BlogMeta(string Path, string Url);
public record Blog(string Title, string Body, DateTime Published);
