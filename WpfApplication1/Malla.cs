using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NormasJuego;
using WpfApplication1;
using System.IO;

namespace WpfApplication2
{
    class Malla
    {
        int x=0; //numero de colummnas   //
        int y=0; //numero de filas



        double cantidadDeFASE; // es la media de la fase en una matriz
        double cantidadDeTEMP; // es la media de la temperatura en una matriz

        Celda[,] matriz_malla; // matriz_malla_Clone =matriz_malla
        Celda[,] matriz_malla_Clone; //matriz espejo
        
        Normas norma1; //   MIRAR

    
        public Celda[,] GetMatriz()
        {
            return this.matriz_malla;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void SetNormas(Normas n)
        { 
        this.norma1=n;
        }

        public void SetMatriz(Celda[,] matriz_MALLA)
        {
            this.matriz_malla = matriz_MALLA;
        }

        /*        public Malla MallaGuardar(int X, int Y, double F, double T, int i, int j)
                {

                    Malla A = new Malla();

                    A.SetNumeroDeFilasYColumnas(X, Y);
                    A.SetTemperaturaDeCelda(i, j, T);
                    A.SetFaseDeCelda(i, j, F);
                    return A;
                }*/
        public Malla ClonarParaLISTA()
        {
            Celda [,] matriz_malla_Clone_LISTA = new Celda[y, x];
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    {
                        Celda fill_clone = new Celda(); // rellenamos la matriz con celdas
                        matriz_malla_Clone_LISTA[i, j] = fill_clone;
                        matriz_malla_Clone_LISTA[i, j].SetFase(matriz_malla[i, j].GetFase());
                        matriz_malla_Clone_LISTA[i, j].SetTemperatura(matriz_malla[i, j].GetTemperatura());

                    }
                }

            Malla malla_para_guardar = new Malla();

            malla_para_guardar.SetNumeroDeFilasYColumnas(getY()-2, getX()-2);
            malla_para_guardar.SetNormas(norma1);
            malla_para_guardar.SetMatriz(matriz_malla_Clone_LISTA);

            return malla_para_guardar;
        }

        public void ClonarMatrix()
        {
            matriz_malla_Clone=new Celda[y,x];
               for (int i = 0; i < y; i++)
                   for (int j = 0; j < x; j++)
                   {
                       {
                           Celda fill_clone = new Celda(); // rellenamos la matriz con celdas
                           matriz_malla_Clone[i, j] = fill_clone;
                           matriz_malla_Clone[i, j].SetFase(matriz_malla[i, j].GetFase());
                           matriz_malla_Clone[i, j].SetTemperatura(matriz_malla[i, j].GetTemperatura());
                          
                       }
                   }

        }

        public Celda[,] GetClon()
        { return this.matriz_malla_Clone; }

        public void SetNumeroDeFilasYColumnas(int fila, int columna)
        {
            this.y = fila+2;
            this.x = columna+2;
            
            this.matriz_malla = new Celda [y,x];
            this.matriz_malla_Clone = new Celda[y,x];

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {{
                    Celda fill = new Celda(); // rellenamos la matriz con celdas
                    Celda fill_clone = new Celda(); // rellenamos la matriz con celdas

                    matriz_malla[i,j] = fill;
                    matriz_malla_Clone[i,j] = fill_clone;
                }}
 
        }


        public void SetCondicionsContornoFaseTemperatura(string condicion)
        {
            if (condicion == "fixed")
            {

                for (int i = 1; i < y; i++)
                {
                    this.matriz_malla[0, i].SetFase(1);
                    this.matriz_malla[0, i].SetTemperatura(-1);
                    this.matriz_malla[y - 1, i].SetFase(1);
                    this.matriz_malla[y - 1, i].SetTemperatura(-1);
                }

                for (int j = 0; j < x; j++)
                {
                    this.matriz_malla[j, 0].SetFase(1);
                    this.matriz_malla[j, 0].SetTemperatura(-1);
                    this.matriz_malla[j, x - 1].SetFase(1);
                    this.matriz_malla[j, x - 1].SetTemperatura(-1);

                }
                if (condicion == "espejo")
                {

                    for (int i = 1; i < y; i++)
                    {
                        this.matriz_malla[0, i].SetFase(matriz_malla[1,i].GetFase());
                        this.matriz_malla[0, i].SetTemperatura(matriz_malla[1, i].GetTemperatura());
                        this.matriz_malla[y - 1, i].SetFase(matriz_malla[y-2, i].GetFase());
                        this.matriz_malla[y - 1, i].SetTemperatura(matriz_malla[y-2, i].GetTemperatura());
                    }

                    for (int j = 0; j < x; j++)
                    {
                        this.matriz_malla[j, 0].SetFase(matriz_malla[j, 1].GetFase());
                        this.matriz_malla[j, 0].SetTemperatura(matriz_malla[j, 1].GetFase());
                        this.matriz_malla[j, x - 1].SetFase(matriz_malla[j, x-2].GetFase());
                        this.matriz_malla[j, x - 1].SetTemperatura(matriz_malla[j, x - 2].GetFase());

                    }

                }

            }
        }


