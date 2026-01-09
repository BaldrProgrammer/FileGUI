using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using FileGUI.DTO.Auth;
using FileGUI.DTO.Folders;

namespace FileGUI;

public partial class MainForm : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    private HttpClientHandler _handler;
    private HttpClient _client;
    CookieContainer _cookies = new CookieContainer();
    
    public MainForm(string token)
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
        if (senderr.Tag != "mainnode")
        {
            string fsObjectPath = string.Join("/", senderr.FullPath.Replace("\\", "/").Split('/').Skip(1));
            List<string> files = GetFolderFiles(fsObjectPath);
            senderr.Nodes.Clear();
            foreach (string file in files)
            {
                if (!file.Contains("."))
                {
                    TreeNode parentNode = new TreeNode(file);
                    parentNode.Nodes.Add(new TreeNode(""));
            
                    senderr.Nodes.Add(parentNode);
                }
                else
                {
                    senderr.Nodes.Add(new TreeNode(file));
                }
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
        var nodeSender = treeView1.SelectedNode;
        Console.WriteLine(senderr.Text);
        if (senderr.Text == "Создать папку" && !nodeSender.Text.Contains("."))
        {
            CreateFolder(nodeSender);
        }
        else if (senderr.Text == "Создать файл" && !nodeSender.Text.Contains("."))
        {
            CreateFile(nodeSender);
        }
        else if (senderr.Text == "Выгрузить файл" && !nodeSender.Text.Contains("."))
        {
            UploadFile(nodeSender);
        }
        else if (senderr.Text == "Переименовать")
        {
            Rename(nodeSender);
        }
        else if (senderr.Text == "Удалить")
        {
            Remove(nodeSender);
        }
    }

    public void CreateFolder(TreeNode nodeSender)
    {
        using var popup = new InputPopup();
        popup.Location = Cursor.Position;
        popup.ShowDialog();

        string path = nodeSender.Parent?.FullPath.Replace("\\", "/") ?? "";
        string fsObjectPath = string.Join("/", path.Split("/").Skip(1));
        var response = _client
            .PostAsync(
                Url + $"/folders/mkdir?folder_path={fsObjectPath}{popup.ResultText.Replace(" ", "+")}", new StringContent(""))
            .GetAwaiter()
            .GetResult();

        if (response.IsSuccessStatusCode)
        {
            TreeNode node = new TreeNode(popup.ResultText);
            node.Nodes.Add(new TreeNode());
            nodeSender.Nodes.Add(node);
        }
    }

    public void CreateFile(TreeNode nodeSender)
    {
        using var popup = new FileCreatePopup();
        popup.Location = Cursor.Position;
        popup.ShowDialog();

        var response = _client
            .PostAsync(
                Url + $"/files/touch?filepath={(nodeSender.Tag != "mainnode" ? nodeSender.FullPath.Replace("\\", "/")+"/" : "")}{popup.ResultText.Replace(" ", "+")}", new StringContent(""))
            .GetAwaiter()
            .GetResult();
        
        if (response.IsSuccessStatusCode)
        {
            TreeNode node = new TreeNode(popup.ResultText);
            node.Nodes.Add(new TreeNode());
            nodeSender.Nodes.Add(node);
        }
    }

    public void UploadFile(TreeNode nodeSender)
    {
        using (OpenFileDialog fd = new OpenFileDialog())
        {
            fd.Multiselect = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string[] files = fd.FileNames;
                foreach (string file in files)
                {
                    using (FileStream stream = File.OpenRead(file))
                    {
                        MultipartFormDataContent content = new MultipartFormDataContent();
                        content.Add(new StreamContent(stream), "uploaded_files", Path.GetFileName(file));

                        string path = nodeSender.Parent?.FullPath.Replace("\\", "/") ?? ".";
                        string fsObjectPath = string.Join("/", path.Split('/').Skip(1));
                        var request = new HttpRequestMessage(HttpMethod.Post, Url + $"/files/?folder={fsObjectPath}")
                        {
                            Content = content
                        };
                        var response = _client.SendAsync(request).GetAwaiter().GetResult();

                        if (response.IsSuccessStatusCode)
                        {
                            TreeNode node = new TreeNode(Path.GetFileName(file));
                            nodeSender.Nodes.Add(node);
                        }
                    }
                }
            }
        }
    }

    public void Rename(TreeNode nodeSender)
    {
        if (nodeSender.Tag != "mainnode")
        {
            using var popup = new RenamePopup();
            popup.Location = Cursor.Position;
            popup.ShowDialog();
            HttpResponseMessage response;

            if (!nodeSender.Text.Contains("."))
            {
                string path = nodeSender.Parent?.FullPath.Replace("\\", "/") ?? "";
                string fsObjectPath = string.Join("/", path.Split('/').Skip(1));
                string fsOldObjectPath = string.Join("/", nodeSender.FullPath.Replace("\\", "/").Split('/').Skip(1));
                string UrlNew = Url +
                                $"/folders/ren?old_path={fsOldObjectPath}&new_path={(string.IsNullOrEmpty(fsObjectPath) ? fsObjectPath : fsObjectPath + "/") + popup.ResultText}"
                                    .Replace(" ", "+");
                response = _client
                    .PatchAsync(UrlNew, new StringContent(""))
                    .GetAwaiter()
                    .GetResult();
            }
            else
            {
                string path = nodeSender.Parent?.FullPath.Replace("\\", "/") ?? "";
                string fsObjectPath = string.Join("/", path.Split('/').Skip(1));
                string fsOldObjectPath = string.Join("/", nodeSender.FullPath.Replace("\\", "/").Split('/').Skip(1));
                string UrlNew = Url +
                                $"/files/ren?filter_value={fsOldObjectPath}&filter_type=name&newname={(string.IsNullOrEmpty(fsObjectPath) ? fsObjectPath : fsObjectPath + "/") + popup.ResultText}"
                                    .Replace(" ", "+");
                response = _client
                    .PatchAsync(UrlNew, new StringContent(""))
                    .GetAwaiter()
                    .GetResult();
            }

            if (response.IsSuccessStatusCode)
            {
                nodeSender.Text = popup.ResultText;
            }
        }
    }

    public void Remove(TreeNode nodeSender)
    {
        if (nodeSender.Tag != "mainnode")
        {
            if (!nodeSender.Text.Contains("."))
            {
                string fsObjectPath = string.Join("/", nodeSender.FullPath.Replace("\\", "/").Split('/').Skip(1));
                var response = _client
                    .DeleteAsync(Url+$"/folders/rmdir?folder_path={fsObjectPath}&hard=false")
                    .GetAwaiter()
                    .GetResult();
            
                if (response.IsSuccessStatusCode)
                {
                    nodeSender.Remove();
                }
            }
            else
            {
                string fsObjectPath = string.Join("/", nodeSender.FullPath.Replace("\\", "/").Split('/').Skip(1));
                var files = new[] { fsObjectPath };
                var json = JsonSerializer.Serialize(files);

                var request = new HttpRequestMessage(HttpMethod.Delete, Url+"/files/remove?filter_type=name")
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                var response = _client.SendAsync(request).GetAwaiter().GetResult();

                Console.WriteLine(response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    nodeSender.Remove();
                }
            }
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

    public void FileRun(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node.Text.Contains("."))
        {
            string fsObjectPath = string.Join("/", e.Node.FullPath.Replace("\\", "/").Split('/').Skip(1));
            byte[] data = _client.GetByteArrayAsync(Url + $"/files/content/?filter_value={fsObjectPath}&filter_type=name").Result;
            File.WriteAllBytes(Directory.GetCurrentDirectory()+$"/temp/{e.Node.Text}", data);
        
            var psi = new ProcessStartInfo
            {
                FileName = Directory.GetCurrentDirectory()+$@"\temp\{e.Node.Text}",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
    
    public UserGetDto GetUser()
    {
        var response = _client
            .GetAsync(Url + "/users/current")
            .GetAwaiter()
            .GetResult();
        
        string responseBody = response.Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();

        UserGetDto? dto = JsonSerializer.Deserialize<UserGetDto>(responseBody);
        
        if (dto != null)
        {
            return dto;
        }
        return new UserGetDto();
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