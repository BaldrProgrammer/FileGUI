using System.Net;
namespace FileGUI;

public partial class mainForm : Form
{
    const string Url = "http://155.212.223.69:8000";
    private Uri _uri = new Uri(Url);
    CookieContainer _cookies = new CookieContainer();
    
    public mainForm(string token)
    {
        InitializeComponent();
        _cookies.SetCookies(_uri, $"access_token={token}; Path=/");
    }
}