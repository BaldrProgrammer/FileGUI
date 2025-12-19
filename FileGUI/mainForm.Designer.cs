using System.ComponentModel;

namespace FileGUI;

partial class mainForm
{
    private IContainer components = null;
    private TreeView treeView1;
    
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
        treeView1.Location = new Point(0, 0);
        treeView1.Size = new Size(800, 800);
        
        List<string> user_files = GetUserFiles();
        for (int i = 0; i < user_files.Count; i++)
        {
            treeView1.Nodes.Add(new TreeNode(user_files[i]));
        }
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.treeView1
        });
        
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1600, 800);
        this.Text = "Main";
    }
}