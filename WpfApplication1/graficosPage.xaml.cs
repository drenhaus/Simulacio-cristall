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
using System.Windows.Threading;




namespace WpfApplication1
{
    /// <summary>
    /// Lógica de interacción para graficosPage.xaml
    /// </summary>
    public partial class graficosPage : Window
    {
        //ATRIBUTOS

        Generadora generador;
        int contadorHISTORIAL;
        List<double> listaFasexIteracion = new List<double>();
        List<double> listaTEMPxIteracion = new List<double>();
        bool estamosFASE = false;
        bool estamosTEMP = false;
        DispatcherTimer Time = new DispatcherTimer(); //Timer para la actualizar graficos



        public graficosPage()
        {
            InitializeComponent();
            BtnCalcularFASE.Click += BtnCalcularFASE_Click; //creamos los eventos click
            BtnCalcularTEMP.Click += BtnCalcularTEMP_Click;
            generador = new Generadora(); // generamos una clase Generadora cuando inicializamos
            Time.Stop();
            Time.Tick += new EventHandler(Time_click);
            Time.Interval = TimeSpan.FromMilliseconds(500);// por defecto establecemos una simulación cada segundo
            Time.Start();

        }

        private void Time_click(object sender, EventArgs e)
        {
            if (estamosFASE == true)    
            {
                // introducimos las listas al generador
                generador.SetListaFASExIteracion(listaFasexIteracion);
                generador.GenerarDatosFASE(Convert.ToDouble(contadorHISTORIAL));

                PlotModel model = new PlotModel(); // creamos un nuevo modelo de plot

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
                model.Title = "Evolución de la Fase media";
                LineSeries linea = new LineSeries();

                foreach (var item in generador.Puntos)
                {
                    linea.Points.Add(new DataPoint(item.X, item.Y));
                }
                linea.Title = "Valores generados";
                model.Series.Add(linea);
                Grafica.Model = model;
            }//actualizar fase grafico

            if (estamosTEMP == true)
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
                model.Title = "Evolución de la Temperatura media";
                LineSeries linea = new LineSeries();

                foreach (var item in generador.Puntos)
                {
                    linea.Points.Add(new DataPoint(item.X, item.Y));
                }
                linea.Title = "Valores generados";
                model.Series.Add(linea);
                Grafica.Model = model;
            }//actualizar temperatura grafico
        }

        //Permite mover ventana
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // SET de los atributos
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

        // GRÁFICA FASE
        private void BtnCalcularFASE_Click(object sender, RoutedEventArgs e)
        {
            estamosFASE = true;
            estamosTEMP = false;

            //Oculamos la label instrucciones
            labelmedio.Visibility = Visibility.Hidden;
            // introducimos las listas al generador
            generador.SetListaFASExIteracion(listaFasexIteracion);
            generador.GenerarDatosFASE(Convert.ToDouble(contadorHISTORIAL));

            PlotModel model = new PlotModel(); // creamos un nuevo modelo de plot

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
            model.Title = "Evolución de la Fase media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;

        }

        //GRÁFICA TEMPERATURA
        private void BtnCalcularTEMP_Click(object sender, RoutedEventArgs e)
        {
            estamosFASE = false;
            estamosTEMP = true;
            //Oculamos la label
            labelmedio.Visibility = Visibility.Hidden;
            //definimos las listas
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
            model.Title = "Evolución de la Temperatura media";
            LineSeries linea = new LineSeries();

            foreach (var item in generador.Puntos)
            {
                linea.Points.Add(new DataPoint(item.X, item.Y));
            }
            linea.Title = "Valores generados";
            model.Series.Add(linea);
            Grafica.Model = model;
        }

        //Cerrar ventana
        private void BtnCerrarGRAF_Click(object sender, RoutedEventArgs e)
        {
            Time.Stop();
            Close();

        }

        //Minimizar ventana
        private void BtnMiniGRAF_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }


}
