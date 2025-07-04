using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace RNA_miniprojet
{
    public partial class Form1 : Form
    {
        public int NBUE { get; set; }
        public int NBUC { get; set; }
        public SerieDeHenon srh;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        public void Generation()
        {
            srh = new SerieDeHenon(500, 1.4, 0.3);
            double[] dX = srh.serieX;
            double[] dY = srh.serieY;
            NBUE = srh.CalculNUE();
            NBUC = srh.CalculNUC();
            if (chart1.Series.Count !=1)
            {
                while (chart1.Series.Count!= 0)
                {
                    chart1.Series.RemoveAt(0);
                }
                chart1.Series.Add("Yn en fonction de Xn");
            }

            chart1.Series[0].Color = Color.Cyan;
            chart1.Series[0].ChartType = SeriesChartType.Point;
  

            for (int i = 1; i < srh.n; i++)
            {
                listBox1.Items.Add("x" + i + "= " + Math.Round(dX[i], 8));
                listBox2.Items.Add("y" + i + "= " + Math.Round(dY[i], 8));

                chart1.Series[0].Points.AddXY(dX[i], dY[i]);
            }

            label2.Text = "(" + NBUE + " , " + NBUC + " , 1 )";
        }
        private void buttonAppre_Click(object sender, EventArgs e)
        {

            DDG ddg = new DDG(NBUE, NBUC, srh.serieX);
            Apprentissage.W = ddg.Apprentissage();
            String[] wentree = new String[100];
            String[] wsortie = new String[100];
            int k = 0;
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            for (int i = 1; i <= DDG.NombreUniteCachee; i++)
            {
                for (int j = 1; j <= DDG.NombreUniteEntree; j++)
                {
                    wentree[k] = "W(2," + i + "," + j + ")=" + Apprentissage.W[2, i, j];
                    listBox3.Items.Add(wentree[k]);
                    k++;
                    
                    
                }
            }
            k = 0;
            for (int i = 1; i <= 1; i++)
            {
                for (int j = 1; j <= DDG.NombreUniteCachee; j++)
                {
                    wsortie[k] = "W(3," + i + "," + j + ")=" + Apprentissage.W[3, i, j];
                    listBox4.Items.Add(wsortie[k]);
                    k++;
                    
                }
            }
               
        }

        private void buttonPrediction_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectTab(3);
            int nb = int.Parse(comboBox1.Text);
            double[] res = new double[100];

            double[] s = srh.serieX;
            DDG ddg = new DDG(NBUE,NBUC, s);
            Apprentissage.W = ddg.Apprentissage();
            if (nb == 1)
            {
                res = ddg.PredictionAUnpas(Apprentissage.W);
            }
            else
            {
                res = ddg.PredictionAPlusieursPas(nb, Apprentissage.W);
            }
            
            List<Resultat> list = new List<Resultat>();
            for (int i = 0; i < res.Count(x => x != 0); i++)
            {
                Resultat resultat = new Resultat(i, DDG.ValeurAttendue[i], Math.Round(res[i], 8));
                list.Add(resultat);
            }
            dataGridView1.DataSource = list;
            
            DrawChartLs2(res,DDG.ValeurAttendue,list.Count);

        }

        private void DrawChartLs2(double[] Xpredite, double[] Xattendue,int n)
        {
            while (chart1.Series.Count != 0)
            {
                chart1.Series.RemoveAt(0);
            }

            chart1.Series.Add("Prédiction");
            chart1.Series.Add("Attendue");
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[0].Color = Color.DarkOrange;
            chart1.Series[1].Color = Color.DarkGreen;
            int iP = chart1.Series.IndexOf("Prédiction");
            int iA = chart1.Series.IndexOf("Attendue");
            for (int i = 0; i < n; i++)
            {
                 chart1.Series[iP].Points.AddXY(i+NBUE, Xpredite[i]);
            }

            for (int i = 0; i < n+NBUE; i++)
            {
                if (i < NBUE)
                {
                    chart1.Series[iA].Points.AddXY(i , srh.serieX[i]);
                }
                else
                {
                    chart1.Series[iA].Points.AddXY(i, Xattendue[i-NBUE]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Veuillez patienter S'il vous plaît ! La génération peut prendre plusieurs secondes!");
            Generation();
            buttonAppre.Enabled = true ;
            comboBox1.Enabled = true ;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonPrediction.Enabled = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
