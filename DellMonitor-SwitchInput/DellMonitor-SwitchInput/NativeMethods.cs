using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DellMonitor_SwitchInput
{
    public class NativeMethods
    {
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr MonitorFromWindow(
            [In] IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr MonitorFromPoint(
            [In] NativeStructures.tagPOINT pt, uint dwFlags);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(IntPtr hMonitor, ref uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor,
            uint dwPhysicalMonitorArraySize,
            [Out] NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyPhysicalMonitors(
            uint dwPhysicalMonitorArraySize, [Out] NativeStructures.PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        public static extern void DestroyPhysicalMonitor(IntPtr hMonitor);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DDCCIGetCapabilitiesStringLength(
            [In] IntPtr hMonitor, ref uint pdwLength);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DDCCIGetCapabilitiesString(
            [In] IntPtr hMonitor, StringBuilder pszString, uint dwLength);


        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DDCCIGetVCPFeature(
            [In] IntPtr hMonitor, [In] uint dwVCPCode, uint pvct, ref uint pdwCurrentValue, ref uint pdwMaximumValue);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVCPFeatureAndVCPFeatureReply(
            [In] IntPtr hMonitor, [In] uint dwVCPCode, uint pvct, ref uint pdwCurrentValue, ref uint pdwMaximumValue);


        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetVCPFeature(
            [In] IntPtr hMonitor, uint dwVCPCode, uint dwNewValue);
        #region unused stuff
#if UNUSED
        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeStructures.Rect lprcMonitor, IntPtr dwData);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorCapabilities(IntPtr hMonitor, ref uint pdwMonitorCapabilities, ref uint pdwSupportedColorTemperatures);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorBrightness(IntPtr hMonitor, ref uint pdwMinimumBrightness, ref uint pdwCurrentBrightness, ref uint pdwMaximumBrightness);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorContrast(IntPtr hMonitor, ref uint pdwMinimumContrast, ref uint pdwCurrentContrast, ref uint pdwMaximumContrast);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorColorTemperature(IntPtr hMonitor, ref uint pdwMinimumBrightness, ref uint pdwSupportedColorTemperaturespdwCurrentBrightness);

        [DllImport("dxva2.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetMonitorBrightness(IntPtr hMonitor, uint dwNewBrightness);
#endif
        #endregion
    }
}