        public void SetFaseDeCelda(int fila, int columna, double fase)
        {

            matriz_malla[fila, columna].SetFase(fase);
        }

        public void SetTemperaturaDeCelda(int fila, int columna, double T)
        {

            matriz_malla[fila, columna].SetTemperatura(T);
        }



        public double DameFASEde(int posFILAS, int posCOLUMNAS)

        {
            return (this.matriz_malla[posFILAS, posCOLUMNAS].GetFase());
        }
       
        public double DameFASEdeClon(int posFILAS, int posCOLUMNAS)
        {
            return (this.matriz_malla_Clone[posFILAS, posCOLUMNAS].GetFase());
        }

        public double DameTEMPERATURAde(int posFILAS, int posCOLUMNAS)
        {
            return (this.matriz_malla[posFILAS, posCOLUMNAS].GetTemperatura());
        }

        public double DameTEMPERATURAdeClon(int posFILAS, int posCOLUMNAS)
        {
            return (this.matriz_malla_Clone[posFILAS, posCOLUMNAS].GetTemperatura());
        }


        public double GetcantidadFase()
        {
            cantidadDeFASE = 0;
            int celdasrecorridas = 0;
            for (int i = 1; i < y-1; i++)
            {
                for (int j = 1; j < x-1; j++)
                {
                    cantidadDeFASE = cantidadDeFASE + matriz_malla[i, j].GetFase();
                    celdasrecorridas ++;

                }
            }
            cantidadDeFASE = cantidadDeFASE / celdasrecorridas; // asi normalizamos la cantidad de celdas

            return cantidadDeFASE;


        }

        public double GetcantidadTEMP()
        {
            cantidadDeTEMP = 0;
            int celdasrecorridas = 0;
            for (int i = 1; i < y - 1; i++)
            {
                for (int j = 1; j < x - 1; j++)
                {
                    cantidadDeTEMP = cantidadDeTEMP + matriz_malla[i, j].GetTemperatura();
                    celdasrecorridas++;

                }
            }
            cantidadDeTEMP = cantidadDeTEMP / celdasrecorridas; // asi normalizamos la cantidad de celdas

            return cantidadDeTEMP;


        }


        public void MallaFutura()
        {
            ClonarMatrix();

            for (int i = 1; i < y-1; i++)
            {
                for (int j = 1; j < x-1; j++)
                {

                   
                    // hem de posar l'estat dels de les fases i temperatura
                    matriz_malla[i, j].SetFaseDerecha(DameFASEdeClon(i,j+1));
                    matriz_malla[i, j].SetFaseIzquierda(DameFASEdeClon(i, j - 1));
                    matriz_malla[i, j].SetFaseAbajo(DameFASEdeClon(i + 1, j));   // es -1 xq la malla empiza por fila 0 i ma augmentando el valor a medida que baja
                    matriz_malla[i, j].SetFaseArriba(DameFASEdeClon(i - 1, j));

                    matriz_malla[i, j].SetTemperaturaDerecha(DameTEMPERATURAdeClon(i, j + 1));
                    matriz_malla[i, j].SetTemperaturaIzquierda(DameTEMPERATURAdeClon(i, j - 1));
                    matriz_malla[i, j].SetTemperaturaAbajo(DameTEMPERATURAdeClon(i + 1, j));
                    matriz_malla[i, j].SetTemperaturaArriba(DameTEMPERATURAdeClon(i - 1, j));



                    //ACTUALIZAMOS LA CELDA


                    matriz_malla[i, j].SetNorma(norma1);
                    matriz_malla[i, j].ActualizarFASEdeCelda();




                }
            }
            

        }

        public int GuardarSimulacion(string nombre)
        {
            try
            {
                StreamWriter w = new StreamWriter(nombre);

                w.Write(this.y + " " + this.x);
                w.Write('\n');

                for (int j = 0; j < y; j++)
                {
                    for (int i = 0; i < x; i++)
                    {
                        w.Write(this.DameFASEde(j, i) + "&" + this.DameTEMPERATURAde(j, i) + " ");
                    }

                    w.Write('\n');

                }
                w.Close();
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        


    
    }

}

    
    }

}

    
