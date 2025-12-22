using System.Net;
using System.Text;
using System.Text.Json;
using FileGUI.DTO.Folders;

namespace FileGUI;

public partial class mainForm : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    private HttpClientHandler _handler;
    private HttpClient _client;
    CookieContainer _cookies = new CookieContainer();
    
    public mainForm(string token)
    {
        Console.OutputEncoding = Encoding.UTF8;
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
        Console.WriteLine(senderr.Level);
        List<string> files = GetFolderFiles(senderr.FullPath.Replace("\\", "/"));
        senderr.Nodes.Clear();
        foreach (string file in files)
        {
            if (!file.Contains("."))
            {
                TreeNode parentNode = new TreeNode(file);
                parentNode.Nodes.Add(new TreeNode("..."));
            
                senderr.Nodes.Add(parentNode);
            }
            else
            {
                senderr.Nodes.Add(new TreeNode(file));
            }
        }
    }
    
    public void MenuShow(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            treeView1.SelectedNode = e.Node;
            menu.Show(Cursor.Position);
        }
    }

    public void MenuButtonClick(object sender, EventArgs e)
    {
        ToolStripMenuItem senderr = sender as ToolStripMenuItem;
        Console.WriteLine(senderr.Text);
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
    
    public List<string> GetFolderFiles(string folder)
    {
        var response = _client
            .GetAsync(Url + $"/folders/items?filter_value={folder}&filter_type=name")
            .GetAwaiter()
            .GetResult();

        string responseBody = response.Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();
            
            Console.WriteLine(responseBody);
        
        FoldersItemsResponseDto? dto = JsonSerializer.Deserialize<FoldersItemsResponseDto>(responseBody);
        List<string> files = dto.content;
        
        return files;
    }
}