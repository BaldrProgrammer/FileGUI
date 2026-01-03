using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TodoApp
{
    public partial class Form1 : Form
    {
        private List<Task> tasks;
        public bool? sortin = null;

        public Form1()
        {
            InitializeComponent();
            tasks = new List<Task>();
            LoadTasksFromFile();
            RefreshTasksList();
            UpdateStats();
        }

        private void LoadTasksFromFile()
        {
            tasks = FileManager.LoadTasks();
        }

        private void SaveTasksToFile()
        {
            FileManager.SaveTasks(tasks);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewTask();
        }

        private void txtNewTask_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // zapobiega dźwiękowi "beep"
                AddNewTask();
            }
        }

        private void MainUpdate(object sender, EventArgs e)
        {
            RefreshTasksList();
            UpdateStats();
            SaveTasksToFile();
        }

        private void AddNewTask()
        {
            string description = txtNewTask.Text.Trim();
            string category = cmbCategory.SelectedItem?.ToString() ?? "Ogólne";

            if (!string.IsNullOrEmpty(description))
            {
                Task newTask = new Task(description, category);
                tasks.Add(newTask);
                txtNewTask.Clear();
                RefreshTasksList();
                UpdateStats();
                SaveTasksToFile();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItems.Count > 0)
            {
                var selectedIndices = lstTasks.SelectedIndices.Cast<int>()
                                         .OrderByDescending(i => i)
                                         .ToList();

                foreach (int index in selectedIndices)
                {
                    tasks.RemoveAt(index);
                }

                RefreshTasksList();
                UpdateStats();
                SaveTasksToFile();
            }
        }

        private void btnToggleComplete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItems.Count > 0)
            {
                var selectedIndices = lstTasks.SelectedIndices.Cast<int>().ToList();

                foreach (int index in selectedIndices)
                {
                    tasks[index].IsCompleted = !tasks[index].IsCompleted;
                }

                RefreshTasksList();
                UpdateStats();
                SaveTasksToFile();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy na pewno chcesz usunąć wszystkie zadania?",
                                        "Potwierdzenie",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                tasks.Clear();
                RefreshTasksList();
                UpdateStats();
                SaveTasksToFile();
            }
        }
        
        private void rbtnFilter_Click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                if (rb.Text == "Wszystkie")
                {
                    sortin = null;
                }
                else if (rb.Text == "Aktywne")
                {
                    sortin = true;
                }
                else if (rb.Text == "Wykonane")
                {
                    sortin = false;
                }
                RefreshTasksList();
                UpdateStats();
                SaveTasksToFile();
            }
        }

        private void open_wondow(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                Form2 child = new Form2();
                child.ShowDialog();
                var selectedIndices = lstTasks.SelectedIndices.Cast<int>().ToList();
                foreach (int i in selectedIndices)
                {
                    tasks[i].Description = child.NovoeIme;
                }
                RefreshTasksList();
                UpdateStats();
                SaveTasksToFile();
                
            }
        }

        private void RefreshTasksList()
        {
            List<Task> newTasks = new List<Task>();
            Console.WriteLine(cmbSort.SelectedIndex);
            Console.WriteLine(cmbSort.SelectedIndex == 3);
            
            if (cmbSort.SelectedIndex == 0)
            {
                newTasks = tasks.OrderBy(t => t.Description).ToList();
            }
            else if (cmbSort.SelectedIndex == 1)
            {
                newTasks = tasks.OrderByDescending(t => t.Description).ToList();
            }
            else if (cmbSort.SelectedIndex == 2)
            {
                newTasks = tasks.OrderBy(t => t.CreatedDate).ToList();
            }
            else if (cmbSort.SelectedIndex == 3)
            {
                newTasks = tasks.OrderByDescending(t => t.CreatedDate).ToList();
            }
            
            lstTasks.Items.Clear();
            if (sortin != null)
            {
                if (sortin != true)
                {
                    foreach (Task task in newTasks)
                    {
                        if (task.IsCompleted)
                        {
                            lstTasks.Items.Add(task.ToString());
                        }
                    }
                }
                else
                {
                    foreach (Task task in newTasks)
                    {
                        if (!task.IsCompleted)
                        {
                            lstTasks.Items.Add(task.ToString());
                        }
                    }
                }
            }
            else
            {
                foreach (Task task in newTasks)
                {
                    lstTasks.Items.Add(task.ToString());
                }
            }
        }

        private void UpdateStats()
        {
            int totalTasks = tasks.Count;
            int completedTasks = tasks.Count(t => t.IsCompleted);

            lblStats.Text = $"Zadań: {totalTasks} | Wykonanych: {completedTasks} | Pozostało: {totalTasks - completedTasks}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveTasksToFile();
            base.OnFormClosing(e);
        }
    }
}