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
using com.chatclube.Adapters;
using com.chatclube.Models;

namespace com.chatclube.Activities
{
    [Activity(Label = "@string/sala", Theme = "@style/AppTheme")]
    public class SalaActivity : BaseActivity
    {
        private ImageButton ovIB_Enviar { get { return FindViewById<ImageButton>(Resource.Id.ovIB_Enviar); } }
        private EditText ovET_Mensagem { get { return FindViewById<EditText>(Resource.Id.ovET_Mensagem); } }
        private ListView ovLV_Sala { get { return FindViewById<ListView>(Resource.Id.ovLV_Sala); } }

        private List<ChatMessage> mensagens = new List<ChatMessage>();
        SalaAdapter salaAdapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Sala);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            #region eventos
            ovIB_Enviar.Click += OvIB_Enviar_Click;
            #endregion
           
        }

        private void OvIB_Enviar_Click(object sender, EventArgs e)
        {
            EnviarMensagem(ovET_Mensagem.Text, UserType.OTHER);
            ovET_Mensagem.Text = string.Empty;

        }

        private void EnviarMensagem(String messageText, UserType userType)
        {

            if (messageText.Trim().Length == 0)
                return;

            ChatMessage message = new ChatMessage();
            message.setMessageStatus(Status.SENT);
            message.setMessageText(messageText);
            message.setUserType(userType);
            message.setMessageTime(DateTime.Now.Ticks);
            mensagens.Add(message);

            if (salaAdapter != null)
                salaAdapter.NotifyDataSetChanged();
            else
            {
                salaAdapter = new SalaAdapter(this, mensagens);
                ovLV_Sala.Adapter = salaAdapter;
            }
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }
    }
}