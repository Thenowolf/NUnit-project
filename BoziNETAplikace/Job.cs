using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoziNETAplikace
{
    public class Job : IJob
    {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Job(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }
    }
}
