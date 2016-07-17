using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wacom.FeelMultiTouch;

namespace WacomGesturePlugin
{
    public class Utils
    {
        public static int[] getFingerAvgPoint(WacomMTFinger[] fingers)
        {
            float X = 0;
            float Y = 0;
            foreach (var i in fingers)
            {
                X += (i.X <= 1) ? i.X * 1920 : i.X;
                Y += (i.Y <= 1) ? i.Y * 1920 : i.Y;
            }
            X = X / fingers.Count();
            Y = Y / fingers.Count();
            return new int[] { (int)X, (int)Y };
        }
    }
}
