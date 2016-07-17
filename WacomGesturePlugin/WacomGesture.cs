using PluginBaseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wacom;
using Wacom.FeelMultiTouch;

namespace WacomGesturePlugin
{
    public class WacomGesture : PluginBase
    {
        class WacomTab
        {
            private WacomGesture parent;
            public int deviceID;
            public IntPtr mHitRectPtr = IntPtr.Zero;
            private bool isMoving = false;
            private WacomMTFinger[] lastFingers;

            public WacomTab(WacomGesture wg)
            {
                parent = wg;
            }
            public int wmtFingerCallback(IntPtr fingerPacket, IntPtr userData)
            {
                WacomMTFingerCollection collection = (WacomMTFingerCollection)MemoryUtil.MarshalUnmanagedBuffer(fingerPacket, typeof(WacomMTFingerCollection));
                WacomMTFinger[] fingers = MemoryUtil.MarshalUnmanagedBufferArray<WacomMTFinger>(collection.FingerData, (uint)collection.FingerCount);
                if (fingers.Count() != 0) lastFingers = fingers;
                if(fingers.Count() == 4 && !isMoving)
                {
                    //gesture start
                    isMoving = true;
                    parent.callOnGesture(Utils.getFingerAvgPoint(fingers)[0], Utils.getFingerAvgPoint(fingers)[1],EventType.OnGestureStart);
                }else if(isMoving && fingers.Count() < 4 && fingers.Count() != 0)
                {
                    //gesture end
                    isMoving = false;
                    parent.callOnGesture(Utils.getFingerAvgPoint(fingers)[0], Utils.getFingerAvgPoint(fingers)[1], EventType.OnGestureEnd);
                }
                else if(isMoving && fingers.Count() == 0)
                {
                    //get last point
                    isMoving = false;
                    parent.callOnGesture(Utils.getFingerAvgPoint(lastFingers)[0], Utils.getFingerAvgPoint(lastFingers)[1], EventType.OnGestureEnd);
                }
                return 0;
            }
        }
        private List<WacomTab> wacomTabList = new List<WacomTab>();

        public override string pluginName
        {
            get
            {
                return "WacomGesturePlugin";
            }
        }

        public override void deletePlugin()
        {
            foreach (var tablet in wacomTabList)
            {
                MTAPI.WacomMTUnRegisterFingerReadCallback(tablet.deviceID, tablet.mHitRectPtr, WacomMTProcessingMode.WMTProcessingModeObserver, IntPtr.Zero);
                MemoryUtil.FreeUnmanagedBuffer(tablet.mHitRectPtr);
            }
            wacomTabList.Clear();
        }

        public override void initializePlugin()
        {
            WacomMTError res = MTAPI.WacomMTInitialize();
            if (res != WacomMTError.WMTErrorSuccess) return;
            int[] mIdAry = MTAPI.WacomMTGetAttachedDeviceIDs();
            foreach (var id in mIdAry)
            {
                WacomMTHitRect hr;
                hr.originX = 0;
                hr.originY = 0;
                hr.width = 1920f;
                hr.height = 1080f;
                WacomTab wt = new WacomTab(this) { deviceID = id };
                wt.mHitRectPtr = MemoryUtil.AllocUnmanagedBuffer(hr);
                MTAPI.WacomMTRegisterFingerReadCallback(id, wt.mHitRectPtr, WacomMTProcessingMode.WMTProcessingModeObserver, new WMT_FINGER_CALLBACK(wt.wmtFingerCallback), IntPtr.Zero);
                wacomTabList.Add(wt);
            }
        }
    }
}