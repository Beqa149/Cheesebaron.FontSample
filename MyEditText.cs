using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Cheesebaron.FontSample
{
    public class MyEditText: EditText
    {
        private const string Tag = "EditText";

        protected MyEditText(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public MyEditText(Context context)
            : this(context, null)
        {
        }

        public MyEditText(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public MyEditText(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            var a = context.ObtainStyledAttributes(attrs,
                    Resource.Styleable.CustomFonts);
            var customFont = a.GetString(Resource.Styleable.CustomFonts_customFont);
            SetCustomFont(customFont);
            a.Recycle();
        }

        public void SetCustomFont(string asset)
        {
            Typeface tf;
            try
            {
                tf = Typeface.CreateFromAsset(Context.Assets, asset);
            }
            catch (Exception e)
            {
                Log.Error(Tag, string.Format("Could not get Typeface: {0} Error: {1}", asset, e));
                return;
            }

            if (null == tf) return;

            var tfStyle = TypefaceStyle.Normal;
            if (null != Typeface) //Takes care of android:textStyle=""
                tfStyle = Typeface.Style;
            SetTypeface(tf, tfStyle);
        }
    }
}