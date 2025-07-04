using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Accord.Math;
using Accord.Math.Decompositions;

namespace RNA_miniprojet
{
    class Takens
    {
        private static int Iteration = 20;
        private static int Dimension = 100;
        private double[] serieTemporelle;
        public Takens(double[] serie)
        {
            serieTemporelle = serie;
        }

        private double[,] MatriceDeCovariance
        {
            get
            {
                double[] value = new double[500];
                value = serieTemporelle;
                return Matrice.ProduitVecteurColLig(value, value, Dimension);
            }

        }
        private double[] Trier(double[] sequence)
        {
            double[] res = new double[Dimension];
            double max = -1000;
            int k = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                for (int j = 0; j < sequence.Length; j++)
                {
                    if (sequence[j] > max)
                    {
                        max = sequence[j];
                        k = j;
                    }
                }
                res[i] = max;
                sequence[k] = -1000;
                max = -1000;
            }
            return res;
        }
        public double[] ValeursPropre()
        {
            //en utilisant la methode QR
            double[,] temp = new double[500, 500];
            temp = MatriceDeCovariance;
            for (int i = 0; i < Iteration; i++)
            {
                QrDecomposition qr = new QrDecomposition(temp);
                double[,] R = qr.UpperTriangularFactor;
                double[,] Q = qr.OrthogonalFactor;
                double[,] Ak = Matrice.ProduitMatrice(R, Q, Dimension);
                temp = Ak;
            }
            double[] res = new double[Dimension];
            double[] resDecroissant = new double[Dimension];
            res = temp.Diagonal();
            resDecroissant = Trier(res);
            return resDecroissant;
        }

        public double[] ErreurApproximationMoyenne
        {
            get
            {
                double[] res = new double[Dimension];
                double[] vp = new double[Dimension];
                vp = ValeursPropre();
                for (int i = 0; i < vp.Length - 1; i++)
                {
                    
                    res[i] = Math.Sqrt(vp[i+1]);
                }
                return res;
            }
        }

    }
}
