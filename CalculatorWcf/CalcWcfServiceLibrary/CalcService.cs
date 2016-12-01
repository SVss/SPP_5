using System.Runtime.InteropServices;

namespace CalcWcfServiceLibrary
{
    public class CalcService : ICalcService
    {
        // Public

        public double Add(double a, double b)
        {
            return C_Add(a, b);
        }

        public double Substract(double a, double b)
        {
            return C_Substract(a, b);
        }

        public double Multiply(double a, double b)
        {
            return C_Multiply(a, b);
        }

        public double Divide(double a, double b)
        {
            return C_Divide(a, b);
        }

        // Internal

        [DllImport("CalcLib.dll", EntryPoint = "Add")]
        private static extern double C_Add(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Substract")]
        private static extern double C_Substract(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Multiply")]
        private static extern double C_Multiply(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Divide")]
        private static extern double C_Divide(double x, double y);
    }
}
