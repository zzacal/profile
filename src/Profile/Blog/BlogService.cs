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

    public async Task<BlogPage> GetBlogPage(string path)
    {
        var list = await GetBlogList(path);
        if(list.FirstOrDefault() is BlogMeta meta) {
            var content = await GetContent(list.FirstOrDefault()?.Path ?? "");
            var blog = ToBlog(content, meta);
            return new BlogPage(Blog: blog ?? new Blog(Title: "Not Found", Body: "Could not find the blog", Published: null), List: list);
        };

        return new BlogPage(
            Blog: new Blog("", ""),
            List: list
        );
    }

    public async Task<string> GetContent(string path)
    {
        return await _github.GetContent("zzacal", "blog", "main", path);
    }

    public async Task<IEnumerable<BlogMeta>> GetBlogList(string path)
    {
        var nodes = await _github.GetTree("zzacal", "blog", "main", true, $"blog/{path}", ".md");
        return nodes.Select(n => ToBlogMeta(n));
    }

    private BlogMeta ToBlogMeta(Node node)
    {
        var name = Regex.Match(node.Path, @"[\w-]*\.(md)", RegexOptions.IgnoreCase).Value;
        var date = Regex.Match(node.Path, @"[0-9]{4}/[0-9]{2}/[0-9]{2}").Value;
        return new BlogMeta(Sha: node.Sha, Path: node.Path, Url: node.Url, Name: name, Date: date);
    }

    private Blog ToBlog(string body, BlogMeta meta)
    {
        return new Blog(Title: "We'll figure it out", Body: body, Published: DateOnly.FromDateTime(DateTime.Now));
    }
}
public record BlogPage(Blog Blog, IEnumerable<BlogMeta> List);
public record BlogMeta(string Sha, string Path, string Date, string Name, string Url);
public record Blog(string Title, string Body, DateOnly? Published = null);
