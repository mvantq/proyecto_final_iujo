using System;

namespace proyecto_final_iujo
{
    public static class AlgoritmoDano
    {
        // Función matemática para cálculo de mitigación y absorción de impacto
        public static int CalcularImpactoNeto(int ataqueBase, double multiplicadorElemental, int defensaRival)
        {
            // Modelo de mitigación no lineal: Máximo (0, (Ataque * Factor) - Defensa)
            double danoBruto = ataqueBase * multiplicadorElemental;
            int danoFinal = (int)Math.Max(0, danoBruto - defensaRival);
            return danoFinal;
        }

        public static int CalcularDanoSismico(int vidaActual, double factorCurvatura)
        {
            // F(x) = Daño porcentual proporcional a la masa/vida
            return (int)Math.Ceiling(vidaActual * factorCurvatura);
        }
    }
}