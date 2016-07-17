using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wacom.FeelMultiTouch
{

    public class WindowMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public const int WM_FINGERDATA  = 0x6205;

        /// <summary>
        /// 
        /// </summary>
        public const int WM_BLOBDATA    = 0x6206;

        /// <summary>
        /// 
        /// </summary>
        public const int WM_RAWDATA     = 0x6207;
    }

    /// <summary>
    /// MultitouchAPI関数の戻り値
    /// </summary>
    public enum WacomMTError : int
    {
        WMTErrorSuccess         = 0,    // 呼び出しが成功
        WMTErrorDriverNotFound  = 1,    // ドライバが見つからない
        WMTErrorBadVersion      = 2,    // ドライバとAPIの互換性がない
        WMTErrorAPIOutdated     = 3,    // ドライバでサポートされていないデータ構造を要求
        WMTErrorInvalidParam    = 4,    // 無効なパラメータが指定された
        WMTErrorQuit            = 5,    // APIが正常に初期化されなかった
        WMTErrorBufferTooSmall  = 6,    // 指定したバッファが小さすぎる
    }

    /// <summary>
    /// デバイスの種別
    /// </summary>
    public enum WacomMTDeviceType : int
    {
        WMTDeviceTypeOpaque     = 0,    // 液晶が組み込まれていないタブレット
        WMTDeviceTypeIntegrated = 1,    // 液晶に組み込まれているタブレット
    }

    /// <summary>
    /// デバイスがサポートするデータの種別
    /// </summary>
    public enum WacomMTCapabilityFlags : int
    {
        WMTCapabilityFlagsRawAvailable          = (1 <<  0),    // RAWデータ
        WMTCapabilityFlagsBlobAvailable         = (1 <<  1),    // BLOBデータ
        WMTCapabilityFlagsSensitivityAvailable  = (1 <<  2),    // フィンガーデータの感度データ
        WMTCapabilityFlagsReserved              = (1 << 31),
    }

    /// <summary>
    /// 指の状態
    /// </summary>
    public enum WacomMTFingerState : int
    {
        WMTFingerStateNone  = 0,
        WMTFingerStateDown  = 1,    // 指の接触が開始された
        WMTFingerStateHold  = 2,    // 指が接触し続けている
        WMTFingerStateUp    = 3,    // 指が離された
    }

    /// <summary>
    /// BLOBのデータ種別
    /// </summary>
    public enum WacomMTBlobType : int
    {
        WMTBlobTypePrimary  = 0,    // BLOBの輪郭
        WMTBlobTypeVoid     = 1,    // 内側のBLOBの輪郭
    }

    /// <summary>
    /// データの処理方法
    /// </summary>
    public enum WacomMTProcessingMode : int
    {
        WMTProcessingModeNone       = 0,            // コールバックに送信され、これ以上の処理は行われない
        WMTProcessingModeObserver   = (1 <<  0),    // OSにも並行して送信される
        WMTProcessingModeReserved   = (1 << 31),
    }

    /// <summary>
    /// タブレット情報の構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WacomMTCapability
    {
        public Int32                    Version;
        public Int32                    DeviceID;
        public WacomMTDeviceType        Type;
        public float                    LogicalOriginX;
        public float                    LogicalOriginY;
        public float                    LogicalWidth;
        public float                    LogicalHeight;
        public float                    PhysicalSizeX;
        public float                    PhysicalSizeY;
        public Int32                    ReportedSizeX;
        public Int32                    ReportedSizeY;
        public Int32                    ScanSizeX;
        public Int32                    ScanSizeY;
        public Int32                    FingerMax;
        public Int32                    BlobMax;
        public Int32                    BlobPointsMax;
        public WacomMTCapabilityFlags   CapabilityFlags;
    }

    /// <summary>
    /// フィンガーデータ構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WacomMTFinger
    {
        public int                  FingerID;
        public float                X;
        public float                Y;
        public float                Width;
        public float                Height;
        public UInt16               Sensitivity;
        public float                Orientation;
        public bool                 Confidence;
        public WacomMTFingerState   TouchState;
    }

    /// <summary>
    /// 複数フィンガーデータをまとめた構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WacomMTFingerCollection
    {
        public int      Version;
        public int      DeviceID;
        public int      FrameNumber;
        public int      FingerCount;
        public IntPtr   FingerData;
    }

    /// <summary>
    /// HitRectの構造体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WacomMTHitRect
    {
        public float originX;
        public float originY;
        public float width;
        public float height;
    }

    /// <summary>
    /// フィンガーデータのコールバック
    /// </summary>
    /// <param name="fingerPacket"></param>
    /// <param name="userData"></param>
    /// <returns></returns>
    public delegate int WMT_FINGER_CALLBACK(IntPtr fingerPacket, IntPtr userData);

}
