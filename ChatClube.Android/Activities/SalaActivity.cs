using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using com.chatclube.Adapters;
using com.chatclube.Models;
using System;
using System.Collections.Generic;


namespace com.chatclube.Activities
{
    [Activity(Label = "@string/sala", Theme = "@style/AppThemeComActionBar")]
    public class SalaActivity : BaseActivity, View.IOnKeyListener, View.IOnClickListener, Android.Text.ITextWatcher
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
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            #region eventos
            ovIB_Enviar.SetOnClickListener(this);
            ovET_Mensagem.SetOnKeyListener(this);
            ovET_Mensagem.AddTextChangedListener(this);
            #endregion

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

        public bool OnKey(View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if ((e.Action == KeyEventActions.Down) && (keyCode == Keycode.Enter))
            {
                EditText editText = (EditText)v;

                if (v == ovET_Mensagem)
                    EnviarMensagem(editText.Text, UserType.OTHER);

                ovET_Mensagem.Text = string.Empty;

                return true;
            }
            return false;
        }

        public void OnClick(View v)
        {
            if (v == ovIB_Enviar)
            {
                EnviarMensagem(ovET_Mensagem.Text, UserType.SELF);
            }

            ovET_Mensagem.Text = "";
        }

        public void AfterTextChanged(IEditable s)
        {
            if (s.Length() == 0)
                ovIB_Enviar.SetImageResource(Resource.Drawable.input_send);
            else
                ovIB_Enviar.SetImageResource(Resource.Drawable.input_send); 
        }

        public void BeforeTextChanged(Java.Lang.ICharSequence s, int start, int count, int after)
        {
        }

        public void OnTextChanged(Java.Lang.ICharSequence s, int start, int before, int count)
        {
            if (!ovET_Mensagem.Text.Equals(""))
                ovIB_Enviar.SetImageResource(Resource.Drawable.input_send);
            
        }
    }
}

