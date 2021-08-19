using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using Rebb.Deliveryman.Assets.Fragments;
using Rebb.Client.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;
using AndroidX.Core.App;
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Upload;

namespace Rebb.Deliveryman
{
    [Activity(Label = "RegisterBasicAccount", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterBasicActivity : AppCompatActivity
    {
        View buttonNext;
        TextView txvCondicoesTerm;
        TextInputLayout Email;
        TextInputLayout Name;
        TextInputLayout CPF;
        TextInputLayout PhoneNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_basic);

            txvCondicoesTerm = (TextView)FindViewById(Resource.Id.txvTermo);

            SpannableString ss = new SpannableString(Resources.GetString(Resource.String.text_main_term));
            ForegroundColorSpan fcsBlue = new ForegroundColorSpan(Color.Red);
            ss.SetSpan(fcsBlue, 21, 46, SpanTypes.User);
            txvCondicoesTerm.SetText(ss, TextView.BufferType.Normal);
            // Create your application here

            Email = (TextInputLayout)FindViewById(Resource.Id.TextInputEmail);
            Name = (TextInputLayout)FindViewById(Resource.Id.TextInputName);
            CPF = (TextInputLayout)FindViewById(Resource.Id.TextInputCPF);
            PhoneNumber = (TextInputLayout)FindViewById(Resource.Id.TextInputPhoneNumber);

            buttonNext = FindViewById(Resource.Id.btnNext);
            buttonNext.Click += NextClick;
        }
        public void NextClick(object sender, EventArgs args)
        {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterPasswordActivity));
            ActivityCompat.StartActivity(this, intent, bundle);

            ApiClient client = new ApiClient(null, "Android (Deliveryman/App)");
            Login login = new Client.Core.Models.Login();
            AccountUpload account = new Client.Core.Models.Upload.AccountUpload
            {
                Name = Name.EditText.ToString(),
                Email = Email.ToString(),
                PhoneNumber = PhoneNumber.ToString()
            };
            client.AccountController.CreateAccount(account);
            
      
        }

        private void ShowError(string name, string error)
        {
            TextInputLayout layout;
            switch (name.ToLowerInvariant())
            {
                default:
                    break;
            }
        }
    }
}