namespace Profile.Github;

public class GithubService
{
    HttpClient _apiClient;
    HttpClient _contentClient;
    public GithubService(IHttpClientFactory httpClientFactory)
    {
        _apiClient = httpClientFactory.CreateClient("GithubApiClient");
        _apiClient.BaseAddress = new Uri("https://api.github.com/");
        _apiClient.DefaultRequestHeaders.Add("User-Agent", "zzacal");

        _contentClient = httpClientFactory.CreateClient("GithubContentClient");
        _contentClient.BaseAddress = new Uri("https://raw.githubusercontent.com/");
        _contentClient.DefaultRequestHeaders.Add("User-Agent", "zzacal");
    }

    public async Task<IEnumerable<Node>> GetTree(string owner, string repo, string branch, bool recursive, string path, string type)
    {
        var response = await _apiClient.GetFromJsonAsync<GetTreeResponse>($"repos/{owner}/{repo}/git/trees/{branch}?recursive=true");

        return response?.Tree
            .Where(n => string.IsNullOrWhiteSpace(path) || n.Path.StartsWith(path))
            .Where(n => string.IsNullOrWhiteSpace(type) || n.Path.EndsWith(type, StringComparison.OrdinalIgnoreCase))
            ?? Array.Empty<Node>();
    }

    public async Task<Blob?> GetBlob(string owner, string repo, string branch, string path)
    {
        return await _apiClient.GetFromJsonAsync<Blob>($"repos/{owner}/{repo}/contents/{path}?ref={branch}");
    }

    public async Task<string> GetContent(string owner, string repo, string branch, string path)
    {
        return await _contentClient.GetStringAsync($"{owner}/{repo}/{branch}/{path}");
    }
}

public record Blob(string Sha, int Size, string Url, string Content, string Encoding);

public record Node(
    string Path,
    string Mode,
    string Type,
    string Sha,
    int Size,
    string Url);

public record GetTreeResponse(
    string Sha,
    string Url,
    List<Node> Tree);
