using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;

namespace Todo_list
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string filePath = "tasks.json";
        private List<string> tasks = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            Date.Text = DateTime.Now.ToString("dd.MM.yyyy");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonConvert.DeserializeObject<List<string>>(json);
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        list.Items.Add(task);
                    }
                }
            }
            this.Closing += MainWindow_Closing;
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            string text = t1.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                list.Items.Add(text);
                tasks.Add(text);
                t1.Clear();
            }
        }

        private void t1_Keyboard(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Add_Button(null, null);
            }
        }

        private void Delete_All_Button(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Opravdu chcete smazat všechny úkoly?", "Potvrzení", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                list.Items.Clear();
                tasks.Clear();
            }
        }

        private void Delete_Task_Button(object sender, RoutedEventArgs e)
        {
            Button deleteTaskButton = sender as Button;
            if (deleteTaskButton != null)
            {
                StackPanel parent = deleteTaskButton.Parent as StackPanel;
                if (parent != null)
                {
                    TextBlock textBlock = parent.Children[0] as TextBlock;
                    string vymazat = textBlock.Text;
                    list.Items.Remove(vymazat);
                    tasks.Remove(vymazat);
                }
            }
        }
        private void Done_Task_Button(object sender, RoutedEventArgs e)
        {
            Button doneTaskButton = sender as Button;
            if (doneTaskButton != null)
            {
                StackPanel parent = doneTaskButton.Parent as StackPanel;
                if (parent != null)
                {
                    TextBlock textBlock = parent.Children[0] as TextBlock;
                    if (textBlock.TextDecorations != TextDecorations.Strikethrough)
                    {
                        textBlock.TextDecorations = TextDecorations.Strikethrough;
                    }
                    else
                    {
                        textBlock.TextDecorations = null;
                    }
                }
            }
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string json = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(filePath, json);
        }

    }
}