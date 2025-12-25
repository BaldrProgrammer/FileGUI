class InputPopup : Form
{
    public string ResultText => textBox.Text;

    private Label label;
    private TextBox textBox;
    private Button submit;

    public InputPopup()
    {
        label = new Label();
        label.Text = "Provide Folder name";
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
        
        Controls.AddRange(
            label, textBox, submit
        );
        
        Size = new Size(300, 200
            );
        Deactivate += (s, e) => Close();
    }
}