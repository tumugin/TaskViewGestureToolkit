using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskViewGestureToolkit.UI
{
    public class StockIcon
    {
        public const UInt32 SHGSI_ICON = 0x000000100;
        public const UInt32 SHGSI_SMALLICON = 0x000000001;

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            public Int32 cbSize;
            public IntPtr hIcon;
            public Int32 iSysImageIndex;
            public Int32 iIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern void SHGetStockIconInfo(UInt32 siid, UInt32 uFlags, ref SHSTOCKICONINFO sii);

        public static Icon getStackIcon()
        {
            SHSTOCKICONINFO sii = new SHSTOCKICONINFO();
            sii.cbSize = Marshal.SizeOf(sii);
            SHGetStockIconInfo(55, SHGSI_ICON, ref sii);
            return (sii.hIcon != IntPtr.Zero) ? Icon.FromHandle(sii.hIcon) : null;
        }
    }
}
