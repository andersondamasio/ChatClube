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
using Java.Lang;

namespace com.chatclube.Adapters
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private List<(string Titulo, Android.Support.V4.App.Fragment Fragment)> mFragmentList = new List<(string Titulo, Android.Support.V4.App.Fragment Fragment)>();//new List<(Titulo: "", Fragment: new Android.Support.V4.App.Fragment())>;


        public ViewPagerAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
        {
        }

    public override int Count => mFragmentList.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mFragmentList[position].Fragment;
        }

        public void AddFragment(Android.Support.V4.App.Fragment fragment, string titulo)
        {
            mFragmentList.Add((titulo,fragment));
           
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(mFragmentList[position].Titulo); 
        }
    }
}