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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_openPMD = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_openRMG = new System.Windows.Forms.ToolStripButton();
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pmd_label = new System.Windows.Forms.Label();
            this.rmg_label = new System.Windows.Forms.Label();
            this.output_label = new System.Windows.Forms.Label();
            this.button_output = new System.Windows.Forms.Button();
            this.button_plot = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.res_input_textBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.NRM_radioButton = new System.Windows.Forms.RadioButton();
            this.ARMgained_radioButton = new System.Windows.Forms.RadioButton();
            this.ARMleft_radioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.NRM_checkBox = new System.Windows.Forms.CheckBox();
            this.ARMgained_checkBox = new System.Windows.Forms.CheckBox();
            this.ARMleft_checkBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.LineNumber_radioButton = new System.Windows.Forms.RadioButton();
            this.Value_radioButton = new System.Windows.Forms.RadioButton();
            this.Remove_residue_button = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZiChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.demagChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARMChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_openPMD,
            this.toolStripButton_openRMG});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(2674, 42);
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
            // toolStripButton_openRMG
            // 
            this.toolStripButton_openRMG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_openRMG.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_openRMG.Image")));
            this.toolStripButton_openRMG.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_openRMG.Name = "toolStripButton_openRMG";
            this.toolStripButton_openRMG.Size = new System.Drawing.Size(136, 36);
            this.toolStripButton_openRMG.Text = "Open RMG";
            this.toolStripButton_openRMG.Click += new System.EventHandler(this.toolStripButton_openRMG_Click);
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2674, 1087);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ZiChart
            // 
            chartArea5.Name = "proj1";
            chartArea6.Name = "proj2";
            this.ZiChart.ChartAreas.Add(chartArea5);
            this.ZiChart.ChartAreas.Add(chartArea6);
            this.ZiChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZiChart.Location = new System.Drawing.Point(6, 6);
            this.ZiChart.Margin = new System.Windows.Forms.Padding(6);
            this.ZiChart.Name = "ZiChart";
            series8.ChartArea = "proj1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series8.Name = "YX";
            series9.ChartArea = "proj1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series9.Name = "YmZ";
            series10.ChartArea = "proj2";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series10.Name = "YmX";
            series11.ChartArea = "proj2";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series11.Name = "ZX";
            this.ZiChart.Series.Add(series8);
            this.ZiChart.Series.Add(series9);
            this.ZiChart.Series.Add(series10);
            this.ZiChart.Series.Add(series11);
            this.ZiChart.Size = new System.Drawing.Size(790, 531);
            this.ZiChart.TabIndex = 0;
            this.ZiChart.Text = "chart1";
            this.ZiChart.Click += new System.EventHandler(this.ZiChart_Click);
            // 
            // demagChart
            // 
            chartArea7.Name = "ChartArea1";
            this.demagChart.ChartAreas.Add(chartArea7);
            this.demagChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.demagChart.Location = new System.Drawing.Point(808, 6);
            this.demagChart.Margin = new System.Windows.Forms.Padding(6);
            this.demagChart.Name = "demagChart";
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series12.Name = "Demag";
            this.demagChart.Series.Add(series12);
            this.demagChart.Size = new System.Drawing.Size(790, 531);
            this.demagChart.TabIndex = 1;
            this.demagChart.Text = "chart1";
            // 
            // ARMChart
            // 
            chartArea8.Name = "ChartArea1";
            this.ARMChart.ChartAreas.Add(chartArea8);
            this.ARMChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.ARMChart.Legends.Add(legend2);
            this.ARMChart.Location = new System.Drawing.Point(1610, 6);
            this.ARMChart.Margin = new System.Windows.Forms.Padding(6);
            this.ARMChart.Name = "ARMChart";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series13.Legend = "Legend1";
            series13.Name = "ARM gained";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series14.Legend = "Legend1";
            series14.Name = "ARM left";
            this.ARMChart.Series.Add(series13);
            this.ARMChart.Series.Add(series14);
            this.ARMChart.Size = new System.Drawing.Size(790, 531);
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
            this.MainTable.Location = new System.Drawing.Point(6, 549);
            this.MainTable.Margin = new System.Windows.Forms.Padding(6);
            this.MainTable.Name = "MainTable";
            this.MainTable.RowHeadersVisible = false;
            this.MainTable.RowHeadersWidth = 82;
            this.MainTable.Size = new System.Drawing.Size(1592, 532);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.pmd_label, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rmg_label, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.output_label, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.button_output, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.button_plot, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2409, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(262, 220);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "NRM file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 51);
            this.label2.TabIndex = 1;
            this.label2.Text = "ARM file";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(6, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 51);
            this.label3.TabIndex = 2;
            this.label3.Text = "Output:";
            // 
            // pmd_label
            // 
            this.pmd_label.AutoSize = true;
            this.pmd_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pmd_label.Location = new System.Drawing.Point(135, 3);
            this.pmd_label.Name = "pmd_label";
            this.pmd_label.Size = new System.Drawing.Size(121, 51);
            this.pmd_label.TabIndex = 3;
            this.pmd_label.Text = "-";
            // 
            // rmg_label
            // 
            this.rmg_label.AutoSize = true;
            this.rmg_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rmg_label.Location = new System.Drawing.Point(135, 57);
            this.rmg_label.Name = "rmg_label";
            this.rmg_label.Size = new System.Drawing.Size(121, 51);
            this.rmg_label.TabIndex = 4;
            this.rmg_label.Text = "-";
            // 
            // output_label
            // 
            this.output_label.AutoSize = true;
            this.output_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output_label.Location = new System.Drawing.Point(135, 111);
            this.output_label.Name = "output_label";
            this.output_label.Size = new System.Drawing.Size(121, 51);
            this.output_label.TabIndex = 5;
            this.output_label.Text = "-";
            // 
            // button_output
            // 
            this.button_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_output.Location = new System.Drawing.Point(6, 168);
            this.button_output.Name = "button_output";
            this.button_output.Size = new System.Drawing.Size(120, 46);
            this.button_output.TabIndex = 6;
            this.button_output.Text = "Output";
            this.button_output.UseVisualStyleBackColor = true;
            // 
            // button_plot
            // 
            this.button_plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_plot.Location = new System.Drawing.Point(135, 168);
            this.button_plot.Name = "button_plot";
            this.button_plot.Size = new System.Drawing.Size(121, 46);
            this.button_plot.TabIndex = 7;
            this.button_plot.Text = "Plot";
            this.button_plot.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Location = new System.Drawing.Point(1607, 546);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(796, 307);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unmagnetized residue";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.res_input_textBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.Remove_residue_button, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(790, 277);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // res_input_textBox
            // 
            this.res_input_textBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.res_input_textBox.Location = new System.Drawing.Point(3, 19);
            this.res_input_textBox.Name = "res_input_textBox";
            this.res_input_textBox.Size = new System.Drawing.Size(580, 31);
            this.res_input_textBox.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.NRM_radioButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ARMgained_radioButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ARMleft_radioButton, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 141);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(784, 63);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // NRM_radioButton
            // 
            this.NRM_radioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.NRM_radioButton.AutoSize = true;
            this.NRM_radioButton.Location = new System.Drawing.Point(85, 5);
            this.NRM_radioButton.Name = "NRM_radioButton";
            this.NRM_radioButton.Size = new System.Drawing.Size(91, 53);
            this.NRM_radioButton.TabIndex = 0;
            this.NRM_radioButton.Text = "NRM";
            this.NRM_radioButton.UseVisualStyleBackColor = true;
            // 
            // ARMgained_radioButton
            // 
            this.ARMgained_radioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ARMgained_radioButton.AutoSize = true;
            this.ARMgained_radioButton.Checked = true;
            this.ARMgained_radioButton.Location = new System.Drawing.Point(310, 5);
            this.ARMgained_radioButton.Name = "ARMgained_radioButton";
            this.ARMgained_radioButton.Size = new System.Drawing.Size(161, 53);
            this.ARMgained_radioButton.TabIndex = 1;
            this.ARMgained_radioButton.TabStop = true;
            this.ARMgained_radioButton.Text = "ARM gained";
            this.ARMgained_radioButton.UseVisualStyleBackColor = true;
            // 
            // ARMleft_radioButton
            // 
            this.ARMleft_radioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ARMleft_radioButton.AutoSize = true;
            this.ARMleft_radioButton.Location = new System.Drawing.Point(589, 5);
            this.ARMleft_radioButton.Name = "ARMleft_radioButton";
            this.ARMleft_radioButton.Size = new System.Drawing.Size(125, 53);
            this.ARMleft_radioButton.TabIndex = 2;
            this.ARMleft_radioButton.TabStop = true;
            this.ARMleft_radioButton.Text = "ARM left";
            this.ARMleft_radioButton.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.NRM_checkBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.ARMgained_checkBox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.ARMleft_checkBox, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 210);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(784, 64);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // NRM_checkBox
            // 
            this.NRM_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.NRM_checkBox.AutoSize = true;
            this.NRM_checkBox.Checked = true;
            this.NRM_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NRM_checkBox.Location = new System.Drawing.Point(85, 5);
            this.NRM_checkBox.Name = "NRM_checkBox";
            this.NRM_checkBox.Size = new System.Drawing.Size(92, 54);
            this.NRM_checkBox.TabIndex = 0;
            this.NRM_checkBox.Text = "NRM";
            this.NRM_checkBox.UseVisualStyleBackColor = true;
            // 
            // ARMgained_checkBox
            // 
            this.ARMgained_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ARMgained_checkBox.AutoSize = true;
            this.ARMgained_checkBox.Checked = true;
            this.ARMgained_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ARMgained_checkBox.Location = new System.Drawing.Point(310, 5);
            this.ARMgained_checkBox.Name = "ARMgained_checkBox";
            this.ARMgained_checkBox.Size = new System.Drawing.Size(162, 54);
            this.ARMgained_checkBox.TabIndex = 1;
            this.ARMgained_checkBox.Text = "ARM gained";
            this.ARMgained_checkBox.UseVisualStyleBackColor = true;
            // 
            // ARMleft_checkBox
            // 
            this.ARMleft_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ARMleft_checkBox.AutoSize = true;
            this.ARMleft_checkBox.Checked = true;
            this.ARMleft_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ARMleft_checkBox.Location = new System.Drawing.Point(589, 5);
            this.ARMleft_checkBox.Name = "ARMleft_checkBox";
            this.ARMleft_checkBox.Size = new System.Drawing.Size(126, 54);
            this.ARMleft_checkBox.TabIndex = 2;
            this.ARMleft_checkBox.Text = "ARM left";
            this.ARMleft_checkBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.LineNumber_radioButton, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.Value_radioButton, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(586, 63);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // LineNumber_radioButton
            // 
            this.LineNumber_radioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.LineNumber_radioButton.AutoSize = true;
            this.LineNumber_radioButton.Checked = true;
            this.LineNumber_radioButton.Location = new System.Drawing.Point(66, 5);
            this.LineNumber_radioButton.Name = "LineNumber_radioButton";
            this.LineNumber_radioButton.Size = new System.Drawing.Size(162, 53);
            this.LineNumber_radioButton.TabIndex = 0;
            this.LineNumber_radioButton.TabStop = true;
            this.LineNumber_radioButton.Text = "Line number";
            this.LineNumber_radioButton.UseVisualStyleBackColor = true;
            // 
            // Value_radioButton
            // 
            this.Value_radioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Value_radioButton.AutoSize = true;
            this.Value_radioButton.Location = new System.Drawing.Point(390, 5);
            this.Value_radioButton.Name = "Value_radioButton";
            this.Value_radioButton.Size = new System.Drawing.Size(98, 53);
            this.Value_radioButton.TabIndex = 1;
            this.Value_radioButton.TabStop = true;
            this.Value_radioButton.Text = "Value";
            this.Value_radioButton.UseVisualStyleBackColor = true;
            // 
            // Remove_residue_button
            // 
            this.Remove_residue_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Remove_residue_button.Location = new System.Drawing.Point(595, 72);
            this.Remove_residue_button.Name = "Remove_residue_button";
            this.Remove_residue_button.Size = new System.Drawing.Size(192, 63);
            this.Remove_residue_button.TabIndex = 4;
            this.Remove_residue_button.Text = "Remove residue";
            this.Remove_residue_button.UseVisualStyleBackColor = true;
            this.Remove_residue_button.Click += new System.EventHandler(this.Remove_residue_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2674, 1129);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thellier";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZiChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.demagChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ARMChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainTable)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton toolStripButton_openRMG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pmd_label;
        private System.Windows.Forms.Label rmg_label;
        private System.Windows.Forms.Label output_label;
        private System.Windows.Forms.Button button_output;
        private System.Windows.Forms.Button button_plot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox res_input_textBox;
        private System.Windows.Forms.RadioButton NRM_radioButton;
        private System.Windows.Forms.RadioButton ARMgained_radioButton;
        private System.Windows.Forms.RadioButton ARMleft_radioButton;
        private System.Windows.Forms.CheckBox NRM_checkBox;
        private System.Windows.Forms.CheckBox ARMgained_checkBox;
        private System.Windows.Forms.CheckBox ARMleft_checkBox;
        private System.Windows.Forms.RadioButton LineNumber_radioButton;
        private System.Windows.Forms.RadioButton Value_radioButton;
        private System.Windows.Forms.Button Remove_residue_button;
    }
}

