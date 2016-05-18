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
        //string projName;
        double[,] voltageProfileArray;
        int[] customerArray;
        double loadCountInt;
        double genCountInt;
        int totalLoadsGens;
        double lengthTol;
        DataTable nodeOverallDataTable = new DataTable();
        List<string> nodeNameString;
        
        ErrorProvider errorProvider1;


        DataSet nodeVecDataSet = new DataSet();
        DataSet nodeDataSet = new DataSet(); //DataSet tempNodeDataSet = new DataSet();
        List<DataTable> nodeVecDataSetBackup = new List<DataTable>();
        DataSet libraryDataSet = new DataSet();
        DataGridView nodeDataGridView = new DataGridView();
        DataTable cableDT = new DataTable();
        double t2;
        List<string> nodeString;
        //NumericUpDown lengthNumericUpDown;
        NumericUpDown lengthNumericUpDown;
        DataView dvOverall = new DataView();
        //NumericUpDown sourceVoltageNumUpDown; 
        //NumericUpDown operatingTempNumUpDown;
        /** Create copies of the risk, temperature, and sourceVoltage ***/
        double riskBackup; double temperatureBackup; double sourceVoltageBackup; private nodeFeederForm frm; decimal oldValue; decimal newValue;
   
        //double k1;
        //DataRow dr;

        public voltageCalculationForm(double risk, double temperature, double sourceVoltage, int loadCount, int genCount, double lengthTol, List<int> mfNodeList, DataSet nodeDataSet, DataSet tempNodeDataSet, DataGridView nodeDataGridView, DataSet libraryDataSet, NumericUpDown lengthNumericUpDown, nodeFeederForm frm)
        {
            InitializeComponent();
            
            //---------------------KEEP THIS ORDER OF VARIABLES------------------------------//
            nodeVecDataSet = tempNodeDataSet; this.nodeDataGridView = nodeDataGridView; 
            this.libraryDataSet = libraryDataSet;
            nodeNum = nodeVecDataSet.Tables[nodeVecDataSet.Tables.Count - 1].Rows.Count / (loadCount + genCount);
            this.lengthNumericUpDown = lengthNumericUpDown;
            t2 = temperature;
            Vs = sourceVoltage;
            t_old = temperature;
            p = risk;
            loadCountInt = loadCount;
            genCountInt = genCount;
            totalLoadsGens = loadCount + genCount;
            voltageProfileArray = new double[nodeNum + 1, 6];
            this.lengthTol = lengthTol;
            nodeNameString = new List<string>();

            this.nodeDataSet = nodeDataSet;
            nodeOverallDataTable = nodeVecDataSet.Tables[0].Copy();

            
                //----additional variables to be initialized can be placed here
            //put the main feeder table names into a list
            for (int i = 0; i < mfNodeList.Count; i++ )
            {
                nodeNameString.Add("node" + mfNodeList[i].ToString());
            }


            numericUpDownRisk.Value = (decimal)p;
            numericUpDownVoltage.Value = (decimal)Vs;
            tempNumUpDown.Value = Convert.ToDecimal(t_old);
            errorProvider1 = new ErrorProvider();

            this.frm = frm;
            sourceVoltageBackup = Vs;
            riskBackup = p;
            temperatureBackup = t2;
            //customerArray = new int[3];
            numericUpDownVoltage.Value = Convert.ToDecimal(Vs);
            numericUpDownRisk.Value = Convert.ToDecimal(p);

            /*** loop to create a backup of the nodeVecDataSet ****/
            for (int x = 0; x < nodeNum; x++)
            {
                nodeVecDataSetBackup.Add(nodeVecDataSet.Tables[x].Copy());
            }

            buttonDiscardNodeSum.Enabled = false;
            buttonUpdateNodeSumm.Enabled = false;

            editTable(nodeSummaryDataGridView);
            nodeSummaryDataGridView.DataSource = nodeOverallDataTable;
            nodeSummaryDataGridView.Columns[5].Visible = false;
            nodeSummaryDataGridView.Columns[6].Visible = false;
            nodeSummaryDataGridView.Columns[7].Visible = false;
            nodeSummaryDataGridView.Columns[8].Visible = false;
            nodeSummaryDataGridView.Columns[9].Visible = false;
            nodeSummaryDataGridView.Columns[10].Visible = false;
            nodeSummaryDataGridView.Columns[11].Visible = false;
            closeTableEdits();

            initDataGridViewLengths();
            nodeString = cableDT.AsEnumerable().Select(x => x[0].ToString()).ToList();
            comboBoxNodeSelect.DataSource = nodeString;           
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
            buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true; // Enable "update" and "discard" buttons
            //Vs = Convert.ToDouble(numericUpDownVoltage.Value);
            //if (Vs == sourceVoltageBackup) 
            //{
            //    buttonUpdateNodeSumm.Enabled = false; buttonDiscardNodeSum.Enabled = false; ; // Enable "update" and "discard" buttons
            //}
            Vs = Convert.ToDouble(numericUpDownVoltage.Value);
            if (totalLoadsGens != 0) //if the form has been initialized(eliminates an exception where the totalLoadsGens int hasn't been initialized to a nonzero value
            {
                voltCalculation();
            }

        }

        //recalculates the voltage drop as well as the voltage profile is a user changes the risk value
        private void numericUpDownRisk_ValueChanged(object sender, EventArgs e)
        {
            buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true; // Enable "update" and "discard" buttons
            //p = Convert.ToDouble(numericUpDownRisk.Value);
            //if (p == riskBackup)
            //{
            //    buttonUpdateNodeSumm.Enabled = false; buttonDiscardNodeSum.Enabled = false; // Enable "update" and "discard" buttons
            //}
            p = Convert.ToDouble(numericUpDownRisk.Value);
            if (totalLoadsGens != 0) //if the form has been initialized(eliminates an exception where the totalLoadsGens int hasn't been initialized to a nonzero value
            {
                voltCalculation();
            }

        }


        private void voltageCalculationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            nodeDataGridView.Update();
            nodeDataGridView.Refresh();
            if (buttonDiscardNodeSum.Enabled == true) 
            { 
                
                const string message = "Would you like to update the changes made to the node feeder profile?";
                const string caption = "Update Changes?";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Question);

                // If the Yes button was pressed ... 
                if (result == DialogResult.Yes)
                {
                    buttonUpdateNodeSumm_Click(sender, e);
                    lengthNumericUpDown.Value = Convert.ToDecimal(cableDT.Rows[cableDT.Rows.Count - 1][1]);
                    frm.setOperatingTempNumUpDown(t2);
                    frm.setSourceVoltageNumUpDown(Vs);
                }

                else if (result == DialogResult.No)
                {
                    buttonDiscardNodeSum_Click(sender, e);
                    lengthNumericUpDown.Value = Convert.ToDecimal(nodeVecDataSetBackup[0].Rows[(cableDT.Rows.Count - 1)*totalLoadsGens][8]) + Convert.ToDecimal(lengthTol*(totalLoadsGens-1));
                    frm.setOperatingTempNumUpDown(temperatureBackup);
                    frm.setSourceVoltageNumUpDown(sourceVoltageBackup);
                }

                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                lengthNumericUpDown.Value = Convert.ToDecimal(cableDT.Rows[cableDT.Rows.Count - 1][1]);
                frm.setOperatingTempNumUpDown(temperatureBackup);
                frm.setSourceVoltageNumUpDown(sourceVoltageBackup);
            }
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
            nodeOverallDataTable.AcceptChanges();
            int rowIndex = e.RowIndex + (comboBoxNodeSelect.SelectedIndex * totalLoadsGens);
            if (nodeOverallDataTable.Rows[rowIndex][e.ColumnIndex].ToString() != nodeVecDataSet.Tables[0].Rows[rowIndex][e.ColumnIndex].ToString())
            {
                double diff = Convert.ToDouble(nodeVecDataSet.Tables[0].Rows[rowIndex][e.ColumnIndex]) - Convert.ToDouble(nodeOverallDataTable.Rows[rowIndex][e.ColumnIndex]);
                nodeVecDataSet.Tables[0].Rows[rowIndex][e.ColumnIndex] = nodeOverallDataTable.Rows[rowIndex][e.ColumnIndex].ToString();
                string node = nodeOverallDataTable.Rows[rowIndex][0].ToString();
                string load = nodeOverallDataTable.Rows[rowIndex][1].ToString();
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
            //buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true;
            //redo the calculation
            voltCalculation();
        }

        //method to check if the pressed value is a digit
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)&&e.KeyChar!='.')
            {
                e.Handled = true;
            }
        }

        private void Column234_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //allows only digits to be entered in the customer section of the datagridview
        private void nodeSummaryDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column234_KeyPress);
            if ((nodeSummaryDataGridView.CurrentCell.ColumnIndex > 1) || (nodeSummaryDataGridView.CurrentCell.ColumnIndex < 5))//Desired Columns
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column234_KeyPress);
                }
            }
        }

        //allow the datagridview to be edited 
        private void editTable(DataGridView nodeSummaryDataGridView)
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

        private void tempNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true; // Enable "update" and "discard" buttons
            double t_new = Convert.ToDouble(tempNumUpDown.Value);

            for (int i = 0; i < nodeNum; i++)
            {
                for (int rows = 0; rows < nodeVecDataSet.Tables[i].Rows.Count; rows++)
                {
                    string cable = nodeVecDataSet.Tables[i].Rows[rows][9].ToString();
                    DataRow dr = libraryDataSet.Tables["Conductors"].AsEnumerable().SingleOrDefault(r => r.Field<string>("Code") == cable);
                    double T = Convert.ToDouble(dr["T"]);

                    nodeVecDataSet.Tables[i].Rows[rows][10] = Convert.ToDouble(nodeVecDataSet.Tables[i].Rows[rows][10]) * (T + t_new) / (T + t_old);
                    nodeVecDataSet.Tables[i].Rows[rows][11] = Convert.ToDouble(nodeVecDataSet.Tables[i].Rows[rows][11]) * (T + t_new) / (T + t_old);
                }
            }

            t_old = t_new;
            voltCalculation();
        }

        private void buttonUpdateNodeSumm_Click(object sender, EventArgs e)
        {
            double rT2L = 0; double k1L = 0;
            int loadsGensNum = nodeDataSet.Tables[0].Rows.Count; int xx = 0;
            for (int i = 0; i < nodeNum; i++)
            {
                for (int x = 0; x < loadsGensNum; x++)
                {
                    nodeDataSet.Tables[nodeNameString[i]].Rows[x][2 + 1] = nodeVecDataSet.Tables[0].Rows[xx][2];
                    nodeDataSet.Tables[nodeNameString[i]].Rows[x][3 + 1] = nodeVecDataSet.Tables[0].Rows[xx][3];
                    nodeDataSet.Tables[nodeNameString[i]].Rows[x][4 + 1] = nodeVecDataSet.Tables[0].Rows[xx][4]; xx++;
                }
            }
            // update the lengths in nodeVecDataSet
            xx = 0;
            for (int x = 0; x < cableDT.Rows.Count; x++)
            {
                nodeVecDataSet.Tables[0].Rows[x * loadsGensNum][8] = Convert.ToDecimal(cableDT.Rows[x][1].ToString()) - (decimal)(loadCountInt * lengthTol - lengthTol);
                if (loadCountInt != 1)
                {
                    for (int y = 0; y < loadCountInt; y++)
                    {
                        if (y % loadsGensNum != 0) nodeVecDataSet.Tables[0].Rows[(x * loadsGensNum) + y][8] = lengthTol;
                    }
                }
                
            }
            // update the nodeDataSet for the dgv's on the nodefeeder form
            for (int i = 0; i < nodeNum; i++)
            {
                for (int x = 0; x < loadsGensNum; x++)
                {
                    nodeDataSet.Tables[nodeNameString[i]].Rows[x][8 + 1] = nodeVecDataSet.Tables[0].Rows[xx][8];
                    nodeDataSet.Tables[nodeNameString[i]].Rows[x][10 + 1] = nodeVecDataSet.Tables[0].Rows[xx][10];
                    nodeDataSet.Tables[nodeNameString[i]].Rows[x][11 + 1] = nodeVecDataSet.Tables[0].Rows[xx][11]; 
                    xx++;
                }
            }
            //gets the correct cable parameters and calculates them for that particular table
            for (int y = 0; y < libraryDataSet.Tables["Conductors"].Rows.Count; y++)
            {
                if (Convert.ToString(libraryDataSet.Tables["Conductors"].Rows[y][0]) == Convert.ToString(cableDT.Rows[0]["Cable"]))
                {
                    rT2L = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][1]) * (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][2]) + t2) / (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][2]) + 20.0);
                    k1L = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][3]);
                }
            }
            for (int x = 0; x < nodeVecDataSet.Tables[0].Rows.Count; x++)
            {
                //nodeVecDataSet.Tables[0].Rows[x][10] = calculateRp(rT2L, 99.99);
                nodeVecDataSet.Tables[0].Rows[x][10] = calculateRp(rT2L, Convert.ToDouble(nodeVecDataSet.Tables[0].Rows[x][8].ToString()));
                nodeVecDataSet.Tables[0].Rows[x][11] = calculateRn(rT2L, Convert.ToDouble(nodeVecDataSet.Tables[0].Rows[x][8].ToString()), k1L);
            }
            ////update the other tables for the voltage profile in the nodeVecDataSet ( the lengths, and Rp and Rn)
            //for (int x = 0; x < nodeNum - 1; x++)
            //{
            //    for (int y = 0; y < nodeVecDataSet.Tables[x + 1].Rows.Count; y++)
            //    {
            //        nodeVecDataSet.Tables[x + 1].Rows[y][8] = nodeVecDataSet.Tables[x].Rows[y][8];
            //        nodeVecDataSet.Tables[x + 1].Rows[y][10] = nodeVecDataSet.Tables[x].Rows[y][10];
            //        nodeVecDataSet.Tables[x + 1].Rows[y][11] = nodeVecDataSet.Tables[x].Rows[y][11];
            //    }
            //}
            nodeVecDataSetBackup.Clear();
            /*** loop to create a backup of the nodeVecDataSet ****/
            for (int x = 0; x < nodeNum; x++)
            {
                nodeVecDataSetBackup.Add(nodeVecDataSet.Tables[x].Copy());
            }
            sourceVoltageBackup = Vs;
            riskBackup = p;
            temperatureBackup = t2 = Convert.ToDouble(tempNumUpDown.Value);

            buttonUpdateNodeSumm.Enabled = false;
            buttonDiscardNodeSum.Enabled = false;

            voltCalculation();
        }

        private void initDataGridViewLengths()
        {
            cableDT.Columns.Add("Node", typeof(String)); cableDT.Columns.Add("Length", typeof(decimal)); cableDT.Columns.Add("Cable", typeof(String));
            decimal cLength = 0; // cable length
            //dataGridViewLengths.Columns[1].DefaultCellStyle.Format = "N2";
            //dataGridViewLengths.Columns[1].DefaultCellStyle.Format = "0.00##";

            int loadsGensNum = nodeDataSet.Tables[0].Rows.Count;
            for (int x = 0; x < nodeOverallDataTable.Rows.Count; x++)
            {
                if (x % loadsGensNum == 0) 
                {
                    cLength = Convert.ToDecimal(nodeVecDataSet.Tables[0].Rows[x][8].ToString()) + (decimal)(loadCountInt - 1)* (decimal)lengthTol;
                    cableDT.Rows.Add(nodeVecDataSet.Tables[0].Rows[x][0].ToString(), cLength, nodeVecDataSet.Tables[0].Rows[x][9].ToString());
                }
                else
                {
                    // do nothing
                }
            }
            dataGridViewLengths.DataSource = cableDT;
            editTable(dataGridViewLengths);
            dataGridViewLengths.Columns[0].ReadOnly = true; dataGridViewLengths.Columns[2].ReadOnly = true;
            dataGridViewLengths.Columns[1].DefaultCellStyle.Format = "N2";
        }

        private void buttonUpdateCables_Click(object sender, EventArgs e)
        {
        }

        private void sidePanelPictureBox_Click(object sender, EventArgs e)
        {
            if(splitContainer1.Panel2Collapsed==false)
            {
                sidePanelPictureBox.BorderStyle = BorderStyle.None;
                splitContainer1.Panel2Collapsed = true;
                
            }
            else
            {
                sidePanelPictureBox.BorderStyle = BorderStyle.Fixed3D;
                splitContainer1.Panel2Collapsed = false;                
            }
        }

        private void voltageProfileChart_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewLengths_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double a = Convert.ToDouble(dataGridViewLengths.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            if (a > 100.00) dataGridViewLengths.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 100;
            // update the lengths in nodeVecDataSet
            double rT2L = 0; double k1L = 0; int loadsGensNum = nodeDataSet.Tables[0].Rows.Count; 
            for (int x = 0; x < cableDT.Rows.Count; x++)
            {
                nodeVecDataSet.Tables[0].Rows[x * loadsGensNum][8] = Convert.ToDecimal(cableDT.Rows[x][1].ToString()) - (decimal)(loadCountInt * lengthTol - lengthTol);
                if (loadCountInt != 1)
                {
                    for (int y = 0; y < loadCountInt; y++)
                    {
                        if (y % loadsGensNum != 0) nodeVecDataSet.Tables[0].Rows[(x * loadsGensNum) + y][8] = lengthTol;
                    }
                }
            }
            //gets the correct cable parameters and calculates them for that particular table
            for (int y = 0; y < libraryDataSet.Tables["Conductors"].Rows.Count; y++)
            {
                if (Convert.ToString(libraryDataSet.Tables["Conductors"].Rows[y][0]) == Convert.ToString(cableDT.Rows[0]["Cable"]))
                {
                    rT2L = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][1]) * (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][2]) + t2) / (Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][2]) + 20.0);
                    k1L = Convert.ToDouble(libraryDataSet.Tables["Conductors"].Rows[y][3]);
                }
            }
            for (int x = 0; x < nodeVecDataSet.Tables[0].Rows.Count; x++)
            {
                //nodeVecDataSet.Tables[0].Rows[x][10] = calculateRp(rT2L, 99.99);
                nodeVecDataSet.Tables[0].Rows[x][10] = calculateRp(rT2L, Convert.ToDouble(nodeVecDataSet.Tables[0].Rows[x][8].ToString()));
                nodeVecDataSet.Tables[0].Rows[x][11] = calculateRn(rT2L, Convert.ToDouble(nodeVecDataSet.Tables[0].Rows[x][8].ToString()), k1L);
            }
            //update the other tables for the voltage profile in the nodeVecDataSet ( the lengths, and Rp and Rn)
            for (int x = 0; x < nodeNum - 1; x++)
            {
                for (int y = 0; y < nodeVecDataSet.Tables[x + 1].Rows.Count; y++)
                {
                    nodeVecDataSet.Tables[x + 1].Rows[y][8] = nodeVecDataSet.Tables[x].Rows[y][8];
                    nodeVecDataSet.Tables[x + 1].Rows[y][10] = nodeVecDataSet.Tables[x].Rows[y][10];
                    nodeVecDataSet.Tables[x + 1].Rows[y][11] = nodeVecDataSet.Tables[x].Rows[y][11];
                }
            }

            //buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true;

            voltCalculation();
        }

        public string calculateRp(double rkm, double calculatedLength)
        {
            return ((rkm / 1000.0) * calculatedLength).ToString();
        }

        public string calculateRn(double rKm, double caclulatedLength, double k1)
        {
            return (rKm / 1000.0 * k1 * caclulatedLength).ToString();
        }

        public double calculateLengths(decimal lengthInput, int rowIndex)
        {
            double calculatedLength = lengthTol;
            if ((rowIndex == 0) && (loadCountInt != 0))
            {
                calculatedLength = Convert.ToDouble(lengthInput) - (lengthTol * (Convert.ToDouble(loadCountInt) - 1.0));
                return calculatedLength;
            }
            else if ((rowIndex == 0) && (genCountInt != 0))
            {
                calculatedLength = Convert.ToDouble(lengthInput) - (lengthTol * (Convert.ToDouble(genCountInt) - 1.0));
                return calculatedLength;
            }
            else
            {
                calculatedLength = lengthTol;
            }
            return calculatedLength;
        }

        private void buttonDiscardNodeSum_Click(object sender, EventArgs e)
        {
            int loadsGensNum = nodeDataSet.Tables[0].Rows.Count; int xx = 0;
            for (int i = 0; i < nodeNum; i++)
            {
                for (int x = 0; x < loadsGensNum; x++)
                {
                    nodeOverallDataTable.Rows[xx][2] = nodeDataSet.Tables[nodeNameString[i]].Rows[x][2 + 1];
                    nodeOverallDataTable.Rows[xx][3] = nodeDataSet.Tables[nodeNameString[i]].Rows[x][3 + 1];
                    nodeOverallDataTable.Rows[xx][4] = nodeDataSet.Tables[nodeNameString[i]].Rows[x][4 + 1]; xx++;
                }
            }
            // update the nodeDataSet for the dgv's on the nodefeeder form
            xx = 0;
            for (int i = 0; i < nodeNum; i++)
            {
                for (int x = 0; x < loadsGensNum; x++)
                {
                    nodeOverallDataTable.Rows[xx][8] = nodeDataSet.Tables[nodeNameString[i]].Rows[x][8 + 1]; xx++;
                }
            }
            // update the lengths in the cableDT
            xx = 0;
            for (int x = 0; x < cableDT.Rows.Count; x++) cableDT.Rows[x][1] = Convert.ToDecimal(nodeOverallDataTable.Rows[x * loadsGensNum][8]) + (decimal)(loadCountInt * lengthTol - lengthTol);
            
            //update the other tables for the voltage profile in the nodeVecDataSet ( the lengths, and Rp and Rn)
            for (int x = 0; x < nodeNum - 1; x++)
            {
                for (int y = 0; y < nodeVecDataSet.Tables[x + 1].Rows.Count; y++)
                {
                    nodeVecDataSet.Tables[x + 1].Rows[y][8]  = nodeVecDataSet.Tables[x].Rows[y][8];
                    nodeVecDataSet.Tables[x + 1].Rows[y][10] = nodeVecDataSet.Tables[x].Rows[y][10];
                    nodeVecDataSet.Tables[x + 1].Rows[y][11] = nodeVecDataSet.Tables[x].Rows[y][11];
                }
            }

            /*** loop to update the nodeVecDataSet with the old data ****/
            for (int x = 0; x < nodeNum; x++)
            {
                for (int i = 0; i < nodeVecDataSetBackup[x].Rows.Count; i++)
                {
                    for (int j = 0; j < nodeVecDataSetBackup[x].Columns.Count; j++ ) nodeVecDataSet.Tables[x].Rows[i][j] = nodeVecDataSetBackup[x].Rows[i][j];
                }
            }
            nodeSummaryDataGridView.Refresh();
            nodeSummaryDataGridView.DataSource = dvOverall;
            dataGridViewLengths.Refresh();
            dataGridViewLengths.Update();
            dataGridViewLengths.DataSource = cableDT;
            Vs = sourceVoltageBackup; p = riskBackup; t2 = temperatureBackup;
            numericUpDownRisk.Value = Convert.ToDecimal(p);
            numericUpDownVoltage.Value = Convert.ToDecimal(Vs);
            tempNumUpDown.Value = Convert.ToDecimal(t2);

            voltCalculation();
        }

        private void dataGridViewLengths_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if ((nodeSummaryDataGridView.CurrentCell.ColumnIndex > 1) || (nodeSummaryDataGridView.CurrentCell.ColumnIndex < 5))//Desired Columns
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null) tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }

        private void nodeSummaryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewLengths_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            const string message = "Please check the format of the number entered. Only numerical values are allowed";
            const string caption = "Format Error";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Exclamation);
        }

        private void dataGridViewLengths_Validated(object sender, EventArgs e)
        {

        }

        private void dataGridViewLengths_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dataGridViewLengths.Rows[e.RowIndex].ErrorText =
                    "Cells cannnot be empty";
                e.Cancel = true;
            }

            if ((dataGridViewLengths.Columns[e.ColumnIndex].Name == "Length")&&!e.Cancel)
            {
                oldValue = Convert.ToDecimal(dataGridViewLengths[e.ColumnIndex, e.RowIndex].Value);
                newValue = Convert.ToDecimal(e.FormattedValue);

                if (oldValue != newValue)
                {
                    buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true; // Enable "update" and "discard" buttons
                }
            }            
        }

        private void comboBoxNodeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = "[Node] = '" + comboBoxNodeSelect.Text + "'";
            DataView dv = new DataView(nodeOverallDataTable, filter, string.Empty, DataViewRowState.CurrentRows);
            dvOverall = dv;
            nodeSummaryDataGridView.DataSource = dv;
        }

        private void nodeSummaryDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true; // Enable "update" and "discard" buttons
        }

        private void dataGridViewLengths_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void nodeSummaryDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                dataGridViewLengths.Rows[e.RowIndex].ErrorText =
                    "Cells cannnot be empty";
                e.Cancel = true;
            }

            if ((e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4) && !e.Cancel)
            {
                oldValue = Convert.ToDecimal(nodeSummaryDataGridView[e.ColumnIndex, e.RowIndex].Value);
                newValue = Convert.ToDecimal(e.FormattedValue);

                if (oldValue != newValue)
                {
                    buttonUpdateNodeSumm.Enabled = true; buttonDiscardNodeSum.Enabled = true;
                }
            }  
        }

        private void buttonUpdateNodeSumm_EnabledChanged(object sender, EventArgs e)
        {
            buttonDiscardNodeSum.Enabled = buttonUpdateNodeSumm.Enabled;
        }

        private void buttonDiscardNodeSum_EnabledChanged(object sender, EventArgs e)
        {
            buttonUpdateNodeSumm.Enabled = buttonDiscardNodeSum.Enabled;
        }

        private void numericUpDownVoltage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}