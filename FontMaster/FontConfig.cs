using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Text;

namespace FontMaster
{
    [TypeConverter(typeof(PropertySorter))]
    class FontConfig
    {
        private const string categorys_0 = "\t\t字体";
        private const string categorys_1 = "\t取模";
        private const string categorys_2 = "编码";

        MyFont def_font = new MyFont();
        MySource def_source = new MySource();
        Padding def_padding = new Padding();

        MyFont _font;
        MySource _source;

        public FontConfig()
        {
            _font = def_font;
            _font.Padding = def_padding;
            _source = def_source;
        }

        #region FontInfo
        [Category(categorys_0), Description("选择类型，选择系统字体或从字体文件加载字体"), PropertyOrder(0x11), DefaultValue("系统字体"), TypeConverter(typeof(FontTypeConverter))]
        public String FontType
        {
            get { return _font.FontType; }
            set { _font.FontType = value; }
        }
        [Category(categorys_0), Description("选择字体"), PropertyOrder(0x12), Editor(typeof(PropertyGridMyFontEditor), typeof(UITypeEditor))]
        public MyFont Font
        {
            get { return _font; }
            set { _font = value; }
        }
        public void ResetFont()
        {
            _font = def_font;
        }
        public bool ShouldSerializeFont()
        {
            return _font != def_font;
        }
        [Category(categorys_0), Description("字体大小"), PropertyOrder(0x13), DefaultValue((int)12)]
        public int Size
        {
            get { return _font.Size; }
            set { _font.Size = value; }
        }
        [Category(categorys_0), Description("是否粗体"), PropertyOrder(0x14), DefaultValue(false)]
        public bool Bold
        {
            get
            {
                return (_font.Style & FontStyle.Bold) != 0;
            }
            set
            {
                if (value)
                {
                    _font.Style |= FontStyle.Bold;
                }
                else
                {
                    _font.Style &= ~FontStyle.Bold;
                }
            }
        }
        [Category(categorys_0), Description("是否斜体"), PropertyOrder(0x15), DefaultValue(false)]
        public bool Italic
        {
            get
            {
                return (_font.Style & FontStyle.Italic) != 0;
            }
            set
            {
                if (value)
                {
                    _font.Style |= FontStyle.Italic;
                }
                else
                {
                    _font.Style &= ~FontStyle.Italic;
                }
            }
        }
        [Category(categorys_0), Description("字符集"), PropertyOrder(0x17), DefaultValue("936,简体中文(GB2312)"), TypeConverter(typeof(CodePageConverter))]
        public String CodePage
        {
            get { return _source.CodePage; }
            set { _source.CodePage = value; }
        }
        #endregion

        #region ScanInfo
        [Category(categorys_1), Description("顺时针旋转角度"), PropertyOrder(0x21), DefaultValue((int)0), TypeConverter(typeof(RotateConverter))]
        public int Rotate
        {
            get { return _font.Rotate; }
            set { _font.Rotate = value; }
        }
        [Category(categorys_1), Description("像素位数"), PropertyOrder(0x22), DefaultValue((int)1), TypeConverter(typeof(GrayBitsConverter))]
        public int GrayBits
        {
            get { return _font.GrayBits; }
            set { _font.GrayBits = value; }
        }
        [Category(categorys_1), Description("水平镜像"), PropertyOrder(0x23), DefaultValue(false)]
        public bool FlipX
        {
            get { return _font.FlipX; }
            set { _font.FlipX = value; }
        }
        [Category(categorys_1), Description("垂直镜像"), PropertyOrder(0x24), DefaultValue(false)]
        public bool FlipY
        {
            get { return _font.FlipY; }
            set { _font.FlipY = value; }
        }
        [Category(categorys_1), Description("字体边距"), PropertyOrder(0x25)]
        public Padding Padding
        {
            get { return _font.Padding; }
            set { _font.Padding = value; }
        }
        public void ResetPadding()
        {
            _font.Padding = def_padding;
        }
        public bool ShouldSerializePadding()
        {
            return _font.Padding != def_padding;
        }
        #endregion

