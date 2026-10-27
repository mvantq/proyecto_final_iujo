using System;

namespace proyecto_final_iujo
{
    public struct Punto2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Punto2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Distancia euclidiana en el plano de la arena: d = sqrt((x2 - x1)^2 + (y2 - y1)^2)
        public double DistanciaA(Punto2D otro)
        {
            double dx = otro.X - this.X;
            double dy = otro.Y - this.Y;
            return Math.Sqrt((dx * dx) + (dy * dy));
        }

        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1})", X, Y);
        }
    }
}