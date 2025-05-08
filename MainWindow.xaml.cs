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

namespace projekt2._0
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Date.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            string text = t1.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                list.Items.Add(text);
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
            list.Items.Clear();
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
    }
}