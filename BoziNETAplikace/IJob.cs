using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoziNETAplikace
{
    public interface IJob
    {
        public string Name { get; set; }
        public double Salary { get; set; }
    }
}
