#pragma once

#ifdef CALCLIB_EXPORTS
#define CALCLIB_EXPORTS extern "C" __declspec(dllexport)
#else
#define CALCLIB_EXPORTS extern "C" __declspec(dllexport)
#endif

CALCLIB_EXPORTS double Add(double x, double y);
CALCLIB_EXPORTS double Substract(double x, double y);
CALCLIB_EXPORTS double Multiply(double x, double y);
CALCLIB_EXPORTS double Divide(double x, double y);

CALCLIB_EXPORTS double Negate(double x);

CALCLIB_EXPORTS double Sqrt(double x);
CALCLIB_EXPORTS double Power(double x, double y);
