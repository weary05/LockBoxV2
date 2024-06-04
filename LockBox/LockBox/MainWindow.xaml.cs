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
        private AccountHandler handler = new AccountHandler();
        public MainWindow()
        {
            InitializeComponent();
            //temporary remove later
            handler.AddAccount("name", "badkljcvobd@gmail.com", "b", "b");
            //
            Update(null, null);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
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
                    AccountName.Text = account.AccountName;
                    EmailAddressBox.Text = account.Address.ToString();
                    Password.Text = account.Password;
                    ExtraDetails.Text = account.ExtraNotes;
                }
            }
        }
    }
}