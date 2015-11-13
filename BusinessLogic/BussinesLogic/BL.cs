using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public class BL
    {
        private char[] ADN = { 'A', 'T', 'C', 'G' };
        private int maxCol = 0;
        private int maxRow = 0;
        private int secuenciaBase = 4;
        private int minSecuenciaMutante = 2;
        private int cantSecuenciasMutantes = 0;
        private DataTable matriz = new DataTable();

        #region Evaluar Entrada

        private bool SecuenciaValida(string[] secuencia)
        {
            try
            {
                maxRow = secuencia.Length;                

                foreach(string s in secuencia)
                {
                    if (s.Length > maxCol)
                        maxCol = s.Length;

                    foreach (char a in s)
                    {
                        if (!ADN.Contains(a))
                            return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool EvaluarMutante()
        {
            try
            {
                if (maxCol > 3 || maxRow > 3)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


        private DataTable ConverArrayToDataTable(string[] arreglo){
            
            try
            {
                //Columnas
                for (int i = 0; i < maxCol; i++)
                {
                    matriz.Columns.Add();
                }

                //Rows
                foreach (string s in arreglo)
                {
                    DataRow row = matriz.NewRow();
                    int cont = 0;
                    foreach (char a in s)
                    {
                        row[cont] = a;
                        cont++;
                    }
                    matriz.Rows.Add(row);
                }
            }
            catch
            {

            }

            return matriz;
        }
        #endregion

        #region ¿Es mutante?

        public bool IsMutant(string[] dna)
        {
            if (!SecuenciaValida(dna))
            {
                return false;
            }
            else
            {
                if (EvaluarMutante())
                {
                    ConverArrayToDataTable(dna);
                    EsMutanteH();

                    if (cantSecuenciasMutantes >= minSecuenciaMutante)
                        return true;
                    else
                        EsMutanteV();
                    
                    if (cantSecuenciasMutantes >= minSecuenciaMutante)
                        return true;
                    else 
                        EsMutanteO();

                    if (cantSecuenciasMutantes >= minSecuenciaMutante)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }

        private void EsMutanteH()
        {
            try
            {
                foreach (DataRow r in matriz.Rows)
                {
                    string secuenciaMutante = string.Empty;
                    foreach (DataColumn c in r.Table.Columns)
                    {
                        if (String.IsNullOrEmpty(secuenciaMutante)){
                            secuenciaMutante = r[0].ToString();
                        }
                        else { 
                            if (secuenciaMutante.Contains(r[c].ToString()))
                                secuenciaMutante += r[c].ToString();
                            else
                            {
                                if (secuenciaMutante.Length >= secuenciaBase)
                                {
                                    cantSecuenciasMutantes++;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void EsMutanteV()
        {
            try
            {
                foreach (DataColumn c in matriz.Columns)
                {
                    string secuenciaMutante = string.Empty;
                    foreach (DataRow r in c.Table.Rows)
                    {
                        if (String.IsNullOrEmpty(secuenciaMutante))
                        {
                            secuenciaMutante = r[c].ToString();
                        }
                        else
                        {
                            if (secuenciaMutante.Contains(r[c].ToString()))
                                secuenciaMutante += r[c].ToString();
                            else
                            {
                                if (secuenciaMutante.Length >= secuenciaBase)
                                {
                                    cantSecuenciasMutantes++;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void EsMutanteO()
        {
            try
            {
                if (maxCol >= secuenciaBase && maxRow >= secuenciaBase)
                {
                    LineaBase();

                    if (cantSecuenciasMutantes < minSecuenciaMutante)
                        DebajoLinea();

                    if (cantSecuenciasMutantes < minSecuenciaMutante)
                        ArribaLinea();
                }
            }
            catch
            {
            }
        }

        private void LineaBase()
        {
            try
            {
                int col = 0;
                string secuenciaMutante = string.Empty;

                foreach (DataRow r in matriz.Rows)
                {
                    //Linea Base
                    if (String.IsNullOrEmpty(secuenciaMutante))
                    {
                        secuenciaMutante = r[0].ToString();
                    }
                    else
                    {
                        if (col < matriz.Columns.Count)
                        {
                            if (secuenciaMutante.Contains(r[col].ToString()))
                                secuenciaMutante += r[col].ToString();
                            else
                            {
                                if (secuenciaMutante.Length >= secuenciaBase)
                                {
                                    cantSecuenciasMutantes++;
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (secuenciaMutante.Length >= secuenciaBase)
                            {
                                cantSecuenciasMutantes++;
                            }
                            break;
                        }
                    }
                    col++;
                }
            }
            catch
            {
            }
        }

        private void DebajoLinea()
        {
            try
            {
                int col = 0;
                int row = 1;
                int repeticion = maxRow - secuenciaBase;
                string secuenciaMutante = string.Empty;

                for (int i = 0; i < repeticion; i++)
                {
                    for (int e = row; e < matriz.Rows.Count; e++)
                    {
                        if (String.IsNullOrEmpty(secuenciaMutante))
                        {
                            secuenciaMutante = matriz.Rows[e][col].ToString();
                        }
                        else
                        {
                            if (col < matriz.Columns.Count)
                            {
                                if (secuenciaMutante.Contains(matriz.Rows[e][col].ToString()))
                                    secuenciaMutante += matriz.Rows[e][col].ToString();
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        col++;
                    }
                    if (secuenciaMutante.Length >= secuenciaBase)
                    {
                        cantSecuenciasMutantes++;
                    }

                    row++;
                    col = 0;
                    secuenciaMutante = string.Empty;
                }
            }
            catch
            {
            }
        }

        private void ArribaLinea()
        {
            try
            {
                int col = 1;
                int row = 0;
                int repeticion = maxCol - secuenciaBase;
                string secuenciaMutante = string.Empty;

                for (int i = 0; i < repeticion; i++)
                {
                    for (int e = row; e < matriz.Rows.Count; e++)
                    {
                        if (String.IsNullOrEmpty(secuenciaMutante))
                        {
                            secuenciaMutante = matriz.Rows[e][col].ToString();
                        }
                        else
                        {
                            if (col < matriz.Columns.Count)
                            {
                                if (secuenciaMutante.Contains(matriz.Rows[e][col].ToString()))
                                    secuenciaMutante += matriz.Rows[e][col].ToString();
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        col++;
                    }
                    if (secuenciaMutante.Length >= secuenciaBase)
                    {
                        cantSecuenciasMutantes++;
                    }
                    row++;
                    col = 1;
                    secuenciaMutante = string.Empty;
                }
            }
            catch
            {
            }
        }

        #endregion

    }
}
