namespace Thellier
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_openPMD = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ZiChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.demagChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ARMChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.MainTable = new System.Windows.Forms.DataGridView();
            this.H = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NRM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARMG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ARML = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZiChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.demagChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARMChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_openPMD});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(2510, 42);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_openPMD
            // 
            this.toolStripButton_openPMD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_openPMD.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_openPMD.Image")));
            this.toolStripButton_openPMD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_openPMD.Name = "toolStripButton_openPMD";
            this.toolStripButton_openPMD.Size = new System.Drawing.Size(136, 36);
            this.toolStripButton_openPMD.Text = "Open PMD";
            this.toolStripButton_openPMD.Click += new System.EventHandler(this.toolStripButton_openPMD_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.ZiChart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.demagChart, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ARMChart, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.MainTable, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2510, 1204);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ZiChart
            // 
            chartArea1.Name = "proj1";
            chartArea2.Name = "proj2";
            this.ZiChart.ChartAreas.Add(chartArea1);
            this.ZiChart.ChartAreas.Add(chartArea2);
            this.ZiChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZiChart.Location = new System.Drawing.Point(6, 6);
            this.ZiChart.Margin = new System.Windows.Forms.Padding(6);
            this.ZiChart.Name = "ZiChart";
            series1.ChartArea = "proj1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Name = "YX";
            series2.ChartArea = "proj1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Name = "YmZ";
            series3.ChartArea = "proj2";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Name = "YmX";
            series4.ChartArea = "proj2";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.Name = "ZX";
            this.ZiChart.Series.Add(series1);
            this.ZiChart.Series.Add(series2);
            this.ZiChart.Series.Add(series3);
            this.ZiChart.Series.Add(series4);
            this.ZiChart.Size = new System.Drawing.Size(741, 590);
            this.ZiChart.TabIndex = 0;
            this.ZiChart.Text = "chart1";
            // 
            // demagChart
            // 
            chartArea3.Name = "ChartArea1";
            this.demagChart.ChartAreas.Add(chartArea3);
            this.demagChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.demagChart.Location = new System.Drawing.Point(759, 6);
            this.demagChart.Margin = new System.Windows.Forms.Padding(6);
            this.demagChart.Name = "demagChart";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series5.Name = "Demag";
            this.demagChart.Series.Add(series5);
            this.demagChart.Size = new System.Drawing.Size(741, 590);
            this.demagChart.TabIndex = 1;
            this.demagChart.Text = "chart1";
            // 
            // ARMChart
            // 
            chartArea4.Name = "ChartArea1";
            this.ARMChart.ChartAreas.Add(chartArea4);
            this.ARMChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.ARMChart.Legends.Add(legend1);
            this.ARMChart.Location = new System.Drawing.Point(1512, 6);
            this.ARMChart.Margin = new System.Windows.Forms.Padding(6);
            this.ARMChart.Name = "ARMChart";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.ARMChart.Series.Add(series6);
            this.ARMChart.Size = new System.Drawing.Size(741, 590);
            this.ARMChart.TabIndex = 2;
            this.ARMChart.Text = "chart1";
            // 
            // MainTable
            // 
            this.MainTable.AllowUserToAddRows = false;
            this.MainTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.H,
            this.X,
            this.Y,
            this.Z,
            this.NRM,
            this.ARMG,
            this.ARML});
            this.tableLayoutPanel1.SetColumnSpan(this.MainTable, 2);
            this.MainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTable.Location = new System.Drawing.Point(6, 608);
            this.MainTable.Margin = new System.Windows.Forms.Padding(6);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowHeadersVisible = false;
            this.MainTable.RowHeadersWidth = 82;
            this.MainTable.Size = new System.Drawing.Size(1494, 590);
            this.MainTable.TabIndex = 3;
            // 
            // H
            // 
            this.H.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.H.HeaderText = "H";
            this.H.MinimumWidth = 10;
            this.H.Name = "H";
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.HeaderText = "X";
            this.X.MinimumWidth = 10;
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.HeaderText = "Y";
            this.Y.MinimumWidth = 10;
            this.Y.Name = "Y";
            // 
            // Z
            // 
            this.Z.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Z.HeaderText = "Z";
            this.Z.MinimumWidth = 10;
            this.Z.Name = "Z";
            // 
            // NRM
            // 
            this.NRM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NRM.HeaderText = "NRM";
            this.NRM.MinimumWidth = 10;
            this.NRM.Name = "NRM";
            // 
            // ARMG
            // 
            this.ARMG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ARMG.HeaderText = "ARM gained";
            this.ARMG.MinimumWidth = 10;
            this.ARMG.Name = "ARMG";
            // 
            // ARML
            // 
            this.ARML.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ARML.HeaderText = "AMR left";
            this.ARML.MinimumWidth = 10;
            this.ARML.Name = "ARML";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2510, 1246);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Thellier";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZiChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.demagChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARMChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart ZiChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart demagChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart ARMChart;
        private System.Windows.Forms.DataGridView MainTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn H;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn NRM;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARMG;
        private System.Windows.Forms.DataGridViewTextBoxColumn ARML;
        private System.Windows.Forms.ToolStripButton toolStripButton_openPMD;
    }
}

