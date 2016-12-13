#pragma once

#ifdef CALCLIB_EXPORTS
#define EXP extern "C" __declspec(dllexport)
#else
#define EXP extern "C" __declspec(dllimport)
#endif

EXP double Add(double x, double y);
EXP double Substract(double x, double y);
EXP double Multiply(double x, double y);
EXP double Divide(double x, double y);

EXP double Negate(double x);

EXP double Sqrt(double x);
EXP double Power(double x, double y);
