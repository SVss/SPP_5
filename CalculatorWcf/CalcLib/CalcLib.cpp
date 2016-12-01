#include "CalcLib.h"
#include <math.h>

double Add(double x, double y)
{
	return x + y;
}

double Substract(double x, double y)
{
	return x - y;
}

double Multiply(double x, double y)
{
	return x * y;
}

double Divide(double x, double y)
{
	if (y != 0) {
		return x / y;
	}
	
	return NAN;
}

double Negate(double x) {
	return -x;
}

double Sqrt(double x) {
	if (!(x < 0)) {
		return sqrt(x);
	}

	return NAN;
}

double Power(double x, double y) {
	if (!((x == 0) && !(y > 0))) {
		return pow(x, y);
	}

	return NAN;
}
