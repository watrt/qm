using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace FontMaster
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct FIXED
    {
        /// WORD->unsigned short
        public ushort fract;
        /// short
        public short value;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct MAT2
    {
        /// FIXED->_FIXED
        public FIXED eM11;
        /// FIXED->_FIXED
        public FIXED eM12;
        /// FIXED->_FIXED
        public FIXED eM21;
        /// FIXED->_FIXED
        public FIXED eM22;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct GLYPHMETRICS
    {
        /// UINT->unsigned int
        public uint gmBlackBoxX;
        /// UINT->unsigned int
        public uint gmBlackBoxY;
        /// POINT->tagPOINT
        public Point gmptGlyphOrigin;
        /// short
        public short gmCellIncX;
        /// short
        public short gmCellIncY;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Point
    {
        /// LONG->int
        public int x;
        /// LONG->int
        public int y;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct TEXTMETRICA
    {
        /// LONG->int
        public int tmHeight;
        /// LONG->int
        public int tmAscent;
        /// LONG->int
        public int tmDescent;
        /// LONG->int
        public int tmInternalLeading;
        /// LONG->int
        public int tmExternalLeading;
        /// LONG->int
        public int tmAveCharWidth;
        /// LONG->int
        public int tmMaxCharWidth;
        /// LONG->int
        public int tmWeight;
        /// LONG->int
        public int tmOverhang;
        /// LONG->int
        public int tmDigitizedAspectX;
        /// LONG->int
        public int tmDigitizedAspectY;
        /// BYTE->unsigned char
        public byte tmFirstChar;
        /// BYTE->unsigned char
        public byte tmLastChar;
        /// BYTE->unsigned char
        public byte tmDefaultChar;
        /// BYTE->unsigned char
        public byte tmBreakChar;
        /// BYTE->unsigned char
        public byte tmItalic;
        /// BYTE->unsigned char
        public byte tmUnderlined;
        /// BYTE->unsigned char
        public byte tmStruckOut;
        /// BYTE->unsigned char
        public byte tmPitchAndFamily;
        /// BYTE->unsigned char
        public byte tmCharSet;
    }

    public struct FontRange
    {
        public UInt16 Low;
        public UInt16 High;
    }

    class NativeMethods
    {
        public const int GGO_METRICS = 0;
        public const int GGO_BITMAP = 1;
        public const int GGO_NATIVE = 2;
        public const int GGO_BEZIER = 3;
        public const int GGO_GRAY2_BITMAP = 4;
        public const int GGO_GRAY4_BITMAP = 5;
        public const int GGO_GRAY8_BITMAP = 6;

        /// Return Type: HGDIOBJ->void*
        ///hdc: HDC->HDC__*
        ///h: HGDIOBJ->void*
        [DllImportAttribute("gdi32.dll", EntryPoint = "SelectObject")]
        public static extern System.IntPtr SelectObject([InAttribute()] System.IntPtr hdc, [InAttribute()] System.IntPtr h);

        /// Return Type: BOOL->int
        ///ho: HGDIOBJ->void*
        [DllImportAttribute("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool DeleteObject([InAttribute()] System.IntPtr ho);

        /// Return Type: DWORD->unsigned int
        ///hdc: HDC->HDC__*
        ///uChar: UINT->unsigned int
        ///fuFormat: UINT->unsigned int
        ///lpgm: LPGLYPHMETRICS->_GLYPHMETRICS*
        ///cjBuffer: DWORD->unsigned int
        ///pvBuffer: LPVOID->void*
        ///lpmat2: MAT2*
        [DllImportAttribute("gdi32.dll", EntryPoint = "GetGlyphOutlineW")]
        public static extern int GetGlyphOutlineW(System.IntPtr hdc, uint uChar, uint fuFormat, out GLYPHMETRICS lpgm, int cjBuffer, System.IntPtr pvBuffer, ref MAT2 lpmat2);

        /// Return Type: BOOL->int
        ///hdc: HDC->HDC__*
        ///lptm: LPTEXTMETRICA->tagTEXTMETRICA*
        [DllImportAttribute("gdi32.dll", EntryPoint = "GetTextMetrics")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool GetTextMetrics(System.IntPtr hdc, out TEXTMETRICA lptm);


        [DllImport("gdi32.dll")]
        public static extern uint GetFontUnicodeRanges(IntPtr hdc, IntPtr lpgs);
    }
}
