

using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using com.chatclube.Adapters;
using Android.Support.V7.Widget;
using com.chatclube.Fragments;

namespace com.chatclube.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class PrincipalActivity : BaseActivity
    {
       private TabLayout tabLayout;
       private ViewPager viewPager;
       private ViewPagerAdapter viewPagerAdapter;

        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        protected override void OnCreateAsync(Bundle savedInstanceState)
        {
            base.OnCreateAsync(savedInstanceState);
            SetContentView(Resource.Layout.Principal);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            viewPagerAdapter = new ViewPagerAdapter(SupportFragmentManager);
            viewPager.Adapter = viewPagerAdapter;
            tabLayout.SetupWithViewPager(viewPager);

            viewPagerAdapter.AddFragment(new SalasFragment(), GetString(Resource.String.salas_proximas));
            viewPagerAdapter.AddFragment(new SalasFragment(), GetString(Resource.String.conversas));
            viewPagerAdapter.AddFragment(new SalasFragment(), GetString(Resource.String.notificacoes));
            viewPagerAdapter.NotifyDataSetChanged();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
             MenuInflater.Inflate(Resource.Menu.menu_principal, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}

