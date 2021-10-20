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
        List<double> listaTEMPxIteracion = new List<double>();

        public void SetListaTEMPxIteracion(List<double> B)
        {
            this.listaTEMPxIteracion = B;
        }
        public void SetListaFASExIteracion(List<double> A)
        {
            this.listaFasexIteracion = A;
        }

        public List<Punto> GenerarDatosTEMP(double limiteSuperior)
        {

            double limiteInferior = 0;
            double incremento = 1;

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarTEMP(x))); // canviar a imagen
            }

            return Puntos;
        }
        public List<Punto> GenerarDatosFASE(double limiteSuperior)
        {
            
            double limiteInferior = 0;
            double incremento = 1;

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x+=incremento)
            {
                Puntos.Add(new Punto(x, EvaluarFASE(x))); // canviar a imagen
            }

            return Puntos;
        }

        private double EvaluarTEMP(double x)
        {
            return listaTEMPxIteracion[Convert.ToInt32(x)];
        }
        private double EvaluarFASE(double x)
        {
            return listaFasexIteracion[Convert.ToInt32(x)];
        }
    }
}
