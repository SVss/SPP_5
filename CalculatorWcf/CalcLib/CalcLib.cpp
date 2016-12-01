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
	else {
		if (x != 0)
			return INFINITY;
		else
			return NAN;
	}
}

double Negate(double x) {
	return -x;
}
