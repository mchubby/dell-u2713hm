using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using static DellMonitor_SwitchInput.NativeStructures;
using static DellMonitor_SwitchInput.MonitorHelpers;
/// <summary>
/// Ported Change Monitor Input Source by lifeweaver
/// http://www.autohotkey.com/board/topic/96884-change-monitor-input-source/
/// auto switch hdmi<-> displayport on Dell UltraSharp U2713HM
/// https://github.com/mchubby/dell-u2713hm
/// </summary>
namespace DellMonitor_SwitchInput
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += AppStartup;
        }

        internal static Task Task_DoWorkAsync()
        {
            return Task.Run(() =>
            {
                // if powered off, bring monitor on
                if (getDpmsControl() != 0x01U)
                {
                    setDpmsControl(0x01U);
                }

                // wait for control to be relinquished
                while (getMonitorInputSource() == 0U)
                {
                    Task.Delay(500);
                }

                if (getMonitorInputSource() != 0x04U)
                {
                    setMonitorInputSource(0x04U);
                }
                else
                {
                    setMonitorInputSource(0x0FU);  // 15 is DisplayPort
                }
            });
        }

    async void AppStartup(object sender, StartupEventArgs e)
        {
            await Task_DoWorkAsync();
            Current.Shutdown();
        }
    }
}
