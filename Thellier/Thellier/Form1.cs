using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

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
            ZiChart.Series["YmX"].Points.AddXY(Y, X);
            ZiChart.Series["ZX"].Points.AddXY((-1) * Z,  X);
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

        //double yr1 = Double.Parse(yr1Text.Text, NumberStyles.Any, CultureInfo.InvariantCulture);

        /*
         private void toolStripButton_LockationFile_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {

                fileDialog.Title = "Select a File with coordinates";
                fileDialog.Filter = "XML Files (*.xml)|*.xml";


                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    coordFilePath = fileDialog.FileName;
                    ReadXML(coordFilePath);
                    CheckIfDataReady();
                }
            }
        }
         */

        /*
         private void readTestFile()
        {
            if (ImportTest.ShowDialog() == DialogResult.Cancel) { return; }

            string filename = ImportTest.FileName;
            int ni = 0;

            try
            {
                string[] lines = System.IO.File.ReadAllLines(filename);

                TestFileLabel.Text = Path.GetFileName(filename);
                double U = 0;
                double Ca = 0;
                double U_std = 0;
                double Ca_std = 0;

                double UCa = 0;
                double UCa_std = 0;

                NumberFormatInfo provider = new NumberFormatInfo();
                provider.NumberDecimalSeparator = ".";

                for (int i = 0; i < lines.Length; i++)
                {
                    ni = i;

                    if (lines[i].Length > 2)
                    {
                        while (lines[i].Contains("  ")) { lines[i] = lines[i].Replace("  ", " "); }
                        while (lines[i].Contains("\t\t")) { lines[i] = lines[i].Replace("\t\t", "\t"); }
                        while (lines[i].Contains("\t ")) { lines[i] = lines[i].Replace("\t ", "\t"); }
                        while (lines[i].Contains(" \t")) { lines[i] = lines[i].Replace(" \t", "\t"); }
                        while (lines[i].Contains(",")) { lines[i] = lines[i].Replace(",", "."); }

                        string[] line = lines[i].Split(new[] { ' ', '\t' });

                        U = Convert.ToDouble(line[1], provider);
                        Ca = Convert.ToDouble(line[3], provider);

                        U_std = Convert.ToDouble(line[2], provider);
                        Ca_std = Convert.ToDouble(line[4], provider);

                        UCa = U / Ca;

                        //UCa_std = (Calc_UCa_std(U,Ca,U_std,Ca_std)/UCa)*(UCa* 1000000);
                        UCa_std = Calc_UCa_std(U, Ca, U_std, Ca_std);

                        TestGrid.Rows.Add((i+1).ToString("F0"), 0,U.ToString("F4"), U_std.ToString("F4"),Ca.ToString("E3"),Ca_std.ToString("E3"),UCa.ToString("E3"),UCa_std.ToString("E3"),0,0);
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Error while reading file at line " + (ni + 1).ToString());
            }


        }


         */

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
                }
            }
        }


    }
}
