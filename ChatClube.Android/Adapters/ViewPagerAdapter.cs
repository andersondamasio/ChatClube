using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace com.chatclube.Adapters
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> mFragmentList = new List<Android.Support.V4.App.Fragment>();

        public ViewPagerAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
        {
        }

        public override int Count => mFragmentList.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mFragmentList[position];
        }

        public void AddFragment(Android.Support.V4.App.Fragment fragment)
        {
            mFragmentList.Add(fragment);
        }
    }
}