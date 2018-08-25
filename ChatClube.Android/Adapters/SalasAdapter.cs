using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.chatclube.Adapters
{
    class SalasAdapter : ArrayAdapter<Sala>
    {
        private Context context;
        private List<Sala> itens;

        public SalasAdapter(Context context, List<Sala> itens)
           : base(context, 0, itens)
        {
            this.context = context;
            this.itens = itens;
        }

        public override int Count
        {
            get { return itens.Count; }
        }

        public override long GetItemId(int position)
        {
            return itens[position].IdSala;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ViewHolder holder = null;
            var item = itens[position];

            if (view != null)
                holder = view.Tag as ViewHolder;

            if (holder == null)
            {
                holder = new ViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.SalasTemplate, parent, false);
                holder.ovTVNomeSala = view.FindViewById<TextView>(Resource.Id.ovTVNomeSala);
                holder.ovTVNumeroUsuarios = view.FindViewById<TextView>(Resource.Id.ovTVNumeroUsuarios);
                view.Tag = holder;
            }

            holder.ovTVNomeSala.Text = item.Nome;
            holder.ovTVNumeroUsuarios.Text = "xxx usuários online";

            return view;
        }

        class ViewHolder : Java.Lang.Object
        {
            //Your adapter views to re-use
            public TextView ovTVNomeSala, ovTVNumeroUsuarios;
        }

    }

   
}