using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NormasJuego;

namespace WpfApplication1
{
    class Celda
    {

       
        bool viva = false;  // PODRIEM TREURE
        int vecinos_vivos;
        double fase = 1;
        double temperatura = -1;

        Normas norma1= new Normas();

        public double GetFase()
        { return (this.fase); }

         public double GetTemperatura()
        { return (this.fase); }


         public void SetFase(double fase)
         { this.fase = fase; }

         public void SetTemperatura(double temperatura)
         { this.temperatura = temperatura; }







        public void SetVida(bool vida)
        {this.viva = vida;}

        public bool GetVida()
        {return (this.viva);}

        public void SetVecinosVivos(int num)
        { this.vecinos_vivos = num; }

        public int GetVecinosVivos()
        { return (this.vecinos_vivos); }

        public void ActualizarCelda(bool Viva, int numVecinosVivos)
        {

            this.viva = norma1.ActualizarVida(Viva, numVecinosVivos);



        }




    }
}
