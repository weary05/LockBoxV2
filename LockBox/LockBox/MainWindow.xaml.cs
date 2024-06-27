using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LockBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        private string password = string.Empty;

        private AccountHandler handler;
        public MainWindow()
        {
            InitializeComponent();
            //temporary remove later
            password = "a";
            handler = new AccountHandler(password, "AppData/Data.txt");
            //
            Update();
        }

        /// <summary>
        /// Closes the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        /// <summary>
        /// Updates the list of accounts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update(object sender = null, RoutedEventArgs e = null)
        {
            AccountList.Children.Clear();
            foreach (Account account in handler.accounts) 
            {
                Button button = new Button();
                button.Content = account.AccountName; button.Style = (Style)FindResource("ButtonStyle"); button.Click += OpenAccountData;
                AccountList.Children.Add(button);
            }
        }

        /// <summary>
        /// Retrieves data on the selected account and displays it to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenAccountData(object sender, RoutedEventArgs e)
        {
            string name = sender.ToString();
            name = name.Substring(name.IndexOf(" ") + 1);

            EditButton.Visibility = Visibility.Visible;

            //search through accounts for the one that matches the selected option and set account data for display.
            foreach (Account account in handler.accounts)
            {
                if (account.AccountName == name)
                {
                    AccountNameBox.Text = account.AccountName;
                    EmailAddressBox.Text = account.Address.ToString();
                    PasswordBox.Text = account.Password;
                    ExtraDetailsBox.Text = account.ExtraNotes;
                }
            }
        }

        /// <summary>
        /// switches the application interface to allow the creation of a new account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewAccount(object sender, RoutedEventArgs e)
        {
            EditModeEnable();
            AccountNameBox.Text = "Account Name";
            EmailAddressBox.Text = "Email Address";
            PasswordBox.Text = "Password";
            ExtraDetailsBox.Text = "Extra Notes";
        }

        /// <summary>
        /// Creates a new account using the information in the text boxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAccountData(object sender, RoutedEventArgs e)
        {
            bool success = handler.AddAccount(AccountNameBox.Text, EmailAddressBox.Text, PasswordBox.Text, ExtraDetailsBox.Text);
            if (success)
            {
                CreateNewAccountButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Hidden;
                CancelButton.Visibility = Visibility.Hidden;
                Update();
            }
        }

        /// <summary>
        /// Deletes the currently selected account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCurrentAccount(object sender, RoutedEventArgs e)
        {
            handler.RemoveAccount(AccountNameBox.Text);
            AccountNameBox.Text = string.Empty;
            EmailAddressBox.Text = string.Empty;
            PasswordBox.Text = string.Empty;
            ExtraDetailsBox.Text = string.Empty;
            Update();
        }

        /// <summary>
        /// Allows the information boxed to be edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditModeEnable(object sender = null, RoutedEventArgs e = null)
        {
            CreateNewAccountButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Hidden;

            AccountNameBox.IsReadOnly = false;
            EmailAddressBox.IsReadOnly=false;
            PasswordBox.IsReadOnly=false;
            ExtraDetailsBox.IsReadOnly=false;
        }

        /// <summary>
        /// Cancels the current edit/ creation of a new account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEdit (object sender, RoutedEventArgs e)
        {
            CreateNewAccountButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Visible;

            AccountNameBox.Text = string.Empty;
            EmailAddressBox.Text = string.Empty;
            PasswordBox.Text = string.Empty;
            ExtraDetailsBox.Text = string.Empty;

            AccountNameBox.IsReadOnly = true;
            EmailAddressBox.IsReadOnly = true;
            PasswordBox.IsReadOnly = true;
            ExtraDetailsBox.IsReadOnly = true;
        }
    }
}