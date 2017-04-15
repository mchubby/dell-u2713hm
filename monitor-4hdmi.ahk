; Change Monitor Input Source by lifeweaver
; http://www.autohotkey.com/board/topic/96884-change-monitor-input-source/

; Finds monitor handle
getMonitorHandle()
{
  ; Initialize Monitor handle
  hMon := DllCall("MonitorFromPoint"
    , "int64", 0 ; point on monitor
    , "uint", 1) ; flag to return primary monitor on failure

    
  ; Get Physical Monitor from handle
  VarSetCapacity(Physical_Monitor, 8 + 256, 0)

  DllCall("dxva2\GetPhysicalMonitorsFromHMONITOR"
    , "int", hMon   ; monitor handle
    , "uint", 1   ; monitor array size
    , "int", &Physical_Monitor)   ; point to array with monitor

  return hPhysMon := NumGet(Physical_Monitor)
}

destroyMonitorHandle(handle)
{
  DllCall("dxva2\DestroyPhysicalMonitor", "int", handle)
}

; Used to change the monitor source
; DVI = 3
; HDMI = 4
; YPbPr = 12
setMonitorInputSource(source)
{
  handle := getMonitorHandle()
  DllCall("dxva2\SetVCPFeature"
    , "int", handle
    , "char", 0x60 ;VCP code for Input Source Select
    , "uint", source)
  destroyMonitorHandle(handle)
}

; Gets Monitor source
getMonitorInputSource()
{
  handle := getMonitorHandle()
  DllCall("dxva2\GetVCPFeatureAndVCPFeatureReply"
    , "int", handle
    , "char", 0x60 ;VCP code for Input Source Select
    , "Ptr", 0
    , "uint*", currentValue
    , "uint*", maximumValue)
  destroyMonitorHandle(handle)
  return currentValue
}

; on = 1
; stby = 4
; ? = 5
setDpmsControl(cmd)
{
  handle := getMonitorHandle()
  DllCall("dxva2\SetVCPFeature"
    , "int", handle
    , "char", 0xd6
    , "uint", cmd)
  destroyMonitorHandle(handle)
}

getDpmsControl()
{
  DllCall("dxva2\GetVCPFeatureAndVCPFeatureReply"
    , "int", handle
    , "char", 0xd6 ;Power Mode
    , "Ptr", 0
    , "uint*", currentValue
    , "uint*", maximumValue)
  destroyMonitorHandle(handle)
  return currentValue
}

; if powered off, bring monitor on
pwr := getDpmsControl()
if(pwr != 1)
  setDpmsControl(1)

; wait for control to be relinquished
while(getMonitorInputSource() = 0)
  Sleep, 500

setMonitorInputSource(4)

