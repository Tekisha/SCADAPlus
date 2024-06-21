namespace IDriver
{
    public interface IDriver
    {
        public double GetValue(string address);
        public void Connect(string address);
    }
}
