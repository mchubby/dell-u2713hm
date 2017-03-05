using System;
using System.Runtime.InteropServices;

namespace DellMonitor_SwitchInput
{
    public class NativeStructures
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct PHYSICAL_MONITOR
        {
            public IntPtr hPhysicalMonitor;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szPhysicalMonitorDescription;
        }

        public struct tagPOINT
        {
            public int x;
            public int y;
            public tagPOINT(int ptx, int pty)
            {
                x = ptx; y = pty;
            }
        };
#if UNUSED
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
#endif
    }
}