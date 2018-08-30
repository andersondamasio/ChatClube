using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;
using System.IO;
using Android.Media;
using Org.Apache.Http.Client.Methods;
using Microsoft.AspNet.SignalR.Client.Http;
using System.Linq;
using Android.OS;
using com.chatclube.Activities;
using Android.Net.Wifi;
using Android.Util;
using Android.Content.PM;
using Android.Support.V4.App;
using System.Diagnostics;
using Android.Locations;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System.Net.Http;
using Android.Text;
using com.chatclube.Models;
using Android;

namespace com.chatclube.Utils
{
    public class Imagem
    {
        public int pe_id { get; set; }
        public int me_id { get; set; }
        public string me_imagem { get; set; }
    }

    public class DroidUtils
    {

        private static bool consultando = false;
        public static async Task<TResult> ExecuteTask<TResult>(Func<TResult> function)
        {
            TResult retorno = default(TResult);
            try
            {
                if (consultando == false)
                {
                    consultando = true;
                    Start.currentActivity.SetProgressBarIndeterminateVisibility(true);
                    retorno = await System.Threading.Tasks.Task.Run(function);
                    Start.currentActivity.SetProgressBarIndeterminateVisibility(false);
                    consultando = false;
                }
            }
            catch (Exception ex)
            {
                var erro = ex;
            }
            return retorno;
        }

        public static async System.Threading.Tasks.Task ExecuteTask(Action action)
        {
            try
            {
                Start.currentActivity.SetProgressBarIndeterminateVisibility(true);
                await System.Threading.Tasks.Task.Run(action);
                Start.currentActivity.SetProgressBarIndeterminateVisibility(false);
            }
            catch (Exception ex)
            {
                var erro = ex;
            }
        }

        public static void Alert(Context context, int mensagem, int appName)
        {
            // Build the dialog.
            var builder = new AlertDialog.Builder(context);
            builder.SetTitle(appName);
            builder.SetMessage(mensagem);
            builder.SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null);
            var dialog = builder.Create();
            dialog.Show();

            var okBtn = dialog.GetButton((int)DialogButtonType.Positive);

            okBtn.Click += (sender, args) =>
            {
                return;
            };
        }

        public static void Alert(Context context, String mensagem, int appName)
        {
            // Build the dialog.
            var builder = new AlertDialog.Builder(context);
            builder.SetTitle(appName);
            builder.SetMessage(mensagem);
            builder.SetPositiveButton("OK", (EventHandler<DialogClickEventArgs>)null);
            var dialog = builder.Create();
            dialog.Show();

            var okBtn = dialog.GetButton((int)DialogButtonType.Positive);

            okBtn.Click += (sender, args) =>
            {
                return;
            };
        }


        public static void MostraMensagem(string mensagem, string exMessage = null)
        {
            try
            {
                Log.Error("ChatClubeService", "MostraMensagem-->" + exMessage);

                if (exMessage != null)
                {
                    //if (AndroidRecursos.IsDebug())
                    //  Toast.MakeText(Start.currentActivity ?? Application.Context, exMessage, ToastLength.Long).Show();
                }
                else
                    Toast.MakeText(Start.currentActivity ?? Application.Context, mensagem, ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Log.Error("ChatClubeService", "MostraMensagem:" + ex.Message);
            }
        }

        public static void LogCat(object mensagem, Exception ex = null, string tag = "ChatClubeService")
        {
            if (ex != null)
                mensagem = mensagem + ":" + ex.Message;
            Log.Error(tag, string.Format("{0}:{1}", MetodoCorrente(ex), mensagem == null ? "null" : mensagem.ToString()));
        }


        private static string MetodoCorrente(Exception ex)
        {
            if (ex == null)
                return string.Empty;
            try
            {
                var trace = new StackTrace(ex);
                var frame = trace.GetFrame(0);
                var method = frame.GetMethod();
                return string.Concat(method.DeclaringType.FullName, ".", method.Name);
            }
            catch { }

            return string.Empty;
        }

        public static int CameraPhotoOrientation(Context context, Android.Net.Uri imageUri, String imagePath)
        {
            int rotate = 0;
            try
            {
                context.ContentResolver.NotifyChange(imageUri, null);
                Java.IO.File imageFile = new Java.IO.File(imagePath);
                ExifInterface exif = new ExifInterface(imageFile.AbsolutePath);
                //int orientation = exif.getAttributeInt(ExifInterface.TAG_ORIENTATION, ExifInterface.ORIENTATION_NORMAL);
                var orientation = exif.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);
                switch (orientation)
                {
                    case (int)Android.Media.Orientation.Rotate270:
                        rotate = 270;
                        break;
                    case (int)Android.Media.Orientation.Rotate180:
                        rotate = 180;
                        break;
                    case (int)Android.Media.Orientation.Rotate90:
                        rotate = 90;
                        break;
                }
                System.Diagnostics.Debug.WriteLine("Orientation for file " + imagePath + " is " + rotate);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception during Camera orientations" + e.StackTrace + e.ToString());
            }
            return rotate;
        }


