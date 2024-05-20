using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaPlus.Drivers
{
    public class SimulationDriver
    {
        public double GetValue(string address)
        {
            return address switch
            {
                "S" => Sine(),
                "C" => Cosine(),
                "R" => Ramp(),
                _ => -1000,
            };
        }

        private double Sine()
        {
            return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private double Cosine()
        {
            return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private double Ramp()
        {
            return 100 * DateTime.Now.Second / 60;
        }
    }
}
