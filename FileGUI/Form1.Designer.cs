namespace FileGUI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    
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
        usernameTxt = new TextBox();
        usernameTxt.Size = new Size(100, 20);
        usernameTxt.Location = new Point(100, 50);
        
        passwordTxt = new TextBox();
        passwordTxt.Size = new Size(100, 20);
        passwordTxt.Location = new Point(100, 100);
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.usernameTxt, this.passwordTxt
        });
        
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 225);
        Text = "Авторизация";
    }

}