using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Wacom
{
    /// <summary>
    /// アンマネージドメモリを管理するクラス
    /// </summary>
    class MemoryUtil
    {

        /// <summary>
        /// 対象タイプ分のメモリを確保する
        /// </summary>
        /// <param name="t">確保するクラスのタイプ</param>
        /// <returns></returns>
        public static IntPtr AllocUnmanagedBuffer(Type t)
        {
            return Marshal.AllocHGlobal(Marshal.SizeOf(t));
        }

        /// <summary>
        /// 対象のオブジェクト分のメモリを確保する
        /// </summary>
        /// <param name="obj">確保するオブジェクト</param>
        /// <returns></returns>
        public static IntPtr AllocUnmanagedBuffer(Object obj)
        {
            IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
            Marshal.StructureToPtr(obj, p, false);
            return p;
        }

        /// <summary>
        /// 対象のタイプを指定数分メモリ確保する
        /// </summary>
        /// <param name="t">確保するクラスのタイプ</param>
        /// <param name="count">指定数</param>
        /// <returns></returns>
        public static IntPtr AllocUnmanagedBuffer(Type t, int count)
        {
            return Marshal.AllocHGlobal(Marshal.SizeOf(t) * count);
        }

        /// <summary>
        /// 指定サイズ分のメモリを確保する
        /// </summary>
        /// <param name="sz">確保するメモリのサイズ</param>
        /// <returns></returns>
        public static IntPtr AllocUnmanagedBuffer(int sz)
        {
            return Marshal.AllocHGlobal(sz);
        }

        /// <summary>
        /// アンマネージドメモリの開放
        /// </summary>
        /// <param name="buf"></param>
        public static void FreeUnmanagedBuffer(IntPtr buf)
        {
            if (buf != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(buf);
                buf = IntPtr.Zero;
            }
        }

        /// <summary>
        /// アンマネージドメモリを指定のオブジェクトに変換する
        /// </summary>
        /// <typeparam name="T">指定のタイプ</typeparam>
        /// <param name="buf">ポインタ</param>
        /// <returns></returns>
        public static Object MarshalUnmanagedBuffer(IntPtr buf, Type t)
        {
            return Marshal.PtrToStructure(buf, t);
        }

        /// <summary>
        /// アンマネージドメモリをUTF8の文字列に変換する
        /// </summary>
        /// <param name="buf">ポインタ</param>
        /// <param name="sz">サイズ</param>
        /// <returns></returns>
        public static string MarshalUnmanagedString(IntPtr buf, int sz)
        {
            return MarshalUnmanagedString(buf, sz, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// アンマネージドメモリを指定のエンコードの文字列に変換する
        /// </summary>
        /// <param name="buf">ポインタ</param>
        /// <param name="sz">サイズ</param>
        /// <param name="encoding">文字エンコード</param>
        /// <returns></returns>
        public static string MarshalUnmanagedString(IntPtr buf, int sz, System.Text.Encoding encoding)
        {
            Byte[] byteArray = new Byte[sz];
            Marshal.Copy(buf, byteArray, 0, sz);
            return encoding.GetString(byteArray);
        }

        /// <summary>
        /// アンマネージドメモリを指定のタイプの配列に変換する
        /// </summary>
        /// <typeparam name="T">指定のタイプ</typeparam>
        /// <param name="buf">ポインタ</param>
        /// <param name="count">配列の要素数</param>
        /// <returns></returns>
        public static T[] MarshalUnmanagedBufferArray<T>(IntPtr buf, uint count)
        {
            T[] result = new T[count];

            IntPtr nowPtr = buf;
            for (int i = 0; i < count; i++)
            {
                result[i] = (T)Marshal.PtrToStructure(nowPtr, typeof(T));
                nowPtr = (IntPtr)((int)nowPtr + Marshal.SizeOf(typeof(T)));
            }

            return result;
        }

        /// <summary>
        /// 対象のタイプのサイズ取得
        /// </summary>
        /// <param name="t">対象のタイプ</param>
        /// <returns></returns>
        public static int SizeOf(Type t)
        {
            return Marshal.SizeOf(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void StructureToPtr(Object src, IntPtr dst)
        {
           Marshal.StructureToPtr(src, dst, false);
        }

    }
}
