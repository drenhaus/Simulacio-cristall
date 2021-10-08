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

        // F mayuscuara referncia a FASE
        double F_derecha;
        double F_izquierda;
        double F_abajo;
        double F_arriba; // COMO LA URRS

        double T_derecha;
        double T_izquierda;
        double T_abajo;
        double T_arriba; // COMO LA URRS

        Normas norma1= new Normas();


        // LOS GET
            //fase

        public double GetFase()
        { return (this.fase); }

         public double GetFaseDerecha()
         { return (this.F_derecha); }
         public double GetFaseIzquierda()
         { return (this.F_izquierda); }
         public double GetFaseAbajo()
         { return (this.F_abajo); }
         public double GetFaseArriba()
         { return (this.F_arriba); }

            //temperatura
         public double GetTemperatura()
         { return (this.temperatura); }
         public double GetTemperaturaDerecha()
         { return (this.T_derecha); }
         public double GetTemperaturaIzquierda()
         { return (this.T_izquierda); }
         public double GetTemperaturaAbajo()
         { return (this.T_abajo); }
         public double GetTemperaturaArriba()
         { return (this.T_arriba); }


         // LOS SET
            //fase
         public void SetFase(double fase)
         { this.fase = fase; }
         public void SetFaseDerecha(double fase)
         { this.F_derecha = fase; }
         public void SetFaseIzquierda(double fase)
         { this.F_izquierda = fase; }
         public void SetFaseAbajo(double fase)
         { this.F_abajo = fase; }
         public void SetFaseArriba(double fase)
         { this.F_arriba = fase; }

            //temperatura
         public void SetTemperatura(double temperatura)
         { this.temperatura = temperatura; }
         public void SetTemperaturaDerecha(double temperatura)
         { this.T_derecha = temperatura; }
         public void SetTemperaturaIzquierda(double temperatura)
         { this.T_izquierda = temperatura; }
         public void SetTemperaturaAbajo(double temperatura)
         { this.T_abajo = temperatura; }
         public void SetTemperaturaArriba(double temperatura)
         { this.T_arriba = temperatura; }



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


        public void ActualizarFASEdeCelda(double estado_actual_fase, double estado_actual_temperatura, double estado_actual_fase_izquierda,
            double estado_actual_fase_derecha, double estado_actual_fase_arriba, double estado_actual_fase_abajo, double estado_actual_temperatura_arriba, double estado_actual_temperatura_abajo,
            double estado_actual_temperatura_derecha,double estado_actual_temperatura_izquierda)
        {

            this.fase = norma1.ActualizarFASE(estado_actual_fase, estado_actual_temperatura, estado_actual_fase_izquierda,
            estado_actual_fase_derecha, estado_actual_fase_arriba, estado_actual_fase_abajo, estado_actual_temperatura_arriba, estado_actual_temperatura_abajo,
            estado_actual_temperatura_derecha, estado_actual_temperatura_izquierda);
        }



    }
}
