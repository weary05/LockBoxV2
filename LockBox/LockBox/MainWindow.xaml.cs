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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

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

        private void OpenAccountData(object sender, RoutedEventArgs e)
        {
            string name = sender.ToString();
            name = name.Substring(name.IndexOf(" ") + 1);

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

        private void CreateNewAccount(object sender, RoutedEventArgs e)
        {
            CreateNewAccountButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
            AccountNameBox.Text = "Account Name";
            EmailAddressBox.Text = "Email Address";
            PasswordBox.Text = "Password";
            ExtraDetailsBox.Text = "Extra Notes";
        }

        private void SaveAccountData(object sender, RoutedEventArgs e)
        {
            handler.AddAccount(AccountNameBox.Text, EmailAddressBox.Text, PasswordBox.Text, ExtraDetailsBox.Text);
            CreateNewAccountButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Hidden;
            AccountNameBox.Text = "";
            EmailAddressBox.Text = "";
            PasswordBox.Text = "";
            ExtraDetailsBox.Text = "";
            Update();
        }

        private void DeleteCurrentAccount(object sender, RoutedEventArgs e)
        {
            handler.RemoveAccount(AccountNameBox.Text);
            Update();
        }
    }
}