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

        

/*        public List<Malla> GetList();
        {
            
        }*/
    public List<Punto> GenerarDatos(double limiteInferior, double limiteSuperior, double incremento)
        {
            
            limiteInferior = 0;
            limiteSuperior = 20; //numero iteraciones
            incremento = 1;

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x+=incremento)
            {
                Puntos.Add(new Punto(x, Evaluar(x))); // canviar a imagen

            }

            return Puntos;
        }

        private double Imagen(double x)
        {
            
            
            return laYdeX;

        }
        private double Evaluar(double x)
        {
            return Math.Sin(x);

        }
    }
}
