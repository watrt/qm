namespace FontMaster
{
    partial class frmSource
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSource));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxEdit = new System.Windows.Forms.TextBox();
            this.buttonSort = new System.Windows.Forms.Button();
            this.labelSourceType = new System.Windows.Forms.Label();
            this.comboBoxSourceType = new System.Windows.Forms.ComboBox();
            this.codeall = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addcode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(432, 308);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 29);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(352, 309);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 29);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxEdit
            // 
            this.textBoxEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEdit.Location = new System.Drawing.Point(16, 15);
            this.textBoxEdit.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEdit.Multiline = true;
            this.textBoxEdit.Name = "textBoxEdit";
            this.textBoxEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEdit.Size = new System.Drawing.Size(483, 244);
            this.textBoxEdit.TabIndex = 2;
            // 
            // buttonSort
            // 
            this.buttonSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSort.Location = new System.Drawing.Point(222, 308);
            this.buttonSort.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSort.Name = "buttonSort";
            this.buttonSort.Size = new System.Drawing.Size(90, 29);
            this.buttonSort.TabIndex = 3;
            this.buttonSort.Text = "排序查重";
            this.buttonSort.UseVisualStyleBackColor = true;
            this.buttonSort.Click += new System.EventHandler(this.buttonSort_Click);
            // 
            // labelSourceType
            // 
            this.labelSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSourceType.AutoSize = true;
            this.labelSourceType.Location = new System.Drawing.Point(16, 316);
            this.labelSourceType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSourceType.Name = "labelSourceType";
            this.labelSourceType.Size = new System.Drawing.Size(61, 15);
            this.labelSourceType.TabIndex = 6;
            this.labelSourceType.Text = "索  引:";
            // 
            // comboBoxSourceType
            // 
            this.comboBoxSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSourceType.FormattingEnabled = true;
            this.comboBoxSourceType.Location = new System.Drawing.Point(82, 311);
            this.comboBoxSourceType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSourceType.Name = "comboBoxSourceType";
            this.comboBoxSourceType.Size = new System.Drawing.Size(132, 23);
            this.comboBoxSourceType.TabIndex = 7;
            // 
            // codeall
            // 
            this.codeall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.codeall.FormattingEnabled = true;
            this.codeall.Items.AddRange(new object[] {
            "0000-007F 基本拉丁字母[*]",
            "0080-00FF 拉丁文补充1",
            "0100-017F 拉丁文扩展A",
            "0180-024F 拉丁文扩展B",
            "0250-02AF 国际音标扩展",
            "02B0-02FF 占位修饰符号",
            "0300-036F 结合附加符号",
            "0370-03FF 希腊字母及科普特字母",
            "0400-04FF 西里尔字母",
            "0500-052F 西里尔字母补充",
            "0530-058F 亚美尼亚字母",
            "0590-05FF 希伯来文",
            "0600-06FF 阿拉伯文",
            "0700-074F 叙利亚文",
            "0750-077F 阿拉伯文补充",
            "0780-07BF 它拿字母",
            "07C0-07FF 西非书面语言",
            "0800-083F 撒玛利亚字母",
            "0840-085F Mandaic",
            "0860-086F Syriac Supplement",
            "08A0-08FF 阿拉伯语扩展",
            "0900-097F 天城文",
            "0980-09FF 孟加拉文",
            "0A00-0A7F 果鲁穆奇字母",
            "0A80-0AFF 古吉拉特文",
            "0B00-0B7F 奥里亚文",
            "0B80-0BFF 泰米尔文",
            "0C00-0C7F 泰卢固文",
            "0C80-0CFF 卡纳达文",
            "0D00-0D7F 马拉雅拉姆文",
            "0D80-0DFF 僧伽罗文",
            "0E00-0E7F 泰文",
            "0E80-0EFF 老挝文",
            "0F00-0FFF 藏文",
            "1000-109F 缅甸文",
            "10A0-10FF 格鲁吉亚字母",
            "1100-11FF 谚文字母",
            "1200-137F 埃塞俄比亚语",
            "1380-139F 埃塞俄比亚语补充",
            "13A0-13FF 切罗基字母",
            "1400-167F 统一加拿大原住民音节文字",
            "1680-169F 欧甘字母",
            "16A0-16FF 卢恩字母",
            "1700-171F 他加禄字母",
            "1720-173F 哈努诺文",
            "1740-175F 布迪文",
            "1760-177F 塔格巴努亚文",
            "1780-17FF 高棉文",
            "1800-18AF 蒙古文",
            "18B0-18FF 统一加拿大原住民音节文字扩展",
            "1900-194F 林布文",
            "1950-197F 德宏傣文",
            "1980-19DF 新傣仂文",
            "19E0-19FF 高棉文符号",
            "1A00-1A1F 布吉文",
            "1A20-1AAF 老傣文",
            "1AB0-1AFF Combining Diacritical Marks Extended",
            "1B00-1B7F 巴厘字母",
            "1B80-1BBF 巽他字母",
            "1BC0-1BFF 巴塔克文",
            "1C00-1C4F 雷布查字母",
            "1C50-1C7F Ol-Chiki",
            "1C80-1C8F Cyrillic Extended C",
            "1C90-1CBF Georgian Extended",
            "1CC0-1CCF 巽他字母补充",
            "1CD0-1CFF 吠陀梵文",
            "1D00-1D7F 语音学扩展",
            "1D80-1DBF 语音学扩展补充",
            "1DC0-1DFF 结合附加符号补充",
            "1E00-1EFF 拉丁文扩展附加",
            "1F00-1FFF 希腊语扩展",
            "2000-206F 常用标点[*]",
            "2070-209F 上标及下标[*]",
            "20A0-20CF 货币符号[*]",
            "20D0-20FF 组合用记号[*]",
            "2100-214F 字母式符号[*]",
            "2150-218F 数字形式[*]",
            "2190-21FF 箭头[*]",
            "2200-22FF 数学运算符[*]",
            "2300-23FF 杂项工业符号[*]",
            "2400-243F 控制图片[*]",
            "2440-245F 光学识别符[*]",
            "2460-24FF 带圈或括号的字母数字[*]",
            "2500-257F 制表符[*]",
            "2580-259F 方块元素[*]",
            "25A0-25FF 几何图形[*]",
            "2600-26FF 杂项符号[*]",
            "2700-27BF 印刷符号",
            "27C0-27EF 杂项数学符号A",
            "27F0-27FF 追加箭头A",
            "2800-28FF 盲文点字模型",
            "2900-297F 追加箭头B",
            "2980-29FF 杂项数学符号B",
            "2A00-2AFF 追加数学运算符",
            "2B00-2BFF 杂项符号和箭头",
            "2C00-2C5F 格拉哥里字母",
            "2C60-2C7F 拉丁文扩展C",
            "2C80-2CFF 科普特字母",
            "2D00-2D2F 格鲁吉亚字母补充",
            "2D30-2D7F 提非纳文",
            "2D80-2DDF 埃塞俄比亚语扩展",
            "2DE0-2DFF 西里尔字母扩展",
            "2E00-2E7F 追加标点",
            "2E80-2EFF 中日韩部首补充",
            "2F00-2FDF 康熙部首",
            "2FF0-2FFF 表意文字描述符",
            "3000-303F 中日韩符号和标点",
            "3040-309F 日文平假名",
            "30A0-30FF 日文片假名",
            "3100-312F 注音字母",
            "3130-318F 谚文兼容字母",
            "3190-319F 象形字注释标志",
            "31A0-31BF 注音字母扩展",
            "31C0-31EF 中日韩笔画",
            "31F0-31FF 日文片假名语音扩展",
            "3200-32FF 带圈中日韩字母和月份",
            "3300-33FF 中日韩字符集兼容",
            "3400-4DBF 中日韩统一表意文字扩展A",
            "4DC0-4DFF 易经六十四卦符号",
            "4E00-9FFF 中日韩统一表意文字[*]",
            "A000-A48F 彝文音节",
            "A490-A4CF 彝文字根",
            "A4D0-A4FF Lisu",
            "A500-A63F 老傈僳文",
            "A640-A69F 西里尔字母扩展B",
            "A6A0-A6FF 巴姆穆语",
            "A700-A71F 声调修饰字母",
            "A720-A7FF 拉丁文扩展D",
            "A800-A82F 锡尔赫特文",
            "A830-A83F 印第安数字",
            "A840-A87F 八思巴文",
            "A880-A8DF 索拉什特拉",
            "A8E0-A8FF 天城文扩展",
            "A900-A92F 克耶字母",
            "A930-A95F 勒姜语",
            "A960-A97F 谚文字母扩展A",
            "A980-A9DF 爪哇语",
            "A9E0-A9FF Myanmar Extended-B",
            "AA00-AA5F 鞑靼文",
            "AA60-AA7F 缅甸语扩展",
            "AA80-AADF 越南傣文",
            "AAE0-AAFF 曼尼普尔文扩展",
            "AB00-AB2F 埃塞俄比亚文",
            "AB30-AB6F Latin Extended-E",
            "AB70-ABBF Cherokee Supplement",
            "ABC0-ABFF 曼尼普尔文",
            "AC00-D7AF 谚文音节",
            "D7B0-D7FF Hangul Jamo Extended-B",
            "D800-DB7F 代理对高位字",
            "DB80-DBFF 代理对私用区高位字",
            "DC00-DFFF 代理对低位字",
            "E000-F8FF 私用区",
            "F900-FAFF 中日韩兼容表意文字",
            "FB00-FB4F 字母表达形式（拉丁字母连字、亚美尼亚字母连字、希伯来文表现形式）",
            "FB50-FDFF 阿拉伯文表达形式A",
            "FE00-FE0F 异体字选择符",
            "FE10-FE1F 竖排形式",
            "FE20-FE2F 组合用半符号",
            "FE30-FE4F 中日韩兼容形式",
            "FE50-FE6F 小写变体形式",
            "FE70-FEFF 阿拉伯文表达形式B",
            "FF00-FFEF 半角及全角形式[*]",
            "FFF0-FFFF 特殊"});
            this.codeall.Location = new System.Drawing.Point(82, 278);
            this.codeall.Name = "codeall";
            this.codeall.Size = new System.Drawing.Size(342, 23);
            this.codeall.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 282);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "编码集:";
            // 
            // addcode
            // 
            this.addcode.Location = new System.Drawing.Point(432, 274);
            this.addcode.Name = "addcode";
            this.addcode.Size = new System.Drawing.Size(75, 29);
            this.addcode.TabIndex = 10;
            this.addcode.Text = "加入";
            this.addcode.UseVisualStyleBackColor = true;
            this.addcode.Click += new System.EventHandler(this.addcode_Click);
            // 
            // frmSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 360);
            this.Controls.Add(this.addcode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codeall);
            this.Controls.Add(this.comboBoxSourceType);
            this.Controls.Add(this.labelSourceType);
            this.Controls.Add(this.buttonSort);
            this.Controls.Add(this.textBoxEdit);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(502, 157);
            this.Name = "frmSource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "索引表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxEdit;
        private System.Windows.Forms.Button buttonSort;
        private System.Windows.Forms.Label labelSourceType;
        private System.Windows.Forms.ComboBox comboBoxSourceType;
        private System.Windows.Forms.ComboBox codeall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addcode;
    }
}