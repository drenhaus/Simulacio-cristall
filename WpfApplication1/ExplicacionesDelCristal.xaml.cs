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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ExplicacionesDelCristal.xaml
    /// </summary>
    public partial class ExplicacionesDelCristal : Window
    {
        public ExplicacionesDelCristal()
        {
            InitializeComponent();

        }

        // PERMITE MOVER LA VENTANA
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        //Cerrar ventana
        private void BtnCerrarEXPL_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //Minimizar ventana
        private void BtnMiniEXPL_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
