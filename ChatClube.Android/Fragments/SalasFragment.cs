﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using com.chatclube.Activities;
using com.chatclube.Adapters;
using com.chatclube.Core;
using com.chatclube.Repository;
using com.chatclube.Repository.SalaX;
using com.chatclube.SalaX;
using com.chatclube.Utils;

namespace com.chatclube.Fragments
{
    public class SalasFragment : Android.Support.V4.App.Fragment
    {
        private View view;
        private RecyclerView ListViewSalas { get { return view.FindViewById<RecyclerView>(Resource.Id.recycler_view); } }
        private TextView Empty { get { return view.FindViewById<TextView>(Resource.Id.empty_view); } }

        private List<Sala> listSalas;
        private SalasAdapter adapter;


        public async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            listSalas = await new SalaRepository().GetSalasAsync();
            adapter = new SalasAdapter(listSalas);
        }

        private void ListViewSalas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.Salas, container, false);
            if (adapter == null)
                adapter = new SalasAdapter(listSalas);
            else adapter.NotifyDataSetChanged();
            ListViewSalas.SetLayoutManager(new LinearLayoutManager(Activity));
            ListViewSalas.SetAdapter(adapter);

            if ((listSalas?.Count ?? 0) == 0)
            {
                ListViewSalas.Visibility = ViewStates.Gone;
                Empty.Visibility = ViewStates.Visible;
            }
            else
            {
                ListViewSalas.Visibility = ViewStates.Visible;
                Empty.Visibility = ViewStates.Gone;
            }

            #region eventos
            //ListViewSalas.ItemClick += ListViewSalas_ItemClick;
            #endregion

            return view;
        }

        public async void Refresh()
        {
            if (adapter == null)
                adapter = new SalasAdapter(listSalas);
            else
            {

                 

           //  listSalas = await new SalaRepository().GetSalasAsync();
                adapter = new SalasAdapter(listSalas);
                adapter.NotifyDataSetChanged();
            }
        }
    }
}