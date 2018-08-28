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
   public class SalaAdapter : ArrayAdapter<ChatMessage>
    {
        private Context context;
        private List<ChatMessage> chatMessages;
        public static SimpleDateFormat SIMPLE_DATE_FORMAT = new SimpleDateFormat("h:mm a", Locale.Default);
       

        public SalaAdapter(Context context, List<ChatMessage> chatMessages)
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
            ChatMessage message = chatMessages[position];
            return (int)message.getUserType();
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = null;
            ChatMessage message = chatMessages[position];
            ViewHolder1 holder1;
            ViewHolder2 holder2;


            if (message.getUserType() == UserType.SELF)
            {
                if (convertView == null)
                {
                    v = LayoutInflater.From(context).Inflate(Resource.Layout.Sala_Usu1Template, null, false);
                    holder1 = new ViewHolder1();


                    holder1.messageTextView = v.FindViewById<TextView>(Resource.Id.textview_message);
                    holder1.timeTextView = v.FindViewById<TextView>(Resource.Id.textview_time);

                    v.Tag = holder1;
                }
                else
                {
                    v = convertView;
                    holder1 = (ViewHolder1)v.Tag;

                }

                holder1.messageTextView.TextFormatted = (Html.FromHtml(Emoji.replaceEmoji(new Java.Lang.String(message.getMessageText()),
                     holder1.messageTextView.Paint.GetFontMetricsInt(), AndroidUtilities.dp(16))
                     + " &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;"));
                holder1.timeTextView.Text = (SIMPLE_DATE_FORMAT.Format(message.getMessageTime()));

            }
            else if (message.getUserType() == UserType.OTHER)
            {

                if (convertView == null)
                {
                    v = LayoutInflater.From(context).Inflate(Resource.Layout.Sala_Usu2Template, null, false);

                    holder2 = new ViewHolder2();


                    holder2.messageTextView = v.FindViewById<TextView>(Resource.Id.textview_message);
                    holder2.timeTextView = v.FindViewById<TextView>(Resource.Id.textview_time);
                    holder2.messageStatus = v.FindViewById<ImageView>(Resource.Id.user_reply_status);
                    v.Tag = holder2;

                }
                else
                {
                    v = convertView;
                    holder2 = (ViewHolder2)v.Tag;

                }

                holder2.messageTextView.TextFormatted = Html.FromHtml(
                    Emoji.replaceEmoji(new Java.Lang.String(message.getMessageText()),
                    holder2.messageTextView.Paint.GetFontMetricsInt(), AndroidUtilities.dp(16))
                    + " &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;" +
                    "&#160;&#160;&#160;&#160;&#160;&#160;&#160;");


               // holder2.messageTextView.TextFormatted = Html.FromHtml(message.getMessageText());
                //holder2.messageTextView.setText(message.getMessageText());
                holder2.timeTextView.Text = SIMPLE_DATE_FORMAT.Format(message.getMessageTime());

                if (message.getMessageStatus() == Status.DELIVERED)
                {
                    holder2.messageStatus.SetImageDrawable(context.Resources.GetDrawable(Resource.Drawable.message_got_receipt_from_target));
                }
                else if (message.getMessageStatus() == Status.SENT)
                {
                    holder2.messageStatus.SetImageDrawable(context.Resources.GetDrawable(Resource.Drawable.message_got_receipt_from_server));

                }
            }
            return v;
        }

        class ViewHolder1 : Java.Lang.Object
        {
            public TextView messageTextView;
            public TextView timeTextView;

        }
        class ViewHolder2 : Java.Lang.Object
        {
            public ImageView messageStatus;
            public TextView messageTextView;
            public TextView timeTextView;

        }
    }

}