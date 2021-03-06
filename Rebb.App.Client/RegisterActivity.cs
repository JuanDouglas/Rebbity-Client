using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
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
using Rebb.App.Client.Assets;
using Rebb.App.Client.Assets.Fragments;
using Rebb.App.Client.Assets.Enums;

namespace Rebb.App.Client
{
    [Activity(Label = "RegisterBasicAccount", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterActivity : AppCompatActivity
    {
        public const string RegisterStepKey = "StartRegisterStep";
        View btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);

            btnBack = FindViewById(Resource.Id.btnBack);
            btnBack.Click += BackButtonClick;

            RegisterStep step = RegisterStep.RegisterBasic;
            if (Intent != null)
            {
                if (Intent.Extras != null)
                {
                    step = (RegisterStep)Intent.Extras.GetInt(RegisterStepKey, 1);
                }
            }
            ByStep(step);
        }

        public void ByStep(RegisterStep step)
        {
            AppCompatDialogFragment fragment = null;
            switch (step)
            {
                case RegisterStep.RegisterBasic:
                    fragment = new RegisterBasicFragment(this);
                    break;
                case RegisterStep.RegisterPassword:
                    fragment = new RegisterPasswordFragment(this, new AccountUpload());
                    break;
                case RegisterStep.RegisterEmail:
                    ISharedPreferences preferences = GetSharedPreferences(Statics.LoginPreferences, FileCreationMode.Private);
                    string email = preferences.GetString("email", null);
                    fragment = new ConfirmEmailFragment(this, email);
                    break;
                case RegisterStep.RegisterDocuments:
                    break;
                default:
                    fragment = new RegisterBasicFragment(this);
                    break;
            }
            SupportFragmentManager.BeginTransaction()
                .SetReorderingAllowed(true)
                .Add(Resource.Id.registerFragment, fragment, RegisterBasicFragment.TAG)
                .Commit();
        }

        public void BackButtonClick(object sender, EventArgs args)
        {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(StartActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
        }
    }
}