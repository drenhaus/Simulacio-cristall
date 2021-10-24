using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using Microsoft.Win32;

namespace WpfApplication1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ATRIBUTOS
        Normas norm = new Normas(); // Clase donde hay guardados todos los parámetros de la simulación
        Rectangle[,] casillas; // Matriz donde guardaremos todos los rectangulos para poder recorrerlos referidos a la fase
        Rectangle[,] casillas2; // Matriz donde guardaremos todos los rectangulos para poder recorrerlos referidos a la temperatura
        int x;  //columnas de la malla a generar (valor introducido en el formulario)
        int y;  //filas de la malla a generar (valor introducido en el formulario)

        Malla matriz_celdas = new Malla(); // Matriz con la que estaremos trabajando en la interación presente
        List<Malla> historial = new List<Malla>(); // Historial donde se van guardando los pasos de las simulaciones anteriores
        DispatcherTimer dispatcherTimer = new DispatcherTimer(); //Timer para la simulación automática


        public MainWindow()
        {
            InitializeComponent();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
            boxIteration.Text = Convert.ToString(historial.Count());
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
            // Nos aseguramos antes de generar la matriz que no haya otra matriz ya establecida. Para ello eliminamos lo que tengamos
            // en los canvas 1 y 2 para evitar problemas de generar mallas encima de otras mallas
            canvas1.Children.Clear();
            canvas2.Children.Clear();

            List<Malla> reset_historial = new List<Malla>();
            historial = reset_historial; // vaciamos el historial

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
            boton_retroceder.IsEnabled = true; // permite retroceder al estar clicando
            button5.IsEnabled = true;

            boxIteration.Text = Convert.ToString(historial.Count());

            try
            {
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

                    TextBoxY.Text = Convert.ToString(10);
                    TextBoxX.Text = Convert.ToString(10);

                    MessageBox.Show("Error. Los valores han de ser positivos/ distintos a 0. Por favor, vuelva " +
                        "a introducir los parámetros o realize la simulación con la matriz creada por defecto de 10x10");




                }
            }
            catch
            {
                // Generamos excepción con datos incorrectos, como seria una letra. Generamos igualmente la matriz defecto 
                // y avisamos del problema
                x = 10;
                y = 10;
                matriz_celdas.SetNumeroDeFilasYColumnas(y, x);

                TextBoxY.Text = Convert.ToString(10);
                TextBoxX.Text = Convert.ToString(10);

                MessageBox.Show("Error en la introducción de los valores. Por favor, vuelva " +
                        "a introducir los parámetros o realize la simulación con la matriz creada por defecto de 10x10");


            }

            // Llamamos a la funcion que nos crea los rectángulos de las matrizes
            this.casillas = generarMalla1(casillas, canvas1); // introducimos como parametros la matriz casillas y canvas1 (corresponden a la fase)
            this.casillas2 = generarMalla1(casillas2, canvas2);// introducimos como parametros la matriz casillas2 y canvas2 (corresponden a la temperatura)
        }

        // GENERAMOS LOS RECTANGULOS DE LA MATRIX
        // le introducimos como parametros si se trata de los rectangulos de fase o temperatura (casillas o casillas2) y en que canvas 
        // vamos a trabajar. Nos retorna la matriz de las casillas para que podemos definirla posteriormente como el atributo
        private Rectangle[,] generarMalla1(Rectangle[,] c, Canvas ca)
        {
            // establecemos las dimensiones de la matriz de las casillas y las alturas en el canvas
            c = new Rectangle[y, x];
            ca.Height = canvas1.Height;
            ca.Width = canvas1.Width;

            // Bucle para crear los rectangulos
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    // creamos un nuevo rectangulo y definimos sus propiedades
                    Rectangle b = new Rectangle();
                    b.Width = canvas1.Width / y;
                    b.Height = canvas1.Height / x;
                    b.Fill = new SolidColorBrush(Colors.White);
                    b.StrokeThickness = 0.5;
                    b.Stroke = Brushes.Black;
                    ca.Children.Add(b);// añadimos el rectangulo al canvas

                    // Posición del cuadrado

                    Canvas.SetTop(b, i * canvas1.Height / x);
                    Canvas.SetLeft(b, j * canvas1.Width / y);
                    b.Tag = new Point(j, i);

                    // definimos los eventos que tiene el rectangulo: clicar y pasar por encima
                    b.MouseDown += new MouseButtonEventHandler(rectangle_MouseDown);
                    b.MouseEnter += new System.Windows.Input.MouseEventHandler(rectangle_MouseEnter);

                    c[i, j] = b; // guardamos el rectangulo en su posición i,j de la matriz de casillas
                }
            }
            return c;

        }

        //FUNCION QUE PINTA LA MATRIZ ACTUAL
        // esta funcion recorre todos los rectangulos de casillas y casillas2 y actualiza el color que tienen
        private void volverApintar()
        {
            boxIteration.Text = Convert.ToString(historial.Count());
            // volvemos a pintar los rectangulos de la matriz CASILLAS
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    double fase = matriz_celdas.DameFASEde(i + 1, j + 1); // nos guardamos la fase que estará entre 1 y 0
                    double temperatura = matriz_celdas.DameTEMPERATURAde(i + 1, j + 1); // guardamos la temperatura que estará entre -1 y 0

                    // si la fase es 1 se establece como color el blanco y si la fase es 0 se establece el color rojo opaco
                    if (fase == 1)
                    { casillas[i, j].Fill = new SolidColorBrush(Colors.White); }
                    if (fase == 0)
                    { casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); }

                    // para los otros valores multiplicamos por diez la fase y luego por 23 (de esta manera obtenemos un valor escalado dentro
                    // del margen de 0-255) 
                    // Este valor lo redondeamos y se lo restamos a 255 y asi obtenemos la opacidad de dicha fase, situando fase 0 a 255 y fase 1 a 0

                    if ((fase != 0) && (fase != 1))
                    {
                        byte alpha = Convert.ToByte(255 - Math.Round(fase * 10) * 23);

                        casillas[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 255, 0, 0));
                    }

                    // si la temperatura es -1 se establece como color el blanco, y si es 0 se establece el color verde opaco
                    if (temperatura == -1)
                    { casillas2[i, j].Fill = new SolidColorBrush(Colors.White); }
                    if (temperatura == 0)
                    { casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)); }

                    // establecemos la misma relación que en la fase para obtener el color de la temperatura, teniendo en cuenta que hay valores negativos
                    if ((temperatura != 0) && (temperatura != -1))
                    {
                        byte alpha = Convert.ToByte(255 + Math.Round(temperatura * 10) * 23);
                        casillas2[i, j].Fill = new SolidColorBrush(Color.FromArgb(alpha, 0, 255, 0));
                    }
                }
            }
        }

        // SIMULACIÓN PASO A PASO
        // cada vez que clicamos al boton de simulación paso a paso calcula la matriz futura y actualiza los colores
        // de los rectángulos
        private void button1_Click(object sender, RoutedEventArgs e) // simular paso a paso
        {
            // si el historial está vacio añadimos la matriz actual al historial y la pintamos
            if (historial.Count < 1)
            {
                historial.Add(matriz_celdas.ClonarParaLISTA());
                volverApintar();
            }

            matriz_celdas.MallaFutura(); // calculamos la matriz futura
            historial.Add(matriz_celdas.ClonarParaLISTA()); // añadimos la matriz futura al historial

            volverApintar(); // pintamos la nueva matriz
        }

        // TICK DE RELOJ
        // cada tick de reloj se actualizará la malla y se volverá a pintar
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // si el historial está vacio añadimos la matriz actual al historial y la pintamos
            if (historial.Count < 1)
            {
                historial.Add(matriz_celdas.ClonarParaLISTA());
                volverApintar();
            }

            matriz_celdas.MallaFutura(); // calculamos la matriz futura
            historial.Add(matriz_celdas.ClonarParaLISTA()); // añadimos la matriz futura al historial

            volverApintar(); // pintamos la nueva matriz
        }

        // SIMULACIÓN AUTOMÁTICA
        // cuando se preme el boton de simulacion automática se inicializa el timer
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);// por defecto establecemos una simulación cada segundo
            dispatcherTimer.Start();
        }

        // DETENER LA SIMULACIÓN AUTOMÁTICA
        //Detenemos el timer e indicamos mediante un MessageBox que se ha detenido la simulación
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            MessageBox.Show("Se ha detenido la simulación");
        }

        // BOTON RESTART
        //reseteamos la simulación y vaciamos el historial
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            List<Malla> reset_historial = new List<Malla>();
            historial = reset_historial; // vaciamos el historial
            boxIteration.Text = Convert.ToString(historial.Count());

            // volvemos a crear la matriz y establecemos que todas las fases son 1 y temperaturas -1
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

        // CARGAR PARÁMETROS
        // Cuando clicamos el boton de cargar parametros definimos los distintos 
        // valores de la clase norma.
        // Definimos que se pueda elejir también entre dos conjuntos de parámetros: A y B
        private void Parametros_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // guardamos los valores introducidos
                norm.SetDxDy(Convert.ToDouble(dxdy.Text));
                norm.SetEpsilon(Convert.ToDouble(epsilon.Text));
                norm.SetBetta(Convert.ToDouble(betta.Text));
                norm.SetDelta(Convert.ToDouble(delta.Text));
                norm.SetM(Convert.ToDouble(M.Text));
                norm.SetDT(Convert.ToDouble(dt.Text));

                if (ParametrosA.IsChecked == true) // Parámetros A
                {
                    norm.SetDxDy(0.005);
                    norm.SetEpsilon(0.005);
                    norm.SetBetta(400);
                    norm.SetDelta(0.5);
                    norm.SetM(20);
                    norm.SetDT(5 * Math.Pow(10, -6));
                }

                if (ParametrosB.IsChecked == true) // Parámetros B
                {
                    norm.SetDxDy(0.005);
                    norm.SetEpsilon(0.005);
                    norm.SetBetta(300);
                    norm.SetDelta(0.7);
                    norm.SetM(30);
                    norm.SetDT(5 * Math.Pow(10, -6));

                }

                // Abilitamos los botones para poder seguir con la simulación
                comboBox1.IsEnabled = true;
                button6.IsEnabled = true;

                MessageBox.Show("Datos cargados"); // informamos con un MessageBox que se han cargado los parámetros
            }

            // si hay error en los parámetros 
            catch { MessageBox.Show("Error en los parametros"); }
        }

        // SELECIONAMOS LOS PARÁMETROS B
        //cuando tenemos selecionados los parametros B escribimos en los textbox los valores de 
        // estos para poder consultarlos
        // Ponemos también una condición que nos permita solo selecionar A o B, es decir, que al 
        // selecionar B se deselecione A
        private void ParametrosB_Checked(object sender, RoutedEventArgs e)
        {
            if (ParametrosB.IsChecked == true)
            {
                ParametrosA.IsChecked = false; // deselecionamos A

                //escribimos los parametros para poder verlos
                dxdy.Text = Convert.ToString(0.005);
                epsilon.Text = Convert.ToString(0.005);
                betta.Text = Convert.ToString(300);
                delta.Text = Convert.ToString(0.7);
                M.Text = Convert.ToString(30);
                double c = 5 * Math.Pow(10, -6);
                dt.Text = Convert.ToString(c);
            }
        }

        // SELECIONAMOS LOS PARÁMETROS A
        //cuando tenemos selecionados los parametros A escribimos en los textbox los valores de 
        // estos para poder consultarlos
        // Ponemos también una condición que nos permita solo selecionar A o B, es decir, que al 
        // selecionar A se deselecione B
        private void ParametrosA_Checked(object sender, RoutedEventArgs e)
        {
            if (ParametrosA.IsChecked == true)
            {
                ParametrosB.IsChecked = false; // deselecionamos B

                //escribimos los parametros para poder verlos
                dxdy.Text = Convert.ToString(0.005);
                epsilon.Text = Convert.ToString(0.005);
                betta.Text = Convert.ToString(400);
                delta.Text = Convert.ToString(0.5);
                M.Text = Convert.ToString(20);
                double c = 5 * Math.Pow(10, -6);
                dt.Text = Convert.ToString(c);
            }
        }


        // ESTABLECER CONDICIONES DE CONTORNO
        //creamos un comboBox que nos deje elegir entre dos opciones de condiciones de contorno
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedItem == null) // si no se ha selecionado nada nos salta un mensaje de error
            {
                MessageBox.Show("Porfavor, seleciona una de las opciones del desplegable");
            }
            else
            {
                // establecemos condiciones de contorno
                matriz_celdas.SetCondicionsContornoFaseTemperatura(comboBox1.SelectedItem.ToString());
                MessageBox.Show("Se han establecido las condiciones de contorno");
                // establecemos las normas
                matriz_celdas.SetNormas(norm);
                // abilitamos todos los botones de simulación
                button1.IsEnabled = true;
                button2.IsEnabled = true;
                button4.IsEnabled = true;
                button5.IsEnabled = true;
                botonCARGAR.IsEnabled = true;
                slider1.IsEnabled = true;
                boton_retroceder.IsEnabled = true;
            }
        }

        // CLICAMOS EN EL MENÚ DE CREAR GRAFICO
        //generamos una lista con los valores de fase y temperatura que hemos guardado en el historial
        // y esta lista la entregamos a la clase de graficos
        private void MenuItem_Click_20(object sender, RoutedEventArgs e)
        {
            int contadorHISTORIAL = historial.Count;
            List<double> listaFasexIteracion = new List<double>(); // lista de Fases
            List<double> listaTEMPxIteracion = new List<double>(); // lista de Temperaturas

            for (int k = 0; k < contadorHISTORIAL; k++) // vamos calculando los valores medios de fase y temperatura
            {
                listaFasexIteracion.Add(historial[k].GetcantidadFase());
                listaTEMPxIteracion.Add(historial[k].GetcantidadTEMP());
            }
            // Abrimos una nueva ventana para mostrar los graficos

            graficosPage lc = new graficosPage();
            lc.SetcontadorHIST(contadorHISTORIAL); // le damos el numero de iteraciones
            lc.SetListaFASExIteracion(listaFasexIteracion); // introducimos las fases
            lc.SetListaTEMPxIteracion(listaTEMPxIteracion); // introducimos las temperaturas
            lc.ShowDialog();

        }

        // VARIAMOS EL SLIDER DE VELOCIDAD
        //Establecemos el nuevo valor del slider como el timespan del timer
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(Convert.ToInt32(e.NewValue));

        }

        // BOTON RETROCEDER
        //cada vez que le damos al boton retroceder eliminamos un elemento del historial, actualizamos
        // la matriz con la que estamos trabajando y la volvemos a pintar
        private void boton_retroceder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                historial.RemoveAt(historial.Count() - 1); // eliminamos
                matriz_celdas.SetMatriz(historial.Last().ClonarParaLISTA().GetMatriz()); // actualizamos
                volverApintar(); // volvemos a pintar
            }

            catch // cuando llegamos al principio de la simulación y no podemos retroceder más nos salta un error
            { MessageBox.Show("No es posible retroceder mas"); }
        }

        //CARGAR FICHERO
        // cargamos el fichero que hemos guardado anteriormente
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                canvas1.Children.Clear();
                canvas2.Children.Clear();
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
                    x = matriz_celdas.getX() - 2;
                    y = matriz_celdas.getY() - 2;

                    // Generamos las Mallas
                    this.casillas = generarMalla1(casillas, canvas1);
                    this.casillas2 = generarMalla1(casillas2, canvas2);

                    volverApintar(); // repintamos 

                    MessageBox.Show("Fichero cargado con éxito! Por favor modifique los parámetros de simulación y condiciones de contorno o se simulará por defecto con los parámetros A y condiciones de controno fijas");

                    // abilitamos todos los botones y textboxs por si se carga el fichero solo iniciar el programa
                    button1.IsEnabled = true;
                    button2.IsEnabled = true;
                    button4.IsEnabled = true;
                    button5.IsEnabled = true;
                    button6.IsEnabled = true;
                    boton_retroceder.IsEnabled = true;
                    botonCARGAR.IsEnabled = true;
                    betta.IsEnabled = true;
                    dxdy.IsEnabled = true;
                    epsilon.IsEnabled = true;
                    delta.IsEnabled = true;
                    M.IsEnabled = true;
                    dt.IsEnabled = true;
                    ParametrosA.IsEnabled = true;
                    ParametrosB.IsEnabled = true;
                    Parametros.IsEnabled = true;
                    button1.IsEnabled = true;
                    button2.IsEnabled = true;
                    button4.IsEnabled = true;
                    button5.IsEnabled = true;
                    botonCARGAR.IsEnabled = true;
                    slider1.IsEnabled = true;
                    boton_retroceder.IsEnabled = true;


                    // Si no cambiamos nada definimos por defecto condiciones de controno fijas y parámetros A
                    matriz_celdas.SetCondicionsContornoFaseTemperatura("fixed");
                    norm.SetDxDy(0.005);
                    norm.SetEpsilon(0.005);
                    norm.SetBetta(400);
                    norm.SetDelta(0.5);
                    norm.SetM(20);
                    norm.SetDT(5 * Math.Pow(10, -6));
                    matriz_celdas.SetNormas(norm);
                    boxIteration.Text = Convert.ToString(historial.Count()); // AL cargar la simulacion que la iteracion se ponga a 0

                }
                else
                { MessageBox.Show("No ha sido posible cargar la simulación"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //GUARDAR FICHERO
        //guardamos el fichero de la simulación que estamos haciendo
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ExplicacionesDelCristal lc = new ExplicacionesDelCristal();
            lc.ShowDialog();
        }
    }
}