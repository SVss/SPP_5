using System.Runtime.InteropServices;

namespace CalcWcfServiceLibrary
{
    public partial class CalcService
    {
        // Internals

        [DllImport("CalcLib.dll", EntryPoint = "Add")]
        private static extern
            double C_Add(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Substract")]
        private static extern
            double C_Substract(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Multiply")]
        private static extern
            double C_Multiply(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Divide")]
        private static extern
            double C_Divide(double x, double y);

        [DllImport("CalcLib.dll", EntryPoint = "Negate")]
        private static extern
            double C_Negate(double x);

        [DllImport("CalcLib.dll", EntryPoint = "Sqrt")]
        private static extern
            double C_Sqrt(double x);

        [DllImport("CalcLib.dll", EntryPoint = "Power")]
        private static extern
            double C_Power(double x, double y);
    }
}
