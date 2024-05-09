using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LockBox
{
    class AccountHandler
    {
        public List<Account> accounts { get; private set; }
        public List<MailAddress> mailAddresses {  get; private set; }
        public AccountHandler() 
        {
            accounts = new List<Account>();
            mailAddresses = new List<MailAddress>();
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
    }
}
