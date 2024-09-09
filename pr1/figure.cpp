#include "figure.h"

double figure::squareArea(double a) {
    return a*a;
}
double figure::rectangleArea(double a, double b) {
    return a*b;
}
double figure::parallelogramArea(double a, double h) {
    return a*h;
}
double figure::rhombusArea(double d1, double d2) {
    return d1*d2/2.0;
}
double figure::triangleArea(double a, double h) {
    return a*h/2.0;
}
double figure::trapezoidArea(double a, double b, double h) {
    return h*(a+b)/2.0;
}
double figure::circleArea(double r) {
    return M_PI*r*r;
}
double figure::sectorArea(double r, double alpha){
    return M_PI*r*r*alpha/360.0;
}

double figure::calculateArea(int currentFigure, double p1, double p2, double p3) {
    double ans;
    figure figureCalc;
    switch (currentFigure) {
    case square:
        ans = figureCalc.squareArea(p1);
        break;
    case rectangle:
        ans = figureCalc.rectangleArea(p1, p2);
        break;
    case parallelogram:
        ans = figureCalc.parallelogramArea(p1, p2);
        break;
    case rhombus:
        ans = figureCalc.rhombusArea(p1, p2);
        break;
    case triangle:
        ans = figureCalc.triangleArea(p1, p2);
        break;
    case trapezoid:
        ans = figureCalc.trapezoidArea(p1, p2, p3);
        break;
    case circle:
        ans = figureCalc.circleArea(p1);
        break;
    case sector:
        ans = figureCalc.sectorArea(p1, p2);
        break;
    default:
        break;
    }
    return ans;
}


