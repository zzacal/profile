using System.Text;
using System.Text.RegularExpressions;
using Profile.Github;

namespace Profile.Blog;

public class BlogService
{
    GithubService _github;
    public BlogService(GithubService github)
    {
        _github = github;
    }

    public async Task<BlogPage> GetBlogPage(string? sha = null)
    {
        var list = await GetBlogList();
        var blog = await GetBlog(sha ?? list.FirstOrDefault()?.Path ?? "");
        return new BlogPage(Blog: blog ?? new Blog(Title: "Not Found", Body: "Could not find the blog", Published: null), List: list);
    }

    public async Task<Blog?> GetBlog(string path)
    {
        return await _github.GetBlob("zzacal", "blog", "main", path) switch {
            Blob blob => ToBlog(blob),
            _ => null
        };
    }

    public async Task<IEnumerable<BlogMeta>> GetBlogList()
    {
        var nodes = await _github.GetTree("zzacal", "blog", "main", true, "entries", ".md");
        return nodes.Select(n => ToBlogMeta(n));
    }

    private BlogMeta ToBlogMeta(Node node)
    {
        var name = Regex.Match(node.Path, @"[\w-]*\.(md)", RegexOptions.IgnoreCase).Value;
        var date = Regex.Match(node.Path, @"[0-9]{4}/[0-9]{2}/[0-9]{2}").Value;
        return new BlogMeta(Sha: node.Sha, Path: node.Path, Url: node.Url, Name: name, Date: date);
    }

    private Blog ToBlog(Blob blob)
    {
        return new Blog(Title: "We'll figure it out", Body: System.Text.Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(blob.Content)), Published: DateOnly.FromDateTime(DateTime.Now));
    }
}
public record BlogPage(Blog Blog, IEnumerable<BlogMeta> List);
public record BlogMeta(string Sha, string Path, string Date, string Name, string Url);
public record Blog(string Title, string Body, DateOnly? Published);
