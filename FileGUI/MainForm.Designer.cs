using System.ComponentModel;
using System.Diagnostics;

namespace FileGUI;

partial class MainForm
{
    private IContainer components = null;
    private TreeView treeView1;
    private ContextMenuStrip menu;
    
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
        treeView1.NodeMouseClick += MenuShow;
        treeView1.NodeMouseDoubleClick += FileRun;
        
        foreach (string file in GetUserFiles())
        {
            
            treeView1.Nodes.Add(file);
        }
        
        menu = new ContextMenuStrip();

        ToolStripMenuItem mkDir = new ToolStripMenuItem("Создать папку");
        ToolStripMenuItem uploadFile = new ToolStripMenuItem("Выгрузить файл");
        ToolStripMenuItem renameObj = new ToolStripMenuItem("Переименовать");
        ToolStripMenuItem removeObj = new ToolStripMenuItem("Удалить");

        mkDir.Click += MenuButtonClick;
        uploadFile.Click += MenuButtonClick;
        renameObj.Click += MenuButtonClick;
        removeObj.Click += MenuButtonClick;
        
        menu.Items.Add(mkDir);
        menu.Items.Add(uploadFile);
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add(renameObj);
        menu.Items.Add(removeObj);
        
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