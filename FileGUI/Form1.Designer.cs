namespace FileGUI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    
    private System.Windows.Forms.Panel loginPnl;
    private System.Windows.Forms.TextBox usernameTxt;
    private System.Windows.Forms.TextBox passwordTxt;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
    
    private void InitializeComponent()
    {
        loginPnl = new Panel();
        loginPnl.Size = new Size(400, 225);
        loginPnl.BackColor = Color.White;
        
        usernameTxt = new TextBox();
        usernameTxt.Size = new Size(100, 20);
        usernameTxt.Location = new Point(100, 50);
        loginPnl.Controls.Add(usernameTxt);
        
        passwordTxt = new TextBox();
        passwordTxt.Size = new Size(100, 20);
        passwordTxt.Location = new Point(100, 100);
        loginPnl.Controls.Add(usernameTxt);
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.loginPnl
        });
        
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        BackColor = Color.FromArgb(200, 200, 200);
        Text = "Авторизация";
    }

}