        #region CodeInfo
        [Category(categorys_2), Description("索引模式"), PropertyOrder(0x31), DefaultValue("分段编码"), TypeConverter(typeof(SourceTypeConverter))]
        public String SourceType
        {
            get { return _source.SourceType; }
            set { _source.SourceType = value; }
        }
        [Category(categorys_2), Description("索引表"), PropertyOrder(0x32), Editor(typeof(PropertyGridMySourceEditor), typeof(UITypeEditor))]
        public MySource Source
        {
            get { return _source; }
            set { _source = value; }
        }
        public void ResetSource()
        {
            _source = def_source;
        }
        public bool ShouldSerializeSource()
        {
            return _source != def_source;
        }
        [Category(categorys_2), Description("字节位序"), PropertyOrder(0x33), DefaultValue("MSB First"), TypeConverter(typeof(BitsFirstConverter))]
        public String BitsFirst
        {
            get { return _font.BitsFirst; }
            set { _font.BitsFirst = value; }
        }
        [Category(categorys_2), Description("字节位有效"), PropertyOrder(0x34), DefaultValue("High"), TypeConverter(typeof(BitsPolarityConverter))]
        public String BitsPolarity
        {
            get { return _font.BitsPolarity; }
            set { _font.BitsPolarity = value; }
        }
        [Category(categorys_2), Description("回转模式"), PropertyOrder(0x35), DefaultValue("整行回转"), TypeConverter(typeof(RoundConverter))]
        public String Round
        {
            get { return _font.Round; }
            set { _font.Round = value; }
        }
        [Category(categorys_2), Description("水平扫描"), PropertyOrder(0x36), DefaultValue("从左到右"), TypeConverter(typeof(ScanXConverter))]
        public String ScanX
        {
            get { return _font.ScanX; }
            set { _font.ScanX = value; }
        }
        [Category(categorys_2), Description("垂直扫描"), PropertyOrder(0x37), DefaultValue("从上到下"), TypeConverter(typeof(ScanYConverter))]
        public String ScanY
        {
            get { return _font.ScanY; }
            set { _font.ScanY = value; }
        }
        [Category(categorys_2), Description("扫描顺序"), PropertyOrder(0x38), DefaultValue("先水平后垂直"), TypeConverter(typeof(ScanXYConverter))]
        public String ScanXY
        {
            get { return _font.ScanXY; }
            set { _font.ScanXY = value; }
        }
        #endregion
    }

