using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormasJuego
{
    class Normas
    {
        //int vecinos_vivos;
        //bool estado_actual_viva;
        bool estado_futuro_viva=false; // definimos que en un inicio estara muerta

        //public void SetVecinosVivos(int num)
        //{ this.vecinos_vivos = num; }

        //public void SetEstadoVida(bool vida)
        //{ this.estado_actual_viva = vida; }

        public bool ActualizarVida(bool estado_actual_viva, int vecinosVIUS)
        {
            if ((estado_actual_viva == true) && (vecinosVIUS == 2 || vecinosVIUS == 3)) // arreglat el estado_actual_viva
            { this.estado_futuro_viva = true; }


            else if ((estado_actual_viva == false) && (vecinosVIUS == 3))
            { this.estado_futuro_viva = true; }

            else
            {
                this.estado_futuro_viva = false;
            }

            return this.estado_futuro_viva;
        }



    }
}
