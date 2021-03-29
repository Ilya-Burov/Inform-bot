using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class TemperatureInfo
    {
        public float Temp { get; set; }
        public float Feels_like { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
    }
}
