using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNA_miniprojet
{
    class Matrice
    {
        
        public static double[,] ProduitMatrice(double[,] A, double[,] B, int dimension)
        {
            double[,] C = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    double somme = 0;
                    for (int k = 0; k < dimension; k++)
                    {
                        somme += A[i, k] * B[k, j];
                    }
                    C[i, j] = somme;
                }
            }
            return C;
        }
        public static double[] MultiplierVecteur(double k, double[] vecteur, int dimension)
        {
            double[] res = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                res[i] = Math.Round(k * vecteur[i], 4);
            }
            return res;

        }
        public static double[] SoustractionVecteur(double[] vecteur1, double[] vecteur2, int dimension)
        {
            double[] res = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                res[i] = vecteur1[i] - vecteur2[i];
            }
            return res;

        }

        public static double[] DiviserVecteur(double k, double[] vecteur, int dimension)
        {
            double[] res = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                res[i] = vecteur[i] / k;
            }
            return res;

        }
        public static double Norme(double[] vecteur)
        {
            double res, somme = 0;
            foreach (double coord in vecteur)
            {
                somme += Math.Abs(coord * coord);
            }
            res = Math.Sqrt(somme);
            return res;
        }
        public static double[,] ProduitVecteurColLig(double[] vectCol, double[] vectLig, int dimension)
        {
            double[,] C = new double[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    C[i, j] = (vectCol[i] * vectLig[j]);
                }
            }
            return C;
        }
    }
}
