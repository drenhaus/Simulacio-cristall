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
        public graficosPage()
        {
            InitializeComponent();
            BtnCalcular.Click += BtnCalcular_Click;
            generador = new Generadora();
            //generador = new Generadora();

        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {

            generador.GenerarDatos(0, 20, 1);

            

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis(); //generamos los ejes
            ejeX.Minimum = 0;
            ejeX.Maximum = 20;  //numero de iteraciones
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = generador.Puntos.Min(p => p.Y);
            ejeY.Maximum = generador.Puntos.Max(p => p.Y);
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Datos geneardos";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;
            /*            List Points = new List<DataPoint>
                                          {
                                              new DataPoint(0, 4),
                                              new DataPoint(10, 13),
                                              new DataPoint(20, 15),
                                              new DataPoint(30, 16),
                                              new DataPoint(40, 12),
                                              new DataPoint(50, 12)
                                          };*/

           // linea.Points.Add
        }



        /*        private void WPF_load(object sender, EventArgs e)
                {
                    OxyPlot.Wpf.PlotView pv = new OxyPlot.Wpf.PlotView();
                    pv.Po


                }*/
    }

}
