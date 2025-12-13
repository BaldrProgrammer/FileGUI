namespace FileGUI;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    
    private System.Windows.Forms.TreeView treeView1;

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
        treeView1 = new TreeView();
        treeView1.DoubleClick += NodeClick;
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.treeView1,
        });
        
        components = new System.ComponentModel.Container();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Text = "File Manager";
    }

}