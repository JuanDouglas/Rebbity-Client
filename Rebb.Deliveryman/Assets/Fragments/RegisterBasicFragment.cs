using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.Deliveryman.Assets.Fragments
{
    public class RegisterBasicFragment : AppCompatDialogFragment
    {
        TextView txvCondicoesTerm;
        public const string TAG = "RegisterBasicFragment";
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.content_register_basic, container, false);

            txvCondicoesTerm = view.FindViewById<TextView>(Resource.Id.txvTermo);

            SpannableString ss = new SpannableString(Resources.GetString(Resource.String.text_main));
            ForegroundColorSpan fcsBlue = new ForegroundColorSpan(Color.Red);
            ss.SetSpan(fcsBlue, 21, 46, SpanTypes.User);
            txvCondicoesTerm.SetText(ss, TextView.BufferType.Normal);

            return view;
        }
    }
}