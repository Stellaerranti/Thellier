using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Thellier
{
    public partial class Form1 : Form
    {

        private readonly BindingList<MeasurementRow> _stepRows = new BindingList<MeasurementRow>();

        private Form2 _form2;
        private Form3 _form3;

        private readonly FileContext _fileContext = new FileContext();
        public Form1()
        {
            InitializeComponent();
            MainTable.RowPostPaint += MainTable_RowPostPaint;
        }

        private void BindMainTable()
        {
            MainTable.AutoGenerateColumns = false;
            MainTable.DataSource = _stepRows;
        }

        private void RefreshMainTable()
        {
            var currencyManager = (CurrencyManager)BindingContext[MainTable.DataSource];
            currencyManager.Refresh();
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
            BindMainTable();

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

            MainTable.Columns[0].DataPropertyName = "H";
            MainTable.Columns[1].DataPropertyName = "X";
            MainTable.Columns[2].DataPropertyName = "Y";
            MainTable.Columns[3].DataPropertyName = "Z";
            MainTable.Columns[4].DataPropertyName = "NRM";
            MainTable.Columns[5].DataPropertyName = "ARMGained";
            MainTable.Columns[6].DataPropertyName = "ARMLeft";
        }

        private void DeleteSelectedRows()
        {
            if (MainTable.SelectedCells.Count == 0 && MainTable.CurrentCell == null)
                return;

            var rowIndexes = new HashSet<int>();

            foreach (DataGridViewCell cell in MainTable.SelectedCells)
            {
                if (cell.RowIndex >= 0 && !MainTable.Rows[cell.RowIndex].IsNewRow)
                    rowIndexes.Add(cell.RowIndex);
            }

            if (rowIndexes.Count == 0 && MainTable.CurrentCell != null)
            {
                int r = MainTable.CurrentCell.RowIndex;
                if (r >= 0 && !MainTable.Rows[r].IsNewRow)
                    rowIndexes.Add(r);
            }

            if (rowIndexes.Count == 0)
                return;

            if (MessageBox.Show("Delete selected rows?", "Confirm",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            foreach (int r in rowIndexes.OrderByDescending(i => i))
            {
                MainTable.Rows.RemoveAt(r);
            }
            plotRMG();
            plotNRM();

            if (_form2 != null && !_form2.IsDisposed)
            {
                _form2.RefreshFromMain();
            }
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
            demagChart.Series["Demag"].Points.Clear();
            ZiChart.Series["YX"].Points.Clear();
            ZiChart.Series["YmZ"].Points.Clear();
            ZiChart.Series["YmX"].Points.Clear();
            ZiChart.Series["ZX"].Points.Clear();

            for (int i = 0; i < _stepRows.Count; i++)
            {
                var row = _stepRows[i];
                string label = (i + 1).ToString();

                int p1 = demagChart.Series["Demag"].Points.AddXY(row.H, row.NRM);
                demagChart.Series["Demag"].Points[p1].Label = label;

                int p2 = ZiChart.Series["YX"].Points.AddXY(row.Y, row.X);
                ZiChart.Series["YX"].Points[p2].Label = label;

                int p3 = ZiChart.Series["YmZ"].Points.AddXY(-row.Z, row.X);
                ZiChart.Series["YmZ"].Points[p3].Label = label;

                int p4 = ZiChart.Series["YmX"].Points.AddXY(row.Y, row.X);
                ZiChart.Series["YmX"].Points[p4].Label = label;

                int p5 = ZiChart.Series["ZX"].Points.AddXY(row.Y, -row.Z);
                ZiChart.Series["ZX"].Points[p5].Label = label;
            }

            AutoAxis(demagChart, 0);

            if (demagChart.ChartAreas[0].AxisX.Minimum < 0) demagChart.ChartAreas[0].AxisX.Minimum = 0;
            if (demagChart.ChartAreas[0].AxisY.Minimum < 0) demagChart.ChartAreas[0].AxisY.Minimum = 0;

            AutoAxis(ZiChart, 0);
            AutoAxis(ZiChart, 1);
        }


        private void plotRMG()
        {
            ARMChart.Series[0].Points.Clear();
            ARMChart.Series[1].Points.Clear();

            for (int i = 0; i < _stepRows.Count; i++)
            {
                var row = _stepRows[i];
                string label = (i+1).ToString();

                int p0 = ARMChart.Series[0].Points.AddXY(row.H, row.ARMGained);
                ARMChart.Series[0].Points[p0].Label = label;

                int p1 = ARMChart.Series[1].Points.AddXY(row.H, row.ARMLeft);
                ARMChart.Series[1].Points[p1].Label = label;
            }

            AutoAxis(ARMChart, 0);

            if (ARMChart.ChartAreas[0].AxisX.Minimum < 0) { ARMChart.ChartAreas[0].AxisX.Minimum = 0; }
        }

        private static string NormalizeLine(string line)
        {
            while (line.Contains("  ")) line = line.Replace("  ", " ");
            while (line.Contains("\t\t")) line = line.Replace("\t\t", "\t");
            while (line.Contains("\t ")) line = line.Replace("\t ", "\t");
            while (line.Contains(" \t")) line = line.Replace(" \t", "\t");
            line = line.Replace(",", ".");
            return line.Trim();
        }

        private static double ParseCleanDouble(string input, NumberFormatInfo provider)
        {
            string token = input.Trim();
            token = token.TrimStart('.', ',', ';', ':');

            return double.Parse(token, NumberStyles.Float, provider);
        }

        private void loadPMD(string path)
        {
            int ni = 0;
            int initiall_count = _stepRows.Count;

            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            try
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string rawLine in lines.Skip(2))
                {
                    if (string.IsNullOrWhiteSpace(rawLine) || rawLine.Length <= 2)
                        continue;

                    string line = NormalizeLine(rawLine);
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts[1] == "Xc")
                        continue;

                    double H = 0, X = 0, Y = 0, Z = 0;

                    H = ParseCleanDouble(parts[0].Substring(parts[0].IndexOfAny(digits)), provider);
                    X = ParseCleanDouble(parts[1], provider);
                    Y = ParseCleanDouble(parts[2], provider);
                    Z = ParseCleanDouble(parts[3], provider);

                    if (initiall_count == 0)
                    {
                        var step = new MeasurementRow
                        {
                            H = H,
                            X = X,
                            Y = Y,
                            Z = Z
                        };

                        step.RecalculateNrm();

                        _stepRows.Add(step);
                    }
                    else
                    {
                        var step = _stepRows[ni];

                        step.H = H; step.X = X; step.Y = Y; step.Z = Z;

                        step.RecalculateNrm();
                    }

                    ni++;
                }
                plotNRM();
                plotRMG();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Import error"); }
        }

        private void loadRMG(string path)
        {
            //int ni = 0;
            int initiall_count = _stepRows.Count;

            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            List<double> _gainedPoints = new List<double> { };
            List<double> _leftPoints = new List<double> { };

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            try
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string rawLine in lines.Skip(2))
                {
                    if (string.IsNullOrWhiteSpace(rawLine) || rawLine.Length <= 2)
                        continue;

                    string line = NormalizeLine(rawLine);
                    var parts = line.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Where(p => p.Any(char.IsLetterOrDigit))
                        .ToArray();

                    if (parts[0].StartsWith("Instrument:") || parts[0].StartsWith("Time:"))
                        continue;

                    if (parts[0].StartsWith("NRM") || parts[0].StartsWith("AFmax"))
                    {
                        _gainedPoints.Add(ParseCleanDouble(parts[5], provider)); 
                        continue;
                    }

                    if (parts[0].StartsWith("ARM"))
                    {
                        _gainedPoints.Add(ParseCleanDouble(parts[5], provider));
                        continue;
                    }

                    if (parts[0].StartsWith("AF") || parts[0].StartsWith("AFz"))
                    {
                        _leftPoints.Add(ParseCleanDouble(parts[5], provider));
                        continue;
                    }
                    
                }

                if(_leftPoints.Count != _gainedPoints.Count - 1)
                    throw new InvalidOperationException("RMG file is inconsistent: ARM gained and ARM left counts differ.");
                                
                if (initiall_count == 0)
                {
                    var step_first = new MeasurementRow
                    {
                        ARMGained = _gainedPoints[0],
                        ARMLeft = _gainedPoints.Last()
                    };

                    _stepRows.Add(step_first);

                    for (int i = 0; i < _leftPoints.Count; i++)
                    {
                        var step = new MeasurementRow
                        {
                            ARMGained = _gainedPoints[i+1],
                            ARMLeft = _leftPoints[i]
                        };

                        _stepRows.Add(step);
                    }
                }
                else
                {                  
                    _stepRows[0].ARMGained = _gainedPoints[0];
                    _stepRows[0].ARMLeft = _gainedPoints.Last();

                    for(int i = 1; i < _stepRows.Count; i++)
                    {
                        _stepRows[i].ARMGained = _gainedPoints[i];
                        _stepRows[i].ARMLeft = _leftPoints[i-1];
                    }
                }                

                plotNRM();
                plotRMG();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Import error"); }
        }


        private (int, int, int, int) countRMG(string[] lines)
        {
            int ARMbeg = -1;
            int ARMLength = 0;
            int AFzbeg = -1;
            int AFzLength = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (line.StartsWith("ARM", StringComparison.OrdinalIgnoreCase))
                {
                    if (ARMbeg == -1) ARMbeg = i;
                    ARMLength++;
                }

                else if (line.StartsWith("AF", StringComparison.OrdinalIgnoreCase))
                {
                    if (AFzbeg == -1) AFzbeg = i;
                    AFzLength++;
                }
            }

            return (ARMbeg, ARMLength, AFzbeg, AFzLength);
        }
        private void toolStripButton_openPMD_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Select .pmd file";
                fileDialog.Filter = "PMD Files (*.pmd)|*.pmd";

                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadPMD(fileDialog.FileName);
                    MainTable.Refresh();
                    //RefreshMainTable();
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
                    MainTable.Refresh();
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

                    if (line_number < 0 || line_number >= _stepRows.Count)
                    {
                        MessageBox.Show("The number is outside the valid range.");
                        return;
                    }

                    double ValueToExtract = 0;

                    try
                    {
                        if (NRM_radioButton.Checked)
                        {
                            ValueToExtract = _stepRows[line_number].NRM;
                        }
                        else if(ARMgained_radioButton.Checked)
                        {
                            ValueToExtract = _stepRows[line_number].ARMGained;
                        }
                        else if(ARMleft_radioButton.Checked)
                        {
                            ValueToExtract = _stepRows[line_number].ARMLeft;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid value");
                    }

                    try 
                    {
                        foreach (var step in _stepRows)
                        {
                            if (NRM_checkBox.Checked)
                            {
                                step.NRM = step.NRM - ValueToExtract;
                            }
                            if (ARMgained_checkBox.Checked)
                            {
                                step.ARMGained = step.ARMGained - ValueToExtract;
                            }
                            if (ARMleft_checkBox.Checked)
                            {
                                step.ARMLeft = step.ARMLeft - ValueToExtract;
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Substracting error");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else if (Value_radioButton.Checked)
            {
                try
                {

                    double ValueToExtract = Double.Parse(res_input_textBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture);

                    try
                    {
                        foreach (var step in _stepRows)
                        {
                            if (NRM_checkBox.Checked)
                            {
                                step.NRM = step.NRM - ValueToExtract;
                            }
                            if (ARMgained_checkBox.Checked)
                            {
                                step.ARMGained = step.ARMGained - ValueToExtract;
                            }
                            if (ARMleft_checkBox.Checked)
                            {
                                step.ARMLeft = step.ARMLeft - ValueToExtract;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Substracting error");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else { MessageBox.Show("Please, select initial value type"); }
        }

        private void Remove_residue_button_Click(object sender, EventArgs e)
        {
            RemoveResidue();
            plotTable();
        }

        private void import_wizzard_Button_Click(object sender, EventArgs e)
        {
            if (_form3 == null || _form3.IsDisposed)
            {
                _form3 = new Form3(_stepRows);
                _form3.FormClosing += (s, args) =>
                {
                    args.Cancel = true;
                    _form3.Hide();
                };
            }

            if (_form3.WindowState == FormWindowState.Minimized)
                _form3.WindowState = FormWindowState.Normal;

            _form3.Show();
            _form3.BringToFront();
            _form3.Activate();
        }

        private void button_plot_Click(object sender, EventArgs e)
        {
            if (_form2 == null || _form2.IsDisposed)
            {
                _form2 = new Form2(_stepRows, _fileContext);
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

        private void button_output_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Create output file";
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.InitialDirectory = Application.StartupPath; 
                sfd.FileName = "output.txt";                   

                if (sfd.ShowDialog() != DialogResult.OK)
                    return; 

                string filePath = sfd.FileName;

                //File.WriteAllText(filePath, "initial content");

                _fileContext.FilePath = filePath;

                output_label.Text = Path.GetFileNameWithoutExtension(filePath);

                if (_form2 != null && !_form2.IsDisposed)
                {
                    _form2.RefreshFromContext();
                }
            }
        }

        private void gridContextMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Delete_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedRows();
        }

        private void MainTable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void MainTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = MainTable.HitTest(e.X, e.Y);

                if (hit.Type == DataGridViewHitTestType.Cell &&
                    hit.RowIndex >= 0 && hit.ColumnIndex >= 0)
                {
                    var cell = MainTable[hit.ColumnIndex, hit.RowIndex];

                    if (!cell.Selected)
                    {
                        MainTable.ClearSelection();
                        cell.Selected = true;
                    }

                    MainTable.CurrentCell = cell;
                }
            }
        }

        private void ExportChart(Chart chart, string defaultFileName = "chart")
        {
            if (chart == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Export chart";
                sfd.FileName = defaultFileName;
                sfd.Filter =
                    "PNG Image (*.png)|*.png|" +
                    "JPEG Image (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Bitmap Image (*.bmp)|*.bmp|" +
                    "EMF Vector (*.emf)|*.emf";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                ChartImageFormat format;

                switch (System.IO.Path.GetExtension(sfd.FileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        format = ChartImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ChartImageFormat.Bmp;
                        break;
                    case ".emf":
                        format = ChartImageFormat.Emf;
                        break;
                    default:
                        format = ChartImageFormat.Png;
                        break;
                }

                chart.Refresh();

                chart.SaveImage(sfd.FileName, format);
            }
        }

        private void ziChart1ProjectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportChart(ZiChart, "Zi");
        }

        private void demagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportChart(demagChart, "DEMAG");
        }

        private void aRMARMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportChart(ARMChart, "ARM");
        }
    }
    public class MeasurementRow
    {
        public double H { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double NRM { get; set; }
        public double ARMGained { get; set; }
        public double ARMLeft { get; set; }

        public void RecalculateNrm()
        {
            NRM = Math.Sqrt(X * X + Y * Y + Z * Z) * 1000.0;
        }
    }
    public class FileContext
    { 
        public string FilePath { get; set; }
    }
}
