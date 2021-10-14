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
using WpfApplication2;
using NormasJuego;
using WpfApplication1;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for datos.xaml
    /// </summary>
    public partial class datos : Window
    {
        public datos()
        {
            InitializeComponent();
            
        }

        
        public int y { get; set; }
        public int x { get; set; }
        public Malla matriz_celdas { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid1.ShowGridLines = true;
            
            for (int i = 1; i < y - 1; i++)
            {
                RowDefinition row = new RowDefinition();
                grid1.RowDefinitions.Add(row);

                for (int j = 1; j < x - 1; j++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    grid1.ColumnDefinitions.Add(column); 
                    TextBlock txt = new TextBlock();
                    txt.Text=Convert.ToString(matriz_celdas.DameFASEde(i,j));
                    Grid.SetRow(txt, i);
                    Grid.SetColumn(txt, j);
                    grid1.Children.Add(txt);
                    
                }
            }
            
        } 

    }
}
