class FileCreatePopup : Form
{
    private Label label;
    private TextBox textBox;
    private Button submit;

    public FileCreatePopup()
    {
        label = new Label();
        label.Text = "Provide File name";
        label.Location = new Point(50, 10);
        label.Font = new Font("Times New Roman", 12);
        label.AutoSize = true;
        
        textBox = new TextBox();
        textBox.Location = new Point(50, 60);
        textBox.Size = new Size(175, 30);
        textBox.Font = new Font("Times New Roman", 12);
        
        submit = new Button();
        submit.Text = "OK";
        submit.Location = new Point(75, 100);
        submit.Size = new Size(125, 30);
        submit.Font = new Font("Times New Roman", 12);
        
        submit.Click += (s, e) => Close();
    }
}