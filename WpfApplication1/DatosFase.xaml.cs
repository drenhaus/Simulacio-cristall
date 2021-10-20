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
    /// Interaction logic for DatosFase.xaml
    /// </summary>
    public partial class DatosFase : Window
    {
        public DatosFase()
        {
            InitializeComponent();
        }

        double[,] fases;
        double[,] temperatura;

        public void SetFases(double[,] f)
        { this.fases = f; }

        public void SetTemperaturas(double[,] t)
        { this.temperatura = t; }

        public void Load(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();
            grid1.ColumnDefinitions.Clear();
            grid1.RowDefinitions.Clear();

            grid2.Children.Clear();
            grid2.ColumnDefinitions.Clear();
            grid2.RowDefinitions.Clear();


            bool visibleya = false;
            int y = fases.GetLength(0);
            int x = fases.GetLength(1);


            if (visibleya == false)
            {
                for (int i = 0; i < y; i++)
                {
                    RowDefinition row = new RowDefinition();
                    grid1.RowDefinitions.Add(row);
                    RowDefinition row2 = new RowDefinition();
                    grid2.RowDefinitions.Add(row2);
                }
                for (int j = 0; j < x; j++)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    grid1.ColumnDefinitions.Add(column);
                    ColumnDefinition column2 = new ColumnDefinition();
                    grid2.ColumnDefinitions.Add(column2);
                }

                visibleya = true;
            }

            if (visibleya == true)
            {

                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        TextBlock txt = new TextBlock();
                        txt.FontSize = 12;
                        txt.FontWeight = FontWeights.Bold;
                        Decimal d = new Decimal();
                        d = Decimal.Round(Convert.ToDecimal(fases[i, j]), 2);

                        txt.Text = d.ToString();

                        Grid.SetColumn(txt, j);
                        Grid.SetRow(txt, i);
                        Grid.SetColumnSpan(txt, 40);

                        grid1.Children.Add(txt);

                        TextBlock txt2 = new TextBlock();
                        txt2.FontSize = 12;
                        txt2.FontWeight = FontWeights.Bold;
                        Decimal d2 = new Decimal();
                        d2 = Decimal.Round(Convert.ToDecimal(temperatura[i, j]), 2);

                        txt2.Text = d2.ToString();

                        Grid.SetColumn(txt2, j);
                        Grid.SetRow(txt2, i);
                        Grid.SetColumnSpan(txt2, 40);

                        grid2.Children.Add(txt2);

                    }

                }
            }
        }
    }
}
