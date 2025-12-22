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
        treeView1.BeforeExpand += BeforeNodeExpand;
        
        foreach (string file in GetUserFiles())
        {
            if (!file.Contains("."))
            {
                TreeNode parentNode = new TreeNode(file);
                parentNode.Nodes.Add(new TreeNode("..."));
            
                treeView1.Nodes.Add(parentNode);
            }
            else
            {
                treeView1.Nodes.Add(file);
            }
        }
        
        ContextMenuStrip menu = new ContextMenuStrip();

        ToolStripMenuItem mkDir = new ToolStripMenuItem("Создать папку");
        ToolStripMenuItem uploadFile = new ToolStripMenuItem("Выгрузить файл");
        ToolStripMenuItem renameObj = new ToolStripMenuItem("Переименовать");
        ToolStripMenuItem removeObj = new ToolStripMenuItem("Удалить");
        
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