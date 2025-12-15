using System.Text;
using System.Text.Json;
namespace FileGUI;

public partial class Form1 : Form
{
    const string URL = "http://155.212.223.69:8000";
    private string cookies = "";
    
    public Form1()
    {
        InitializeComponent();
    }

    private void LogIn(object sender, EventArgs e)
    {
        string username = usernameTxt.Text;
        string password = passwordTxt.Text;
        
        var body = new
        {
            username,
            password,
            is_admin = false
        };

        string json = JsonSerializer.Serialize(body);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpClient client = new HttpClient();
        
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        
        var response = client
            .PostAsync(URL + "/auth/log", content)
            .GetAwaiter()
            .GetResult();

        string responseBody = response.Content
            .ReadAsStringAsync()
            .GetAwaiter()
            .GetResult();

        Console.WriteLine(responseBody);
    }
}