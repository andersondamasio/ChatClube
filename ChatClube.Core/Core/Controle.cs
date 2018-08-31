using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace com.chatclube.Core
{
    public class Controle
    {
        public static void GetWifi()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var teste = e;
        }
    }
}
