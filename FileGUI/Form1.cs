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
        
        
    }
}