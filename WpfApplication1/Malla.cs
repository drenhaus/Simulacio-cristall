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


        int numeroTotalDeVivos;
        int contadorVecinosVivos;
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
        
        public void ClonarMatrix()
        {
            matriz_malla_Clone=new Celda[y,x];
               for (int i = 0; i < y; i++)
                   for (int j = 0; j < x; j++)
                   {
                       {
                           Celda fill_clone = new Celda(); // rellenamos la matriz con celdas
                           matriz_malla_Clone[i, j] = fill_clone;
                           matriz_malla_Clone[i, j].SetVida(matriz_malla[i, j].GetVida());//
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

        public void SetCondicionesContorno(bool VidaSuperior, bool VidaInferior, bool VidaDerecha, bool vidaIzquierda) // ELIMINAR
        {
            for (int i = 0; i < x; i++)
            {
                this.matriz_malla[0, i].SetVida(VidaSuperior);
                this.matriz_malla[y - 1, i].SetVida(VidaInferior);
            }

            for (int j = 1; j < y-1; j++)
            {
                this.matriz_malla[j, 0].SetVida(vidaIzquierda);
                this.matriz_malla[j, x - 1].SetVida(VidaDerecha);
            }
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


        public void SetVidaDeCelda(int fila, int columna, bool vida) // ERRORSS
        {
            //this.matriz_malla[1, 1].SetVida(vida);

            matriz_malla[fila, columna].SetVida(vida);
        
        }

        public void SetFaseDeCelda(int fila, int columna, double fase)
        {

            matriz_malla[fila, columna].SetFase(fase);
        }
        public void SetTemperaturaDeCelda(int fila, int columna, double T)
        {

            matriz_malla[fila, columna].SetTemperatura(T);
        }


        public bool DameElEstadoDe(int posFILAS, int posCOLUMNAS)
        {

            return (this.matriz_malla[posFILAS, posCOLUMNAS].GetVida());
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

        public int GetNumeroDeVivosDeLaMatriz()
        {
            numeroTotalDeVivos = 0;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; i < x; j++)
                {
                    if (matriz_malla[i, j].GetVida() == true)
                    numeroTotalDeVivos++;
                }
            }
            return numeroTotalDeVivos;
        }

        public int NumeroDeVecinosVivos(int Fila, int Columna)
        {

            contadorVecinosVivos = 0;

            if (matriz_malla_Clone[Fila - 1, Columna].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila + 1, Columna].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila - 1, Columna + 1].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila, Columna + 1].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila + 1, Columna + 1].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila - 1, Columna - 1].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila, Columna - 1].GetVida() == true)
            {
                contadorVecinosVivos++;
            }

            if (matriz_malla_Clone[Fila + 1, Columna - 1].GetVida() == true)
            {
                contadorVecinosVivos++;
            }


            return contadorVecinosVivos;
            //matriz_malla[Fila, Columna].SetVecinosVivos(contadorVecinosVivos);
            //norma1.SetVecinosVivos(contadorVecinosVivos);
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

        public Malla CargarSimulacion(string name)
        {
            Malla matriz_celdas = new Malla();

            StreamReader sr = new StreamReader(name);
            string linea = sr.ReadLine();
            string[] trozos = linea.Split(' ');
            matriz_celdas.SetNumeroDeFilasYColumnas(Convert.ToInt32(trozos[0])-2, Convert.ToInt32(trozos[1])-2);


            string line = sr.ReadLine();
            int i = 0;                     
            while (line != null)
            {
                string[] traces = line.Split(' ');
                for (int j = 0; j < traces.Length - 1; j++)
                {
                    string[] data = traces[j].Split('&');
                    matriz_celdas.SetFaseDeCelda(i, j, Convert.ToDouble(data[0]));
                    matriz_celdas.SetTemperaturaDeCelda(i, j, Convert.ToDouble(data[1]));
                }
                line = sr.ReadLine();

                i++;
            }
            sr.Close();
            return matriz_celdas;
        }


    }

}

    
