using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace FontMaster
{
    public partial class frmMain : Form
    {
        const int SHOW_MARGIN = 20;
        const int SCAN_HEADER_LENGTH = 24;
        const int SCAN_INFO_LENGTH = 8;
        const int SCAN_INDEX_LENGTH = 8;

        const ushort FONT_STYLE_BLOD = 0x0001; /* bit0 1~Blod */
        const ushort FONT_STYLE_ITALIC = 0x0002; /* bit1 1~Italic */
        const ushort FONT_STYLE_GRAYBITS = 0x000C; /* bit3~2 GrayBits 0~3 -> 1,2,4,8 */
        const ushort FONT_STYLE_ROTATE = 0x0030; /* bit5~4 Rotate 0~3 -> 0,90,180,270 */
        const ushort FONT_STYLE_FLIPX = 0x0040; /* bit6 1~FlipX */
        const ushort FONT_STYLE_FLIPY = 0x0080; /* bit7 1~FlipY */
        const ushort FONT_STYLE_MSB_FIRST = 0x0100; /* bit8 0~LSBFirst,1~MSBFirst */
        const ushort FONT_STYLE_HIGH_POLARITY = 0x0200; /* bit9 0~LowPolarity,1~HighPolarity */
        const ushort FONT_STYLE_LINE_ROUND = 0x0400; /* bit10 0~ByteRound,1~LineRound */
        const ushort FONT_STYLE_SCANX = 0x1000; /* bit12 0~Left to Right,1~Right to Left */
        const ushort FONT_STYLE_SCANY = 0x2000; /* bit 13 0~Top to Bottom,1~Bottom to Top */
        const ushort FONT_STYLE_SCANXY = 0x4000; /* bit14 0~Horizontal then Vertical,1~Vertical then Horizontal */

        const ushort FONT_GRAYBITS_1 = 0x0000; /* bit3~2 GrayBits 0~3 -> 1,2,4,8 */
        const ushort FONT_GRAYBITS_2 = 0x0004;
        const ushort FONT_GRAYBITS_4 = 0x0008;
        const ushort FONT_GRAYBITS_8 = 0x000C;

        const ushort FONT_ROTATE_0 = 0x0000; /* bit5~4 Rotate 0~3 -> 0,90,180,270 */
        const ushort FONT_ROTATE_90 = 0x0010;
        const ushort FONT_ROTATE_180 = 0x0020;
        const ushort FONT_ROTATE_270 = 0x0030;

        FontConfig config = new FontConfig();
        MAT2 mat2;
        int showCodePage = Encoding.Default.CodePage;
        SourceType showSourceType = SourceType.SourceTypeSection;
        bool showMsbFirst = true;
        bool showHighPolarity = true;
        bool showLineRound = true;
        bool showScanX = false;
        bool showScanY = false;
        bool showScanXY = false;

        TEXTMETRICA showTM;
        GLYPHMETRICS showGM;
        IntPtr showFont = IntPtr.Zero;
        List<FontRange> showFontRanges = new List<FontRange>();
        Bitmap showBitmap;

        List<CodeSection> showCodeSectionList = new List<CodeSection>();

        int scan_section;
        int scan_section_max;
        int scan_index;
        int scan_count;
        int scan_count_max;
        int scan_char_max;
        int scan_data_max;

        const int OUTPUT_INDEX_CLNG = 1;
        const int OUTPUT_INDEX_FNT = 2;
        const int OUTPUT_INDEX_TXT = 3;
        const string outputFilter = "C语言文件(*.c)|*.c|字库文件(*.fnt)|*.fnt|文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";

        int outputIndex = 0;
        string outputFileName = "";

        int textSelectionStart = 0;

        FontHeader saveListHeader;
        List<FontSection> saveListSection = new List<FontSection>();
        List<FontIndex> saveListIndex = new List<FontIndex>();
        List<FontData> saveListData = new List<FontData>();

        public enum SourceType
        {
            SourceTypeSection = 0,
            SourceTypeString
        }

        public frmMain()
        {
            InitializeComponent();

            InitMat2(ref mat2);
            propertyGridFont.SelectedObject = config;

            propertyGridFont_PropertyValueChanged(null, null);
            pictureBoxDraw.MouseWheel += pictureBoxDraw_MouseWheel;
        }

        FIXED FixedFromDouble(double d)
        {
            FIXED f;
            long n = (long)(d * 65536);

            f.value = (short)(n / 65536);
            f.fract = (ushort)(n & 65535);

            return f;
        }

        void InitMat2(ref MAT2 lpmat2)
        {
            lpmat2.eM11 = FixedFromDouble(1);
            lpmat2.eM12 = FixedFromDouble(0);
            lpmat2.eM21 = FixedFromDouble(0);
            lpmat2.eM22 = FixedFromDouble(1);
        }

        int ShowFontWidth()
        {
            int width = showGM.gmCellIncX;

            if (width < (int)(showGM.gmBlackBoxX))
            {
                width = (int)(showGM.gmBlackBoxX);
            }

            if (width < (int)(showGM.gmptGlyphOrigin.x + showGM.gmBlackBoxX))
            {
                width = (int)(showGM.gmptGlyphOrigin.x + showGM.gmBlackBoxX);
            }

            return width + config.Font.Padding.Left + config.Font.Padding.Right;
        }

        int ShowFontHeight()
        {
            long height = showTM.tmHeight;

            return (int)height + config.Font.Padding.Top + config.Font.Padding.Bottom;
        }

        RotateFlipType GetRotateFlip(int rotate, bool flipX, bool flipY)
        {
            string[] strRotate = new string[] { "None", "90", "180", "270" };
            string[] strFlip = new string[] { "None", "X", "Y", "XY" };

            int flip = 0;
            if (flipX) flip |= 0x01;
            if (flipY) flip |= 0x02;

            string strValue = "Rotate" + strRotate[rotate / 90] + "Flip" + strFlip[flip];

            RotateFlipType rotateFlip = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), strValue);
            return rotateFlip;
        }

        public bool CheckIfCharInFont(int codepage, List<FontRange> ranges, UInt16 intval)
        {
            if (codepage != Encoding.Unicode.CodePage && codepage != Encoding.UTF32.CodePage)
            {
                byte[] bytes = BitConverter.GetBytes(intval);
                if (bytes[1] == 0)
                {
                    Array.Resize(ref bytes, 1);
                }
                else
                {
                    Array.Reverse(bytes);
                }
                bytes = Encoding.Convert(Encoding.GetEncoding(codepage), Encoding.Unicode, bytes);
                if (bytes.Length != 2)
                {
                    return false;
                }

                byte[] bytesTmp = Encoding.Convert(Encoding.Unicode, Encoding.GetEncoding(codepage), bytes);
                Array.Reverse(bytesTmp);
                Array.Resize(ref bytesTmp, 2);
                if (BitConverter.ToUInt16(bytesTmp, 0) != intval)
                {
                    return false;
                }

                intval = BitConverter.ToUInt16(bytes, 0);
            }

            /*foreach (FontRange range in showFontRanges)
            {
                if ((intval >= range.Low && intval <= range.High))
                {
                    return true;
                }
            }*/

            {
                int i = 0, j = ranges.Count - 1;
                int n = (i + j) / 2;
                while (i < n)
                {
                    if (intval > ranges[n].Low)
                    {
                        i = n;
                    }
                    else
                    {
                        j = n;
                    }

                    n = (i + j) / 2;
                }
                if (intval >= ranges[n + 1].Low)
                {
                    n++;
                }
                if ((intval >= ranges[n].Low) && (intval <= ranges[n].High))
                {
                    return true;
                }
            }

            return false;
        }

        Bitmap PictureDrawChar(string text, bool invert)
        {
            try
            {
                UInt16 uChar = MySource.EncodingGetUint16(Encoding.Unicode.CodePage, text);

                Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                IntPtr hDC = g.GetHdc();
                IntPtr hFontOld = NativeMethods.SelectObject(hDC, showFont);

                uint[] GGO_GRAY_TABLE = new uint[] { NativeMethods.GGO_BITMAP, NativeMethods.GGO_GRAY2_BITMAP, 
                NativeMethods.GGO_GRAY4_BITMAP, NativeMethods.GGO_GRAY8_BITMAP };
                int[] GGO_VMAX_TABLE = new int[] { 1, 4, 16, 64 };

                int ggoIndex = (int)Math.Log(config.GrayBits, 2);
                uint ggoBitmap = GGO_GRAY_TABLE[ggoIndex];
                int dwNeedSize = NativeMethods.GetGlyphOutlineW(hDC, uChar, ggoBitmap, out showGM, 0, IntPtr.Zero, ref mat2);
                Bitmap bitmap = new Bitmap(ShowFontWidth(), ShowFontHeight());
                Graphics gg = Graphics.FromImage(bitmap);
                {
                    int rgb = (invert) ? 0xFF : 0x00;
                    gg.Clear(Color.FromArgb(rgb, rgb, rgb));
                }
                if (dwNeedSize > 0)
                {
                    IntPtr lpBuf = Marshal.AllocHGlobal((int)dwNeedSize);
                    if (lpBuf != IntPtr.Zero)
                    {
                        NativeMethods.GetGlyphOutlineW(hDC, uChar, ggoBitmap, out showGM, dwNeedSize, lpBuf, ref mat2);
                        Bitmap bitmapTmp;
                        if (config.GrayBits == 1)
                        {
                            bitmapTmp = new Bitmap((int)showGM.gmBlackBoxX, (int)showGM.gmBlackBoxY,
                                (int)(((showGM.gmBlackBoxX + 31) / 32) * 4), PixelFormat.Format1bppIndexed, lpBuf);
                        }
                        else
                        {
                            bitmapTmp = new Bitmap((int)showGM.gmBlackBoxX, (int)showGM.gmBlackBoxY, PixelFormat.Format8bppIndexed);

                            {
                                BitmapData bitmapData = bitmapTmp.LockBits(new Rectangle(0, 0, bitmapTmp.Width, bitmapTmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                                int vMax = GGO_VMAX_TABLE[ggoIndex];
                                int pow = (int)(Math.Pow(2, config.GrayBits) - 1);
                                int vMaxHalf = vMax >> 1;
                                int powHalf = pow >> 1;
                                byte[] bytes = new byte[dwNeedSize];
                                Marshal.Copy(lpBuf, bytes, 0, dwNeedSize);
                                for (int i = 0; i < dwNeedSize; i++)
                                {
                                    bytes[i] = (byte)((255 * ((pow * bytes[i] + vMaxHalf) / vMax) + powHalf) / pow);
                                }

                                int scanSize = bitmapData.Stride * bitmapData.Height;
                                if (scanSize > dwNeedSize)
                                {
                                    scanSize = dwNeedSize;
                                }
                                Marshal.Copy(bytes, 0, bitmapData.Scan0, scanSize);

                                bitmapTmp.UnlockBits(bitmapData);
                            }
                        }

                        {
                            ColorPalette palette = bitmapTmp.Palette;
                            int entriesMax = palette.Entries.Length - 1;
                            for (int i = 0; i <= entriesMax; i++)
                            {
                                int rgb = 255 * ((invert) ? (entriesMax - i) : i) / entriesMax;
                                palette.Entries[i] = Color.FromArgb(rgb, rgb, rgb);
                            }
                            bitmapTmp.Palette = palette;
                        }

                        {
                            long gg_x = showGM.gmptGlyphOrigin.x + config.Font.Padding.Left;
                            long gg_y = showTM.tmHeight - (showGM.gmptGlyphOrigin.y + showTM.tmDescent);
                            gg_y += config.Font.Padding.Top;
                            if (gg_y < 0)
                            {
                                gg_y = 0;
                            }
                            else if (gg_y > bitmap.Height - showGM.gmBlackBoxY)
                            {
                                gg_y = bitmap.Height - showGM.gmBlackBoxY;
                            }
                            gg.DrawImage(bitmapTmp, gg_x, gg_y);
                        }

                        Marshal.FreeHGlobal(lpBuf);
                    }
                }
                gg.Dispose();
                NativeMethods.SelectObject(hDC, hFontOld);
                g.ReleaseHdc(hDC);
                g.Dispose();

                return bitmap;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return null;
        }

        private void textBoxChar_KeyDown(object sender, KeyEventArgs e)
        {
            textBoxChar_MouseMove(null, null);
        }

        private void textBoxChar_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxChar_MouseMove(null, null);
        }

        int GetSelectionStart(TextBox textBox)
        {
            int SelectionStart = textBox.SelectionStart;
            if (textBox.SelectionLength > 0)
            {
                if (SelectionStart >= textBox.Text.Length)
                {
                    SelectionStart = textBox.Text.Length - 1;
                }
            }
            else
            {
                if (SelectionStart < 1)
                {
                    SelectionStart = 1;
                }
                SelectionStart--;
            }

            return SelectionStart;
        }

        private void textBoxChar_MouseMove(object sender, MouseEventArgs e)
        {
            int SelectionStart = GetSelectionStart(textBoxChar);
            if (textSelectionStart != SelectionStart)
            {
                textBoxChar_TextChanged(null, null);
            }
        }

        private void textBoxChar_TextChanged(object sender, EventArgs e)
        {
            if (textBoxChar.Text.Length > 0)
            {
                int SelectionStart = GetSelectionStart(textBoxChar);
                textSelectionStart = SelectionStart;
                string text = textBoxChar.Text.Substring(textSelectionStart, 1);

                UInt16 code = MySource.EncodingGetUint16(showCodePage, text);
                if (text != MySource.EncodingGetString(showCodePage, code))
                {
                    textBoxChar.ForeColor = Color.Red;
                    numericUpDownCode.ForeColor = Color.Red;
                }
                else if (!CheckIfCharInFont(showCodePage, showFontRanges, code))
                {
                    textBoxChar.ForeColor = Color.Black;
                    numericUpDownCode.ForeColor = Color.Red;

                    numericUpDownCode.Value = code;
                }
                else
                {
                    textBoxChar.ForeColor = Color.Black;
                    numericUpDownCode.ForeColor = Color.Black;

                    numericUpDownCode.Value = code;
                    showBitmap = PictureDrawChar(text, true);

                    showBitmap.RotateFlip(GetRotateFlip(config.Rotate, config.FlipX, config.FlipY));
                    groupBoxPreview.Text = "预览(" + showBitmap.Width.ToString() + "," + showBitmap.Height.ToString() + ")";

                    pictureBoxDraw_Delta(0);
                    pictureBoxDraw.Image = ZoomImage(showBitmap, pictureBoxDraw.Width, pictureBoxDraw.Height);
                    pictureBoxDraw.Refresh();
                }
            }
        }

        private void numericUpDownCode_ValueChanged(object sender, EventArgs e)
        {
            UInt16 code = (UInt16)numericUpDownCode.Value;
            if (!CheckIfCharInFont(showCodePage, showFontRanges, code))
            {
                numericUpDownCode.ForeColor = Color.Red;
            }
            else
            {
                textBoxChar.ForeColor = Color.Black;
                numericUpDownCode.ForeColor = Color.Black;
                string text = MySource.EncodingGetString(showCodePage, code);
                if ((textBoxChar.Text.Length == 0) || (text != textBoxChar.Text.Substring(textSelectionStart, 1)))
                {
                    textBoxChar.Text = text;
                }
            }
        }

        List<FontRange> GetUnicodeRangesForFont(IntPtr hdc)
        {
            uint size = NativeMethods.GetFontUnicodeRanges(hdc, IntPtr.Zero);
            IntPtr glyphSet = Marshal.AllocHGlobal((int)size);
            NativeMethods.GetFontUnicodeRanges(hdc, glyphSet);
            List<FontRange> ranges = new List<FontRange>();
            int count = Marshal.ReadInt32(glyphSet, 12);
            for (int i = 0; i < count; i++)
            {
                FontRange range = new FontRange();
                range.Low = (UInt16)Marshal.ReadInt16(glyphSet, 16 + i * 4);
                range.High = (UInt16)(range.Low + Marshal.ReadInt16(glyphSet, 18 + i * 4) - 1);
                ranges.Add(range);
            }
            Marshal.FreeHGlobal(glyphSet);

            ranges.Sort(delegate(FontRange a, FontRange b)
            {
                if (a.Low > b.Low)
                {
                    return 1;
                }
                if (a.Low < b.Low)
                {
                    return -1;
                }

                return 0;
            });

            return ranges;
        }

        List<CodeSection> CodeSectionListAnalyzer(int codepage, List<FontRange> ranges, List<CodeSection> list_old)
        {
            List<CodeSection> list_new = new List<CodeSection>();

            bool valid = false;
            int first = 0;

            for (int i = 0; i < list_old.Count; i++)
            {
                if (list_old[i].range != null)
                {
                    List<CodeSection> list = list_old[i].range.list;

                    for (int j = 0; j < list.Count; j++)
                    {
                        int k;
                        for (k = list[j].first; k <= list[j].last; k++)
                        {
                            if (CheckIfCharInFont(codepage, ranges, (UInt16)k))
                            {
                                if (!valid)
                                {
                                    valid = true;
                                    first = k;
                                }
                            }
                            else
                            {
                                if (valid)
                                {
                                    valid = false;
                                    list_new.Add(new CodeSection(first, k - 1));
                                }
                            }
                        }
                        if (valid)
                        {
                            valid = false;
                            list_new.Add(new CodeSection(first, k - 1));
                        }
                    }
                }
                else
                {
                    int k;
                    for (k = list_old[i].first; k <= list_old[i].last; k++)
                    {
                        if (CheckIfCharInFont(codepage, ranges, (UInt16)k))
                        {
                            if (!valid)
                            {
                                valid = true;
                                first = k;
                            }
                        }
                        else
                        {
                            if (valid)
                            {
                                valid = false;
                                list_new.Add(new CodeSection(first, k - 1));
                            }
                        }
                    }
                    if (valid)
                    {
                        valid = false;
                        list_new.Add(new CodeSection(first, k - 1));
                    }
                }
            }

            {
                int i = 1;
                while (i < list_new.Count)
                {
                    if (list_new[i].first == list_new[i - 1].last + 1)
                    {
                        list_new[i - 1].last = list_new[i].last;
                        list_new.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }


            return list_new;
        }

        private void propertyGridFont_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                {
                    Font font;
                    if (config.FontType == FontTypeConverter.FontTypeSystem)
                    {
                        font = new Font(config.Font.Name, config.Font.Size, config.Font.Style, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        PrivateFontCollection pfc = new PrivateFontCollection();
                        pfc.AddFontFile(config.Font.Name);
                        font = new Font(pfc.Families[0], config.Font.Size, config.Font.Style, GraphicsUnit.Pixel);
                    }

                    if (showFont != IntPtr.Zero)
                    {
                        NativeMethods.DeleteObject(showFont);
                        showFont = IntPtr.Zero;
                    }
                    showFont = font.ToHfont();

                    Graphics g = pictureBoxDraw.CreateGraphics();
                    IntPtr hDC = g.GetHdc();
                    IntPtr hFontOld = NativeMethods.SelectObject(hDC, showFont);
                    showFontRanges = GetUnicodeRangesForFont(hDC);
                    NativeMethods.GetTextMetrics(hDC, out showTM);
                    NativeMethods.SelectObject(hDC, hFontOld);
                    g.ReleaseHdc(hDC);
                    g.Dispose();
                }

                {
                    showCodePage = CodePageConverter.GetCodePage(config.CodePage);
                    showSourceType = (config.SourceType == SourceTypeConverter.SourceTypeSection) ? SourceType.SourceTypeSection : SourceType.SourceTypeString;

                    showMsbFirst = (config.BitsFirst == BitsFirstConverter.BitsFirstMSB);
                    showHighPolarity = (config.BitsPolarity == BitsPolarityConverter.BitsPolarityHigh);
                    showLineRound = (config.Round == RoundConverter.RoundLine);
                    showScanX = (config.ScanX == ScanXConverter.ScanXRL);
                    showScanY = (config.ScanY == ScanYConverter.ScanYBT);
                    showScanXY = (config.ScanXY == ScanXYConverter.ScanYX);
                }

                {
                    string source = frmSource.CheckValid(config.SourceType, showCodePage, config.Source.Source);
                    if (source.Length > 0)
                    {
                        config.Source.Source = source;
                    }
                }

                {
                    List<CodeSection> list;
                    if (showSourceType == SourceType.SourceTypeSection)
                    {
                        list = CodeSection.Parse(config.Source.Source);
                    }
                    else
                    {
                        list = CodeSection.ParseString(showCodePage, config.Source.Source);
                    }
                    showCodeSectionList = CodeSectionListAnalyzer(showCodePage, showFontRanges, list);
                }

                textBoxChar_TextChanged(null, null);

                ////////////////////////////////////////////////////////////////

                int scan_count_tmp = 0;
                for (int i = 0; i < showCodeSectionList.Count; i++)
                {
                    scan_count_tmp += showCodeSectionList[i].last - showCodeSectionList[i].first + 1;
                }

                slab_section.Text = "分段:" + showCodeSectionList.Count.ToString();
                slab_count.Text = "字符:" + scan_count_tmp.ToString();

                if (scan_count_tmp == 0)
                {
                    throw new ArgumentException();
                }

                buttonStart.Enabled = true;
            }
            catch
            {
                buttonStart.Enabled = false;
            }
        }

        private Bitmap ZoomImage(Bitmap bitmap, int destWidth, int destHeight)
        {
            int kw = (destWidth - SHOW_MARGIN) / bitmap.Width;
            int kh = (destHeight - SHOW_MARGIN) / bitmap.Height;
            int k = (kw < kh) ? kw : kh;
            if (k < 1) k = 1;
            int xx = (destWidth - bitmap.Width * k) / 2;
            int yy = (destHeight - bitmap.Height * k) / 2;
            if (xx < 0) xx = 0;
            if (yy < 0) yy = 0;

            Bitmap destBitmap = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(destBitmap);
            if (k == 1)
            {
                g.DrawImage(bitmap, xx, yy, bitmap.Width * k, bitmap.Height * k);
            }
            else
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        g.FillRectangle(new SolidBrush(bitmap.GetPixel(x, y)), xx + x * k + 1, yy + y * k + 1, k - 1, k - 1);
                        g.DrawRectangle(Pens.DarkGray, xx + x * k, yy + y * k, k, k);
                    }
                }
            }
            g.Dispose();
            return destBitmap;
        }

        private void pictureBoxDraw_SizeChanged(object sender, EventArgs e)
        {
            if (showBitmap != null)
            {
                pictureBoxDraw_Delta(0);
                pictureBoxDraw.Image = ZoomImage(showBitmap, pictureBoxDraw.Width, pictureBoxDraw.Height);
                pictureBoxDraw.Refresh();
            }
        }

        private void pictureBoxDraw_Delta(int delta)
        {
            if (delta < showBitmap.Width + SHOW_MARGIN - pictureBoxDraw.Width) delta = showBitmap.Width + SHOW_MARGIN - pictureBoxDraw.Width;
            if (delta < showBitmap.Height + SHOW_MARGIN - pictureBoxDraw.Height) delta = showBitmap.Height + SHOW_MARGIN - pictureBoxDraw.Height;
            if (delta > pictureBoxDraw.Parent.Width - pictureBoxDraw.Width) delta = pictureBoxDraw.Parent.Width - pictureBoxDraw.Width;
            if (delta > pictureBoxDraw.Parent.Height - pictureBoxDraw.Height) delta = pictureBoxDraw.Parent.Height - pictureBoxDraw.Height;
            int ww = pictureBoxDraw.Width + delta;
            int hh = pictureBoxDraw.Height + delta;
            pictureBoxDraw.SetBounds((pictureBoxDraw.Parent.Width - ww) / 2, (pictureBoxDraw.Parent.Height - hh) / 2, ww, hh);
        }

        private void pictureBoxDraw_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = (showBitmap.Width > showBitmap.Height) ? showBitmap.Width : showBitmap.Height;
            if (e.Delta < 0) delta = -delta;

            pictureBoxDraw_Delta(delta);
        }

        private void pictureBoxDraw_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxDraw.Focus();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (timerScan.Enabled)
            {
                timerScan.Enabled = false;
                textBoxChar.Enabled = true;
                numericUpDownCode.Enabled = true;
                propertyGridFont.Enabled = true;

                buttonStart.Text = "生成字库";
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.Filter = outputFilter;
                dlg.FilterIndex = outputIndex;
                if (dlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                outputIndex = dlg.FilterIndex;
                outputFileName = dlg.FileName;

                {
                    saveListSection.Clear();
                    saveListIndex.Clear();
                    saveListData.Clear();

                    scan_char_max = 0;
                    scan_data_max = 0;
                    scan_section_max = showCodeSectionList.Count;

                    scan_count_max = 0;
                    for (int i = 0; i < showCodeSectionList.Count; i++)
                    {
                        FontSection sec_tmp = new FontSection();
                        sec_tmp.first = (ushort)showCodeSectionList[i].first;
                        sec_tmp.last = (ushort)showCodeSectionList[i].last;
                        sec_tmp.offset = (uint)(SCAN_HEADER_LENGTH + SCAN_INFO_LENGTH * scan_section_max + SCAN_INDEX_LENGTH * scan_count_max);
                        saveListSection.Add(sec_tmp);

                        scan_count_max += showCodeSectionList[i].last - showCodeSectionList[i].first + 1;
                    }

                    scan_section = 0;
                    if (scan_count_max > 0)
                    {
                        scan_index = showCodeSectionList[scan_section].first;
                    }

                    saveListHeader.magic = new byte[4];
                    saveListHeader.magic[0] = (byte)'F';
                    saveListHeader.magic[1] = (byte)'N';
                    saveListHeader.magic[2] = (byte)'T';
                    saveListHeader.magic[3] = (byte)0x01;

                    saveListHeader.style = 0;
                    if (config.Bold) saveListHeader.style |= FONT_STYLE_BLOD;
                    if (config.Italic) saveListHeader.style |= FONT_STYLE_ITALIC;
                    if (config.GrayBits == 2) saveListHeader.style |= FONT_GRAYBITS_2;
                    else if (config.GrayBits == 4) saveListHeader.style |= FONT_GRAYBITS_4;
                    else if (config.GrayBits == 8) saveListHeader.style |= FONT_GRAYBITS_8;
                    else saveListHeader.style |= FONT_GRAYBITS_1;
                    if (config.Rotate == 90) saveListHeader.style |= FONT_ROTATE_90;
                    else if (config.Rotate == 180) saveListHeader.style |= FONT_ROTATE_180;
                    else if (config.Rotate == 270) saveListHeader.style |= FONT_ROTATE_270;
                    else saveListHeader.style |= FONT_ROTATE_0;
                    if (config.FlipX) saveListHeader.style |= FONT_STYLE_FLIPX;
                    if (config.FlipY) saveListHeader.style |= FONT_STYLE_FLIPY;
                    if (showMsbFirst) saveListHeader.style |= FONT_STYLE_MSB_FIRST;
                    if (showHighPolarity) saveListHeader.style |= FONT_STYLE_HIGH_POLARITY;
                    if (showLineRound) saveListHeader.style |= FONT_STYLE_LINE_ROUND;
                    if (showScanX) saveListHeader.style |= FONT_STYLE_SCANX;
                    if (showScanY) saveListHeader.style |= FONT_STYLE_SCANY;
                    if (showScanXY) saveListHeader.style |= FONT_STYLE_SCANXY;

                    saveListHeader.height = (ushort)ShowFontHeight();
                    saveListHeader.codepage = (ushort)showCodePage;
                    saveListHeader.padding = new byte[4];
                    saveListHeader.padding[0] = (byte)config.Padding.Left;
                    saveListHeader.padding[1] = (byte)config.Padding.Top;
                    saveListHeader.padding[2] = (byte)config.Padding.Right;
                    saveListHeader.padding[3] = (byte)config.Padding.Bottom;
                    saveListHeader.total_sections = (ushort)scan_section_max;
                    saveListHeader.total_chars = 0;
                    saveListHeader.total_size = 0;
                }

                if (scan_count_max > 0)
                {
                    scan_count = 0;
                    progressBarMake.Value = scan_count * 100 / scan_count_max;

                    textBoxChar.Enabled = false;
                    numericUpDownCode.Enabled = false;
                    propertyGridFont.Enabled = false;
                    timerScan.Enabled = true;

                    buttonStart.Text = "停止生成";
                }
            }
        }

        byte[] BitmapToBits(Bitmap bitmap, bool MsbFirst, bool LineRound, bool HighPolarity, int GrayBits)
        {
            int line_bytes = ((bitmap.Width * GrayBits) + 7) / 8;
            byte[] bytes = new byte[line_bytes * bitmap.Height];
            int k = 0, b = 0;
            int LastBits = 8 - GrayBits;
            byte BitsMask = (byte)(0xFF << LastBits);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            if (LineRound)
            {
                int byteTmpLength = bitmapData.Width;
                int[] byteTmp = new int[byteTmpLength];

                for (int y = 0; y < bitmap.Height; y++)
                {
                    Marshal.Copy(bitmapData.Scan0 + y * bitmapData.Stride, byteTmp, 0, byteTmpLength);

                    for (int x = 0; x < byteTmpLength; x++)
                    {
                        if (MsbFirst)
                        {
                            if (HighPolarity)
                            {
                                bytes[k] |= (byte)(((byteTmp[x] & BitsMask) >> b));
                            }
                            else
                            {
                                bytes[k] |= (byte)((((255 - byteTmp[x]) & BitsMask) >> b));
                            }
                        }
                        else
                        {
                            if (HighPolarity)
                            {
                                bytes[k] |= (byte)(((byteTmp[x] & BitsMask) >> (LastBits - b)));
                            }
                            else
                            {
                                bytes[k] |= (byte)((((255 - byteTmp[x]) & BitsMask) >> (LastBits - b)));
                            }
                        }

                        b += GrayBits;
                        if (b >= 8)
                        {
                            b = 0;
                            k++;
                        }
                    }

                    while (b > 0)
                    {
                        if (MsbFirst)
                        {
                            if (HighPolarity)
                            {
                                bytes[k] |= (byte)(((0x00 & BitsMask) >> b));
                            }
                            else
                            {
                                bytes[k] |= (byte)((((255 - 0x00) & BitsMask) >> b));
                            }
                        }
                        else
                        {
                            if (HighPolarity)
                            {
                                bytes[k] |= (byte)(((0x00 & BitsMask) >> (LastBits - b)));
                            }
                            else
                            {
                                bytes[k] |= (byte)((((255 - 0x00) & BitsMask) >> (LastBits - b)));
                            }
                        }

                        b += GrayBits;
                        if (b >= 8)
                        {
                            b = 0;
                            k++;
                        }
                    }
                }
            }
            else
            {
                int byteTmpLength = 8 / GrayBits;
                int[] byteTmp = new int[byteTmpLength];

                for (int x = 0; x < line_bytes; x++)
                {
                    int lengthTmp = byteTmpLength;
                    if (lengthTmp > (bitmapData.Width - x * byteTmpLength))
                    {
                        lengthTmp = bitmapData.Width - x * byteTmpLength;
                    }

                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        Marshal.Copy(bitmapData.Scan0 + x * byteTmpLength * 4 + y * bitmapData.Stride, byteTmp, 0, lengthTmp);

                        for (int i = 0; i < lengthTmp; i++)
                        {
                            if (MsbFirst)
                            {
                                if (HighPolarity)
                                {
                                    bytes[k] |= (byte)(((byteTmp[i] & BitsMask) >> b));
                                }
                                else
                                {
                                    bytes[k] |= (byte)((((255 - byteTmp[i]) & BitsMask) >> b));
                                }
                            }
                            else
                            {
                                if (HighPolarity)
                                {
                                    bytes[k] |= (byte)(((byteTmp[i] & BitsMask) >> (LastBits - b)));
                                }
                                else
                                {
                                    bytes[k] |= (byte)((((255 - byteTmp[i]) & BitsMask) >> (LastBits - b)));
                                }
                            }

                            b += GrayBits;
                            if (b >= 8)
                            {
                                b = 0;
                                k++;
                            }
                        }

                        while (b > 0)
                        {
                            if (MsbFirst)
                            {
                                if (HighPolarity)
                                {
                                    bytes[k] |= (byte)(((0x00 & BitsMask) >> b));
                                }
                                else
                                {
                                    bytes[k] |= (byte)((((255 - 0x00) & BitsMask) >> b));
                                }
                            }
                            else
                            {
                                if (HighPolarity)
                                {
                                    bytes[k] |= (byte)(((0x00 & BitsMask) >> (LastBits - b)));
                                }
                                else
                                {
                                    bytes[k] |= (byte)((((255 - 0x00) & BitsMask) >> (LastBits - b)));
                                }
                            }

                            b += GrayBits;
                            if (b >= 8)
                            {
                                b = 0;
                                k++;
                            }
                        }
                    }
                }
            }

            bitmap.UnlockBits(bitmapData);

            return bytes;
        }

        void SaveFileToClng(string FileName)
        {
            StreamWriter fs = File.CreateText(FileName);

            {
                fs.Write("typedef unsigned char   uint8_t;\r\n"
                        + "typedef unsigned short  uint16_t;\r\n"
                        + "typedef unsigned long   uint32_t;\r\n"
                        + "\r\n"
                        + "#define FONT_STYLE_BLOD             0x0001 /* bit0 1~Blod */\r\n"
                        + "#define FONT_STYLE_ITALIC           0x0002 /* bit1 1~Italic */\r\n"
                        + "#define FONT_STYLE_GRAYBITS         0x000C /* bit3~2 GrayBits 0~3 -> 1,2,4,8 */\r\n"
                        + "#define FONT_STYLE_ROTATE           0x0030 /* bit5~4 Rotate 0~3 -> 0,90,180,270 */\r\n"
                        + "#define FONT_STYLE_FLIPX            0x0040 /* bit6 1~FlipX */\r\n"
                        + "#define FONT_STYLE_FLIPY            0x0080 /* bit7 1~FlipY */\r\n"
                        + "#define FONT_STYLE_MSB_FIRST        0x0100 /* bit8 0~LSBFirst,1~MSBFirst */\r\n"
                        + "#define FONT_STYLE_HIGH_POLARITY    0x0200 /* bit9 0~LowPolarity,1~HighPolarity */\r\n"
                        + "#define FONT_STYLE_LINE_ROUND       0x0400 /* bit10 0~ByteRound,1~LineRound */\r\n"
                        + "#define FONT_STYLE_SCANX            0x1000 /* bit12 0~Left to Right,1~Right to Left */\r\n"
                        + "#define FONT_STYLE_SCANY            0x2000 /* bit 13 0~Top to Bottom,1~Bottom to Top */\r\n"
                        + "#define FONT_STYLE_SCANXY           0x4000 /* bit14 0~Horizontal then Vertical,1~Vertical then Horizontal */\r\n"
                        + "\r\n"
                        + "#define FONT_GRAYBITS_1     0x0000; /* bit3~2 GrayBits 0~3 -> 1,2,4,8 */\r\n"
                        + "#define FONT_GRAYBITS_2     0x0004;\r\n"
                        + "#define FONT_GRAYBITS_4     0x0008;\r\n"
                        + "#define FONT_GRAYBITS_8     0x000C;\r\n"
                        + "\r\n"
                        + "#define FONT_ROTATE_0       0x0000 /* bit5~4 Rotate 0~3 -> 0,90,180,270 */\r\n"
                        + "#define FONT_ROTATE_90      0x0010\r\n"
                        + "#define FONT_ROTATE_180     0x0020\r\n"
                        + "#define FONT_ROTATE_270     0x0030\r\n"
                        + "\r\n"
                        + "typedef struct _font_header\r\n"
                        + "{\r\n"
                        + "    uint8_t magic[4]; /* 'F', 'N', 'T', Ver */\r\n"
                        + "    uint32_t style; /* the font style */\r\n"
                        + "    uint16_t height; /* the font height */\r\n"
                        + "    uint16_t codepage; /* 936 GB2312, 1200 Unicode */\r\n"
                        + "    uint8_t padding[4]; /* left, top, right, bottom padding */\r\n"
                        + "\r\n"
                        + "    uint16_t total_sections; /* total sections */\r\n"
                        + "    uint16_t total_chars; /* total characters */\r\n"
                        + "    uint32_t total_size; /* file total size or data total size */\r\n"
                        + "} font_header_t;\r\n"
                        + "\r\n"
                        + "typedef struct _font_section\r\n"
                        + "{\r\n"
                        + "    uint16_t first; /* first character */\r\n"
                        + "    uint16_t last; /* last character */\r\n"
                        + "    uint32_t offset; /* the first font_index offset */\r\n"
                        + "} font_section_t;\r\n"
                        + "\r\n"
                        + "typedef struct _font_index\r\n"
                        + "{\r\n"
                        + "    uint16_t width; /* the width of the character */\r\n"
                        + "    uint16_t size; /* the bitmap data size */\r\n"
                        + "    uint32_t offset; /* the font bitmap data offset */\r\n"
                        + "} font_index_t;\r\n"
                        + "\r\n");
            }

            {
                fs.Write("/******************* header ***************************************************/\r\n"
                        + "const font_header_t font_header =\r\n"
                        + "{\r\n");
                fs.Write("    {'F', 'N', 'T', " + "0x" + saveListHeader.magic[3].ToString("x2").ToUpper() + "},\r\n");
                fs.Write("    " + "0x" + saveListHeader.style.ToString("x8").ToUpper() + ",\r\n");
                fs.Write("    " + saveListHeader.height.ToString() + ",\r\n");
                fs.Write("    " + saveListHeader.codepage.ToString() + ",\r\n");
                fs.Write("    {" + saveListHeader.padding[0].ToString() + ", " + saveListHeader.padding[1].ToString() + ", "
                        + saveListHeader.padding[2].ToString() + ", " + saveListHeader.padding[3].ToString() + "},\r\n");
                fs.Write("    " + saveListHeader.total_sections.ToString() + ",\r\n");
                fs.Write("    " + saveListHeader.total_chars.ToString() + ",\r\n");
                fs.Write("    " + saveListHeader.total_size.ToString() + ",\r\n");
                fs.Write("};\r\n" + "\r\n");
            }

            {
                fs.Write("/******************* section **************************************************/\r\n"
                        + "const font_section_t font_sections[" + scan_section_max.ToString() + "] =\r\n"
                        + "{\r\n");

                for (int i = 0; i < saveListSection.Count; i++)
                {
                    uint SectionIndex = (saveListSection[i].offset >= saveListSection[0].offset) ?
                        (saveListSection[i].offset - saveListSection[0].offset) / SCAN_INDEX_LENGTH : 0;

                    fs.Write("    {" + "0x" + saveListSection[i].first.ToString("x4").ToUpper() + ", "
                            + "0x" + saveListSection[i].last.ToString("x4").ToUpper() + ", "
                            + SectionIndex.ToString() + "},\r\n");
                }

                fs.Write("};\r\n" + "\r\n");
            }

            {
                fs.Write("/******************* index ****************************************************/\r\n"
                        + "const font_index_t font_indexs[" + scan_count_max.ToString() + "] =\r\n"
                        + "{\r\n");

                for (int i = 0; i < saveListIndex.Count; i++)
                {
                    uint OffAddr = (saveListIndex[i].offset >= saveListIndex[0].offset) ?
                        (saveListIndex[i].offset - saveListIndex[0].offset) : 0;

                    fs.Write("    {" + saveListIndex[i].width.ToString() + ", "
                            + saveListIndex[i].size.ToString() + ", "
                            + "0x" + OffAddr.ToString("x8").ToUpper() + "},");
                    if (saveListIndex[i].width > 0)
                    {
                        fs.Write(" /* " + "0x" + saveListIndex[i].code.ToString("x4").ToUpper()
                            + ", " + "'" + MySource.EncodingGetString(showCodePage, saveListIndex[i].code) + "'"
                            + " */");
                    }

                    fs.Write("\r\n");
                }

                fs.Write("};\r\n" + "\r\n");
            }

            {
                fs.Write("/******************* data *****************************************************/\r\n"
                        + "const uint8_t font_data[" + scan_data_max.ToString() + "] =\r\n"
                        + "{\r\n");

                for (int i = 0; i < saveListData.Count; i++)
                {
                    fs.Write("    /* " + "0x" + saveListData[i].code.ToString("x4").ToUpper()
                         + ", " + "'" + MySource.EncodingGetString(showCodePage, saveListData[i].code) + "'"
                         + " */\r\n");

                    for (int j = 0; j < saveListData[i].data.Length; j++)
                    {
                        if ((j % 16) == 0)
                        {
                            if (j > 0)
                            {
                                fs.Write("\r\n");
                            }
                            fs.Write("    ");
                        }
                        else
                        {
                            fs.Write(" ");
                        }

                        fs.Write("0x" + saveListData[i].data[j].ToString("x2").ToUpper() + ",");
                    }
                    fs.Write("\r\n");

                    Application.DoEvents();
                }

                fs.Write("};\r\n");
            }

            fs.Close();
        }

        void FileWrite(FileStream fs, byte[] bytes)
        {
            fs.Write(bytes, 0, bytes.Length);
        }

        void SaveFileToFnt(string FileName)
        {
            FileStream fs = File.Create(FileName);

            {
                FileWrite(fs, saveListHeader.magic);
                FileWrite(fs, BitConverter.GetBytes(saveListHeader.style));
                FileWrite(fs, BitConverter.GetBytes(saveListHeader.height));
                FileWrite(fs, BitConverter.GetBytes(saveListHeader.codepage));
                FileWrite(fs, saveListHeader.padding);
                FileWrite(fs, BitConverter.GetBytes(saveListHeader.total_sections));
                FileWrite(fs, BitConverter.GetBytes(saveListHeader.total_chars));
                FileWrite(fs, BitConverter.GetBytes(saveListHeader.total_size));
                if (fs.Length != SCAN_HEADER_LENGTH)
                {
                    MessageBoxEx.Show("文件头长度错误！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            {
                for (int i = 0; i < saveListSection.Count; i++)
                {
                    FileWrite(fs, BitConverter.GetBytes(saveListSection[i].first));
                    FileWrite(fs, BitConverter.GetBytes(saveListSection[i].last));
                    FileWrite(fs, BitConverter.GetBytes(saveListSection[i].offset));
                }
                if (fs.Length != (SCAN_HEADER_LENGTH + SCAN_INFO_LENGTH * scan_section_max))
                {
                    MessageBoxEx.Show("段信息长度错误！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            {
                for (int i = 0; i < saveListIndex.Count; i++)
                {
                    FileWrite(fs, BitConverter.GetBytes(saveListIndex[i].width));
                    FileWrite(fs, BitConverter.GetBytes(saveListIndex[i].size));
                    FileWrite(fs, BitConverter.GetBytes(saveListIndex[i].offset));
                }
                if (fs.Length != (SCAN_HEADER_LENGTH + SCAN_INFO_LENGTH * scan_section_max + SCAN_INDEX_LENGTH * scan_count_max))
                {
                    MessageBoxEx.Show("检索表长度错误！！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            {
                for (int i = 0; i < saveListData.Count; i++)
                {
                    FileWrite(fs, saveListData[i].data);
                }
            }

            fs.Close();
        }

        void SaveFileToTxt(string FileName)
        {
            StreamWriter fs = File.CreateText(FileName);

            for (int i = 0; i < saveListIndex.Count; i++)
            {
                string strTmp = MySource.EncodingGetString(showCodePage, saveListIndex[i].code);
                if (strTmp.Length == 0)
                {
                    strTmp = "?";
                }
                fs.Write(strTmp);
            }

            fs.Close();
        }

        bool timer_busy = false;

        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (!timer_busy)
            {
                timer_busy = true;
                int retry = 0;
                while (timerScan.Enabled)
                {
                    try
                    {
                        {
                            UInt16 code = (UInt16)scan_index;
                            FontIndex index_tmp = new FontIndex();
                            byte[] bytes = new byte[0];

                            index_tmp.code = code;

                            if (outputIndex != OUTPUT_INDEX_TXT)
                            {
                                string strTmp = MySource.EncodingGetString(showCodePage, code);
                                Bitmap bitmap = PictureDrawChar(strTmp, false);
                                index_tmp.width = (ushort)bitmap.Width;
                                index_tmp.offset = (uint)(SCAN_HEADER_LENGTH + SCAN_INFO_LENGTH * scan_section_max + SCAN_INDEX_LENGTH * scan_count_max + scan_data_max);

                                bitmap.RotateFlip(GetRotateFlip(config.Rotate, config.FlipX, config.FlipY));
                                bitmap.RotateFlip(GetRotateFlip(0, showScanX, showScanY));
                                if (showScanXY)
                                {
                                    bitmap.RotateFlip(GetRotateFlip(90, true, false));
                                }

                                bytes = BitmapToBits(bitmap, showMsbFirst, showLineRound, showHighPolarity, config.GrayBits);
                                index_tmp.size = (ushort)bytes.Length;
                                saveListData.Add(new FontData(code, bytes));
                            }

                            scan_char_max++;
                            scan_data_max += bytes.Length;

                            saveListIndex.Add(index_tmp);
                        }

                        scan_count++;
                        progressBarMake.Value = scan_count * 99 / scan_count_max;

                        if (scan_index == showCodeSectionList[scan_section].last)
                        {
                            scan_section++;
                            if (scan_section >= showCodeSectionList.Count)
                            {
                                if (outputIndex == OUTPUT_INDEX_CLNG) // .c
                                {
                                    saveListHeader.total_size = (uint)scan_data_max;
                                    saveListHeader.total_chars = (ushort)scan_char_max;

                                    SaveFileToClng(outputFileName);
                                }
                                else if (outputIndex == OUTPUT_INDEX_TXT) // .txt
                                {
                                    SaveFileToTxt(outputFileName);
                                }
                                else // .fnt
                                {
                                    saveListHeader.total_size = (uint)(SCAN_HEADER_LENGTH + SCAN_INFO_LENGTH * scan_section_max + SCAN_INDEX_LENGTH * scan_count_max + scan_data_max);
                                    saveListHeader.total_chars = (ushort)scan_char_max;

                                    SaveFileToFnt(outputFileName);
                                }

                                timerScan.Enabled = false;
                                progressBarMake.Value = 100;
                            }
                            else
                            {
                                scan_index = showCodeSectionList[scan_section].first;
                            }
                        }
                        else
                        {
                            scan_index++;
                        }

                        retry = 0;
                        Application.DoEvents();
                    }
                    catch
                    {
                        GC.Collect();

                        retry++;
                        if (retry >= 3)
                        {
                            timerScan.Enabled = false;
                            break;
                        }
                    }
                }

                buttonStart.Text = "生成字库";
                textBoxChar.Enabled = true;
                numericUpDownCode.Enabled = true;
                propertyGridFont.Enabled = true;

                saveListSection.Clear();
                saveListIndex.Clear();
                saveListData.Clear();

                GC.Collect();
                timer_busy = false;
            }
        }

        public struct FontHeader
        {
            // uint8_t magic[4]; /* "FNT" + X */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] magic;
            // uint32_t style;	/* the font style */
            public uint style;
            // uint16_t height; /* the font height */
            public ushort height;
            // uint16_t codepage; /* 936 GB2312, 1200 Unicode */
            public ushort codepage;
            // uint8_t padding[4]; /* left, top, right, bottom padding */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] padding;
            // uint16_t total_sections; /* total sections */
            public ushort total_sections;
            // uint16_t total_chars; /* total characters */
            public ushort total_chars;
            // uint32_t total_size; /* file total size or data total size */
            public uint total_size;
        }

        public struct FontSection
        {
            // uint16_t first; /* first character */
            public ushort first;
            // uint16_t last; /* last character */
            public ushort last;
            // uint32_t offset; /* the first font_index offset */
            public uint offset;
        }

        public struct FontIndex
        {
            public ushort code;

            // uint16_t width; /* the width of the character */
            public ushort width;
            // uint16_t size; /* the bitmap data size */
            public ushort size;
            // uint32_t offset; /* the font bitmap data offset */
            public uint offset;
        }

        public class FontData
        {
            public ushort code;
            public byte[] data;

            public FontData()
            {
            }

            public FontData(ushort code, byte[] data)
            {
                this.code = code;
                this.data = data;
            }
        }

        private void slab_info_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.付坤.中国");  // 在浏览器中打开链接
        }

        private void labelHelp_Click(object sender, EventArgs e)
        {
            frmHelp help = new frmHelp();

            help.ShowDialog();
        }

        private void labelExport_Click(object sender, EventArgs e)
        {
            //config.FontType
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
