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
                Console.WriteLine($"class SMSLowBalanceNotifyer balance is low: {balance}\n");
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
    internal class Program
    {
        static void Main(string[] args)
        {
            Account student = new Account(5);
            EMailBalanceChangedNotifyer mail_notification = new EMailBalanceChangedNotifyer("example@mail.com");
            SMSLowBalanceNotifyer sms_notification = new SMSLowBalanceNotifyer("+73247835757", 2);
            student.AddNotifyer(mail_notification);
            student.AddNotifyer(sms_notification);
            student.ChangeBalance(10);
            student.ChangeBalance(15);
            student.ChangeBalance(1);

            Console.WriteLine("Press any key to continue..\n");
            Console.ReadKey(true);

            Account another_student = new Account();
            another_student.ChangeBalance(1000);
            EMailBalanceChangedNotifyer mail_notification1 = new EMailBalanceChangedNotifyer("example2@mail.com");
            SMSLowBalanceNotifyer sms_notification1 = new SMSLowBalanceNotifyer("+73247848989", 100);
            another_student.AddNotifyer(mail_notification1);
            another_student.AddNotifyer(sms_notification1);
            another_student.ChangeBalance(800);
            another_student.ChangeBalance(300);
            another_student.ChangeBalance(-5);
        }

    }
}