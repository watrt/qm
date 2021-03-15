using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontMaster
{
    public partial class frmSource : Form
    {
        public MySource source;

        int codepage = Encoding.Default.CodePage;

        public frmSource(MySource source)
        {
            InitializeComponent();

            this.source = source;
            this.codepage = CodePageConverter.GetCodePage(source.CodePage);

            comboBoxSourceType.Items.AddRange(new string[] { SourceTypeConverter.SourceTypeSection, SourceTypeConverter.SourceTypeString });
            comboBoxSourceType.Text = source.SourceType;
            textBoxEdit.Text = source.Source;
        }

        public static string CheckValid(string SourceType, int codepage, string text)
        {
            if (SourceType == SourceTypeConverter.SourceTypeSection)
            {
                List<CodeSection> list = CodeSection.Parse(text);
                text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        text += ",";
                    }
                    if (list[i].range != null)
                    {
                        text += "0x" + list[i].range.first_hh.ToString("X2").ToUpper() + ":" + list[i].range.first_ll.ToString("X2").ToUpper()
                            + "-" + "0x" + list[i].range.last_hh.ToString("X2").ToUpper() + ":" + list[i].range.last_ll.ToString("X2").ToUpper();
                    }
                    else
                    {
                        text += "0x" + list[i].first.ToString("X4").ToUpper() + "-0x" + list[i].last.ToString("X4").ToUpper();
                    }

                }
            }
            else
            {
                List<UInt16> list = new List<UInt16>();

                for (int i = 0; i < text.Length; i++)
                {
                    string strTmp = text.Substring(i, 1);
                    if (strTmp.Length > 0)
                    {
                        list.Add(MySource.EncodingGetUint16(codepage, strTmp));
                    }
                }
                list.Sort();
                text = "";
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                    {
                        if (list[i] == list[i - 1])
                        {
                            continue;
                        }
                    }
                    text += MySource.EncodingGetString(codepage, list[i]);
                }
            }

            return text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string text = CheckValid(comboBoxSourceType.Text, codepage, textBoxEdit.Text);

            if (text.Length == 0)
            {
                MessageBoxEx.Show("请检查输入数据！！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            textBoxEdit.Text = text;

            MySource sourceTmp = new MySource(source);

            sourceTmp.SourceType = comboBoxSourceType.Text;
            sourceTmp.Source = textBoxEdit.Text;

            source = sourceTmp;

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            string text = CheckValid(comboBoxSourceType.Text, codepage, textBoxEdit.Text);

            if (text.Length == 0)
            {
                MessageBoxEx.Show("请检查输入数据！！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            textBoxEdit.Text = text;
        }
    }
}
