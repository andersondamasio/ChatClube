using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.chatclube.Activities
{
    [Application]
    public class Start : Application
    {
        public static Activity currentActivity { get; set; }

        public Start(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        { }

        public override void OnCreate()
        {
            base.OnCreate();
 
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                args.Handled = true;
                try
                {
                    Toast.MakeText(this, $"{args.Exception.Message} - {args.Exception?.InnerException?.Message}", ToastLength.Long).Show();
                }
                catch { }
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
            };
        }
    }
}