        public static void EnviarNotificacao(Mensagem mensagemDto)
        {
            /*try
            {
                int id = mensagemDto.me_id;
                string titulo = mensagemDto.usuarioDto.us_apelido ?? mensagemDto.usuarioDto.us_nome;
                string texto = null;

                if (mensagemDto.sa_id.HasValue)
                {
                    titulo = string.Format("<b>{0}</b>", mensagemDto.sa_nome);
                    id = mensagemDto.sa_id.Value;
                }

                texto = string.Format("<b>{0}</b>:{1}", mensagemDto.usuarioDto.us_nome, mensagemDto.me_mensagem);

                Intent intent = new Intent(Application.Context, typeof(SplashActivity));
                intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                if (mensagemDto.me_activity != null)
                {
                    intent.PutExtra("activity", mensagemDto.me_activity);
                    if (mensagemDto.sa_id.HasValue)
                        intent.PutExtra("sa_id", mensagemDto.sa_id.Value);

                }
                PendingIntent pendingIntent = PendingIntent.GetActivity(Application.Context, -1, intent, PendingIntentFlags.UpdateCurrent);

                NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(Html.FromHtml(titulo))
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentText(Html.FromHtml(texto));

                long[] pattern = { 0, 80, 300, 80 };
                builder.SetVibrate(pattern);

                NotificationManager notificationManager =
                    (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
                notificationManager.Notify(id, builder.Build());


                if ((ControleReceiver.listConversas?.Sum(s => s.count) ?? 0) > 0)
                    ME.Leolin.Shortcutbadger.ShortcutBadger.ApplyCount(Application.Context, ControleReceiver.listConversas.Sum(s => s.count));
                else ME.Leolin.Shortcutbadger.ShortcutBadger.ApplyCount(Application.Context, 1);
                */
            

        }

        public static void EnviarNotificaocao(string titulo, string texto, int id, string activity = null)
        {
            try
            {
                Intent intent = new Intent(Application.Context, typeof(SplashActivity));
                intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
                if (activity != null)
                    intent.PutExtra("activity", activity);

                PendingIntent pendingIntent = PendingIntent.GetActivity(Application.Context, -1, intent, PendingIntentFlags.UpdateCurrent);

                NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context)
             .SetAutoCancel(true)
             .SetContentIntent(pendingIntent)
             .SetContentTitle(titulo)
             .SetSmallIcon(Resource.Drawable.abc_ic_menu_overflow_material)
             .SetContentText(texto);

                NotificationManager notificationManager =
                    (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
                notificationManager.Notify(id, builder.Build());


              /*  if ((ControleReceiver.listConversas?.Sum(s => s.count) ?? 0) > 0)
                    ME.Leolin.Shortcutbadger.ShortcutBadger.ApplyCount(Application.Context, ControleReceiver.listConversas.Sum(s => s.count));
                else ME.Leolin.Shortcutbadger.ShortcutBadger.ApplyCount(Application.Context, 1);
                */
            }
            catch (Exception ex)
            {


            }
        }

        public static bool IsConnected(Context context)
        {
            bool connectedState = IsNetworkAvailable(context) || IsWifiAvailable(context);
            return connectedState;
        }

        public static bool IsNetworkAvailable(Context context)
        {

            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo mobileNetworkInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
            return mobileNetworkInfo != null && mobileNetworkInfo.IsConnected;

        }

