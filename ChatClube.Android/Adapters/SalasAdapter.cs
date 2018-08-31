using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using com.chatclube.Activities;
using com.chatclube.SalaX;

namespace com.chatclube.Adapters
{
    internal class SalasAdapter : RecyclerView.Adapter, Android.Views.View.IOnClickListener
    {
        private List<Sala> itens;

        public SalasAdapter(List<Sala> _itens)
        {
            itens = _itens;
        }


        public void Add(Sala sala)
        {
            itens.Add(sala);
        }

        public override int ItemCount => itens.Count;

          public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
          {
            SalaViewHolder vh = holder as SalaViewHolder;

            vh.ovTVNomeSala.Text = itens[position].Nome;
            vh.ovTVNumeroUsuarios.Text = itens[position].Nome;
        }

        public void OnClick(View v)
        {
            Intent intent = new Intent(v.Context, typeof(SalaActivity));
            v.Context.StartActivity(intent);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
          {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SalasTemplate, parent, false);
            SalaViewHolder vh = new SalaViewHolder(itemView);

            itemView.SetOnClickListener(this);

            return vh;
        }

        private class SalaViewHolder : RecyclerView.ViewHolder
        {
            public TextView ovTVNomeSala, ovTVNumeroUsuarios;
            public SalaViewHolder(View itemView) : base(itemView)
            {
                ovTVNomeSala = itemView.FindViewById<TextView>(Resource.Id.ovTVNomeSala);
                ovTVNumeroUsuarios = itemView.FindViewById<TextView>(Resource.Id.ovTVNumeroUsuarios);
            }
        }
    }
}