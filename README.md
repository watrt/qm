# FontMaster

#### 项目介绍
1、字体设置 <br>
    FontType 字体类型设置，可以选择系统字体或者其他字体，在选择系统字体的时候，可以通过Font属性选择字体名称，在选择其他字体的时候，可以通过Font属性选择ttf字库文件的路径。
    Font 字体名称或者字体路径设置。
    Size 字体大小设置，一般情况下，字体大小和字体高度是相同的。
    Blod 字体粗体选择。
    Italic 字体斜体选择。
    CharCode 字符编码选择，影响字符集的大小，也影响生成字库的编码顺序。可以选择MBCS多字节编码方式，此方式一般为计算机本地化后的内码，或者选择Unicode编码方式，此方式编码的区间更大，可以兼容更多的字符集。

2、取模设置 <br>
    Rotate 取模顺时针旋转角度，可以选择0，90，180，270度。注意这个设置不影响生成字库的字模宽度！生成字库的字模宽度始终为未旋转之前的宽度。
    FlipX 取模X方向镜像。
    FlipY 取模Y方向镜像。
    Padding 分别设置在取模上下左右额外扩充的空白数。

3、编码设置 <br>
    SourceType 选择编码的设置方式，可以选择分段编码或者索引表的方式。通过Source设置具体的分段或者索引表
    Source 打开编码内容设置对话框，设置编码内容。在分段编码时，设置分段编码的范围，以“,”分隔，如 “0x0020-0x007F,0xB0:A1-0xF7:FE”。在索引表方式时，直接输入需要编码的字符串即可，如“测试编码”。在输入完成后，可以使用排序查重对输入内容进行检查。
    BitsFirst 设置字节内编码顺序是MSBFirst或者LSBFist。
    BitsPolariry 设置字节内编码的位极性，可以为高有效或低有效
    Round 设置编码的回转方式，可以设置为在一行编码完成后回转到下一行编码或者在编码完成一个字节后，就回转到下一行编码，最后一行编码完成后，再次回到第一行编码，如此往复。
    ScanX 水平扫描方式，可以选择“从左到右”或者“从右到左”。
    ScanY 垂直扫描方式，可以选择“从上到下”或者从下到上”。
    ScanXY 设置扫描先后顺序，可以选择“先水平后垂直”或者“先垂直后水平”。

4、数据结构 <br>
    typedef unsigned char   uint8_t; <br>
	typedef unsigned short  uint16_t; <br>
	typedef unsigned long   uint32_t; <br>

	#define FONT_STYLE_BLOD             0x0001 /* bit0 1~Blod */
	#define FONT_STYLE_ITALIC           0x0002 /* bit1 1~Italic */
	#define FONT_STYLE_ROTATE           0x0030 /* bit5~4 Rotate 0~0,1~90... */
	#define FONT_STYLE_FLIPX            0x0040 /* bit6 1~FlipX */
	#define FONT_STYLE_FLIPY            0x0080 /* bit7 1~FlipY */
	#define FONT_STYLE_MSB_FIRST        0x0100 /* bit8 0~LSBFirst,1~MSBFirst */
	#define FONT_STYLE_HIGH_POLARITY    0x0200 /* bit9 0~LowPolarity,1~HighPolarity */
	#define FONT_STYLE_LINE_ROUND       0x0400 /* bit10 0~ByteRound,1~LineRound */
	#define FONT_STYLE_SCANX            0x1000 /* bit12 0~Left to Right,1~Right to Left */
	#define FONT_STYLE_SCANY            0x2000 /* bit 13 0~Top to Bottom,1~Bottom to Top */
	#define FONT_STYLE_SCANXY           0x4000 /* bit14 0~Horizontal then Vertical,1~Vertical then Horizontal */

	#define FONT_ROTATE_0       0x0000 /* bit5~4 Rotate 0~0,1~90... */
	#define FONT_ROTATE_90      0x0010
	#define FONT_ROTATE_180     0x0020
	#define FONT_ROTATE_270     0x0030

	typedef struct _font_header
	{
		uint8_t magic[4]; /* "FNT" + X */
		uint16_t style; /* the font style */
		uint16_t height; /* the font height */
		uint16_t codepage; /* 936 GB2312, 1200 Unicode */
		uint8_t padding[4]; /* left, top, right, bottom padding */

		uint16_t total_sections; /* total sections */
		uint16_t total_chars; /* total characters */
		uint32_t total_size; /* file total size or data total size */
	} font_header_t;

	typedef struct _font_section
	{
		uint16_t first; /* first character */
		uint16_t last; /* last character */
		uint32_t offset; /* the first font_index offset */
	} font_section_t;

	typedef struct _font_index
	{
		uint16_t width; /* the width of the character */
		uint32_t offset; /* the font bitmap data offset */
	} font_index_t;

