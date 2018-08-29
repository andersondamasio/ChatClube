namespace com.chatclube.Widgets
{
    public class EmojiView
    {/*
        private List<EmojiGridAdapter> adapters = new List<EmojiGridAdapter>();

        private int[] icons = {
            Resource.Drawable.ic_emoji_recent,
            Resource.Drawable.ic_emoji_smile,
            Resource.Drawable.ic_emoji_flower,
            Resource.Drawable.ic_emoji_bell,
            Resource.Drawable.ic_emoji_car,
            Resource.Drawable.ic_emoji_symbol };
        private IListener listener;
        private ViewPager pager;
        private FrameLayout recentsWrap;
        private List<GridView> views = new List<GridView>();

        public EmojiView(Context paramContext) : base(paramContext)
        {
            //init();
        }
        public EmojiView(Context paramContext, IAttributeSet paramAttributeSet, int paramInt) : base(paramContext, paramAttributeSet, paramInt)
        {
            //init();
        }

        private void addToRecent(ulong paramLong)
        {
            if (this.pager.CurrentItem == 0)
            {
                return;
            }
            List<ulong> localArrayList = new List<ulong>();
            ulong[] currentRecent = Emoji.data[0];
            Boolean was = false;
            foreach (ulong aCurrentRecent in currentRecent)
            {
                if (paramLong == aCurrentRecent)
                {
                    localArrayList.Insert(0, paramLong);
                    was = true;
                }
                else
                {
                    localArrayList.Add(aCurrentRecent);
                }
            }
            if (!was)
            {
                localArrayList.Insert(0, paramLong);
            }
            Emoji.data[0] = new ulong[Math.Min(localArrayList.Count(), 50)];
            for (int q = 0; q < Emoji.data[0].Length; q++)
            {
                Emoji.data[0][q] = localArrayList[q];
            }
            adapters[0].data = Emoji.data[0];
            adapters[0].notifyDataSetChanged();
            saveRecents();
        }

        private string convert(long paramLong)
        {
            String str = "";
            for (int i = 0; ; i++)
            {
                if (i >= 4)
                {
                    return str;
                }
                int j = (int)(0xFFFF & paramLong >> 16 * (3 - i));
                if (j != 0)
                {
                    str = str + (char)j;
                }
            }
        }

        private void saveRecents()
        {
            List<ulong> localArrayList = new List<ulong>();
            ulong[] arrayOfLong = Emoji.data[0];
            int i = arrayOfLong.Length;
            for (int j = 0; ; j++)
            {
                if (j >= i)
                {
                    Context.GetSharedPreferences("emoji", 0).Edit().PutString("recents", String.Join(",", localArrayList)).Commit();
                    return;
                }
                localArrayList.Add(arrayOfLong[j]);
            }
        }

        public void loadRecents()
        {
            String str = Context.GetSharedPreferences("emoji", 0).GetString("recents", "");
            String[] arrayOfString = null;
            if ((str != null) && (str.Length > 0))
            {
                arrayOfString = str.Split(',');
                Emoji.data[0] = new ulong[arrayOfString.Length];
            }
            if (arrayOfString != null)
            {
                for (int i = 0; i < arrayOfString.Length; i++)
                {
                    Emoji.data[0][i] = Convert.ToUInt64(arrayOfString[i]);
                }
                adapters[0].data = Emoji.data[0];
                adapters[0].notifyDataSetChanged();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(View.MeasureSpec.MakeMeasureSpec(View.MeasureSpec.GetSize(widthMeasureSpec), MeasureSpecMode.Exactly), MeasureSpec.MakeMeasureSpec(View.MeasureSpec.GetSize(heightMeasureSpec), MeasureSpecMode.Exactly));
        }

        public void setListener(IListener paramListener)
        {
            this.listener = paramListener;
        }


        public void invalidateViews()
        {
            foreach (GridView gridView in views)
            {
                if (gridView != null)
                {
                    gridView.InvalidateViews();
                }
            }
        }

        private class EmojiGridAdapter : BaseAdapter
        {
            ulong[] data;
            EmojiView emojiView;

            public EmojiGridAdapter(ulong[] arg2, EmojiView emojiView)
            {
                this.data = arg2;
                this.emojiView = emojiView;
            }

            public override int Count => data.Length;

            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                ulong i = Convert.ToUInt64(position);

                return (long)data[i];
            }

            public override View GetView(int position, View view, ViewGroup parent)
            {
                ulong i = Convert.ToUInt64(position);



                ImageView imageView = (ImageView)view;

                if (imageView == null)
                {
                    imageView = new ImageView(emojiView.Context)
                    {
                        //imageView.Measure(View.MeasureSpec.GetSize( MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                    };
                    imageView.Measure(41, 5);

                    imageView.Click += ImageView_Click;

                    imageView.SetBackgroundResource(Resource.Drawable.list_selector);
                    imageView.SetScaleType(ImageView.ScaleType.Center);

                }

                imageView.SetImageDrawable(Emoji.getEmojiBigDrawable(data[i]));
                imageView.Tag = (long)data[i];

                return imageView;
            }

            private void ImageView_Click(object sender, EventArgs e)
            {
                if (emojiView.listener != null) {
                    emojiView.listener.OnEmojiSelected(emojiView.convert(Convert.ToInt64(((View)sender).Tag)));
                }
            }


            public override void UnregisterDataSetObserver(DataSetObserver observer)
            {
                if (observer != null) {
                    base.UnregisterDataSetObserver(observer);
                }
            }

        }

        private class EmojiPagesAdapter : PagerAdapter
        {
            public override int Count => 10;//views.size();

            public override bool IsViewFromObject(View view, Java.Lang.Object @object)
            {
                return view == @object;
            }
        }

    }

    public interface IListener
    {
        void OnBackspace();
        void OnEmojiSelected(string paramString);
    }
}*/
    }
}