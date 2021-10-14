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

    public partial class MainWindow : Window
    {

        Normas norm = new Normas();
        Rectangle[,] casillas; // Matriz donde guardaremos todos los rectangulos para poder recorrerlos
        Rectangle[,] casillas2;
        int x;  //columnas
        int y;  //filas
        Malla matriz_celdas= new Malla();

        List<Malla> historial= new List<Malla>();
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();



        public MainWindow()
        {
            InitializeComponent();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) // mostrar reglas
        {
            Form1 lc = new Form1();
            lc.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) // guardar fichero
        {

            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Simulación"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                int n = matriz_celdas.GuardarSimulacion(filename);
                if (n == 0)
                { MessageBox.Show("Simulación guardada correctamente!"); }
                else
                { MessageBox.Show("No ha sido posible guardar la simulación"); }
            }
            else
            { MessageBox.Show("No ha sido posible guardar la simulación"); }



        }

        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            Point p = (Point)a.Tag;

            matriz_celdas.SetTemperaturaDeCelda(Convert.ToInt32(p.Y)+1, Convert.ToInt32(p.X)+1,0);
            matriz_celdas.SetFaseDeCelda(Convert.ToInt32(p.Y)+1, Convert.ToInt32(p.X)+1,0);

            casillas[Convert.ToInt32(p.Y), Convert.ToInt32(p.X)].Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); // FromArgb(alpha, red, green, blue)
            casillas2[Convert.ToInt32(p.Y), Convert.ToInt32(p.X)].Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)); // FromArgb(alpha, red, green, blue)

        } // pintar rojo --> fase 0

        private void button3_Click(object sender, RoutedEventArgs e) // crear rejilla
        {
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            button4.IsEnabled = true;
            button5.IsEnabled = true;
            botonCARGAR.IsEnabled = true;

            canvas1.Children.Clear();
            canvas2.Children.Clear();

            try
            {
                x = Convert.ToInt32(TextBoxX.Text);
                y = Convert.ToInt32(TextBoxY.Text);
                matriz_celdas.SetNumeroDeFilasYColumnas(y, x);  // es crea matriu i somple de cell
                if ((x <= 0) || (y <= 0))
                {
                    x = 10;
                    y = 10;
                    matriz_celdas.SetNumeroDeFilasYColumnas(y, x);
                }
                
            }
            catch
            {
                x = 10;
                y = 10;
                matriz_celdas.SetNumeroDeFilasYColumnas(y, x);
            }

            generarMalla1();
            generarMalla2();
            
        }

        private void generarMalla1()
        {
            casillas = new Rectangle[y, x];

            canvas1.Height = y * 15;
            canvas1.Width = x * 15;
            

            // Bucle para crear los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Rectangle b = new Rectangle();
                    b.Width = 15;
                    b.Height = 15;
                    b.Fill = new SolidColorBrush(Colors.White);
                    b.StrokeThickness = 0.5;
                    b.Stroke = Brushes.Black;
                    canvas1.Children.Add(b);

                    // Posicion del cuadrado
                    Canvas.SetTop(b, (i - 1) * 15);
                    Canvas.SetLeft(b, (j - 1) * 15);
                    b.Tag = new Point(j, i);

                    b.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);

                    casillas[i, j] = b;
                }
            }

             
        }

        private void generarMalla2()
        {
            casillas2 = new Rectangle[y, x];

            canvas2.Height = y * 15;
            canvas2.Width = x * 15;


            // Bucle para crear los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Rectangle b = new Rectangle();
                    b.Width = 15;
                    b.Height = 15;
                    b.Fill = new SolidColorBrush(Colors.White);
                    b.StrokeThickness = 0.5;
                    b.Stroke = Brushes.Black;
                    canvas2.Children.Add(b);

                    // Posicion del cuadrado
                    Canvas.SetTop(b, (i - 1) * 15);
                    Canvas.SetLeft(b, (j - 1) * 15);
                    b.Tag = new Point(j, i);

                    b.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);

                    casillas2[i, j] = b;
                }
            }


        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e) // cargar fichero
        {
            try
            {
                canvas1.Children.Clear();
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Filter = "Text documents (.txt)|*.txt";
                Nullable<bool> result = ofd.ShowDialog();

                if (result == true)
                {
                    // Cargar documento
                    string filename = ofd.FileName;
                    Malla matriz = matriz_celdas.CargarSimulacion(filename);
                    matriz_celdas = matriz;
                    x = matriz_celdas.getX()-2;
                    y = matriz_celdas.getY()-2;

                    generarMallaEnCARGA();

                    MessageBox.Show("Fichero cargado con éxito!");

                    button1.IsEnabled = true;
                    button2.IsEnabled = true;
                    button4.IsEnabled = true;
                    button5.IsEnabled = true;
                    botonCARGAR.IsEnabled = true;

                    

                }
                else
                { MessageBox.Show("No ha sido posible cargar la simulación"); }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void generarMallaEnCARGA()
        {
            casillas = new Rectangle[y, x];

            canvas1.Height = y * 15;
            canvas1.Width = x * 15;


            // Bucle para crear los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Rectangle b = new Rectangle();
                    b.Width = 15;
                    b.Height = 15;
                    b.Fill = new SolidColorBrush(Colors.Gray);
                    b.StrokeThickness = 0.5;
                    b.Stroke = Brushes.Black;
                    canvas1.Children.Add(b);

                    // Posicion del cuadrado
                    Canvas.SetTop(b, (i - 1) * 15);
                    Canvas.SetLeft(b, (j - 1) * 15);
                    b.Tag = new Point(j, i);

                    b.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);

                    casillas[i, j] = b;
                }
            }

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                    if (matriz_celdas.DameElEstadoDe(i + 1, j + 1) == false)
                    { casillas[i, j].Fill = new SolidColorBrush(Colors.Gray); }
                    if (matriz_celdas.DameElEstadoDe(i + 1, j + 1) == true)
                    { casillas[i, j].Fill = new SolidColorBrush(Colors.Black); }

                }
            }
            
        } // hay que modificar para que se cargen las dos

        private void button1_Click(object sender, RoutedEventArgs e) // simular paso a paso
        {

            matriz_celdas.SetNormas(norm);
            historial.Add(matriz_celdas);
            historial.Last().MallaFutura(); // actualizamos


            // volvemos a pintar los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                   
                        double fase = historial.Last().DameFASEde(i+1,j+1); // estará entre 1 y 0
                        double temperatura = historial.Last().DameTEMPERATURAde(i + 1, j + 1); // estará entre -1 y 0

                        if (fase == 1)
                        { casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(0,0,0,0)); }
                        if (fase !=1)
                        {
                         byte alpha = Convert.ToByte((-255+60)*(fase-1)+60); // provamos para que se vea bien y establecemos 1 a 40 y 0 a 255, mirar de ajustar bien

                        casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 255, 0, 0));  
                        }

                        if (temperatura == -1)
                        { casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)); }
                        if (temperatura != -1)
                        {
                            byte alpha = Convert.ToByte((255-40)*(temperatura+1)+40); // provamos para que se vea bien y establecemos 1 a 40 y 0 a 255, mirar de ajustar bien

                            casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 0, 255, 0));
                        }

                       

                }
            }


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            matriz_celdas.SetNormas(norm);
            historial.Add(matriz_celdas);
            historial.Last().MallaFutura(); // actualizamos


            // volvemos a pintar los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {


                    double fase = historial.Last().DameFASEde(i + 1, j + 1); // estará entre 1 y 0
                    double temperatura = historial.Last().DameTEMPERATURAde(i + 1, j + 1); // estará entre -1 y 0

                    if (fase == 1)
                    { casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)); }
                    if (fase != 1)
                    {
                        byte alpha = Convert.ToByte((-255 + 60) * (fase - 1) + 60); // provamos para que se vea bien y establecemos 1 a 40 y 0 a 255, mirar de ajustar bien

                        casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 255, 0, 0));
                    }

                    if (temperatura == -1)
                    { casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)); }
                    if (temperatura != -1)
                    {
                        byte alpha = Convert.ToByte((255 - 40) * (temperatura + 1) + 40); // provamos para que se vea bien y establecemos 1 a 40 y 0 a 255, mirar de ajustar bien

                        casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 0, 255, 0));
                    }


                }
            }
        }
        
        private void button2_Click(object sender, RoutedEventArgs e) // simulación automatica
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void button4_Click(object sender, RoutedEventArgs e) // stop
        {
            dispatcherTimer.Stop();
        }

        private void button5_Click(object sender, RoutedEventArgs e) //restart
        {
            List<Malla> reset_historial = new List<Malla>();
            historial = reset_historial;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                    matriz_celdas.SetNumeroDeFilasYColumnas(y, x);
                    matriz_celdas.SetFaseDeCelda(i, j, 1);
                    matriz_celdas.SetTemperaturaDeCelda(i, j, -1);
                    matriz_celdas.SetVidaDeCelda(i, j, false);
                    casillas[i, j].Fill = new SolidColorBrush(Colors.White);
                    casillas2[i, j].Fill = new SolidColorBrush(Colors.White);
                    
                }
            }
        }

        private void Parametros_Click(object sender, RoutedEventArgs e)
        {
            norm.SetDxDy(Convert.ToDouble(dxdy.Text));
            norm.SetEpsilon(Convert.ToDouble(epsilon.Text));
            norm.SetBetta(Convert.ToDouble(betta.Text));
            norm.SetDelta(Convert.ToDouble(delta.Text));
            norm.SetM(Convert.ToDouble(M.Text));
            norm.SetDT(Convert.ToDouble(dt.Text));
            
            if (ParametrosA.IsChecked == true)
            { 

            norm.SetDxDy(0.005);
            norm.SetEpsilon(0.005);
            norm.SetBetta(400);
            norm.SetDelta(0.5);
            norm.SetM(20);
            norm.SetDT(5*Math.Pow(10,-6));
            
            }

            if (ParametrosB.IsChecked == true)
            { 
            norm.SetDxDy(0.005);
            norm.SetEpsilon(0.005);
            norm.SetBetta(300);
            norm.SetDelta(0.7);
            norm.SetM(30);
            norm.SetDT(5 * Math.Pow(10, -6));
                      
            }

            MessageBox.Show("Datos cargados");
        }

        private void ParametrosB_Checked(object sender, RoutedEventArgs e)
        {
            if (ParametrosB.IsChecked == true)
            {
                ParametrosA.IsChecked = false;

                dxdy.Text = Convert.ToString(0.005);
                epsilon.Text = Convert.ToString(0.005);
                betta.Text = Convert.ToString(300);
                delta.Text = Convert.ToString(0.7);
                M.Text = Convert.ToString(30);
                double c = 5 * Math.Pow(10, -6);
                dt.Text = Convert.ToString(c);

            }
        }

        private void ParametrosA_Checked(object sender, RoutedEventArgs e)
        {
            if (ParametrosA.IsChecked == true)
            {
                ParametrosB.IsChecked = false;

                dxdy.Text = Convert.ToString(0.005);
                epsilon.Text = Convert.ToString(0.005);
                betta.Text = Convert.ToString(400);
                delta.Text = Convert.ToString(0.5);
                M.Text = Convert.ToString(20);
                double c = 5 * Math.Pow(10, -6);
                dt.Text = Convert.ToString(c);
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e) // condicions de contorn
        {
         
            matriz_celdas.SetCondicionsContornoFaseTemperatura(comboBox1.SelectedItem.ToString());
            MessageBox.Show("Se han establecido las condiciones de contorno");

        }

        private void button7_Click(object sender, RoutedEventArgs e) // creo que no hace nada
        {

        }

        private void Boton_Retroceder(object sender, RoutedEventArgs e)
        {
            int maxPos = historial.Count;

            int minPos = 0;

            int currPos = maxPos -1;

            while ()

            historial[numero_iteraciones].GetMatriz();


        }
       


        
    }
}

