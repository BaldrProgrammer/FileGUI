namespace FileGUI;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void NodeClick(object sender, EventArgs e)
    {
        TreeView? senderr = sender as TreeView;
        Console.WriteLine(senderr.SelectedNode.FullPath);
    }
}