using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    class Generadora
    {
        public List<Punto> Puntos { get; set; }
        public List<Punto> GenerarDatos(double limiteInferior, double limiteSuperior, double incremento)
        {
            limiteInferior = 0;
            limiteInferior = 20;
            incremento = 1;

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x+=incremento)
            {
                Puntos.Add(new Punto(x, Evaluar(x)));

            }

            return Puntos;
        }

        private double Evaluar(double x)
        {
            return Math.Sin(x);

        }
    }
}
