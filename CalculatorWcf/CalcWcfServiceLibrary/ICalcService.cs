using System.ServiceModel;

namespace CalcWcfServiceLibrary
{
    [ServiceContract]
    public interface ICalcService
    {
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Substract(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);

        [OperationContract]
        double Divide(double a, double b);

        [OperationContract]
        double Negate(double a);

        [OperationContract]
        double Sqrt(double a);

        [OperationContract]
        double Power(double a, double b);
    }
}
