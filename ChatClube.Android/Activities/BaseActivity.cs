using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V7.Widget;

namespace com.chatclube.Activities
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : AppCompatActivity
    {
        
        protected override void OnCreateAsync(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           
            /* RequestWindowFeature(WindowFeatures.IndeterminateProgress);
             RequestWindowFeature(WindowFeatures.ActionBar);
             SetProgressBarIndeterminate(true);*/

            /*RequestWindowFeature(WindowFeatures.ActionBar);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            ProgressBar progressBar = toolbar.FindViewById<ProgressBar>(Resource.Id.progress_spinner);
            progressBar.Visibility = ViewStates.Visible;*/
        }

    
    }
}