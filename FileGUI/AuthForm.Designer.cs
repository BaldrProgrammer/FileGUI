namespace FileGUI;

partial class AuthForm
{
    private System.ComponentModel.IContainer components = null;
    
    private System.Windows.Forms.Panel loginPnl;
    private System.Windows.Forms.Label loginLbl;
    private System.Windows.Forms.TextBox usernameTxt;
    private System.Windows.Forms.TextBox passwordTxt;
    private System.Windows.Forms.Button sumbitBtn;
    private System.Windows.Forms.Label logtoregSwitch;
    
    private System.Windows.Forms.Panel regPnl;
    private System.Windows.Forms.Label regLbl;
    private System.Windows.Forms.TextBox regUsernameTxt;
    private System.Windows.Forms.TextBox regPasswordTxt;
    private System.Windows.Forms.Button regSumbitBtn;
    private System.Windows.Forms.Label regtologSwitch;

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
        loginLbl.Text = "Авторизация";
        loginLbl.Size = new Size(150, 40);
        loginLbl.Location = new Point(25, 10);
        loginLbl.Font = new Font("Times New Roman", 14);
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

        logtoregSwitch = new Label();
        logtoregSwitch.Text = "Нет аккаунта?";
        logtoregSwitch.Size = new Size(150, 20);
        logtoregSwitch.Location = new Point(20, 210);
        logtoregSwitch.ForeColor = Color.FromArgb(6, 69, 173);
        logtoregSwitch.Cursor = Cursors.Hand;
        logtoregSwitch.Font = new Font("Times New Roman", 12);
        logtoregSwitch.Click += ChangePanel;
        loginPnl.Controls.Add(logtoregSwitch);
        
        regPnl = new Panel();
        regPnl.Size = new Size(200, 250);
        regPnl.Location = new Point(300, 75);
        regPnl.BackColor = Color.White;
        
        regLbl = new Label();
        regLbl.Text = "Регистрация";
        regLbl.Size = new Size(150, 40);
        regLbl.Location = new Point(25, 10);
        regLbl.Font = new Font("Times New Roman", 14);
        regPnl.Controls.Add(regLbl);
        
        regUsernameTxt = new TextBox();
        regUsernameTxt.Size = new Size(150, 25);
        regUsernameTxt.Location = new Point(25, 50);
        regPnl.Controls.Add(regUsernameTxt);
        
        regPasswordTxt = new TextBox();
        regPasswordTxt.Size = new Size(150, 25);
        regPasswordTxt.Location = new Point(25, 100);
        regPnl.Controls.Add(regPasswordTxt);
        
        regSumbitBtn = new Button();
        regSumbitBtn.Text = "Создать";
        regSumbitBtn.Size = new Size(150, 50);
        regSumbitBtn.Location = new Point(25, 150);
        regSumbitBtn.Font = new Font("Times New Roman", 16);
        regSumbitBtn.Click += Register;
        regPnl.Controls.Add(regSumbitBtn);
        
        regtologSwitch = new Label();
        regtologSwitch.Text = "Есть аккаунт?";
        regtologSwitch.Size = new Size(150, 20);
        regtologSwitch.Location = new Point(20, 210);
        regtologSwitch.ForeColor = Color.FromArgb(6, 69, 173);
        regtologSwitch.Cursor = Cursors.Hand;
        regtologSwitch.Font = new Font("Times New Roman", 12);
        regtologSwitch.Click += ChangePanel;
        regPnl.Controls.Add(regtologSwitch);
        
        regPnl.Hide();
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.loginPnl, this.regPnl
        });
        
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 500);
        BackColor = Color.FromArgb(200, 200, 200);
        Text = "Авторизация";
    }
}