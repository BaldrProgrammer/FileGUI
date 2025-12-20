using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;

namespace FileGUI;

public partial class mainForm : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    private HttpClientHandler _handler;
    CookieContainer _cookies = new CookieContainer();
    private HttpClient _client;
    
    public mainForm(string token)
    {
        _cookies.SetCookies(_uri, $"access_token={token}; Path=/");
        _handler = new HttpClientHandler
        {
            CookieContainer = _cookies,
            UseCookies = true
        };
        _client = new HttpClient(_handler);
        InitializeComponent();
    }

    public void BeforeNodeExpand(object sender, TreeViewCancelEventArgs e)
    {
        TreeNode senderr = e.Node;

        List<string> files = GetFolderFiles();
        senderr.Nodes.Clear();
        foreach (string file in files)
        {
            senderr.Nodes.Add(new TreeNode(file));
        }
    }

    public List<string>? GetUserFiles()
    {
        if (true)
        {
            var c = _cookies.GetCookies(_uri);
            Console.WriteLine(c.Count);
            var response = _client
                .GetAsync(Url + "/users/files")
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            List<string> files = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                files = JsonSerializer.Deserialize<List<string>>(responseBody);
            }
            else
            {
                Console.WriteLine(responseBody, response.StatusCode);
            }
            return files;
        }
    }
    
    public List<string> GetFolderFiles()
    {
        var response = _client
            .GetAsync(Url + "/folders/items")
            .GetAwaiter()
            .GetResult();

        string responseBody = response.Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();
            
            Console.WriteLine(responseBody);
        
        List<string> files = new List<string> { "123.png", "auf.mp3", "virus.exe" };
        return files;
    }
}