using System.Net;
using System.Reflection.Metadata;

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

    public List<string> fsd()
    {
        List<string> numbers = new List<string> { "FISRTFILE.txt", "SECONDFILE.txt", "THIRDFILE.exe" };
        return numbers;
    }
}