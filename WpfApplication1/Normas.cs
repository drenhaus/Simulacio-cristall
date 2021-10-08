using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NormasJuego
{
    class Normas
    {

        bool estado_futuro_viva=false; // definimos que en un inicio estara muerta
        double estado_futuro_fase = 1; // por defecto esta liquida
        double estado_futuro_temperatura = -1;

        double m = 20;
        double delta = 0.5;
        double dt = 10e-5;
        double betta = 400;
        double epsilon = 0.005;
        double dx = 0.005;
        double dy = 0.005;

        // CREO que si mas a delante anadimos un get/set de los parametros sera la forma de poder modificar los parametros de arriba a traves del window


        public double ActualizarFASE(double estado_actual_fase, double estado_actual_temperatura, double estado_actual_fase_izquierda,
            double estado_actual_fase_derecha, double estado_actual_fase_arriba, double estado_actual_fase_abajo)
        {


            double dFase_dY_quadrat = (estado_actual_fase_arriba - 2 * estado_actual_fase + estado_actual_fase_abajo) / (dy * dy); //derivada segona Y
            double dFase_dX_quadrat = (estado_actual_fase_derecha - 2 * estado_actual_fase + estado_actual_fase_izquierda) / (dx * dx); //derivada segona X
            double gradient_gradeint = dFase_dX_quadrat + dFase_dY_quadrat; // GRADIENT^2

            double A = ((1/(epsilon*epsilon*m))*(estado_actual_fase*(1-estado_actual_fase)*(estado_actual_fase-0.5+30*epsilon*betta*delta*estado_actual_temperatura*estado_actual_fase*(1-estado_actual_fase))));//Primera parte
            double B = epsilon * epsilon * gradient_gradeint; //segona part
            double d_fase_d_t = A+B;

            this.estado_futuro_fase = estado_actual_fase + dt * d_fase_d_t;


            A = 0;
            B = 0;
            d_fase_d_t = 0;
            dFase_dY_quadrat = 0;
            dFase_dX_quadrat = 0;
            gradient_gradeint = 0;

            return this.estado_futuro_fase;
        }

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
