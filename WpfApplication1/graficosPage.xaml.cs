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
        public graficosPage()
        {
            InitializeComponent();

            PlotModel model = new PlotModel();

            LinearAxis ejeX = new LinearAxis();
            ejeX.Minimum = 0;
            ejeX.Maximum = 10;
            ejeX.Position = AxisPosition.Bottom;

            LinearAxis ejeY = new LinearAxis();
            ejeY.Minimum = 0;
            ejeY.Maximum = 10;
            ejeY.Position = AxisPosition.Left;

            model.Axes.Add(ejeY);
            model.Axes.Add(ejeX);
            model.Title = "Datos";
            LineSeries linea = new LineSeries();

            List Points = new List<DataPoint>
                              {
                                  new DataPoint(0, 4),
                                  new DataPoint(10, 13),
                                  new DataPoint(20, 15),
                                  new DataPoint(30, 16),
                                  new DataPoint(40, 12),
                                  new DataPoint(50, 12)
                              };

            linea.Points.Add



        }



/*        private void WPF_load(object sender, EventArgs e)
        {
            OxyPlot.Wpf.PlotView pv = new OxyPlot.Wpf.PlotView();
            pv.Po


        }*/
    }

}
