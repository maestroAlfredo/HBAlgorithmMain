using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MathNet.Numerics;
using VoltageDropCalculatorApplication;



namespace VoltageDropCalculatorApplication
{
    public partial class voltageCalculationForm : Form
    {
        //double[,] voltageVector;
        //double[,] nodeVector;
        int nodeNum;
        double Vs;
        double p;
        string projName;
        double[,] newVoltageVector;
        double[,] newerNode;
        double[,] voltageProfileArray;
        int phaseCountCol;
        int phaseCountRow;
        double tolerance;
        double[,] feederProfileVoltage;
        double loadCountInt;
        double genCountInt;
        int totalLoadsGens;        
        

        DataSet nodeVecDataSet = new DataSet();

        public voltageCalculationForm(string projectName, double risk, int loadCount, int genCount)
        {
            InitializeComponent();


            tolerance = nodeFeederForm.lengthTol;
            nodeVecDataSet.ReadXml(projectName);
            //nodeVecDataGridView.DataSource = nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 4];
            //nodeVector = new double[nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 1].Rows.Count, 10];
            //voltageVector = new double[nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 1].Rows.Count, 35];
            nodeNum = nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 1].Rows.Count / (loadCount + genCount);
            projName = projectName;
            Vs = 230;
            p = risk;
            feederProfileVoltage = new double[nodeNum + 1, 1];
            feederProfileVoltage[0, 0] = Vs;
            voltageProfileArray = new double[nodeNum + 1, 6];
            phaseCountRow = 1;
            phaseCountCol = 0;
            tolerance = 0.1;
            numericUpDownVoltage.Value = Convert.ToDecimal(Vs);
            numericUpDownRisk.Value = Convert.ToDecimal(p);
            loadCountInt = loadCount;
            genCountInt = genCount;
            totalLoadsGens = loadCount + genCount;


            //voltageProfileChart.Titles.Add("3 Phase Feeder Voltage Profile");
            //voltageProfileChart.Titles[0].Font = new System.Drawing.Font("Arial", 24, FontStyle.Bold);

            //initialize the node vector to zero
            

   

            //reads the project name and sets the datagridview datasource to the node tables.. Merges all the node tables together.
            DataSet nodeTableDataSet = new DataSet();
            nodeTableDataSet.ReadXml(projName);

            DataTable nodeOverallDataTable = new DataTable();
            nodeOverallDataTable = nodeTableDataSet.Tables[0].Copy();


            nodeSummaryDataGridView.DataSource = nodeOverallDataTable;
            //nodeSummaryDataGridView.Columns[9].Visible = false;
            //nodeSummaryDataGridView.Columns[10].Visible = false;
            //nodeSummaryDataGridView.Columns[11].Visible = false;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(projName);

            for (int i = 0; i < 4; i++)
            {
                ds.Tables.Remove(ds.Tables[ds.Tables.Count - 1]);
            }

            ds.WriteXml(projName);
            this.Close();
        }

