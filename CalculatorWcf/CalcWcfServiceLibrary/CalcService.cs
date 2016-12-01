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

        public double Negate(double a)
        {
            return C_Negate(a);
        }

        public double Sqrt(double a)
        {
            return C_Sqrt(a);
        }

        public double Power(double a, double b)
        {
            return C_Power(a, b);
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

        [DllImport("CalcLib.dll", EntryPoint = "Negate")]
        private static extern double C_Negate(double x);

        [DllImport("CalcLib.dll", EntryPoint = "Sqrt")]
        private static extern double C_Sqrt(double x);

        [DllImport("CalcLib.dll", EntryPoint = "Power")]
        private static extern double C_Power(double x, double y);
    }
}
