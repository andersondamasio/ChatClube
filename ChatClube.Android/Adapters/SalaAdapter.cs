using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using com.chatclube.Models;
using com.chatclube.Utils;
using com.chatclube.Widgets;
using Java.Text;
using Java.Util;

namespace com.chatclube.Adapters
{
   public class SalaAdapter : ArrayAdapter<Mensagem>
    {
        private Context context;
        private List<Mensagem> chatMessages;
        public static SimpleDateFormat SIMPLE_DATE_FORMAT = new SimpleDateFormat("HH:mm:ss", Locale.Default);
       

        public SalaAdapter(Context context, List<Mensagem> chatMessages)
          : base(context, 0, chatMessages)
        {
            this.context = context;
            this.chatMessages = chatMessages;
        }
        public override int Count
        {
            get { return chatMessages.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override int ViewTypeCount => 2;

        public override int GetItemViewType(int position)
        {
            Mensagem message = chatMessages[position];
            return (int)message.Tipo;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = null;
            Mensagem message = chatMessages[position];
            ViewHolder1 holderEnviador;
            ViewHolder2 holderResposta;


            if (message.Tipo == UserType.SELF)
            {
                if (convertView == null)
                {
                    v = LayoutInflater.From(context).Inflate(Resource.Layout.chat_user_send_item, null, false);
                    holderEnviador = new ViewHolder1();


                    holderEnviador.messageTextView = v.FindViewById<TextView>(Resource.Id.message_text);
                    holderEnviador.timeTextView = v.FindViewById<TextView>(Resource.Id.time_text);
                    holderEnviador.messageStatus = v.FindViewById<ImageView>(Resource.Id.user_reply_status);
                    v.Tag = holderEnviador;
                }
                else
                {
                    v = convertView;
                    holderEnviador = (ViewHolder1)v.Tag;

                }

                /*holderEnviador.messageTextView.TextFormatted = (Html.FromHtml(Emoji.replaceEmoji(new Java.Lang.String(message.Descricao),
                     holderEnviador.messageTextView.Paint.GetFontMetricsInt(), AndroidUtilities.dp(16))
                     + " &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;"));
                holderEnviador.timeTextView.Text = (SIMPLE_DATE_FORMAT.Format(message.Hora));
                */

                //sendHolder.messageTextView.setText(Emoji.replaceEmoji(message.getContactId() + "#" + message.getMsgid() + "\r\n" + message.getMessageText(), sendHolder.messageTextView.getPaint().getFontMetricsInt(), DisplayUtils.dp(16)));
                holderEnviador.messageTextView.Text = message.Descricao;
                holderEnviador.timeTextView.Text = message.Hora.ToShortTimeString();

                if (message.Status == Status.NEW)
                {
                    holderEnviador.messageStatus.Visibility = ViewStates.Invisible;
                }
                else
                {
                    holderEnviador.messageStatus.Visibility = ViewStates.Visible;
                    if (message.Status == Status.SENT)
                    {
                        holderEnviador.messageStatus.SetImageDrawable(context.Resources.GetDrawable(Resource.Drawable.ic_double_tick));
                    }
                    else if (message.Status == Status.DELIVERED)
                    {
                        holderEnviador.messageStatus.SetImageDrawable(context.Resources.GetDrawable(Resource.Drawable.ic_single_tick));
                    }
                }
                /*
                if (message.Status == Status.DELIVERED)
                {
                    holder1.messageStatus.SetImageDrawable(context.Resources.GetDrawable(Resource.Drawable.ic_double_tick));
                }
                else if (message.Status == Status.SENT)
                {
                    holder1.messageStatus.SetImageDrawable(context.Resources.GetDrawable(Resource.Drawable.ic_single_tick));

                }*/

            }
            else if (message.Tipo == UserType.OTHER)
            {

                if (convertView == null)
                {
                    Boolean groupMessage = true;
                   if(groupMessage)
                    v = LayoutInflater.From(context).Inflate(Resource.Layout.chat_user_reply_withsender_item, null, false);
                   else
                        v = LayoutInflater.From(context).Inflate(Resource.Layout.chat_user_reply_item, null, false);

                    holderResposta = new ViewHolder2();

                    if (groupMessage)
                        holderResposta.senderTextView = v.FindViewById<TextView>(Resource.Id.chat_company_reply_author);

                    holderResposta.messageTextView = v.FindViewById<TextView>(Resource.Id.message_text);
                    holderResposta.timeTextView = v.FindViewById<TextView>(Resource.Id.time_text);
                    v.Tag = holderResposta;

                }
                else
                {
                    v = convertView;
                    holderResposta = (ViewHolder2)v.Tag;

                }

                /* holder2.messageTextView.TextFormatted = Html.FromHtml(
                     Emoji.replaceEmoji(new Java.Lang.String(message.Descricao),
                     holder2.messageTextView.Paint.GetFontMetricsInt(), AndroidUtilities.dp(16))
                     + " &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;" +
                     "&#160;&#160;&#160;&#160;&#160;&#160;&#160;");*/

                // replyHolder.messageTextView.setText(Emoji.replaceEmoji(message.getContactId() + "#" + message.getMsgid() + "\r\n" + message.getMessageText(), replyHolder.messageTextView.getPaint().getFontMetricsInt(), DisplayUtils.dp(16)));
                holderResposta.messageTextView.Text = message.Descricao;
                holderResposta.timeTextView.Text = message.Hora.ToShortTimeString();


            }
            return v;
        }

        class ViewHolder1 : Java.Lang.Object
        {
            public ImageView messageStatus;
            public TextView messageTextView;
            public TextView timeTextView;

        }
        class ViewHolder2 : Java.Lang.Object
        {
            public TextView senderTextView;
            public TextView messageTextView;
            public TextView timeTextView;

        }
    }

}