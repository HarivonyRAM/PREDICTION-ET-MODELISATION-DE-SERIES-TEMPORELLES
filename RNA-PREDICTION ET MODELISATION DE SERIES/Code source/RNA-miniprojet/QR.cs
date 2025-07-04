using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNA_miniprojet
{
    class QR
    {
        public double[,] A { get; set; }
        private double K { get; set; }
        private double[] X { get; set; }
        private double[] U { get; set; }
        private double[] W { get; set; }
        private double[] E1 { get; set; }
        private double[,] P { get; set; }
        private double[,] Q { get; set; }
        public int Dimension { get; set; }
        public int Iteration { get; set; }

        public QR(double[,] matrice, int n)
        {
            A = matrice;
            Dimension = n;
            X = new double[n];
            U = new double[n];
            W = new double[n];
            E1 = new double[n];
            P = new double[n, n];
            Q = new double[n, n];

        }

        private void Initialisation()
        {
            //initialisation de x et e1
            E1[0] = 1;
            for (int i = 1; i < Dimension; i++)
            {
                X[i - 1] = A[i, 0];
                E1[i] = 0;
            }

            //choix de k
            K = Matrice.Norme(X);

            //Calcul de U
            Console.WriteLine("---------U-------");
            U = Matrice.SoustractionVecteur(X, Matrice.MultiplierVecteur(K, E1, Dimension - 1), Dimension - 1);


            //Calcul de W           
            W = Matrice.DiviserVecteur(Matrice.Norme(U), U, Dimension - 1);

            //Calcul de P=I-2*W*Wt
            double[,] temp = new double[Dimension - 1, Dimension - 1];
            temp = Matrice.ProduitVecteurColLig(Matrice.MultiplierVecteur(2, W, Dimension - 1), W, Dimension - 1);
            for (int i = 0; i < Dimension - 1; i++)
            {
                for (int j = 0; j < Dimension - 1; j++)
                {
                    if (i == j)
                    {
                        P[i, j] = 1 - temp[i, j];
                    }
                    else
                    {
                        P[i, j] = -temp[i, j];
                    }
                }
            }

            //Calcul de Q
            Q[0, 0] = 1;
            for (int i = 1; i < Dimension; i++)
            {
                Q[i, 0] = 0;
                Q[0, i] = 0;
            }

            for (int i = 1; i < Dimension; i++)
            {
                for (int j = 1; j < Dimension; j++)
                {
                    Q[i, j] = P[i - 1, j - 1];
                }
            }

        }
        public double[,] MethodeQR()
        {
            double[,] Ak = new double[Dimension, Dimension];
            for (int i = 0; i < Iteration; i++)
            {
                Initialisation();
                Ak = Matrice.ProduitMatrice(Matrice.ProduitMatrice(Q, A, Dimension), Q, Dimension);
                A = Ak;
            }
            return Ak;

        }



    }
}
