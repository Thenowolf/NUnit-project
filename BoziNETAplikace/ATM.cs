using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoziNETAplikace
{
    public class ATM
    {
        private String nameofbank;
        private double costofservice;
        public string Nameofbank { get => nameofbank; set => nameofbank = value; }
        public double Costofservice { get => costofservice; set => costofservice = value; }
        public ATM(string nameofbank, double costofservice)
        {
            this.Nameofbank = nameofbank;
            this.Costofservice = costofservice;
        }

    }
}
