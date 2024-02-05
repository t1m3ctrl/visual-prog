using System;

namespace project
{
    interface INotifyer
    {
        void Notify(decimal balance);
    }
    class Account
    {
        private decimal _balance;
        private List<INotifyer> _notifyers;

        public Account() 
        { 
            _balance = 0; 
            _notifyers = new List<INotifyer>(); 
        }
        public Account(decimal _balance) 
        { 
            this._balance = _balance; 
            _notifyers = new List<INotifyer>(); 
        }
        public void AddNotifyer(INotifyer notifyer) 
        {
            _notifyers.Add(notifyer);
        }
        public void ChangeBalance(decimal value)
        {
            _balance = value;
            Notification();
        }
        public decimal Balance
        {
            get { return _balance; }
        }
        private void Notification()
        {
            for (int i = 0; i < _notifyers.Count ; i++)
            {
                _notifyers[i].Notify(_balance);
            }
        }
    }
    public class SMSLowBalanceNotifyer : INotifyer
    {
        private string _phone;
        private decimal _lowBalanceValue;
        
        public SMSLowBalanceNotifyer(string phone, decimal lowBalanceValue)
        {
            _phone = phone;
            _lowBalanceValue = lowBalanceValue;
        }
        public void Notify(decimal balance) 
        {
            if (balance < _lowBalanceValue)
            {
                Console.WriteLine($"class SNSLowBalanceNotifyer balance is low: {balance}\n");
            }
        }
    }
    public class EMailBalanceChangedNotifyer : INotifyer
    {
        private string _email;
        public EMailBalanceChangedNotifyer(string email)
        {
            _email = email;
        }
        public void Notify(decimal balance)
        {
            Console.WriteLine($"class EMailBalanceChangedNotifyer balance changed: {balance}\n");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Account Student = new Account(5);
        }

    }
}