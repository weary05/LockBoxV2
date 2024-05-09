```mermaid
classDiagram
    class Account{
        public get string : AccountName
        public get string : Password
        public get string : ExtraNotes
        public get MailAddress : RegisteredEmail
    }
    class AccountHandler{
        public get List : Accounts
        public get List : EmailAddresses
        public AddAccount()
        public RemoveAccount()
    }

    AccountHandler *-- Account
```
