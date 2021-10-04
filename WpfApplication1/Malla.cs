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

        Normas norma1 = new Normas(); //   MIRAR

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

        public void ClonarMatrix()
        {
            matriz_malla_Clone=new Celda[y,x];
               for (int i = 0; i < y; i++)
                   for (int j = 0; j < x; j++)
                   {
                       {
                           Celda fill_clone = new Celda(); // rellenamos la matriz con celdas
                           matriz_malla_Clone[i, j] = fill_clone;
                           matriz_malla_Clone[i, j].SetVida(matriz_malla[i, j].GetVida());
                          // matriz_malla_Clone[j, i].SetVecinosVivos(matriz_malla[j, i].GetVecinosVivos()); // clonem 0 ja que no hem contat els veins encara
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

        public void SetCondicionesContorno(bool VidaSuperior, bool VidaInferior, bool VidaDerecha, bool vidaIzquierda)
        {
            for (int i = 1; i < y; i++)
            {
                this.matriz_malla[0, i].SetVida(VidaSuperior);
                this.matriz_malla[y - 1, i].SetVida(VidaInferior);
            }

            for (int j = 0; j < x; j++)
            {
                this.matriz_malla[j, 0].SetVida(vidaIzquierda);
                this.matriz_malla[j, x - 1].SetVida(VidaDerecha);
            }
        }


        public void SetVidaDeCelda(int fila, int columna, bool vida) // ERRORSS
        {
            //this.matriz_malla[1, 1].SetVida(vida);

            matriz_malla[fila, columna].SetVida(vida);
        
        }

        public bool DameElEstadoDe(int posFILAS, int posCOLUMNAS)
        {

            return (this.matriz_malla[posFILAS, posCOLUMNAS].GetVida());
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

                    matriz_malla_Clone[i, j].SetVecinosVivos(NumeroDeVecinosVivos(i, j));  // gusrada # de veisn en el clone  
                
                    // origin te valor viu mort // clone te vius i veins

                    matriz_malla[i, j].ActualizarCelda(matriz_malla_Clone[i, j].GetVida(), matriz_malla_Clone[i, j].GetVecinosVivos()); //need clonar
                    
                }
            }
            

        }

        public int GuardarSimulacion(string nombre)
        {
            try
            {
                StreamWriter w = new StreamWriter(nombre);
                for (int j = 0; j < y; j++)
                {
                    for (int i = 0; i < x; i++)
                    {

                        if (i == x - 1)
                        {
                            if (DameElEstadoDe(j, i) == true)
                            {
                                w.Write(1);
                            }
                            else
                            {
                                w.Write(0);
                            }
                        }
                        else
                        {
                            if (DameElEstadoDe(j, i) == true)
                            {
                                w.Write(1 + " ");
                            }
                            else
                            {
                                w.Write(0 + " ");
                            }
                        }


                    }
                    if (j == y - 1)
                    { }
                    else
                    {
                        w.Write('\n');
                    }

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
            int i = 0;
            string linea = sr.ReadLine();
            string[] trozos = linea.Split(' ');
            while (linea != null)
            {
                i++;
                linea = sr.ReadLine();
            }
            sr.Close();

            matriz_celdas.SetNumeroDeFilasYColumnas(i-2, trozos.Length-2);

            StreamReader f = new StreamReader(name);
            
            string line = f.ReadLine();    // 1 0
            int h = 0;                     // 0 1
            while (line != null)
            {
                
                string[] traces = line.Split(' ');
                for (int j = 0; j < traces.Length; j++)
                {
                    if (Convert.ToInt32(traces[j]) == 0)
                    { matriz_celdas.SetVidaDeCelda(h, j, false); }
                    else
                    { matriz_celdas.SetVidaDeCelda(h, j, true); }
                }
                line = f.ReadLine();

             h++;
            }
            f.Close();
            return matriz_celdas;
        }

    
    }

}

    
