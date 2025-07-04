using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNA_miniprojet
{
    public class SerieDeHenon
    {
        public double a { get; set; }
        public double b { get; set; }
        public int n { get; set; }
        public double[] serieX { get; set; }
        public double[] serieY { get; set; }
        public int nue { get; set; }

        public static double[] NMSE = new Double[100];
        public SerieDeHenon(int n,double a, double b)
        {
            this.a = a;
            this.b = b;
            this.n = n;
            serieX = new double[n];
            serieY = new double[n];
            Serietemporelle(0,0);
        }

        public double FonctionX(double Xprec,double Yprec)
        {
            double res;
            res=Yprec + 1- (a*Math.Pow(Xprec,2));
            return res;
        }
        public double FonctionY(double Xprec)
        {
            double res;
            res = b * Xprec;
            return res;
        }

        public void Serietemporelle(double x0, double y0)
        {
            double [][] res= new double[n][];
            double precX = x0;
            double precY = y0;
            res[0] = new double[2];
            res[0][0] = x0;
            res[0][1] = y0;
            for (int i = 1; i < n; i++)
            {
                res[i] = new double[2];
                res[i][0]= FonctionX(precX,precY);
                res[i][1] = FonctionY(precX);
                precX = res[i][0];
                precY = res[i][1];

                serieX[i] = res[i][0];
                serieY[i] = res[i][1];
            }
            
            return ;
        }
        public int CalculNUE()
        {
            double[] serie = serieX;
            double[] erreur = new Takens(serie).ErreurApproximationMoyenne;
            double error;
            for (int i = 1; i < erreur.Length; i++)
            {
                error = Math.Abs(erreur[i + 1] - erreur[i]);
                if (error < 0.001)
                {
                    nue = i + 1;
                    return nue;
                }
            }
            return 0;
        }
        public int CalculNUC()
        {
            int res = 0;
            double nmse;
            double min = double.MaxValue;

            for (int i = 1; i < 50; i++)
            {
                DDG dg = new DDG(nue, i, serieX);
                nmse = DDG.NMSE();
                NMSE[i] = nmse;
                if (nmse < min)
                {
                    min = nmse;
                    res = i;
                }
            }
            return res;
        }

    }
}