        public static bool IsWifiAvailable(Context context)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            NetworkInfo wifiNetworkInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
            return wifiNetworkInfo != null && wifiNetworkInfo.IsConnected;
        }

        public static HubConnection _hubConnection;
        public static void StartService(HubConnection hubConnection, bool verificarServicoRodando = true)
        {
            try
            {
               /* _hubConnection = null;
                if (!verificarServicoRodando || DroidUtils.ServiceRodando(typeof(ChatClubeService)) == false)
                {
                    _hubConnection = hubConnection;

                    String qs = "{}";
                    Intent intent = new Intent(Application.Context, typeof(ChatClubeService));
                    Bundle args = new Bundle();
                    args.PutInt(ChatClubeService.FLAG_SERVICE_CMD, ChatClubeService.FLAG_INIT_CONNECTION);
                    args.PutString(ChatClubeService.FLAG_INIT_CONNECTION_QS, qs);
                    intent.PutExtra("args", args);

                    Application.Context.StartService(intent);
                }*/
            }
            catch (Exception ex)
            {
                LogCat(ex);
            }
        }

        public static bool ServiceRodando(Type servico)
        {
            try
            {
                var manager = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
                var servicos = manager.GetRunningServices(int.MaxValue).Select(s => s.Service.ClassName).ToList().OrderBy(s => s);

                foreach (string service in servicos)
                {
                    if (System.IO.Path.GetExtension(service.ToLower()).Equals(System.IO.Path.GetExtension(servico.ToString().ToLower())))
                        return true;
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Toast.MakeText(Application.Context, ex.Message, ToastLength.Short).Show();
                }
                catch { }
            }
            return false;
        }

        public static Tuple<WifiInfo, IList<ScanResult>> WifiInfo()
        {
            try
            {
                if (!DroidUtils.IsWifiAvailable(Application.Context))
                    return null;

              var teste01 =  Xamarin.Essentials.Connectivity.NetworkAccess;
            


                WifiManager wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
                return new Tuple<Android.Net.Wifi.WifiInfo, IList<ScanResult>>(wifiManager.ConnectionInfo, wifiManager.ScanResults);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string WifiBSSID()
        {
            try
            {
                WifiManager wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
                return wifiManager.ConnectionInfo.BSSID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static void AbreConfirmacao(Android.App.Activity activity, string titulo, string mensagem, EventHandler<DialogClickEventArgs> eventoSim, EventHandler<DialogClickEventArgs> eventoNao, bool permitirFechar = false)
        {
            var alertBuilder = new AlertDialog.Builder(activity);
            alertBuilder.SetCancelable(permitirFechar);
            alertBuilder.SetTitle(titulo);
            alertBuilder.SetMessage(mensagem);
            if (eventoSim == null && eventoNao == null)
                alertBuilder.SetPositiveButton("Fechar", (sender, args) =>
                {
                    ((AlertDialog)sender).Dismiss();
                    alertBuilder = null;
                });
            else
            {

                alertBuilder.SetPositiveButton("Sim", eventoSim + new EventHandler<DialogClickEventArgs>((s1, e1) => { alertBuilder = null; }));
                alertBuilder.SetNegativeButton("Não", eventoNao + new EventHandler<DialogClickEventArgs>((s1, e1) => { alertBuilder = null; }));

            }
            alertBuilder.Create().Show();
        }

        public static Boolean ActivityAberta(String activity)
        {
            ComponentName componentInfo = null;
            try
            {
                ActivityManager manager = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
                var runningTaskInfo = manager.GetRunningTasks(1);
                componentInfo = runningTaskInfo[0].TopActivity;

                DroidUtils.LogCat(componentInfo.ClassName, null, "XXX");

            }
            catch (Exception ex)
            {
                DroidUtils.LogCat(ex.Message, ex, "XXX");

            }
            return componentInfo.ClassName.EndsWith(activity);
        }


        public static string VersaoAplicativo()
        {
            try
            {
                return Application.Context.PackageManager.GetPackageInfo((Start.currentActivity ?? Application.Context).PackageName, PackageInfoFlags.MetaData).VersionName;
            }
            catch (Exception ex)
            {
                MostraMensagem("VersaoAplicativo()", ex.Message);
                return null;
            }
        }


        public static async Task<Address> LocationAddress(double latitude, double longitude)
        {
            Geocoder geocoder = new Geocoder(Application.Context);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(latitude, longitude, 1);

            return addressList.FirstOrDefault();
        }

        public static bool IsDebug()
        {
            return true;

#if DEBUG
            return true;
#endif
            return false;
        }

    }
}