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

namespace Thellier
{
    public partial class Form3 : Form
    {
        private bool is_pmd;
        private readonly BindingList<MeasurementRow> _stepRows;

        private Form1 _mainForm;

        string file_path;
        public Form3(BindingList<MeasurementRow> stepRows, Form1 mainForm)
        {
            InitializeComponent();
            _stepRows = stepRows;
            _mainForm = mainForm;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void openPMD_toolStripButton_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Select .pmd file";
                fileDialog.Filter = "PMD Files (*.pmd)|*.pmd";

                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadPMD(fileDialog.FileName);

                    file_path = fileDialog.FileName;
                    //MainTable.Refresh();
                    //RefreshMainTable();
                    //pmd_label.Text = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                }
            }
        }

        private void openRMG_toolStripButton_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = "Select .rmg file";
                fileDialog.Filter = "RMG Files (*.rmg)|*.rmg";

                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadRMG(fileDialog.FileName);
                    file_path = fileDialog.FileName;
                    //MainTable.Refresh();
                    //rmg_label.Text = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                }
            }
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
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            wizzardGrid.Rows.Clear();

            try
            {
                string[] lines = File.ReadAllLines(path);

                wizzardGrid.SuspendLayout();

                for (int i = 0; i < lines.Length; i++)
                {
                    int rowIndex = wizzardGrid.Rows.Add(lines[i]);
                    wizzardGrid.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString();
                }

                wizzardGrid.ResumeLayout();
                wizzardGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                is_pmd = true;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Import error"); }
        }

        private void loadRMG(string path)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            wizzardGrid.Rows.Clear();

            try
            {
                string[] lines = File.ReadAllLines(path);

                wizzardGrid.SuspendLayout();

                for (int i = 0; i < lines.Length; i++)
                {
                    int rowIndex = wizzardGrid.Rows.Add(lines[i]);
                    wizzardGrid.Rows[rowIndex].HeaderCell.Value = (rowIndex + 1).ToString();
                }

                wizzardGrid.ResumeLayout();
                wizzardGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

                is_pmd = false;

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Import error"); }
        }

        private List<int> GetRange(string input)
        {
            List<int> range = new List<int>();
            try
            {             
                string[] parts = input.Split(',');

                foreach (string part in parts)
                {
                    if (part.Contains('-'))
                    {
                        string[] sub_part = part.Split('-');

                        for (int i = int.Parse(sub_part[0]); i <= int.Parse(sub_part[1]); i ++)
                            range.Add(i);
                    }
                    else
                    {
                        range.Add(int.Parse(part));
                    }
                }

                return range;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Invalid lines input"); return range; }
        }

        private void export_PMD(List<int> range)
        {
            int fileLineNumber = 1;
            int targetIndex = 0;

            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            try
            {
                string[] lines = File.ReadAllLines(file_path);

                foreach (string rawLine in lines)
                {
                    bool selected = range.Contains(fileLineNumber);
                    fileLineNumber++;

                    if (!selected)
                        continue;

                    if (string.IsNullOrWhiteSpace(rawLine) || rawLine.Length <= 2)
                        continue;

                    string line = NormalizeLine(rawLine);
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length < 4)
                        continue;

                    if (parts[1] == "Xc")
                        continue;

                    double H = ParseCleanDouble(parts[0].Substring(parts[0].IndexOfAny(digits)), provider);
                    double X = ParseCleanDouble(parts[1], provider);
                    double Y = ParseCleanDouble(parts[2], provider);
                    double Z = ParseCleanDouble(parts[3], provider);

                    if (targetIndex >= _stepRows.Count)
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
                        var step = _stepRows[targetIndex];
                        step.H = H;
                        step.X = X;
                        step.Y = Y;
                        step.Z = Z;
                        step.RecalculateNrm();
                    }

                    targetIndex++;
                }

                //_mainForm.RefreshFromImportWizard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Import error");
            }
        }

        private void export_RMG(List<int> range)
        {
            int fileLineNumber = 1;
            int initiall_count = _stepRows.Count;

            List<double> _gainedPoints = new List<double>();
            List<double> _leftPoints = new List<double>();

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            try
            {
                string[] lines = File.ReadAllLines(file_path);

                foreach (string rawLine in lines)
                {
                    if (!range.Contains(fileLineNumber))
                    {
                        fileLineNumber++;
                        continue;
                    }

                    fileLineNumber++;

                    if (string.IsNullOrWhiteSpace(rawLine) || rawLine.Length <= 2)
                        continue;

                    string line = NormalizeLine(rawLine);
                    var parts = line.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Where(p => p.Any(char.IsLetterOrDigit))
                                    .ToArray();

                    if (parts.Length <= 5)
                        continue;

                    if (parts[0].StartsWith("Instrument:", StringComparison.OrdinalIgnoreCase) ||
                        parts[0].StartsWith("Time:", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (parts[0].StartsWith("NRM", StringComparison.OrdinalIgnoreCase) ||
                        parts[0].StartsWith("AFmax", StringComparison.OrdinalIgnoreCase))
                    {
                        _gainedPoints.Add(ParseCleanDouble(parts[5], provider));
                        continue;
                    }

                    if (parts[0].StartsWith("ARM", StringComparison.OrdinalIgnoreCase))
                    {
                        _gainedPoints.Add(ParseCleanDouble(parts[5], provider));
                        continue;
                    }

                    if (parts[0].StartsWith("AF", StringComparison.OrdinalIgnoreCase) ||
                        parts[0].StartsWith("AFz", StringComparison.OrdinalIgnoreCase))
                    {
                        _leftPoints.Add(ParseCleanDouble(parts[5], provider));
                        continue;
                    }
                }

                if (_gainedPoints.Count == 0)
                    throw new InvalidOperationException("No valid RMG data found in selected lines.");

                if (_leftPoints.Count != _gainedPoints.Count - 1)
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
                            ARMGained = _gainedPoints[i + 1],
                            ARMLeft = _leftPoints[i]
                        };

                        _stepRows.Add(step);
                    }
                }
                else
                {
                    if (_stepRows.Count < _gainedPoints.Count)
                        throw new InvalidOperationException("Not enough rows in the main table for selected RMG data.");

                    _stepRows[0].ARMGained = _gainedPoints[0];
                    _stepRows[0].ARMLeft = _gainedPoints.Last();

                    for (int i = 1; i < _gainedPoints.Count; i++)
                    {
                        _stepRows[i].ARMGained = _gainedPoints[i];
                        _stepRows[i].ARMLeft = _leftPoints[i - 1];
                    }
                }

                //_mainForm.RefreshFromImportWizard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Import error");
            }
        }

        private void add_button_Click(object sender, EventArgs e)
        {            
            string input = Rang_textBox.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }
            List<int> range = new List<int>();
            range = GetRange(input);

            if(is_pmd)
            { export_PMD(range); }
            else { export_RMG(range); }

            _mainForm.RefreshFromImportWizard();
        }
    }
}
