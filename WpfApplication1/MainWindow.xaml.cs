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
        // ATRIBUTOS
        Normas norm = new Normas(); // Clase donde hay guardados todos los parámetros de la simulación
        Rectangle[,] casillas; // Matriz donde guardaremos todos los rectangulos para poder recorrerlos referidos a la fase
        Rectangle[,] casillas2; // Matriz donde guardaremos todos los rectangulos para poder recorrerlos referidos a la temperatura
        int x;  //columnas de la malla a generar (valor introducido en el formulario)
        int y;  //filas de la malla a generar (valor introducido en el formulario)

        Malla matriz_celdas = new Malla(); // Matriz con la que estaremos trabajando en la interación presente
        List<Malla> historial= new List<Malla>(); // Historial donde se van guardando los pasos de las simulaciones anteriores
        DispatcherTimer dispatcherTimer = new DispatcherTimer(); //Timer para la simulación automática

        
        public MainWindow()
        {
            InitializeComponent();

        }

        //GUARDAR FICHERO
        private void MenuItem_Click_2(object sender, RoutedEventArgs e) 
        {

        //    // Configure save file dialog box
        //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        //    dlg.FileName = "Simulación"; // Default file name
        //    dlg.DefaultExt = ".txt"; // Default file extension
        //    dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

        //    // Show save file dialog box
        //    Nullable<bool> result = dlg.ShowDialog();

        //    // Process save file dialog box results
        //    if (result == true)
        //    {
        //        // Save document
        //        string filename = dlg.FileName;
        //        int n = matriz_celdas.GuardarSimulacion(filename);
        //        if (n == 0)
        //        { MessageBox.Show("Simulación guardada correctamente!"); }
        //        else
        //        { MessageBox.Show("No ha sido posible guardar la simulación"); }
        //    }
        //    else
        //    { MessageBox.Show("No ha sido posible guardar la simulación"); }



        }


        // CLICAR ENCIMA DE UNA CASILLA
            // cuando clicamos encima de una casilla esta se establecerá con fase y temperatura 0 y el color correspondiente 
            // Para determinar el color se ha utilizado FromArgb(alpha, red, green, blue), donde alpha es la transparencia que iremos
            // variando para lograr las distintas tonalidades de fase y temperatura
        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            Point p = (Point)a.Tag; // obtenemos el punto donde se ha clicado

            // el punto que obtenemos x,y es respecto la matriz de casillas (lo que se muestra), al trabajar con la matriz_celda hay que trabajar con x+1,y+1
            // ya que en esta matriz hay un contorno definido para las condiciones de contorno

            matriz_celdas.SetTemperaturaDeCelda(Convert.ToInt32(p.Y) + 1, Convert.ToInt32(p.X) + 1, 0); // definimos que en ese punto hay temperatura 0
            matriz_celdas.SetFaseDeCelda(Convert.ToInt32(p.Y) + 1, Convert.ToInt32(p.X) + 1, 0); // definimos que en ese punto hay fase 0

            casillas[Convert.ToInt32(p.Y), Convert.ToInt32(p.X)].Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); // Para fase 0 definimos color rojo completamente opaco
            casillas2[Convert.ToInt32(p.Y), Convert.ToInt32(p.X)].Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)); // Para temperatura verde elejimos color verde completamente opaco

            historial.Add(matriz_celdas.ClonarParaLISTA()); // Cada nuevo clic es añadido al historial por si quisieramos retroceder a algún estado anterior donde no se ha clicado una casilla 
        }

        // CUANDO PASAMOS EL RATÓN POR ENCIMA DE UNA CASILLA
            // para esta función se han generado dos labels en los que se iran mostrando las fases y la temperatura de la celda en la que 
            //tengamos el raton encima
        private void rectangle_MouseEnter(object sender, EventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            Point p = (Point)a.Tag; // obtenemos el punto donde esta el ratón

            // escribimos en las labels el valor de la fase y de la temperatura, teniendo en cuenta que en la matriz seran los puntos x+1,y+1

            labelFase.Text = Convert.ToString(matriz_celdas.DameFASEde(Convert.ToInt32(p.Y) + 1, Convert.ToInt32(p.X) + 1)); // valor de fase
            labelTemperatura.Text = Convert.ToString(matriz_celdas.DameTEMPERATURAde(Convert.ToInt32(p.Y) + 1, Convert.ToInt32(p.X) + 1)); // valor de temperatura
        }
     

        // CREAMOS LA MATRIZ/REJILLA AL CLICAR AL BOTON DE CREAR
            //Una vez hemos definido las columnas y filas manualmente, clicamos al boton de crear y generamos el grid de rectangulos
        private void button3_Click(object sender, RoutedEventArgs e) 
        {
            
            try
            {
                // Nos aseguramos antes de generar la matriz que no haya otra matriz ya establecida. Para ello eliminamos lo que tengamos
                // en los canvas 1 y 2 para evitar problemas de generar mallas encima de otras mallas
                canvas1.Children.Clear();
                canvas2.Children.Clear();

                x = Convert.ToInt32(TextBoxX.Text); // Guardamos el numero de columnas introducidas en la variable x
                y = Convert.ToInt32(TextBoxY.Text); // Guardamos el numero de filas introducidas en la variable y
                matriz_celdas.SetNumeroDeFilasYColumnas(y, x);  // definimos la matriz llamando la función de la clase Malla
               
                // Generamos la excepción de que cuando se haya introducido un numero menor o igual a 0 el programa de el aviso
                // pero igualmente nos genere una matriz 'defecto' de 10x10 para poder simular si el usuario no lo cambia
                if ((x <= 0) || (y <= 0))
                {
                    x = 10;
                    y = 10;
                    matriz_celdas.SetNumeroDeFilasYColumnas(y, x);
                    MessageBox.Show("Error. Los valores han de ser positivos/ distintos a 0. Por favor, vuelva " +
                        "a introducir los parámetros o realize la simulación con la matriz creada por defecto de 10x10");
                }

                // Abilitamos los textboxs y botones correspondientes a los valores que se deben introducir a continuación
                // para poder realizar la simulación
                betta.IsEnabled = true;
                dxdy.IsEnabled = true;
                epsilon.IsEnabled = true;
                delta.IsEnabled = true;
                M.IsEnabled = true;
                dt.IsEnabled = true;
                ParametrosA.IsEnabled = true;
                ParametrosB.IsEnabled = true;
                Parametros.IsEnabled = true;

            }
            catch
            {
                // Generamos excepción con datos incorrectos, como seria una letra. Generamos igualmente la matriz defecto 
                // y avisamos del problema
                x = 10;
                y = 10;
                matriz_celdas.SetNumeroDeFilasYColumnas(y, x);
                MessageBox.Show("Error en la introducción de los valores. Por favor, vuelva " +
                        "a introducir los parámetros o realize la simulación con la matriz creada por defecto de 10x10");
            }

            // Llamamos a la funcion que nos crea los rectángulos de las matrizes
            generarMalla1(casillas, canvas1); // introducimos como parametros la matriz casillas y canvas1 (corresponden a la fase)
            generarMalla1(casillas2, canvas2);// introducimos como parametros la matriz casillas2 y canvas2 (corresponden a la temperatura)
        }

        private void generarMalla1(Rectangle[,] c, Canvas ca)
        {
            // establecemos las dimensiones de la matriz de las casillas y las alturas en el canvas
            c = new Rectangle[y, x]; 
            ca.Height = y * 15;
            ca.Width = x * 15; 

            // Bucle para crear los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    // creamos un nuevo rectangulo y definimos sus propiedades
                    Rectangle b = new Rectangle();
                    b.Width = 15;
                    b.Height = 15;
                    b.Fill = new SolidColorBrush(Colors.White);
                    b.StrokeThickness = 0.5;
                    b.Stroke = Brushes.Black;
                    ca.Children.Add(b);// añadimos el rectangulo al canvas

                    // Posición del cuadrado
                    Canvas.SetTop(b, (i - 1) * 15);
                    Canvas.SetLeft(b, (j - 1) * 15);
                    b.Tag = new Point(j, i);

                    // definimos los eventos que tiene el rectangulo: clicar y pasar por encima
                    b.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);
                    b.MouseEnter += new System.Windows.Input.MouseEventHandler(rectangle_MouseEnter);
                    
                    c[i, j] = b; // guardamos el rectangulo en su posición i,j de la matriz de casillas
                }
            }
        }

     
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) // cargar fichero
        {

        //    try
        //    {
        //        canvas1.Children.Clear();
        //        OpenFileDialog ofd = new OpenFileDialog();
        //        ofd.Multiselect = true;
        //        ofd.Filter = "Text documents (.txt)|*.txt";
        //        Nullable<bool> result = ofd.ShowDialog();

        //        if (result == true)
        //        {
        //            // Cargar documento
        //            string filename = ofd.FileName;
        //            Malla matriz = matriz_celdas.CargarSimulacion(filename);
        //            matriz_celdas = matriz;
        //            x = matriz_celdas.getX()-2;
        //            y = matriz_celdas.getY()-2;

        //            generarMallaEnCARGA();

        //            MessageBox.Show("Fichero cargado con éxito!");

        //            button1.IsEnabled = true;
        //            button2.IsEnabled = true;
        //            button4.IsEnabled = true;
        //            button5.IsEnabled = true;
        //            botonCARGAR.IsEnabled = true;




        //        }
        //        else
        //        { MessageBox.Show("No ha sido posible cargar la simulación"); }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        }


        //private void generarMallaEnCARGA()
        //{
        //    casillas = new Rectangle[y, x];

        //    canvas1.Height = y * 15;
        //    canvas1.Width = x * 15;


        //    // Bucle para crear los rectangulos
        //    for (int i = 0; i < y; i++)
        //    {
        //        for (int j = 0; j < x; j++)
        //        {
        //            Rectangle b = new Rectangle();
        //            b.Width = 15;
        //            b.Height = 15;
        //            b.Fill = new SolidColorBrush(Colors.Gray);
        //            b.StrokeThickness = 0.5;
        //            b.Stroke = Brushes.Black;
        //            canvas1.Children.Add(b);

        //            // Posicion del cuadrado
        //            Canvas.SetTop(b, (i - 1) * 15);
        //            Canvas.SetLeft(b, (j - 1) * 15);
        //            b.Tag = new Point(j, i);

        //            b.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);

        //            casillas[i, j] = b;
        //        }
        //    }

        //    for (int i = 0; i < y; i++)
        //    {
        //        for (int j = 0; j < x; j++)
        //        {

        //            if (matriz_celdas.DameElEstadoDe(i + 1, j + 1) == false)
        //            { casillas[i, j].Fill = new SolidColorBrush(Colors.Gray); }
        //            if (matriz_celdas.DameElEstadoDe(i + 1, j + 1) == true)
        //            { casillas[i, j].Fill = new SolidColorBrush(Colors.Black); }

        //        }
        //    }
            
        //} // hay que modificar para que se cargen las dos

        private void volverApintar()
        {
            // volvemos a pintar los rectangulos 1
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {

                    double fase = matriz_celdas.DameFASEde(i + 1, j + 1); // estará entre 1 y 0
                    double temperatura = matriz_celdas.DameTEMPERATURAde(i + 1, j + 1); // estará entre -1 y 0

                    if (fase == 1)
                    { casillas[i, j].Fill = new SolidColorBrush(Colors.White); }

                    if ((fase != 0) && (fase != 1))
                    {
                        byte alpha = Convert.ToByte(255 - Math.Round(fase * 10) * 23); // truncamos los valores y en funcion de esto establecemos una alpha

                        casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 255, 0, 0));
                    }

                    if (temperatura == -1)
                    { casillas2[i, j].Fill = new SolidColorBrush(Colors.White); }
                    if ((temperatura != 0) && (temperatura != -1))
                    {
                        byte alpha = Convert.ToByte(255 + Math.Round(temperatura * 10) * 23);

                        casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 0, 255, 0));
                    }



                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) // simular paso a paso
        {

            if (historial.Count < 1)
            {
                historial.Add(matriz_celdas.ClonarParaLISTA());

                volverApintar();
            }//si esta vacio

                matriz_celdas.MallaFutura(); // actualizamos
                historial.Add(matriz_celdas.ClonarParaLISTA());

            volverApintar();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (historial.Count < 1)
            {
                historial.Add(matriz_celdas.ClonarParaLISTA());

                volverApintar();
            }//si esta vacio

            matriz_celdas.MallaFutura(); // actualizamos
            historial.Add(matriz_celdas.ClonarParaLISTA());

            volverApintar();

        }

        private void button2_Click(object sender, RoutedEventArgs e) // simulación automatica
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
 
            dispatcherTimer.Start();

        }

        private void button4_Click(object sender, RoutedEventArgs e) // stop
        {
            dispatcherTimer.Stop();
            MessageBox.Show("Se ha detenido la simulación");
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
                    casillas[i, j].Fill = new SolidColorBrush(Colors.White);
                    casillas2[i, j].Fill = new SolidColorBrush(Colors.White);

                }
            }
        }

        private void Parametros_Click(object sender, RoutedEventArgs e)
        {
            

            try
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
                    norm.SetDT(5 * Math.Pow(10, -6));

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

                comboBox1.IsEnabled = true;
                button6.IsEnabled = true;

                MessageBox.Show("Datos cargados");
            }
            
            catch { MessageBox.Show("Error en los parametros"); }
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
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Porfavor, seleciona una de las opciones del desplegable"); 
            }
            else { 
                matriz_celdas.SetCondicionsContornoFaseTemperatura(comboBox1.SelectedItem.ToString());
                MessageBox.Show("Se han establecido las condiciones de contorno");
                matriz_celdas.SetNormas(norm);

                button1.IsEnabled = true;
                button2.IsEnabled = true;
                button4.IsEnabled = true;
                button5.IsEnabled = true;
                botonCARGAR.IsEnabled = true;
                slider1.IsEnabled = true;
                boton_retroceder.IsEnabled = true;
                }
        }

        private void MenuItem_Click_20(object sender, RoutedEventArgs e) // click en el primer graff
        {
            int contadorHISTORIAL = historial.Count;
            List<double> listaFasexIteracion = new List<double>();
            List<double> listaTEMPxIteracion = new List<double>();

            for (int k = 0; k < contadorHISTORIAL; k++)
            {
                listaFasexIteracion.Add(historial[k].GetcantidadFase());
                listaTEMPxIteracion.Add(historial[k].GetcantidadTEMP());
            }

            graficosPage lc = new graficosPage();

            lc.SetcontadorHIST(contadorHISTORIAL);
            lc.SetListaFASExIteracion(listaFasexIteracion);
            lc.SetListaTEMPxIteracion(listaTEMPxIteracion);
            // hem de anar ageneradora

            lc.ShowDialog();

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(Convert.ToInt32(e.NewValue));
            dispatcherTimer.Start();

        }

        private void boton_retroceder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                boton_retroceder.IsEnabled = true;
                historial.RemoveAt(historial.Count() - 1);
                
                matriz_celdas.SetMatriz(historial.Last().ClonarParaLISTA().GetMatriz());

                volverApintar();
            }

            catch
            { MessageBox.Show("No es posible retroceder mas"); }
        }//Boton retroceder

    }

}

