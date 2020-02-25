using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ОбходГрафа
{
    public partial class Form1 : Form
    {
        int V;
        double[,] A;
        double[,] p;
        double min;

        public Form1()
        {
            InitializeComponent();
        }

        private void nudV_ValueChanged(object sender, EventArgs e)
        {
            V = (int)nudV.Value;
            dgvMatrix.RowCount = dgvMatrix.ColumnCount = V;
            dgvleastWays.RowCount = dgvleastWays.ColumnCount = V;
            for (int i = 0; i < V; i++)
            {
                dgvMatrix[i, i].Value = 0;
                dgvMatrix[i, i].Style.BackColor = Color.Gray;
                dgvMatrix[i, i].ReadOnly = true;
                dgvleastWays[i, i].Value = 0;
                dgvleastWays[i, i].Style.BackColor = Color.Gray;
                dgvleastWays[i, i].ReadOnly = true;
                            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nudV_ValueChanged(sender, e);
        }

        bool BuildMatrix()
        {
            A = new double[V, V];

            bool bError = false;
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    try
                    {
                        A[i, j] = Convert.ToDouble(dgvMatrix[j, i].Value);
                    }
                    catch
                    {
                        bError = true;
                    }
            if (bError)
            {
                MessageBox.Show("Исправьте значения, помеченные красным!", "Ошибка ввода");
                return false;
            }
            else
                return true;
        }

        private void buTraverse_Click(object sender, EventArgs e)
        {
            double[,] A;
            A = new double[V, V];

            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    A[i, j] = Convert.ToDouble(dgvMatrix[j, i].Value);

        }

        //private void leastWays()
       // {
           // double[,] p;
           // p = new double[V, V];
       // }

        private void dgvMatrix_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int j = e.ColumnIndex, i = e.RowIndex;
            try
            {
                Convert.ToDouble(dgvMatrix[j, i].Value);
                dgvMatrix[j, i].Style.ForeColor = Color.Black;
            }
            catch
            {
                dgvMatrix[j, i].Style.ForeColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BuildMatrix() != true) return;
            double[,] p;
            p = new double[V, V];
            // матрица смежности A готова
            //min - минимальный путь, A - матрица смежности, p-матрица кратчайших путей.
            for (int k = 0; k < V; ++k)
            {
                for (int i = 0; i < V; ++i)
                {
                    for (int j = 0; j < V; ++j)
                    {
                        if (A[i,j] < A[i,k] + A[k,j])
                            min = A[i,j];
                        else min = A[i,k] + A[k,j];
                        //p[i,j] = min;
                        dgvleastWays[j, i].Value = min;
                    }
                }
            }
   
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void случайнаяМатрицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random x = new Random();
            for (int i = 0; i < V; i++)
                for (int j = 0; j < V; j++)
                    if (i!=j)
                    dgvMatrix[j, i].Value = Math.Round(x.NextDouble() * 20 - 10, 2);
            button1.Enabled = true;
        }

   

    
    }
}
