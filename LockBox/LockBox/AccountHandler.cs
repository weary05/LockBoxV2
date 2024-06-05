using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace LockBox
{
    /// <summary>
    /// Responsible for managing the list of accounts for the user.
    /// </summary>
    class AccountHandler
    {
        public List<Account> accounts { get; private set; }
        private string password;

        public AccountHandler(string password) 
        {
            accounts = new List<Account>();
            this.password = password;
            LoadData(password);
        }

        public void AddAccount(string accountName, string emailAddress, string password, string extraNotes) 
        { 
            accounts.Add(new Account(accountName, password, extraNotes, emailAddress));
        }

        public void RemoveAccount(string accountName) 
        {
            foreach(Account account in accounts)
            {
                if(accountName == account.AccountName)
                {
                    accounts.Remove(account);
                }
            }
        }

        private string ConvertAccountDataToString()
        {
            string output = string.Empty;
            foreach (Account account in accounts)
            {
                output += account.ToString() + "\r\n";
            }
            return output;
        }

        private void LoadAccountDataFromString(string data)
        {
            string[] lines = data.Split("\r\n");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                Account a = new Account(parts[0], parts[1], parts[2], parts[3]);
                accounts.Add(a);
            }
        }

        private void SaveData(string data) 
        {
            throw new NotImplementedException();
        }

        private string LoadData(string path) 
        {
            throw new NotImplementedException();
        }
    }
}
