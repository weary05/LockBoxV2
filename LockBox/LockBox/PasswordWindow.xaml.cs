using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LockBox
{
    public partial class PasswordWindow : Window
    {
        private bool CreateNewMode = false;
        public PasswordWindow()
        {
            InitializeComponent();
            if (File.Exists("FileLocation.txt")) 
            {
                StreamReader reader = new("FileLocation.txt");
                FilePathBox.Text = reader.ReadToEnd();
                reader.Close();
            }
        }
        /// <summary>
        /// Changes between telling the program to create a new file and loading an existing one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ToggleMode(object sender = null, RoutedEventArgs e = null)
        {
            CreateNewMode = !CreateNewMode;
            if (CreateNewMode)
            {
                CreateNewButton.Content = "Cancel";
                PasswordBox.Text = "Password";
                FilePathBox.Text = "File Path";
            }
            else
            {
                CreateNewButton.Content = "Create New";
            }
        }

        /// <summary>
        /// Submits the given file path and password to try and open the data file and load the main window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Submit(object sender = null, RoutedEventArgs e = null)
        {
                try
                {
                    if (CreateNewMode)
                    {
                        StreamWriter filemaker = new(FilePathBox.Text); //had to use this instead of File.Create because File.Create does not seem to close the file after use.
                        filemaker.Close();
                    }
                    MainWindow main = new MainWindow();
                    main.SubmitPassword(PasswordBox.Text, FilePathBox.Text);
                    StreamWriter writer = new("FileLocation.txt");
                    writer.Write(FilePathBox.Text);
                    writer.Close();
                    main.Show();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Incorrect file path or password entered.");
                }
        }
    }
}
