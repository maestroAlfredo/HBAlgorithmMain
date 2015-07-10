using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MathNet.Numerics;
using System.Linq;



namespace VoltageDropCalculatorApplication
{
    public partial class voltageCalculationForm : Form
    {

        int nodeNum;
        double Vs;
        double p;
        double t_old;
        string projName;
        double[,] voltageProfileArray;
        int[] customerArray;
        double loadCountInt;
        double genCountInt;
        int totalLoadsGens;
        DataTable nodeOverallDataTable = new DataTable();
        ErrorProvider errorProvider1;


        DataSet nodeVecDataSet = new DataSet();

        public voltageCalculationForm(string projectName, double risk, double temperature, double sourceVoltage, int loadCount, int genCount, List<int> mfNodeList)
        {
            InitializeComponent();           
            nodeVecDataSet.ReadXml(projectName);
            nodeNum = nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 1].Rows.Count / (loadCount + genCount);
            projName = projectName;
            Vs = sourceVoltage;
            t_old = temperature;
            p = risk;
            numericUpDownRisk.Value = (decimal)p;
            numericUpDownVoltage.Value = (decimal)Vs;
            temperatureTextBox.Text = t_old.ToString();
            errorProvider1 = new ErrorProvider();
            voltageProfileArray = new double[nodeNum + 1, 6];
            //customerArray = new int[3];
            numericUpDownVoltage.Value = Convert.ToDecimal(Vs);
            numericUpDownRisk.Value = Convert.ToDecimal(p);
            loadCountInt = loadCount;
            genCountInt = genCount;
            totalLoadsGens = loadCount + genCount;
            nodeOverallDataTable = nodeVecDataSet.Tables[0].Copy();
            editTable();
            nodeSummaryDataGridView.DataSource = nodeOverallDataTable;
            nodeSummaryDataGridView.Columns[5].Visible = false;
            nodeSummaryDataGridView.Columns[6].Visible = false;
            nodeSummaryDataGridView.Columns[7].Visible = false;
            nodeSummaryDataGridView.Columns[8].Visible = false;
            nodeSummaryDataGridView.Columns[9].Visible = false;
            nodeSummaryDataGridView.Columns[10].Visible = false;
            nodeSummaryDataGridView.Columns[11].Visible = false;            
            closeTableEdits();
            voltCalculation();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //calculates the voltage drop given an input nodevector
        private double[,] voltagedrop(double Vs, double p, char phase, double[,] nodeVector) // voltagedrop(vs, p, phase, nodeVector)
        {
            double[,] voltageVector = new double[nodeVector.GetLength(0), 35];


            double ma = 0.0; double mb = 0.0; double mc = 0.0; double c = 0.0;
            int count = 0;
            for (int rows = 0; rows < nodeVector.GetLength(0); rows++)
            {
                count++;

                if (count > totalLoadsGens) count = 1;
                // the red phase is selected.
                if (phase == 'r') { ma = nodeVector[rows, 0]; mb = nodeVector[rows, 1]; mc = nodeVector[rows, 2]; c = nodeVector[rows, 5]; }
                // if the white phase is selected.
                if (phase == 'w') { ma = nodeVector[rows, 1]; mb = nodeVector[rows, 2]; mc = nodeVector[rows, 0]; c = nodeVector[rows, 5]; }
                // if the blue phase is selected.
                if (phase == 'b') { ma = nodeVector[rows, 2]; mb = nodeVector[rows, 0]; mc = nodeVector[rows, 1]; c = nodeVector[rows, 5]; }

                if (count <= loadCountInt)
                {
                    if (rows == 0)
                    {

                        voltageVector[rows, 0] = nodeVector[rows, 3] / (nodeVector[rows, 3] + nodeVector[rows, 4]);
                        double Gi = voltageVector[rows, 0];
                        voltageVector[rows, 1] = nodeVector[rows, 3] * (nodeVector[rows, 3] + 1.0) / ((nodeVector[rows, 3] + nodeVector[rows, 4]) * (nodeVector[rows, 3] + nodeVector[rows, 4] + 1.0));
                        double Hi = voltageVector[rows, 1];
                        voltageVector[rows, 2] = nodeVector[rows, 8];
                        double Rp = voltageVector[rows, 2];
                        voltageVector[rows, 3] = nodeVector[rows, 9];
                        double Rn = voltageVector[rows, 3];
                        voltageVector[rows, 4] = voltageVector[rows, 3] / voltageVector[rows, 2];
                        double ki = voltageVector[rows, 4];
                        voltageVector[rows, 5] = Convert.ToDouble(((1.0m + (decimal)ki) * (decimal)Rp * (decimal)c * ((decimal)ma * 1.0m)) - (0.5m * (decimal)ki * (decimal)Rp * (decimal)c * (((decimal)mb * 0.0m) + ((decimal)mc * 0.0m))));
                        double Vr_min = voltageVector[rows, 5];
                        voltageVector[rows, 6] = (Math.Sqrt(3.0) / 2.0) * ki * Rp * c * (mb * 0.0 - mc * 0.0);
                        double Vi_min = voltageVector[rows, 6];
                        voltageVector[rows, 7] = ((1.0 + ki) * Rp * c * (ma * 0.0)) - (0.5 * ki * Rp * c * (mb * 1.0 + mc * 1.0));
                        double Vr_max = voltageVector[rows, 7];
                        voltageVector[rows, 8] = (Math.Sqrt(3.0) / 2.0) * ki * Rp * c * (mb * 1.0 - mc * 1.0);
                        double Vi_max = voltageVector[rows, 8];
                        voltageVector[rows, 9] = Math.Sqrt(((Math.Pow((Vs - Vr_max), 2)) + (Math.Pow(Vi_max, 2))));

                        if((genCountInt==0)&&(Vr_min>Vs)) voltageVector[rows, 10] = -1.0*Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                        else
                        {
                            voltageVector[rows, 10] = Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                        }
                        voltageVector[rows, 11] = (1.0 + ki) * ma - ki * 0.5 * (mb + mc);
                        double C1 = voltageVector[rows, 11];
                        voltageVector[rows, 12] = Math.Pow(voltageVector[rows, 4], 2) * (4.0 * Math.Abs(ma) + Math.Abs(mb) + Math.Abs(mc)) / 4.0 + (1.0 + 2.0 * voltageVector[rows, 4]) * Math.Abs(ma);
                        double C2 = voltageVector[rows, 12];

                        double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1.0) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1.0) * (Math.Abs(mb) + Math.Abs(mc));
                        double F2 = Math.Abs(ma) * (2.0 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2.0);
                        double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1.0);

                        voltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                        double C3 = voltageVector[rows, 13];
                        voltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                        double C4 = voltageVector[rows, 14];
                        voltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow((Math.Abs(mb) - Math.Abs(mc)), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                        double C5 = voltageVector[rows, 15];
                        voltageVector[rows, 16] = 0.5 * (Math.Sqrt(3.0)) * ki * (mb - mc);
                        double C6 = voltageVector[rows, 16];
                        voltageVector[rows, 17] = C1 * Rp * c * Gi;
                        double E_DVre = voltageVector[rows, 17];
                        voltageVector[rows, 18] = Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C2 * Hi + C3 * (Math.Pow(Gi, 2)));
                        double E_DVre2 = voltageVector[rows, 18];
                        voltageVector[rows, 19] = C6 * Rp * c * Gi;
                        double E_DVim = voltageVector[rows, 19];
                        voltageVector[rows, 20] = (Math.Pow(Rp, 2) * Math.Pow(c, 2) * (C4 * Hi + C5 * Math.Pow(Gi, 2)));
                        double E_DVim2 = voltageVector[rows, 20];
                        voltageVector[rows, 21] = 0.0;
                        double Sum_DVre = voltageVector[rows, 21];
                        voltageVector[rows, 22] = 0.0;
                        double Sum_DVim = voltageVector[rows, 22];
                        voltageVector[rows, 23] = E_DVre;
                        double E_DVr = voltageVector[rows, 23];
                        voltageVector[rows, 24] = E_DVre2 + (2.0 * Sum_DVre * E_DVre);
                        double E_DVr2 = voltageVector[rows, 24];
                        voltageVector[rows, 25] = 0.0;
                        double E_DVi = voltageVector[rows, 25];
                        voltageVector[rows, 26] = E_DVim2 + (2.0 * Sum_DVim * E_DVim);
                        double E_DVi2 = voltageVector[rows, 26];
                        voltageVector[rows, 27] = Vs - E_DVr + (0.5 * E_DVi2 / Vs);
                        double E_Vc = voltageVector[rows, 27];
                        voltageVector[rows, 28] = Math.Pow(Vs, 2) - (2.0 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                        double E_Vc2 = voltageVector[rows, 28];
                        voltageVector[rows, 29] = (E_Vc - voltageVector[rows, 10]) / (voltageVector[rows, 9] - voltageVector[rows, 10]);
                        double E_vc = voltageVector[rows, 29];
                        voltageVector[rows, 30] = (E_Vc2 - (2.0 * voltageVector[rows, 10] * E_Vc) + (Math.Pow(voltageVector[rows, 10], 2))) / Math.Pow((voltageVector[rows, 9] - voltageVector[rows, 10]), 2);
                        double E_vc2 = voltageVector[rows, 30];
                        voltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                        double alpha_ = voltageVector[rows, 31];
                        voltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                        double beta_ = voltageVector[rows, 32];


                        if (voltageVector[rows, 9] > voltageVector[rows, 10])
                        {
                            if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                            {
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 10];
                                double Vc_percent = voltageVector[rows, 34];
                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 10];
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }

                        else
                        {
                            if (double.IsNaN(voltageVector[rows, 31]) || double.IsNaN(voltageVector[rows, 30]))
                            {
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 9];
                                double Vc_percent = voltageVector[rows, 34];
                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(voltageVector[rows, 32]), Math.Abs(voltageVector[rows, 31]));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 9];
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }
                    }
                    else
                    {

                        int z = rows - 1;  // z = i - 1;

                        voltageVector[rows, 0] = nodeVector[rows, 3] / (nodeVector[rows, 3] + nodeVector[rows, 4]);
                        double Gi = voltageVector[rows, 0];
                        voltageVector[rows, 1] = nodeVector[rows, 3] * (nodeVector[rows, 3] + 1.0) / ((nodeVector[rows, 3] + nodeVector[rows, 4]) * (nodeVector[rows, 3] + nodeVector[rows, 4] + 1.0));
                        double Hi = voltageVector[rows, 1];
                        voltageVector[rows, 2] = voltageVector[z, 2] + nodeVector[rows, 8];
                        double Rp = voltageVector[rows, 2];
                        voltageVector[rows, 3] = voltageVector[z, 3] + nodeVector[rows, 9];
                        double Rn = voltageVector[rows, 3];
                        voltageVector[rows, 4] = voltageVector[rows, 3] / voltageVector[rows, 2];
                        double ki = voltageVector[rows, 4];

                        voltageVector[rows, 5] = voltageVector[z, 5] + ((1.0 + ki) * Rp * c * (ma * 1.0)) - (0.5 * ki * Rp * c * (mb * 0.0 + mc * 0.0));
                        double Vr_min = voltageVector[rows, 5];
                        voltageVector[rows, 6] = voltageVector[z, 6] + (Math.Sqrt(3.0) / 2.0) * ki * Rp * c * (mb * 0.0 - mc * 0.0);
                        double Vi_min = voltageVector[rows, 6];
                        voltageVector[rows, 7] = voltageVector[z, 7] + ((1.0 + ki) * Rp * c * (ma * 0.0) - (0.5 * ki * Rp * c * (mb * 1.0 + mc * 1.0)));
                        double Vr_max = voltageVector[rows, 7];
                        voltageVector[rows, 8] = voltageVector[z, 8] + (Math.Sqrt(3.0) / 2.0) * ki * Rp * c * (mb * 1.0 - mc * 1.0);
                        double Vi_max = voltageVector[rows, 8];
                        voltageVector[rows, 9] = Math.Sqrt(Math.Pow(Vs - Vr_max, 2) + Math.Pow(Vi_max, 2));
                        if ( (Vr_min > Vs)) voltageVector[rows, 10] = -1.0 * Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                        else
                        {
                            voltageVector[rows, 10] = Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                        }
                        voltageVector[rows, 11] = (1.0 + ki) * ma - ki * 0.5 * (mb + mc);
                        double C1 = voltageVector[rows, 11];
                        voltageVector[rows, 12] = Math.Pow(voltageVector[rows, 4], 2) * (4.0 * Math.Abs(ma) + Math.Abs(mb) + Math.Abs(mc)) / 4.0 + (1.0 + 2.0 * voltageVector[rows, 4]) * Math.Abs(ma);
                        double C2 = voltageVector[rows, 12];

                        double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1.0) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1.0) * (Math.Abs(mb) + Math.Abs(mc));
                        double F2 = Math.Abs(ma) * (2.0 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2.0); double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1.0);

                        voltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                        double C3 = voltageVector[rows, 13];
                        voltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                        double C4 = voltageVector[rows, 14];
                        voltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow(Math.Abs(mb) - Math.Abs(mc), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                        double C5 = voltageVector[rows, 15];
                        voltageVector[rows, 16] = 0.5 * (Math.Sqrt(3.0)) * ki * ((mb) - (mc));
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
                        voltageVector[rows, 28] = Math.Pow(Vs, 2) - (2.0 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                        double E_Vc2 = voltageVector[rows, 28];
                        voltageVector[rows, 29] = (E_Vc - voltageVector[rows, 10]) / (voltageVector[rows, 9] - voltageVector[rows, 10]);
                        double E_vc = voltageVector[rows, 29];
                        voltageVector[rows, 30] = (E_Vc2 - (2.0 * voltageVector[rows, 10] * E_Vc) + (Math.Pow(voltageVector[rows, 10], 2))) / Math.Pow((voltageVector[rows, 9] - voltageVector[rows, 10]), 2);
                        double E_vc2 = voltageVector[rows, 30];
                        voltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                        double alpha_ = voltageVector[rows, 31];
                        voltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                        double beta_ = voltageVector[rows, 32];

                        if (voltageVector[rows, 9] > voltageVector[rows, 10])
                        {
                            if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                            {
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 10];
                                double Vc_percent = voltageVector[rows, 34];

                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 10];
                                double Vc_percent = voltageVector[rows, 34];
                            }
                        }

                        else
                        {
                            if (double.IsNaN(voltageVector[rows, 31]) || double.IsNaN(voltageVector[rows, 30]))
                            {
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 9];
                                double Vc_percent = voltageVector[rows, 34];
                            }
                            else
                            {
                                voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(voltageVector[rows, 32]), Math.Abs(voltageVector[rows, 31]));
                                double v_percent = voltageVector[rows, 33];
                                voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 9];
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
                    voltageVector[rows, 1] = nodeVector[rows, 3] * (nodeVector[rows, 3] + 1.0) / ((nodeVector[rows, 3] + nodeVector[rows, 4]) * (nodeVector[rows, 3] + nodeVector[rows, 4] + 1.0));
                    double Hi = voltageVector[rows, 1];
                    voltageVector[rows, 2] = voltageVector[z, 2] + nodeVector[rows, 8];
                    double Rp = voltageVector[rows, 2];
                    voltageVector[rows, 3] = voltageVector[z, 3] + nodeVector[rows, 9];
                    double Rn = voltageVector[rows, 3];
                    voltageVector[rows, 4] = voltageVector[rows, 3] / voltageVector[rows, 2];
                    double ki = voltageVector[rows, 4];



                    voltageVector[rows, 5] = voltageVector[z, 5] + ((1.0 + ki) * Rp * c * (ma * 0.0)) - (0.5 * ki * Rp * c * (mb * 1.0 + mc * 1.0));
                    double Vr_min = voltageVector[rows, 5];
                    voltageVector[rows, 6] = voltageVector[z, 6] + (Math.Sqrt(3.0) / 2.0) * ki * Rp * c * (mb * 1.0 - mc * 1.0);
                    double Vi_min = voltageVector[rows, 6];
                    voltageVector[rows, 7] = voltageVector[z, 7] + ((1.0 + ki) * Rp * c * (ma * 1.0) - (0.5 * ki * Rp * c * (mb * 0.0 + mc * 0.0)));
                    double Vr_max = voltageVector[rows, 7];
                    voltageVector[rows, 8] = voltageVector[z, 8] + (Math.Sqrt(3.0) / 2.0) * ki * Rp * c * (mb * 0.0 - mc * 0.0);
                    double Vi_max = voltageVector[rows, 8];
                    voltageVector[rows, 9] = Math.Sqrt(Math.Pow(Vs - Vr_max, 2) + Math.Pow(Vi_max, 2));
                    if ( (Vr_min > Vs)) voltageVector[rows, 10] = -1.0 * Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                    else
                    {
                        voltageVector[rows, 10] = Math.Sqrt(((Math.Pow((Vs - Vr_min), 2)) + (Math.Pow(Vi_min, 2))));
                    }
                    voltageVector[rows, 11] = (1.0 + ki) * ma - ki * 0.5 * (mb + mc);
                    double C1 = voltageVector[rows, 11];
                    voltageVector[rows, 12] = Math.Pow(voltageVector[rows, 4],2) * (4.0 * Math.Abs(ma) + Math.Abs(mb) + Math.Abs(mc)) / 4.0 + (1.0 + 2.0 * voltageVector[rows, 4]) * Math.Abs(ma);
                    double C2 = voltageVector[rows, 12];

                    double F1 = Math.Abs(ma) * (Math.Abs(ma) - 1.0) - Math.Abs(ma) * (Math.Abs(mb) + Math.Abs(mc)) + 0.25 * (Math.Abs(mb) + Math.Abs(mc) - 1.0) * (Math.Abs(mb) + Math.Abs(mc));
                    double F2 = Math.Abs(ma) * (2.0 * Math.Abs(ma) - Math.Abs(mb) - Math.Abs(mc) - 2.0); double F3 = Math.Abs(ma) * (Math.Abs(ma) - 1.0);

                    voltageVector[rows, 13] = F1 * Math.Pow(ki, 2) + F2 * ki + F3;
                    double C3 = voltageVector[rows, 13];
                    voltageVector[rows, 14] = 0.75 * Math.Pow(ki, 2) * (Math.Abs(mb) + Math.Abs(mc));
                    double C4 = voltageVector[rows, 14];
                    voltageVector[rows, 15] = 0.75 * Math.Pow(ki, 2) * (Math.Pow(Math.Abs(mb) - Math.Abs(mc), 2) - (Math.Abs(mb) + Math.Abs(mc)));
                    double C5 = voltageVector[rows, 15];
                    voltageVector[rows, 16] = 0.5 * (Math.Sqrt(3.0)) * ki * (mb - mc);
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
                    voltageVector[rows, 28] = Math.Pow(Vs, 2) - (2.0 * Vs * E_DVr) + E_DVr2 + E_DVi2;
                    double E_Vc2 = voltageVector[rows, 28];
                    voltageVector[rows, 29] = (E_Vc - voltageVector[rows, 10]) / (voltageVector[rows, 9] - voltageVector[rows, 10]);
                    double E_vc = voltageVector[rows, 29];
                    voltageVector[rows, 30] = (E_Vc2 - (2.0 * voltageVector[rows, 10] * E_Vc) + (Math.Pow(voltageVector[rows, 10], 2))) / Math.Pow((voltageVector[rows, 9] - voltageVector[rows, 10]), 2);
                    double E_vc2 = voltageVector[rows, 30];
                    voltageVector[rows, 31] = (E_vc2 - E_vc) / (E_vc - E_vc2 / E_vc);
                    double alpha_ = voltageVector[rows, 31];
                    voltageVector[rows, 32] = alpha_ / E_vc - alpha_;
                    double beta_ = voltageVector[rows, 32];

                    if (voltageVector[rows, 9] > voltageVector[rows, 10])
                    {
                        if (double.IsNaN(alpha_) || double.IsNaN(beta_))
                        {
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 10];
                            double Vc_percent = voltageVector[rows, 34];
                        }
                        else
                        {
                            voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(alpha_), Math.Abs(beta_));
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 10];
                            double Vc_percent = voltageVector[rows, 34];
                        }
                    }

                    else
                    {
                        if (double.IsNaN(voltageVector[rows, 31]) || double.IsNaN(voltageVector[rows, 30]))
                        {
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 9];
                            double Vc_percent = voltageVector[rows, 34];
                        }
                        else
                        {
                            voltageVector[rows, 33] = ExcelFunctions.BetaInv(p / 100, Math.Abs(voltageVector[rows, 32]), Math.Abs(voltageVector[rows, 31]));
                            double v_percent = voltageVector[rows, 33];
                            voltageVector[rows, 34] = (Math.Abs(voltageVector[rows, 9] - voltageVector[rows, 10]) * v_percent) + voltageVector[rows, 9];
                            double Vc_percent = voltageVector[rows, 34];
                        }
                    }
                }
            }

            return voltageVector;
        }

        //calculates the voltage drop and plots the voltageprofile
        private void voltCalculation()
        {
            double[,] nodeVector = createNodeVector(nodeVecDataSet.Tables[0]);

            List<double[,]> nodeVectors = new List<double[,]>();
            for (int i = 0; i < nodeNum; i++)
            {
                nodeVectors.Add(createNodeVector(nodeVecDataSet.Tables[i]));
            }

            int lCount = 1; // to keep track of the lengths in the for loop below
            for (int i = 0; i < nodeVector.GetLength(0); i = i + totalLoadsGens)
            {
                voltageProfileArray[lCount, 3] = Math.Round(nodeVector[i, 6] + nodeFeederForm.lengthTol + voltageProfileArray[lCount - 1, 3]);
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
                    for (int j = nodeNum + 1 - 1; j > 0; j--)
                    {
                        double[,] voltageVectorFeeder = voltagedrop(Vs, p, 'b', nodeVectors[j - 1]);
                        voltageProfileArray[nodeNum + 1 - j, 2] = voltageVectorFeeder[nodeVectors[j - 1].GetLength(0) - 1, 34];
                    }
                    bluePhaseVoltageText.Text = Convert.ToString(Math.Round(voltageVector[totalLoadsGens * nodeNum - 1, 34], 4));

                    for (int rows = 0; rows < nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < 35; cols++)
                        {
                            nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows[rows][cols] = voltageVector[rows, cols];

                        }

                    }

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

                    for (int j = nodeNum + 1 - 1; j > 0; j--)
                    {
                        double[,] voltageVectorFeeder = voltagedrop(Vs, p, 'w', nodeVectors[j - 1]);
                        voltageProfileArray[nodeNum + 1 - j, 1] = voltageVectorFeeder[nodeVectors[j - 1].GetLength(0) - 1, 34];
                    }
                    whitePhaseVoltageText.Text = Convert.ToString(Math.Round(voltageVector[totalLoadsGens * nodeNum - 1, 34], 4));

                    for (int rows = 0; rows < nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < 35; cols++)
                        {
                            nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows[rows][cols] = voltageVector[rows, cols];

                        }

                    }

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

                    for (int j = nodeNum + 1 - 1; j > 0; j--)
                    {
                        double[,] voltageVectorFeeder = voltagedrop(Vs, p, 'r', nodeVectors[j - 1]);
                        voltageProfileArray[nodeNum + 1 - j, 0] = voltageVectorFeeder[nodeVectors[j - 1].GetLength(0) - 1, 34];
                    }
                    redPhaseVoltageText.Text = Convert.ToString(Math.Round(voltageVector[totalLoadsGens * nodeNum - 1, 34], 4));

                    for (int rows = 0; rows < nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < 35; cols++)
                        {
                            nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - i - 1].Rows[rows][cols] = voltageVector[rows, cols];

                        }

                    }

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
            DataTable customerChartTable = new DataTable();

            feederProfileDataTable.Columns.Add("RedVoltage", typeof(Double));
            feederProfileDataTable.Columns.Add("WhiteVoltage", typeof(Double));
            feederProfileDataTable.Columns.Add("BlueVoltage", typeof(Double));
            feederProfileDataTable.Columns.Add("Distance", typeof(Double));
            feederProfileDataTable.Columns.Add("LowerLimit", typeof(Double));
            feederProfileDataTable.Columns.Add("UpperLimit", typeof(Double));

            customerChartTable.Columns.Add("Node", typeof(string));
            customerChartTable.Columns.Add("RedCustomer", typeof(Int16));
            customerChartTable.Columns.Add("WhiteCustomer", typeof(Int16));
            customerChartTable.Columns.Add("BlueCustomer", typeof(Int16));
            customerChartTable.Columns.Add("RedGenerator", typeof(Int16));
            customerChartTable.Columns.Add("WhiteGenerator", typeof(Int16));
            customerChartTable.Columns.Add("BlueGenerator", typeof(Int16));


            customerArray = sumCustomers(nodeVecDataSet.Tables[0]);
            customerChartTable.Rows.Add(customerArray[0], customerArray[1], customerArray[2]);

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

            //customerChart.Series["Series1"].YValueMembers = "RedCustomer";
            //customerChart.Series["Series2"].YValueMembers = "WhiteCustomer";
            //customerChart.Series["Series3"].YValueMembers = "BlueCustomer";

            //customerChart.DataSource = customerChartTable;
            //customerChart.DataBind();

            voltageProfileChart.DataSource = feederProfileDataTable;
            voltageProfileChart.ChartAreas[0].AxisY.Minimum = 180;
            voltageProfileChart.ChartAreas[0].AxisX.Minimum = 0;
            voltageProfileChart.ChartAreas[0].AxisX.Maximum = voltageProfileArray[voltageProfileArray.GetLength(0) - 1, 3] + 2.0;

            voltageProfileChart.DataBind();
        }

        private void numericUpDownVoltage_ValueChanged(object sender, EventArgs e)
        {
            Vs = Convert.ToDouble(numericUpDownVoltage.Value);
            if (totalLoadsGens != 0) //if the form has been initialized(eliminates an exception where the totalLoadsGens int hasn't been initialized to a nonzero value
            {
                voltCalculation();
            }

        }

        //recalculates the voltage drop as well as the voltage profile is a user changes the risk value
        private void numericUpDownRisk_ValueChanged(object sender, EventArgs e)
        {
            p = Convert.ToDouble(numericUpDownRisk.Value);
            if (totalLoadsGens != 0) //if the form has been initialized(eliminates an exception where the totalLoadsGens int hasn't been initialized to a nonzero value
            {
                voltCalculation();
            }

        }


        //deletes all tables from the projectxml file if the user decides to close the form
        private void voltageCalculationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(projName);


            for (int i = ds.Tables.Count - 1; i >= 0; i--)
            {
                ds.Tables.Remove(ds.Tables[i]);
            }

            ds.WriteXml(projName);
        }

        //creates an array of a nodevector given a nodevector datatable
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
                    if(cols!=7)
                    {
                        nodeVector[rows, cols] = Convert.ToDouble(nodeVecDataTable.Rows[rows][cols + 2]);
                        if (rowRelativePosition >= loadCountInt) //ifgenerator
                        { if ((cols == 0) || (cols == 1) || (cols == 2)) nodeVector[rows, cols] = nodeVector[rows, cols] * -1.0; }
                    }                    

                }
                rowRelativePosition++;
            }
            //nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 4]
            return nodeVector;
        }

        //method updates all the nodevector datatables and recalculates the voltage drop and the feeder profile voltages once the user has finished updating the customer cell
        private void nodeSummaryDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if the node datagrid view cell doesn't match the nodeVector table cell. i.e the user has changed a value in a particular cell
            if (nodeOverallDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString() != nodeVecDataSet.Tables[0].Rows[e.RowIndex][e.ColumnIndex].ToString())
            {
                double diff = Convert.ToDouble(nodeVecDataSet.Tables[0].Rows[e.RowIndex][e.ColumnIndex]) - Convert.ToDouble(nodeOverallDataTable.Rows[e.RowIndex][e.ColumnIndex]);
                nodeVecDataSet.Tables[0].Rows[e.RowIndex][e.ColumnIndex] = nodeOverallDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString();
                string node = nodeOverallDataTable.Rows[e.RowIndex][0].ToString();
                string load = nodeOverallDataTable.Rows[e.RowIndex][1].ToString();
                for (int i = nodeNum - 1; i >= 1; i--)
                {
                    bool containsNode = nodeVecDataSet.Tables[i].AsEnumerable().Any(row => node == row.Field<String>("Node"));
                    //if the nodetable contains the specific node
                    if (containsNode)
                    {
                        for (int rows = nodeVecDataSet.Tables[i].Rows.Count - 1; rows >= 0; rows--)
                        {
                            if ((Convert.ToString(nodeVecDataSet.Tables[i].Rows[rows][1]) == load) && (Convert.ToString(nodeVecDataSet.Tables[i].Rows[rows][0]) == node))
                            {
                                nodeVecDataSet.Tables[i].Rows[rows][e.ColumnIndex] = Convert.ToDouble(nodeVecDataSet.Tables[i].Rows[rows][e.ColumnIndex]) - diff;
                                break;
                            }

                        }

                    }
                    //update the last node customer which has
                    else
                    {
                        for (int rows = nodeVecDataSet.Tables[i].Rows.Count - 1; rows >= 0; rows--)
                        {
                            if (Convert.ToString(nodeVecDataSet.Tables[i].Rows[rows][1]) == load)
                            {
                                nodeVecDataSet.Tables[i].Rows[rows][e.ColumnIndex] = Convert.ToDouble(nodeVecDataSet.Tables[i].Rows[rows][e.ColumnIndex]) - diff;
                                break;
                            }

                        }

                    }


                }
            }
            //redo the calculation
            voltCalculation();
        }

        //method to check if the pressed value is a digit
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //allows only digits to be entered in the customer section of the datagridview
        private void nodeSummaryDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if ((nodeSummaryDataGridView.CurrentCell.ColumnIndex > 1) || (nodeSummaryDataGridView.CurrentCell.ColumnIndex < 5))//Desired Columns
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        //allow the datagridview to be edited 
        private void editTable()
        {
            for (int i = 0; i < nodeSummaryDataGridView.Columns.Count; i++)
            {
                nodeSummaryDataGridView.Columns[i].ReadOnly = false;
            }
        }

        //prevent datagridview to be edited except the r,w,b customer sections corresponding to i = 2, i = 3 and i = 4
        private void closeTableEdits()
        {
            for (int i = 0; i < nodeSummaryDataGridView.Columns.Count; i++)
            {
                if ((i != 2) && (i != 3) && (i != 4))
                {
                    nodeSummaryDataGridView.Columns[i].ReadOnly = true;
                }
            }
        }

        private int[] sumCustomers(DataTable dt)
        {
            int[] summedCustomers = new int[3];
            int sum;

            for (int i = 0; i < 3; i++)
            {
                sum = 0;
                for (int rows = 0; rows < dt.Rows.Count; rows++)
                {
                    sum = sum + Convert.ToInt16(dt.Rows[rows][i + 2]);
                }
                summedCustomers[i] = sum;
            }

            return summedCustomers;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void temperatureTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (temperatureTextBox.Text == "")
            {
                e.Cancel = true;
                this.errorProvider1.SetError(temperatureTextBox, "temperature cannot be null");
            }            
        }



        private void temperatureTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void temperatureTextBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(temperatureTextBox, "");
            double t_new = Convert.ToDouble(temperatureTextBox.Text);
            DataSet ds = new DataSet();
            ds.ReadXml("Libraries.xml");

            
           

            for (int i = 0; i < nodeNum; i++)
            {
                for (int rows = 0; rows < nodeVecDataSet.Tables[i].Rows.Count; rows++)
                {
                    string cable = nodeVecDataSet.Tables[i].Rows[rows][9].ToString();
                    DataRow dr = ds.Tables["Conductors"].AsEnumerable().SingleOrDefault(r => r.Field<string>("Code") == cable);
                    double T = Convert.ToDouble(dr["T"]);

                    nodeVecDataSet.Tables[i].Rows[rows][10] = Convert.ToDouble(nodeVecDataSet.Tables[i].Rows[rows][10]) * (T + t_new) / (T + t_old);
                    nodeVecDataSet.Tables[i].Rows[rows][11] = Convert.ToDouble(nodeVecDataSet.Tables[i].Rows[rows][11]) * (T + t_new) / (T + t_old);
                }
            }

            t_old = t_new;
            voltCalculation();
        }
    }
}
