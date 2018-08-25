using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.chatclube.Adapters;
using com.chatclube.Repository;

namespace com.chatclube.Fragments
{
    public class SalasFragment : Fragment
    {
        private View view;
        private ListView ListViewSalas { get { return view.FindViewById<ListView>(Resource.Id.ListViewSalas); } }
        private TextView Empty { get { return view.FindViewById<TextView>(Android.Resource.Id.Empty); } }

        private List<Sala> listSalas;

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Activity.SetProgressBarIndeterminateVisibility(true);
            listSalas = await new SalaRepository().GetSalasAsync();
            Activity.SetProgressBarIndeterminateVisibility(false);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Salas, container, false);
            new SalaRepository().SalvarSala();
            var adapter = new SalasAdapter(Activity, listSalas);
            ListViewSalas.Adapter = adapter;
            ListViewSalas.EmptyView = Empty;
            return view;
        }
    }
}