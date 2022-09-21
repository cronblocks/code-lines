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
using WinForms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using CodeLines.Lib.Exceptions;
using CodeLines.Lib;

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

        private string _selectedPath = "";

        private void OnFolderSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog()
            {
                AutoUpgradeEnabled = true,
                Description = "Select file or directory",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };

            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                _selectedPath = dialog.SelectedPath;
                pathTextBox.Text = _selectedPath;
            }
        }

        private void OnClearButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBox.Text = "";
        }

        private void OnProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPath))
            {
                MessageBox.Show("Path is not provided", "Invalid Path");
            }
            else
            {
                Thread thread = new Thread(ProcessThread);
                thread.Start(_selectedPath);
            }
        }

        private void ProcessThread(object? path)
        {
            string? targetPath = path as string;

            if (!string.IsNullOrEmpty(targetPath))
            {
                RunOnGuiThread(() =>
                {
                    pathLabel.IsEnabled = false;
                    pathTextBox.IsEnabled = false;
                    folderSelectionButton.IsEnabled = false;
                    clearButton.IsEnabled = false;
                    processButton.IsEnabled = false;
                });

                try
                {
                    LinesCounter counter =
                        new LinesCounter(targetPath, PrintOutputLine,
                                         skipped_dir_or_filenames: ".git,bin,obj");

                    counter.Process();
                    counter.PrintResult();
                }
                catch (NeitherFileNorDirectoryException ex)
                {
                    PrintOutputLine($"ERROR! Neither a file nor a directory: \"{ex.Name}\"");
                }
                catch (Exception ex)
                {
                    PrintOutputLine($"ERROR! \"{ex.Message}\"");
                }

                RunOnGuiThread(() =>
                {
                    pathLabel.IsEnabled = true;
                    pathTextBox.IsEnabled = true;
                    folderSelectionButton.IsEnabled = true;
                    clearButton.IsEnabled = true;
                    processButton.IsEnabled = true;
                });
            }
        }

        private void RunOnGuiThread(Action action)
        {
            mainWindow.Dispatcher.Invoke(action);
        }

        private void PrintOutputLine(string text)
        {
            RunOnGuiThread(() =>
            {
                outputTextBox.AppendText($"{text}{Environment.NewLine}");
                outputTextBox.ScrollToEnd();
            });
        }
    }
}
