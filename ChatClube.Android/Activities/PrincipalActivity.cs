

using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using com.chatclube.Adapters;
using Android.Support.V7.Widget;
using com.chatclube.Fragments;
using com.chatclube.Utils;
using com.chatclube.Repository.SalaX;
using Microsoft.AspNetCore.SignalR.Client;

namespace com.chatclube.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class PrincipalActivity : BaseActivity
    {
        private TabLayout tabLayout;
        private ViewPager viewPager;
        private ViewPagerAdapter viewPagerAdapter;
        SalasFragment salasFragment;

        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        HubConnection connection;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Principal);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            tabLayout = FindViewById<TabLayout>(Resource.Id.tabs);
            viewPager = FindViewById<ViewPager>(Resource.Id.pager);
            viewPagerAdapter = new ViewPagerAdapter(SupportFragmentManager);
            viewPager.Adapter = viewPagerAdapter;
            tabLayout.SetupWithViewPager(viewPager);

            salasFragment = new SalasFragment();

            viewPagerAdapter.AddFragment(salasFragment, GetString(Resource.String.salas_proximas));
            viewPagerAdapter.AddFragment(new Android.Support.V4.App.Fragment(), GetString(Resource.String.conversas));
            viewPagerAdapter.AddFragment(new Android.Support.V4.App.Fragment(), GetString(Resource.String.notificacoes));
            viewPagerAdapter.NotifyDataSetChanged();

            IniciaProcessos();


            connection = new HubConnectionBuilder()
          .WithUrl("http://192.168.1.6:5000/ChatClubeHub")
          .Build();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_principal, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        private async void IniciaProcessos()
        {
            Xamarin.Essentials.Connectivity.ConnectivityChanged += null;
            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            SalvarSalaWifi();
        }

        private void SalvarSalaWifi()
        {
            var wiffiInfo = DroidUtils.GetWifi();

            if (wiffiInfo.HasValue)
            {
                if (wiffiInfo.Value.Info != null)
                {
                    var info = wiffiInfo.Value.Info;

                    new SalaRepository().InsertUpdateSalaWifi(info.SSID, info.BSSID);
                }
            }
        }


        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
           if(e.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                SalvarSalaWifi();
                salasFragment.Refresh();
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Detach(salasFragment);
                ft.Attach(salasFragment);
                ft.Commit();
            }
        }
    }
}

