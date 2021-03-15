namespace FontMaster
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBoxPreview = new System.Windows.Forms.GroupBox();
            this.panelShow = new System.Windows.Forms.Panel();
            this.pictureBoxDraw = new System.Windows.Forms.PictureBox();
            this.groupBoxFont = new System.Windows.Forms.GroupBox();
            this.labelImport = new System.Windows.Forms.Label();
            this.labelExport = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.numericUpDownCode = new System.Windows.Forms.NumericUpDown();
            this.labelCode = new System.Windows.Forms.Label();
            this.textBoxChar = new System.Windows.Forms.TextBox();
            this.labelChar = new System.Windows.Forms.Label();
            this.progressBarMake = new System.Windows.Forms.ProgressBar();
            this.buttonStart = new System.Windows.Forms.Button();
            this.propertyGridFont = new System.Windows.Forms.PropertyGrid();
            this.timerScan = new System.Windows.Forms.Timer(this.components);
            this.splitContainerBack = new System.Windows.Forms.SplitContainer();
            this.status_bottom = new System.Windows.Forms.StatusStrip();
            this.slab_info = new System.Windows.Forms.ToolStripStatusLabel();
            this.slab_section = new System.Windows.Forms.ToolStripStatusLabel();
            this.slab_count = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxPreview.SuspendLayout();
            this.panelShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDraw)).BeginInit();
            this.groupBoxFont.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBack)).BeginInit();
            this.splitContainerBack.Panel1.SuspendLayout();
            this.splitContainerBack.Panel2.SuspendLayout();
            this.splitContainerBack.SuspendLayout();
            this.status_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxPreview
            // 
            this.groupBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPreview.Controls.Add(this.panelShow);
            this.groupBoxPreview.Location = new System.Drawing.Point(4, 4);
            this.groupBoxPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxPreview.Name = "groupBoxPreview";
            this.groupBoxPreview.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxPreview.Size = new System.Drawing.Size(790, 661);
            this.groupBoxPreview.TabIndex = 0;
            this.groupBoxPreview.TabStop = false;
            this.groupBoxPreview.Text = "预览";
            // 
            // panelShow
            // 
            this.panelShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShow.Controls.Add(this.pictureBoxDraw);
            this.panelShow.Location = new System.Drawing.Point(8, 25);
            this.panelShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelShow.Name = "panelShow";
            this.panelShow.Size = new System.Drawing.Size(774, 629);
            this.panelShow.TabIndex = 0;
            // 
            // pictureBoxDraw
            // 
            this.pictureBoxDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDraw.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxDraw.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxDraw.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxDraw.Name = "pictureBoxDraw";
            this.pictureBoxDraw.Size = new System.Drawing.Size(774, 629);
            this.pictureBoxDraw.TabIndex = 0;
            this.pictureBoxDraw.TabStop = false;
            this.pictureBoxDraw.SizeChanged += new System.EventHandler(this.pictureBoxDraw_SizeChanged);
            this.pictureBoxDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDraw_MouseDown);
            // 
            // groupBoxFont
            // 
            this.groupBoxFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFont.Controls.Add(this.labelImport);
            this.groupBoxFont.Controls.Add(this.labelExport);
            this.groupBoxFont.Controls.Add(this.labelHelp);
            this.groupBoxFont.Controls.Add(this.numericUpDownCode);
            this.groupBoxFont.Controls.Add(this.labelCode);
            this.groupBoxFont.Controls.Add(this.textBoxChar);
            this.groupBoxFont.Controls.Add(this.labelChar);
            this.groupBoxFont.Controls.Add(this.progressBarMake);
            this.groupBoxFont.Controls.Add(this.buttonStart);
            this.groupBoxFont.Controls.Add(this.propertyGridFont);
            this.groupBoxFont.Location = new System.Drawing.Point(4, 4);
            this.groupBoxFont.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxFont.Name = "groupBoxFont";
            this.groupBoxFont.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxFont.Size = new System.Drawing.Size(268, 661);
            this.groupBoxFont.TabIndex = 0;
            this.groupBoxFont.TabStop = false;
            this.groupBoxFont.Text = "字体";
            // 
            // labelImport
            // 
            this.labelImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelImport.AutoSize = true;
            this.labelImport.BackColor = System.Drawing.SystemColors.Control;
            this.labelImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelImport.Location = new System.Drawing.Point(124, 0);
            this.labelImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImport.Name = "labelImport";
            this.labelImport.Size = new System.Drawing.Size(37, 15);
            this.labelImport.TabIndex = 8;
            this.labelImport.Text = "导入";
            // 
            // labelExport
            // 
            this.labelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelExport.AutoSize = true;
            this.labelExport.BackColor = System.Drawing.SystemColors.Control;
            this.labelExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelExport.Location = new System.Drawing.Point(171, 0);
            this.labelExport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExport.Name = "labelExport";
            this.labelExport.Size = new System.Drawing.Size(37, 15);
            this.labelExport.TabIndex = 8;
            this.labelExport.Text = "导出";
            this.labelExport.Click += new System.EventHandler(this.labelExport_Click);
            // 
            // labelHelp
            // 
            this.labelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelp.AutoSize = true;
            this.labelHelp.BackColor = System.Drawing.SystemColors.Control;
            this.labelHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHelp.Location = new System.Drawing.Point(217, 0);
            this.labelHelp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(37, 15);
            this.labelHelp.TabIndex = 7;
            this.labelHelp.Text = "帮助";
            this.labelHelp.Click += new System.EventHandler(this.labelHelp_Click);
            // 
            // numericUpDownCode
            // 
            this.numericUpDownCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownCode.Hexadecimal = true;
            this.numericUpDownCode.Location = new System.Drawing.Point(97, 592);
            this.numericUpDownCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numericUpDownCode.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownCode.Name = "numericUpDownCode";
            this.numericUpDownCode.Size = new System.Drawing.Size(65, 25);
            this.numericUpDownCode.TabIndex = 2;
            this.numericUpDownCode.ValueChanged += new System.EventHandler(this.numericUpDownCode_ValueChanged);
            // 
            // labelCode
            // 
            this.labelCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCode.AutoSize = true;
            this.labelCode.Location = new System.Drawing.Point(43, 595);
            this.labelCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(45, 15);
            this.labelCode.TabIndex = 3;
            this.labelCode.Text = "编码:";
            // 
            // textBoxChar
            // 
            this.textBoxChar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChar.Location = new System.Drawing.Point(64, 591);
            this.textBoxChar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxChar.Name = "textBoxChar";
            this.textBoxChar.Size = new System.Drawing.Size(0, 25);
            this.textBoxChar.TabIndex = 4;
            this.textBoxChar.Text = "迈";
            this.textBoxChar.TextChanged += new System.EventHandler(this.textBoxChar_TextChanged);
            this.textBoxChar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChar_KeyDown);
            this.textBoxChar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxChar_KeyUp);
            this.textBoxChar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.textBoxChar_MouseMove);
            // 
            // labelChar
            // 
            this.labelChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelChar.AutoSize = true;
            this.labelChar.Location = new System.Drawing.Point(9, 595);
            this.labelChar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelChar.Name = "labelChar";
            this.labelChar.Size = new System.Drawing.Size(45, 15);
            this.labelChar.TabIndex = 5;
            this.labelChar.Text = "字符:";
            // 
            // progressBarMake
            // 
            this.progressBarMake.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMake.Location = new System.Drawing.Point(12, 625);
            this.progressBarMake.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBarMake.Name = "progressBarMake";
            this.progressBarMake.Size = new System.Drawing.Size(151, 29);
            this.progressBarMake.TabIndex = 6;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(168, 591);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(92, 62);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "生成字库";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // propertyGridFont
            // 
            this.propertyGridFont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridFont.Location = new System.Drawing.Point(8, 25);
            this.propertyGridFont.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.propertyGridFont.Name = "propertyGridFont";
            this.propertyGridFont.Size = new System.Drawing.Size(252, 559);
            this.propertyGridFont.TabIndex = 0;
            this.propertyGridFont.ToolbarVisible = false;
            this.propertyGridFont.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridFont_PropertyValueChanged);
            // 
            // timerScan
            // 
            this.timerScan.Interval = 10;
            this.timerScan.Tick += new System.EventHandler(this.timerScan_Tick);
            // 
            // splitContainerBack
            // 
            this.splitContainerBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerBack.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerBack.Location = new System.Drawing.Point(16, 15);
            this.splitContainerBack.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainerBack.Name = "splitContainerBack";
            // 
            // splitContainerBack.Panel1
            // 
            this.splitContainerBack.Panel1.Controls.Add(this.groupBoxPreview);
            // 
            // splitContainerBack.Panel2
            // 
            this.splitContainerBack.Panel2.Controls.Add(this.groupBoxFont);
            this.splitContainerBack.Panel2MinSize = 276;
            this.splitContainerBack.Size = new System.Drawing.Size(1079, 665);
            this.splitContainerBack.SplitterDistance = 798;
            this.splitContainerBack.SplitterWidth = 5;
            this.splitContainerBack.TabIndex = 0;
            this.splitContainerBack.TabStop = false;
            // 
            // status_bottom
            // 
            this.status_bottom.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.status_bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slab_info,
            this.slab_section,
            this.slab_count});
            this.status_bottom.Location = new System.Drawing.Point(0, 696);
            this.status_bottom.Name = "status_bottom";
            this.status_bottom.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.status_bottom.Size = new System.Drawing.Size(1107, 25);
            this.status_bottom.TabIndex = 4;
            this.status_bottom.Text = "statusStrip1";
            // 
            // slab_info
            // 
            this.slab_info.IsLink = true;
            this.slab_info.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.slab_info.LinkColor = System.Drawing.SystemColors.ControlText;
            this.slab_info.Name = "slab_info";
            this.slab_info.Size = new System.Drawing.Size(786, 20);
            this.slab_info.Spring = true;
            this.slab_info.Text = "开源的字体取模工具";
            this.slab_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.slab_info.Click += new System.EventHandler(this.slab_info_Click);
            // 
            // slab_section
            // 
            this.slab_section.AutoSize = false;
            this.slab_section.Name = "slab_section";
            this.slab_section.Size = new System.Drawing.Size(131, 20);
            this.slab_section.Text = "分段数:0";
            this.slab_section.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // slab_count
            // 
            this.slab_count.AutoSize = false;
            this.slab_count.Name = "slab_count";
            this.slab_count.Size = new System.Drawing.Size(131, 20);
            this.slab_count.Text = "字符:0";
            this.slab_count.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1107, 721);
            this.Controls.Add(this.status_bottom);
            this.Controls.Add(this.splitContainerBack);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(610, 287);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汉字取模软件";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBoxPreview.ResumeLayout(false);
            this.panelShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDraw)).EndInit();
            this.groupBoxFont.ResumeLayout(false);
            this.groupBoxFont.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCode)).EndInit();
            this.splitContainerBack.Panel1.ResumeLayout(false);
            this.splitContainerBack.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerBack)).EndInit();
            this.splitContainerBack.ResumeLayout(false);
            this.status_bottom.ResumeLayout(false);
            this.status_bottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPreview;
        private System.Windows.Forms.PictureBox pictureBoxDraw;
        private System.Windows.Forms.GroupBox groupBoxFont;
        private System.Windows.Forms.PropertyGrid propertyGridFont;
        private System.Windows.Forms.ProgressBar progressBarMake;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox textBoxChar;
        private System.Windows.Forms.Label labelChar;
        private System.Windows.Forms.NumericUpDown numericUpDownCode;
        private System.Windows.Forms.Panel panelShow;
        private System.Windows.Forms.Timer timerScan;
        private System.Windows.Forms.SplitContainer splitContainerBack;
        private System.Windows.Forms.StatusStrip status_bottom;
        private System.Windows.Forms.ToolStripStatusLabel slab_info;
        private System.Windows.Forms.ToolStripStatusLabel slab_section;
        private System.Windows.Forms.ToolStripStatusLabel slab_count;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Label labelImport;
        private System.Windows.Forms.Label labelExport;
    }
}

