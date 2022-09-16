using Microsoft.Win32;
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

namespace CodeLines.App.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<string> _filenames = new List<string>();

        private void OnFolderSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,
                RestoreDirectory = true,
                Title = "Select file or directory",
                Filter = "Directory|*.this.directory",
                FileName = "Select",
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true
            };

            dialog.ShowDialog();

            if (dialog.FileNames.Any())
            {
                _filenames.Clear();

                foreach (string filename in dialog.FileNames)
                {
                    _filenames.Add(filename
                        .Replace("\\Select.this.directory", "")
                        .Replace(".this.directory", ""));
                }

                pathTextBox.Text = _filenames.FirstOrDefault();
            }
        }

        private void OnClearButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Text = "";
        }

        private void OnProcessButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
