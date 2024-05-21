using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Driver.SimulationDriver
{
    public static class SimulationDriver
    {
        public static double GetValue(string address)
        {
            return address switch
            {
                "S" => Sine(),
                "C" => Cosine(),
                "R" => Ramp(),
                _ => -1000,
            };
        }

        private static double Sine()
        {
            return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private static double Cosine()
        {
            return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }

        private static double Ramp()
        {
            return 100 * DateTime.Now.Second / 60;
        }
    }
}
