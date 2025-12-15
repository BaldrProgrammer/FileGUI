namespace FileGUI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    
    private System.Windows.Forms.Panel loginPnl;
    private System.Windows.Forms.Label loginLbl;
    private System.Windows.Forms.TextBox usernameTxt;
    private System.Windows.Forms.TextBox passwordTxt;
    private System.Windows.Forms.Button sumbitBtn;

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
        loginPnl.Size = new Size(200, 250);
        loginPnl.Location = new Point(300, 75);
        loginPnl.BackColor = Color.White;
        
        loginLbl = new Label();
        loginLbl.Text = "LOGIN";
        loginLbl.Size = new Size(100, 50);
        loginLbl.Location = new Point(25, 10);
        loginLbl.Font = new Font("Times New Roman", 15);
        loginPnl.Controls.Add(loginLbl);
        
        usernameTxt = new TextBox();
        usernameTxt.Size = new Size(150, 25);
        usernameTxt.Location = new Point(25, 50);
        loginPnl.Controls.Add(usernameTxt);
        
        passwordTxt = new TextBox();
        passwordTxt.Size = new Size(150, 25);
        passwordTxt.Location = new Point(25, 100);
        loginPnl.Controls.Add(passwordTxt);
        
        sumbitBtn = new Button();
        sumbitBtn.Text = "Войти";
        sumbitBtn.Size = new Size(150, 50);
        sumbitBtn.Location = new Point(25, 150);
        sumbitBtn.Font = new Font("Times New Roman", 16);
        sumbitBtn.Click += LogIn;
        loginPnl.Controls.Add(sumbitBtn);
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.loginPnl
        });
        
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 500);
        BackColor = Color.FromArgb(200, 200, 200);
        Text = "Авторизация";
    }
}