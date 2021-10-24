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

namespace WpfApplication1
{
    /// <summary>
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        bool cambiar = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.cambiar = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.cambiar = false;
            Close();
        }

        public bool GetCambio()
        { return this.cambiar; }
    }
}
