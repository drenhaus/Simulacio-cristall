using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApplication1;
using WpfApplication2;


namespace WpfApplication1
{
    class Generadora
    {
        public List<Punto> Puntos { get; set; }

        List<double> listaFasexIteracion = new List<double>();

        public void SetListaFASExIteracion(List<double> A)
        {
            this.listaFasexIteracion = A;
        }
        public List<Punto> GenerarDatos(double limiteSuperior)
        {
            
            double limiteInferior = 0;
            // limiteSuperior = 20; //numero iteraciones
            double incremento = 1;

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x+=incremento)
            {
                Puntos.Add(new Punto(x, Evaluar(x))); // canviar a imagen
            }

            return Puntos;
        }


        private double Evaluar(double x)
        {
            return listaFasexIteracion[Convert.ToInt32(x)];
        }
    }
}
