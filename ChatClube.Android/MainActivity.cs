using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.chatclube.Fragments;
using com.chatclube.Repository;
using com.chatclube.Repository.Config;

namespace com.chatclube
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessag;

        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textMessag =  FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            new SalaRepository().SalvarSala();
            var teste = new SalaRepository().GetSalas();
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    //textMessag.SetText(Resource.String.title_home);
                    return true;
                case Resource.Id.navigation_dashboard:
                    AbrirFragment();//textMessag.SetText(Resource.String.title_dashboard);
                    return true;
                case Resource.Id.navigation_notifications:
                   // textMessag.SetText(Resource.String.title_notifications);
                    return true;
            }
            return false;
        }

        private void AbrirFragment()
        {
            Fragment fragment = new SalasFragment();
            var ft = FragmentManager.BeginTransaction();//SupportFragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.mainFrame, fragment);
            ft.Commit();


        }

    }
}

