using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LockBox
{
    class Account
    {
        public MailAddress Address { get; private set; }
        public string AccountName { get; private set; }
        public string Password { get; private set; }
        public string ExtraNotes { get; private set; }

        public Account(string AccountName, string Password, string ExtraNotes, string emailAddress)
        {
            this.AccountName = AccountName;
            this.Password = Password;
            this.ExtraNotes = ExtraNotes;
            try
            {
                this.Address = new MailAddress(emailAddress);
            }
            catch { throw new Exception("Email address entered is not valid"); }
        }
    }
}
