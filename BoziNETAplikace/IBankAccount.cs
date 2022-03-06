using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoziNETAplikace
{
    public interface IBankAccount
    {
        public double Balance { get; }

        public void Debit(double amount);

        public void Credit(double amount, ATM atm);

        //public double PayCheck(List<Job> jobs);

    }
}
