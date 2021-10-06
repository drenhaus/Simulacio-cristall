using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1;
using NormasJuego;
using WpfApplication2;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;

namespace WpfApplication1
{
    class Graficas
    {
        Malla matriz_celdas = new Malla();
        List<Malla> historial = new List<Malla>();
        int Lenght_lista;
        int[] vectorFase;
        int[] vectorIteraciones;

        public void SetLista(List<Malla> H)
        {
            this.historial = H;
        }

        public List<Malla> GetLista()
        {
            return historial;
        }

        public void graficaVariacionFASE() // haremos de vecinos vivos

        {
            Lenght_lista = historial.Count;

           vectorFase = new int[Lenght_lista];
           vectorIteraciones = new int[Lenght_lista];

            for (int l = 0; l <= Lenght_lista; l++)
            {
                int c = historial[l].GetNumeroDeVivosDeLaMatriz();
                vectorFase[l] = c;
                vectorIteraciones[l] = l;
            }

            

        }


    }
}
