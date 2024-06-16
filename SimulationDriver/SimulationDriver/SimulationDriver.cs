using System;

namespace SimulationDriver;

public class SimulationDriver: IDriver.IDriver
{
    public double GetValue(string address)
    {
        return address switch
        {
            "S" => Sine(),
            "C" => Cosine(),
            "R" => Ramp(),
            _ => -1000
        };
    }

    private static double Sine() => 100 * Math.Sin(DateTime.Now.Second / 60.0 * Math.PI);

    private static double Cosine() => 100 * Math.Cos(DateTime.Now.Second / 60.0 * Math.PI);

    private static double Ramp() => 100.0 * DateTime.Now.Second / 60;
}