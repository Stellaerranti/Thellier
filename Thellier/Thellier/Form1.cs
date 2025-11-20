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
        private Form2 _form2;
        public Form1()
        {
            InitializeComponent();
            MainTable.RowPostPaint += MainTable_RowPostPaint;
        }

        private void MainTable_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string rowNumber = (e.RowIndex + 1).ToString();

            // Draw the number in the row header
            var grid = (DataGridView)sender;
            using (SolidBrush brush = new SolidBrush(grid.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(
                    rowNumber,
                    grid.RowHeadersDefaultCellStyle.Font,
                    brush,
                    e.RowBounds.Left + 10,   
                    e.RowBounds.Top + 4      
                );
            }
        }

        private void ArrowPos(Chart chart)
        {
            foreach (Series s in chart.Series)
            {
                s.IsValueShownAsLabel = true;

                s.SmartLabelStyle.Enabled = false;
                s.SmartLabelStyle.IsOverlappedHidden = false;
                s.SmartLabelStyle.MovingDirection =
                    System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Center;
                s.SmartLabelStyle.CalloutStyle =
                    System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.None;
            }
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

            ArrowPos(demagChart);
            ArrowPos(ZiChart);
            ArrowPos(ARMChart);

            //Making charts look better

            demagChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            demagChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            demagChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            demagChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            demagChart.ChartAreas[0].AxisX.Crossing = 0; 
            demagChart.ChartAreas[0].AxisY.Crossing = 0;

            ARMChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ARMChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ARMChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            ARMChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            ARMChart.ChartAreas[0].AxisX.Crossing = 0;
            ARMChart.ChartAreas[0].AxisY.Crossing = 0;

            ZiChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ZiChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ZiChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            ZiChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            ZiChart.ChartAreas[0].AxisX.Crossing = 0;
            ZiChart.ChartAreas[0].AxisY.Crossing = 0;

            ZiChart.ChartAreas[1].AxisX.MajorGrid.Enabled = false;
            ZiChart.ChartAreas[1].AxisY.MajorGrid.Enabled = false;
            ZiChart.ChartAreas[1].AxisX.MinorGrid.Enabled = false;
            ZiChart.ChartAreas[1].AxisY.MinorGrid.Enabled = false;

            ZiChart.ChartAreas[1].AxisX.Crossing = 0;
            ZiChart.ChartAreas[1].AxisY.Crossing = 0;
        }

        public static void GetNiceAxis(double dataMin, double dataMax,
                               out double niceMin, out double niceMax, out double step)
        {
            // Swap if reversed
            if (dataMax < dataMin)
            {
                double tmp = dataMin;
                dataMin = dataMax;
                dataMax = tmp;
            }

            // If all values equal, create a small range around them
            if (dataMax == dataMin)
            {
                double center = dataMin;
                double delta = Math.Abs(center);
                if (delta == 0) delta = 1.0; // avoid log10(0)

                dataMin = center - delta * 0.5;
                dataMax = center + delta * 0.5;
            }

            double range = dataMax - dataMin;

            // Add padding (5% on each side)
            double padding = range * 0.05;
            double min = dataMin - padding;
            double max = dataMax + padding;
            range = max - min;

            // Target ~5 major ticks
            double roughStep = range / 5.0;

            // "Nice" step: 1, 2, or 5 times 10^n
            double pow10 = Math.Pow(10, Math.Floor(Math.Log10(roughStep)));
            double[] niceSteps = { 1, 2, 5 };
            double bestStep = niceSteps[0] * pow10;

            for (int i = 1; i < niceSteps.Length; i++)
            {
                double candidate = niceSteps[i] * pow10;
                if (Math.Abs(candidate - roughStep) < Math.Abs(bestStep - roughStep))
                    bestStep = candidate;
            }

            step = bestStep;

            // Round bounds to multiples of step
            niceMin = Math.Floor(min / step) * step;
            niceMax = Math.Ceiling(max / step) * step;
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

        public static void AutoAxis(Chart chart, string areaName)
        {
            if (!chart.ChartAreas.IsUniqueName(areaName) &&
                chart.ChartAreas.IndexOf(areaName) < 0)
                return;

            var ca = chart.ChartAreas[areaName];

            double dataMinX = double.PositiveInfinity;
            double dataMaxX = double.NegativeInfinity;
            double dataMinY = double.PositiveInfinity;
            double dataMaxY = double.NegativeInfinity;

            bool hasPoints = false;

            foreach (Series s in chart.Series)
            {
                // Use only series that belong to this ChartArea
                if (s.ChartArea != areaName)
                    continue;

                if (s.Points.Count == 0)
                    continue;

                hasPoints = true;

                double sMinX = s.Points.Min(p => p.XValue);
                double sMaxX = s.Points.Max(p => p.XValue);
                double sMinY = s.Points.Min(p => p.YValues[0]);
                double sMaxY = s.Points.Max(p => p.YValues[0]);

                if (sMinX < dataMinX) dataMinX = sMinX;
                if (sMaxX > dataMaxX) dataMaxX = sMaxX;
                if (sMinY < dataMinY) dataMinY = sMinY;
                if (sMaxY > dataMaxY) dataMaxY = sMaxY;
            }

            if (!hasPoints)
                return; // nothing to do

            double niceMinX, niceMaxX, stepX;
            double niceMinY, niceMaxY, stepY;

            GetNiceAxis(dataMinX, dataMaxX, out niceMinX, out niceMaxX, out stepX);
            GetNiceAxis(dataMinY, dataMaxY, out niceMinY, out niceMaxY, out stepY);

            ca.AxisX.Minimum = niceMinX;
            ca.AxisX.Maximum = niceMaxX;
            ca.AxisX.Interval = stepX;

            ca.AxisY.Minimum = niceMinY;
            ca.AxisY.Maximum = niceMaxY;
            ca.AxisY.Interval = stepY;
        }

        public static void AutoAxis(Chart chart, int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= chart.ChartAreas.Count)
                return;

            string areaName = chart.ChartAreas[areaIndex].Name;
            AutoAxis(chart, areaName);
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
    
                string label = (row.Index + 1).ToString();

                int p1 = demagChart.Series["Demag"].Points.AddXY(c0, c4);
                demagChart.Series["Demag"].Points[p1].Label = label;

                int p2 = ZiChart.Series["YX"].Points.AddXY(c2, c1);
                ZiChart.Series["YX"].Points[p2].Label = label;

                int p3 = ZiChart.Series["YmZ"].Points.AddXY(-c3, c1);
                ZiChart.Series["YmZ"].Points[p3].Label = label;

                int p4 = ZiChart.Series["YmX"].Points.AddXY(c2, c1);
                ZiChart.Series["YmX"].Points[p4].Label = label;

                int p5 = ZiChart.Series["ZX"].Points.AddXY(c2, -c3);
                ZiChart.Series["ZX"].Points[p5].Label = label;
            }

            AutoAxis(demagChart, 0);



            if (demagChart.ChartAreas[0].AxisX.Minimum < 0) { demagChart.ChartAreas[0].AxisX.Minimum = 0; }
            if(demagChart.ChartAreas[0].AxisY.Minimum < 0) { demagChart.ChartAreas[0].AxisY.Minimum = 0; }

            AutoAxis(ZiChart, 0);
            AutoAxis(ZiChart, 1);
        }
        

        private void plotRMG()
        {

            foreach (DataGridViewRow row in MainTable.Rows)
            {
                if (row.IsNewRow) continue;

                double x = Convert.ToDouble(row.Cells[0].Value);
                double y1 = Convert.ToDouble(row.Cells[5].Value);
                double y2 = Convert.ToDouble(row.Cells[6].Value);

                int p0 = ARMChart.Series[0].Points.AddXY(x, y1);
                ARMChart.Series[0].Points[p0].Label = (row.Index + 1).ToString();

                int p1 = ARMChart.Series[1].Points.AddXY(x, y2);
                ARMChart.Series[1].Points[p1].Label = (row.Index + 1).ToString();
            }
            AutoAxis(ARMChart, 0);

            if (ARMChart.ChartAreas[0].AxisX.Minimum < 0) { ARMChart.ChartAreas[0].AxisX.Minimum = 0; }
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
                        //NRM = double.Parse(line[4], System.Globalization.NumberStyles.Float, provider);

                        NRM = Math.Sqrt(X*X+Y*Y+Z*Z)*1000;

                        MainTable.Rows[i].Cells[0].Value = H;
                        MainTable.Rows[i].Cells[1].Value = X;
                        MainTable.Rows[i].Cells[2].Value = Y;
                        MainTable.Rows[i].Cells[3].Value = Z;
                        MainTable.Rows[i].Cells[4].Value = NRM;

                        //plotPMD(H, X, Y, Z, NRM);

                    }

                    plotRMG();
                    plotNRM();
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
                        //NRM = double.Parse(line[4], System.Globalization.NumberStyles.Float, provider);

                        NRM = Math.Sqrt(X * X + Y * Y + Z * Z)*1000;

                        MainTable.Rows.Add(H,X,Y,Z,NRM,0,0);
                        //plotPMD(H, X, Y, Z, NRM);

                    }
                    plotNRM();
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
                try
                {


                    double ValueToExtract = Double.Parse(res_input_textBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture);

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
                    MessageBox.Show("The input is not a valid number.");
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The number is too large or too small for an Float32.");
                }
            }
            else { MessageBox.Show("Please, select initial value type"); }
        }

        private void Remove_residue_button_Click(object sender, EventArgs e)
        {
            RemoveResidue();
            plotTable();
        }

        private void button_plot_Click(object sender, EventArgs e)
        {
            if (_form2 == null || _form2.IsDisposed)
            {
                _form2 = new Form2(MainTable);
                _form2.FormClosing += (s, args) =>
                {
                    args.Cancel = true;
                    _form2.Hide();
                };
            }

            _form2.RefreshFromMain(); 

            if (_form2.WindowState == FormWindowState.Minimized)
                _form2.WindowState = FormWindowState.Normal;

            _form2.Show();         
            _form2.BringToFront(); 
            _form2.Activate();
        }
    }
}
