using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Thellier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            demagChart.Series["Demag"].Points.Clear();
            ZiChart.Series["YX"].Points.Clear();
            ZiChart.Series["YmZ"].Points.Clear();
            ZiChart.Series["YmX"].Points.Clear();
            ZiChart.Series["ZX"].Points.Clear();

            ARMChart.Series[0].Points.Clear();
            ARMChart.Series[1].Points.Clear();

            ZiChart.ChartAreas["proj1"].Visible = false;
        }

        private (int,int) countDM(string[] lines)
        {

            int Lcount = 0;
            int Lbegin = 0;

            char[] mChar = {'M', 'm'};

            foreach (string line in lines)
            {

                if (mChar.Contains(line[0]))
                {                    
                    if (line.Substring(1, 3) == "000" && Lcount > 0)
                    {
                        Lbegin = Lbegin + Lcount;
                        Lcount = 0;
                    }

                    Lcount++;
                }
                else if(Lcount == 0) 
                {
                    Lbegin++;
                }
            }

            return (Lcount,Lbegin);
        }

        private void plotPMD(double H, double X, double Y, double Z, double NRM)
        {
            demagChart.Series["Demag"].Points.AddXY(H, NRM);
            ZiChart.Series["YX"].Points.AddXY(Y, X);
            ZiChart.Series["YmZ"].Points.AddXY(Y, (-1)*Z);
            ZiChart.Series["YmX"].Points.AddXY(Y, (-1) * X);
            ZiChart.Series["ZX"].Points.AddXY(Z,  X);
        }

        private void plotNRM()
        {
            foreach (DataGridViewRow row in MainTable.Rows)
            {
                double c0 = Convert.ToDouble(row.Cells[0].Value);
                double c1 = Convert.ToDouble(row.Cells[1].Value);
                double c2 = Convert.ToDouble(row.Cells[2].Value);
                double c3 = Convert.ToDouble(row.Cells[3].Value);
                double c4 = Convert.ToDouble(row.Cells[4].Value);

                demagChart.Series["Demag"].Points.AddXY(c0, c4);

                ZiChart.Series["YX"].Points.AddXY(c2, c1);
                ZiChart.Series["YmZ"].Points.AddXY(c2, -c3);
                ZiChart.Series["YmX"].Points.AddXY(c2, -c1);
                ZiChart.Series["ZX"].Points.AddXY(c2, c1);
            }
        }

        private void plotRMG()
        {

            foreach (DataGridViewRow row in MainTable.Rows)
            {
                ARMChart.Series[0].Points.AddXY(row.Cells[0].Value, row.Cells[5].Value);
                ARMChart.Series[1].Points.AddXY(row.Cells[0].Value, row.Cells[6].Value);
            }
        }

        private void loadPMD(string path)
        {
            int ni = 0;

            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);

                (int MLines, int Lbegin) = countDM(lines);

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";

                if (MainTable.RowCount != MLines && MainTable.RowCount > 0)
                {
                    MessageBox.Show("Different steps!");
                    return;
                }

                if (MainTable.RowCount > 0)
                {
                    double H = 0, X = 0, Y = 0, Z = 0, NRM = 0;

                    for (int i = 0; i < MLines; i++)
                    {
                        ni = i;

                        while (lines[Lbegin + i].Contains("  ")) { lines[Lbegin + i] = lines[Lbegin + i].Replace("  ", " "); }
                        while (lines[Lbegin + i].Contains("\t\t")) { lines[Lbegin + i] = lines[Lbegin + i].Replace("\t\t", "\t"); }
                        while (lines[Lbegin + i].Contains("\t ")) { lines[Lbegin + i] = lines[Lbegin + i].Replace("\t ", "\t"); }
                        while (lines[Lbegin + i].Contains(" \t")) { lines[Lbegin + i] = lines[Lbegin + i].Replace(" \t", "\t"); }
                        while (lines[Lbegin + i].Contains(",")) { lines[Lbegin + i] = lines[Lbegin + i].Replace(",", "."); }

                        string[] line = lines[Lbegin + i].Split(new[] { ' ', '\t' });

                        H = double.Parse(line[0].Substring(1), System.Globalization.NumberStyles.Float, provider);
                        X = double.Parse(line[1], System.Globalization.NumberStyles.Float, provider);
                        Y = double.Parse(line[2], System.Globalization.NumberStyles.Float, provider);
                        Z = double.Parse(line[3], System.Globalization.NumberStyles.Float, provider);
                        NRM = double.Parse(line[4], System.Globalization.NumberStyles.Float, provider);

                        MainTable.Rows[i].Cells[0].Value = H;
                        MainTable.Rows[i].Cells[1].Value = X;
                        MainTable.Rows[i].Cells[2].Value = Y;
                        MainTable.Rows[i].Cells[3].Value = Z;
                        MainTable.Rows[i].Cells[4].Value = NRM;

                        plotPMD(H, X, Y, Z, NRM);

                    }

                    plotRMG();
                }
                else
                {
                    double H = 0, X = 0, Y = 0, Z = 0, NRM = 0;

                    for (int i = 0; i < MLines; i++)
                    {
                        ni = i;

                        while (lines[Lbegin + i].Contains("  ")) { lines[Lbegin + i] = lines[Lbegin + i].Replace("  ", " "); }
                        while (lines[Lbegin + i].Contains("\t\t")) { lines[Lbegin + i] = lines[Lbegin + i].Replace("\t\t", "\t"); }
                        while (lines[Lbegin + i].Contains("\t ")) { lines[Lbegin + i] = lines[Lbegin + i].Replace("\t ", "\t"); }
                        while (lines[Lbegin + i].Contains(" \t")) { lines[Lbegin + i] = lines[Lbegin + i].Replace(" \t", "\t"); }
                        while (lines[Lbegin + i].Contains(",")) { lines[Lbegin + i] = lines[Lbegin + i].Replace(",", "."); }

                        string[] line = lines[Lbegin + i].Split(new[] { ' ', '\t' });

                        H = double.Parse(line[0].Substring(1), System.Globalization.NumberStyles.Float, provider);
                        X = double.Parse(line[1], System.Globalization.NumberStyles.Float, provider);
                        Y = double.Parse(line[2], System.Globalization.NumberStyles.Float, provider);
                        Z = double.Parse(line[3], System.Globalization.NumberStyles.Float, provider);
                        NRM = double.Parse(line[4], System.Globalization.NumberStyles.Float, provider);

                        MainTable.Rows.Add(H,X,Y,Z,NRM,0,0);
                        plotPMD(H, X, Y, Z, NRM);

                    }
                }

            }
            catch 
            {
                MessageBox.Show("Error while reading file at line " + (ni + 1).ToString());
            }
        }

        private (int, int, int, int) countRMG(string[] lines)
        {
            int ARMbeg = -1;
            int ARMLength = 0;
            int AFzbeg = -1;
            int AFzLength = 0;
            
            for (int i = 0; i<lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (line.StartsWith("ARM", StringComparison.OrdinalIgnoreCase))
                {
                    if (ARMbeg == -1) ARMbeg = i;
                    ARMLength++;
                }

                else if (line.StartsWith("AFZ", StringComparison.OrdinalIgnoreCase))
                {
                    if (AFzbeg == -1) AFzbeg = i;
                    AFzLength++;
                }
            }

            return (ARMbeg, ARMLength, AFzbeg, AFzLength);
        }

        private void loadRMG(string path)
        {
            int ni = 0;

            try
            {
                string[] lines = System.IO.File.ReadAllLines(path);

                (int ARMbeg, int ARMLength, int AFzbeg, int AFzLength) = countRMG(lines);

                if (ARMLength != AFzLength)
                {
                    MessageBox.Show("Different AFz and ARM!");
                    return;
                }

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";

                if (MainTable.RowCount != ARMLength + 1 && MainTable.RowCount > 0)
                {
                    MessageBox.Show("Different steps!");
                    return;
                }

                if (MainTable.RowCount > 0)
                {
                    while (lines[ARMbeg - 1].Contains("  ")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace("  ", " ");
                    while (lines[ARMbeg - 1].Contains("\t\t")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace("\t\t", "\t");
                    while (lines[ARMbeg - 1].Contains("\t ")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace("\t ", "\t");
                    while (lines[ARMbeg - 1].Contains(" \t")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace(" \t", "\t");
                    while (lines[ARMbeg - 1].Contains(" , ")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace(" , ", "\t");
                    lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace(',', ' ');

                    while (lines[ARMbeg + ARMLength - 1].Contains("  ")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace("  ", " ");
                    while (lines[ARMbeg + ARMLength - 1].Contains("\t\t")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace("\t\t", "\t");
                    while (lines[ARMbeg + ARMLength - 1].Contains("\t ")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace("\t ", "\t");
                    while (lines[ARMbeg + ARMLength - 1].Contains(" \t")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace(" \t", "\t");
                    while (lines[ARMbeg + ARMLength - 1].Contains(" , ")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace(" , ", "\t");
                    lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace(',', ' ');

                    string[] lineARM_0 = lines[ARMbeg - 1]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    string[] lineAFZ_0 = lines[ARMbeg + ARMLength - 1]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    MainTable.Rows[0].Cells[5].Value = double.Parse(lineARM_0[5], NumberStyles.Float, provider);
                    MainTable.Rows[0].Cells[6].Value = double.Parse(lineAFZ_0[5], NumberStyles.Float, provider);

                    double ARMgained = 0, ARMLeft = 0;

                    for (int i = 0; i < ARMLength; i++)
                    {
                        ni = i;

                        while (lines[ARMbeg + i].Contains("  ")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace("  ", " ");
                        while (lines[ARMbeg + i].Contains("\t\t")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace("\t\t", "\t");
                        while (lines[ARMbeg + i].Contains("\t ")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace("\t ", "\t");
                        while (lines[ARMbeg + i].Contains(" \t")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace(" \t", "\t");
                        while (lines[ARMbeg + i].Contains(" , ")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace(" , ", "\t");
                        lines[ARMbeg + i] = lines[ARMbeg + i].Replace(',', ' ');

                        while (lines[AFzbeg + i].Contains("  ")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace("  ", " ");
                        while (lines[AFzbeg + i].Contains("\t\t")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace("\t\t", "\t");
                        while (lines[AFzbeg + i].Contains("\t ")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace("\t ", "\t");
                        while (lines[AFzbeg + i].Contains(" \t")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace(" \t", "\t");
                        while (lines[AFzbeg + i].Contains(" , ")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace(" , ", "\t");
                        lines[AFzbeg + i] = lines[AFzbeg + i].Replace(',', ' ');

                        string[] lineARM = lines[ARMbeg + i]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        string[] lineAFZ = lines[AFzbeg + i]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (lineARM.Length <= 5 || lineAFZ.Length <= 5)
                            continue;

                        ARMgained = double.Parse(lineARM[5], NumberStyles.Float, provider);
                        ARMLeft = double.Parse(lineAFZ[5], NumberStyles.Float, provider);

                        MainTable.Rows[i+1].Cells[5].Value = ARMgained;
                        MainTable.Rows[i+1].Cells[6].Value = ARMLeft;
                    }

                    plotRMG();
                }
                else
                {

                    while (lines[ARMbeg - 1].Contains("  ")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace("  ", " ");
                    while (lines[ARMbeg - 1].Contains("\t\t")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace("\t\t", "\t");
                    while (lines[ARMbeg - 1].Contains("\t ")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace("\t ", "\t");
                    while (lines[ARMbeg - 1].Contains(" \t")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace(" \t", "\t");
                    while (lines[ARMbeg - 1].Contains(" , ")) lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace(" , ", "\t");
                    lines[ARMbeg - 1] = lines[ARMbeg - 1].Replace(',', ' ');

                    while (lines[ARMbeg + ARMLength - 1].Contains("  ")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace("  ", " ");
                    while (lines[ARMbeg + ARMLength - 1].Contains("\t\t")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace("\t\t", "\t");
                    while (lines[ARMbeg + ARMLength - 1].Contains("\t ")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace("\t ", "\t");
                    while (lines[ARMbeg + ARMLength - 1].Contains(" \t")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace(" \t", "\t");
                    while (lines[ARMbeg + ARMLength - 1].Contains(" , ")) lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace(" , ", "\t");
                    lines[ARMbeg + ARMLength - 1] = lines[ARMbeg + ARMLength - 1].Replace(',', ' ');

                    string[] lineARM_0 = lines[ARMbeg - 1]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    string[] lineAFZ_0 = lines[ARMbeg + ARMLength - 1]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    MainTable.Rows.Add(0, 0, 0, 0, 0, double.Parse(lineARM_0[5], NumberStyles.Float, provider),
                        double.Parse(lineAFZ_0[5], NumberStyles.Float, provider));

                    double ARMgained = 0, ARMLeft = 0;

                    for (int i = 0; i < ARMLength; i++)
                    {
                        ni = i;

                        while (lines[ARMbeg + i].Contains("  ")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace("  ", " ");
                        while (lines[ARMbeg + i].Contains("\t\t")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace("\t\t", "\t");
                        while (lines[ARMbeg + i].Contains("\t ")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace("\t ", "\t");
                        while (lines[ARMbeg + i].Contains(" \t")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace(" \t", "\t");
                        while (lines[ARMbeg + i].Contains(" , ")) lines[ARMbeg + i] = lines[ARMbeg + i].Replace(" , ", "\t");
                        lines[ARMbeg + i] = lines[ARMbeg + i].Replace(',', ' '); 

                        while (lines[AFzbeg + i].Contains("  ")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace("  ", " ");
                        while (lines[AFzbeg + i].Contains("\t\t")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace("\t\t", "\t");
                        while (lines[AFzbeg + i].Contains("\t ")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace("\t ", "\t");
                        while (lines[AFzbeg + i].Contains(" \t")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace(" \t", "\t");
                        while (lines[AFzbeg + i].Contains(" , ")) lines[AFzbeg + i] = lines[AFzbeg + i].Replace(" , ", "\t");
                        lines[AFzbeg + i] = lines[AFzbeg + i].Replace(',', ' '); 

                        string[] lineARM = lines[ARMbeg + i]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        string[] lineAFZ = lines[AFzbeg + i]
                            .Trim()
                            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (lineARM.Length <= 5 || lineAFZ.Length <= 5)
                            continue;

                        ARMgained = double.Parse(lineARM[5], NumberStyles.Float, provider);
                        ARMLeft = double.Parse(lineAFZ[5], NumberStyles.Float, provider);

                        MainTable.Rows.Add(0, 0, 0, 0, 0, ARMgained, ARMLeft);
                    }
                }

            }
            catch 
            {
                MessageBox.Show("Error while reading file at line " + (ni + 1).ToString());
            }
        }

        private void toolStripButton_openPMD_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Select .pmd file";
                fileDialog.Filter = "RMG Files (*.pmd)|*.pmd";

                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadPMD(fileDialog.FileName);
                    pmd_label.Text = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                }
            }
        }

        private void toolStripButton_openRMG_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Select .rmg file";
                fileDialog.Filter = "RMG Files (*.rmg)|*.rmg";

                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadRMG(fileDialog.FileName);
                    rmg_label.Text = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                }
            }
        }

        private void ZiChart_Click(object sender, EventArgs e)
        {
            ZiChart.ChartAreas[0].Visible = ! ZiChart.ChartAreas[0].Visible;
            ZiChart.ChartAreas[1].Visible = ! ZiChart.ChartAreas[1].Visible;
        }

        private void plotTable()
        {
            demagChart.Series["Demag"].Points.Clear();
            ZiChart.Series["YX"].Points.Clear();
            ZiChart.Series["YmZ"].Points.Clear();
            ZiChart.Series["YmX"].Points.Clear();
            ZiChart.Series["ZX"].Points.Clear();

            ARMChart.Series[0].Points.Clear();
            ARMChart.Series[1].Points.Clear();

            plotRMG();
            plotNRM();
        }

        private void RemoveResidue()
        {
            if (LineNumber_radioButton.Checked)
            {
                try
                {
                    int line_number = int.Parse(res_input_textBox.Text, NumberStyles.Integer)-1;

                    if (line_number < 0 || line_number >= MainTable.Rows.Count)
                    {
                        MessageBox.Show("The number is outside the valid range.");
                        return;
                    }

                    double ValueToExtract = 0;

                    try
                    {
                        if (NRM_radioButton.Checked)
                        {
                            ValueToExtract = Convert.ToDouble(MainTable.Rows[line_number].Cells[4].Value);
                        }
                        else if(ARMgained_radioButton.Checked)
                        {
                            ValueToExtract = Convert.ToDouble(MainTable.Rows[line_number].Cells[5].Value);
                        }
                        else if(ARMleft_radioButton.Checked)
                        {
                            ValueToExtract = Convert.ToDouble(MainTable.Rows[line_number].Cells[6].Value);
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Cell does not contain a valid number.");
                        return;
                    }
                    catch (ArgumentNullException)
                    {
                        MessageBox.Show("Cell is empty.");
                        return;
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Cell contains a non-numeric value.");
                        return;
                    }

                    try 
                    {
                        foreach (DataGridViewRow row in MainTable.Rows)
                        {
                            try
                            {
                                if (NRM_checkBox.Checked)
                                {
                                    double current = Convert.ToDouble(row.Cells[4].Value);
                                    row.Cells[4].Value = current - ValueToExtract;
                                }
                                if (ARMgained_checkBox.Checked)
                                {
                                    double current = Convert.ToDouble(row.Cells[5].Value);
                                    row.Cells[5].Value = current - ValueToExtract;
                                }
                                if (ARMleft_checkBox.Checked)
                                {
                                    double current = Convert.ToDouble(row.Cells[6].Value);
                                    row.Cells[6].Value = current - ValueToExtract;
                                }
                            }

                            catch (FormatException)
                            {
                                MessageBox.Show("Invalid format in line  " + row.Index);
                            }
                            catch (ArgumentNullException)
                            {
                                MessageBox.Show("The cell in line " + row.Index + " is empty");
                            }
                            catch (InvalidCastException)
                            {
                                MessageBox.Show("Unable to convert number in line  " + row.Index);
                            }
                            catch (OverflowException)
                            {
                                MessageBox.Show("The number is too large or too small in line " + row.Index);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                }
                catch (FormatException)
                {
                    MessageBox.Show("The input is not a valid integer.");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The number is too large or too small for an Int32.");
                }
            }
            else if (Value_radioButton.Checked)
            {

            }
            else { MessageBox.Show("Please, select initial value type"); }
        }

        private void Remove_residue_button_Click(object sender, EventArgs e)
        {
            RemoveResidue();
            plotTable();
        }
    }
}
