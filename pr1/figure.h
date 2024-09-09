    #ifndef FIGURE_H
    #define FIGURE_H

    #include <cmath>

    enum figureType  {
        square,
        rectangle,
        parallelogram,
        rhombus,
        triangle,
        trapezoid,
        circle,
        sector,
    };

    class figure
    {
    public:
        static double calculateArea(int currentFigure, double p1, double p2, double p3);
    private:
        double squareArea(double a);
        double rectangleArea(double a, double b);
        double parallelogramArea(double a, double h);
        double rhombusArea(double d1, double d2);
        double triangleArea(double a, double h);
        double trapezoidArea(double a, double b, double h);
        double circleArea(double r);
        double sectorArea(double r, double alpha);

    };

    #endif // FIGURE_H
