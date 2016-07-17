using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wacom.FeelMultiTouch
{

    class MTAPI
    {
        /// <summary>
        /// APIバージョン
        /// </summary>
        public const int Version = 4;

        /** ****************************************************************************************************************
         * アンマネージドコードの定義
         * 
         * 以下のメソッドはwacommt.dllからインポートしています。
         * wacommt.dllがインストールされていない場合は例外が発生するので、タブレットドライバをインストールしてください。
         * _[メソッド名]で定義しているものは、「_」を抜いたものを使いやすいように定義しています。
         * 
         * *****************************************************************************************************************
         */

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="libraryAPIVersion"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTInitialize")]
        protected static extern WacomMTError _WacomMTInitialize(Int32 libraryAPIVersion);

        /// <summary>
        /// 終了処理
        /// </summary>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WacomMTQuit();

        /// <summary>
        /// 接続しているタブレットデバイスのIDを収得
        /// </summary>
        /// <param name="deviceArray"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTGetAttachedDeviceIDs")]
        protected static extern int _WacomMTGetAttachedDeviceIDs(IntPtr deviceArray, UInt32 bufferSize);

        /// <summary>
        /// タブレットデバイスの情報取得
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="capabilityBuffer"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTGetDeviceCapabilities")]
        protected static extern WacomMTError _WacomMTGetDeviceCapabilities(int deviceID, IntPtr capabilityBuffer);

        // **** 以下追加コード

        /// <summary>
        /// フィンガーデータ受信するウインドウハンドルを設定
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="mode"></param>
        /// <param name="hWnd"></param>
        /// <param name="bufferDepth"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTRegisterFingerReadHWND")]
        public static extern WacomMTError WacomMTRegisterFingerReadHWND(int deviceID, WacomMTProcessingMode mode, IntPtr hWnd, int bufferDepth);

        /// <summary>
        /// フィンガーデータを受信するウインドウハンドルを解除
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTUnRegisterFingerReadHWND")]
        public static extern WacomMTError WacomMTUnRegisterFingerReadHWND(IntPtr hwnd);

        /// <summary>
        /// フィンガーデータを受け取るコールバックの設定
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="hitRect"></param>
        /// <param name="mode"></param>
        /// <param name="fingerCallback"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTRegisterFingerReadCallback")]
        public static extern WacomMTError WacomMTRegisterFingerReadCallback(int deviceID, IntPtr hitRect, WacomMTProcessingMode mode, [MarshalAs(UnmanagedType.FunctionPtr)] WMT_FINGER_CALLBACK fingerCallback, IntPtr userData);

        /// <summary>
        /// フィンガーデータを受け取るコールバックの解除
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="hitRect"></param>
        /// <param name="mode"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        [DllImport("wacommt.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTUnRegisterFingerReadCallback")]
        public static extern WacomMTError WacomMTUnRegisterFingerReadCallback(int deviceID, IntPtr hitRect, WacomMTProcessingMode mode, IntPtr userData);

        /** ****************************************************************************************************************
         * アンマネージドのメソッドはポインタをIntPtrで表しているため、IntPtr関連の処理を内部を行うようにしたメソッドを作っています。
         * *****************************************************************************************************************
         */

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns>初期化の結果</returns>
        public static WacomMTError WacomMTInitialize()
        {
            // 定義しているバージョンを指定する
            return _WacomMTInitialize(Version);
        }

        /// <summary>
        /// 接続しているタブレットのID取得
        /// </summary>
        /// <returns>認識しているタブレットのID配列</returns>
        public static int[] WacomMTGetAttachedDeviceIDs()
        {
            int[] idArray = new int[0];
            int res = _WacomMTGetAttachedDeviceIDs(IntPtr.Zero, 0);

            if (res != 0)
            {
                IntPtr pIdArray = IntPtr.Zero;

                try
                {
                    pIdArray = MemoryUtil.AllocUnmanagedBuffer(typeof(int), res);
                    res = _WacomMTGetAttachedDeviceIDs(pIdArray, sizeof(int) * (UInt32)res);
                    idArray = MemoryUtil.MarshalUnmanagedBufferArray<int>(pIdArray, (uint)res);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    MemoryUtil.FreeUnmanagedBuffer(pIdArray);
                }
            }

            return idArray;
        }

        /// <summary>
        /// タブレットデバイスの情報取得
        /// </summary>
        /// <param name="deviceID">タブレットデバイスのID</param>
        /// <returns>タブレットデバイスの情報</returns>
        public static WacomMTCapability WacomMTGetDeviceCapabilities(int deviceID)
        {
            IntPtr capabilitiesPtr = IntPtr.Zero;
            WacomMTCapability capability;

            try
            {
                capabilitiesPtr = MemoryUtil.AllocUnmanagedBuffer(typeof(WacomMTCapability));
                WacomMTError res = _WacomMTGetDeviceCapabilities(deviceID, capabilitiesPtr);

                if (res != WacomMTError.WMTErrorSuccess)
                {
                    // 失敗した場合は例外を発生させる
                    throw new Exception("WacomMTGetDeviceCapabilities : " + res);
                }

                capability = (WacomMTCapability)MemoryUtil.MarshalUnmanagedBuffer(capabilitiesPtr, typeof(WacomMTCapability));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                MemoryUtil.FreeUnmanagedBuffer(capabilitiesPtr);
            }

            return capability;
        }

    }

}
