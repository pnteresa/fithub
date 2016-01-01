using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fithub1
{
    [DataContract]
    public class runningData
    {
        public runningData(DateTime date, double distance, string duration, double avgspeed, double avgpace, double calories)
        {
            this.date = date;
            this.duration = duration;
            this.distance = distance;
            this.calories = calories;
            this.avgpace = avgpace;
            this.avgspeed = avgspeed;
        }

        public runningData(DateTime date, double distance, double calories)
        {
            this.date = date;
            this.distance = distance;
            this.calories = calories;
        }

        public DateTime date { get; set; }
        public string duration { get; set; }
        public double distance { get; set; }
        public double calories { get; set; }
        public double avgspeed { get; set; }
        public double avgpace { get; set; }
        
    }
}
