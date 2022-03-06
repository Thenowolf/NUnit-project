using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoziNETAplikace
{
    public class BoziApp
    {
        public bool isTriangle(int a, int b, int c)
        {
            if (a + b > c) return true;
            else return false;
        }
    }
}
