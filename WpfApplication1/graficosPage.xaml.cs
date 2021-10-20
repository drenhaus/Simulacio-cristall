﻿using System;
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
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;




namespace WpfApplication1
{
    /// <summary>
    /// Lógica de interacción para graficosPage.xaml
    /// </summary>
    public partial class graficosPage : Window
    {
        Generadora generador;
        int contadorHISTORIAL;
        List<double> listaFasexIteracion = new List<double>();
        List<double> listaTEMPxIteracion = new List<double>();

        public graficosPage()
        {
            InitializeComponent();
            BtnCalcularFASE.Click += BtnCalcularFASE_Click;
            BtnCalcularTEMP.Click += BtnCalcularTEMP_Click;
            generador = new Generadora();
        }

        public void SetListaFASExIteracion(List<double> A)
        {
            this.listaFasexIteracion = A;
        }
        public void SetListaTEMPxIteracion(List<double> B)
        {
            this.listaTEMPxIteracion = B;
        }
        public void SetcontadorHIST(int count)
        {
            this.contadorHISTORIAL = count;
        }


        private void BtnCalcularFASE_Click(object sender, RoutedEventArgs e)
        {
            generador.SetListaFASExIteracion(listaFasexIteracion);
            generador.GenerarDatosFASE(Convert.ToDouble(contadorHISTORIAL));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = contadorHISTORIAL;  //numero de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 0;//generador.Puntos.Min(p => p.Y);
            ejeY.Maximum = 1;//generador.Puntos.Max(p => p.Y);
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Datos geneardos FASE";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }

        private void BtnCalcularTEMP_Click(object sender, RoutedEventArgs e)
        {
            generador.SetListaTEMPxIteracion(listaTEMPxIteracion);
            generador.GenerarDatosTEMP(Convert.ToDouble(contadorHISTORIAL));

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = contadorHISTORIAL;  //numero de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = -1;//generador.Puntos.Min(p => p.Y);
            ejeY.Maximum = 0;//generador.Puntos.Max(p => p.Y);
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Datos geneardos TEMPERATURA";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }

    }

}