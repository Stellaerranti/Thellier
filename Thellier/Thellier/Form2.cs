using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Thellier
{
    public partial class Form2 : Form
    {
        private readonly DataGridView _mainTable;

        double b_DEMAG = 0, b_AraiNagata = 0, b_ARM = 0;
        double sigma_b_DEMAG = 0, sigma_b_AraiNagata = 0, sigma_b_ARM = 0;
        double sigma_resid_DEMAG = 0, sigma_resid_AraiNagata = 0, sigma_resid_ARM = 0;
        double beta_DEMAG = 0, beta_AraiNagata = 0, beta_ARM = 0;        
        double f_DEMAG = 0, f_AraiNagata = 0, f_ARM = 0;
        double R_DEMAG = 0, R_AraiNagata = 0, R_ARM = 0;
        double fresid_DEMAG = 0, fresid_AraiNagata =0, fresid_ARM = 0;
        //double bAA_DEMAG = 0, bAA_AraiNagata = 0, bAA_ARM = 0;

        double intercept_DEMAG = 0, intercept_AraiNagata = 0, intercept_ARM = 0;
        public Form2(DataGridView mainTable)
        {
            InitializeComponent();
            _mainTable = mainTable;

            RefreshFromMain();
            AddPlaceholder(points_textBox, "1 - 4");
        }

        private void AddPlaceholder(System.Windows.Forms.TextBox tb, string text)
        {
            tb.ForeColor = Color.Gray;
            tb.Text = text;

            tb.GotFocus += (s, e) =>
            {
                if (tb.Text == text)
                {
                    tb.Text = "";
                    tb.ForeColor = Color.Black;
                }
            };

            tb.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.ForeColor = Color.Gray;
                    tb.Text = text;
                }
            };
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

        private void ArrowPos_ForSeries(Chart chart, string seriesName)
        {
            if (!chart.Series.IsUniqueName(seriesName) &&
                chart.Series.IndexOf(seriesName) < 0)
                return;   

            Series s = chart.Series[seriesName];

            s.IsValueShownAsLabel = true;
            
            s.SmartLabelStyle.Enabled = false;
            s.SmartLabelStyle.IsOverlappedHidden = false;
            s.SmartLabelStyle.MovingDirection =
                System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Center;
            s.SmartLabelStyle.CalloutStyle =
                System.Windows.Forms.DataVisualization.Charting.LabelCalloutStyle.None;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ArrowPos_ForSeries(DemagDemagChart, "Data");
            ArrowPos_ForSeries(ARMARMchart, "Data");
            ArrowPos_ForSeries(AraiNagatachart, "Data");

            DemagDemagChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            DemagDemagChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            DemagDemagChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            DemagDemagChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            DemagDemagChart.ChartAreas[0].AxisX.Crossing = 0;
            DemagDemagChart.ChartAreas[0].AxisY.Crossing = 0;

            ARMARMchart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ARMARMchart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ARMARMchart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            ARMARMchart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            ARMARMchart.ChartAreas[0].AxisX.Crossing = 0;
            ARMARMchart.ChartAreas[0].AxisY.Crossing = 0;

            AraiNagatachart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            AraiNagatachart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            AraiNagatachart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            AraiNagatachart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

            AraiNagatachart.ChartAreas[0].AxisX.Crossing = 0;
            AraiNagatachart.ChartAreas[0].AxisY.Crossing = 0;
        }

        private void DrawMainChart()
        {
            DemagDemagChart.Series[0].Points.Clear();
            ARMARMchart.Series[0].Points.Clear();
            AraiNagatachart.Series[0].Points.Clear();

            foreach (DataGridViewRow row in _mainTable.Rows)
            {
                if (row.IsNewRow) continue;

                string label = (row.Index + 1).ToString();

                double NRM = Convert.ToDouble(row.Cells[4].Value);
                double ARMgained = Convert.ToDouble(row.Cells[5].Value);
                double ARMleft = Convert.ToDouble(row.Cells[6].Value);

                int p1 = DemagDemagChart.Series[0].Points.AddXY(NRM, ARMleft);
                DemagDemagChart.Series[0].Points[p1].Label = label;

                int p2 = AraiNagatachart.Series[0].Points.AddXY(ARMgained, NRM);
                AraiNagatachart.Series[0].Points[p2].Label = label;

                int p3 = ARMARMchart.Series[0].Points.AddXY(ARMgained, ARMleft);
                ARMARMchart.Series[0].Points[p3].Label = label;

            }

            AutoAxis(DemagDemagChart, 0);
            AutoAxis(AraiNagatachart, 0);
            AutoAxis(ARMARMchart, 0);

            DemagDemagChart.Invalidate();
            ARMARMchart.Invalidate();
            AraiNagatachart.Invalidate();
        }

        public void RefreshFromMain()
        {
            if (_mainTable == null) return;

            DrawMainChart();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private bool TryParseRange(string input, out int start, out int end, out string error)
        {
            start = 0;
            end = 0;
            error = null;

            if (string.IsNullOrWhiteSpace(input))
            {
                error = "Empty range";
                return false;
            }

            input = input.Replace(" ", "");

            if (!input.Contains("-"))
            {
                error = "Enter the range in the X-Y format";
                return false;
            }

            string[] parts = input.Split('-');

            if (parts.Length != 2)
            {
                error = "Incorrect range format";
                return false;
            }

            if (!int.TryParse(parts[0], out start))
            {
                error = "The left part of the range is not a number";
                return false;
            }

            if (!int.TryParse(parts[1], out end))
            {
                error = "The right part of the range is not a number";
                return false;
            }

            if (start < 0 || end < 0)
            {
                error = "The numbers must be positive";
                return false;
            }

            return true;
        }

        private void plotFitting()
        {
            DemagDemagChart.Series[1].Points.Clear();
            ARMARMchart.Series[1].Points.Clear();
            AraiNagatachart.Series[1].Points.Clear();

            if (DemagDemagChart.Series[0].Points.Count == 0)
            {
                return; 
            }

            double minX_DEMAG = DemagDemagChart.Series[0].Points.Min(p => p.XValue);
            double maxX_DEMAG = DemagDemagChart.Series[0].Points.Max(p => p.XValue);

            DemagDemagChart.Series[1].Points.AddXY(minX_DEMAG, intercept_DEMAG + b_DEMAG * minX_DEMAG);
            DemagDemagChart.Series[1].Points.AddXY(maxX_DEMAG, intercept_DEMAG + b_DEMAG * maxX_DEMAG);

            double minX_AraiNagata = AraiNagatachart.Series[0].Points.Min(p => p.XValue);
            double maxX_AraiNagata = AraiNagatachart.Series[0].Points.Max(p => p.XValue);

            AraiNagatachart.Series[1].Points.AddXY(minX_AraiNagata, intercept_AraiNagata + b_AraiNagata * minX_AraiNagata);
            AraiNagatachart.Series[1].Points.AddXY(maxX_AraiNagata, intercept_AraiNagata + b_AraiNagata * maxX_AraiNagata);

            double minX_ARMA = ARMARMchart.Series[0].Points.Min(p => p.XValue);
            double maxX_ARMA = ARMARMchart.Series[0].Points.Max(p => p.XValue);

            ARMARMchart.Series[1].Points.AddXY(minX_ARMA, intercept_ARM + b_ARM * minX_ARMA);
            ARMARMchart.Series[1].Points.AddXY(maxX_ARMA, intercept_ARM + b_ARM * maxX_ARMA);
        }

        private (double,double,double) Rcorr(int start, int end)
        {
            double xDemag = 0, xArai = 0, xARM = 0;
            double yDemag = 0, yArai = 0, yARM = 0;

            double n = 0;

            for (int i = start; i < end; i++)
            {
                double NRM = Convert.ToDouble(_mainTable.Rows[i].Cells[4].Value);
                double ARMgained = Convert.ToDouble(_mainTable.Rows[i].Cells[5].Value);
                double ARMleft = Convert.ToDouble(_mainTable.Rows[i].Cells[6].Value);

                xDemag += NRM;
                yDemag += ARMleft;

                xArai += ARMgained;
                yArai += NRM;

                xARM += ARMgained;
                yARM += ARMleft;

                n++;
            }

            double xDemag_mean = xDemag / n;
            double yDemag_mean = yDemag / n;

            double xArai_mean = xArai / n;
            double yArai_mean = yArai / n;

            double xARM_mean = xARM / n;
            double yARM_mean = yARM / n;

            double DEMAG_Numerator = 0, Arai_Numerator = 0, ARM_Numerator = 0;
            double xsqDemag = 0, xsqArai = 0, xsqARM = 0;
            double ysqDemag = 0, ysqArai = 0, ysqARM = 0;

            for (int i = start; i < end; i++)
            {
                double NRM = Convert.ToDouble(_mainTable.Rows[i].Cells[4].Value);
                double ARMgained = Convert.ToDouble(_mainTable.Rows[i].Cells[5].Value);
                double ARMleft = Convert.ToDouble(_mainTable.Rows[i].Cells[6].Value);

                DEMAG_Numerator += (NRM - xDemag_mean) * (ARMleft - yDemag_mean);
                xsqDemag += (NRM - xDemag_mean) * (NRM - xDemag_mean);
                ysqDemag += (ARMleft - yDemag_mean) * (ARMleft - yDemag_mean);

                Arai_Numerator += (ARMgained - xArai_mean) * (NRM - yArai_mean);
                xsqArai += (ARMgained - xArai_mean) * (ARMgained - xArai_mean);
                ysqArai += (NRM - yArai_mean) * (NRM - yArai_mean);

                ARM_Numerator += (ARMgained - xARM_mean) * (ARMleft - yARM_mean);
                xsqARM += (ARMgained - xARM_mean) * (ARMgained - xARM_mean);
                ysqARM += (ARMleft - yARM_mean) * (ARMleft - yARM_mean);
            }

            double R_DEMAG = DEMAG_Numerator * DEMAG_Numerator / (xsqDemag * ysqDemag);
            double R_Arai = Arai_Numerator * Arai_Numerator / (xsqArai * ysqArai);
            double R_ARM = ARM_Numerator * ARM_Numerator /(xsqARM * ysqARM);

            return (R_DEMAG, R_Arai, R_ARM);
        }

        private void Calculate(int start, int end)
        {
            double xSum_DEMAG = 0, xSum_AraiNagata = 0, xSum_ARM = 0;
            double ySum_DEMAG = 0, ySum_AraiNagata = 0, ySum_ARM = 0;
            double xySum_DEMAG = 0, xySum_AraiNagata = 0, xySum_ARM = 0;
            double xsqSum_DEMAG = 0, xsqSum_AraiNagata =0, xsqSum_ARM = 0;
            double ysqSum_DEMAG = 0, ysqSum_AraiNagata = 0, ysqSum_ARM = 0;

            double xMin_DEMAG = double.PositiveInfinity, xMax_DEMAG = double.NegativeInfinity;
            double xMin_Arai = double.PositiveInfinity, xMax_Arai = double.NegativeInfinity;
            double xMin_ARM = double.PositiveInfinity, xMax_ARM = double.NegativeInfinity;

            double yMin_DEMAG = double.PositiveInfinity, yMax_DEMAG = double.NegativeInfinity;
            double yMin_Arai = double.PositiveInfinity, yMax_Arai = double.NegativeInfinity;
            double yMin_ARM = double.PositiveInfinity, yMax_ARM = double.NegativeInfinity;

            double n = 0;

            for (int i = start; i < end; i++)
            {
                double NRM = Convert.ToDouble(_mainTable.Rows[i].Cells[4].Value);
                double ARMgained = Convert.ToDouble(_mainTable.Rows[i].Cells[5].Value);
                double ARMleft = Convert.ToDouble(_mainTable.Rows[i].Cells[6].Value);

                // DEMAG: x = NRM, y = ARMleft
                xSum_DEMAG += NRM;
                ySum_DEMAG += ARMleft;
                xySum_DEMAG += ARMleft*NRM;
                xsqSum_DEMAG += NRM * NRM;
                ysqSum_DEMAG += ARMleft * ARMleft;

                if (NRM < xMin_DEMAG) xMin_DEMAG = NRM;
                if (NRM > xMax_DEMAG) xMax_DEMAG = NRM;

                if (ARMleft < yMin_DEMAG) yMin_DEMAG = ARMleft;
                if (ARMleft > yMax_DEMAG) yMax_DEMAG = ARMleft;

                // Arai–Nagata: x = ARMgained, y = NRM
                xSum_AraiNagata += ARMgained;
                ySum_AraiNagata += NRM;
                xySum_AraiNagata += ARMgained * NRM;
                xsqSum_AraiNagata += ARMgained * ARMgained;
                ysqSum_AraiNagata += NRM * NRM;

                if (ARMgained < xMin_Arai) xMin_Arai = ARMgained;
                if (ARMgained > xMax_Arai) xMax_Arai = ARMgained;

                if (NRM < yMin_Arai) yMin_Arai = NRM;
                if (NRM > yMax_Arai) yMax_Arai = NRM;

                // ARM–ARM: x = ARMgained, y = ARMleft
                xSum_ARM += ARMgained;
                ySum_ARM += ARMleft;
                xySum_ARM += ARMleft * ARMgained;
                xsqSum_ARM += ARMgained * ARMgained;
                ysqSum_ARM += ARMleft * ARMleft;

                if (ARMgained < xMin_ARM) xMin_ARM = ARMgained;
                if (ARMgained > xMax_ARM) xMax_ARM = ARMgained;

                if (ARMleft < yMin_ARM) yMin_ARM = ARMleft;
                if (ARMleft > yMax_ARM) yMax_ARM = ARMleft;

                n++;
            }

            double R_DEMAG, R_Arai, R_ARM;

            (R_DEMAG, R_Arai, R_ARM) = Rcorr(start, end);

            // ---------- DEMAG ----------
            double Sxx_DEMAG = xsqSum_DEMAG - (xSum_DEMAG * xSum_DEMAG) / n;
            double Syy_DEMAG = ysqSum_DEMAG - (ySum_DEMAG * ySum_DEMAG) / n;
            double Sxy_DEMAG = xySum_DEMAG - (xSum_DEMAG * ySum_DEMAG) / n;

            b_DEMAG = Sxy_DEMAG / Sxx_DEMAG;

            double RSS_DEMAG = Syy_DEMAG - (Sxy_DEMAG * Sxy_DEMAG) / Sxx_DEMAG;
            double sigma2_DEMAG = RSS_DEMAG / (n - 2);

            sigma_resid_DEMAG = Math.Sqrt(sigma2_DEMAG);
            sigma_b_DEMAG = Math.Sqrt(sigma2_DEMAG / Sxx_DEMAG);

            double dx_DEMAG = xMax_DEMAG - xMin_DEMAG;
            beta_DEMAG = (dx_DEMAG > 0) ? (sigma_resid_DEMAG / dx_DEMAG) : double.NaN;

            //intercept_DEMAG = (ySum_DEMAG - b_DEMAG * xSum_DEMAG) / n;

            intercept_DEMAG = (ySum_DEMAG - b_DEMAG * xSum_DEMAG) / n;

            b_DEMAG_label.Text = b_DEMAG.ToString("F4");
            sigmab_DEMAG_label.Text = sigma_b_DEMAG.ToString("F4");

            //beta_DEMAG_label.Text = beta_DEMAG.ToString();

            n_DEMAG_label.Text = n.ToString("F4");

            fresid_DEMAG = intercept_DEMAG/(yMax_DEMAG - yMin_DEMAG);
           
            fresid_DEMAG_label.Text = fresid_DEMAG.ToString("F4");

            R_DEMAG_label.Text = R_DEMAG.ToString("F4");

            // ---------- ARAI-NAGATA ----------
            double Sxx_AN = xsqSum_AraiNagata - (xSum_AraiNagata * xSum_AraiNagata) / n;
            double Syy_AN = ysqSum_AraiNagata - (ySum_AraiNagata * ySum_AraiNagata) / n;
            double Sxy_AN = xySum_AraiNagata - (xSum_AraiNagata * ySum_AraiNagata) / n;

            double NRM_total = Convert.ToDouble(_mainTable.Rows[0].Cells[4].Value);          
            double NRM_startSeg = Convert.ToDouble(_mainTable.Rows[start].Cells[4].Value);   
            double NRM_endSeg = Convert.ToDouble(_mainTable.Rows[end - 1].Cells[4].Value);  

            f_AraiNagata = (NRM_startSeg - NRM_endSeg) / NRM_total;

            b_AraiNagata = Sxy_AN / Sxx_AN;

            double RSS_AN = Syy_AN - (Sxy_AN * Sxy_AN) / Sxx_AN;
            double sigma2_AN = RSS_AN / (n - 2);

            sigma_resid_AraiNagata = Math.Sqrt(sigma2_AN);
            sigma_b_AraiNagata = Math.Sqrt(sigma2_AN / Sxx_AN);

            double dx_Arai = xMax_Arai - xMin_Arai;

            beta_AraiNagata = (b_AraiNagata > 0) ? (sigma_b_AraiNagata / b_AraiNagata) : double.NaN;

            intercept_AraiNagata = (ySum_AraiNagata - b_AraiNagata * xSum_AraiNagata) / n;            

            b_AraiNagata_label.Text = b_AraiNagata.ToString("F4");
            sigmab_AraiNagata_label.Text = sigma_b_AraiNagata.ToString("F4");

            beta_AraiNagata_label.Text = beta_AraiNagata.ToString("F4");

            n_AraiNagata_label.Text = n.ToString("F4");

            f_AraiNagata_label.Text = f_AraiNagata.ToString("F4");

            R_AraiNagata_label.Text = R_Arai.ToString("F4");

            // ---------- ARM ----------
            double Sxx_ARM = xsqSum_ARM - (xSum_ARM * xSum_ARM) / n;
            double Syy_ARM = ysqSum_ARM - (ySum_ARM * ySum_ARM) / n;
            double Sxy_ARM = xySum_ARM - (xSum_ARM * ySum_ARM) / n;

            b_ARM = Sxy_ARM / Sxx_ARM;

            double RSS_ARM = Syy_ARM - (Sxy_ARM * Sxy_ARM) / Sxx_ARM;
            double sigma2_ARM = RSS_ARM / (n - 2);

            sigma_resid_ARM = Math.Sqrt(sigma2_ARM);
            sigma_b_ARM = Math.Sqrt(sigma2_ARM / Sxx_ARM);

            double dx_ARM = xMax_ARM - xMin_ARM;
            beta_ARM = (dx_ARM > 0) ? (sigma_resid_ARM / dx_ARM) : double.NaN;

            intercept_ARM = (ySum_ARM - b_ARM * xSum_ARM) / n;

            //beta_ARM_label.Text = beta_ARM.ToString();

            b_ARM_label.Text = b_ARM.ToString("F4");
            sigmab_ARM_label.Text = sigma_b_ARM.ToString("F4");

            n_ARM_label.Text = n.ToString("F4");

            bAA_ARM_label.Text = Math.Abs(b_ARM).ToString("F4");

            R_ARM_label.Text = R_ARM.ToString("F4");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string error;
            int start, end;

            if (!TryParseRange(points_textBox.Text, out start, out end, out error))
            {
                MessageBox.Show(error);
                return;
            }

            if (start > end)
            {
                int tmp = start;
                start = end;
                end = tmp;
            }

            Calculate(start - 1, end);
            plotFitting();
        }
    }
}
