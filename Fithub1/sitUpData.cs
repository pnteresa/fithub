using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fithub1
{
    [DataContract]
    public class sitUpData
    {
        public sitUpData(int count, DateTime date, string duration, double calories)
        {
            this.count = count;
            this.date = date;
            this.duration = duration;
            this.calories = calories;
        }

        // untuk stats harian
        public sitUpData(int count, DateTime date, double calories)
        {
            this.count = count;
            this.date = date;
            this.calories = calories;
        }
        
        public int count { get; set; }
        public DateTime date { get; set; }
        public string duration { get; set; }
        public double calories { get; set; }
    }
}
