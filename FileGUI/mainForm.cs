using System.Net;
using System.Reflection.Metadata;

namespace FileGUI;

public partial class mainForm : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    private HttpClientHandler _handler;
    CookieContainer _cookies = new CookieContainer();
    
    public mainForm(string token)
    {
        _cookies.SetCookies(_uri, $"access_token={token}; Path=/");
        _handler = new HttpClientHandler
        {
            CookieContainer = _cookies,
            UseCookies = true
        };
        InitializeComponent();
    }

    public List<string> fsd()
    {
        using (var client = new HttpClient(_handler))
        {
            var c = _cookies.GetCookies(_uri);
            Console.WriteLine(c.Count);
            var response = client
                .GetAsync(Url + "/users/files")
                .GetAwaiter()
                .GetResult();

            string responseBody = response.Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();
            
            Console.WriteLine(responseBody);
        }
        
        List<string> numbers = new List<string> { "FISRTFILE.txt", "SECONDFILE.txt", "THIRDFILE.exe" };
        return numbers;
    }
}