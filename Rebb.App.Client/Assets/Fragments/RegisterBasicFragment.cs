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
using Rebb.Client.Core.Models.Result;
using Rebb.Client.Core.Models.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace Rebb.App.Client.Assets.Fragments
{
    public class RegisterBasicFragment : RegisterFragment
    {
        TextView txvCondicoesTerm;
        View btnNext;
        TextInputLayout Email;
        TextInputLayout Name;
        TextInputLayout PhoneNumber;
        CheckBox AcceptTerms;
        public const string TAG = "RegisterBasicFragment";
        ValidationErrorsResult Errors;

        public AccountUpload Account { get; set; }
        public RegisterBasicFragment(AppCompatActivity activity) : base(activity)
        {
            Account = new AccountUpload();
        }
        public RegisterBasicFragment(ValidationErrorsResult result, AppCompatActivity activity) : this(activity)
        {
            Errors = result;
        }

        public RegisterBasicFragment(ValidationErrorsResult result, AppCompatActivity activity, AccountUpload account) : this(result,activity)
        {
            Account = account;
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
            View view = inflater.Inflate(Resource.Layout.fragment_register_basic, container, false);

            txvCondicoesTerm = view.FindViewById<TextView>(Resource.Id.txvTermo);
            btnNext = view.FindViewById(Resource.Id.btnNext);
            Email = view.FindViewById<TextInputLayout>(Resource.Id.TextInputEmail);
            Name = view.FindViewById<TextInputLayout>(Resource.Id.TextInputName);
            PhoneNumber = view.FindViewById<TextInputLayout>(Resource.Id.TextInputPhoneNumber);
            AcceptTerms = view.FindViewById<CheckBox>(Resource.Id.checkAcceptTerms);

            string emp = string.Empty;
            if (Account != null)
            {
                Email.EditText.Text = Account.Email ?? emp;
                PhoneNumber.EditText.Text = Account.PhoneNumber ?? emp;
                Name.EditText.Text = Account.Name ?? emp;
                AcceptTerms.Checked = Account.AcceptTerms;
            }

            btnNext.Click += NextClick;
            Email.EditText.TextChanged += ErrorDisableClick;
            PhoneNumber.EditText.TextChanged += ErrorDisableClick;
            Name.EditText.TextChanged += ErrorDisableClick;
            PhoneNumber.EditText.TextChanged += PhoneTextChangedEvent;
           
            if (Errors != null)
                ShowErrors(Errors);

            AlterProgress(500);
            return view;
        }
        private void NextClick(object sender, EventArgs args)
        {
            Email.EditText.Text ??= string.Empty;
            Name.EditText.Text ??= string.Empty;
            PhoneNumber.EditText.Text ??= string.Empty;

            if (!AcceptTerms.Checked)
            {
                string error = Resources.GetString(Resource.String.text_terms_required);

                AlertDialog.Builder builder = new AlertDialog.Builder(Context);
                builder.SetMessage(error);

                builder.Show();
            }

            if (Email.ErrorEnabled || PhoneNumber.ErrorEnabled || Name.ErrorEnabled)
            {
                Vibration.Vibrate(250);
                return;
            }

            AccountUpload account = new AccountUpload()
            {
                Email = Email.EditText.Text,
                Name = Name.EditText.Text,
                PhoneNumber = PhoneNumber.EditText.Text,
                AcceptTerms = AcceptTerms.Checked
            };

            RegisterPasswordFragment fragment = new RegisterPasswordFragment(CompatActivity, account);
            CompatActivity.SupportFragmentManager.BeginTransaction()
                .SetReorderingAllowed(true)
                .SetCustomAnimations(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_fade_out)
                .Replace(Resource.Id.registerFragment, fragment, RegisterPasswordFragment.TAG)
                .Commit();
        }


        public void ShowError(TextInputLayout layout, string error)
        {
            layout.Error = error;
            layout.ErrorEnabled = true;
        }

        public void HideError(TextInputLayout layout)
        {
            layout.Error = string.Empty;
            layout.ErrorEnabled = false;
        }

        public void ShowErrors(ValidationErrorsResult validationErrorsResult)
        {
            var errors = validationErrorsResult.Errors;
            foreach (var keyUper in errors.Keys)
            {
                TextInputLayout input = null;
                string key = keyUper.ToLowerInvariant();
                if (key == nameof(Email).ToLowerInvariant())
                    input = Email;

                if (key == nameof(PhoneNumber).ToLowerInvariant())
                    input = PhoneNumber;

                if (key == nameof(Name).ToLowerInvariant())
                    input = Name;

                if (key == nameof(AcceptTerms).ToLowerInvariant())
                {
                    AcceptTerms.Error = errors[keyUper][0];
                }

                if (input != null)
                {
                    ShowError(input, errors[keyUper][0]);
                }
            }

        }

        public void ErrorDisableClick(object sender, EventArgs args)
        {
            if (sender is TextInputEditText)
            {
                TextInputEditText editText = sender as TextInputEditText;

                int id = editText.Id;
                if (id == Resource.Id.edtEmail)
                {
                    HideError(Email);
                }

                if (id == Resource.Id.edtPhone)
                {
                    HideError(PhoneNumber);
                }

                if (id == Resource.Id.edtName)
                {
                    HideError(Name);
                }
            }
        }
        public void PhoneTextChangedEvent(object sender, TextChangedEventArgs args)
        {
            string text = args.Text.ToString();
        }
    }
}