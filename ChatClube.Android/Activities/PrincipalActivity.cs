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

namespace com.chatclube.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class PrincipalActivity : BaseActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessag;

        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Principal);

            textMessag = FindViewById<TextView>(Resource.Id.message);
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            AbrirFragment(new SalasFragment());
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_SalasProximas:
                    AbrirFragment(new SalasFragment());
                    return true;
                case Resource.Id.navigation_Conversas:
                    return true;
                case Resource.Id.navigation_Notificacoes:
                    return true;
            }          
            return false;
        }

        private void AbrirFragment(Fragment fragment)
        {
            var ft = FragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.FL_Principal, fragment);
            ft.Commit();
        }

    }
}

