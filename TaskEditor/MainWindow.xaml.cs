using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;


namespace TaskEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            editorTextBox.SelectAll();
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            editorTextBox.Paste();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            editorTextBox.Copy();
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
            editorTextBox.Cut();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = fileNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                TextRange textRange = new TextRange(editorTextBox.Document.ContentStart, editorTextBox.Document.ContentEnd);
                using (System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                {
                    textRange.Save(fileStream, DataFormats.Text);
                }
            }
        }

        private void AutoSave(object sender, TextChangedEventArgs e)
        {
            string fileName = fileNameTextBox.Text;
            if (autoSaveCheckBox.IsChecked == true)
            {
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    TextRange textRange = new TextRange(editorTextBox.Document.ContentStart, editorTextBox.Document.ContentEnd);
                    using (System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                    {
                        textRange.Save(fileStream, DataFormats.Text);

                    }
                }
            }
            else return;


        }

        private void autoSaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (autoSaveCheckBox.IsChecked == true)
            {
                editorTextBox.TextChanged += AutoSave;
            }
            else
            {
                editorTextBox.TextChanged -= AutoSave;
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                fileNameTextBox.Text = fileName;

                TextRange textRange = new TextRange(editorTextBox.Document.ContentStart, editorTextBox.Document.ContentEnd);
                using (System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
                {
                    textRange.Load(fileStream, DataFormats.Text);
                }
            }
        }
    }
}
