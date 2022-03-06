using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BoziNETAplikace
{
    public class BankAccount : IBankAccount
    {
        private readonly string m_customerName;
        private double m_balance;
        private double m_debt = 0;
        private List<Job> m_jobs;
        //private IJobs jobs1;

        private BankAccount() { }

        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
            m_jobs = new List<Job>();
        }

        public string CustomerName
        {
            get { return m_customerName; }
        }

        public double Balance
        {
            get { return m_balance; }
        }

        public List<Job> Jobs { get => m_jobs; set => m_jobs = value; }
        public double Debt { get => m_debt; set => m_debt = value; }

        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("not enough balance");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("wrong amount");
            }

            m_balance -= amount; // intentionally incorrect code
        }

        public void Credit(double amount, ATM usedATM)
        {
            if (usedATM != null)
            {
                amount -= usedATM.Costofservice;
            } 

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        public double PayCheck(IJobs jobs)
        {
            double amount = 0;

            foreach(var job in jobs.GetJobs())
            {
                amount += job.Salary;
            }

            if (this.Debt > 0)
            {
                amount -= Debt;
            }
            Credit(amount, null);
            return (amount - Debt);
        }

        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Bryan Walton", 11.99);

            ba.Credit(5.77,null);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
        }
    }
}
