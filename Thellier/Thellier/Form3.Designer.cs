namespace Thellier
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.wizzardGrid = new System.Windows.Forms.DataGridView();
            this.fileRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openPMD_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openRMG_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wizzardGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPMD_toolStripButton,
            this.openRMG_toolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(962, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.wizzardGrid, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(962, 540);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // wizzardGrid
            // 
            this.wizzardGrid.AllowUserToAddRows = false;
            this.wizzardGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.wizzardGrid.ColumnHeadersVisible = false;
            this.wizzardGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileRow});
            this.wizzardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizzardGrid.Location = new System.Drawing.Point(3, 3);
            this.wizzardGrid.Name = "wizzardGrid";
            this.wizzardGrid.Size = new System.Drawing.Size(715, 534);
            this.wizzardGrid.TabIndex = 0;
            // 
            // fileRow
            // 
            this.fileRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileRow.HeaderText = "Row";
            this.fileRow.Name = "fileRow";
            this.fileRow.ReadOnly = true;
            // 
            // openPMD_toolStripButton
            // 
            this.openPMD_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openPMD_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openPMD_toolStripButton.Image")));
            this.openPMD_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openPMD_toolStripButton.Name = "openPMD_toolStripButton";
            this.openPMD_toolStripButton.Size = new System.Drawing.Size(69, 22);
            this.openPMD_toolStripButton.Text = "Open PMD";
            this.openPMD_toolStripButton.Click += new System.EventHandler(this.openPMD_toolStripButton_Click);
            // 
            // openRMG_toolStripButton
            // 
            this.openRMG_toolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openRMG_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openRMG_toolStripButton.Image")));
            this.openRMG_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openRMG_toolStripButton.Name = "openRMG_toolStripButton";
            this.openRMG_toolStripButton.Size = new System.Drawing.Size(69, 22);
            this.openRMG_toolStripButton.Text = "Open RMG";
            this.openRMG_toolStripButton.Click += new System.EventHandler(this.openRMG_toolStripButton_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wizzardGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView wizzardGrid;
        private System.Windows.Forms.ToolStripButton openPMD_toolStripButton;
        private System.Windows.Forms.ToolStripButton openRMG_toolStripButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileRow;
    }
}