using System.Net;
namespace FileGUI;

public partial class Main : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    CookieContainer _cookies = new CookieContainer();
    
    public Main(string token)
    {
        InitializeComponent();
        _cookies.SetCookies(_uri, $"access_token={token}; Path=/");
    }
}