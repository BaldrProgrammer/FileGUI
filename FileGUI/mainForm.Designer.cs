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
        TreeView treeView1 = new TreeView();
        
        this.Controls.AddRange(new System.Windows.Forms.Control[]
        {
            this.treeView1
        });
        
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Main";
    }
}