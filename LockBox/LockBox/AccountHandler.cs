using DevExpress.Utils.CommonDialogs.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace LockBox
{
    /// <summary>
    /// Responsible for managing the list of accounts for the user.
    /// </summary>
    class AccountHandler
    {
        public List<Account> accounts { get; private set; }
        private string password;
        private string filePath;

        public AccountHandler(string password, string filePath) 
        {
            accounts = new List<Account>();
            this.password = password;
            this.filePath = filePath;
            LoadData(filePath);
        }

        /// <summary>
        /// Add a new account to the accounts list
        /// </summary>
        /// <param name="accountName">The name of the account.</param>
        /// <param name="emailAddress">E-mail address to which the account is linked.</param>
        /// <param name="password">The password for the account.</param>
        /// <param name="extraNotes">Any extra details about the account that do not fit into the prior categories.</param>
        public bool AddAccount(string accountName, string emailAddress, string password, string extraNotes)
        {
            try
            {
                foreach(Account account in accounts)
                {
                    if(account.AccountName == accountName) 
                    {
                        var result = MessageBox.Show("An account with this name already exists, would you like to overwrite it?", "", MessageBoxButton.YesNo);
                        if(result == MessageBoxResult.Yes)
                        {
                            accounts.Remove(account);
                            break;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                accounts.Add(new Account(accountName, emailAddress, password, extraNotes));
                SaveData(filePath);
                return true;
            }
            catch (Exception e) { MessageBox.Show(e.Message); return false; }
        }

        /// <summary>
        /// Removes the account that has the given name from the AccountList.
        /// </summary>
        /// <param name="accountName">Name of Account to remove.</param>
        public void RemoveAccount(string accountName) 
        {
            foreach(Account account in accounts)
            {
                if(accountName == account.AccountName)
                {
                    accounts.Remove(account);
                    SaveData(filePath);
                    break;
                }
            }
        }

        /// <summary>
        /// Converts the objects held in the accounts list into a string.
        /// </summary>
        /// <returns></returns>
        private string ConvertAccountDataToString()
        {
            string output = string.Empty;
            foreach (Account account in accounts)
            {
                output += account.ToString() + "\r\n";
            }
            return output;
        }

        /// <summary>
        /// Takes string data and uses it to populate accounts list.
        /// </summary>
        /// <param name="data">Data to be converted into accounts.</param>
        private void LoadAccountDataFromString(string data)
        {
            if (string.IsNullOrEmpty(data)) { return; }
            try
            {
                string[] lines = data.Split("\r\n");
                foreach (string line in lines)
                {
                    if(line == "") {  continue; }
                    string[] parts = line.Split(',');
                    Account a = new Account(parts[0], parts[1], parts[2], parts[3]);
                    accounts.Add(a);
                }
            }
            catch { throw new FormatException(); }
        }

        /// <summary>
        /// Writes encrypted string to file.
        /// </summary>
        /// <param name="path">File path to save data to.</param>
        private void SaveData(string path) 
        {
            string data = ConvertAccountDataToString();
            StreamWriter writer = new(path);
            writer.Write(Encrypter.Encrypt(data, password));
            writer.Close();
        }

        /// <summary>
        /// Reads Data from file and loads to Account Handler.
        /// </summary>
        /// <param name="path">File path to load data from.</param>
        /// <returns></returns>
        private void LoadData(string path) 
        {
            StreamReader reader = new(path);
            string data = reader.ReadToEnd();
            reader.Close();
            data = Encrypter.Decrypt(data, password);
            LoadAccountDataFromString (data);
        }
    }
}
