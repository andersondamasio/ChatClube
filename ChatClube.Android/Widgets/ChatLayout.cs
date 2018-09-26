using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace com.chatclube.Widgets
{
   public class ChatLayout : RelativeLayout
    {
        public ChatLayout(Context context) : base(context)
        {
            
        }

        public ChatLayout(Context context, IAttributeSet attrs) : base(context, attrs, 0)
        {
        }

        public ChatLayout(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
         
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);


            float adjustVal = (float)12.667;
            if (ChildCount < 3)
                return;

            View v1 = GetChildAt(0); //message tv
            View v2 = GetChildAt(1); //image v or timetv //untuk replay tidak ada image
            View v3 = GetChildAt(2); //time tv //untuk send

            int messageHeight = v1.MeasuredHeight + v3.MeasuredHeight;
            int messageWidth = v1.MeasuredWidth;
            int imageViewWidth = v2.MeasuredWidth;
            int timeWidth = v3.MeasuredWidth;

            //int layoutWidth = (int) (imageViewWidth + timeWidth + messageWidth + convertDpToPixel(adjustVal, getContext()));
            int infoWidth = imageViewWidth + timeWidth;
            int chatMessageWidth = messageWidth > infoWidth ? messageWidth : infoWidth;
            int layoutWidth = (int)(chatMessageWidth + convertDpToPixel(adjustVal, Context));

            SetMeasuredDimension(layoutWidth, messageHeight);
        }

        /**
    * This method converts dp unit to equivalent pixels, depending on device density.
    *
    * @param dp A value in dp (density independent pixels) unit. Which we need to convert into pixels
    * @param context Context to get resources and device specific display metrics
    * @return A float value to represent px equivalent to dp depending on device density
    */
        public static float convertDpToPixel(float dp, Context context)
        {
            Resources resources = context.Resources;
            DisplayMetrics metrics = resources.DisplayMetrics;
            float px = dp * ((float)metrics.DensityDpi / 160f);
            return px;
        }

    }
}