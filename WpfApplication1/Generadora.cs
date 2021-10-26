using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Generadora
    {
        public List<Punto> Puntos { get; set; } //generamos una lista de puntos

        List<double> listaFasexIteracion = new List<double>();
        List<double> listaTEMPxIteracion = new List<double>();

        //SET de las listas
        public void SetListaTEMPxIteracion(List<double> B)
        { this.listaTEMPxIteracion = B; }
        public void SetListaFASExIteracion(List<double> A)
        { this.listaFasexIteracion = A; }

        //GENERAMOS EL GRÁFICO DE LA TEMPERATURA
        public List<Punto> GenerarDatosTEMP(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarTEMP(x)));
            }

            return Puntos;
        }

        //GENERAMOS EL GRÁFICO DE LA FASE
        public List<Punto> GenerarDatosFASE(double limiteSuperior)
        {
            double limiteInferior = 0; //límite inferior
            double incremento = 1; // incremento

            Puntos = new List<Punto>();
            for (double x = limiteInferior; x < limiteSuperior; x += incremento)
            {
                Puntos.Add(new Punto(x, EvaluarFASE(x)));
            }

            return Puntos;
        }

        //GENERAMOS LAS FUNCIONES QUE VAMOS A PLOTEAR
        // dada una iteración, nos devuelve el valor de la temperatura media en esa iteración
        private double EvaluarTEMP(double x)
        {
            return listaTEMPxIteracion[Convert.ToInt32(x)]; // hace la busqueda en la lista
        }
        // dada una iteración, nos devuelve el valor de la fase media en esa iteración
        private double EvaluarFASE(double x)
        {
            return listaFasexIteracion[Convert.ToInt32(x)];// hace la búsqueda en la lista
        }
    }
}
