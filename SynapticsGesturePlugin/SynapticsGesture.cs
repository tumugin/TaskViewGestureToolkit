using PluginBaseLib;
using SYNCTRLLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynapticsGesturePlugin
{
    public class SynapticsGesture : PluginBase
    {
        private SynAPICtrl synApi;
        private SynDeviceCtrl synTouchPad;
        private int deviceHandle;
        private int[] lastState = {114514,114514};
        //for position
        private int[] startPos;
        private int[] endPos;
        private class synPos{ public int nof, fstate, xd, yd, x, y; }
        private synPos lastPos = new synPos();
        private bool isMoving = false;
        Mutex mx = new Mutex();

        public override string pluginName
        {
            get
            {
                return "SynapticsGesturePlugin";
            }
        }

        public override void deletePlugin()
        {
            synTouchPad.OnPacket -= SynTouchPad_OnPacket;
            synTouchPad.Deactivate();
            synApi.Deactivate();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(synTouchPad);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(synApi);
        }

        public override void initializePlugin()
        {
            synApi = new SynAPICtrl();
            synTouchPad = new SynDeviceCtrl();
            synApi.Initialize();
            synApi.Activate();
            deviceHandle = synApi.FindDevice(SynConnectionType.SE_ConnectionAny,SynDeviceType.SE_DeviceTouchPad,-1);
            synTouchPad.Select(deviceHandle);
            synTouchPad.Activate();
            synTouchPad.OnPacket -= SynTouchPad_OnPacket;
            synTouchPad.OnPacket += SynTouchPad_OnPacket;
        }

        private void SynTouchPad_OnPacket()
        {
            mx.WaitOne();
            SynPacketCtrl packet = new SynPacketCtrl();
            synTouchPad.LoadPacket(packet);
            int nof = packet.GetLongProperty(SynPacketProperty.SP_ExtraFingerState) & 3;
            int fstate = packet.GetLongProperty(SynPacketProperty.SP_FingerState);
            int xd = packet.GetLongProperty(SynPacketProperty.SP_XDelta);
            int yd = packet.GetLongProperty(SynPacketProperty.SP_YDelta);
            int y = packet.GetLongProperty(SynPacketProperty.SP_Y);
            int x = packet.GetLongProperty(SynPacketProperty.SP_X);
            if (lastState.SequenceEqual(new int[] { nof, fstate, xd, yd, y, x }))
            {
                mx.ReleaseMutex();
                return;
            }
            //Debug.WriteLine($"nof={nof},fstate={fstate},xd={xd},yd={yd},x={x},y={y}");
            //nof=0 AND fstate=0 is invalid value for captureing
            if (fstate != 0 && nof != 0)
            {
                if (nof == 3)
                {
                    if (!isMoving)
                    {
                        startPos = new int[] { x, y };
                        isMoving = true;
                        callOnGesture(x, y, EventType.OnGestureStart);
                        //Debug.WriteLine($"[START] nof={nof},fstate={fstate},xd={xd},yd={yd},x={x},y={y}");
                    }
                    lastPos = new synPos { nof = nof,fstate = fstate,xd=xd,yd=yd,x=x,y=y };
                }
                else if (nof < 3 && isMoving)
                {
                    endPos = new int[] { x, y };
                    isMoving = false;
                    callOnGesture(x, y, EventType.OnGestureEnd);
                    //Debug.WriteLine($"[END] nof={nof},fstate={fstate},xd={xd},yd={yd},x={x},y={y}");
                }
            }else if(nof == 0 && isMoving)
            {
                endPos = new int[] { lastPos.x, lastPos.y };
                isMoving = false;
                callOnGesture(lastPos.x, lastPos.y, EventType.OnGestureEnd);
                //Debug.WriteLine($"[END] nof={lastPos.nof},fstate={lastPos.fstate},xd={lastPos.xd},yd={lastPos.yd},x={lastPos.x},y={lastPos.y}");
            }
            lastState = new int[] { nof, fstate, xd, yd, y, x };
            mx.ReleaseMutex();
        }
    }
}