    public class FontTypeConverter : StringConverter
    {
        static public string FontTypeSystem = "系统字体";
        static public string FontTypeOthers = "其他字体";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { FontTypeSystem, FontTypeOthers });
        }
    }

    public class CodePageConverter : StringConverter
    {
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> codePageList = new List<string>();
            EncodingInfo[] encInfos = Encoding.GetEncodings();
            foreach (EncodingInfo info in encInfos)
            {
                codePageList.Add(info.CodePage.ToString() + "," + info.DisplayName);
            }

            return new StandardValuesCollection(codePageList.ToArray());
        }

        public static int GetCodePage(string name)
        {
            int n = name.IndexOf(",");
            if (n >= 0)
            {
                return int.Parse(name.Substring(0, n));
            }
            else
            {
                EncodingInfo[] encInfos = Encoding.GetEncodings();
                foreach (EncodingInfo info in encInfos)
                {
                    if (info.DisplayName == name)
                    {
                        return info.CodePage;
                    }
                }
            }


            return 0;
        }

        public static string GetDisplayName(int codepage)
        {
            EncodingInfo[] encInfos = Encoding.GetEncodings();
            foreach (EncodingInfo info in encInfos)
            {
                if (info.CodePage == codepage)
                {
                    return info.CodePage.ToString() + "," + info.DisplayName;
                }
            }

            return "";
        }
    }

    public class RotateConverter : UInt16Converter
    {

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new UInt16[] { 0, 90, 180, 270 });
        }
    }

    public class GrayBitsConverter : UInt16Converter
    {

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new UInt16[] { 1, 2, 4, 8 });
        }
    }

    public class CharCodeConverter : StringConverter
    {
        static public string CharCodeMbcs = "MBCS";
        static public string CharCodeUnicode = "UNICODE";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { CharCodeMbcs, CharCodeUnicode });
        }
    }

    public class ScanXYConverter : StringConverter
    {
        static public string ScanXY = "先水平后垂直";
        static public string ScanYX = "先垂直后水平";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { ScanXY, ScanYX });
        }
    }


    public class ScanXConverter : StringConverter
    {
        static public string ScanXLR = "从左到右";
        static public string ScanXRL = "从右到左";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { ScanXLR, ScanXRL });
        }
    }

    public class ScanYConverter : StringConverter
    {
        static public string ScanYTB = "从上到下";
        static public string ScanYBT = "从下到上";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { ScanYTB, ScanYBT });
        }
    }

    public class RoundConverter : StringConverter
    {
        static public string RoundLine = "整行回转";
        static public string RoundByte = "字节回转";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { RoundLine, RoundByte });
        }
    }

    public class BitsFirstConverter : StringConverter
    {
        static public string BitsFirstMSB = "MSB First";
        static public string BitsFirstLSB = "LSB First";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { BitsFirstMSB, BitsFirstLSB });
        }
    }

    public class BitsPolarityConverter : StringConverter
    {
        static public string BitsPolarityHigh = "High";
        static public string BitsPolarityLow = "Low";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { BitsPolarityHigh, BitsPolarityLow });
        }
    }

    public class SourceTypeConverter : StringConverter
    {
        static public string SourceTypeSection = "分段编码";
        static public string SourceTypeString = "索引表";

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { SourceTypeSection, SourceTypeString });
        }
    }

    public class CodeRange
    {
        public int first_hh;
        public int first_ll;
        public int last_hh;
        public int last_ll;

        public List<CodeSection> list;

        public CodeRange()
        {
        }

        public CodeRange(int[] nums)
        {
            first_hh = nums[0];
            first_ll = nums[1];
            last_hh = nums[2];
            last_ll = nums[3];

            List<CodeSection> list = new List<CodeSection>();

            for (int i = first_hh; i <= last_hh; i++)
            {
                list.Add(new CodeSection(i * 256 + first_ll, i * 256 + last_ll));
            }

            this.list = list;
        }
    }

    public class CodeSection
    {
        public int first;
        public int last;

        public CodeRange range;

        public CodeSection()
        {
        }

        public CodeSection(int first, int last)
        {
            this.first = first;
            this.last = last;
        }

        public CodeSection(CodeRange range)
        {
            this.range = range;
        }

        public static List<CodeSection> Parse(String text)
        {
            string[] strs = text.Split(',');
            List<CodeSection> list = new List<CodeSection>();

            try
            {
                for (int i = 0; i < strs.Length; i++)
                {
                    string[] strs_tmp = strs[i].Split('-');
                    if (strs_tmp.Length != 2)
                    {
                        throw new ArgumentException();
                    }
                    // 0x0000-0xFFFF,0xA1:0xA0-0x12:0x13
                    if (strs_tmp[0].IndexOf(":") > 0)
                    {
                        int[] nums = new int[4];
                        for (int j = 0; j < 2; j++)
                        {
                            string[] ss_tmp = strs_tmp[j].Split(':');
                            if (ss_tmp.Length != 2)
                            {
                                throw new ArgumentException();
                            }
                            nums[j * 2 + 0] = Convert.ToByte(ss_tmp[0].Trim().ToLower().Replace("0x", ""), 16);
                            nums[j * 2 + 1] = Convert.ToByte(ss_tmp[1].Trim().ToLower().Replace("0x", ""), 16);
                        }
                        if ((nums[0] <= nums[2]) && (nums[1] <= nums[3]))
                        {
                            list.Add(new CodeSection(new CodeRange(nums)));
                        }
                    }
                    else
                    {
                        int first = Convert.ToUInt16(strs_tmp[0].Trim().ToLower().Replace("0x", ""), 16);
                        int last = Convert.ToUInt16(strs_tmp[1].Trim().ToLower().Replace("0x", ""), 16);
                        if (first <= last)
                        {
                            list.Add(new CodeSection(first, last));
                        }
                    }
                }
                list.Sort(delegate(CodeSection a, CodeSection b)
                {
                    int a_first = a.first;
                    int b_first = b.first;

                    if (a.range != null)
                    {
                        a_first = a.range.first_hh * 256 + a.range.first_ll;
                    }
                    if (b.range != null)
                    {
                        b_first = b.range.first_hh * 256 + b.range.first_ll;
                    }

                    if (a_first > b_first)
                    {
                        return 1;
                    }
                    if (a_first < b_first)
                    {
                        return -1;
                    }

                    return 0;
                });

                for (int i = 1; i < list.Count; i++)
                {
                    int a_first = list[i].first;
                    int b_last = list[i - 1].last;

                    if (list[i].range != null)
                    {
                        a_first = list[i].range.first_hh * 256 + list[i].range.first_ll;
                    }
                    if (list[i - 1].range != null)
                    {
                        b_last = list[i - 1].range.last_hh * 256 + list[i - 1].range.last_ll;
                    }

                    if (a_first <= b_last)
                    {
                        if (list[i].range != null)
                        {
                            list[i].range.first_hh = (b_last + 1) / 256;
                            list[i].range.first_ll = (b_last + 1) % 256;
                        }
                        else
                        {
                            list[i].first = b_last + 1;
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    int a_first = list[i].first;
                    int a_last = list[i].last;

                    if (list[i].range != null)
                    {
                        a_first = list[i].range.first_hh * 256 + list[i].range.first_ll;
                        a_last = list[i].range.last_hh * 256 + list[i].range.last_ll;
                    }

                    if (a_first > a_last)
                    {
                        list.RemoveAt(i);
                    }
                }

                return list;
            }
            catch
            {
                return new List<CodeSection>();
            }
        }

        public static List<CodeSection> ParseString(int codepage, string text)
        {
            List<UInt16> chars = new List<UInt16>();
            List<CodeSection> list = new List<CodeSection>();
            int first = 0, last = 0;
            int i;

            for (i = 0; i < text.Length; i++)
            {
                chars.Add(MySource.EncodingGetUint16(codepage, text.Substring(i, 1)));
            }
            for (i = 0; i < chars.Count; i++)
            {
                if (i == 0)
                {
                    first = chars[i];
                }
                else
                {
                    if (chars[i] != chars[i - 1])
                    {
                        last = chars[i - 1];
                        list.Add(new CodeSection(first, last));
                        first = chars[i];
                    }
                }
            }
            last = chars[i - 1];
            list.Add(new CodeSection(first, last));

            return list;
        }
    }

    public class MySource
    {
        public String CodePage { get; set; }
        public String SourceType { get; set; }
        public String Source { get; set; }

        public MySource()
        {
            CodePage = CodePageConverter.GetDisplayName(Encoding.Default.CodePage);
            SourceType = SourceTypeConverter.SourceTypeSection;
            Source = "0x0020-0x007F,0x20:00-0x2B:FF,0x4E:00-0x9F:FF,0xFF:00-0xFF:FF";
        }

        public MySource(MySource source)
        {
            var type = typeof(MySource);
            foreach (var item in type.GetProperties())
            {
                item.SetValue(this, item.GetValue(source, null), null);
            }
        }

        public override string ToString()
        {
            return Source;
        }

        public bool checkValid()
        {
            if (SourceType == SourceTypeConverter.SourceTypeSection)
            {
                if (CodeSection.Parse(Source).Count == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static UInt16 EncodingGetUint16(int codepage, string text)
        {
            byte[] bytes;

            bytes = Encoding.GetEncoding(codepage).GetBytes(text);
            if (codepage != Encoding.Unicode.CodePage && codepage != Encoding.UTF32.CodePage)
            {
                Array.Reverse(bytes);
            }

            Array.Resize(ref bytes, 2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public static string EncodingGetString(int codepage, UInt16 code)
        {
            string text;
            byte[] bytes = BitConverter.GetBytes(code);

            if (codepage != Encoding.Unicode.CodePage && codepage != Encoding.UTF32.CodePage)
            {
                if (bytes[1] == 0)
                {
                    Array.Resize(ref bytes, 1);
                }
                else
                {
                    Array.Reverse(bytes);
                }
            }
            text = Encoding.GetEncoding(codepage).GetString(bytes);

            return text.Substring(0, 1);
        }
    }

    public class MyFont
    {
        public String FontType { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public FontStyle Style { get; set; }
        public String ScanXY { get; set; }
        public String ScanX { get; set; }
        public String ScanY { get; set; }
        public String Round { get; set; }
        public String BitsFirst { get; set; }
        public String BitsPolarity { get; set; }
        public Padding Padding { get; set; }

        public int Rotate { get; set; }

        public bool FlipX { get; set; }

        public bool FlipY { get; set; }

        public int GrayBits { get; set; }

        public MyFont()
        {
            FontType = FontTypeConverter.FontTypeSystem;
            Name = "宋体";
            Size = 12;
            Style = 0;

            Rotate = 0;
            GrayBits = 1;

            ScanXY = ScanXYConverter.ScanXY;
            ScanX = ScanXConverter.ScanXLR;
            ScanY = ScanYConverter.ScanYTB;
            Round = RoundConverter.RoundLine;
            BitsFirst = BitsFirstConverter.BitsFirstMSB;
            BitsPolarity = BitsPolarityConverter.BitsPolarityHigh;
            Padding = new Padding();
        }

        public MyFont(MyFont font)
        {
            var type = typeof(MyFont);
            foreach (var item in type.GetProperties())
            {
                item.SetValue(this, item.GetValue(font, null), null);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class PropertyGridMyFontEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.Modal;

        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    MyFont font = new MyFont((MyFont)value);

                    if (font.FontType == FontTypeConverter.FontTypeSystem)
                    {
                        FontDialog dialog = new FontDialog();

                        dialog.ShowEffects = false;
                        dialog.Font = new Font(font.Name, font.Size, font.Style);

                        if (dialog.ShowDialog().Equals(DialogResult.OK))
                        {
                            font.Name = dialog.Font.Name;
                            font.Size = (int)dialog.Font.SizeInPoints;
                            font.Style = dialog.Font.Style;
                            return font;
                        }
                    }
                    else
                    {
                        OpenFileDialog dialog = new OpenFileDialog();

                        dialog.Filter = "TrueType Font(*.ttf)|*.ttf|所有文件(*.*)|*.*";
                        dialog.FileName = font.Name;
                        if (dialog.ShowDialog().Equals(DialogResult.OK))
                        {
                            font.Name = dialog.FileName;
                            return font;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return value;
        }
    }

    public class PropertyGridMySourceEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.Modal;

        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                frmSource dialog = new frmSource((MySource)value);

                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return dialog.source;
                }
            }

            return value;
        }
    }
}
