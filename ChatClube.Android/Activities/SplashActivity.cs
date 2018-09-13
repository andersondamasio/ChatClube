﻿using System;
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
    public class SplashActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            var intent = new Intent(this, typeof(LoginActivity));
            var activity = Intent.GetStringExtra("activity");
            if (!string.IsNullOrEmpty(activity))
                intent.PutExtra("activity", activity);

            StartActivity(intent);
            Finish();
        }
    }
}