using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using com.chatclube.Activities;
using Java.IO;
using Java.Lang;
using Boolean = Java.Lang.Boolean;
using String = Java.Lang.String;

namespace com.chatclube.Utils
{
    public class AndroidUtilities
    {

        public static float density = 1;
        public static int statusBarHeight = 0;
        public static Point displaySize = new Point();

        /* static {
         density = App.getInstance().getResources().getDisplayMetrics().density;
         checkDisplaySize();
     }*/

        public static int dp(float value)
        {
            return (int)Java.Lang.Math.Ceil(density * value);
        }

        public static extern void loadBitmap(System.String path, Bitmap bitmap, int scale, int width, int height, int stride);


        public static void runOnUIThread(Runnable runnable)
        {
            runOnUIThread(runnable, 0);
        }

        public static void runOnUIThread(Runnable runnable, long delay)
        {
            if (delay == 0)
            {
                Start.applicationHandler.Post(runnable);
            }
            else
            {
                Start.applicationHandler.PostDelayed(runnable, delay);
            }
        }


        public static Boolean copyFile(Stream sourceFile, Java.IO.File destFile)
        {
            var out2 = new FileOutputStream(destFile);
            byte[] buf = new byte[4096];
            int len;
            while ((len = sourceFile.Read(buf, 0, buf.Length)) > 0)
            {
                Thread.Yield();
                out2.Write(buf, 0, len);
            }
            out2.Close();
            return Java.Lang.Boolean.True;
        }
    }
}
