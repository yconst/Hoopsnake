namespace HoopSnake
{
    partial class HSCPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HSCPanel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Looper = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.console = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewImageColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Msg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.toolStripEx1 = new System.Windows.Forms.ToolStripEx();
            this.StopButton = new System.Windows.Forms.ToolStripButton();
            this.LoopAllButton = new System.Windows.Forms.ToolStripButton();
            this.ResetAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.UpdateViewButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.HSCombo = new System.Windows.Forms.ToolStripComboBox();
            this.StepButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.delayBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console)).BeginInit();
            this.toolStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Looper,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 319);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Looper
            // 
            this.Looper.Image = ((System.Drawing.Image)(resources.GetObject("Looper.Image")));
            this.Looper.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.Looper.Name = "Looper";
            this.Looper.Size = new System.Drawing.Size(16, 17);
            this.Looper.Visible = false;
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(42, 17);
            this.statusLabel.Text = "Ready.";
            // 
            // console
            // 
            this.console.AllowUserToAddRows = false;
            this.console.AllowUserToDeleteRows = false;
            this.console.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.console.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.console.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.console.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.console.ColumnHeadersVisible = false;
            this.console.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.From,
            this.Msg});
            this.console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console.Location = new System.Drawing.Point(0, 26);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.RowHeadersVisible = false;
            this.console.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.console.Size = new System.Drawing.Size(499, 293);
            this.console.TabIndex = 7;
            this.console.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Type
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.Type.DefaultCellStyle = dataGridViewCellStyle1;
            this.Type.HeaderText = "Type";
            this.Type.Image = global::HoopSnake.Properties.Resources.DOT;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Type.Width = 20;
            // 
            // From
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.From.DefaultCellStyle = dataGridViewCellStyle2;
            this.From.HeaderText = "From";
            this.From.Name = "From";
            this.From.ReadOnly = true;
            this.From.ToolTipText = "Message Source";
            this.From.Width = 40;
            // 
            // Msg
            // 
            this.Msg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Msg.DefaultCellStyle = dataGridViewCellStyle3;
            this.Msg.HeaderText = "Message";
            this.Msg.Name = "Msg";
            this.Msg.ReadOnly = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // dataGridViewImageColumn1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewImageColumn1.HeaderText = "Type";
            this.dataGridViewImageColumn1.Image = global::HoopSnake.Properties.Resources.DOT;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn1.Width = 20;
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.AutoSize = false;
            this.toolStripEx1.ClickThrough = true;
            this.toolStripEx1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StopButton,
            this.LoopAllButton,
            this.ResetAllButton,
            this.toolStripSeparator3,
            this.UpdateViewButton,
            this.toolStripSeparator2,
            this.HSCombo,
            this.StepButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.delayBox});
            this.toolStripEx1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripEx1.Size = new System.Drawing.Size(499, 26);
            this.toolStripEx1.Stretch = true;
            this.toolStripEx1.TabIndex = 2;
            this.toolStripEx1.Text = "toolStrip1";
            this.toolStripEx1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripEx1_ItemClicked);
            // 
            // StopButton
            // 
            this.StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopButton.Image = global::HoopSnake.Properties.Resources.STOP;
            this.StopButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(23, 23);
            this.StopButton.Text = "Stop!";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // LoopAllButton
            // 
            this.LoopAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoopAllButton.Image = ((System.Drawing.Image)(resources.GetObject("LoopAllButton.Image")));
            this.LoopAllButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LoopAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoopAllButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.LoopAllButton.Name = "LoopAllButton";
            this.LoopAllButton.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.LoopAllButton.Size = new System.Drawing.Size(23, 23);
            this.LoopAllButton.Text = "Loop All";
            this.LoopAllButton.Click += new System.EventHandler(this.LoopAllButton_Click);
            // 
            // ResetAllButton
            // 
            this.ResetAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetAllButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetAllButton.Image")));
            this.ResetAllButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ResetAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetAllButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.ResetAllButton.Name = "ResetAllButton";
            this.ResetAllButton.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.ResetAllButton.Size = new System.Drawing.Size(23, 23);
            this.ResetAllButton.Text = "Reset All";
            this.ResetAllButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // UpdateViewButton
            // 
            this.UpdateViewButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.UpdateViewButton.Checked = true;
            this.UpdateViewButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UpdateViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UpdateViewButton.Image = ((System.Drawing.Image)(resources.GetObject("UpdateViewButton.Image")));
            this.UpdateViewButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.UpdateViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UpdateViewButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.UpdateViewButton.Name = "UpdateViewButton";
            this.UpdateViewButton.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.UpdateViewButton.Size = new System.Drawing.Size(31, 23);
            this.UpdateViewButton.Text = "Update Rhino View?";
            this.UpdateViewButton.Click += new System.EventHandler(this.UpdateViewButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // HSCombo
            // 
            this.HSCombo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.HSCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HSCombo.Name = "HSCombo";
            this.HSCombo.Size = new System.Drawing.Size(108, 26);
            // 
            // StepButton
            // 
            this.StepButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StepButton.Image = ((System.Drawing.Image)(resources.GetObject("StepButton.Image")));
            this.StepButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StepButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 2);
            this.StepButton.Name = "StepButton";
            this.StepButton.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.StepButton.Size = new System.Drawing.Size(23, 23);
            this.StepButton.Text = "Step";
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(38, 23);
            this.toolStripLabel1.Text = "Delay";
            // 
            // delayBox
            // 
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(80, 26);
            this.delayBox.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // HSCPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(499, 341);
            this.Controls.Add(this.console);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 106);
            this.Name = "HSCPanel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "HoopSnake";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Panel_Closing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.console)).EndInit();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripEx toolStripEx1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.DataGridView console;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn From;
        private System.Windows.Forms.DataGridViewTextBoxColumn Msg;
        private System.Windows.Forms.ToolStripStatusLabel Looper;
        private System.Windows.Forms.ToolStripButton StopButton;
        private System.Windows.Forms.ToolStripButton LoopAllButton;
        private System.Windows.Forms.ToolStripButton ResetAllButton;
        private System.Windows.Forms.ToolStripButton UpdateViewButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton StepButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripComboBox HSCombo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox delayBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

    }
}