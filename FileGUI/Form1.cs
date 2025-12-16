using System.Net;
using System.Text;
using System.Text.Json;
using FileGUI.DTO.Auth;
namespace FileGUI;

public partial class Form1 : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    CookieContainer _cookies = new CookieContainer();
    
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

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        
            var response = client
                .PostAsync(Url + "/auth/log", content)
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();
            
            LoginResponseDto? dto = JsonSerializer.Deserialize<LoginResponseDto>(responseBody);
            if (dto?.access_token != null)
            {
                _cookies.SetCookies(_uri, $"access_token={dto.access_token}; Path=/");
            }
            Console.WriteLine(_cookies.GetCookies(_uri).Count);
        }
    }

    private void Register(object sender, EventArgs e)
    {
        string username = regUsernameTxt.Text;
        string password = regPasswordTxt.Text;

        var body = new
        {
            username,
            hashed_password=password,
            is_admin = false
        };
        
        string json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = client
                .PostAsync(Url + "/auth/register", content)
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();
            
            Console.WriteLine(responseBody);
        }
    }
}