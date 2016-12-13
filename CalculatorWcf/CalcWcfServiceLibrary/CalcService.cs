using System.ServiceModel;

namespace CalcWcfServiceLibrary
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.PerSession,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class CalcService : ICalcService
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
    }
}
