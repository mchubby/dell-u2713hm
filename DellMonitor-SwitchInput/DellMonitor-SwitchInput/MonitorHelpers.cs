using System;
using static DellMonitor_SwitchInput.NativeStructures;
using static DellMonitor_SwitchInput.NativeMethods;


namespace DellMonitor_SwitchInput
{
    public class MonitorHelpers
    {
        public class HmonitorWrapper : IDisposable
        {
            PHYSICAL_MONITOR handle;
            public HmonitorWrapper()
            {
                handle = getMonitorHandle();
            }
            public void Dispose()
            {
                destroyMonitorHandle(handle.hPhysicalMonitor);  // probably leaks, should call DestroyPhysicalMonitors instead
            }

            public static explicit operator IntPtr(HmonitorWrapper wrapper)
            {
                return wrapper.handle.hPhysicalMonitor;
            }

        }

        /// <summary>
        /// get a handle for top-left monitor (usually main monitor)
        /// using native call dxva2/MonitorFromPoint
        /// </summary>
        /// <returns>a PHYSICAL_MONITOR struct, whose hPhysicalMonitor is an HMONITOR / IntPtr</returns>
        public static PHYSICAL_MONITOR getMonitorHandle()
        {
            // Initialize Monitor handle
            IntPtr hMon = MonitorFromPoint(
                new tagPOINT(0, 0),  // point on monitor
                1); //flag to return primary monitor on failure

            // Get Physical Monitor from handle
            PHYSICAL_MONITOR[] pPhysicalMonitorArray = new PHYSICAL_MONITOR[8+256];
            GetPhysicalMonitorsFromHMONITOR(
                hMon, // monitor handle
                1,  // monitor array size
                pPhysicalMonitorArray);  // point to array with monitor
            return pPhysicalMonitorArray[0];  // probably leaky, ahem
        }

        /// <summary>
        /// Dispose resource handle
        /// using native call dxva2/DestroyPhysicalMonitor
        /// </summary>
        /// <param name="hMon"></param>
        public static void destroyMonitorHandle(IntPtr hMon)
        {
            DestroyPhysicalMonitor(hMon);
        }

        /// <summary>
        /// Used to change the monitor source
        /// using native call dxva2/SetVCPFeature
        /// and VCP code 0x60-Input Source Select
        /// </summary>
        /// <param name="source">DVI = 3
        /// HDMI = 4
        /// YPbPr = 12
        /// </param>
        public static void setMonitorInputSource(uint source)
        {
            using (var hWrapper = new HmonitorWrapper())
            {
                SetVCPFeature(
                    (IntPtr)hWrapper,
                    0x60U,  // VCP code for Input Source Select
                    source);
            }
        }

        /// <summary>
        /// Gets Monitor source
        /// using native call dxva2/GetVCPFeatureAndVCPFeatureReply
        /// and VCP code 0x60-Input Source Select
        /// </summary>
        /// <returns>See setMonitorInputSource() for source definitions</returns>
        public static uint getMonitorInputSource()
        {
            uint currentValue = 0;
            uint maximumValue = 0;
            using (var hWrapper = new HmonitorWrapper())
            {
                GetVCPFeatureAndVCPFeatureReply(
                    (IntPtr)hWrapper,
                    0x60U,  // VCP code for Input Source Select
                    0U,
                    ref currentValue,
                    ref maximumValue);
            }
            return currentValue;
        }

        /// <summary>
        /// Runs a Dpms (power) command
        /// using native call dxva2/SetVCPFeature
        /// and VCP code 0xd6-Power Mode
        /// </summary>
        /// <param name="command">undefined = 0
        /// on = 1
        /// stby = 4
        /// phy_off = 5</param>
        public static void setDpmsControl(uint command)
        {
            using (var hWrapper = new HmonitorWrapper())
            {
                SetVCPFeature(
                    (IntPtr)hWrapper,
                    0xD6U,  // VCP code for Power Mode
                    command);
            }
        }


        /// <summary>
        /// Get Dpms (power) status
        /// using native call dxva2/GetVCPFeatureAndVCPFeatureReply
        /// and VCP code 0xd6-Power Mode
        /// </summary>
        /// <returns>See setDpmsControl() for status definitions</returns>
        public static uint getDpmsControl()
        {
            uint currentValue = 0;
            uint maximumValue = 0;
            using (var hWrapper = new HmonitorWrapper())
            {
                GetVCPFeatureAndVCPFeatureReply(
                    (IntPtr)hWrapper,
                    0xD6U,  // VCP code for Power Mode
                    0U,
                    ref currentValue,
                    ref maximumValue);
            }
            return currentValue;
        }
    }
}
