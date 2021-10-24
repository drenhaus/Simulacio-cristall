using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Celda
    {
        //ATRIBUTOS 
        double fase = 1; //fase por defecto
        double temperatura = -1; // temperatura por defecto

        // valores vecinos de las fases
        double F_derecha = 1;
        double F_izquierda = 1;
        double F_abajo = 1;
        double F_arriba = 1;
        // valores vecinos de las temperaturas
        double T_derecha = -1;
        double T_izquierda = -1;
        double T_abajo = -1;
        double T_arriba = -1;

        double estado_futuro_fase;
        double estado_futuro_temperatura;

        Normas n = new Normas();

        // LOS GET de fases y temperatura
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

        // LOS SET de fase y temperatura
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

        // SET de los parámetros
        public void SetNorma(Normas norm)
        {
            this.n = norm;
        }

        // CALCULAMOS EL VALOR DE LA NUEVA FASE Y TEMPERATURA
        public void ActualizarFASEdeCelda()
        {
            // definimos los valores de los parámetros
            double dy = n.GetDxDy();
            double dx = n.GetDxDy();
            double epsilon = n.GetEpsilon();
            double m = n.GetM();
            double betta = n.GetBetta();
            double delta = n.GetDelta();
            double dt = n.GetDT();

            double dFase_dY_quadrat = (F_arriba - 2.0 * fase + F_abajo) / (dy * dy); //derivada segunda
            double dFase_dX_quadrat = (F_derecha - 2.0 * fase + F_izquierda) / (dx * dx); //derivada segunda
            double gradient_gradeint = dFase_dX_quadrat + dFase_dY_quadrat; // GRADIENTE^2

            double A = ((1.0 / (epsilon * epsilon * m)) * (fase * (1.0 - fase) * (fase - 0.5 + 30.0 * epsilon * betta * delta * temperatura * fase * (1.0 - fase))));//Primera parte
            double B = epsilon * epsilon * gradient_gradeint * (1.0 / (epsilon * epsilon * m)); //segunda parte
            double d_fase_d_t = A + B;

            this.estado_futuro_fase = fase + dt * d_fase_d_t;

            double dTemperatura_dY_quadrat = (T_arriba - 2.0 * temperatura + T_abajo) / (dy * dy);
            double dTemperatura_dX_quadrat = (T_derecha - 2.0 * temperatura + T_izquierda) / (dx * dx); //derivada segunda X
            double gradient_gradeint_TEMP = dTemperatura_dX_quadrat + dTemperatura_dY_quadrat;

            double C = gradient_gradeint_TEMP;
            double D = (1.0 / delta) * (30.0 * fase * fase - 60.0 * fase * fase * fase + 30.0 * fase * fase * fase * fase) * d_fase_d_t;
            double d_temperatura_d_t = C - D;

            this.estado_futuro_temperatura = temperatura + dt * d_temperatura_d_t;

            // actualizamos la celda
            fase = estado_futuro_fase;
            temperatura = estado_futuro_temperatura;
        }
    }
}
