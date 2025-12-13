namespace FileGUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void LogIn(object sender, EventArgs e)
    {
        string username = usernameTxt.Text;
        string password = passwordTxt.Text;
        Console.Write(username + ' ');
        Console.Write(password);
    }
}