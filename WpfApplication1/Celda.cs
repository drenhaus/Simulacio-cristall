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

        Normas norma1= new Normas();
        
       
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
