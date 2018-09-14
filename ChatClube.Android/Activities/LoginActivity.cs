using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using chatclube.com.Services;
using com.chatclube.Data.Repository.UsuarioX;
using com.chatclube.Services;
using com.chatclube.UsuarioX;
using com.chatclube.Utils;
using Java.Lang;
using Java.Security;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;

[assembly: MetaData("com.facebook.sdk.ApplicationId", Value = "@string/app_id")]

namespace com.chatclube.Activities
{
    [Activity(Label = "Chat Clube", MainLauncher = false,  WindowSoftInputMode = SoftInput.AdjustResize, Theme = "@style/AppTheme.SplashScreen", Icon = "@drawable/icon")]
    public class LoginActivity : BaseActivity, IFacebookCallback
    {

        #region Variáveis
        private ICallbackManager callBackMgr;
        private MyProfileTracker profileTracker;
        private bool erro = false;
        private bool jaEntrou = false;
        HubConnection connection;
        #endregion

        #region Componentes de Tela
        private LoginButton btnFacebook { get { return FindViewById<LoginButton>(Resource.Id.ovLB_Facebook); } }
        private Button btnTentarNovamente { get { return FindViewById<Button>(Resource.Id.ovBT_TentarNovamente); } }
        #endregion
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        

            RequestWindowFeature(WindowFeatures.NoTitle);
            FacebookSdk.SdkInitialize(ApplicationContext);
            SetContentView(Resource.Layout.Login);


            btnTentarNovamente.Click += btnTentarNovamente_Click;

            try
            {

                profileTracker = new MyProfileTracker();
            profileTracker.OnProfileChanged += profileChanged;
            profileTracker.StartTracking();

            btnFacebook.Visibility = Android.Views.ViewStates.Gone;
            btnFacebook.SetReadPermissions("public_profile", "email", "user_friends");
            callBackMgr = CallbackManagerFactory.Create();
            btnFacebook.RegisterCallback(callBackMgr, this);

            if (AccessToken.CurrentAccessToken != null && Profile.CurrentProfile != null)
            {
                    await Conectar(Profile.CurrentProfile);
            }
            else
                btnFacebook.Visibility = Android.Views.ViewStates.Visible;

            }
            catch (System.Exception ex)
            {
                if (btnFacebook.Visibility != ViewStates.Visible)
                    btnTentarNovamente.Visibility = ViewStates.Visible;
                SetProgressBarVisibility(false);
                DroidUtils.MostraMensagem("Problemas ao tentar se conectar, verifique sua internet e tente novamente.", ex.Message);
            }
        }

        private async Task Conectar(Profile profile)
        {
            Usuario usuarioNovo = new Usuario();

            usuarioNovo.Nome = profile.FirstName;
            usuarioNovo.Sobrenome = profile.LastName;
            usuarioNovo.IDProfile = profile.Id;
            //await ChatDataStore<Usuario>.Instance.AddAsync(usuario);
            var connection = await SignalR.GetHubConnection();

            Usuario usuario = await new UsuarioRepository().GetUsuarioAsync(usuarioNovo.IDProfile);

            if (usuario == null)
            {
               await new UsuarioRepository().SalvarUsuarioAsync(usuarioNovo);
               await connection.InvokeAsync("Conectar", usuarioNovo);
            }
            else
            {
                new UsuarioRepository().SalvarUsuarioAsync(usuarioNovo);
                connection.InvokeAsync("Conectar", usuarioNovo);
            }

            GetHash();

            jaEntrou = true;
            var intent = new Intent(this, typeof(PrincipalActivity));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(intent);
            Finish();
        }

        void btnTentarNovamente_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SplashActivity));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(intent);
        }

        public void OnCancel()
        {
           
        }

        public void OnError(FacebookException error)
        {
            RunOnUiThread(() => btnTentarNovamente.Visibility = Android.Views.ViewStates.Visible);
        }

        public async void OnSuccess(Java.Lang.Object result)
        {
            LoginResult loginResult = result as LoginResult;
            if (Profile.CurrentProfile != null)
            {
                await Conectar(Profile.CurrentProfile);
            }
        }

        private async void profileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                try
                {
                    Profile profile = e.mProfile;
                    //await Task.Delay(5000);
                    if (!jaEntrou)
                        await Conectar(profile);
                }
                catch (System.Exception ex)
                {
                    DroidUtils.MostraMensagem("profileChanged", ex.Message);
                }
            }
        }

        private void GetHash()
        {
            try
            {
                PackageInfo info = PackageManager.GetPackageInfo("com.chatclube", PackageInfoFlags.Signatures);
                foreach (var signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());
                    var sign = Base64.EncodeToString(md.Digest(), Base64.Default);
                    Log.Info("MY KEY HASH:", sign);
                    Toast.MakeText(ApplicationContext, sign, ToastLength.Long).Show();
                }
            }
            catch (Android.Content.PM.PackageManager.NameNotFoundException e)
            {
            }
            catch (NoSuchAlgorithmException e)
            {
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        public class MyProfileTracker : ProfileTracker
        {
            public event EventHandler<OnProfileChangedEventArgs> OnProfileChanged;

            protected override void OnCurrentProfileChanged(Profile oldProfile, Profile newProfile)
            {
                if (OnProfileChanged != null)
                {
                    OnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
                }
            }
        }

        public class OnProfileChangedEventArgs : EventArgs
        {
            public Profile mProfile;

            public OnProfileChangedEventArgs(Profile profile) { mProfile = profile; }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            callBackMgr.OnActivityResult(requestCode, (int)resultCode, data);
        }

    }

}