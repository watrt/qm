import os
import struct
str="好今天奇才奇才一"
f=open("st12_u.fnt","rb")
buf=f.read(24)
print(buf)
magic,ver,height,codepage,total_sections,total_chars,total_size = struct.unpack("3s1B4xHH4xHHL",buf)
print(magic)
print(ver)
print(height)
print(codepage)
print(total_sections)
print(total_chars)
print(total_size)
fontsize=12                 #字大小
startadd=95*fontsize        #开头的英文汉字只站一半的位置
for s in str:
    f.seek((ord(s)-0x4E00)*2*fontsize+startadd)
    buf=f.read(2*fontsize)
    print(s,":")
    for i in range(fontsize):
        code=0x00
        code |= buf[i*2]<<8
        code |= buf[i*2+1]
        fontmatrix="{:016b}".format(code)
        fontmatrix=fontmatrix.replace("0","_")
        fontmatrix=fontmatrix.replace("1","X")
        print(fontmatrix)