        private double[,] voltagedrop(double Vs, double p, char phase, double[,] nodeVector) // voltagedrop(vs, p, phase, nodeVector)
        {
            double[,] voltageVector = new double[nodeVector.GetLength(0), 35];
            

            double ma = 0; double mb = 0; double mc = 0; double c = 0;
            int count = 0;
            //try
            // {
            for (int rows = 0; rows < nodeVector.GetLength(0); rows++)
            {
                count++;

                if (count > totalLoadsGens) count = 1;
                // the red phase is selected.
                if (phase == 'r') { ma = nodeVector[rows, 0]; mb = nodeVector[rows, 1]; mc = nodeVector[rows, 2]; c = nodeVector[rows, 5]; }
                //// if the white phase is selected.
                if (phase == 'w') { ma = nodeVector[rows, 1]; mb = nodeVector[rows, 2]; mc = nodeVector[rows, 0]; c = nodeVector[rows, 5]; }
                //// if the blue phase is selected.
                if (phase == 'b') { ma = nodeVector[rows, 2]; mb = nodeVector[rows, 0]; mc = nodeVector[rows, 1]; c = nodeVector[rows, 5]; }

                if (count <= loadCountInt)
                {
                    if (rows == 0)
                    {

                        voltageVector[rows, 0] = nodeVector[rows, 3] / (nodeVector[rows, 3] + nodeVector[rows, 4]);
                        double Gi = voltageVector[rows, 0];
                        voltageVector[rows, 1] = nodeVector[rows, 3] * (nodeVector[rows, 3] + 1) / ((nodeVector[rows, 3] + nodeVector[rows, 4]) * (nodeVector[rows, 3] + nodeVector[rows, 4] + 1));
                        double Hi = voltageVector[rows, 1];
                        voltageVector[rows, 2] = nodeVector[rows, 8];
                        double Rp = voltageVector[rows, 2];
                        voltageVector[rows, 3] = nodeVector[rows, 9];
                        double Rn = voltageVector[rows, 3];
                        voltageVector[rows, 4] = voltageVector[rows, 3] / voltageVector[rows, 2];
                        double ki = voltageVector[rows, 4];
                        voltageVector[rows, 5] = ((1 + ki) * Rp * c * (ma * 1)) - (0.5 * ki * Rp * c * (mb * 0 + mc * 0));
                        double Vr_min = voltageVector[rows, 5];
                        voltageVector[rows, 6] = (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 0 - mc * 0);
                        double Vi_min = voltageVector[rows, 6];
                        voltageVector[rows, 7] = ((1 + ki) * Rp * c * (ma * 0)) - (0.5 * ki * Rp * c * (mb * 1 + mc * 1));
                        double Vr_max = voltageVector[rows, 7];
                        voltageVector[rows, 8] = (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 1 - mc * 1);
                        double Vi_max = voltageVector[rows, 8];
                        voltageVector[rows, 9] = Math.Sqrt(((Math.Pow((Vs - Vr_max), 2)) + (Math.Pow(Vi_max, 2))));
                        double Vmax = voltageVector[rows, 9];
                        voltageVector[rows, 10] = Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                        double Vmin = voltageVector[rows, 10];
                        voltageVector[rows, 11] = (1 + ki) * ma - ki * 0.5 * (mb + mc);
                        double C1 = voltageVector[rows, 11];
                        voltageVector[rows, 12] = (Math.Pow(ki, 2)) * ((Math.Abs(ma) + 0.25 * Math.Abs(mb) + 0.25 * Math.Abs(mc)) + (2 * ki + 1) * Math.Abs(ma));
                        double C2 = voltageVector[rows, 12];

                        double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1) * (Math.Abs(mb) + Math.Abs(mc));
                        double F2 = Math.Abs(ma) * (2 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2);
                        double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1);

                        voltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                        double C3 = voltageVector[rows, 13];
                        voltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                        double C4 = voltageVector[rows, 14];
                        voltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow((Math.Abs(mb) - Math.Abs(mc)), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                        double C5 = voltageVector[rows, 15];
                        voltageVector[rows, 16] = 0.5 * (Math.Sqrt(3)) * ki * (mb - mc);
                        double C6 = voltageVector[rows, 16];
                        voltageVector[rows, 17] = C1 * Rp * c * Gi;
                        double E_DVre = voltageVector[rows, 17];
                        voltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                        double E_DVre2 = voltageVector[rows, 18];
                        voltageVector[rows, 19] = C6 * Rp * c * Gi;
                        double E_DVim = voltageVector[rows, 19];
                        voltageVector[rows, 20] = (Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2)));
                        double E_DVim2 = voltageVector[rows, 20];
                        voltageVector[rows, 21] = 0;
                        double Sum_DVre = voltageVector[rows, 21];
                        voltageVector[rows, 22] = 0;
                        double Sum_DVim = voltageVector[rows, 22];
                        voltageVector[rows, 23] = E_DVre;
                        double E_DVr = voltageVector[rows, 23];
                        voltageVector[rows, 24] = E_DVre2 + (2 * Sum_DVre * E_DVre);
                        double E_DVr2 = voltageVector[rows, 24];
                        voltageVector[rows, 25] = 0;
                        double E_DVi = voltageVector[rows, 25];
                        voltageVector[rows, 26] = E_DVim2 + (2 * Sum_DVim * E_DVim);
                        double E_DVi2 = voltageVector[rows, 26];
                        voltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                        double E_Vc = voltageVector[rows, 27];
                        voltageVector[rows, 28] = Math.Pow(Vs, 2) - (2 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                        double E_Vc2 = voltageVector[rows, 28];
                        voltageVector[rows, 29] = (E_Vc - Vmin) / (Vmax - Vmin);
                        double E_vc = voltageVector[rows, 29];
                        voltageVector[rows, 30] = (E_Vc2 - (2 * Vmin * E_Vc) + (Math.Pow(Vmin, 2))) / Math.Pow((Vmax - Vmin), 2);
                        double E_vc2 = voltageVector[rows, 30];
                        voltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                        double alpha_ = voltageVector[rows, 31];
                        voltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                        double beta_ = voltageVector[rows, 32];


                        if (Vmax > Vmin)
                        {
                            if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                            {
                                //voltageVector[rows, 33] = voltageVector[rows, 33];
                                //double v_percent = voltageVector[rows, 33];
                                //voltageVector[rows, 34] = Vs;
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }

                        else
                        {
                            if (double.IsNaN(voltageVector[rows, 31]) || double.IsNaN(voltageVector[rows, 30]))
                            {
                                //voltageVector[rows, 33] = voltageVector[rows, 33];
                                //voltageVector[rows, 34] = Vs;
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(voltageVector[rows, 31]), Math.Abs(voltageVector[rows, 30]));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }
                    }
                    else
                    {

                        int z = rows - 1;  // z = i - 1;

                        voltageVector[rows, 0] = nodeVector[rows, 3] / (nodeVector[rows, 3] + nodeVector[rows, 4]);
                        double Gi = voltageVector[rows, 0];
                        voltageVector[rows, 1] = nodeVector[rows, 3] * (nodeVector[rows, 3] + 1) / ((nodeVector[rows, 3] + nodeVector[rows, 4]) * (nodeVector[rows, 3] + nodeVector[rows, 4] + 1));
                        double Hi = voltageVector[rows, 1];
                        voltageVector[rows, 2] = voltageVector[z, 2] + nodeVector[rows, 8];
                        double Rp = voltageVector[rows, 2];
                        voltageVector[rows, 3] = voltageVector[z, 3] + nodeVector[rows, 9];
                        double Rn = voltageVector[rows, 3];
                        voltageVector[rows, 4] = voltageVector[rows, 3] / voltageVector[rows, 2];
                        double ki = voltageVector[rows, 4];


                        //ma = nodeVector[rows, 0]; mb = nodeVector[rows, 1]; mc = nodeVector[rows, 2]; c = nodeVector[rows, 5];

                        voltageVector[rows, 5] = voltageVector[z, 5] + ((1 + ki) * Rp * c * (ma * 1)) - (0.5 * ki * Rp * c * (mb * 0 + mc * 0));
                        double Vr_min = voltageVector[rows, 5];
                        voltageVector[rows, 6] = voltageVector[z, 6] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 0 - mc * 0);
                        double Vi_min = voltageVector[rows, 6];
                        voltageVector[rows, 7] = voltageVector[z, 7] + ((1 + ki) * Rp * c * (ma * 0) - (0.5 * ki * Rp * c * (mb * 1 + mc * 1)));
                        double Vr_max = voltageVector[rows, 7];
                        voltageVector[rows, 8] = voltageVector[z, 8] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 1 - mc * 1);
                        double Vi_max = voltageVector[rows, 8];
                        voltageVector[rows, 9] = Math.Sqrt(Math.Pow(Vs - Vr_max, 2) + Math.Pow(Vi_max, 2));
                        double Vmax = voltageVector[rows, 9];
                        voltageVector[rows, 10] = Math.Sqrt(Math.Pow(Vs - Vr_min, 2) + Math.Pow(Vi_min, 2));
                        double Vmin = voltageVector[rows, 10];
                        voltageVector[rows, 11] = (1 + ki) * ma - ki * 0.5 * (mb + mc);
                        double C1 = voltageVector[rows, 11];
                        voltageVector[rows, 12] = (Math.Pow(ki, 2)) * (Math.Abs(ma) + 0.25 * Math.Abs(mb) + 0.25 * Math.Abs(mc) + (2 * ki + 1) * Math.Abs(ma));
                        double C2 = voltageVector[rows, 12];

                        double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1) * (Math.Abs(mb) + Math.Abs(mc));
                        double F2 = Math.Abs(ma) * (2 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2); double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1);

                        voltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                        double C3 = voltageVector[rows, 13];
                        voltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                        double C4 = voltageVector[rows, 14];
                        voltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow(Math.Abs(mb) - Math.Abs(mc), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                        double C5 = voltageVector[rows, 15];
                        voltageVector[rows, 16] = 0.5 * (Math.Sqrt(3)) * ki * ((mb) - (mc));
                        double C6 = voltageVector[rows, 16];
                        voltageVector[rows, 17] = C1 * Rp * c * Gi;
                        double E_DVre = voltageVector[rows, 17];
                        voltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                        double E_DVre2 = voltageVector[rows, 18];
                        voltageVector[rows, 19] = C6 * Rp * c * Gi;
                        double E_DVim = voltageVector[rows, 19];
                        voltageVector[rows, 20] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2));
                        double E_DVim2 = voltageVector[rows, 20];
                        voltageVector[rows, 21] = voltageVector[z, 17] + voltageVector[z, 21];
                        double Sum_DVre = voltageVector[rows, 21];
                        voltageVector[rows, 22] = voltageVector[z, 19] + voltageVector[z, 22];
                        double Sum_DVim = voltageVector[rows, 22];
                        voltageVector[rows, 23] = voltageVector[z, 23] + E_DVre;
                        double E_DVr = voltageVector[rows, 23];
                        voltageVector[rows, 24] = voltageVector[z, 24] + E_DVre2 + (2 * Sum_DVre * E_DVre);
                        double E_DVr2 = voltageVector[rows, 24];
                        voltageVector[rows, 25] = voltageVector[z, 25] + E_DVim;
                        double E_DVi = voltageVector[rows, 25];
                        voltageVector[rows, 26] = voltageVector[z, 26] + E_DVim2 + (2 * Sum_DVim * E_DVim);
                        double E_DVi2 = voltageVector[rows, 26];
                        voltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                        double E_Vc = voltageVector[rows, 27];
                        voltageVector[rows, 28] = Math.Pow(Vs, 2) - (2 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                        double E_Vc2 = voltageVector[rows, 28];
                        voltageVector[rows, 29] = (E_Vc - Vmin) / (Vmax - Vmin);
                        double E_vc = voltageVector[rows, 29];
                        voltageVector[rows, 30] = (E_Vc2 - (2 * Vmin * E_Vc) + (Math.Pow(Vmin, 2))) / Math.Pow((Vmax - Vmin), 2);
                        double E_vc2 = voltageVector[rows, 30];
                        voltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                        double alpha_ = voltageVector[rows, 31];
                        voltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                        double beta_ = voltageVector[rows, 32];

                        if (Vmax > Vmin)
                        {
                            if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                            {
                                //voltageVector[rows, 33] = voltageVector[z, 33];
                                //voltageVector[rows, 34] = voltageVector[z, 34];
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = voltageVector[rows, 34];

                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }

                        else
                        {
                            if (double.IsNaN(voltageVector[rows, 31]) || double.IsNaN(voltageVector[rows, 30]))
                            {
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(voltageVector[rows, 31]), Math.Abs(voltageVector[rows, 30]));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }
                    }


                }
                else
                {
                    int z = rows - 1;  // z = i - 1;

                    voltageVector[rows, 0] = nodeVector[rows, 3] / (nodeVector[rows, 3] + nodeVector[rows, 4]);
                    double Gi = voltageVector[rows, 0];
                    voltageVector[rows, 1] = nodeVector[rows, 3] * (nodeVector[rows, 3] + 1) / ((nodeVector[rows, 3] + nodeVector[rows, 4]) * (nodeVector[rows, 3] + nodeVector[rows, 4] + 1));
                    double Hi = voltageVector[rows, 1];
                    voltageVector[rows, 2] = voltageVector[z, 2] + nodeVector[rows, 8];
                    double Rp = voltageVector[rows, 2];
                    voltageVector[rows, 3] = voltageVector[z, 3] + nodeVector[rows, 9];
                    double Rn = voltageVector[rows, 3];
                    voltageVector[rows, 4] = voltageVector[rows, 3] / voltageVector[rows, 2];
                    double ki = voltageVector[rows, 4];

                    //ma = nodeVector[rows, 0]; mb = nodeVector[rows, 1]; mc = nodeVector[rows, 2]; c = nodeVector[rows, 5];

                    voltageVector[rows, 5] = voltageVector[z, 5] + ((1 + ki) * Rp * c * (ma * 0)) - (0.5 * ki * Rp * c * (mb * 1 + mc * 1));
                    double Vr_min = voltageVector[rows, 5];
                    voltageVector[rows, 6] = voltageVector[z, 6] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 1 - mc * 1);
                    double Vi_min = voltageVector[rows, 6];
                    voltageVector[rows, 7] = voltageVector[z, 7] + ((1 + ki) * Rp * c * (ma * 1) - (0.5 * ki * Rp * c * (mb * 0 + mc * 0)));
                    double Vr_max = voltageVector[rows, 7];
                    voltageVector[rows, 8] = voltageVector[z, 8] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 0 - mc * 0);
                    double Vi_max = voltageVector[rows, 8];
                    voltageVector[rows, 9] = Math.Sqrt(Math.Pow(Vs - Vr_max, 2) + Math.Pow(Vi_max, 2));
                    double Vmax = voltageVector[rows, 9];
                    voltageVector[rows, 10] = Math.Sqrt(Math.Pow(Vs - Vr_min, 2) + Math.Pow(Vi_min, 2));
                    double Vmin = voltageVector[rows, 10];
                    voltageVector[rows, 11] = (1 + ki) * ma - ki * 0.5 * (mb + mc);
                    double C1 = voltageVector[rows, 11];
                    voltageVector[rows, 12] = (Math.Pow(ki, 2)) * (Math.Abs(ma) + 0.25 * Math.Abs(mb) + 0.25 * Math.Abs(mc) + (2 * ki + 1) * Math.Abs(ma));
                    double C2 = voltageVector[rows, 12];

                    double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1) * (Math.Abs(mb) + Math.Abs(mc));
                    double F2 = Math.Abs(ma) * (2 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2); double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1);

                    voltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                    double C3 = voltageVector[rows, 13];
                    voltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                    double C4 = voltageVector[rows, 14];
                    voltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow(Math.Abs(mb) - Math.Abs(mc), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                    double C5 = voltageVector[rows, 15];
                    voltageVector[rows, 16] = 0.5 * (Math.Sqrt(3)) * ki * (mb - mc);
                    double C6 = voltageVector[rows, 16];
                    voltageVector[rows, 17] = C1 * Rp * c * Gi;
                    double E_DVre = voltageVector[rows, 17];
                    voltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                    double E_DVre2 = voltageVector[rows, 18];
                    voltageVector[rows, 19] = C6 * Rp * c * Gi;
                    double E_DVim = voltageVector[rows, 19];
                    voltageVector[rows, 20] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2));
                    double E_DVim2 = voltageVector[rows, 20];
                    voltageVector[rows, 21] = voltageVector[z, 17] + voltageVector[z, 21];
                    double Sum_DVre = voltageVector[rows, 21];
                    voltageVector[rows, 22] = voltageVector[z, 19] + voltageVector[z, 22];
                    double Sum_DVim = voltageVector[rows, 22];
                    voltageVector[rows, 23] = voltageVector[z, 23] + E_DVre;
                    double E_DVr = voltageVector[rows, 23];
                    voltageVector[rows, 24] = voltageVector[z, 24] + E_DVre2 + (2 * Sum_DVre * E_DVre);
                    double E_DVr2 = voltageVector[rows, 24];
                    voltageVector[rows, 25] = voltageVector[z, 25] + E_DVim;
                    double E_DVi = voltageVector[rows, 25];
                    voltageVector[rows, 26] = voltageVector[z, 26] + E_DVim2 + (2 * Sum_DVim * E_DVim);
                    double E_DVi2 = voltageVector[rows, 26];
                    voltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                    double E_Vc = voltageVector[rows, 27];
                    voltageVector[rows, 28] = Math.Pow(Vs, 2) - (2 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                    double E_Vc2 = voltageVector[rows, 28];
                    voltageVector[rows, 29] = (E_Vc - Vmin) / (Vmax - Vmin);
                    double E_vc = voltageVector[rows, 29];
                    voltageVector[rows, 30] = (E_Vc2 - (2 * Vmin * E_Vc) + (Math.Pow(Vmin, 2))) / Math.Pow((Vmax - Vmin), 2);
                    double E_vc2 = voltageVector[rows, 30];
                    voltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                    double alpha_ = voltageVector[rows, 31];
                    voltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                    double beta_ = voltageVector[rows, 32];

                    if (Vmax > Vmin)
                    {
                        if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                        {
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                            double Vc_percent = voltageVector[rows, 34];
                        }
                        else
                        {
                            voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                            double Vc_percent = voltageVector[rows, 34];
                        }
                    }

                    else
                    {
                        if (double.IsNaN(voltageVector[rows, 31]) || double.IsNaN(voltageVector[rows, 30]))
                        {
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                            double Vc_percent = voltageVector[rows, 34];
                        }
                        else
                        {
                            voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(voltageVector[rows, 31]), Math.Abs(voltageVector[rows, 30]));
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                            double Vc_percent = voltageVector[rows, 34];
                        }
                    }
                }
            }

            return voltageVector;
        }

        private void nodeVoltagedrop(double[,] newNodeVector, double Vs, double bc, char cd) // voltagedrop(vs, p, phase)
        {



            int rows4debug = 0; // so i can display the values of row,34 out of the for loop

            double ma = 0; double mb = 0; double mc = 0; double c = 0;
            int count = 0;
            double[,] newVoltageVector = new double[newNodeVector.GetLength(0), 35];
            this.newVoltageVector = newVoltageVector;
            //try
            // {
            for (int rows = 0; rows < newNodeVector.GetLength(0); rows++)
            {

                //Vs = 230;
                //if (count > 10) count = 1;
                // the red phase is selected.
                if (cd == 'r') { ma = newNodeVector[rows, 0]; mb = newNodeVector[rows, 1]; mc = newNodeVector[rows, 2]; c = newNodeVector[rows, 5]; }
                //// if the white phase is selected.
                if (cd == 'w') { ma = newNodeVector[rows, 1]; mb = newNodeVector[rows, 2]; mc = newNodeVector[rows, 0]; c = newNodeVector[rows, 5]; }
                //// if the blue phase is selected.
                if (cd == 'b') { ma = newNodeVector[rows, 2]; mb = newNodeVector[rows, 0]; mc = newNodeVector[rows, 1]; c = newNodeVector[rows, 5]; }

                //if (count <= 5)
                if (count % 2 == 0)
                {
                    if (rows == 0)
                    {


                        newVoltageVector[rows, 0] = newNodeVector[rows, 3] / (newNodeVector[rows, 3] + newNodeVector[rows, 4]);
                        double Gi = newVoltageVector[rows, 0];
                        newVoltageVector[rows, 1] = newNodeVector[rows, 3] * (newNodeVector[rows, 3] + 1) / ((newNodeVector[rows, 3] + newNodeVector[rows, 4]) * (newNodeVector[rows, 3] + newNodeVector[rows, 4] + 1));
                        double Hi = newVoltageVector[rows, 1];
                        newVoltageVector[rows, 2] = newNodeVector[rows, 8];
                        double Rp = newVoltageVector[rows, 2];
                        newVoltageVector[rows, 3] = newNodeVector[rows, 9];
                        double Rn = newVoltageVector[rows, 3];
                        newVoltageVector[rows, 4] = newVoltageVector[rows, 3] / newVoltageVector[rows, 2];
                        double ki = newVoltageVector[rows, 4];
                        newVoltageVector[rows, 5] = ((1 + ki) * Rp * c * (ma * 1)) - (0.5 * ki * Rp * c * (mb * 0 + mc * 0));
                        double Vr_min = newVoltageVector[rows, 5];
                        newVoltageVector[rows, 6] = (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 0 - mc * 0);
                        double Vi_min = newVoltageVector[rows, 6];
                        newVoltageVector[rows, 7] = ((1 + ki) * Rp * c * (ma * 0)) - (0.5 * ki * Rp * c * (mb * 1 + mc * 1));
                        double Vr_max = newVoltageVector[rows, 7];
                        newVoltageVector[rows, 8] = (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 1 - mc * 1);
                        double Vi_max = newVoltageVector[rows, 8];
                        newVoltageVector[rows, 9] = Math.Sqrt(((Math.Pow((Vs - Vr_max), 2)) + (Math.Pow(Vi_max, 2))));
                        double Vmax = newVoltageVector[rows, 9];
                        newVoltageVector[rows, 10] = Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                        double Vmin = newVoltageVector[rows, 10];
                        newVoltageVector[rows, 11] = (1 + ki) * ma - ki * 0.5 * (mb + mc);
                        double C1 = newVoltageVector[rows, 11];
                        newVoltageVector[rows, 12] = (Math.Pow(ki, 2)) * ((Math.Abs(ma) + 0.25 * Math.Abs(mb) + 0.25 * Math.Abs(mc)) + (2 * ki + 1) * Math.Abs(ma));
                        double C2 = newVoltageVector[rows, 12];

                        double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1) * (Math.Abs(mb) + Math.Abs(mc));
                        double F2 = Math.Abs(ma) * (2 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2);
                        double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1);

                        newVoltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                        double C3 = newVoltageVector[rows, 13];
                        newVoltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                        double C4 = newVoltageVector[rows, 14];
                        newVoltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow((Math.Abs(mb) - Math.Abs(mc)), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                        double C5 = newVoltageVector[rows, 15];
                        newVoltageVector[rows, 16] = 0.5 * (Math.Sqrt(3)) * ki * (mb - mc);
                        double C6 = newVoltageVector[rows, 16];
                        newVoltageVector[rows, 17] = C1 * Rp * c * Gi;
                        double E_DVre = newVoltageVector[rows, 17];
                        newVoltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                        double E_DVre2 = newVoltageVector[rows, 18];
                        newVoltageVector[rows, 19] = C6 * Rp * c * Gi;
                        double E_DVim = newVoltageVector[rows, 19];
                        newVoltageVector[rows, 20] = (Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2)));
                        double E_DVim2 = newVoltageVector[rows, 20];
                        newVoltageVector[rows, 21] = 0;
                        double Sum_DVre = newVoltageVector[rows, 21];
                        newVoltageVector[rows, 22] = 0;
                        double Sum_DVim = newVoltageVector[rows, 22];
                        newVoltageVector[rows, 23] = E_DVre;
                        double E_DVr = newVoltageVector[rows, 23];
                        newVoltageVector[rows, 24] = E_DVre2 + (2 * Sum_DVre * E_DVre);
                        double E_DVr2 = newVoltageVector[rows, 24];
                        newVoltageVector[rows, 25] = 0;
                        double E_DVi = newVoltageVector[rows, 25];
                        newVoltageVector[rows, 26] = E_DVim2 + (2 * Sum_DVim * E_DVim);
                        double E_DVi2 = newVoltageVector[rows, 26];
                        newVoltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                        double E_Vc = newVoltageVector[rows, 27];
                        newVoltageVector[rows, 28] = Math.Pow(Vs, 2) - (2 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                        double E_Vc2 = newVoltageVector[rows, 28];
                        newVoltageVector[rows, 29] = (E_Vc - Vmin) / (Vmax - Vmin);
                        double E_vc = newVoltageVector[rows, 29];
                        newVoltageVector[rows, 30] = (E_Vc2 - (2 * Vmin * E_Vc) + (Math.Pow(Vmin, 2))) / Math.Pow((Vmax - Vmin), 2);
                        double E_vc2 = newVoltageVector[rows, 30];
                        newVoltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                        double alpha_ = newVoltageVector[rows, 31];
                        newVoltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                        double beta_ = newVoltageVector[rows, 32];


                        if (Vmax > Vmin)
                        {
                            if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                            {
                                //newVoltageVector[rows, 33] = newVoltageVector[rows, 33];
                                //double v_percent = newVoltageVector[rows, 33];
                                //newVoltageVector[rows, 34] = Vs;
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                            else
                            {
                                newVoltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                        }

                        else
                        {
                            if (double.IsNaN(newVoltageVector[rows, 31]) || double.IsNaN(newVoltageVector[rows, 30]))
                            {
                                //newVoltageVector[rows, 33] = newVoltageVector[rows, 33];
                                //newVoltageVector[rows, 34] = Vs;
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                            else
                            {
                                newVoltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(newVoltageVector[rows, 31]), Math.Abs(newVoltageVector[rows, 30]));
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                        }
                        //  Vr[vCount] = newVoltageVector[rows, 34];
                    }

                    else
                    {
                        // Vs = Vr[vCount - 1];
                        //Vs = newVoltageVector[rows - 1, 34];
                        // Debug.Print("This is Vr[" + vCount + "] at load - else: " + Vr[vCount].ToString());

                        int z = rows - 1;  // z = i - 1;

                        newVoltageVector[rows, 0] = newNodeVector[rows, 3] / (newNodeVector[rows, 3] + newNodeVector[rows, 4]);
                        double Gi = newVoltageVector[rows, 0];
                        newVoltageVector[rows, 1] = newNodeVector[rows, 3] * (newNodeVector[rows, 3] + 1) / ((newNodeVector[rows, 3] + newNodeVector[rows, 4]) * (newNodeVector[rows, 3] + newNodeVector[rows, 4] + 1));
                        double Hi = newVoltageVector[rows, 1];
                        newVoltageVector[rows, 2] = newVoltageVector[z, 2] + newNodeVector[rows, 8];
                        double Rp = newVoltageVector[rows, 2];
                        newVoltageVector[rows, 3] = newVoltageVector[z, 3] + newNodeVector[rows, 9];
                        double Rn = newVoltageVector[rows, 3];
                        newVoltageVector[rows, 4] = newVoltageVector[rows, 3] / newVoltageVector[rows, 2];
                        double ki = newVoltageVector[rows, 4];


                        //ma = newNodeVector[rows, 0]; mb = newNodeVector[rows, 1]; mc = newNodeVector[rows, 2]; c = newNodeVector[rows, 5];

                        newVoltageVector[rows, 5] = newVoltageVector[z, 5] + ((1 + ki) * Rp * c * (ma * 1)) - (0.5 * ki * Rp * c * (mb * 0 + mc * 0));
                        double Vr_min = newVoltageVector[rows, 5];
                        newVoltageVector[rows, 6] = newVoltageVector[z, 6] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 0 - mc * 0);
                        double Vi_min = newVoltageVector[rows, 6];
                        newVoltageVector[rows, 7] = newVoltageVector[z, 7] + ((1 + ki) * Rp * c * (ma * 0) - (0.5 * ki * Rp * c * (mb * 1 + mc * 1)));
                        double Vr_max = newVoltageVector[rows, 7];
                        newVoltageVector[rows, 8] = newVoltageVector[z, 8] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 1 - mc * 1);
                        double Vi_max = newVoltageVector[rows, 8];
                        newVoltageVector[rows, 9] = Math.Sqrt(Math.Pow(Vs - Vr_max, 2) + Math.Pow(Vi_max, 2));
                        double Vmax = newVoltageVector[rows, 9];
                        newVoltageVector[rows, 10] = Math.Sqrt(Math.Pow(Vs - Vr_min, 2) + Math.Pow(Vi_min, 2));
                        double Vmin = newVoltageVector[rows, 10];
                        newVoltageVector[rows, 11] = (1 + ki) * ma - ki * 0.5 * (mb + mc);
                        double C1 = newVoltageVector[rows, 11];
                        newVoltageVector[rows, 12] = (Math.Pow(ki, 2)) * (Math.Abs(ma) + 0.25 * Math.Abs(mb) + 0.25 * Math.Abs(mc) + (2 * ki + 1) * Math.Abs(ma));
                        double C2 = newVoltageVector[rows, 12];

                        double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1) * (Math.Abs(mb) + Math.Abs(mc));
                        double F2 = Math.Abs(ma) * (2 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2); double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1);

                        newVoltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                        double C3 = newVoltageVector[rows, 13];
                        newVoltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                        double C4 = newVoltageVector[rows, 14];
                        newVoltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow(Math.Abs(mb) - Math.Abs(mc), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                        double C5 = newVoltageVector[rows, 15];
                        newVoltageVector[rows, 16] = 0.5 * (Math.Sqrt(3)) * ki * (mb - mc);
                        double C6 = newVoltageVector[rows, 16];
                        newVoltageVector[rows, 17] = C1 * Rp * c * Gi;
                        double E_DVre = newVoltageVector[rows, 17];
                        newVoltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                        double E_DVre2 = newVoltageVector[rows, 18];
                        newVoltageVector[rows, 19] = C6 * Rp * c * Gi;
                        double E_DVim = newVoltageVector[rows, 19];
                        newVoltageVector[rows, 20] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2));
                        double E_DVim2 = newVoltageVector[rows, 20];
                        newVoltageVector[rows, 21] = newVoltageVector[z, 17] + newVoltageVector[z, 21];
                        double Sum_DVre = newVoltageVector[rows, 21];
                        newVoltageVector[rows, 22] = newVoltageVector[z, 19] + newVoltageVector[z, 22];
                        double Sum_DVim = newVoltageVector[rows, 22];
                        newVoltageVector[rows, 23] = newVoltageVector[z, 23] + E_DVre;
                        double E_DVr = newVoltageVector[rows, 23];
                        newVoltageVector[rows, 24] = newVoltageVector[z, 24] + E_DVre2 + (2 * Sum_DVre * E_DVre);
                        double E_DVr2 = newVoltageVector[rows, 24];
                        newVoltageVector[rows, 25] = newVoltageVector[z, 25] + E_DVim;
                        double E_DVi = newVoltageVector[rows, 25];
                        newVoltageVector[rows, 26] = newVoltageVector[z, 26] + E_DVim2 + (2 * Sum_DVim * E_DVim);
                        double E_DVi2 = newVoltageVector[rows, 26];
                        newVoltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                        double E_Vc = newVoltageVector[rows, 27];
                        newVoltageVector[rows, 28] = Math.Pow(Vs, 2) - (2 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                        double E_Vc2 = newVoltageVector[rows, 28];
                        newVoltageVector[rows, 29] = (E_Vc - Vmin) / (Vmax - Vmin);
                        double E_vc = newVoltageVector[rows, 29];
                        newVoltageVector[rows, 30] = (E_Vc2 - (2 * Vmin * E_Vc) + (Math.Pow(Vmin, 2))) / Math.Pow((Vmax - Vmin), 2);
                        double E_vc2 = newVoltageVector[rows, 30];
                        newVoltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                        double alpha_ = newVoltageVector[rows, 31];
                        newVoltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                        double beta_ = newVoltageVector[rows, 32];

                        if (Vmax > Vmin)
                        {
                            if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                            {
                                //newVoltageVector[rows, 33] = newVoltageVector[z, 33];
                                //newVoltageVector[rows, 34] = newVoltageVector[z, 34];
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = newVoltageVector[rows, 34];

                            }
                            else
                            {
                                newVoltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                        }

                        else
                        {
                            if (double.IsNaN(newVoltageVector[rows, 31]) || double.IsNaN(newVoltageVector[rows, 30]))
                            {
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                            else
                            {
                                newVoltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(newVoltageVector[rows, 31]), Math.Abs(newVoltageVector[rows, 30]));
                                double v_percent = newVoltageVector[rows, 33];
                                newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                                double Vc_percent = newVoltageVector[rows, 34];
                            }
                        }
                        //Vr[vCount] = newVoltageVector[rows, 34];
                    }
                }

                //else
                if (count % 2 != 0)
                {
                    //Vs = newVoltageVector[rows - 1, 34];
                    //Vs = Vr[vCount - 1];

                    //  Debug.Print("This is Vr[" + vCount + "] at DG: " + Vr[vCount].ToString());
                    int z = rows - 1;  // z = i - 1;

                    newVoltageVector[rows, 0] = newNodeVector[rows, 3] / (newNodeVector[rows, 3] + newNodeVector[rows, 4]);
                    double Gi = newVoltageVector[rows, 0];
                    newVoltageVector[rows, 1] = newNodeVector[rows, 3] * (newNodeVector[rows, 3] + 1) / ((newNodeVector[rows, 3] + newNodeVector[rows, 4]) * (newNodeVector[rows, 3] + newNodeVector[rows, 4] + 1));
                    double Hi = newVoltageVector[rows, 1];
                    newVoltageVector[rows, 2] = newVoltageVector[z, 2] + newNodeVector[rows, 8];
                    double Rp = newVoltageVector[rows, 2];
                    newVoltageVector[rows, 3] = newVoltageVector[z, 3] + newNodeVector[rows, 9];
                    double Rn = newVoltageVector[rows, 3];
                    newVoltageVector[rows, 4] = newVoltageVector[rows, 3] / newVoltageVector[rows, 2];
                    double ki = newVoltageVector[rows, 4];

                    //ma = newNodeVector[rows, 0]; mb = newNodeVector[rows, 1]; mc = newNodeVector[rows, 2]; c = nodeVector[rows, 5];

                    newVoltageVector[rows, 5] = newVoltageVector[z, 5] + ((1 + ki) * Rp * c * (ma * 0)) - (0.5 * ki * Rp * c * (mb * 1 + mc * 1));
                    double Vr_min = newVoltageVector[rows, 5];
                    newVoltageVector[rows, 6] = newVoltageVector[z, 6] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 1 - mc * 1);
                    double Vi_min = newVoltageVector[rows, 6];
                    newVoltageVector[rows, 7] = newVoltageVector[z, 7] + ((1 + ki) * Rp * c * (ma * 1) - (0.5 * ki * Rp * c * (mb * 0 + mc * 0)));
                    double Vr_max = newVoltageVector[rows, 7];
                    newVoltageVector[rows, 8] = newVoltageVector[z, 8] + (Math.Sqrt(3) / 2) * ki * Rp * c * (mb * 0 - mc * 0);
                    double Vi_max = newVoltageVector[rows, 8];
                    newVoltageVector[rows, 9] = Math.Sqrt(Math.Pow(Vs - Vr_max, 2) + Math.Pow(Vi_max, 2));
                    double Vmax = newVoltageVector[rows, 9];
                    newVoltageVector[rows, 10] = Math.Sqrt(Math.Pow(Vs - Vr_min, 2) + Math.Pow(Vi_min, 2));
                    double Vmin = newVoltageVector[rows, 10];
                    newVoltageVector[rows, 11] = (1 + ki) * ma - ki * 0.5 * (mb + mc);
                    double C1 = newVoltageVector[rows, 11];
                    newVoltageVector[rows, 12] = (Math.Pow(ki, 2)) * (Math.Abs(ma) + 0.25 * Math.Abs(mb) + 0.25 * Math.Abs(mc) + (2 * ki + 1) * Math.Abs(ma));
                    double C2 = newVoltageVector[rows, 12];

                    double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1) * (Math.Abs(mb) + Math.Abs(mc));
                    double F2 = Math.Abs(ma) * (2 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2); double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1);

                    newVoltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                    double C3 = newVoltageVector[rows, 13];
                    newVoltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                    double C4 = newVoltageVector[rows, 14];
                    newVoltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow(Math.Abs(mb) - Math.Abs(mc), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                    double C5 = newVoltageVector[rows, 15];
                    newVoltageVector[rows, 16] = 0.5 * (Math.Sqrt(3)) * ki * (mb - mc);
                    double C6 = newVoltageVector[rows, 16];
                    newVoltageVector[rows, 17] = C1 * Rp * c * Gi;
                    double E_DVre = newVoltageVector[rows, 17];
                    newVoltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                    double E_DVre2 = newVoltageVector[rows, 18];
                    newVoltageVector[rows, 19] = C6 * Rp * c * Gi;
                    double E_DVim = newVoltageVector[rows, 19];
                    newVoltageVector[rows, 20] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2));
                    double E_DVim2 = newVoltageVector[rows, 20];
                    newVoltageVector[rows, 21] = newVoltageVector[z, 17] + newVoltageVector[z, 21];
                    double Sum_DVre = newVoltageVector[rows, 21];
                    newVoltageVector[rows, 22] = newVoltageVector[z, 19] + newVoltageVector[z, 22];
                    double Sum_DVim = newVoltageVector[rows, 22];
                    newVoltageVector[rows, 23] = newVoltageVector[z, 23] + E_DVre;
                    double E_DVr = newVoltageVector[rows, 23];
                    newVoltageVector[rows, 24] = newVoltageVector[z, 24] + E_DVre2 + (2 * Sum_DVre * E_DVre);
                    double E_DVr2 = newVoltageVector[rows, 24];
                    newVoltageVector[rows, 25] = newVoltageVector[z, 25] + E_DVim;
                    double E_DVi = newVoltageVector[rows, 25];
                    newVoltageVector[rows, 26] = newVoltageVector[z, 26] + E_DVim2 + (2 * Sum_DVim * E_DVim);
                    double E_DVi2 = newVoltageVector[rows, 26];
                    newVoltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                    double E_Vc = newVoltageVector[rows, 27];
                    newVoltageVector[rows, 28] = Math.Pow(Vs, 2) - (2 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                    double E_Vc2 = newVoltageVector[rows, 28];
                    newVoltageVector[rows, 29] = (E_Vc - Vmin) / (Vmax - Vmin);
                    double E_vc = newVoltageVector[rows, 29];
                    newVoltageVector[rows, 30] = (E_Vc2 - (2 * Vmin * E_Vc) + (Math.Pow(Vmin, 2))) / Math.Pow((Vmax - Vmin), 2);
                    double E_vc2 = newVoltageVector[rows, 30];
                    newVoltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                    double alpha_ = newVoltageVector[rows, 31];
                    newVoltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                    double beta_ = newVoltageVector[rows, 32];

                    if (Vmax > Vmin)
                    {
                        if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                        {
                            double v_percent = newVoltageVector[rows, 33];
                            newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                            double Vc_percent = newVoltageVector[rows, 34];
                        }
                        else
                        {
                            newVoltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                            double v_percent = newVoltageVector[rows, 33];
                            newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmin;
                            double Vc_percent = newVoltageVector[rows, 34];
                        }
                    }

                    else
                    {
                        if (double.IsNaN(newVoltageVector[rows, 31]) || double.IsNaN(newVoltageVector[rows, 30]))
                        {
                            double v_percent = newVoltageVector[rows, 33];
                            newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                            double Vc_percent = newVoltageVector[rows, 34];
                        }
                        else
                        {
                            newVoltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(newVoltageVector[rows, 31]), Math.Abs(newVoltageVector[rows, 30]));
                            double v_percent = newVoltageVector[rows, 33];
                            newVoltageVector[rows, 34] = (Math.Abs(Vmax - Vmin) * v_percent) + Vmax;
                            double Vc_percent = newVoltageVector[rows, 34];
                        }
                    }
                    // Vr[vCount] = newVoltageVector[rows, 34];
                }

                // vCount++;
                count++;
                rows4debug = rows;
            }
            feederProfileVoltage[0, 0] = Vs;
            feederProfileVoltage[phaseCountRow, 0] = newVoltageVector[rows4debug, 34];
            phaseCountRow++;
            if (phaseCountRow == nodeNum + 1) phaseCountRow = 1;
            ///** For some reason, the phaseCountRow kept on increasing beyond the index of the voltageProfileArray;
            // * We had to manually just tell it to not go beyond the dimensions of the array **/
            //if (cd == 'r' && phaseCountRow < nodeNum + 1)
            //{
            //    //Debug.Print("Red:");
            //    voltageProfileArray[phaseCountRow, 0] = newVoltageVector[rows4debug, 34];
            //}
            //if ((cd == 'r' || cd == 'w') && phaseCountRow == nodeNum) {/*do nothing */}
            //if (cd == 'w' && phaseCountRow < nodeNum + 1)
            //{
            //    //Debug.Print("White:");
            //    voltageProfileArray[phaseCountRow, 1] = newVoltageVector[rows4debug, 34];
            //}
            //if (cd == 'b' && phaseCountRow < nodeNum + 1)
            //{
            //    //Debug.Print("Blue:");
            //    voltageProfileArray[phaseCountRow, 2] = newVoltageVector[rows4debug, 34];
            //    phaseCountRow++;
            //    //if (phaseCountRow < nodeNum + 1) phaseCountRow++;
            //    //else Debug.Print("VoltageArrayWeirdphaseCountRow = " + newVoltageVector[rows4debug, 34].ToString());
            //}
            //if (cd == 'b' && phaseCountRow == nodeNum)
            //{
            //    voltageProfileArray[0, 0] = voltageProfileArray[0, 1]= voltageProfileArray[0, 2]= Vs;
            //    voltageProfileArray[0, 3] = 0.0; phaseCountRow = 1;
            //}

            //else
            //{
            //    phaseCountRow = 1;                
            //}

            //if (cd == 'w') Debug.Print("White:");
            //if (cd == 'b') Debug.Print("Blue:");

            //voltageProfileArray[phaseCountRow, phaseCountCol] = newVoltageVector[rows4debug, 34];
            //if (phaseCountCol == 2) //done all phases move to next row and reset phaseCountCol
            //{
            //    phaseCountRow++; phaseCountCol = 0;
            //}
            //phaseCountCol++;

            // Debug.Print(newVoltageVector[rows4debug, 34].ToString());
        }

        private void voltCalculationButton_Click(object sender, EventArgs e)
        {
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[nodeSummaryDataGridView.DataSource];
            currencyManager1.SuspendBinding();
            foreach (DataGridViewRow dr in nodeSummaryDataGridView.Rows)
            {
                if (dr.Cells[0].Value.ToString() == "0") dr.Visible = false;
            }
            currencyManager1.ResumeBinding();

            double[,] nodeVector = createNodeVector(nodeVecDataSet.Tables[0]);
            
            List<double[,]> nodeVectors = new List<double[,]>();
            for(int i = 0; i<nodeNum; i++)
            {
                nodeVectors.Add(createNodeVector(nodeVecDataSet.Tables[i]));
            }
            
            int lCount = 1; // to keep track of the lengths in the for loop below
            for (int i = 0; i < nodeVector.GetLength(0); i = i + totalLoadsGens)
            {
                voltageProfileArray[lCount, 3] = nodeVector[i, 6]+nodeFeederForm.lengthTol + voltageProfileArray[lCount - 1, 3];

                lCount++;
            }
            //assigns the voltage limits uppder and lower for the feeder profile.
            for (int i = 0; i < voltageProfileArray.GetLength(0); i++)
            {
                if (i == 0) voltageProfileArray[i, 0] = voltageProfileArray[i, 1] = voltageProfileArray[i, 2] = Vs;
                voltageProfileArray[i, 4] = Vs * 0.9;
                voltageProfileArray[i, 5] = Vs * 1.1;

            }

            for (int i = 0; i < 3; i++) //this loops through each of the phases
            {
                if (i == 0)
                {
                    double[,] voltageVector = voltagedrop(Vs, p, 'b', nodeVector);
                    //voltageProfile(Vs, p, 'b');
                    for (int j = feederProfileVoltage.GetLength(0)-1; j > 0; j--)
                    {
                        double[,] voltageVectorFeeder = voltagedrop(Vs, p, 'b', nodeVectors[j-1]);
                        voltageProfileArray[feederProfileVoltage.GetLength(0) - j, 2] = voltageVectorFeeder[nodeVectors[j - 1].GetLength(0)-1, 34];
                    }
                    bluePhaseVoltageText.Text = Convert.ToString(Math.Round(voltageVector[totalLoadsGens * nodeNum - 1, 34], 4));

                    for (int rows = 0; rows < nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < 35; cols++)
                        {
                            nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows[rows][cols] = voltageVector[rows, cols];

                        }

                    }
                    //nodeVecDataSet.WriteXml(projName);
                    //initialize the voltage vector to zero
                    for (int l = 0; l < (totalLoadsGens * nodeNum); l++)
                    {
                        for (int m = 0; m < 35; m++)
                        {
                            voltageVector[l, m] = 0;
                        }
                    }

                }
                if (i == 1)
                {
                    double[,] voltageVector = voltagedrop(Vs, p, 'w', nodeVector);
                    //voltageProfile(Vs, p, 'w');
                    for (int j = feederProfileVoltage.GetLength(0) - 1; j > 0; j--)
                    {
                        double[,] voltageVectorFeeder = voltagedrop(Vs, p, 'w', nodeVectors[j-1]);
                        voltageProfileArray[feederProfileVoltage.GetLength(0) - j, 1] = voltageVectorFeeder[nodeVectors[j - 1].GetLength(0) - 1, 34];
                    }
                    whitePhaseVoltageText.Text = Convert.ToString(Math.Round(voltageVector[totalLoadsGens * nodeNum - 1, 34], 4));

                    for (int rows = 0; rows < nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < 35; cols++)
                        {
                            nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows[rows][cols] = voltageVector[rows, cols];

                        }

                    }
                    //nodeVecDataSet.WriteXml(projName);
                    for (int l = 0; l < (totalLoadsGens * nodeNum); l++)
                    {
                        for (int m = 0; m < 35; m++)
                        {
                            voltageVector[l, m] = 0;
                        }
                    }


                }
                if (i == 2)
                {
                    double[,] voltageVector = voltagedrop(Vs, p, 'r', nodeVector);
                    //voltageProfile(Vs, p, 'r');
                    for (int j = feederProfileVoltage.GetLength(0) - 1; j > 0; j--)
                    {
                        double[,] voltageVectorFeeder = voltagedrop(Vs, p, 'r', nodeVectors[j-1]);
                        voltageProfileArray[feederProfileVoltage.GetLength(0) - j, 0] = voltageVectorFeeder[nodeVectors[j - 1].GetLength(0) - 1, 34];
                    }
                    redPhaseVoltageText.Text = Convert.ToString(Math.Round(voltageVector[totalLoadsGens * nodeNum - 1, 34], 4));

                    for (int rows = 0; rows < nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < 35; cols++)
                        {
                            nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows[rows][cols] = voltageVector[rows, cols];

                        }

                    }
                    //nodeVecDataSet.WriteXml(projName);
                    for (int l = 0; l < (totalLoadsGens * nodeNum); l++)
                    {
                        for (int m = 0; m < 35; m++)
                        {
                            voltageVector[l, m] = 0;
                        }
                    }

                }
            }

            //recreates the node vector and then calculates the voltage profile for each of the phases.

            DataTable feederProfileDataTable = new DataTable();

            feederProfileDataTable.Columns.Add("RedVoltage", typeof(Double));
            feederProfileDataTable.Columns.Add("WhiteVoltage", typeof(Double));
            feederProfileDataTable.Columns.Add("BlueVoltage", typeof(Double));
            feederProfileDataTable.Columns.Add("Distance", typeof(Double));
            feederProfileDataTable.Columns.Add("LowerLimit", typeof(Double));
            feederProfileDataTable.Columns.Add("UpperLimit", typeof(Double));




            for (int j = 0; j < voltageProfileArray.GetLength(0); j++)
            {
                feederProfileDataTable.Rows.Add(0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

            }


            for (int i = 0; i < 6; i++)
            {

                for (int rows = 0; rows < voltageProfileArray.GetLength(0); rows++)
                {
                    DataRow dr = feederProfileDataTable.Rows[rows];
                    dr[i] = voltageProfileArray[rows, i];
                }

            }

            voltageProfileChart.Series["Series1"].YValueMembers = "blueVoltage";
            voltageProfileChart.Series["Series1"].LegendText = "V blue";
            voltageProfileChart.Series["Series1"].XValueMember = "Distance";
            voltageProfileChart.Series["Series2"].YValueMembers = "WhiteVoltage";
            voltageProfileChart.Series["Series2"].LegendText = "V white";
            voltageProfileChart.Series["Series2"].XValueMember = "Distance";
            voltageProfileChart.Series["Series3"].YValueMembers = "redVoltage";
            voltageProfileChart.Series["Series3"].LegendText = "V red";
            voltageProfileChart.Series["Series3"].XValueMember = "Distance";
            voltageProfileChart.Series["Series4"].YValueMembers = "LowerLimit";
            voltageProfileChart.Series["Series4"].LegendText = "Lower Limit";
            voltageProfileChart.Series["Series4"].XValueMember = "Distance";
            voltageProfileChart.Series["Series5"].YValueMembers = "UpperLimit";
            voltageProfileChart.Series["Series5"].LegendText = "Upper Limit";
            voltageProfileChart.Series["Series5"].XValueMember = "Distance";

            voltageProfileChart.DataSource = feederProfileDataTable;
            voltageProfileChart.ChartAreas[0].AxisY.Minimum = 180;
            voltageProfileChart.ChartAreas[0].AxisX.Minimum = 0;
            voltageProfileChart.ChartAreas[0].AxisX.Maximum = voltageProfileArray[voltageProfileArray.GetLength(0) - 1, 3] + 2.0;

            voltageProfileChart.DataBind();
        }

        /** Calculates the voltages for the Voltage profile.
         * This method creates a node vector (node2) which sums up the loads and gens in each of the phases (like in the excel sheet)
         * and then decompresses node2 while calculating the voltage drop at each of the nodes. **/
        //public void voltageProfile(double Vs, double p, char ph)
        //{
        //    double RL = 0; double BL = 0; double WL = 0; double RDG = 0; double WDG = 0; double BDG = 0;
        //    double load_sumR = 0; double load_sumB = 0; double load_sumW = 0; double DG_sumR = 0; double DG_sumB = 0; double DG_sumW = 0;
        //    double[,] node2 = new double[nodeNum * 2, 10];
        //    double[,] partL = new double[10, 10]; double[,] partDG = new double[10, 10];
        //    int[] redL = new int[nodeNum]; int[] whiteL = new int[nodeNum]; int[] blueL = new int[nodeNum];
        //    int[] redDG = new int[nodeNum]; int[] whiteDG = new int[nodeNum]; int[] blueDG = new int[nodeNum];

        //    /*** Create the new node vector called node2 (sum up loads and gens) like on the excel spreasheet***/
        //    int node2count = 0;
        //    /** This code only gets the first load/gen at each node and those are the values of columns
        //     * 3 to 9 that are used for the newly created node2****/
        //    for (int x = 0; x < nodeNum * 10; x++)
        //    {
        //        if (x % 10 == 0) // if x is divisible by 10; if it is the first line of the load
        //        {
        //            if (node2count != 0) node2count++;
        //            node2[node2count, 3] = nodeVector[x, 3];
        //            node2[node2count, 4] = nodeVector[x, 4];
        //            node2[node2count, 5] = nodeVector[x, 5];
        //            node2[node2count, 6] = nodeVector[x, 6];
        //            node2[node2count, 7] = nodeVector[x, 7];
        //            node2[node2count, 8] = nodeVector[x, 8];
        //            node2[node2count, 9] = nodeVector[x, 9];
        //        }
        //        if ((x % 10 >= 0) && (x % 10 <= 4)) // if the last digit of x is btn 0 and 4; if it is a load
        //        {
        //            //int y = x % 10; // So that if in the first 10, save to 0; in the teens, save to row 1
        //            node2[node2count, 0] = nodeVector[x, 0] + node2[node2count, 0];
        //            node2[node2count, 1] = nodeVector[x, 1] + node2[node2count, 1];
        //            node2[node2count, 2] = nodeVector[x, 2] + node2[node2count, 2];
        //        }
        //        if ((x % 10 != 0) && (x % 5 == 0)) // if x is divisible by 5; if it is the first line of the gens
        //        {
        //            node2count++;
        //            node2[node2count, 3] = nodeVector[x, 3];
        //            node2[node2count, 4] = nodeVector[x, 4];
        //            node2[node2count, 5] = nodeVector[x, 5];
        //            node2[node2count, 6] = nodeVector[x, 6];
        //            node2[node2count, 7] = nodeVector[x, 7];
        //            node2[node2count, 8] = nodeVector[x, 8];
        //            node2[node2count, 9] = nodeVector[x, 9];
        //        }
        //        if ((x % 10 >= 5) && (x % 10 <= 9)) // if the last digit of x is btn 5 and 9; if it is a gen
        //        {
        //            node2[node2count, 0] = nodeVector[x, 0] + node2[node2count, 0];
        //            node2[node2count, 1] = nodeVector[x, 1] + node2[node2count, 1];
        //            node2[node2count, 2] = nodeVector[x, 2] + node2[node2count, 2];
        //        }
        //    }

        //    /*** With the new node vector created, do the Voltage profile calculation ***/

        //    // Get the sum of the loads in each of the phases.
        //    for (int x = 0; x < node2.GetLength(0); x = x + 2)
        //    {
        //        load_sumR = node2[x, 0] + load_sumR; load_sumW = node2[x, 1] + load_sumW; load_sumB = node2[x, 2] + load_sumB;
        //    }

        //    // Get the sum of the DGs in each of the phases.
        //    for (int x = 1; x < node2.GetLength(0); x = x + 2)
        //    {
        //        DG_sumR = DG_sumR + node2[x, 0]; DG_sumW = DG_sumW + node2[x, 1]; DG_sumB = DG_sumB + node2[x, 2];
        //    }

        //    // Create the newer node vector.
        //    for (int x = 0; x < nodeNum; x++)
        //    {
        //        newerNode = new double[(x + 1) * 2, 10];
        //        // Fill out columns 3 to 9;
        //        for (int a = 3; a < 10; a++)
        //        {
        //            for (int b = 0; b < newerNode.GetLength(0); b++)
        //            { newerNode[b, a] = node2[b, a]; }
        //        }

        //        for (int i = 1; i < nodeNum * 2; i = i + 2)
        //        {
        //            for (int j = 0; j < newerNode.GetLength(0); j++)
        //            {
        //                if ((i % 2 != 0) && j == newerNode.GetLength(0) - 1 && i >= newerNode.GetLength(0) - 1 && node2[i, 3] != 999)
        //                {
        //                    newerNode[j, 3] = node2[i, 3];
        //                    newerNode[j, 4] = node2[i, 4];
        //                    newerNode[j, 5] = node2[i, 5];
        //                    newerNode[j, 6] = node2[i, 6];
        //                    newerNode[j, 7] = node2[i, 7];
        //                    newerNode[j, 8] = node2[i, 8];
        //                    newerNode[j, 9] = node2[i, 9];
        //                }
        //                if ((i % 2 != 0) && j == i && node2[i, 3] != 999)
        //                {
        //                    newerNode[j, 3] = node2[i, 3];
        //                    newerNode[j, 4] = node2[i, 4];
        //                    newerNode[j, 5] = node2[i, 5];
        //                    newerNode[j, 6] = node2[i, 6];
        //                    newerNode[j, 7] = node2[i, 7];
        //                    newerNode[j, 8] = node2[i, 8];
        //                    newerNode[j, 9] = node2[i, 9];
        //                }
        //            }
        //        }
        //        if (x == 0)
        //        {
        //            newerNode[x, 0] = load_sumR; newerNode[x + 1, 0] = DG_sumR;
        //            newerNode[x, 1] = load_sumW; newerNode[x + 1, 1] = DG_sumW;
        //            newerNode[x, 2] = load_sumB; newerNode[x + 1, 2] = DG_sumB;
        //        }


        //        if (x != 0)
        //        {
        //            // Fill out the top parts of the nodevector.
        //            for (int i = 0; i <= (x * 2) - 2; i = i + 2)
        //            {
        //                newerNode[i, 0] = node2[i, 0]; newerNode[i + 1, 0] = node2[i + 1, 0];
        //                newerNode[i, 1] = node2[i, 1]; newerNode[i + 1, 1] = node2[i + 1, 1];
        //                newerNode[i, 2] = node2[i, 2]; newerNode[i + 1, 2] = node2[i + 1, 2];
        //            }

        //            // Fill out the remaining parts of the nodevector that are sums.
        //            RL = 0; BL = 0; WL = 0; RDG = 0; WDG = 0; BDG = 0;
        //            for (int n = 0; n < x * 2; n = n + 2)
        //            {
        //                RL = node2[n, 0] + RL; RDG = node2[n + 1, 0] + RDG;
        //                WL = node2[n, 1] + WL; WDG = node2[n + 1, 1] + WDG;
        //                BL = node2[n, 2] + BL; BDG = node2[n + 1, 2] + BDG;
        //            }
        //            newerNode[x * 2, 0] = load_sumR - RL; newerNode[x * 2 + 1, 0] = DG_sumR - RDG;
        //            newerNode[x * 2, 1] = load_sumW - WL; newerNode[x * 2 + 1, 1] = DG_sumW - WDG;
        //            newerNode[x * 2, 2] = load_sumB - BL; newerNode[x * 2 + 1, 2] = DG_sumB - BDG;
        //        }
        //        nodeVoltagedrop(newerNode, Vs, p, ph);
        //    }
        //}

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void voltageProfileChart_Click(object sender, EventArgs e)
        {

        }




        private void numericUpDownVoltage_ValueChanged(object sender, EventArgs e)
        {
            Vs = Convert.ToDouble(numericUpDownVoltage.Value);
            voltCalculationButton_Click(sender, e);
        }

        private void numericUpDownRisk_ValueChanged(object sender, EventArgs e)
        {
            p = Convert.ToDouble(numericUpDownRisk.Value);
           voltCalculationButton_Click(sender, e);
        }

        private void nodeSummaryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void voltageCalculationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(projName);

            for (int i = 0; i < 4; i++)
            {
                ds.Tables.Remove(ds.Tables[ds.Tables.Count - 1]);
            }

            ds.WriteXml(projName);
        }

        private double[,] createNodeVector(DataTable nodeVecDataTable)
        {
            int rowRelativePosition = 0;
            double[,] nodeVector = new double[nodeVecDataTable.Rows.Count, 10];
            for (int i = 0; i < nodeVecDataTable.Rows.Count; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    nodeVector[i, j] = 0;
                    if ((j == 3) || (j == 4))
                    {
                        nodeVector[i, j] = 999;
                    }
                }
            }            
            for (int rows = 0; rows < nodeVecDataTable.Rows.Count; rows++)
            {
                if (rows % totalLoadsGens == 0) rowRelativePosition = 0;
                for (int cols = 0; cols < 10; cols++)
                {
                    nodeVector[rows, cols] = Convert.ToDouble(nodeVecDataTable.Rows[rows][cols + 1]);
                    if (rowRelativePosition >= loadCountInt) //ifgenerator
                    { if ((cols == 0) || (cols == 1) || (cols == 2)) nodeVector[rows, cols] = nodeVector[rows, cols] * -1.0; }

                }
                rowRelativePosition++;
            }
            //nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 4]
            return nodeVector;
        }


    }
}
