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
        private readonly BindingList<MeasurementRow> _stepRows;
        public Form3(BindingList<MeasurementRow> stepRows)
        {
            InitializeComponent();
            _stepRows = stepRows;
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
                    //loadPMD(fileDialog.FileName);
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
                    //MainTable.Refresh();
                    //rmg_label.Text = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                }
            }
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

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Import error"); }
        }
    }
}
