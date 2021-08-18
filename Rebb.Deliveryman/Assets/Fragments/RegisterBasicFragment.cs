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
using Google.Android.Material.TextField;
using Rebb.Client.Core.Models.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.Deliveryman.Assets.Fragments
{
    public class RegisterBasicFragment : AppCompatDialogFragment
    {
        TextView txvCondicoesTerm;
        View btnNext;
        TextInputLayout Email;
        TextInputLayout Name;
        TextInputLayout PhoneNumber;
        public const string TAG = "RegisterBasicFragment";
        public AppCompatActivity CompatActivity { get; set; }
        public RegisterBasicFragment(AppCompatActivity activity)
        {
            CompatActivity = activity;
        }
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
            btnNext = view.FindViewById(Resource.Id.btnNext);
            Email = view.FindViewById<TextInputLayout>(Resource.Id.TextInputEmail);
            Name = view.FindViewById<TextInputLayout>(Resource.Id.TextInputName);
            PhoneNumber = view.FindViewById<TextInputLayout>(Resource.Id.TextInputPhoneNumber);

            btnNext.Click += NextClick;

            SpannableString ss = new SpannableString(Resources.GetString(Resource.String.text_main));
            ForegroundColorSpan fcsBlue = new ForegroundColorSpan(Color.Red);
            ss.SetSpan(fcsBlue, 21, 46, SpanTypes.User);
            txvCondicoesTerm.SetText(ss, TextView.BufferType.Normal);
            return view;
        }

        private void NextClick(object sender, EventArgs args)
        {
            Email.EditText.Text ??= string.Empty;
            Name.EditText.Text ??= string.Empty;
            PhoneNumber.EditText.Text ??= string.Empty;

            AccountUpload account = new AccountUpload()
            {
                Email = Email.EditText.Text,
                Name = Name.EditText.Text,
                PhoneNumber = PhoneNumber.EditText.Text
            };

            RegisterPasswordFragment fragment = new RegisterPasswordFragment(CompatActivity, account);
            CompatActivity.SupportFragmentManager.BeginTransaction()
                .SetReorderingAllowed(true)
                .SetCustomAnimations(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_fade_out)
                .Replace(Resource.Id.registerFragment, fragment, RegisterPasswordFragment.TAG)
                .Commit();
        }
    }
}